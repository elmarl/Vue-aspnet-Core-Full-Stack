<template>
  <div>
      <v-row class="" no-gutters>
      <v-col>
        <span class="" tile>
            {{eventitem["eventName"]}}
        </span>
      </v-col>
          <v-col>
              <span class="" tile>
                  {{formatdate}}
              </span>
      </v-col>
          <v-col>
              <router-link :to="routemsg" tag="button">Osav6tjad</router-link>
        <!--<v-btn @click="$emit('edit_item', eventitem['id'])" to=eventitem["id"] depressed small>Osav6tjad</v-btn>-->
      </v-col>
          <v-col>
        <v-btn :required="ispastevent ? false : true" id="deletebtn" @click="$emit('del_item', eventitem['eventid'])" depressed small>delete</v-btn>
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
  },
  computed: {
    routemsg: function() {
        return '/uritus/' + this.eventitem.eventid.toString();
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
