<template>
  <!-- main sidebar -->
    <v-navigation-drawer  fixed  :clipped="$vuetify.breakpoint.lgAndUp" app :value="showLeftSideBar" @input="toggleSidebar" width="250">
      <v-list>
          <v-list-group
            v-for="item in menus"
            v-model="item.active"
            :key="item.title"
            :prepend-icon="item.action"
            v-if="item.show"
            no-action
          >
            <v-list-tile slot="activator">
              <v-list-tile-content>
                <v-list-tile-title>{{ item.title }}</v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile v-for="subItem in item.items" :key="subItem.title" :to="'/'+subItem.link" v-if="subItem.show">
              <v-list-tile-content>
                <v-list-tile-title>{{ subItem.title }}</v-list-tile-title>
              </v-list-tile-content>
              <v-list-tile-action>
                <v-icon>{{ subItem.action }}</v-icon>
              </v-list-tile-action>
            </v-list-tile>
          </v-list-group>
        </v-list>
    </v-navigation-drawer>
  <!-- main sidebar end -->
</template>
<script>
  import * as MUTATION_TYPES from '../../store/MUTATION_TYPES.js'
  import store from '../../store'
  export default {
    name: 'LeftSidebar',
    data() {
      return {
        isVisible: true,
        drawer: true,
        user: this.$store.state.user
      }
    },
    computed: {
      showLeftSideBar () {
        return this.$store.state.app.showLeftSideBar
      },
      menus() {
        return [
          {
            action: 'list_alt',
            title: 'Công việc',
            show: this.user,
            items: [
              { title: 'Quản lý công việc', link: 'ql-cong-viec', show: this.user && this.user.User.UserId == 1 },
              { title: 'Danh sách công việc', link: 'ds-cong-viec', show: this.user && this.user.User.UserId == 2 },
              { title: 'Công việc của tôi', link: 'cong-viec-cua-toi', show: this.user && this.user.User.UserId == 2 }
            ]
          },
          {
            action: 'settings',
            title: 'Thông tin chung',
            show: this.user && this.user.User.UserId == 1,
            items: [
              { title: 'Danh mục loại công việc', link: 'loai-cong-viec', show: this.user && this.user.User.UserId == 1 },
            ]
          }
        ]
      }
    },
    methods: {
      toggleSidebar(val) {
        if (val !== this.$store.state.app.showLeftSideBar) {
          store.commit(MUTATION_TYPES.TOOGLE_LEFT_SIDE_BAR)
        }
      },
      goTo(link) {
        if (link !== undefined && link !== '') {
          this.$router.push(link)
        }
      }
    }
  }
</script>