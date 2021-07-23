<template>
  <!-- built files will be auto injected -->
  <div id="app" >
    <v-app id="inspire">
      <left-sidebar v-if="auth.isLoggedIn" />
      <header-menu v-if="auth.isLoggedIn" />
      <v-content>
        <v-container fluid grid-list-lg :fill-height="!auth.isLoggedIn">
          <v-layout justify-center wrap>
             <v-flex :xs12="!auth.isLoggedIn" :sm12="auth.isLoggedIn">
              <router-view></router-view>
             </v-flex>
          </v-layout>
        </v-container>
      </v-content>
    </v-app>
   
    <nprogress-container></nprogress-container>
  </div>
</template>
<script>
import HeaderMenu from './layouts/HeaderMenu'
import LeftSidebar from './layouts/LeftSidebar'
import RightSidebar from './layouts/RightSidebar'
import NprogressContainer from 'vue-nprogress/src/NprogressContainer'
import Vue from 'vue'

export default {
  name: 'app',
  components: {
    HeaderMenu,
    LeftSidebar,
    RightSidebar,
    NprogressContainer
  },
  data () {
    return {
      auth: this.$store.state.auth,
      dialog: false,
      thongBaoChung: [],
      thongBao: [
        { icon: 'assignment', iconClass: 'blue white--text', title: 'Lịch cắt điện Thứ hai ngày 22/05/2018', subtitle: '19/05/2018, 07:00:00' },
        { icon: 'notifications', iconClass: 'amber white--text', title: 'Lịch tập huấn phòng cháy tháng 4 chữa cháy', subtitle: '19/04/2018, 07:00:00' },
        { icon: 'notifications', iconClass: 'blue white--text', title: 'Thông báo về việc quản lý vận hành hầm xe', subtitle: '30/03/2018, 07:00:00' },
        { icon: 'notifications', iconClass: 'blue white--text', title: 'Báo cáo về việc xây dựng nội qui, quy chế hoạt động', subtitle: '10/03/2018, 07:00:00' }
      ]
    }
  },
  props: {
    source: String
  },
  computed: {
    showLeftSideBar () {
      return this.$store.state.app.showLeftSideBar
    },
    showRightSideBar () {
      return this.$store.state.app.showRightSideBar
    }
  },
  methods: {
    showChiTietThongBao (thongBao) {
      this.$refs.modelChiTietThongBao.show(thongBao)
    },
    getDanhSachTB () {
    }
  },
  beforeCreate () {
    Vue.prototype.$notify = (params) => {
      if (typeof params === 'string') {
        params = { title: '', text: params }
      }
      // if (typeof params === 'object') {
      //   this.$refs.notifications.show(params)
      // }
    }
  },
  created() {
    this.getDanhSachTB()
  }
}

</script>