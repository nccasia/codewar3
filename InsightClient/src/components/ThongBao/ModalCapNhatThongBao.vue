<template>
  <v-layout row justify-center>
    <v-dialog v-model="isShow" persistent max-width="800px">
      <v-card>
        <v-card-title class="headline primary">
          {{ isUpdate? 'Cập nhật thông báo' : 'Tạo thông báo' }}
          <v-spacer></v-spacer>
          <v-btn icon dark @click.native="hide">
            <v-icon>close</v-icon>
          </v-btn>
        </v-card-title>
        <v-card-text class="py-0">
          <v-form scope="frmThongBao">
          <v-container grid-list-md>
            <v-layout wrap>
              <v-flex xs12>
                <v-text-field label="Tiêu đề " hide-details 
                v-model="ThongBao.TieuDe" 
                :error-messages="errors.collect('tenHoaDon')"
                v-validate="'required'"
                name="tenHoaDon" data-vv-scope="frmThongBao"
                required></v-text-field>
              </v-flex>
              <v-flex xs4>
                <v-select
                  v-model="ThongBao.ToaNhaId"
                  :items="DataToaNha"
                  label="Tòa Nhà"
                  item-value="ToaNhaId"
                  item-text="TenToaNha">
                </v-select>
              </v-flex>
              <v-flex xs3>
                <v-menu
                  ref="menu"
                  :close-on-content-click="false"
                  v-model="menu"
                  :nudge-right="40"
                  lazy
                  transition="scale-transition"
                  offset-y
                  full-width
                  min-width="290px" data-vv-scope="frmUser"
                  required
                >
                  <v-text-field
                    slot="activator"
                    label="Thời gian "
                    required
                    v-model="ThongBao.ThoiGian"
                    name="CreatedTime"
                  ></v-text-field>
                  <v-date-picker v-model="ThongBao.ThoiGian" no-title scrollable>
                    <v-spacer></v-spacer>
                    <v-btn flat color="primary" @click="menu = false">Cancel</v-btn>
                    <v-btn flat color="primary" @click="$refs.menu.save(ThongBao.ThoiGian)">OK</v-btn>
                  </v-date-picker>
                </v-menu>
              </v-flex>
              <v-flex xs5>
                <v-checkbox style="margin-top: 13px" v-model="ThongBao.LaThongBaoQuanTrong" label="Thông báo quan trọng" hide-details></v-checkbox>
              </v-flex>
              <v-flex xs12>
                <!-- <v-text-field
                  name="input-7-3"
                  label="Nội dung"
                  v-model="ThongBao.NoiDung"
                  data-vv-scope="frmThongBao"
                  multi-line
                ></v-text-field> -->
                <!-- <ckeditor ref="ckeditorref" type="classic" :config="config" v-model="ThongBao.NoiDung"></ckeditor> -->
                <vue-editor  id="editor" useCustomImageHandler @imageAdded="handleImageAdded" v-model="ThongBao.NoiDung"></vue-editor>
              </v-flex>
              <!-- <v-flex xs12>
                <v-text-field label="Nội dung " hide-details 
                v-model="ThongBao.NoiDung" 
                :error-messages="errors.collect('tenHoaDon')"
                v-validate="'required'"
                name="tenHoaDon" data-vv-scope="frmThongBao"
                required></v-text-field>
              </v-flex> -->
              <v-flex xs12 class="text-xs-right">
                <v-btn flat @click.native="isShow = false" class="mb-0">Hủy</v-btn>
                <v-btn v-if="isUpdate" class="mr-0 mb-0" color="primary" @click.native="capnhatThongBao">Cập nhật</v-btn>
                <v-btn v-if="!isUpdate" class="mr-0 mb-0" color="primary" @click.native="taoThongBao">Tạo</v-btn>
              </v-flex>
            </v-layout>
          </v-container>
          </v-form>
        </v-card-text>
      </v-card>
    </v-dialog>
  </v-layout>
</template>
<script>
import APIS from '@/apis-const.js'
import { VueEditor } from 'vue2-editor'
import {
    oHTTP
  } from '@/ohttp-services'
import {
    HTTP
  } from '@/http-services'
export default {
  name: 'ModalCapNhatThongBao',
  $_veeValidate: {
    validator: 'new'
  },
  components: {
    VueEditor
  },
  data() {
    return {
      APIS: APIS,
      msg: 'abc',
      menu: false,
      DataToaNha: [],
      isShow: false,
      isUpdate: false,
      ThongBao: {},
      ThongBaoTmp: {},
      ThongBaoOpts: []
    }
  },
  created () {
    this.getDanhSachToaNha()
  },
  methods: {
    show(ThongBao, isUpdate) {
      this.isShow = true
      this.isUpdate = isUpdate
      this.ThongBao = Object.assign({}, ThongBao)
      this.ThongBao.ThoiGian = this.ThongBao.ThoiGian ? this.ThongBao.ThoiGian.replace('T00:00:00', '') : ''
    },
    hide() {
      this.isShow = false
    },
    taoThongBao() {
      this.$validator.validateAll('frmThongBao').then((result) => {
        if (result) {
          this.$emit('taoThongBao', this.ThongBao)
        }
      })
    },
    capnhatThongBao() {
      this.$validator.validateAll('frmThongBao').then((result) => {
        if (result) {
          this.$emit('capnhatThongBao', this.ThongBao)
        }
      })
    },
    getDanhSachToaNha() {
      oHTTP('ToaNha').get(res => {
        this.DataToaNha = res
      })
    },
    handleImageAdded(file, Editor, cursorLocation) {
      var formData = new FormData()
      var re = /^\w+\.(?:jpg|png|gif|jpeg)$/g
      if (re.test(file.name)) {
        formData.append('image', file)
        HTTP.post('api/img', formData, { headers: { 'Content-Type': 'multipart/form-data' } })
          .then(res => {
            // console.log(res)
            let url = this.APIS.BASE_API + 'fileupload/download?key=' + res.data
            Editor.insertEmbed(cursorLocation, 'image', url)
          })
      } else {
        this.$notify({
          text: 'File không đúng định dạng',
          color: 'error'
        })
      }
    }
  }
}
</script>
<style>
.ck-rounded-corners .ck.ck-editor__main>.ck-editor__editable, .ck.ck-editor__main>.ck-editor__editable.ck-rounded-corners {
    border-radius: var(--ck-border-radius);
    border-top-left-radius: 0;
    border-top-right-radius: 0;
    HEIGHT: 300PX;
}
</style>
