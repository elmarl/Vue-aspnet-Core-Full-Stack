<template>
  <div>
      <v-row class="" no-gutters>
      <v-col>
          <span class="" tile>
              {{index}}: {{eventitem["eventName"]}}
          </span>
          </v-col>
          <v-col>
              <span tile>
                  {{formatdate}}
              </span>
          </v-col>
          <v-col>
              <v-btn :to="routemsg" depressed small>Osavõtjad</v-btn>
              <!--<router-link :to="routemsg" tag="button">Osavõtjad</router-link>-->
          </v-col>
          <v-col v-show="!ispastevent">
            <v-btn id="deletebtn" @click="$emit('del_item', eventitem['eventid'])" depressed small>Eemalda</v-btn>
          </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: 'Event_component',
  props: {
      eventitem: Object,
      ispastevent: Boolean,
      index: Number,
  },
  computed: {
    routemsg: function() {
        return '/uritus/'.concat(this.eventitem.eventid.toString());
     },
      formatdate: function() {
            const today = new Date(this.eventitem.eventDate);
            let dd = today.getDate();
            let mm = today.getMonth() + 1;
            const yyyy = today.getFullYear();
            if (dd < 10) 
            {dd = '0' + dd; }
            if (mm < 10) 
            {mm = '0' + mm; }
            return mm + '-' + dd + '-' + yyyy;
      },
   },
};
</script>

<style scoped>
    div {
    margin:0px;
    }
</style>
