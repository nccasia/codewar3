// Set the key we'll use in local storage.
// Go to Chrome dev tools, application tab, click "Local Storage" and "http://localhost:8080"
// and you'll see this key set below (if logged in):
export const STORAGE_KEY = 'insight-client-system'

let syncedData = {
  auth: {
    isLoggedIn: false,
    accessToken: null,
    refreshToken: null,
    effectiveTime: null,
    expiresTime: null
  },
  user: {
    username: '',
    fullName: '',
    photo: null,
    isAdmin: true,
    isManager: false,
    isUser: false
  }
}

const notSyncedData = {
  app: {
    showLeftSideBar: true,
    showRightSideBar: false,
    searchText: '',
    searchTimestamp: null,
    title: ''
  }
}

// Sync with local storage.
if (localStorage.getItem(STORAGE_KEY)) {
  syncedData = JSON.parse(localStorage.getItem(STORAGE_KEY))
}

// Merge data and export it.
export const state = Object.assign(syncedData, notSyncedData)
