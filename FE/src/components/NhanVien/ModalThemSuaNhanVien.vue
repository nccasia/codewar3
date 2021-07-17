<template>
    <v-dialog v-model="dialog" scrollable width="800px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật nhân viên' : 'Thêm mới nhân viên'}}</span>
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
                  label="Tên nhân viên"
                  v-model="data.HoTen"
                  data-vv-name="Tên nhân viên"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Tên nhân viên')"
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
                <v-flex xs12 sm4>
                  <v-text-field
                  label="Số điện thoại 1"
                  v-model="data.SoDienThoai1"
                  data-vv-name="Số điện thoại 1"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Số điện thoại 1')"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm4>
                  <v-text-field
                  label="Số điện thoại 2"
                  v-model="data.SoDienThoai2"
                  data-vv-name="Số điện thoại 2"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('Số điện thoại 2')"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm4>
                  <v-text-field
                  label="Email"
                  v-model="data.Email"
                  data-vv-name="Email"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('Email')"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 my-3>
                  <v-layout>
                    <v-divider></v-divider>
                  </v-layout>
                </v-flex>
                <v-flex xs12>
                  <h4>Chứng minh thư:</h4>
                </v-flex>
                <v-flex xs12 sm4>
                  <v-text-field
                  label="Số chứng minh thư"
                  v-model="data.SoCmt"
                  data-vv-name="Số chứng minh thư"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('Số chứng minh thư')"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm4>
                  <v-datepicker
                  label="Ngày cấp"
                  v-model="data.NgayCapCmt"
                  data-vv-name="Ngày cấp"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('Ngày cấp')"
                  ></v-datepicker>
                </v-flex>
                <v-flex xs12 sm4>
                  <v-text-field
                  label="Nới cấp"
                  v-model="data.NoiCapCmt"
                  data-vv-name="Nới cấp"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('Nơi cấp')"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 my-3>
                  <v-layout>
                    <v-divider></v-divider>
                  </v-layout>
                </v-flex>
                <v-flex xs12>
                  <h4>Địa chỉ:</h4>
                </v-flex>
                <v-flex xs12 sm4>
                  <v-autocomplete
                    label="Thành phố"
                    :items="dsThanhPho"
                    :loading="dsThanhPhoLoading"
                    item-text="TenThanhPho"
                    item-value="ThanhPhoId"
                    v-model="data.ThanhPhoId"
                    data-vv-name="Thành phố"
                    @change="changeThanhPho"
                    data-vv-scope="formEdit"
                    v-validate="{required: false}"
                    :error-messages="errors.collect('Thành phố')"
                    clearable
                  ></v-autocomplete>
                </v-flex>
                <v-flex xs12 sm4>
                  <v-autocomplete
                    label="Quận/Huyện"
                    :items="dsQuanHuyen"
                    :loading="dsQuanHuyenLoading"
                    item-text="TenQuanHuyen"
                    item-value="QuanHuyenId"
                    v-model="data.QuanHuyenId"
                    data-vv-name="Quận/Huyện"
                    data-vv-scope="formEdit"
                    v-validate="{required: false}"
                    @change="changeQuanHuyen"
                    :error-messages="errors.collect('Quận/Huyện')"
                    clearable
                  ></v-autocomplete>
                </v-flex>
                <v-flex xs12 sm4>
                  <v-autocomplete
                    label="Xã phường"
                    :items="dsXaPhuong"
                    :loading="dsXaPhuongLoading"
                    item-text="TenXaPhuong"
                    item-value="XaPhuongId"
                    v-model="data.XaPhuongId"
                    data-vv-name="Xã phường"
                    data-vv-scope="formEdit"
                    v-validate="{required: false}"
                    :error-messages="errors.collect('Xã phường')"
                    clearable
                  ></v-autocomplete>
                </v-flex>
                <v-flex xs12>
                   <v-text-field
                  label="Đường - Số nhà"
                  v-model="data.DiaChiChiTiet"
                  data-vv-name="Đường - Số nhà"
                  data-vv-scope="formEdit"
                  v-validate="{required: false}"
                  :error-messages="errors.collect('Đường - Số nhà')"
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
import NhanVienApi from '../../apiResources/NhanVienApi'
import { NhanVien } from '@/models/NhanVien'

export default Vue.extend({
  name: 'ModalThemSuaNhanVien',
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      saving: false,
      dialog: false,
      data: {} as NhanVien,
      isUpdate: false,
      dsNhanVien: [] as NhanVien[],
      dsNhanVienLoading: false,
      loadingModal: false,
      dsThanhPho: [],
      dsThanhPhoLoading: false,
      dsQuanHuyen: [],
      dsQuanHuyenLoading: false,
      dsXaPhuong: [],
      dsXaPhuongLoading: false
    }
  },
  mounted () {
    this.getThanhPho()
  },
  computed: {
  },
  methods: {
    hide () {
      this.dialog = false
    },
    getData (NhanVienID: number) {
      this.loadingModal = true
      NhanVienApi.detail(NhanVienID)
        .then(res => {
          this.data = res
          this.loadingModal = false
          if (this.data.QuanHuyenId != null) {
            this.getQuanHuyen(this.data.QuanHuyenId)
          }
          if (this.data.XaPhuongId != null) {
            this.getXaPhuong(this.data.XaPhuongId)
          }
        })
        .catch(res => {
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    changeThanhPho (id: number) {
      if (id !== undefined) {
        this.data.QuanHuyenId = null as any
        this.data.XaPhuongId = null as any
        this.data.ThanhPhoId = id
        this.getQuanHuyen(id)
      }
    },
    changeQuanHuyen (id: number) {
      if (id !== undefined) {
        this.data.QuanHuyenId = id
        this.data.XaPhuongId = null as any
        this.getXaPhuong(id)
      }
    },
    getThanhPho () {
      this.dsThanhPhoLoading = true
      HTTP.get('DiaChi/ThanhPho').then(response => {
        this.dsThanhPhoLoading = false
        this.dsThanhPho = response.data
      })
        .catch(res => {
          this.dsThanhPhoLoading = false
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    getQuanHuyen (id: number) {
      this.dsQuanHuyenLoading = true
      HTTP.get('DiaChi/QuanHuyen?ThanhPhoId=' + id).then(response => {
        this.dsQuanHuyenLoading = false
        this.dsQuanHuyen = response.data
      })
        .catch(res => {
          this.dsQuanHuyenLoading = false
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    getXaPhuong (id: number) {
      this.dsXaPhuongLoading = true
      HTTP.get('DiaChi/XaPhuong?QuanHuyenId=' + id).then(response => {
        this.dsXaPhuongLoading = false
        this.dsXaPhuong = response.data
      })
        .catch(res => {
          this.dsXaPhuongLoading = false
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
    show (NhanVienID: any, isUpdate: boolean) {
      this.data = {} as NhanVien
      if (isUpdate) {
        this.getData(NhanVienID)
      } else {
      }
      this.isUpdate = isUpdate
      this.dialog = true
    }
  }
})
</script>
