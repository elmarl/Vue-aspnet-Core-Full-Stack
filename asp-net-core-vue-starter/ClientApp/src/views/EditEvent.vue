<!--
    companyform - juriidilise isiku vorm
    personform - fuusilise isiku vorm

                                                // vormi sisestamine toimub 'axios'-t kasutades, teeb GET, POST, PUT ja DELETE http requeste
    v-on:keydown.enter.prevent="submitperson()" // vormi sisestamisel enter klahviga vaikimisi actioni (post ja refresh) asemel suunab funktsioonile
    @click.stop.prevent="submitperson()"        // sama mis eelmine, ainult submit nupu hiirega klikkimise jaoks
-->
<template>
    <v-container fluid fill-height class="container sheet">
        <v-row no-gutters class="subheader">
            <v-col cols="4">
                <div class="pa-2">
                    <h2 class="notbold">Ürituse info</h2>
                </div>
            </v-col>
            <v-col cols="8">
                <div class="pa-0">
                    <v-img class="subheaderimg" src="../assets/libled.jpg"></v-img>
                </div>
            </v-col>
        </v-row>
        <h2 class="container blue-text offset-sm-2">Üritus</h2>
        <div class="container col-sm-6 myrow">
            <div class="row">
                <span class="col">Ürituse nimi</span>
                <span class="col">{{eventitem.eventName}}</span>
            </div>
            <div class="row">
                <span class="col">Kuupäev</span>
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
        <h2 class="container blue-text offset-sm-2">Osalejad</h2>
        <table class="container col-sm-6 offset-sm-3">
            <tr v-bind:key="i.id" v-for="(i, index) in participantlist">
                <td class="col pa-1">
                    <span>{{index + 1}}: {{formatname(i)}}</span>
                </td>
                <td class="col pa-1">
                    <span>{{i.idcode}}</span>
                <td class="col pa-1">
                    <v-btn :to="formatroute(i.participantid)" depressed small>Vaata</v-btn>
                </td>
                <td class="col pa-1">
                    <span>
                        <v-btn @click="deleteparticipant(i)" depressed small>Eemalda</v-btn>
                    </span>
                </td>
            </tr>
        </table>
        <h2 class="container blue-text offset-sm-2">Osaleja lisamine</h2>
        <!--Choose either to fill in a form for individuals or for companies-->
        <div class="container col-sm-2 offset-sm-6">
            <div class="row">
                <input class="col" type="radio" name="myradio" value="1" checked="checked" @click="toggleform('personform')" />Eraisik
                <input class="col " type="radio" name="myradio" value="2" @click="toggleform('companyform')" />Ettevõte
            </div>
        </div>
        <!--Person form for individuals-->
        <form class="container" id="personform">
            <div class="container col-sm-6 myrow">
                <div class="row pa-1">
                    <label for="firstname" class="col pa-1">Eesnimi</label>
                    <input id="firstname" type="text" name="firstname" class="col pa-1" v-on:keydown.enter.prevent="submitperson()">
                </div>
                <div class="row pa-1">
                    <label for="familyname" class="col pa-1">Perekonnanimi</label>
                    <input id="familyname" type="text" name="familyname" class="col pa-1" v-on:keydown.enter.prevent="submitperson()">
                </div>
                <div class="row pa-1">
                    <label for="idcode" class="col pa-1">Isikukood</label>
                    <input id="idcode" type="text" name="idcode" class="col pa-1" v-on:keydown.enter.prevent="submitperson()">
                </div>
                <div class="row pa-1">
                    <label for="personpaymentmethod" class="col pa-1">Maksmisviis</label>
                    <select id="personpaymentmethod" class="col pa-1" name="personpaymentmethod" v-on:keydown.enter.prevent="submitperson()"><option>Sularaha</option><option>Pangaülekanne</option></select>
                </div>
                <div class="row pa-1">
                    <label for="persondetails" class="col pa-1">Lisainfo</label>
                    <textarea id="persondetails" type="text" class="col pa-1" v-on:keydown.enter.prevent="submitperson()"></textarea>
                </div>
                <div class="row pa-1">
                    <router-link to="/" tag="button" class="col pa-1">Tagasi</router-link>
                    <input type="submit" class="col blue-text pa-1 routerbtn" @click.stop.prevent="submitperson()" value="Lisa">
                </div>
            </div>
        </form>
        <!--Entity form for companies-->
        <form class="container" id="companyform" style="display:none;">
            <div class="container col-sm-6 myrow">
                <div class="row pa-1">
                    <label for="name" class="col pa-1">Ettevõtte nimi</label>
                    <input id="name" type="text" name="firstname" class="col pa-1" v-on:keydown.enter.prevent="submitcompany()">
                </div>
                <div class="row pa-1">
                    <label for="companyidcode" class="col pa-1">Registrikood</label>
                    <input id="companyidcode" type="text" name="companyidcode" class="col pa-1" v-on:keydown.enter.prevent="submitcompany()">
                </div>
                <div class="row pa-1">
                    <label for="numparticipants" class="col pa-1">Osalejate arv</label>
                    <input id="numparticipants" type="text" name="numparticipants" class="col pa-1" v-on:keydown.enter.prevent="submitcompany()">
                </div>
                <div class="row pa-1">
                    <label for="companypaymentmethod" class="col pa-1">Maksmisviis</label>
                    <select id="companypaymentmethod" class="col pa-1" name="companypaymentmethod" v-on:keydown.enter.prevent="submitcompany()"><option>Sularaha</option><option>Pangaülekanne</option></select>
                </div>
                <div class="row pa-1">
                    <label for="companydetails" class="col pa-1">Lisainfo</label>
                    <textarea id="companydetails" type="text" class="col pa-1" v-on:keydown.enter.prevent="submitcompany()"></textarea>
                </div>
                <div class="row pa-1">
                    <router-link to="/" tag="button" class="col pa-1">Tagasi</router-link>
                    <input type="submit" class="col blue-text pa-1 routerbtn" @click.stop.prevent="submitcompany()" value="Lisa">
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
    import BaseUrl from '../BaseUrl/BaseUrl';
    @Component({})
    export default class EditEvent extends Vue {
        private eventitem: Event = new Event(0, '', '', '', '');
        private participantlist: Participant[] = [];
        private async created() {
            await this.fetchEvent();
            await this.fetchParticipants();
        }
        // Data queries
        private async fetchEvent() {
            try {
                const eventid = window.location.href.split('/').slice(-1)[0];
                const response = await axios.get<Event>(BaseUrl + eventid);
                const res = response.data;
                this.eventitem = new Event(res.eventid, res.eventName, res.eventDate, res.location, res.details);
            } catch (e) {
                alert('failed fetch');
            }
        }
        private async fetchParticipants() {
            try {
                const response = await axios.get<Participant[]>(BaseUrl.concat(this.eventitem.eventid.toString()).concat('/participants'));
                const res = response.data;
                const myevent: Event = new Event(this.eventitem.eventid, '', '', '', '');
                this.participantlist = [];
                for (let i = 0; i < res.length; i++) {
                    this.participantlist.push(new Participant(res[i].participantid, res[i].firstname, res[i].familyname,
                        res[i].idcode, res[i].numParticipants, res[i].paymentmethod, res[i].detailsPerson,  res[i].detailsCompany, res[i].participantType, myevent));
                }
            } catch (e) {
                alert(e);
            }
        }
        // submit person form
        private async submitperson() {
            // ignore id when posting a new event, using undefined
            const firstnameinput = (document.getElementById('firstname') as HTMLInputElement).value;
            const familynameinput = (document.getElementById('familyname') as HTMLInputElement).value;
            const idcodeinput = (document.getElementById('idcode') as HTMLInputElement).value;
            const paymentmethodinput = (document.getElementById('personpaymentmethod') as HTMLSelectElement).value;
            const detailsinput = (document.getElementById('persondetails') as HTMLInputElement).value;
            const myevent: Event = new Event(this.eventitem.eventid, null, null, null, null);
            const inputperson = new Participant(0, firstnameinput, familynameinput, idcodeinput, 0,
                paymentmethodinput, detailsinput, '', 'person', myevent);
            try {
                const result: any = await axios.post(BaseUrl.concat(this.eventitem.eventid.toString()).concat('/participants'), inputperson);
            } catch (e) {
                alert(e);
            }
            this.fetchParticipants();
        }
        // submit entity (business, corporation ...) form
        private async submitcompany() {
            // ignore id when posting a new event, using undefined
            const nameinput = (document.getElementById('name') as HTMLInputElement).value;
            const numparticipants = parseInt((document.getElementById('numparticipants') as HTMLInputElement).value, 10);
            const idcodeinput = (document.getElementById('companyidcode') as HTMLInputElement).value;
            const paymentmethodinput = (document.getElementById('personpaymentmethod') as HTMLSelectElement).value;
            const detailsinput = (document.getElementById('companydetails') as HTMLInputElement).value;
            const myevent: Event = new Event(this.eventitem.eventid, '', '', '', '');
            const inputcompany = new Participant(0, nameinput, '', idcodeinput, numparticipants,
                paymentmethodinput, '', detailsinput, 'company', myevent);
            try {
                const result: any = await axios.post(BaseUrl.concat(
                    this.eventitem.eventid.toString()).concat('/participants'), inputcompany);
            }
            catch (e) {
                alert(e);
            }
            this.fetchParticipants();
        }
        private async deleteparticipant(p: Participant) {
            try {
                const result: any = await axios.delete(BaseUrl.concat(
                    this.eventitem.eventid.toString()).concat('/participants/').concat(p.participantid.toString()));
            }
            catch (e) {
                alert('error deleting participant');
            }
            const index: number = this.participantlist.findIndex((x) => x.participantid === p.participantid);
            this.participantlist.splice(index, 1);
        }
        // format date to human readable form
        private formatdate(): string {
            if (this.eventitem.eventDate === '') {
                return '';
            }
            const eventdate = new Date(this.eventitem.eventDate || '');
            let dd: string = eventdate.getDate().toString();
            let mm: string = (eventdate.getMonth() + 1).toString();
            const hours: string = eventdate.getHours().toString();
            const minutes: string = eventdate.getMinutes().toString();
            const yyyy = eventdate.getFullYear();
            if (parseInt(dd, 10) < 10) {
                dd = '0' + dd;
            }
            if (parseInt(mm, 10) < 10) {
                mm = '0' + mm;
            }
            return mm + '-' + dd + '-' + yyyy + ' ' + hours + ':' + minutes;
        }
        private formatname(p: Participant) {
            if (p.participantType === 'person') {
                return p.firstname.concat(' ').concat(p.familyname);
            } else {
                // otherwise its 'company'
                return p.firstname;
            }
        }
        // format route path for participant details
        private formatroute(participant: number): string {
            return '/uritus/'.concat(this.eventitem.eventid.toString()).concat('/osaleja/').concat(participant.toString());
        }
        // toggle between the two forms using vanilla js
        private toggleform(form: string): void {
            (document.getElementById('personform') as HTMLFormElement).style.display = 'none';
            (document.getElementById('companyform') as HTMLFormElement).style.display = 'none';
            (document.getElementById(form) as HTMLFormElement).style.display = 'block';
        }
    }
</script>

<style scoped>
    .myrow > div > span, .myrow > div > input,
    .myrow > div > select,
    .myrow > div > .routerbtn {
        padding-bottom: 0px;
        padding-top: 0px;
    }

    input, textarea, select {
        border-style: ridge;
    }

    table {
        padding: 0px;
    }

    .sheet {
        background-color: white;
        margin-top: 10px;
    }

    .blue-text {
        color: royalblue;
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