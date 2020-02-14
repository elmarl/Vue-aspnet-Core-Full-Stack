<template>
    <div>
        Form
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Events } from '../models/Events';
import axios from 'axios';

@Component({})
export default class FetchDataView extends Vue {
    private loading: boolean = true;
    private showError: boolean = false;
    private errorMessage: string = 'Error while loading weather forecast.';
    private event_details: Events;

   
    //private async created() {
    //  await this.fetchWeatherForecasts();
    //}

    //private async fetchWeatherForecasts() {
    //    try {
    //        const response = await axios.get<Forecast[]>('api/WeatherForecast');
    //        this.forecasts = response.data;
    //        alert(this.forecasts.toString());
    //    } catch (e) {
    //        this.showError = true;
    //        this.errorMessage = `Error while loading weather forecast: ${e.message}.`;
    //    }
    //    this.loading = false;
    //}
}
</script>

<style scoped>

</style>
