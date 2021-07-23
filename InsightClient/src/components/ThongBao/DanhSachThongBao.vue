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
        <v-flex xs12 sm8 md8 class="text-xs-right">
        </v-flex>
        <v-flex xs12>
          <v-data-table class="table-border" 
          :headers="headersThongBao" 
          :items="DataThongBao" 
          :rows-per-page-items="[15]" :pagination.sync="pagination" hide-actions>
            <template slot="items" slot-scope="props" style="max-height:100px;">
              <td @click="showChiTietThongBao(props.item, 1)" style="max-width:100px;width:20%; text-overflow: ellipsis; overflow: hidden;">
                {{ props.item.TieuDe }}
              </td>
              <td @click="showChiTietThongBao(props.item, 1)">
                {{ props.item.ToaNha ? props.item.ToaNha.TenToaNha: '' }}
              </td>
              <td  @click="showChiTietThongBao(props.item, 1)">
                {{ props.item.ThoiGian | moment('DD/MM/YYYY, HH:mm') }}
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
    <modal-chi-tiet-thong-bao ref="modalChiTietThongBao"/>
  </v-card>
</template>
<script>
  import ModalChiTietThongBao from '../ThongBao/ModalChiThongBao'
  import {
    oHTTP
  } from '@/ohttp-services'
  import {
    HTTP
  } from '@/http-services'
  export default {
    name: 'DSThongBao',
    components: {
      ModalChiTietThongBao
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
            text: 'Thời Gian',
            align: 'left',
            value: 'ThoiGian'
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
      showChiTietThongBao (thongBao, page) {
        this.$refs.modalChiTietThongBao.show(thongBao, page)
      },
      showXoa(item) {
        this.dialogXoaItem = true
        this.seclectedItem = item
      },
      filters () {
        HTTP.get('odata/ThongBaoChung?$orderby=ThoiGian%20desc&$filter=(substringof(%27' + this.search + '%27,TieuDe))&$top=1&$inlinecount=allpages&$expand=ToaNha').then(res => {
          this.count = res.data ? res.data['odata.count'] : null
          oHTTP('ThongBaoChung').search(['TieuDe'], this.search).top(15).skip((this.page - 1) * 15).expand('ToaNha').get(res => {
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
