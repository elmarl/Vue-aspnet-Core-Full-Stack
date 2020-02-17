import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import EditEvent from './views/EditEvent.vue';
import AddEvent from './views/AddEvent.vue';
import EditParticipant from './views/EditParticipant.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
        path: '/',
        name: 'home',
        component: Home,
    },
    {
        path: '/uritus/:id',
        name: 'edit_event',
        component: EditEvent,
    },
    {
        path: '/uritus/:id/osaleja/:oid',
        name: 'edit_participant',
        component: EditParticipant,
    },
    {
        path: '/lisauritus',
        name: 'add_event',
        component: AddEvent,
    },
  ],
});
