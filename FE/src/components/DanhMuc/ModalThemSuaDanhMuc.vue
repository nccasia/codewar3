<template>
    <v-dialog v-model="dialog" scrollable width="500px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật danh mục' : 'Thêm mới danh mục'}}</span>
          <v-spacer></v-spacer>
          <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
        </v-card-title>
        <v-card-text>
          <v-container v-if="loadingModal">
            <div class="text-xs-center">
              <v-progress-circular
                indeterminate
                color="primary"
                :size="70"
                :width=6
              ></v-progress-circular>
            </div>
          </v-container>
         <form scope="formEdit" v-else>
            <v-container grid-list-md pa-0>
               <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                  label="Tên danh mục"
                  v-model="data.TenDanhMuc"
                  data-vv-name="Tên danh mục"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Tên danh mục')"
                  required
                  ></v-text-field>
                </v-flex>
              </v-layout>
              <v-layout wrap>
                <v-flex xs12>
                  <v-autocomplete
                  :items="dsDanhMuc"
                  :loading="dsDanhMucLoading"
                  item-text="TenDanhMuc"
                  item-value="DanhMucId"
                  label="Danh mục cha"
                  v-model="data.DanhMucPid"
                  data-vv-name="danh mục cha"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('danh mục cha')"
                  required
                  clearable
                  ></v-autocomplete>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
                  <v-textarea
                    v-model="data.GhiChu"
                    label="Ghi chú"
                    auto-grow
                    rows="4"
                    hide-details
                  >
                  </v-textarea>
                </v-flex>
              </v-layout>
            </v-container>
          </form>
        </v-card-text>
        <v-card-actions v-if="!loadingModal">
          <v-spacer></v-spacer>
          <v-btn flat @click.native="hide">Hủy</v-btn>
          <v-btn color="blue darken-1" :loading="saving" :disabled="saving" flat @click.native="save">Lưu</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  HTTP
} from '../../HTTPServices'
import DanhMucApi from '../../apiResources/DanhMucApi'
import { DanhMuc } from '@/models/DanhMuc'

export default Vue.extend({
  name: 'ModalThemSuaDanhMuc',
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      saving: false,
      dialog: false,
      data: {} as DanhMuc,
      isUpdate: false,
      dsDanhMuc: [] as DanhMuc[],
      dsDanhMucLoading: false,
      loadingModal: false
    }
  },
  computed: {
  },
  methods: {
    hide () {
      this.dialog = false
    },
    getDanhMuc (DanhMucID: number) {
      this.dsDanhMucLoading = true
      HTTP.get('DanhMuc/danhsachcapnhat?DanhMucID=' + DanhMucID).then(res => {
        this.dsDanhMuc = res.data
        this.dsDanhMucLoading = false
      }).catch(res => {
        this.dsDanhMucLoading = false
        this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
      })
    },
    getData (DanhMucID: number) {
      this.loadingModal = true
      DanhMucApi.detail(DanhMucID)
        .then(res => {
          this.data = res
          this.loadingModal = false
        })
        .catch(res => {
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    save () {
      this.saving = true
      this.$validator.validateAll('formEdit').then((res) => {
        if (res) {
          this.$emit('save', this.data, this.isUpdate)
        } else {
          this.saving = false
          this.$notify({
            text: 'Cần điền đủ thông tin',
            color: 'error'
          })
        }
      })
    },
    show (DanhMucID: any, isUpdate: boolean) {
      Object.assign(this.$data, (this.$options as any).data.apply(this))
      if (isUpdate) {
        this.getData(DanhMucID)
      } else {
      }
      this.isUpdate = isUpdate
      this.dialog = true
      this.getDanhMuc(DanhMucID)
    }
  }
})
</script>
