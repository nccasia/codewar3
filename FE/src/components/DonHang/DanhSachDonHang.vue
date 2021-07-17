<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsDonHang.q"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @input="getDataFromApi(searchParamsDonHang)"
                  ></v-text-field>
                </v-flex>
                <!-- <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalThemSuaDonHang(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex> -->
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="dsDonHang"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsDonHang"
                                :total-items="searchParamsDonHang.totalItems"
                                :loading="loadingTable"
                                class="table-border table">
                          <template slot="items" slot-scope="props" style="sox-height:100px;">
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.DonHangId }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.HoTenNhanVien }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.HoTenKhachHang }}
                            </td>
                             <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.NgayXuat | moment("DD/MM/YYYY")}}
                            </td>
                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                            <v-tooltip bottom>
                              <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalThemSuaDonHang(props.item.DonHangId, true)">
                                <v-icon color="teal" small>edit</v-icon>
                              </v-btn>
                              <span>Cập nhật đơn hàng</span>
                            </v-tooltip>
                            <v-tooltip bottom>
                              <v-btn icon slot="activator" small @click="showModalXoa(props.item.DonHangId)" class="mx-0 my-0">
                                <v-icon color="pink" small>delete</v-icon>
                              </v-btn>
                              <span>Xoá đơn hàng</span>
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
        <!-- <component :is="'ModalThemSuaDonHang'" ref="modalThemSuaDonHang" @save="save"/> -->
    </v-flex>
</template>
<script lang="ts">
import DonHangApi, { DonHangApiSearchParams } from '@/apiResources/DonHangApi'
import { DonHang } from '@/models/DonHang'
import ModalXoa from '../Commons/ModalXoa.vue'
// import ModalThemSuaDonHang from './ModalThemSuaDonHang.vue'

import Vue from 'vue'
export default Vue.extend({
  name: 'DanhSachDonHang',
  components: {
    // ModalThemSuaDonHang,
    ModalXoa
  },
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      dsDonHang: [] as DonHang[],
      tableHeader: [
        { text: 'Mã đơn hàng', align: 'center', value: 'HoTen', sortable: false },
        { text: 'Nhân viên', align: 'center', value: 'NgaySinh', sortable: false },
        { text: 'Khách hàng', align: 'center', value: 'SoDienThoai', sortable: false },
        { text: 'Ngày chuyển', align: 'center', value: 'Email', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      searchParamsDonHang: { includeEntities: true, rowsPerPage: 10 } as DonHangApiSearchParams,
      loadingTable: false,
      selected: 0,
      dialogConfirmDelete: false
    }
  },
  watch: {
  },
  methods: {
    getDataFromApi (searchParamsDonHang: DonHangApiSearchParams): void {
      this.loadingTable = true
      DonHangApi.getDanhSach(searchParamsDonHang).then(res => {
        this.dsDonHang = res.Data
        this.searchParamsDonHang.totalItems = res.Pagination.totalItems
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    showModalThemSuaDonHang (DonHangID: any, isUpdate: boolean) {
      // (this.$refs.modalThemSuaDonHang as any).show(DonHangID, isUpdate)
    },
    showModalXoa (id: any) {
      this.selected = id;
      (this.$refs.modalXoa as any).show()
    },
    xoa () {
      if (this.selected == null) {
        return
      }
      DonHangApi.delete(this.selected)
        .then(res => {
          (this.$refs.modalXoa as any).hide()
          this.getDataFromApi(this.searchParamsDonHang)
          this.$notify({
            text: 'Xóa đơn hàng thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa đơn hàng thất bại',
            color: 'error'
          })
        })
    },
    save (item: any, isUpdate: boolean) {
      if (isUpdate) {
        DonHangApi.update(item.DonHangId, item)
          .then(res => {
            this.getDataFromApi(this.searchParamsDonHang)
            this.$notify({
              text: 'Cập nhật đơn hàng thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaDonHang as any).hide();
            (this.$refs.modalThemSuaDonHang as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Cập nhật đơn hàng thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaDonHang as any).saving = false
          })
      } else {
        DonHangApi.insert(item)
          .then(res => {
            this.getDataFromApi(this.searchParamsDonHang)
            this.$notify({
              text: 'Thêm mới đơn hàng thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaDonHang as any).hide();
            (this.$refs.modalThemSuaDonHang as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Thêm mới đơn hàng thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaDonHang as any).saving = false
          })
      }
    }
  }
})
</script>
