import { STORAGE_KEY } from './state'
var Cookies = require('js-cookie')

const localStoragePlugin = (store: any) => {
  store.subscribe((mutation: any, state: any) => {
    const syncedData = { app: state.app, user: state.user }
    Cookies.set(STORAGE_KEY, JSON.stringify(syncedData))
    if (mutation.type === 'CLEAR_ALL_DATA') {
      Cookies.remove(STORAGE_KEY)
    }
  })
}

// TODO: setup env
// export default process.env.NODE_ENV !== 'production' ? [localStoragePlugin] : [localStoragePlugin]
export default [localStoragePlugin]
