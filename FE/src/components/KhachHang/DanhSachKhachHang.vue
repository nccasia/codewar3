<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsKhachHang.q"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @input="getDataFromApi(searchParamsKhachHang)"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalThemSuaKhachHang(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex>
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="dsKhachHang"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsKhachHang"
                                :total-items="searchParamsKhachHang.totalItems"
                                :loading="loadingTable"
                                class="table-border table">
                          <template slot="items" slot-scope="props" style="sox-height:100px;">
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.HoTen }}
                            </td>
                             <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.NgaySinh | moment("DD/MM/YYYY")}}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.SoDienThoai }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.Email }}
                            </td>
                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                            <v-tooltip bottom>
                              <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalThemSuaKhachHang(props.item.KhachHangId, true)">
                                <v-icon color="teal" small>edit</v-icon>
                              </v-btn>
                              <span>Cập nhật khách hàng</span>
                            </v-tooltip>
                            <v-tooltip bottom>
                              <v-btn icon slot="activator" small @click="showModalXoa(props.item.KhachHangId)" class="mx-0 my-0">
                                <v-icon color="pink" small>delete</v-icon>
                              </v-btn>
                              <span>Xoá khách hàng</span>
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
        <component :is="'ModalThemSuaKhachHang'" ref="modalThemSuaKhachHang" @save="save"/>
    </v-flex>
</template>
<script lang="ts">
import KhachHangApi, { KhachHangApiSearchParams } from '@/apiResources/KhachHangApi'
import { KhachHang } from '@/models/KhachHang'
import ModalXoa from '../Commons/ModalXoa.vue'
import ModalThemSuaKhachHang from './ModalThemSuaKhachHang.vue'

import Vue from 'vue'
export default Vue.extend({
  name: 'DanhSachKhachHang',
  components: {
    ModalThemSuaKhachHang,
    ModalXoa
  },
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      dsKhachHang: [] as KhachHang[],
      tableHeader: [
        { text: 'Tên khách hàng', align: 'center', value: 'HoTen', sortable: false },
        { text: 'Ngày sinh', align: 'center', value: 'NgaySinh', sortable: false },
        { text: 'Số điện thoại', align: 'center', value: 'SoDienThoai', sortable: false },
        { text: 'Email', align: 'center', value: 'Email', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      searchParamsKhachHang: { includeEntities: true, rowsPerPage: 10 } as KhachHangApiSearchParams,
      loadingTable: false,
      selected: 0,
      dialogConfirmDelete: false
    }
  },
  watch: {
  },
  methods: {
    getDataFromApi (searchParamsKhachHang: KhachHangApiSearchParams): void {
      this.loadingTable = true
      KhachHangApi.getDanhSach(searchParamsKhachHang).then(res => {
        this.dsKhachHang = res.Data
        this.searchParamsKhachHang.totalItems = res.Pagination.totalItems
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    showModalThemSuaKhachHang (KhachHangID: any, isUpdate: boolean) {
      (this.$refs.modalThemSuaKhachHang as any).show(KhachHangID, isUpdate)
    },
    showModalXoa (id: any) {
      this.selected = id;
      (this.$refs.modalXoa as any).show()
    },
    xoa () {
      if (this.selected == null) {
        return
      }
      KhachHangApi.delete(this.selected)
        .then(res => {
          (this.$refs.modalXoa as any).hide()
          this.getDataFromApi(this.searchParamsKhachHang)
          this.$notify({
            text: 'Xóa khách hàng thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa khách hàng thất bại',
            color: 'error'
          })
        })
    },
    save (item: any, isUpdate: boolean) {
      if (isUpdate) {
        KhachHangApi.update(item.KhachHangId, item)
          .then(res => {
            this.getDataFromApi(this.searchParamsKhachHang)
            this.$notify({
              text: 'Cập nhật khách hàng thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaKhachHang as any).hide();
            (this.$refs.modalThemSuaKhachHang as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Cập nhật khách hàng thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaKhachHang as any).saving = false
          })
      } else {
        KhachHangApi.insert(item)
          .then(res => {
            this.getDataFromApi(this.searchParamsKhachHang)
            this.$notify({
              text: 'Thêm mới khách hàng thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaKhachHang as any).hide();
            (this.$refs.modalThemSuaKhachHang as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Thêm mới khách hàng thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaKhachHang as any).saving = false
          })
      }
    }
  }
})
</script>
