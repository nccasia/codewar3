import store from './store'
import APIS from './apis-const'
var o = require('odata')

export const oHTTP = o

oHTTP().config({
  endpoint: APIS.BASE_API_ODATA, // The default endpoint to use.
  // format: 'json', // The media format. Default is JSON.
  autoFormat: false, // Will always append a $format=json to each query if set to true.
  version: 3, // currently only tested for Version 4. Most will work in version 3 as well.
  strictMode: true, // strict mode throws exception, non strict mode only logs them
  start: null, // a function which is executed on loading
  ready: null, // a function which is executed on ready
  error: null, // a function which is executed on error
  headers: [
    { name: 'access_token', value: store.state.auth.accessToken },
    // { name: 'Content-Type', value: 'application/json' },
    { name: 'Access-Control-Allow-Origin', value: '*' }
  ], // an array of additional headers [{name:'headername',value:'headervalue'}]
  username: null, // the basic auth username
  password: null, // the basic auth password
  isAsync: true, // set this to false to enable sync requests. Only usable without basic auth
  isCors: false, // set this to false to disable CORS
  isHashRoute: true, // set this var to false to disable automatic #-hash setting on routes
  appending: '' // set this value to append something to a any request. eg.: [{name:'apikey', value:'xyz'}]
})
