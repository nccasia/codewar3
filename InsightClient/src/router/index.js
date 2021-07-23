import Vue from 'vue'
import Router from 'vue-router'

import { HTTP } from '@/http-services'
import Auth from '@/auth'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'main',
      redirect: { name: 'login' }
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: function (resolve) {
        require(['@/components/Dashboard/Dashboard.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/error-page',
      name: 'error-page',
      component: function (resolve) {
        require(['@/components/Login/ErrorPage.vue'], resolve)
      }
    },
    {
      path: '/login',
      name: 'login',
      component: function (resolve) {
        require(['@/components/login/Login.vue'], resolve)
      }
    },
    {
      path: '/register',
      name: 'register',
      component: function (resolve) {
        require(['@/components/register/register.vue'], resolve)
      }
    },
    {
      path: '/verify-email',
      name: 'verifyEmail',
      component: function (resolve) {
        require(['@/components/register/verifyEmail.vue'], resolve)
      }
    },
    // user
    {
      path: '/ql-cong-viec',
      name: 'Job',
      component: function (resolve) {
        require(['@/components/Job/Job.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/cong-viec-cua-toi',
      name: 'MyJob',
      component: function (resolve) {
        require(['@/components/Job/MyJob.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/ds-cong-viec',
      name: 'SearchJob',
      component: function (resolve) {
        require(['@/components/Job/SearchJob.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/chi-tiet-cong-viec/:JobK',
      name: 'JobDetail',
      component: function (resolve) {
        require(['@/components/Job/JobDetail.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/loai-cong-viec',
      name: 'JobType',
      component: function (resolve) {
        require(['@/components/JobType/JobType.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/ql-nguoi-dung',
      name: 'User',
      component: function (resolve) {
        require(['@/components/User/User.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/ql-phan-quyen',
      name: 'UserAuthorization',
      component: function (resolve) {
        require(['@/components/UserAuthorization/UserAuthorization.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/phe-duyet-dang-ky',
      name: 'ApproveJob',
      component: function (resolve) {
        require(['@/components/Job/ApproveJob.vue'], resolve)
      },
      beforeEnter: guardRoute
    },
    {
      path: '/nghiem-thu-cong-viec',
      name: 'AcceptJobFinish',
      component: function (resolve) {
        require(['@/components/Job/AcceptJobFinish.vue'], resolve)
      },
      beforeEnter: guardRoute
    }
  ]
})

function guardRoute (to, from, next) {
  // work-around to get to the Vuex store (as of Vue 2.0)
  const auth = router.app.$options.store.state.auth
  if (!auth.isLoggedIn) {
    next({
      path: '/login',
      query: {
        redirect: to.fullPath
      }
    })
  } else {
    const headers = { "Authorization": "Bearer " + auth.accessToken };
    HTTP.get('auth/validate-token', { headers })
    .then(response => {
      next()
    })
    .catch(e => {
      this.$notify({
        text: 'Phiên đăng nhập đã hết hạn',
        color: 'error'
      })
      Auth.logout()
      next({
        path: '/login',
        query: {
          redirect: to.fullPath
        }
      })
    })
  }
}

export default router
