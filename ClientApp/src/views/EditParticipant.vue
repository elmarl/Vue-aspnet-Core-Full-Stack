<template>
    <v-container fluid fill-height class="container" style="">
        <h2 class="container">Osaleja</h2>
        <div id="persondata" style="visibility:hidden" class="container col-sm-6 myrow">
            <div class="row">
                <span class="col">Urituse nimi</span>
                <span class="col">{{participant.firstname}}</span>
            </div>
            <div class="row">
                <span class="col">Kuupaev</span>
                <span class="col">{{participant.familyname}}</span>
            </div>
            <div class="row">
                <span class="col">Koht</span>
                <span class="col">{{participant.paymentmethod}}</span>
            </div>
            <div class="row">
                <span class="col">Lisainfo</span>
                <span class="col">{{participant.details}}</span>
            </div>
        </div>
     
    </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';
import { Participant } from '../models/Participant';
@Component
export default class Counter extends Vue {
    private participant: Participant = new Participant(0,'','','',0,'','','',0);
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
}
</script>

<style scoped>

</style>