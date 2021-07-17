<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsDanhMuc.q"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @input="getDataFromApi(searchParamsDanhMuc)"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalThemSuaDanhMuc(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex>
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="dsDanhMuc"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsDanhMuc"
                                :total-items="searchParamsDanhMuc.totalItems"
                                :loading="loadingTable"
                                class="table-border table">
                          <template slot="items" slot-scope="props" style="sox-height:100px;">
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.TenDanhMuc }}
                            </td>
                             <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.TenDanhMucCha}}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.GhiChu }}
                            </td>
                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                            <v-tooltip bottom>
                              <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalThemSuaDanhMuc(props.item.DanhMucId, true)">
                                <v-icon color="teal" small>edit</v-icon>
                              </v-btn>
                              <span>Cập nhật danh mục</span>
                            </v-tooltip>
                            <v-tooltip bottom>
                              <v-btn icon slot="activator" small @click="showModalXoa(props.item.DanhMucId)" class="mx-0 my-0">
                                <v-icon color="pink" small>delete</v-icon>
                              </v-btn>
                              <span>Xoá danh mục</span>
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
        <component :is="'ModalThemSuaDanhMuc'" ref="modalThemSuaDanhMuc" @save="save"/>
    </v-flex>
</template>
<script lang="ts">
import DanhMucApi, { DanhMucApiSearchParams } from '@/apiResources/DanhMucApi'
import { DanhMuc } from '@/models/DanhMuc'
import ModalXoa from '../Commons/ModalXoa.vue'
import ModalThemSuaDanhMuc from './ModalThemSuaDanhMuc.vue'

import Vue from 'vue'
export default Vue.extend({
  name: 'DanhSachDanhMuc',
  components: {
    ModalThemSuaDanhMuc,
    ModalXoa
  },
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      dsDanhMuc: [] as DanhMuc[],
      tableHeader: [
        { text: 'Tên danh mục', align: 'center', value: 'TenDanhMuc', sortable: false },
        { text: 'danh mục cha', align: 'center', value: 'DanhMucPID', sortable: false },
        { text: 'Ghi chú', align: 'center', value: 'GhiChu', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      searchParamsDanhMuc: { includeEntities: true, rowsPerPage: 10 } as DanhMucApiSearchParams,
      loadingTable: false,
      selected: 0,
      dialogConfirmDelete: false
    }
  },
  watch: {
  },
  methods: {
    getDataFromApi (searchParamsDanhMuc: DanhMucApiSearchParams): void {
      this.loadingTable = true
      DanhMucApi.getDanhSach(searchParamsDanhMuc).then(res => {
        this.dsDanhMuc = res.Data
        this.searchParamsDanhMuc.totalItems = res.Pagination.totalItems
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    showModalThemSuaDanhMuc (DanhMucID: any, isUpdate: boolean) {
      (this.$refs.modalThemSuaDanhMuc as any).show(DanhMucID, isUpdate)
    },
    showModalXoa (id: any) {
      this.selected = id;
      (this.$refs.modalXoa as any).show()
    },
    xoa () {
      if (this.selected == null) {
        return
      }
      DanhMucApi.delete(this.selected)
        .then(res => {
          (this.$refs.modalXoa as any).hide()
          this.getDataFromApi(this.searchParamsDanhMuc)
          this.$notify({
            text: 'Xóa danh mục thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa danh mục thất bại',
            color: 'error'
          })
        })
    },
    save (item: any, isUpdate: boolean) {
      if (isUpdate) {
        DanhMucApi.update(item.DanhMucId, item)
          .then(res => {
            this.getDataFromApi(this.searchParamsDanhMuc)
            this.$notify({
              text: 'Cập nhật danh mục thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaDanhMuc as any).hide();
            (this.$refs.modalThemSuaDanhMuc as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Cập nhật danh mục thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaDanhMuc as any).saving = false
          })
      } else {
        DanhMucApi.insert(item)
          .then(res => {
            this.getDataFromApi(this.searchParamsDanhMuc)
            this.$notify({
              text: 'Thêm mới danh mục thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaDanhMuc as any).hide();
            (this.$refs.modalThemSuaDanhMuc as any).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Thêm mới danh mục thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaDanhMuc as any).saving = false
          })
      }
    }
  }
})
</script>
