<template>
    <v-dialog v-model="dialog" scrollable width="800px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật khách hàng' : 'Thêm mới khách hàng'}}</span>
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
                <v-flex xs12 sm7>
                  <v-text-field
                  label="Tên khách hàng"
                  v-model="data.HoTen"
                  data-vv-name="Tên khách hàng"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Tên khách hàng')"
                  required
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm3>
                  <v-datepicker
                  label="Ngày sinh"
                  v-model="data.NgaySinh"
                  data-vv-name="Ngày sinh"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Ngày sinh')"
                  required
                  ></v-datepicker>
                </v-flex>
                <v-flex xs12 sm2>
                  <v-checkbox
                    label="Giới tính"
                    v-model="data.GioiTinh"
                    data-vv-name="Giới tính"
                    data-vv-scope="formEdit"
                    v-validate="{required: true}"
                    :error-messages="errors.collect('Giới tính')"
                    required
                  ></v-checkbox>
                </v-flex>
                <v-flex xs12 sm6>
                  <v-text-field
                  label="Số điện thoại"
                  v-model="data.SoDienThoai"
                  data-vv-name="Số điện thoại"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('Số điện thoại')"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6>
                  <v-text-field
                  label="Email"
                  v-model="data.Email"
                  data-vv-name="Email"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('Email')"
                  ></v-text-field>
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
              <v-layout my-4  v-if="isUpdate">
                <v-divider></v-divider>
              </v-layout>
              <v-layout v-if="isUpdate" wrap>
                <v-flex xs12>
                  <h4>Địa chỉ:</h4>
                </v-flex>
                <v-flex xs12>
                  <v-data-table
                    :headers="tableHeader"
                    :items="dsDiaChiKhachHang"
                    :loading="dsDiaChiKhachHangLoading"
                    hide-actions
                    class="table-border table"
                  >
                    <template slot="items" slot-scope="props" style="sox-height:100px;">
                      <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.TenThanhPho }}
                      </td>
                       <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.TenQuanHuyen | moment("DD/MM/YYYY")}}
                      </td>
                      <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.TenXaPhuong }}
                      </td>
                      <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.DiaChiChiTiet }}
                      </td>
                      <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.SoDienThoai }}
                      </td>
                      <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.GhiChu }}
                      </td>
                    </template>
                    <template slot="no-data">
                      <div class="text-xs-center">Không có dữ liệu</div>
                    </template>
                  </v-data-table>
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
import KhachHangApi from '../../apiResources/KhachHangApi'
import { KhachHang } from '@/models/KhachHang'

export default Vue.extend({
  name: 'ModalThemSuaKhachHang',
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      saving: false,
      dialog: false,
      data: {} as KhachHang,
      isUpdate: false,
      dsDiaChiKhachHang: [],
      dsDiaChiKhachHangLoading: false,
      loadingModal: false,
      tableHeader: [
        { text: 'Thành phố', align: 'center', value: 'ThanhPhoId', sortable: false },
        { text: 'Xã phường', align: 'center', value: 'XaPhuongId', sortable: false },
        { text: 'Quận huyện', align: 'center', value: 'QuanHuyenId', sortable: false },
        { text: 'Chi tiết địa chỉ', align: 'center', value: 'ChiTietDiaChi', sortable: false },
        { text: 'Số điện thoại', align: 'center', value: 'SoDienThoai', sortable: false }
      ]
    }
  },
  computed: {
  },
  methods: {
    hide () {
      this.dialog = false
    },
    getDiaChiKhachHang (KhachHangID: number) {
      this.dsDiaChiKhachHangLoading = true
      HTTP.get('khachhang/' + KhachHangID + '/diachikhachhang').then(res => {
        this.dsDiaChiKhachHang = res.data
        this.dsDiaChiKhachHangLoading = false
      }).catch(res => {
        this.dsDiaChiKhachHangLoading = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    getData (KhachHangID: number) {
      this.loadingModal = true
      KhachHangApi.detail(KhachHangID)
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
    show (KhachHangID: any, isUpdate: boolean) {
      this.data = {} as KhachHang
      if (isUpdate) {
        this.getData(KhachHangID)
        this.getDiaChiKhachHang(KhachHangID)
      } else {
      }
      this.isUpdate = isUpdate
      this.dialog = true
    }
  }
})
</script>
