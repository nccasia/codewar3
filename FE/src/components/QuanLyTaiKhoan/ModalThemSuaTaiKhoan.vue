<template>
    <v-dialog v-model="dialog" scrollable width="400px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật tài khoản' : 'Thêm mới tài khoản'}}</span>
          <v-spacer></v-spacer>
          <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
        </v-card-title>
        <v-card-text>
         <form scope="formEdit">
            <v-container grid-list-md pa-0>
               <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                  label="Tên tài khoản"
                  v-model="data.UserName"
                  data-vv-name="Tên tài khoản"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Tên tài khoản', 'formEdit')"
                  required
                  ></v-text-field>
                </v-flex>
              </v-layout>
              <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                  label="Email"
                  v-model="data.Email"
                  data-vv-name="Email"
                  data-vv-scope="formEdit"
                  v-validate="{required: false, email: true}"
                  :error-messages="errors.collect('Email', 'formEdit')"
                  ></v-text-field>
                </v-flex>
              </v-layout>
              <v-layout wrap>
                <v-flex xs12>
                  <v-autocomplete
                    :items="dsNhanVien"
                    :loading="dsNhanVienLoading"
                    label="Nhân viên"
                    item-text="HoTen"
                    item-value="NhanVienId"
                    v-model="data.NhanVienId"
                    data-vv-name="Nhân viên"
                    data-vv-scope="formEdit"
                    v-validate="{required: true}"
                    :error-messages="errors.collect('Nhân viên')"
                    required
                    clearable
                  ></v-autocomplete>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
                   <v-checkbox v-model="data.LaQuanLyNhanSu" label="Tài khoản quản lý nhân sự"></v-checkbox>
                   <v-checkbox v-model="data.LaQuanLyVacxin" label="Tài khoản quản lý vacxin"></v-checkbox>
                   <v-checkbox v-model="data.LaQuanLyTaiChinh" label="Tài khoản quản lý tài chính"></v-checkbox>
                </v-flex>
              </v-layout>
              <div><i>*Tài khoản sẽ được tự động đặt mật khẩu là 123456 (người dùng tài khoản có thể đổi mật khẩu sau đó)</i></div>
            </v-container>
          </form>
        </v-card-text>
        <v-card-actions>
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
export default Vue.extend({
  name: 'ModalThemSuaTaiKhoan',
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      saving: false,
      dialog: false,
      data: {},
      isUpdate: false,
      dsNhanVien: [],
      dsNhanVienLoading: false
    }
  },
  computed: {
  },
  methods: {
    hide () {
      this.dialog = false
    },
    getNhanVien () {
      this.dsNhanVienLoading = true
      HTTP.get('nhanvien')
        .then(res => {
          this.dsNhanVien = res.data
          this.dsNhanVienLoading = false
        })
        .catch(res => {
          this.dsNhanVienLoading = false
          this.$notify({
            text: 'Lấy dữ liệu thất bại',
            color: 'error'
          })
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
    getData (UserId: number) {
      HTTP.get('users/' + UserId)
        .then(res => {
          this.data = res.data
        })
        .catch(res => {
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    show (UserId: any, isUpdate: boolean) {
      Object.assign(this.$data, (this.$options as any).data.apply(this))
      if (isUpdate) {
        this.getData(UserId)
      } else {
      }
      this.getNhanVien()
      this.isUpdate = isUpdate
      this.dialog = true
    }
  }
})
</script>
