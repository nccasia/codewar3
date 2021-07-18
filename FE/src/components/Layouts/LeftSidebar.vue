<template>
  <v-navigation-drawer persistent
                      v-if="isAuthenticated"
                       v-model="drawer"
                       enable-resize-watcher
                       fixed
                       width="250"
                       app>
    <v-toolbar flat color="primary" dark>
      <v-list>
        <v-list-tile>
          <v-list-tile-title class="title text-xs-center">
            Quản lý bán hàng
          </v-list-tile-title>
        </v-list-tile>
      </v-list>
    </v-toolbar>
    <v-divider></v-divider>
    <v-list>
      <div v-for="(menu, i) in menus" :key="i">
        <v-list-group
          v-if="menu.show"
          :prepend-icon="menu.icon"
        >
          <v-list-tile slot="activator">
            <v-list-tile-title>{{menu.text}}</v-list-tile-title>
          </v-list-tile>
          <div v-for="(item, j) in menu.children" :key="j">
            <v-list-tile
              v-if="item.show"
                :to="item.link"
            >
              <v-list-tile-action>
                <v-icon v-text="item.icon"></v-icon>
              </v-list-tile-action>
              <v-list-tile-title v-text="item.text"></v-list-tile-title>
            </v-list-tile>
          </div>
        </v-list-group>
      </div>
    </v-list>
  </v-navigation-drawer>
</template>

<style>
   .icon-sidebar{
     min-width: 40px !important;
   }
   .icon-sidebar .icon{
     font-size: 20px;
   }
</style>

<script lang="ts">
import Vue from 'vue'
import * as MUTATION_TYPES from '../../store/mutations'
import store from '../../store/store'
export default Vue.extend({
  name: 'LeftSidebar',
  data () {
    return {
      drawer: this.$store.state.app.drawer,
      user: this.$store.state.user.Profile
    }
  },
  watch: {
    '$store.state.app.drawer': function () {
      this.drawer = this.$store.state.app.drawer
    },
    '$store.state.user.Profile': function () {
      this.user = this.$store.state.user.Profile
    }
  },
  computed: {
    isAuthenticated () {
      return store.state.user.AccessToken.IsAuthenticated
    },
    menus (): any {
      return [
        {
          icon: 'task',
          text: 'Công việc',
          model: true,
          show: true,
          children: [
            {
              icon: 'list',
              show: true,
              text: 'Danh sách Công việc',
              link: '/danh-sach-cong-viec'
            },
            {
              icon: 'list',
              show: true,
              text: 'Đơn hàng',
              link: '/danh-sach-don-hang'
            },
            {
              icon: 'list',
              show: true,
              text: 'Loại công việc',
              link: '/danh-sach-loai-cong-viec'
            }
          ]
        },
        {
          icon: 'account_circle',
          text: 'Nhân viên',
          model: true,
          show: true,
          children: [
            {
              icon: 'list',
              show: true,
              text: 'Danh sách nhân viên',
              link: '/danh-sach-nhan-vien'
            }
          ]
        },
        {
          icon: 'account_circle',
          text: 'Khách hàng',
          model: true,
          show: true,
          children: [
            {
              icon: 'list',
              show: true,
              text: 'Danh sách khách hàng',
              link: '/danh-sach-khach-hang'
            }
          ]
        },
        {
          icon: 'settings',
          text: 'Danh mục',
          model: true,
          show: true,
          children: [
            {
              icon: 'list',
              show: true,
              text: 'Danh mục sản phẩm',
              link: '/danh-sach-danh-muc'
            },
            {
              icon: 'list',
              show: true,
              text: 'Quản lý tài khoản',
              link: '/danh-sach-tai-khoan'
            }
          ]
        }
      ]
    }
  },
  methods: {
    goTo (link: any) {
      if (link !== undefined && link !== '') {
        this.$router.push(link)
      }
    },
    updateTitle (val: any) {
      store.commit('updateTitle', val)
    }
  }
})
</script>
