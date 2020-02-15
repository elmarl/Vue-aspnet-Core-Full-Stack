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
                    <span for="paymentmethod" class="col">Maksmisviis</span>
                    <select id="paymentmethod" class="col" name="paymentmethod"><option>Sularaha</option><option>Pangaulekanne</option></select>
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
                    <span for="idcode" class="col">Registrikood</span>
                    <input id="idcode" type="text" name="familyname" class="col">
                </div>
                <div class="row">
                    <span for="numparticipants" class="col">Osalejate arv</span>
                    <input id="numparticipants" type="text" name="idcode" class="col">
                </div>
                <div class="row">
                    <span for="paymentmethod" class="col">Maksmisviis</span>
                    <select id="paymentmethod" class="col" name="paymentmethod"><option>Sularaha</option><option>Pangaulekanne</option></select>
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
import axios from 'axios';
@Component({})
export default class Home extends Vue {
private eventitem!: Event;
private async created() {
        await this.fetchEvent();
  }
private async fetchEvent() {
    const dt = new Date();
    this.eventitem = new Event(0, 'test', dt.toISOString(), 'tll', 'det');
    try {
        const response = await axios.get<Event[]>('api/Events/1');
        const res = response.data;
           
        for (let i = 0; i < res.length; i++) {
            this.eventitem = new Event(res[i].id, res[i].eventName, res[i].eventDate, res[i].location, res[i].details);
        }
    } catch (e) {
    }
}
private formatdate(): string {
    const today = new Date(this.eventitem.eventDate);
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
// submit person form
private submitperson(): void {
    alert('ha');
}
// submit entity (business, corporation ...) form
private submitentity(): void {
    alert('ha');
}
// toggle between the two forms using vanilla js
private toggleform(form: string): void {
    (<HTMLFormElement>document.getElementById('personform')).style.display = "none"; 
    (<HTMLFormElement>document.getElementById('entityform')).style.display = "none"; 
    (<HTMLFormElement>document.getElementById(form)).style.display = "block"; 
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