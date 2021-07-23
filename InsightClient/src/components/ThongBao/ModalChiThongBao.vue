<template>
  <v-layout justify-center>
    <v-dialog v-model="isShow" scrollable max-width="800px">
      <v-card>
        <v-card-title class="primary">
          <div class="headline">Thông báo</div>
          <v-spacer></v-spacer>
          <v-btn @click="hide" small icon>
            <v-icon color="white">close</v-icon>
          </v-btn>
        </v-card-title>
        <v-card-text style="min-height:300px">
          <v-form scope="frmLoiPhe">
          <v-container grid-list-md style="position: relative;">
            <img width="160px" height="160px" src="/static/images/CD1.png" style="position:absolute;z-index:9; bottom: -20px;right: 60px;">
            <v-layout wrap>
              <v-flex xs12 class="mb-3">
                <h2 class="text-xs-center">{{thongBao.tieuDe}}</h2>
              </v-flex>
              <v-flex xs6>
                <label>Ngày đăng: {{thongBao.thoiGian | moment('DD/MM/YYYY, HH:mm')}}
                </label>
              </v-flex>
              <v-flex xs12>
                <div v-html="thongBao.noiDung"></div>
              </v-flex>
            </v-layout>
          </v-container>
          </v-form>
        </v-card-text>
         
        <v-card-actions>
          <v-flex class="text-xs-center">
            <v-pagination v-if="lengthPage !== 1" :length="lengthPage" circle v-model="page"></v-pagination>
          </v-flex>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-layout>
</template>
<script>
export default {
  name: 'ModalChiTietHoaDon',
  $_veeValidate: {
    validator: 'new'
  },
  data() {
    return {
      isShow: false,
      isUpdate: false,
      thongBao: {},
      user: this.$store.state.user,
      lengthPage: 1,
      page: 1,
      lstThongBao: []
    }
  },
  watch: {
    page: function (val) {
      this.thongBao = Object.assign({}, this.lstThongBao[val - 1])
    }
  },
  methods: {
    show(thongBao, page) {
      this.isShow = true
      this.lengthPage = page
      if (page === 1) {
        this.thongBao = Object.assign({}, thongBao)
      } else {
        this.lstThongBao = thongBao
        this.thongBao = Object.assign({}, this.lstThongBao[0])
      }
    },
    hide() {
      this.isShow = false
    }
  }
}
</script>
<style scopedSlots>
p img {
  min-height: 200px;
  max-height: 1000px;
  width: 100%;
}
</style>
