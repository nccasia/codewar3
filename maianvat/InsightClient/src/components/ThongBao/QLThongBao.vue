<template>
  <v-card>
    <v-card-title>
      <v-container grid-list-md>
      <v-layout wrap>
        <v-flex xs12 sm4 md4>
          <v-text-field
          v-model="search"
          append-icon="search"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
        </v-flex>
        <v-flex xs12 sm4 md4>
          <v-checkbox style="margin-top:18px" label="Thông báo quan trọng" v-model="ThongBaoQuanTrong" hide-details></v-checkbox>
        </v-flex>
        <v-flex xs12 sm4 md4 class="text-xs-right">
          <v-btn fab dark small color="primary" @click="showModalThongBao({}, false)">
            <v-icon>add</v-icon>
          </v-btn>
        </v-flex>
        <v-flex xs12>
          <v-data-table class="table-border" 
          :headers="headersThongBao" 
          :items="DataThongBao" 
          :rows-per-page-items="[15]" :pagination.sync="pagination" hide-actions>
            <template slot="items" slot-scope="props" style="max-height:100px;">
              <td @click="showModalThongBao(props.item, true)" style="max-width:100px;width:20%; text-overflow: ellipsis; overflow: hidden;">
                {{ props.item.TieuDe }}
              </td>
              <td @click="showModalThongBao(props.item, true)">
                {{ props.item.ToaNha ? props.item.ToaNha.TenToaNha: '' }}
              </td>
              <td @click="showModalThongBao(props.item, true)" >
                <div style="max-height:60px; max-width:320px; overflow: hidden;" v-html="props.item.NoiDung"></div>
              </td>
              <td>
                {{ props.item.ThoiGian | moment('DD/MM/YYYY, HH:mm') }}
              </td>
              <td class="justify-center" style="width:90px;">
                <v-btn icon small class="mx-0 my-0" @click="showModalThongBao(props.item, true)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon small class="mx-0 my-0" @click="showXoa(props.item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
              </td>
            </template>
            <v-alert slot="no-results" :value="true" color="error" icon="warning">
              Không có dữ liệu.
            </v-alert>
          </v-data-table>
          <div class="text-xs-center pt-2" style="margin:20px">
            <v-pagination v-model="page" circle   :total-visible="7" :length="pages"></v-pagination>
          </div>
        </v-flex>
      </v-layout>
      </v-container>
    </v-card-title>
    <v-dialog v-model="dialogXoaItem" persistent max-width="290">
      <v-card>
        <v-card-title class="headline">Xác nhận xóa</v-card-title>
        <v-card-text>
          Bạn có chắc chắn muốn xóa bản ghi này không?
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="pink" flat @click.native="xoaThongBao">Xóa</v-btn>
          <v-btn color="green darken-1" flat @click.native="dialogXoaItem = false">Hủy</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <modal-cap-nhat-thong-bao ref="modalCapNhatThongBao" @taoThongBao="themThongBao" @capnhatThongBao="suaThongBao"></modal-cap-nhat-thong-bao>
  </v-card>
</template>
<script>
  import ModalCapNhatThongBao from './ModalCapNhatThongBao.vue'
  import {
    oHTTP
  } from '@/ohttp-services'
  import {
    HTTP
  } from '@/http-services'
  export default {
    name: 'QLThongBao',
    components: {
      ModalCapNhatThongBao
    },
    data() {
      return {
        dialogXoaItem: false,
        search: '',
        page: 1,
        count: 0,
        pagination: {},
        DataThongBao: [],
        headersThongBao: [
          {
            text: 'Tiêu đề',
            align: 'left',
            value: 'TieuDe'
          },
          {
            text: 'Tòa nhà',
            align: 'left',
            value: 'ToaNha.TenToaNha'
          },
          {
            text: 'Nội dung',
            align: 'left',
            value: 'NoiDung'
          },
          {
            text: 'Thời Gian',
            align: 'left',
            value: 'ThoiGian'
          },
          {
            text: 'Thao tác',
            align: 'left',
            value: 'tongTien'
          }
        ],
        dataFiltersLoiPhe: {
          page: 1,
          rowsPerPage: 50,
          sortBy: '',
          descending: true,
          totalItems: 0,
          search: ''
        },
        totalItems: 0,
        ThongBaoQuanTrong: false
      }
    },
    computed: {
      pages () {
        if (this.pagination.rowsPerPage == null ||
          this.pagination.totalItems == null
        ) return 0
        return Math.ceil(this.count / 15)
      }
    },
    watch: {
      page: function () {
        this.search === '' ? this.getDanhSachThongBao() : this.filters()
        // this.getDSYeuCau()
      },
      search: function () {
        this.search === '' ? this.getDanhSachThongBao() : this.filters()
      },
      ThongBaoQuanTrong: function(val) {
        if (val === true) {
          oHTTP('ThongBaoChung').filter('LaThongBaoQuanTrong eq ' + val).get(res => {
            this.DataThongBao = res
          })
        } else {
          this.getDanhSachThongBao()
        }
      }
    },
    created () {
      this.getDanhSachThongBao()
    },
    methods: {
      themThongBao (ThongBaoChung) {
        ThongBaoChung.ThongBaoChungId = 1
        oHTTP('ThongBaoChung').post(ThongBaoChung).save(res => {
          this.getDanhSachThongBao()
          this.$refs.modalCapNhatThongBao.hide()
          this.$notify({
            text: 'Thêm thông báo thành công',
            color: 'success'
          })
        }, status => {
          this.$notify({
            text: 'Thêm thông báo thất bại',
            color: 'error'
          })
        })
      },
      suaThongBao (ThongBaoChung) {
        oHTTP('ThongBaoChung(' + ThongBaoChung.ThongBaoChungId + ')').put(ThongBaoChung).save(res => {
          this.getDanhSachThongBao()
          this.$refs.modalCapNhatThongBao.hide()
          this.$notify({
            text: 'Cập nhật thông báo thành công',
            color: 'success'
          }, status => {
            this.$notify({
              text: 'Cập nhật thông báo thất bại',
              color: 'error'
            })
          })
        })
      },
      xoaThongBao () {
        oHTTP('ThongBaoChung(' + this.seclectedItem.ThongBaoChungId + ')').remove().save(res => {
          this.getDanhSachThongBao()
          this.dialogXoaItem = false
          this.$notify({
            text: 'Xóa thông báo thành công',
            color: 'success'
          })
        }, status => {
          this.$notify({
            text: 'Xóa thông báo thất bại',
            color: 'error'
          })
        })
      },
      showModalThongBao(loiPhe, isUpdate) {
        this.$refs.modalCapNhatThongBao.show(loiPhe, isUpdate)
      },
      showXoa(item) {
        this.dialogXoaItem = true
        this.seclectedItem = item
      },
      filters () {
        HTTP.get('odata/ThongBaoChung?$orderby=ThoiGian%20desc&$filter=(substringof(%27' + this.search + '%27,TieuDe))or(substringof(%27' + this.search + '%27,ToaNha/TenToaNha))or(substringof(%27' + this.search + '%27,NoiDung))&$top=1&$inlinecount=allpages&$expand=ToaNha').then(res => {
          this.count = res.data ? res.data['odata.count'] : null
          oHTTP('ThongBaoChung').search(['TieuDe', 'ToaNha/TenToaNha', 'NoiDung'], this.search).top(15).skip((this.page - 1) * 15).inlineCount('allpages').expand('ToaNha').get(res => {
            this.DataThongBao = res
          }, status => {
            this.$notify({
              text: 'Lây dữ liệu thất bại',
              color: 'error'
            })
          })
        })
      },
      getDanhSachThongBao () {
        HTTP.get('odata/ThongBaoChung?$top=1&$inlinecount=allpages').then(res => {
          this.count = res.data ? res.data['odata.count'] : null
          oHTTP('ThongBaoChung').top(15).skip((this.page - 1) * 15).inlineCount('allpages').expand('ToaNha').get(res => {
            this.DataThongBao = res
          })
        }, status => {
          this.$notify({
            text: 'Lấy dữ liệu thất bại',
            color: 'error'
          })
        })
      }
    }
  }
</script>

<style>
  .my-header .ag-header-cell-label {
    text-align: center;
    display: block;
  }

  .md-card .md-card-toolbar {
    height: 36px;
    text-align: center;
    font-weight: bold;
    padding-top: 8px;
  }

  .my-header span {
    float: none !important;
    font-weight: bold;
  }
</style>
