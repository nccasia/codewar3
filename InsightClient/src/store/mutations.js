import {
  TOOGLE_LEFT_SIDE_BAR,
  TOOGLE_RIGHT_SIDE_BAR
} from './MUTATION_TYPES'

export default {
  updateTitle (state, text) {
    state.app.title = text
  },

  UPDATE_AUTH (state, auth) {
    state.auth = auth
  },

  UPDATE_USER (state, user) {
    state.user = user
    if (state.user.User.LoaiTaiKhoanId === 1) {
      state.user.HoGiaDinh = true
    } else {
      state.user.QuanTriVien = true
    }
  },
  UPDATE_USER_PHOTO (state, photo) {
    state.user.photo = photo
  },

  [TOOGLE_LEFT_SIDE_BAR] (state) {
    state.app.showLeftSideBar = !state.app.showLeftSideBar
  },

  [TOOGLE_RIGHT_SIDE_BAR] (state) {
    state.app.showRightSideBar = !state.app.showRightSideBar
  },

  /**
   * Clear each property, one by one, so reactivity still works.
   *
   * (ie. clear out state.auth.isLoggedIn so Navbar component automatically reacts to logged out state,
   * and the Navbar menu adjusts accordingly)
   *
   * TODO: use a common import of default state to reset these values with.
   */
  CLEAR_ALL_DATA (state) {
    // Auth
    state.auth.isLoggedIn = false
    state.auth.accessToken = null
    state.auth.refreshToken = null
    state.auth.effectiveTime = null
    state.auth.expiresTime = null

    // User
    state.user.User = {}
    state.user.username = ''
    state.user.fullName = ''
    state.user.photo = ''
    state.user.isAdmin = false
    state.user.isManager = false
    state.user.isUser = false
    state.user.QuanTriVien = false
    state.user.HoGiaDinh = false
    state.user.Phong = false
  }
}
