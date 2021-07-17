import Vue from 'vue'
import Router, { Route } from 'vue-router'
import store from './store/store'
import { HTTP } from '@/HTTPServices'
import Home from './components/Home.vue'
import NotFoundComponent from './components/NotFound.vue'
import Login from './components/Login/Login.vue'
import DanhSachDanhMuc from './components/DanhMuc/DanhSachDanhMuc.vue'
import DanhSachSanPham from './components/SanPham/DanhSachSanPham.vue'
import DanhSachNhanVien from './components/NhanVien/DanhSachNhanVien.vue'
import DanhSachKhachHang from './components/KhachHang/DanhSachKhachHang.vue'
import DanhSachDonHang from './components/DonHang/DanhSachDonHang.vue'
import QuanLyTaiKhoan from './components/QuanLyTaiKhoan/DanhSachTaiKhoan.vue'

Vue.use(Router)
export default new Router({
  routes: [
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/',
      name: 'Home',
      component: Home,
      beforeEnter: guardRoute
    },
    {
      path: '/danh-sach-tai-khoan',
      name: 'QuanLyTaiKhoan',
      component: QuanLyTaiKhoan
      // beforeEnter: guardRoute
    },
    {
      path: '/danh-sach-danh-muc',
      name: 'DanhSachDanhMuc',
      component: DanhSachDanhMuc
      // beforeEnter: guardRoute
    },
    {
      path: '/danh-sach-san-pham',
      name: 'DanhSachDanhMucSanPham',
      component: DanhSachSanPham
      // beforeEnter: guardRoute
    },
    {
      path: '/danh-sach-nhan-vien',
      name: 'DanhSachNhanVien',
      component: DanhSachNhanVien
      // beforeEnter: guardRoute
    },
    {
      path: '/danh-sach-khach-hang',
      name: 'DanhSachKhachHang',
      component: DanhSachKhachHang
      // beforeEnter: guardRoute
    },
    {
      path: '/danh-sach-don-hang',
      name: 'DanhSachDonHang',
      component: DanhSachDonHang
      // beforeEnter: guardRoute
    },
    { path: '*', component: NotFoundComponent }
  ]
})
function guardRoute (to: Route, from: Route, next: any): void {
  const isAuthenticated = store.state.user && store.state.user.AccessToken ? store.state.user.AccessToken.IsAuthenticated : false
  if (!isAuthenticated) {
    next({
      path: '/login',
      query: {
        redirect: to.fullPath
      }
    })
  } else {
    HTTP.get('/auth/validate-token/')
      .then(response => {
        next()
      })
      .catch(e => {
        store.commit('CLEAR_ALL_DATA')
        next({
          path: '/login',
          query: {
            redirect: to.fullPath
          }
        })
      })
  }
}
