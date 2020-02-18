<template>
    <v-container fluid fill-height class="container sheet" style="">
        <h2 class="container">Osaleja</h2>
        <div id="personform" style="display:none" class="container col-sm-6 myrow">
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
                <input type="submit" class="col" @click.stop.prevent="savePersonChanges()" />
            </div>
        </div>
        <div id="companyform" style="display:none" class="container col-sm-6 myrow">
            <div class="row">
                <span class="col">Ettevõtte nimi</span>
                <input class="col" id="companyname" :value="participant.firstname" />
            </div>
            <div class="row">
                <span class="col">Registrikood</span>
                <input class="col" id="companyidcode" :value="participant.idcode" />
            </div>
            <div class="row">
                <span class="col">Osalejate arv</span>
                <input class="col" id="companynumparticipants" :value="participant.numParticipants" />
            </div>
            <div class="row">
                <span class="col">Maksmisviis</span>
                <select class="col" id="companypaymentmethod" :value="participant.paymentmethod"><option>Sularaha</option><option>Kaardimakse</option></select>
            </div>
            <div class="row">
                <span class="col">Lisainfo</span>
                <textarea class="col" id="companydetails" :value="participant.details" />
            </div>
            <div class="row">
                <a id="routerbtn" class="col" href="javascript:history.go(-1)">Tagasi</a>
                <input type="submit" class="col" @click.stop.prevent="saveCompanyChanges()" />
            </div>
        </div>
    </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';
import { Participant } from '../models/Participant';
import { Event } from '../models/Event';
import BaseUrl from '../NewFolder/BaseUrl'

@Component
export default class EditParticipant extends Vue {
    private participant: Participant = new Participant(0,'','','',0,'','','', new Event(0, '','','',''));
    private async created() {
    await this.fetchParticipant();
  }

    private async fetchParticipant() {
        try {const eventid = window.location.href.split('/').slice(-3)[0];
            const participantid = window.location.href.split('/').slice(-1)[0];
            const response = await axios.get<Participant>(BaseUrl + eventid + '/participants/' + participantid);
            this.participant = response.data;
        } catch (e) {
            alert("error loading participant");
        }
        if (this.participant.participantType == 'company') {
            (document.getElementById('companyform') as HTMLDivElement).style.display = 'block';
        } else {
            (document.getElementById('personform') as HTMLDivElement).style.display = 'block';
        }
        
    }
    private async savePersonChanges() {
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
            const response = await axios.put<Participant>(BaseUrl + eventid + '/participants/' + participantid, inputperson);    
        } catch (e) {
            alert("error updating participant");
        }
        history.go(-1);
    }
    private async saveCompanyChanges() {
        const eventid = parseInt(window.location.href.split('/').slice(-3)[0], 10);
        const participantid = window.location.href.split('/').slice(-1)[0];

        const firstnameinput = (document.getElementById('companyname') as HTMLInputElement).value;
        const numparticipantsinput = parseInt((document.getElementById('companynumparticipants') as HTMLInputElement).value, 10);
        const idcodeinput = (document.getElementById('companyidcode') as HTMLInputElement).value;
        const paymentmethodinput = (document.getElementById('personpaymentmethod') as HTMLSelectElement).value;
        const detailsinput = (document.getElementById('companydetails') as HTMLTextAreaElement).value;
        const myevent: Event = new Event(eventid, null, null, null, null);
        const inputcompany = new Participant(0, firstnameinput, '', idcodeinput, numparticipantsinput,
        paymentmethodinput, detailsinput, 'company', myevent);

        const response = await axios.put<Participant>(BaseUrl + eventid + '/participants/' + participantid, inputcompany);
                
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
    .sheet{
        background-color:white;
        margin-top: 10px;
    }
</style>