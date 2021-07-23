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
      }
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
      }
    },
    {
      path: '/cong-viec-cua-toi',
      name: 'MyJob',
      component: function (resolve) {
        require(['@/components/Job/MyJob.vue'], resolve)
      }
    },
    {
      path: '/ds-cong-viec',
      name: 'SearchJob',
      component: function (resolve) {
        require(['@/components/Job/SearchJob.vue'], resolve)
      }
    },
    {
      path: '/chi-tiet-cong-viec/:JobK',
      name: 'JobDetail',
      component: function (resolve) {
        require(['@/components/Job/JobDetail.vue'], resolve)
      }
    },
    {
      path: '/loai-cong-viec',
      name: 'JobType',
      component: function (resolve) {
        require(['@/components/JobType/JobType.vue'], resolve)
      }
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
    next()
    // HTTP.get('auth/validatetoken')
    // .then(response => {
    //   next()
    // })
    // .catch(e => {
    //   Auth.logout()
    //   next({
    //     path: '/login',
    //     query: {
    //       redirect: to.fullPath
    //     }
    //   })
    // })
  }
}

export default router
