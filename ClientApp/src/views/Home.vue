<template>
    <v-container fluid fill-height class="container">
        <v-container class="container">
            <v-layout row>
                <v-flex xs6 order-lg1>
                    <v-card dark tile flat color="indigo" class="primary" height="100%">
                        <v-card-text class="center-text white-text">
                            Lorem ipsum dolor sit amet, <b>consectetur</b> adipiscing elit. Aliquam euismod maximus odio eu vehicula. Nulla feugiat,
                            arcu non sodales varius, leo felis <i><b>faucibus</b></i> lorem, sed convallis odio dolor vitae elit. Sed non orci cursus, varius
                            ante vitae, finibus urna.
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs6 order-lg2>
                    <v-card dark tile flat class="primary" height="100%">
                        <v-img src="../assets/pingid.jpg"></v-img>
                    </v-card>
                </v-flex>
            </v-layout>
        </v-container>

        <!--<v-alert :value="showError" type="error" v-text="errorMessage">
            This is an error alert.
        </v-alert>

        <v-alert :value="showError" type="warning">
            Are you sure you're using ASP.NET Core endpoint? (default at <a href="http://localhost:5000/fetch-data">http://localhost:5000</a>)<br>
            API call would fail with status code 404 when calling from Vue app (default at <a href="http://localhost:8080/fetch-data">http://localhost:8080</a>) without devServer proxy settings in vue.config.js file.
        </v-alert>-->
        <!--added container class to container in App.vue to keep row from being too wide-->
            <v-row>
                <v-col cols="6">
                    <v-card class="pa-2" outlined tile>
                        <div class="center-text bkg">Yritused</div>
                        <div v-bind:key="i.id" v-for="i in events">
                            <Event_comp :eventitem="i" v-on:del_item="del_item" v-on:edit_item="edit_item"/>
                        </div>
                    </v-card>
                </v-col>
                <v-col cols="6">
                    <v-card class="pa-2" outlined tile>
                        .col-6
                    </v-card>
                </v-col>
            </v-row>
        </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Event_comp from '@/components/Event_comp.vue';
import { Event } from '../models/Event';
import axios from 'axios';

@Component({
  components: {
        Event_comp,
  },
})

export default class Home extends Vue {
    private events: Event[] = [];
    private errorMessage: string = 'Error while loading event data.';
    private showError: boolean = false;
    private loading: boolean = true;

    private async created() {
        await this.fetchEvents();
  }
    private async fetchEvents() {
        const dt = new Date();
        const mydate = dt.getFullYear() + '/' + (dt.getMonth() + 1) + '/' + dt.getDate();
        this.events.push(new Event(0, 'test', dt.toISOString(), 'tll', 'det'));
        this.events.push(new Event(1, 'test2', dt.toISOString(), 'par', 'det')); 
        try {
            const response = await axios.get<Event[]>('api/Events');
            const res = response.data;
           
            for (let i = 0; i < res.length; i++) {
                this.events.push(new Event(res[i].id, res[i].eventName, res[i].eventDate, res[i].location, res[i].details));
            }
        } catch (e) {
            this.showError = true;
            this.errorMessage = `Error while loading events: ${e.message}.`;
        }
        this.loading = false;
    }
    private async del_item(id: number) {
        //delete on front end
        const index: number = this.events.findIndex((x) => x.id === id);
        this.events.splice(index, 1);
        //delete on back end using path: 'api/Events/{id}
        try {
            let path: string = 'api/Events/';
            let url: string = path.concat(id.toString());
            const response = await axios.delete(url); //axios.get<Events[]>('api/Events');
            // this.events = response.data;
        } catch (e) {
            this.showError = true;
            this.errorMessage = `Error while deleting event: ${e.message}.`;
            alert(this.errorMessage);
        }
    }
    private edit_item(id: number) {
        // alert(id);
    }
}
</script>

<style scoped>
    img {
        width:100%;
        height:auto;
    }
    .center-text{
        text-align:center;
        vertical-align:middle;
        padding:10px;
       top:50%;
    }
    .white-text {
         color:white;
    }
    bkg{
        background-color: darkcyan;
    }

</style>