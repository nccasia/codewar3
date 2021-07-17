<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsPhong.q"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @keyup.enter="getDataFromApi(searchParamsPhong)"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalThemSuaTaiKhoan(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex>
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="dsPhong"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsPhong"
                                :total-items="searchParamsPhong.totalItems"
                                :loading="loadingTable"
                                class="table-border table">
                        <template slot="items" slot-scope="props" style="sox-height:100px;">
                          <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.UserName }}
                          </td>
                          <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.Email }}
                          </td>
                          <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.HoTen }}
                          </td>
                          <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;">
                          <v-tooltip bottom>
                            <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalThemSuaTaiKhoan(props.item.UserId, true)">
                              <v-icon color="teal" small>edit</v-icon>
                            </v-btn>
                            <span>Cập nhật tài khoản</span>
                          </v-tooltip>
                          <v-tooltip bottom>
                            <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalComfirm(props.item.UserId)">
                              <v-icon color="teal" small>cached</v-icon>
                            </v-btn>
                            <span>Cập nhật mật khẩu</span>
                          </v-tooltip>
                          <v-tooltip bottom>
                            <v-btn icon slot="activator" small @click="showModalXoa(props.item.UserId)" class="mx-0 my-0">
                              <v-icon color="pink" small>delete</v-icon>
                            </v-btn>
                            <span>Xoá tài khoản</span>
                          </v-tooltip>
                          </td>
                        </template>
                        <template slot="no-data">
                          <div class="text-xs-center">Không có dữ liệu</div>
                        </template>
                        </v-data-table>
                    </v-flex>
                </v-layout>
            </v-card-text>
        </v-card>
        <component :is="'ModalXoa'" ref="modalXoa" @xoa="xoa"/>
        <component :is="'ModalThemSuaTaiKhoan'" ref="modalThemSuaTaiKhoan" @save="save"/>
        <v-dialog v-model="dialogConfirm" max-width="400">
                    <v-card>
                <v-card-title class="headline">Xác nhận cập nhật lại mật khẩu</v-card-title>
                <v-card-text class="pt-3">Bạn có chắc chắn muốn cập nhật lại mật khẩu (Mật khẩu sẽ được đặt mặc định là 123456)?</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn @click.native="dialogConfirm=false" flat>Hủy</v-btn>
                    <v-btn color="primary" @click.native="capNhatMatKhau" flat>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-flex>
</template>
<script lang="ts">
import { Vue } from 'vue-property-decorator'
import PhongApi, { PhongApiSearchParams } from '@/apiResources/PhongApi'
import { Phong } from '@/models/Phong'
import {
  HTTP
} from '../../HTTPServices'
import ModalXoa from '../Commons/ModalXoa.vue'
import ModalThemSuaTaiKhoan from './ModalThemSuaTaiKhoan.vue'

export default Vue.extend({
  name: 'DanhSachTaiKhoan',
  components: {
    ModalThemSuaTaiKhoan,
    ModalXoa
  },
  data () {
    return {
      dsPhong: [] as Phong[],
      tableHeader: [
        { text: 'Tên tài khoản', align: 'center', value: 'TenNhaCungCap', sortable: false },
        { text: 'Email', align: 'center', value: 'SoDienThoai', sortable: false },
        { text: 'Nhân viên', align: 'center', value: 'DiaChi', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      searchParamsPhong: { includeEntities: true, rowsPerPage: 10 } as PhongApiSearchParams,
      loadingTable: false,
      selected: 0,
      dialogConfirm: false
    }
  },
  watch: {
  },
  methods: {
    showModalComfirm (userID: number) {
      this.dialogConfirm = true
      this.selected = userID
    },
    capNhatMatKhau () {
      HTTP.put('users/capnhatmatkhau/' + this.selected).then(res => {
        this.dialogConfirm = false
        this.selected = 0
        this.$notify({
          text: 'Cập nhật mật khẩu thành công',
          color: 'success'
        })
      }).catch(res => {
        this.$notify({
          text: 'Cập nhật mật khẩu thất bại',
          color: 'error'
        })
      })
    },
    getDataFromApi (searchParamsPhong: PhongApiSearchParams): void {
      this.loadingTable = true
      HTTP.get('users/danhsach', {
        params: searchParamsPhong
      }).then(res => {
        this.dsPhong = res.data.Data
        this.searchParamsPhong.totalItems = res.data.Pagination.totalItems
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    showModalThemSuaTaiKhoan (UserId: any, isUpdate: boolean) {
      (this.$refs.modalThemSuaTaiKhoan as any).show(UserId, isUpdate)
    },
    showModalXoa (id: any) {
      this.selected = id;
      (this.$refs.modalXoa as any).show()
    },
    xoa () {
      if (this.selected == null) {
        return
      }
      HTTP.delete('Users/' + this.selected)
        .then(res => {
          (this.$refs.modalXoa as any).hide()
          this.getDataFromApi(this.searchParamsPhong)
          this.$notify({
            text: 'Xóa tài khoản thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa tài khoản thất bại',
            color: 'error'
          })
        })
    },
    save (item: any, isUpdate: boolean) {
      if (isUpdate) {
        HTTP.put('Users/' + item.UserId, item)
          .then(res => {
            this.getDataFromApi(this.searchParamsPhong)
            this.$notify({
              text: 'Cập nhật tài khoản thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaTaiKhoan as any).hide();
            (this.$refs.modalThemSuaTaiKhoan as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Cập nhật tài khoản thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaTaiKhoan as any).saving = false
          })
      } else {
        HTTP.post('Users', item)
          .then(res => {
            this.getDataFromApi(this.searchParamsPhong)
            this.$notify({
              text: 'Thêm mới tài khoản thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaTaiKhoan as any).hide();
            (this.$refs.modalThemSuaTaiKhoan as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Thêm mới tài khoản thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaTaiKhoan as any).saving = false
          })
      }
    }
  }
})
</script>
