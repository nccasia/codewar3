<template>
  <!-- main header color="blue darken-2"-->
      <v-toolbar style="background: #cd5c5c;"  dark app :clipped-left="$vuetify.breakpoint.lgAndUp" fixed >
      <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
        <v-toolbar-side-icon @click.stop="toogleLeftSidebar"></v-toolbar-side-icon>
        <a @click="goToMVCNet()" class="px-2">
          <v-avatar v-if="user.photo !== ''"> 
            <img style="min-width:62px;max-width:62px" src="/static/images/logo_1.png"/>
          </v-avatar>
        </a>
        <span class="hidden-sm-and-down">Mai An Vat</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
 
      <v-menu offset-y>
      <v-btn icon large slot="activator" dark>
        <v-avatar size="42px" v-if="user.photo !== ''"> 
          <img v-if="(user.User.AnhDaiDien !== null)" :src="APIS.BASE_API + 'fileupload/download?key=' + user.User.AnhDaiDien" :alt="user.User.AnhDaiDien" />
          <img v-else src="static/images/user-photo.png">
        </v-avatar>
        <v-icon x-large v-else>account_circle</v-icon>
      </v-btn>
        <v-list>
          <v-list-tile @click="logout">
            <a href="/login">
              <v-list-tile-title>
                Đăng xuất
              </v-list-tile-title>
            </a>
          </v-list-tile>
        </v-list>
      </v-menu>
    </v-toolbar>
  <!-- main header end -->
</template>
<script>
  import Auth from '../../auth'
  import APIS from '../../apis-const'
  import * as MUTATION_TYPES from '../../store/MUTATION_TYPES.js'
  import store from '../../store'
  export default {
    name: 'HeaderMenu',
    data() {
      return {
        auth: this.$store.state.auth,
        user: this.$store.state.user,
        drawer: null,
        token: 'http://localhost:8089/Auth/CheckIsLogin?Token=' + this.$store.state.auth.accessToken,
        APIS: APIS
      }
    },
    methods: {
      logout() {
        Auth.logout()
      },
      toogleLeftSidebar() {
        store.commit(MUTATION_TYPES.TOOGLE_LEFT_SIDE_BAR)
      },
      toogleRightSidebar() {
        store.commit(MUTATION_TYPES.TOOGLE_RIGHT_SIDE_BAR)
      },
      ChangePassword() {
        this.$router.push({
          name: 'change-password-login'
        })
      },
      goToMVCNet () {
        window.location.href = this.token
      }
    }
  }
</script>