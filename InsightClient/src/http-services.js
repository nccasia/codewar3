import axios from 'axios'
import store from './store'
import APIS from './apis-const'

export const HTTP = axios.create({
  baseURL: APIS.BASE_API,
  headers: {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*'
  }
})

// Add a request interceptor
HTTP.interceptors.request.use(function (config) {
  // Do something before request is sent
  config.headers.common['access_token'] = store.state.auth.accessToken
  return config
}, function (error) {
  // Do something with request error
  console.log(error)
  return Promise.reject(error)
})
// Add a response interceptor
HTTP.interceptors.response.use(function (response) {
  // Do something with response data
  return response
}, function (error) {
  console.log(error)
  // Do something with response error
  return Promise.reject(error)
})

export const HTTPDownload = axios.create({
  baseURL: APIS.BASE_API,
  headers: {
    // access_token: store.state.auth.accessToken,
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*'
  },
  responseType: 'blob'
})

// Add a request interceptor
HTTPDownload.interceptors.request.use(function (config) {
  // Do something before request is sent
  config.headers['access_token'] = store.state.auth.accessToken
  return config
}, function (error) {
  // Do something with request error
  console.log(error)
  return Promise.reject(error)
})
// Add a response interceptor
HTTPDownload.interceptors.response.use(function (response) {
  // Do something with response data
  return response
}, function (error) {
  console.log(error)
  // Do something with response error
  return Promise.reject(error)
})
