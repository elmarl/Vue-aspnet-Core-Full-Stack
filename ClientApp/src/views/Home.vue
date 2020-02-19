<!--
                                                // vormi sisestamine toimub 'axios'-t kasutades, teeb GET, POST, PUT ja DELETE http requeste
    v-on:keydown.enter.prevent="submitperson()" // vormi sisestamisel enter klahviga vaikimisi actioni (post ja refresh) asemel suunab funktsioonile
    @click.stop.prevent="submitperson()"        // sama mis eelmine, ainult submit nupu hiirega klikkimise jaoks
    -->

<template>
    <v-container fluid fill-height class="container">
        <div class="container">
            <v-layout row>
                <v-flex xs6 order-lg1>
                    <v-card dark tile flat class="primary bkg" height="100%">
                        <div class="center-text white-text">
                            Lorem ipsum dolor sit amet, <b>consectetur</b> adipiscing elit. Aliquam euismod maximus odio eu vehicula. Nulla feugiat,
                            arcu non sodales varius, leo felis <i><b>faucibus</b></i> lorem, sed convallis odio dolor vitae elit. Sed non orci cursus, varius
                            ante vitae, finibus urna.
                        </div>
                    </v-card>
                </v-flex>
                <v-flex xs6 order-lg2>
                    <v-card dark tile flat class="primary" height="100%">
                        <v-img src="../assets/pingid.jpg"></v-img>
                    </v-card>
                </v-flex>
            </v-layout>
        </div>
        <div class="container">
            <div class="row ">
                <table class=" col-sm-6 sheet">

                    <th class="center-text bkg">Tulevased üritused</th>
                    <tr v-bind:key="i.id" v-for="(i, index) in computefutureevent('future')">
                        <Event_comp :eventitem="i" :index="index + 1" :ispastevent="false" v-on:del_item="del_item" v-on:edit_item="edit_item" />
                    </tr>

                </table>
                <table class=" col-sm-6 sheet">

                    <th class="center-text bkg">Toimunud üritused</th>
                    <tr v-bind:key="i.id" v-for="(i, index) in computefutureevent('past')">
                        <Event_comp :eventitem="i" :index="index + 1" v-bind:ispastevent="true" v-on:del_item="del_item" v-on:edit_item="edit_item" />
                    </tr>

                </table>
            </div>
        </div>
        </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Event_comp from '@/components/Event_comp.vue';
import { Event } from '../models/Event';
import axios from 'axios';
import BaseUrl from '../BaseUrl/BaseUrl';

@Component({
  components: {
        Event_comp,
  },
})

export default class Home extends Vue {
    private events: Event[] = [];
    private errorMessage: string = 'Error while loading event data.';

    private async created() {
        await this.fetchEvents();
    }
    private async fetchEvents() {
        try {
            const response = await axios.get<Event[]>(BaseUrl);
            const res = response.data;
            for (let i = 0; i < res.length; i++) {
                this.events.push(new Event(res[i].eventid, res[i].eventName,
                    res[i].eventDate, res[i].location, res[i].details));
            }
        } catch (e) {
            alert('Error loading events');
        }
    }
    private async del_item(id: number) {
        // delete on back end using path: 'api/Events/{id}
        try {
            const path: string = BaseUrl;
            const url: string = path.concat(id.toString());
            const response = await axios.delete(url);
            // delete on front end
            const index: number = this.events.findIndex((x) => x.eventid === id);
            this.events.splice(index, 1);
        } catch (e) {
            alert('Error deleting event');
        }
    }
    private computefutureevent(temp: string) {
        if (temp === 'future') {
            return this.events.filter(this.computefuturestatus);
        } else if (temp === 'past') {
            return this.events.filter(this.computepaststatus);
        }
    }
    private computepaststatus(item: Event) {
        const nowDate: Date = new Date();
        if (new Date(item.eventDate || '') < nowDate) {
            return true;
        } else {
            return false;
        }
    }
    private computefuturestatus(item: Event) {
        const nowDate: Date = new Date();
        if (new Date(item.eventDate || '') > nowDate) {
            return true;
        } else {
            return false;
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
    .bkg{
        color:white;
        background-color: royalblue;
    }
    .sheet{
        background-color:white;
        margin-top: 10px;
        margin-left: 0px;
        padding: 0px;
    }
</style>