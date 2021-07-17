import Vue from 'vue'
import './plugins/axios'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import store from './store/store'
import './registerServiceWorker'
import moment from 'moment'
import DateTimePicker from '@/components/Commons/DateTimePicker'
import VeeValidate, { Validator } from 'vee-validate'
// import CKEditor from '@ckeditor/ckeditor5-vue/dist'
// import ClassicEditor from '@ckeditor/ckeditor5-build-classic'

// Vue.use(CKEditor, {
//   editors: {
//     classic: ClassicEditor
//   }
// })

Vue.use(VeeValidate)
Validator.localize('vi')
Vue.use(DateTimePicker)
Vue.config.productionTip = false

declare module 'vue/types/vue' {
  interface Vue {
      $moment: any,
      $validator: Validator,
      $notify: any
  }
}

require('moment/locale/vi')

Vue.use(require('vue-moment'), {
  moment
})

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
