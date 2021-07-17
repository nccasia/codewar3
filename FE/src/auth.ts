import router from './router'
import store from './store/store'
import apisServer from './apisServer'
import axios from 'axios'
import { HTTPSingleton } from '@/HTTPServices'

/**
 * @var{string} LOGIN_URL The endpoint for logging in. This endpoint should be proxied by Webpack dev server
 *    and maybe nginx in production (cleaner calls and avoids CORS issues).
 */
const LOGIN_URL = apisServer.AUTH_LOGIN

/**
 * @var{string} REFRESH_TOKEN_URL The endpoint for refreshing an access_token. This endpoint should be proxied
 *    by Webpack dev server and maybe nginx in production (cleaner calls and avoids CORS issues).
 */
const REFRESH_TOKEN_URL = apisServer.AUTH_LOGIN

/**
 *
 */
export default {
  /**
  * Login
  *
  * @param {Object.<string>} creds The username and password for logging in.
  * @param {string|null} redirect The name of the Route to redirect to.
  * @return {Promise}
  */
  login (creds: any, redirect: any) {
    let promise = new Promise((resolve, reject) => {
      axios.post(LOGIN_URL, creds).then((response: any) => {
        if (response.data === 'Login failed!') {
          reject(response.data)
          return
        }
        this._storeToken(response.data)
        HTTPSingleton.getInstance().setAccessToken()
        if (redirect) {
          router.push({
            name: redirect
          })
        }
        resolve(response.data)
      }).catch(err => {
        reject(err)
      })
    })
    return promise
  },
  /**
   * Logout
   *
   * Clear all data in our Vuex store (which resets logged-in status) and redirect back
   * to login form.
   *
   * @return {void}
   */
  logout () {
    store.commit('CLEAR_ALL_DATA')
    router.push({
      name: 'login'
    })
  },
  /**
   * Set the Authorization header on a Vue-resource Request.
   *
   * @param {Request} request The Vue-Resource Request instance to set the header on.
   * @return {void}
   */
  setAuthHeader (request: any) {
    request.headers.set('access_token', store.state.auth.accessToken)
    // The demo Oauth2 server we are using requires this param, but normally you only set the header.
    /* eslint-disable camelcase */
    request.params.access_token = store.state.auth.accessToken
  },
  /**
   * Store tokens
   *
   * Update the Vuex store with the access/refresh tokens received from the response from
   * the Oauth2 server.
   *
   * @private
   * @param {Response} response Vue-resource Response instance from an OAuth2 server.
   *      that contains our tokens.
   * @return {void}
   */
  _storeToken (data: any) {
    store.commit('UPDATE_USER', data)
  }
}
