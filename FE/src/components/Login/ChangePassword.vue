<template>
  <v-layout>
    <v-flex xs12 sm4 offset-sm4 pt-5>
   <v-card xs4 offset-xs4>
      <form scope="formChangePassword">
            <v-container grid-list-md>
              <v-flex>
              <v-layout wrap style="padding: 5px 20px;">
                <v-flex xs12>
                  <v-text-field
                    v-model="change.OldPassword"
                    label="Mật khẩu hiện tại"
                    required
                    name="Mật khẩu hiện tại"
                    data-vv-scope="formChangePassword"
                    :error-messages="errors.collect('Mật khẩu hiện tại', 'formChangePassword')"
                    v-validate="'required|min: 6'"
                    :append-icon="e1 ? 'visibility' : 'visibility_off'"
                    :append-icon-cb="() => (e1 = !e1)"
                    :type="e1 ? 'password' : 'text'"
                  >
                  </v-text-field>
                  </v-flex>
                  <v-flex xs12>
                    <v-text-field
                      v-model="change.NewPassword"
                      label="Mật khẩu mới"
                      required
                      v-validate="'required|min: 6'"
                      :error-messages="errors.collect('Mật khẩu mới', 'formChangePassword')"
                      name="Mật khẩu mới"
                      data-vv-scope="formChangePassword"
                      :append-icon="e2 ? 'visibility' : 'visibility_off'"
                      :append-icon-cb="() => (e2 = !e2)"
                      :type="e2 ? 'password' : 'text'"
                      @input="changePassword"
                  >
                  </v-text-field>
                  <small class="caption">Độ mạnh của mật khẩu: <span :class="dsLoaiMatKhau[loaiMatKhau].color + '--text'">{{ dsLoaiMatKhau[loaiMatKhau].text }}</span></small>
                  </v-flex>
                  <v-flex xs12>
                    <v-text-field
                      v-model="repassword"
                      label="Nhập lại mật khẩu mới"
                      required
                      :error-messages="errors.collect('Nhập lại mật khẩu mới', 'formChangePassword')"
                      v-validate="'required|min: 6'"
                      name="Nhập lại mật khẩu mới"
                      data-vv-scope="formChangePassword"
                      :append-icon="e3 ? 'visibility' : 'visibility_off'"
                      :append-icon-cb="() => (e3 = !e3)"
                  :type="e3 ? 'password' : 'text'"
                >
                </v-text-field>
              </v-flex>
              <v-layout>
                <v-spacer></v-spacer>
                <v-btn class="primary" flat @click.native="save">Đổi mật khẩu</v-btn>
              </v-layout>
            </v-layout>
          </v-flex>
        </v-container>
      </form>
    </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import {
  HTTP
} from '@/HTTPServices'
export default {
  name: 'ChangePassword',
  data () {
    return {
      e1: true,
      e2: true,
      e3: true,
      dialog: false,
      change: {
        UserName: this.$store.state.user.Profile.Username,
        NewPassword: '',
        OldPassword: ''
      },
      dsLoaiMatKhau: [
        { color: 'error', text: 'Yếu' },
        { color: 'warning', text: 'Trung bình' },
        { color: 'success', text: 'Mạnh' }
      ],
      repassword: '',
      loaiMatKhau: 0
    }
  },
  methods: {
    changePassword (val) {
      // ít nhất 8 ký tự, có ít nhất 1 chữ và 1 số
      var reg = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/g

      // ít nhất 8 ký tự, có ít nhất 1 chữ, 1 số, 1 ký tự đặc biệt
      var reg2 = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$/g

      if (reg2.test(val)) {
        this.loaiMatKhau = 2
      } else if (reg.test(val)) {
        this.loaiMatKhau = 1
      } else {
        this.loaiMatKhau = 0
      }
    },
    save () {
      this.$validator.validateAll('formChangePassword')
        .then(res => {
          if (res && (this.repassword === this.change.NewPassword) && (this.change.NewPassword !== this.change.OldPassword)) {
            HTTP.put('ChangePassword/Change', this.change)
              .then(res => {
                this.$notify({
                  text: 'Đổi mật khẩu thành công',
                  color: 'success'
                })
                Object.assign(this.$data, this.$options.data.apply(this))
              })
              .catch(res => {
                this.$notify({
                  text: res.response.data.Message,
                  color: 'error'
                })
              })
          } else if (this.change.NewPassword === this.change.OldPassword) {
            this.$notify({
              text: 'Mật khẩu mới nhập trùng với mật khẩu hiện tại!',
              color: 'error'
            })
          } else if (this.repassword !== this.change.NewPassword) {
            this.$notify({
              text: 'Mật khẩu không khớp!',
              color: 'error'
            })
          } else {
            this.$notify({
              text: 'Vui lòng điền đủ thông tin!',
              color: 'error'
            })
          }
        })
    }
  }
}
</script>
