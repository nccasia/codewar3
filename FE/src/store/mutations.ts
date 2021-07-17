export default {
  UPDATE_APP (state: any, app: any) {
    state.app = app
  },
  UPDATE_USER (state: any, user: any) {
    state.user = user
    console.log(state.user);
  },
  UPDATE_LEFSIDEBAR (state: any) {
    state.app.drawer = !state.app.drawer
  },
  CLEAR_ALL_DATA (state: any) {
    state.user = {
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
}
