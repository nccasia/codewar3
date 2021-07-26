<template>
  <v-layout align-center class="register_page_wrapper mx-auto">
        <v-flex xs12 v-if="isLogin">
            <v-card class="elevation-12">
              <v-card-text>
                <v-form>
                  <v-text-field
                   v-model="credentials.email"
                   @keypress.enter="submit"
                   :rules="[rules.email]"
                   prepend-icon="mail" 
                   label="Email" 
                   type="email" 
                   :error-messages="errors.collect('Email')"
                   name="Email" 
                   data-vv-scope="formRegister"
                   v-validate="'required'"
                   required
                  ></v-text-field>
                  <v-text-field
                   v-model="credentials.username"
                   @keypress.enter="submit"
                   prepend-icon="person" 
                   label="Tài khoản" 
                   type="text" 
                   :error-messages="errors.collect('Tài khoản')"
                   name="Tài khoản" 
                   data-vv-scope="formRegister"
                   v-validate="'required'"
                   required
                  ></v-text-field>
                  <v-text-field 
                   v-model="credentials.password"
                    @keypress.enter="submit"
                    prepend-icon="vpn_key" 
                    label="Mật khẩu"
                    required
                    :error-messages="errors.collect('Mật khẩu')"
                    v-validate="'required|min: 6'"
                    name="Mật khẩu" 
                    data-vv-scope="formRegister"
                    :type="'password'"
                   ></v-text-field>
                  <v-text-field 
                   v-model="credentials.repassword"
                    @keypress.enter="submit"
                    :rules="[rules.repassword]"
                    prepend-icon="vpn_key" 
                    label="Nhắc lại mật khẩu"
                    required
                    :error-messages="errors.collect('Nhắc lại mật khẩu')"
                    name="Nhắc lại mật khẩu" 
                    data-vv-scope="formRegister"
                    :type="'password'"
                   ></v-text-field>
                </v-form>
                <v-btn block color="primary" :loading="registering" :disabled="registering" @click="submit">Đăng ký</v-btn>
                <v-layout row wrap>
                  <v-flex xs12 class="text-xs-center">
                        <router-link to="/login">Đăng nhập</router-link>
                  </v-flex>
                </v-layout>
              </v-card-text>
            </v-card>
          </v-flex>
           <v-snackbar
              :top="true"
              :timeout="3000"
              color="error"
              v-model="snackbar"
            >
              {{ error }}
              <v-btn dark flat @click.native="snackbar = false">Close</v-btn>
            </v-snackbar>
    <v-dialog v-model="dialog" max-width="350">
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">Thành công</span>
          <v-spacer></v-spacer>
          <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
        </v-card-title>
        <v-card-text class="py-3">Bạn đã đăng ký thành công, vui lòng kiểm tra email để xác nhận!</v-card-text>

      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script>
  // import Auth from '@/auth'
  export default {
    $_veeValidate: {
      validator: 'new'
    },
    name: 'login',
    data () {
      return {
        rules: {
          email: value => {
            const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return pattern.test(value) || "Định dạng email không đúng.";
          },
          repassword: value => {
            return value == this.credentials.password || "Mật khẩu nhắc lại không trùng với mật khẩu.";
          }
        },
        dialog: false,
        snackbar: false,
        credentials: {
          email: '',
          username: '',
          remember: false,
          password: '',
          repassword: '',
          changepasswordkey: ''
        },
        registering: false,
        isLogin: true,
        error: '',
        emailRules:[]
      }
    },
    methods: {
      hide() {
        this.dialog = false;
      },
      submit () {
        const credentials = {
          email:this.credentials.email,
          username: this.credentials.username,
          password: this.credentials.password,
          repassword: this.credentials.repassword,
        }
        this.$validator.validateAll('formRegister').then(res => {
          if (res) {
            // this.registering = true
            // this.$auth.login(credentials, 'dashboard')
            // .then((response) => {
            //   this.registering = false
            //   if (!this.$store.state.auth.isLoggedIn) {
            //     this.$notify({
            //       text: 'Sai tên đăng nhập hoặc mật khẩu',
            //       color: 'error'
            //     })
            //   }
            // })
          }
        }).catch(() => {
          console.log(this.error)
        })
      },
      CheckAuthIsLogin () {
        if (this.$store.state.auth.isLoggedIn) {
          window.location.href = '/dashboard'
        }
      }
    },
    mounted () {
    },
    created () {
      this.isLogin = true
      this.CheckAuthIsLogin()
    }
  }

</script>
<style lang="css" scoped>
  body {
      font-size: 15px;
      padding: 64px 24px;
      overflow-y: auto;
      height: auto;
      min-height: 100%;
  }
  .register_page_wrapper {
      width: 720px;
      max-width: 100%;
      margin: 0 auto;
      -webkit-transition: all 280ms cubic-bezier(.4,0,.2,1);
      transition: all 280ms cubic-bezier(.4,0,.2,1);
  }
  .md-input-container input, .md-input-container textarea {
    height: auto;
  }
  #page_content{
    margin-right:0
  }
</style>
