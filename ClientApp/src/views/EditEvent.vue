<!--
    entityform - juriidilise isiku vorm
    personform - fuusilise isiku vorm
-->
<template>
    <v-container fluid fill-height class="container" style="">
        <h2 class="container">Yritus</h2>
        <div class="container col-sm-6 myrow">
            <div class="row">
                <span class="col">Urituse nimi</span>
                <span class="col">{{eventitem.eventName}}</span>
            </div>
            <div class="row">
                <span class="col">Kuupaev</span>
                <span class="col">{{formatdate()}}</span>
            </div>
            <div class="row">
                <span class="col">Koht</span>
                <span class="col">{{eventitem.location}}</span>
            </div>
            <div class="row">
                <span class="col">Lisainfo</span>
                <span class="col">{{eventitem.details}}</span>
            </div>
        </div>
        <h2 class="container">Osalejad</h2>
        <div v-bind:key="i.id" v-for="i in participantlist" class="container col-sm-2">
            <div>
                <router-link id="routerbtn" :to="formatroute(i.participantid)" tag="button" class="">{{i.firstname}}</router-link>
                <br />
            </div>
        </div>
        <h2 class="container">Osaleja lisamine</h2>
        <!--Choose either to fill in a form for individuals or for companies-->
        <div class="container col-sm-2 offset-sm-6">
            <div class="row">
                <input class="col" type="radio" name="myradio" value="1" checked="checked" @click="toggleform('personform')" />Eraisik
                <input class="col " type="radio" name="myradio" value="2" @click="toggleform('entityform')" />Ettevote
            </div>
        </div>
        <!--Person form for individuals-->
        <form class="container" id="personform">
            <div class="container col-sm-6 myrow">
                <div class="row">
                    <span for="firstname" class="col">Eesnimi</span>
                    <input id="firstname" type="text" name="firstname" class="col">
                </div>
                <div class="row">
                    <span for="familyname" class="col">Perekonnanimi</span>
                    <input id="familyname" type="text" name="familyname" class="col">
                </div>
                <div class="row">
                    <span for="idcode" class="col">Isikukood</span>
                    <input id="idcode" type="text" name="idcode" class="col">
                </div>
                <div class="row">
                    <span for="personpaymentmethod" class="col">Maksmisviis</span>
                    <select id="personpaymentmethod" class="col" name="personpaymentmethod"><option>Sularaha</option><option>Pangaulekanne</option></select>
                </div>
                <div class="row">
                    <span for="details" class="col">Lisainfo</span>
                    <textarea id="details" type="text" class="col"></textarea>
                </div>
                <div class="row">
                    <router-link id="routerbtn" to="/" tag="button" class="col">Tagasi</router-link>
                    <input type="submit" class="col" @click.stop.prevent="submitperson()">
                </div>
            </div>
        </form>
        <!--Entity form for companies-->
        <form class="container" id="entityform" style="display:none;">
            <div class="container col-sm-6 myrow">
                <div class="row">
                    <span for="name" class="col">Ettevotte nimi</span>
                    <input id="name" type="text" name="firstname" class="col">
                </div>
                <div class="row">
                    <span for="entityidcode" class="col">Registrikood</span>
                    <input id="entityidcode" type="text" name="entityidcode" class="col">
                </div>
                <div class="row">
                    <span for="numparticipants" class="col">Osalejate arv</span>
                    <input id="numparticipants" type="text" name="numparticipants" class="col">
                </div>
                <div class="row">
                    <span for="entitypaymentmethod" class="col">Maksmisviis</span>
                    <select id="entitypaymentmethod" class="col" name="entitypaymentmethod"><option>Sularaha</option><option>Pangaulekanne</option></select>
                </div>
                <div class="row">
                    <span for="entitydetails" class="col">Lisainfo</span>
                    <textarea id="entitydetails" type="text" class="col"></textarea>
                </div>
                <div class="row">
                    <router-link id="routerbtn" to="/" tag="button" class="col">Tagasi</router-link>
                    <input type="submit" class="col" @click.stop.prevent="submitentity()">
                </div>
            </div>
        </form>
    </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Event } from '../models/Event';
