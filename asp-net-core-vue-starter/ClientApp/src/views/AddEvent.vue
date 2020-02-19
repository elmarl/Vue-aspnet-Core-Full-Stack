<!-- 
    Aim is to submit form by typescript, to enable posting via axios and rerouting
    to the index page.
    To prevent enter from submitting the form, using v-on:keydown.enter.prevent=""
    -->
    <template>   
        <v-container fluid fill-height class="container sheet" style="text-align:center">
            <v-row no-gutters class="subheader">
                <v-col cols="4">
                    <div class="pa-2">
                        <h2 class="notbold">Ürituse Lisamine</h2>
                    </div>
                </v-col>
                <v-col cols="8">
                    <div class="pa-0">
                        <v-img class="subheaderimg" src="../assets/libled.jpg"></v-img>
                    </div>
                </v-col>
            </v-row>
            <h2 class="col-sm-6 blue-text">Ürituse lisamine</h2>
            <form class="form-horizontal col-sm-6 offset-sm-2" @submit.prevent="">
                <div class=" row pa-1">
                    <label class="col pa-1" for="name">Nimi</label>
                    <input type="text" class=" col pa-1" id="name" name="name" style="border-style:ridge" v-on:keydown.enter.prevent="submit()" required>
                </div>
                <div class=" row pa-1">
                    <label class=" col pa-1" for="date">Kuupäev:</label>
                    <input type="datetime-local" class=" col pa-1" v-bind:min="getTime()" id="date" style="border-style:ridge" v-on:keydown.enter.prevent="submit()" required>
                </div>
                <div class=" row pa-1">
                    <label class="col pa-1" for="location">Koht</label>
                    <input type="text" class=" col pa-1" id="location" name="location" style="border-style:ridge" v-on:keydown.enter.prevent="submit()" required>
                </div>
                <div class=" row pa-1">
                    <label class=" col pa-1" for="details">Lisainfo</label>
                    <textarea type="text" class="col pa-1" id="details" name="details" style="border-style:ridge" v-on:keydown.enter.prevent="submit()"></textarea>
                </div>
                <div class="row pa-1">
                    <router-link to="/" tag="button" class="col pa-1">Tagasi</router-link>
                    <input type="submit" class="col blue-text pa-1" @click.stop.prevent="submit()" value="Lisa">
                    <!-- @click.stop.prevent="submit()"-->
                </div>
            </form>

        </v-container>
    </template>
<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import { Event } from '../models/Event';
    import axios from 'axios';
    import BaseUrl from '../BaseUrl/BaseUrl';

    @Component({})
    export default class FetchDataView extends Vue {
        //private event_details: Events = new Events();
        // In order to set the min date for date selector, the min date has to be in the format: yyyy-mm-ddThh:mm
        private getTime() {
            const date: Date = new Date();
            let y: string = date.getFullYear().toString();
            let m: string = (date.getMonth() + 1).toString();
            let d: string = date.getDate().toString();
            let time: string = 'T' + ('0' + date.getHours()).slice(-2) + ':' + ('0' + date.getMinutes()).slice(-2);
            (d.length == 1) && (d = '0' + d);
            (m.length == 1) && (m = '0' + m);
            const formatted_date: string = y + '-' + m + '-' + d + time;
            return formatted_date;
        }
        private async submit() {
            const nameinput = (document.getElementById('name') as HTMLInputElement).value;
            const dateinput = (document.getElementById('date') as HTMLInputElement).value;
            const locationinput = (document.getElementById('location') as HTMLInputElement).value;
            const detailsinput = (document.getElementById('details') as HTMLTextAreaElement).value; 
            // ignore id when posting a new event, using undefined
            const inputevent: Event = new Event(undefined, nameinput, dateinput, locationinput, detailsinput);
            // var msg = JSON.stringify(inputevent);
            try {
                const result: any = await axios.post(BaseUrl, inputevent);
            }
            catch(e) {
                alert('error posting data');
                return;
            }
            this.$router.push('/');
        }
    }
</script>

<style scoped>

    form {
        display: inline-block;
    }
    .sheet{
        background-color:white;
        margin-top: 10px;
    }
    .blue-text{
        color:royalblue;
    }
    .subheader{
        padding:0px;
        margin:0px;
        width:auto;
        background-color:royalblue;
        color:white;
    }
    .subheaderimg {
        width:100%;
        max-height:65px
    }
    .notbold {
        font-weight:normal;
        line-height:45px;
    }
</style>
