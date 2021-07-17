<template>
        <v-toolbar app  color="primary" v-if='isAuthenticated'>
            <v-toolbar-side-icon dark @click.stop="toogleLeftSidebar"></v-toolbar-side-icon>
            <v-spacer></v-spacer>
            <v-menu offset-y>
                <v-btn icon large slot="activator" dark>
                    <v-avatar size="42px">
                        <!-- <img :src="user.photo + '?' + new Date().getTime()" :alt="user.fullName" /> -->
                        <img src="https://community.smartsheet.com/sites/default/files/default_user.jpg" :alt="''" />
                    </v-avatar>
                </v-btn>
                <v-list>
                    <v-list-tile>
                        <v-list-tile-title>
                            {{ this.user.Username }}
                        </v-list-tile-title>
                    </v-list-tile>

                    <v-divider></v-divider>
                    <v-list-tile to="/change-password">
                        <v-list-tile-title>
                            Đổi mật khẩu
                        </v-list-tile-title>
                    </v-list-tile>
                    <v-list-tile @click="logout">
                        <v-list-tile-title>
                            Đăng xuất
                        </v-list-tile-title>
                    </v-list-tile>
                </v-list>
            </v-menu>
        </v-toolbar>
</template>

<script lang="ts">
import Vue from 'vue'
import Auth from '../../auth'
import store from '../../store/store'
export default Vue.extend({
  name: 'HeaderMenu',
  data () {
    return {
      auth: this.$store.state.auth,
      user: this.$store.state.user.Profile,
      drawer: null
    }
  },
  computed: {
    isAuthenticated () {
      return store.state.user.AccessToken.IsAuthenticated
    },
    title (): string {
      return this.$store.state.app.title
    }
  },
  watch: {
    '$store.state.user': function () {
      this.user = this.$store.state.user.Profile
    }
  },
  methods: {
    logout (): void {
      Auth.logout()
    },
    toogleLeftSidebar () {
      store.commit('UPDATE_LEFSIDEBAR')
    },
    ChangePassword (): void {
      this.$router.push({
        name: 'change-password-login'
      })
    }
  }
})
</script>