import { Participant } from '../models/Participant';
import axios from 'axios';
@Component({})
export default class EditEvent extends Vue {
private eventitem: Event = new Event(0, '', '', '', '');
private participantlist: Participant[] = [];
private async created() {
      //  this.participantlist.push(new Participant(5,'nimi','s','123',3,'card','det','person',3));
        await this.fetchEvent();
        await this.fetchParticipants();
    }
// Data queries
private async fetchEvent() {
    try {
        const eventid = window.location.href.split('/').slice(-1)[0];
        const response = await axios.get<Event>('https://localhost:5001/api/Events/' + eventid);
        const res = response.data;
        this.eventitem = new Event(res.eventid, res.eventName, res.eventDate, res.location, res.details);
    } catch (e) {
        alert("failed fetch")
    }
}
private async fetchParticipants() {
    try {
        const response = await axios.get<Participant[]>('https://localhost:5001/api/Events/' + this.eventitem.eventid + '/Participants');
        const res = response.data;
        const myevent: Event = new Event(this.eventitem.eventid, '', '', '', '');
        for (let i = 0; i < res.length; i++) {
            this.participantlist.push(new Participant(res[i].participantid, res[i].firstname, res[i].familyname,
                res[i].idcode, res[i].numparticipants, res[i].paymentmethod, res[i].details, res[i].participantType, myevent));
        }
    } catch (e) {
        alert("failed fetch")
    }
}
// submit person form
private async submitperson() {
    alert('ha');
    // ignore id when posting a new event, using undefined
    const firstnameinput = (document.getElementById('firstname') as HTMLInputElement).value;
    const familynameinput = (document.getElementById('familyname') as HTMLInputElement).value;
    const idcodeinput = (document.getElementById('idcode') as HTMLInputElement).value;
    const paymentmethodinput = (document.getElementById('entitypaymentmethod') as HTMLSelectElement).value;
    const detailsinput = (document.getElementById('details') as HTMLInputElement).value;
    const myevent: Event = new Event(this.eventitem.eventid, null, null, null, null);
    const inputperson = new Participant(0, firstnameinput, familynameinput, idcodeinput, 0,
        paymentmethodinput, detailsinput, 'person', myevent);
    try {
        const result: any = await axios.post('https://localhost:5001/api/Events/' + this.eventitem.eventid + '/Participants', inputperson);
    } catch (e) {
        alert('error posting data');
    }
    this.$router.push('/');
}
// submit entity (business, corporation ...) form
private async submitentity() {
    alert('ha');
    // ignore id when posting a new event, using undefined
    const nameinput = (document.getElementById('name') as HTMLInputElement).value;
    const numparticipants = parseInt((document.getElementById('numparticipants') as HTMLInputElement).value, 10);
    const idcodeinput = (document.getElementById('entityidcode') as HTMLInputElement).value;
    const paymentmethodinput = (document.getElementById('personpaymentmethod') as HTMLSelectElement).value;
    const detailsinput = (document.getElementById('entitydetails') as HTMLInputElement).value;
    const myevent: Event = new Event(this.eventitem.eventid, '', '', '', '');
    const inputcompany = new Participant(0, nameinput, '', idcodeinput, numparticipants,
        paymentmethodinput, detailsinput, 'company', myevent);
    try {
        const result: any = axios.post('https://localhost:5001/api/Events/' + this.eventitem.eventid + '/Participants', inputcompany);
    }
    catch (e) {
        alert('error posting data');
    }
    this.$router.push('/');
}
// format date to human readable form
private formatdate(): string {
    const today = new Date(this.eventitem.eventDate || '');
    let dd: string = today.getDate().toString();
    let mm: string = (today.getMonth() + 1).toString();
    const yyyy = today.getFullYear();
    if (parseInt(dd, 10) < 10)
    {
        dd = '0' + dd;
    }
    if (parseInt(mm, 10) < 10)
    {
        mm = '0' + mm;
    }
    return mm + '-' + dd + '-' + yyyy;
}
// format route path for participant details
private formatroute(participant: number): string {
    return '/uritus/' + this.eventitem.eventid.toString() + '/osaleja/' + participant;
}
// toggle between the two forms using vanilla js
private toggleform(form: string): void {
    (document.getElementById('personform') as HTMLFormElement).style.display = 'none';
    (document.getElementById('entityform') as HTMLFormElement).style.display = 'none';
    (document.getElementById(form) as HTMLFormElement).style.display = 'block';
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