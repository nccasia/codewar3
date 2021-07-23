<template>
    <v-dialog v-model="dialog" scrollable width="500px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật người dùng' : 'Thêm mới người dùng'}}</span>
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
                    label="Tên người dùng"
                    v-model="data.UserName"
                    data-vv-name="Tên người dùng"
                    data-vv-scope="formEdit"
                    v-validate="{required: true}"
                    :error-messages="errors.collect('Tên người dùng')"
                    required
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
              <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                    label="Email"
                    v-model="data.Email"
                    data-vv-name="Email"
                    data-vv-scope="formEdit"
                    v-validate="{required: true}"
                    :error-messages="errors.collect('Email')"
                    required
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
              <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                  type="password"
                  label="Mật khẩu"
                  v-model="data.Password"
                  data-vv-name="Mật khẩu"
                  data-vv-scope="formEdit"
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
              <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                  type="number"
                  label="Số điện thoại"
                  v-model="data.PhoneNumber"
                  data-vv-name="Số điện thoại"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Số điện thoại')"
                  required
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
                <v-flex xs12 sm6>
                  <v-menu
                    ref="menu1"
                    :close-on-content-click="false"
                    v-model="menu1"
                    :nudge-right="40"
                    lazy
                    transition="scale-transition"
                    offset-y
                    full-width
                    min-width="290px"
                  >
                    <v-text-field
                      slot="activator"
                      v-model="data.DateOfBirth"
                      label="Ngày sinh"
                      data-vv-name="Ngày sinh"
                      append-icon="event"
                      :error-messages="errors.collect('Ngày sinh')"
                      readonly
                    ></v-text-field>
                    <v-date-picker
                      ref="picker"
                      locale="vi-VN"
                      v-validate="{required: true}"
                      v-model="data.DateOfBirth"
                      data-vv-scope="formEdit"
                      :max="new Date().toISOString().substr(0, 10)"
                      min="1900-01-01"
                    ></v-date-picker>
                  </v-menu>
                </v-flex>
              </v-layout>
               <v-layout>
                <v-flex xs12>
                  <v-checkbox
                    v-model="data.Status"
                    label="Kích hoạt"
                    auto-grow
                    rows="4"
                  >
                  </v-checkbox>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
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

<script>
import Vue from 'vue'
import UserApi from '../../apiResources/UserApi'

export default {
  $_veeValidate: {
    validator: 'new'
  },
  name: 'ModalInsertOrUpdateUser',
  data () {
    return {
      menu1: false,
      saving: false,
      dialog: false,
      data: {},
      isUpdate: false,
      loadingModal: false,
      loadingUser: false
    }
  },
  computed: {
  },
  methods: {
    hide () {
      this.dialog = false
    },
    getData (id) {
      this.loadingModal = true
      UserApi.detail(id)
        .then(res => {
          this.data = res
          console.log(this.data);
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
    show (id, isUpdate) {
      Object.assign(this.$data, (this.$options).data.apply(this))
      if (isUpdate) {
        this.getData(id)
      } else {
      }
      this.isUpdate = isUpdate
      this.dialog = true
    }
  }
}
</script>
