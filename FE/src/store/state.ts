export const STORAGE_KEY = 'jdow344dsmbde5'
let Cookies = require('js-cookie')

let syncedData = {
  app: {
    drawer: true,
    title: 'Title'
  },
  user: {
    AccessToken: {
      IsAuthenticated: false,
      Token: null,
      UserName: null,
      RefreshToken: null,
      EffectiveTime: null,
      ExpiresTime: null
    },
    Profile: {
      UserId: null,
      Username: null,
      Email: null
    }
  }
}

const notSyncedData = {
}

// Sync with local storage.
if (Cookies.get(STORAGE_KEY)) {
  var data = Cookies.get(STORAGE_KEY)
  if (data === undefined) {
    data = ''
  } else {
    syncedData = JSON.parse(data)
  }
}

// Merge data and export it.
export const state = Object.assign(syncedData, notSyncedData)
