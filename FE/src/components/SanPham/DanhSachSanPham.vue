<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsSanPham.q"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @input="getDataFromApi(searchParamsSanPham)"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalThemSuaSanPham(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex>
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="dsSanPham"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsSanPham"
                                :total-items="searchParamsSanPham.totalItems"
                                :loading="loadingTable"
                                class="table-border table">
                          <template slot="items" slot-scope="props" style="sox-height:100px;">
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.TenSanPham }}
                            </td>
                             <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.TenDanhMucCha}}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.GhiChu }}
                            </td>
                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                            <v-tooltip bottom>
                              <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalThemSuaSanPham(props.item.SanPhamId, true)">
                                <v-icon color="teal" small>edit</v-icon>
                              </v-btn>
                              <span>Cập nhật sản phẩm</span>
                            </v-tooltip>
                            <v-tooltip bottom>
                              <v-btn icon slot="activator" small @click="showModalXoa(props.item.SanPhamId)" class="mx-0 my-0">
                                <v-icon color="pink" small>delete</v-icon>
                              </v-btn>
                              <span>Xoá sản phẩm</span>
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
        <component :is="'ModalThemSuaSanPham'" ref="modalThemSuaSanPham" @save="save"/>
    </v-flex>
</template>
<script lang="ts">
import SanPhamApi, { SanPhamApiSearchParams } from '@/apiResources/SanPhamApi'
import { SanPham } from '@/models/SanPham.ts'
import ModalXoa from '../Commons/ModalXoa.vue'
import ModalThemSuaSanPham from './ModalThemSuaSanPham.vue'
import DanhMucSanPhamApi from '@/apiResources/DanhMucSanPhamApi'

import Vue from 'vue'
export default Vue.extend({
  name: 'DanhSachSanPham',
  components: {
    ModalThemSuaSanPham,
    ModalXoa
  },
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      dsSanPham: [] as SanPham[],
      tableHeader: [
        { text: 'Tên sản phẩm', align: 'center', value: 'TenSanPham', sortable: false },
        { text: 'Danh mục sản phẩm', align: 'center', value: 'SanPhamPID', sortable: false },
        { text: 'Ghi chú', align: 'center', value: 'GhiChu', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      searchParamsSanPham: { includeEntities: true, rowsPerPage: 10 } as SanPhamApiSearchParams,
      loadingTable: false,
      selected: 0,
      dialogConfirmDelete: false
    }
  },
  watch: {
  },
  methods: {
    getDataFromApi (searchParamsSanPham: SanPhamApiSearchParams): void {
      this.loadingTable = true
      SanPhamApi.getDanhSach(searchParamsSanPham).then(res => {
        this.dsSanPham = res.Data
        this.searchParamsSanPham.totalItems = res.Pagination.totalItems
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    showModalThemSuaSanPham (SanPhamID: any, isUpdate: boolean) {
      (this.$refs.modalThemSuaSanPham as any).show(SanPhamID, isUpdate)
    },
    showModalXoa (id: any) {
      this.selected = id;
      (this.$refs.modalXoa as any).show()
    },
    xoa () {
      if (this.selected == null) {
        return
      }
      SanPhamApi.delete(this.selected)
        .then(res => {
          (this.$refs.modalXoa as any).hide()
          this.getDataFromApi(this.searchParamsSanPham)
          this.$notify({
            text: 'Xóa sản phẩm thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa sản phẩm thất bại',
            color: 'error'
          })
        })
    },
    save (item: any, isUpdate: boolean) {
      if (isUpdate) {
        SanPhamApi.update(item.SanPhamId, item)
          .then(res => {
            this.getDataFromApi(this.searchParamsSanPham)
            this.$notify({
              text: 'Cập nhật sản phẩm thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaSanPham as any).hide();
            (this.$refs.modalThemSuaSanPham as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Cập nhật sản phẩm thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaSanPham as any).saving = false
          })
      } else {
        SanPhamApi.insert(item)
          .then(res => {
            this.getDataFromApi(this.searchParamsSanPham)
            this.$notify({
              text: 'Thêm mới sản phẩm thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaSanPham as any).hide();
            (this.$refs.modalThemSuaSanPham as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Thêm mới sản phẩm thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaSanPham as any).saving = false
          })
      }
    }
  }
})
</script>
