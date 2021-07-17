<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsNhanVien.q"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @input="getDataFromApi(searchParamsNhanVien)"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalThemSuaNhanVien(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex>
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="dsNhanVien"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsNhanVien"
                                :total-items="searchParamsNhanVien.totalItems"
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
                              {{ props.item.SoDienThoai1 }}
                            </td>
                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                            <v-tooltip bottom>
                              <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalThemSuaNhanVien(props.item.NhanVienId, true)">
                                <v-icon color="teal" small>edit</v-icon>
                              </v-btn>
                              <span>Cập nhật nhân viên</span>
                            </v-tooltip>
                            <v-tooltip bottom>
                              <v-btn icon slot="activator" small @click="showModalXoa(props.item.NhanVienId)" class="mx-0 my-0">
                                <v-icon color="pink" small>delete</v-icon>
                              </v-btn>
                              <span>Xoá nhân viên</span>
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
        <component :is="'ModalThemSuaNhanVien'" ref="modalThemSuaNhanVien" @save="save"/>
    </v-flex>
</template>
<script lang="ts">
import NhanVienApi, { NhanVienApiSearchParams } from '@/apiResources/NhanVienApi'
import { NhanVien } from '@/models/NhanVien'
import ModalXoa from '../Commons/ModalXoa.vue'
import ModalThemSuaNhanVien from './ModalThemSuaNhanVien.vue'

import Vue from 'vue'
export default Vue.extend({
  name: 'DanhSachNhanVien',
  components: {
    ModalThemSuaNhanVien,
    ModalXoa
  },
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      dsNhanVien: [] as NhanVien[],
      tableHeader: [
        { text: 'Tên nhân viên', align: 'center', value: 'HoTen', sortable: false },
        { text: 'Ngày sinh', align: 'center', value: 'NgaySinh', sortable: false },
        { text: 'Số điện thoại', align: 'center', value: 'SoDienThoai1', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      searchParamsNhanVien: { includeEntities: true, rowsPerPage: 10 } as NhanVienApiSearchParams,
      loadingTable: false,
      selected: 0,
      dialogConfirmDelete: false
    }
  },
  watch: {
  },
  methods: {
    getDataFromApi (searchParamsNhanVien: NhanVienApiSearchParams): void {
      this.loadingTable = true
      NhanVienApi.getDanhSach(searchParamsNhanVien).then(res => {
        this.dsNhanVien = res.Data
        this.searchParamsNhanVien.totalItems = res.Pagination.totalItems
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    showModalThemSuaNhanVien (NhanVienID: any, isUpdate: boolean) {
      (this.$refs.modalThemSuaNhanVien as any).show(NhanVienID, isUpdate)
    },
    showModalXoa (id: any) {
      this.selected = id;
      (this.$refs.modalXoa as any).show()
    },
    xoa () {
      if (this.selected == null) {
        return
      }
      NhanVienApi.delete(this.selected)
        .then(res => {
          (this.$refs.modalXoa as any).hide()
          this.getDataFromApi(this.searchParamsNhanVien)
          this.$notify({
            text: 'Xóa nhân viên thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa nhân viên thất bại',
            color: 'error'
          })
        })
    },
    save (item: any, isUpdate: boolean) {
      if (isUpdate) {
        NhanVienApi.update(item.NhanVienId, item)
          .then(res => {
            this.getDataFromApi(this.searchParamsNhanVien)
            this.$notify({
              text: 'Cập nhật nhân viên thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaNhanVien as any).hide();
            (this.$refs.modalThemSuaNhanVien as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Cập nhật nhân viên thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaNhanVien as any).saving = false
          })
      } else {
        NhanVienApi.insert(item)
          .then(res => {
            this.getDataFromApi(this.searchParamsNhanVien)
            this.$notify({
              text: 'Thêm mới nhân viên thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaNhanVien as any).hide();
            (this.$refs.modalThemSuaNhanVien as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Thêm mới nhân viên thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaNhanVien as any).saving = false
          })
      }
    }
  }
})
</script>
