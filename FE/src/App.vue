<template>
  <v-app>
    <left-sidebar/>
    <header-menu/>
    <v-content>
      <v-container fluid grid-list-md>
        <v-layout row wrap>
          <v-flex xs12>
            <router-view></router-view>
          </v-flex>
        </v-layout>
      </v-container>
      <notifications ref="notifications" />
    </v-content>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue'
import HeaderMenu from './components/Layouts/HeaderMenu.vue'
import LeftSidebar from './components/Layouts/LeftSidebar.vue'
import Notifications from './components/Commons/Notifications.vue'
export default Vue.extend({
  name: 'App',
  components: {
    HeaderMenu,
    LeftSidebar,
    Notifications
  },
  data () {
    return {
      auth: this.$store.state.auth,
      user: this.$store.state.user,
      dialog: false,
      dtMin: false,
      height: 300,
      fav: true,
      menu: true,
      message: false,
      hints: true
    }
  },
  computed: {
    showLeftSideBar (): boolean {
      return this.$store.state.app.drawer
    }
  },
  beforeCreate () {
    Vue.prototype.$notify = (params: any) => {
      if (typeof params === 'string') {
        params = { title: '', text: params }
      }
      if (typeof params === 'object') {
        (this.$refs.notifications as any).show(params)
      }
    }
  }
})
</script>
