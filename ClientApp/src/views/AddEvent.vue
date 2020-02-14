<template>
    <v-container fluid fill-height class="container">
        <v-container class="container" style="text-align:center" outlined>
            <form class="form-horizontal" action="/action_page.php">
                <div class="form-group row">
                    <label class="control-label col-sm-2" for="name">Nimi</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="name" name="name" >
                    </div>
                </div>
                <div class="form-group row">
                    <label class="control-label col-sm-2" for="date">Kuupaev:</label>
                    <div class="col-sm-10">
                        <input type="datetime-local" class="form-control" v-bind:min="getTime()" id="date">
                    </div>
                </div>
                <div class="form-group row">
                    <label class="control-label col-sm-2" for="location">Koht</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="location" name="location">
                    </div>
                </div>
                <div class="form-group row">
                    <label class="control-label col-sm-2" for="details">Lisainfo</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="details" name="details">
                    </div>
                </div>
                <div class="form-group row">
                    <router-link to="/" tag="button" class="col-sm-2">Tagasi</router-link>
                    <div class="col-sm-10">
                        <input type="submit" class="form-control" @click.stop.prevent="submit()">
                    </div>
                </div>
            </form>
            
        </v-container>
    </v-container>
</template>

<!--<div class="container">
    <h2>Horizontal form</h2>
    <form class="form-horizontal" action="/action_page.php">
        <div class="form-group">
            <label class="control-label col-sm-2" for="email">Email:</label>
            <div class="col-sm-10">
                <input type="email" class="form-control" id="email" placeholder="Enter email" name="email">
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="pwd">Password:</label>
            <div class="col-sm-10">
                <input type="password" class="form-control" id="pwd" placeholder="Enter password" name="pwd">
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <div class="checkbox">
                    <label><input type="checkbox" name="remember"> Remember me</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <button type="submit" class="btn btn-default">Submit</button>
            </div>
        </div>
    </form>
</div>-->


<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Events } from '../models/Events';
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
}
</script>

<style scoped>
   
form {
    display: inline-block;
}
input {
    border:thin;
}
</style>
