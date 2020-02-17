<template>
    <v-container fluid fill-height class="container" style="">
        <h2 class="container">Osaleja</h2>
        <div id="persondata" style="visibility:hidden" class="container col-sm-6 myrow">
            <div class="row">
                <span class="col">Eesnimi</span>
                <input class="col" id="firstname" :value="participant.firstname" />
            </div>
            <div class="row">
                <span class="col">Perekonnanimi</span>
                <input class="col" id="familyname" :value="participant.familyname" />
            </div>
            <div class="row">
                <span class="col">Isikukood</span>
                <input class="col" id="personidcode" :value="participant.idcode" />
            </div>
            <div class="row">
                <span class="col">Maksmisviis</span>
                <select class="col" id="personpaymentmethod" :value="participant.paymentmethod"><option>Sularaha</option><option>Kaardimakse</option></select>
            </div>
            <div class="row">
                <span class="col">Lisainfo</span>
                <textarea class="col" id="persondetails" :value="participant.details" />
            </div>
            <div class="row">
                <a id="routerbtn" class="col" href="javascript:history.go(-1)">Tagasi</a>
                <input type="submit" class="col" @click.stop.prevent="saveChanges()" />
            </div>
        </div>
     
    </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';
import { Participant } from '../models/Participant';
import { Event } from '../models/Event';
@Component
export default class EditParticipant extends Vue {
    private participant: Participant = new Participant(0,'','','',0,'','','', new Event(0, '','','',''));
    private async created() {
    await this.fetchParticipant();
  }

    private async fetchParticipant() {
        try {const eventid = window.location.href.split('/').slice(-3)[0];
            const participantid = window.location.href.split('/').slice(-1)[0];
            const response = await axios.get<Participant>('https://localhost:5001/api/Events/' + eventid + '/Participants/' + participantid);
            this.participant = response.data;
        } catch (e) {
            alert("error loading participant");
        }
        (document.getElementById('persondata') as HTMLDivElement).style.visibility = 'visible';
    }
    private async saveChanges() {
        try {
            const eventid = parseInt(window.location.href.split('/').slice(-3)[0], 10);
            const participantid = window.location.href.split('/').slice(-1)[0];

            const firstnameinput = (document.getElementById('firstname') as HTMLInputElement).value;
            const familynameinput = (document.getElementById('familyname') as HTMLInputElement).value;
            const idcodeinput = (document.getElementById('personidcode') as HTMLInputElement).value;
            const paymentmethodinput = (document.getElementById('personpaymentmethod') as HTMLSelectElement).value;
            const detailsinput = (document.getElementById('persondetails') as HTMLTextAreaElement).value;
            const myevent: Event = new Event(eventid, null, null, null, null);
            const inputperson = new Participant(0, firstnameinput, familynameinput, idcodeinput, 0,
            paymentmethodinput, detailsinput, 'person', myevent);

            const response = await axios.put<Participant>('https://localhost:5001/api/Events/' + eventid + '/Participants/' + participantid, inputperson);
                
        } catch (e) {
            alert("error updating participant");
        }
        history.go(-1);
    }
}
</script>

<style scoped>
    .myrow > div > span, .myrow > div > input,
    .myrow > div > select,
    .myrow > div > #routerbtn {
        padding-bottom:5px;
        padding-top:5px;
    }
    input, textarea, select {
        border-style:ridge;
    }
</style>