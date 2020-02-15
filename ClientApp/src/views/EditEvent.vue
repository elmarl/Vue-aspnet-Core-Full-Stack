<template>
    <v-container fluid fill-height class="container" style="">
        <h2 class="container">Yritus</h2>
        <div class="container col-sm-4">
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
        <div>
            <div class="row">
                <span class="col">Urituse nimi</span>
                <input type="text" class="col">
            </div>
            <div class="row">
                <span class="col">Kuupaev</span>
                <input type="text" class="col">
            </div>
            <div class="row">
                <span class="col">Koht</span>
                <input type="text" class="col">
            </div>
            <div class="row">
                <span class="col">Lisainfo</span>
                <input type="text" class="col">
            </div>
        </div>
    </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Event } from '../models/Event';
import axios from 'axios';
@Component({})
export default class Home extends Vue {
private eventitem: Event;
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
    let dd = today.getDate();
    let mm = today.getMonth() + 1;
    const yyyy = today.getFullYear();
    if (dd < 10) 
    {dd = '0' + dd; }
    if (mm < 10) 
    {mm = '0' + mm; }
    return mm + '-' + dd + '-' + yyyy;
    }
}
</script>

<style scoped>

</style>