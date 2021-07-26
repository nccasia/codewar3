<template>
  <v-layout align-center class="register_page_wrapper mx-auto">
        <v-flex xs12>
            <v-card class="elevation-12">
              <v-progress-circular
                v-if="isLoading"
                class="loading"
                indeterminate
                color="primary"
                :size="70"
                :width=6
              ></v-progress-circular>
              <v-card-text class="text-xs-center">
                <img src="../../../assets/emailConfirm.svg" alt="email confirm">
                <template v-if="!isLoading">
                  <h1 class="text-xs-center">Xác thực email thành công!</h1>
                  <h4 class="text-xs-center">Cảm ơn bạn đã đăng ký!</h4>
                  <v-layout row wrap>
                    <v-flex xs12 class="text-xs-center">
                          <router-link to="/login">Đăng nhập</router-link>
                    </v-flex>
                  </v-layout>
                </template>
              </v-card-text>
            </v-card>
          </v-flex>
  </v-layout>
</template>

<script>
  // import Auth from '@/auth'
  export default {
    $_veeValidate: {
      validator: 'new'
    },
    name: 'verifyEmail',
    data () {
      return {
        isLoading: true,
      }
    },
    methods: {
      CheckAuthIsLogin () {
        if (this.$store.state.auth.isLoggedIn) {
          window.location.href = '/dashboard'
        }
      }
    },
    mounted () {
    },
    created () {
      this.CheckAuthIsLogin()
    }
  }

</script>
<style lang="css" scoped>
  .loading{
    height: 70px;
    position: absolute;
    width: 70px;
    left: 50%;
    top: 50%;
    transform: translateX(-50%) translateY(-50%);
  }
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
