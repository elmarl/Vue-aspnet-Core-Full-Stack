<template>
  <v-app>
      <v-app-bar class="container table" :clipped-left="clipped" color="info" dark>
          <img src="../public/img/icons/logo.svg" class="column"/>
          <v-spacer></v-spacer>
              <v-btn class="column" value="true" v-for="(item, i) in items" :key="i" :to="item.link">
                  <v-list-item-content>
                      <v-list-item-title v-text="item.title"></v-list-item-title>
                  </v-list-item-content>
              </v-btn>
          <v-spacer></v-spacer>
          <img src="../public/img/icons/symbol.svg" class="column" />
      </v-app-bar>

    <v-content >
      <router-view/>
    </v-content>

    <v-footer fixed class="vue-footer">
        <div class="container dark">
            <table class="footer-list-container">
                <tr>
                    <td>
                        <ul>
                            <li><h2>Pealkiri</h2></li>
                            <li>tekst1</li>
                            <li>tekst2</li>
                            <li>tekst3</li>
                            <li>tekst4</li>
                        </ul>
                    </td>
                    <td>
                        <ul>
                            <li><h2>Pealkiri</h2></li>
                            <li>tekst1</li>
                            <li>tekst2</li>
                            <li>tekst3</li>
                            <li>tekst4</li>
                        </ul>
                    </td>
                    <td>
                        <ul>
                            <li><h2>Pealkiri</h2></li>
                            <li>Peakontor: Tallinnas</li>
                            <li>Väike-Ameerika 1, 11415 Tallinn</li>
                            <li>Telefon: 605 4450</li>
                            <li>Faks 605 4450</li>
                        </ul>
                    </td>
                    <td>
                        <ul>
                            <li><h2 style="visibility:hidden">hidden</h2></li>
                            <li>Harukontor: Võrus</li>
                            <li>Oja tn 7 (külastusaadress)</li>
                            <li>Telefon: 605 3330</li>
                            <li>Faks: 605 3155</li>
                        </ul>
                    </td>
                </tr>
            </table>
        </div>
    </v-footer>
  </v-app>
</template>

<script lang="ts">
// import HelloWorld from '@/components/HelloWorld.vue';
import { Component, Vue } from 'vue-property-decorator';

@Component({
  // components: { HelloWorld },
})
export default class App extends Vue {
  private clipped: boolean = true;
  private drawer: boolean = true;
  private miniVariant: boolean = false;
  private right: boolean = true;
  private title: string = 'ASP.NET Core Vue Starter';
  private items = [
    { title: 'Home', icon: 'home', link: '/' },
    { title: 'Urituse lisamine', icon: 'touch_app', link: '/lisauritus' },
    { title: 'Fetch data', icon: 'get_app', link: '/fetch-data' },
  ];
}
</script>

<style scoped>
 
.vue-footer { 
     position: relative;
     bottom: 0;
     background-color:white;
}
li {
    list-style: none;
    text-align: left;
}
.dark {
    background-color: rgb(30, 30, 30);
    color:white;
}
.table {
    display: table;
    border-collapse: collapse;
    table-layout: fixed;
    width: 100%;
}
.table > div {
    display: table-cell;
    border: 1px solid red;
}

</style>