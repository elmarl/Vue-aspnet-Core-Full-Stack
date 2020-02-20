<template>
    <v-container fluid fill-height class="container sheet" style="">
        <v-row no-gutters class="subheader">
            <v-col cols="4">
                <div class="pa-2">
                    <h2 class="notbold">Osavõtja info</h2>
                </div>
            </v-col>
            <v-col cols="8">
                <div class="pa-0">
                    <v-img class="subheaderimg" src="../assets/libled.jpg"></v-img>
                </div>
            </v-col>
        </v-row>
        <h2 class="container blue-text offset-sm-2">Osaleja</h2>
        <form id="personform" style="display:none" class="container col-sm-6 myrow">
            <div class="row pa-1">
                <label for="firstname" class="col pa-1">Eesnimi</label>
                <input class="col pa-1" id="firstname" :value="participant.firstname" />
            </div>
            <div class="row pa-1">
                <label for="familyname" class="col pa-1">Perekonnanimi</label>
                <input class="col pa-1" id="familyname" :value="participant.familyname" />
            </div>
            <div class="row pa-1">
                <label for="personidcode" class="col pa-1">Isikukood</label>
                <input class="col pa-1" id="personidcode" :value="participant.idcode" />
            </div>
            <div class="row pa-1">
                <label for="personpaymentmethod" class="col pa-1">Maksmisviis</label>
                <select class="col pa-1" id="personpaymentmethod" :value="participant.paymentmethod"><option>Sularaha</option><option>Pangaülekanne</option></select>
            </div>
            <div class="row pa-1">
                <label for="persondetails" class="col pa-1">Lisainfo</label>
                <textarea class="col pa-1" id="persondetails" :value="participant['detailsPerson']"></textarea>
            </div>
            <div class="row pa-1">
                <a id="routerbtn" class="col pa-1" href="javascript:history.go(-1)">Tagasi</a>
                <input type="submit" class="col pa-1" @click.stop.prevent="savePersonChanges()" value="Salvesta" />
            </div>
        </form>
        <form id="companyform" style="display:none" class="container col-sm-6 myrow">
            <div class="row pa-1">
                <label for="companyname" class="col pa-1">Ettevõtte nimi</label>
                <input class="col pa-1" id="companyname" :value="participant.firstname" />
            </div>
            <div class="row pa-1">
                <label for="companyidcode" class="col pa-1">Registrikood</label>
                <input class="col pa-1" id="companyidcode" :value="participant.idcode" />
            </div>
            <div class="row pa-1">
                <label for="companynumparticipants" class="col pa-1">Osalejate arv</label>
                <input class="col pa-1" id="companynumparticipants" :value="participant.numParticipants" />
            </div>
            <div class="row pa-1">
                <label for="companypaymentmethod" class="col pa-1">Maksmisviis</label>
                <select class="col pa-1" id="companypaymentmethod" :value="participant.paymentmethod"><option>Sularaha</option><option>Pangaülekanne</option></select>
            </div>
            <div class="row pa-1">
                <label for="companydetails" class="col pa-1">Lisainfo</label>
                <textarea class="col pa-1" id="companydetails" :value="participant['detailsCompany']" />
            </div>
            <div class="row pa-1">
                <a id="routerbtn" class="col pa-1" href="javascript:history.go(-1)">Tagasi</a>
                <input type="submit" class="col pa-1" @click.stop.prevent="saveCompanyChanges()" value="Salvesta" />
            </div>
        </form>
    </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';
import { Participant } from '../models/Participant';
import { Event } from '../models/Event';
import BaseUrl from '../BaseUrl/BaseUrl';

@Component
export default class EditParticipant extends Vue {
    private participant: Participant = new Participant(0, '', '', '', 0, '', '', '', '', new Event(0, '', '', '', ''));
    private async created() {
    await this.fetchParticipant();
  }

    private async fetchParticipant() {
        try {
            const eventid = window.location.href.split('/').slice(-3)[0];
            const participantid = window.location.href.split('/').slice(-1)[0];
            const response = await axios.get<Participant>(
                BaseUrl.concat(eventid).concat('/participants/').concat(participantid));
            this.participant = response.data;
        } catch (e) {
            alert('error loading participant');
        }
        if (this.participant.participantType === 'company') {
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
                paymentmethodinput, detailsinput, '', 'person', myevent);
            const response = await axios.put<Participant>(
                BaseUrl.concat(eventid.toString()).concat('/participants/').concat(participantid), inputperson);
        } catch (e) {
            alert('error updating participant');
        }
        history.go(-1);
    }
    private async saveCompanyChanges() {
        try {
            const eventid = parseInt(window.location.href.split('/').slice(-3)[0], 10);
            const participantid = window.location.href.split('/').slice(-1)[0];

            const firstnameinput = (document.getElementById('companyname') as HTMLInputElement).value;
            const numparticipantsinput = parseInt((document.getElementById('companynumparticipants') as HTMLInputElement).value, 10);
            const idcodeinput = (document.getElementById('companyidcode') as HTMLInputElement).value;
            const paymentmethodinput = (document.getElementById('personpaymentmethod') as HTMLSelectElement).value;
            const detailsinput = (document.getElementById('companydetails') as HTMLTextAreaElement).value;
            const myevent: Event = new Event(eventid, null, null, null, null);
            const inputcompany = new Participant(0, firstnameinput, '', idcodeinput, numparticipantsinput,
            paymentmethodinput, '', detailsinput,'company', myevent);
            const response = await axios.put<Participant>(
                BaseUrl.concat(eventid.toString()).concat('/participants/').concat(participantid), inputcompany);
        } catch (e) {
            alert('error updating participant');
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