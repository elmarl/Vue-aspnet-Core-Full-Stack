<template>
  <div>
      <v-row class="" no-gutters>
      <v-col>
        <span class="" tile>
            {{eventitem["name"]}}
        </span>
      </v-col>
          <v-col>
        <span class="" tile>
          {{eventitem["date"]}}
        </span>
      </v-col>
          <v-col>
        <v-btn @click="$emit('edit_item', eventitem['id'])" depressed small>Osav6tjad</v-btn>
      </v-col>
          <v-col>
        <v-btn @click="$emit('del_item', eventitem['id'])" depressed small>delete</v-btn>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: 'Event_component',
  props: {
    eventitem: Object,
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    div {
    margin:0px;
    }
</style>
