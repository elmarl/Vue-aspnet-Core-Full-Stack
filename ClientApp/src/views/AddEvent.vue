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
                        Urituse Lisamine
                    </div>
                </v-col>
                <v-col cols="8">
                    <div class="pa-2">
                        <v-img class="subheaderimg" src="../assets/libled.jpg"></v-img>
                    </div>
                </v-col>
            </v-row>
            <h2 class="col-sm-6 offset-sm-2 blue-text">Urituse lisamine</h2>
            <form class="form-horizontal col-sm-6 offset-sm-4" @submit.prevent="">
                <div class="form-group row">
                    <label class="control-label col-sm-2" for="name">Nimi</label>
                    <input type="text" class="form-control col-sm-8" id="name" name="name" style="border-style:ridge" v-on:keydown.enter.prevent="" required>
                </div>
                <div class="form-group row">
                    <label class="control-label col-sm-2" for="date">Kuupaev:</label>
                    <input type="datetime-local" class="form-control col-sm-8" v-bind:min="getTime()" id="date" style="border-style:ridge" v-on:keydown.enter.prevent="" required>
                </div>
                <div class="form-group row">
                    <label class="control-label col-sm-2" for="location">Koht</label>
                    <input type="text" class="form-control col-sm-8" id="location" name="location" style="border-style:ridge" v-on:keydown.enter.prevent="" required>
                </div>
                <div class="form-group row">
                    <label class="control-label col-sm-2" for="details">Lisainfo</label>
                    <textarea type="text" class="form-control col-sm-8" id="details" name="details" style="border-style:ridge" v-on:keydown.enter.prevent=""></textarea>
                </div>
                <div class="form-group row">
                    <router-link to="/" tag="button" class="col-sm-2">Tagasi</router-link>
                    <input type="submit" class="form-control col-sm-8" @click.stop.prevent="submit()">
                    <!-- @click.stop.prevent="submit()"-->
                </div>
            </form>

        </v-container>
    </template>
<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import { Event } from '../models/Event';
    import axios from 'axios';

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
        public async submit() {
            alert("form entered");
            const nameinput = (document.getElementById('name') as HTMLInputElement).value;
            const dateinput = (document.getElementById('date') as HTMLInputElement).value;
            const locationinput = (document.getElementById('location') as HTMLInputElement).value;
            const detailsinput = (document.getElementById('details') as HTMLTextAreaElement).value; 
            // ignore id when posting a new event, using undefined
            const inputevent: Event = new Event(undefined, nameinput, dateinput, locationinput, detailsinput);
            // var msg = JSON.stringify(inputevent);
            try {
                const result: any = await axios.post('api/Events', inputevent);
            }
            catch(e) {
                alert('error posting data');
            }
            this.$router.push("/");
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
        color:midnightblue;
    }
    .subheader{
        padding:0px;
        margin:0px;
        width:auto;
    }
    .subheaderimg {
        width:100%;
        max-height:40px
    }
</style>
