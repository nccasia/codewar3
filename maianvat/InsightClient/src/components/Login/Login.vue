<template>
  <v-layout align-center class="login_page_wrapper mx-auto">
        <v-flex xs12 v-if="isLogin">
            <v-card class="elevation-12">
              <v-card-text>
                <v-form>
                  <v-text-field
                   v-model="credentials.username"
                   @keypress.enter="submit"
                   prepend-icon="person" 
                   label="Tài khoản" 
                   type="text" 
                   :error-messages="errors.collect('Tài khoản')"
                   name="Tài khoản" 
                   data-vv-scope="formLogin"
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
                    data-vv-scope="formLogin"
                    :append-icon="e1 ? 'visibility' : 'visibility_off'"
                    :append-icon-cb="() => (e1 = !e1)"
                    :type="e1 ? 'password' : 'text'"
                   ></v-text-field>
                </v-form>
                <v-btn block color="primary" :loading="loggingIn" :disabled="loggingIn" @click="submit">Đăng nhập</v-btn>
                <v-layout row wrap>
                  <v-flex xs6 class="text-xs-left">
                        <router-link to="/request-change-password">Quên mật khẩu ?</router-link>
                  </v-flex>
                  <v-flex xs6 class="text-xs-right">
                        <router-link to="/register">Đăng ký</router-link>
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
        snackbar: false,
        credentials: {
          username: '',
          remember: false,
          password: '',
          repassword: '',
          changepasswordkey: ''
        },
        loggingIn: false,
        isLogin: true,
        error: '',
        e1: true
      }
    },
    methods: {
      submit () {
        const credentials = {
          username: this.credentials.username,
          password: this.credentials.password,
          repassword: this.credentials.repassword,
          changepasswordkey: this.credentials.changepasswordkey
        }
        this.$validator.validateAll('formLogin').then(res => {
          if (res) {
            this.loggingIn = true
            this.$auth.login(credentials, 'dashboard')
            .then((response) => {
              this.loggingIn = false
              if (!this.$store.state.auth.isLoggedIn) {
                this.$notify({
                  text: 'Sai tên đăng nhập hoặc mật khẩu',
                  color: 'error'
                })
              }
            })
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
  .login_page_wrapper {
      width: 360px;
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
