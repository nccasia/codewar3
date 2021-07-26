/* Vue */
import Vue from 'vue'
import router from './router'
import store from './store'
import VueResource from 'vue-resource'
/* App component */
import App from './components/App'
/* Auth plugin */
import Auth from './auth'
import Vue2Filters from 'vue2-filters'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'
Vue.use(Vuetify, {
  theme: {
    primary: '#cd5c5c'
  }
})

import vuexI18n from 'vuex-i18n'
import translationsVi from './languages/vi.json'
import translationsEn from './languages/en.json'
Vue.use(vuexI18n.plugin, store)
Vue.i18n.add('vi', translationsVi)
Vue.i18n.add('en', translationsEn)
Vue.i18n.set('vi')

import vClickOutside from 'v-click-outside'
Vue.use(vClickOutside)

import vuescroll from 'vue-scroll'
Vue.use(vuescroll)
// var vueScrollTo = require('vue-scroll-to')
// Vue.use(vueScrollTo)

Vue.use(VueResource)
Vue.config.productionTip = false
require('./utils')
Vue.use(Auth)
Vue.use(Vue2Filters)
Vue.use(require('vue-moment'))

// notification
import Notifications from 'vue-notification'
Vue.use(Notifications)

import vi from 'vee-validate/dist/locale/vi'
import VeeValidate, { Validator } from 'vee-validate'
Validator.localize('vi', vi)
Vue.use(VeeValidate)

import VueChatScroll from 'vue-chat-scroll'
Vue.use(VueChatScroll)

import NProgress from 'vue-nprogress'
Vue.use(NProgress)

const nprogress = new NProgress({ parent: '.nprogress-container' })

/* eslint-disable no-new */

new Vue({
  nprogress,
  el: '#app',
  created: function () {
    window.Vue = this
  },
  router,
  store,
  render: h => h(App)
})
