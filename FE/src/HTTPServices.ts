import axios, {
  AxiosRequestConfig,
  AxiosResponse,
  AxiosError,
  AxiosInstance,
  AxiosAdapter,
  Cancel,
  CancelToken,
  CancelTokenSource,
  Canceler
} from 'axios'
import store from './store/store'
import apisServer from './apisServer'

export class HTTPSingleton {
  private static instance: HTTPSingleton;
  private static config: AxiosRequestConfig = {
    baseURL: apisServer.BASE_API,
    headers: {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*'
    },
    timeout: 1000000,
    responseType: 'json',
    xsrfCookieName: 'XSRF-TOKEN',
    xsrfHeaderName: 'X-XSRF-TOKEN',
    onUploadProgress: (progressEvent: any) => { },
    onDownloadProgress: (progressEvent: any) => { },
    cancelToken: new axios.CancelToken((cancel: Canceler) => { })
  };
  private HTTP: AxiosInstance
  private constructor () {
    this.HTTP = axios.create(HTTPSingleton.config)
    this.setAccessToken()
  }
  static getInstance () {
    if (!HTTPSingleton.instance) {
      HTTPSingleton.instance = new HTTPSingleton()
    }
    return HTTPSingleton.instance
  }
  getHTTP (): AxiosInstance {
    return this.HTTP
  }
  setAccessToken (): void {
    if (store.state.user && store.state.user.AccessToken && store.state.user.AccessToken.Token) {
      this.HTTP.defaults.headers.common['access_token'] = store.state.user.AccessToken.Token
    }
  }
}
export const HTTP: AxiosInstance = HTTPSingleton.getInstance().getHTTP()
