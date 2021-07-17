<template>
    <v-dialog v-model="dialog" scrollable width="1000px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật sản phẩm' : 'Thêm mới sản phẩm'}}</span>
          <v-spacer></v-spacer>
          <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
        </v-card-title>
        <v-card-text>
          <v-container v-if="loadingModal">
            <div class="text-xs-center">
              <v-progress-circular
                indeterminate
                color="primary"
                :size="70"
                :width=6
              ></v-progress-circular>
            </div>
          </v-container>
         <form scope="formEdit" v-else>
            <v-container grid-list-lg pa-0>
               <v-layout wrap>
                <v-flex xs12 sm6>
                  <v-text-field
                  label="Tên sản phẩm"
                  v-model="data.TenSanPham"
                  data-vv-name="Tên sản phẩm"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Tên sản phẩm')"
                  required
                  ></v-text-field>
                </v-flex>
                 <v-flex xs12 sm6>
                  <v-autocomplete
                  :items="dsDanhMuc"
                  :loading="dsDanhMucLoading"
                  item-text="TenDanhMuc"
                  item-value="DanhMucId"
                  multiple
                  label="Danh mục"
                  v-model="dsDanhMucSanPham"
                  data-vv-name="Danh mục"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Danh mục')"
                  required
                  clearable
                  ></v-autocomplete>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
                  <v-textarea
                    v-model="data.ThongTinSanPham"
                    label="Thông tin sản phẩm"
                    data-vv-name="Thông tin sản phẩm"
                    data-vv-scope="formEdit"
                    v-validate="{required: true}"
                    auto-grow
                    rows="4"
                    :error-messages="errors.collect('Thông tin sản phẩ')"
                  ></v-textarea>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
                  <v-textarea
                    v-model="data.GhiChu"
                    label="Ghi chú"
                    auto-grow
                    rows="4"
                  >
                  </v-textarea>
                </v-flex>
              </v-layout>
              <v-layout my-3 v-if="isUpdate">
                <v-divider></v-divider>
              </v-layout>
              <v-layout wrap  v-if="isUpdate">
                <v-flex xs12 mb-2>
                  <v-layout>
                    <h3 style="margin-top:10px">Thông số sản phẩm:</h3>
                  </v-layout>
                </v-flex>
                <v-flex xs12>
                  <v-layout wrap>
                    <v-flex xs5>
                      <v-autocomplete
                        :items="dsThongSo"
                        v-model="thongSoSanPham.LoaiThongSoId"
                        label="Thông số"
                        item-text="TenLoaiThongSo"
                        item-value="LoaiThongSoId"
                        data-vv-name="Thông số"
                        data-vv-scope="formThongSo"
                        v-validate="{required: true}"
                        :error-messages="errors.collect('Thông số')"
                      ></v-autocomplete>
                    </v-flex>
                    <v-flex xs5>
                      <v-text-field
                        v-model="thongSoSanPham.GiaTri"
                        label="Giá trị"
                        data-vv-name="Giá trị"
                        data-vv-scope="formThongSo"
                        v-validate="{required: true}"
                        :error-messages="errors.collect('Giá trị')"
                      ></v-text-field>
                    </v-flex>
                    <v-flex xs2 mt-3 class="text-xs-right">
                      <v-btn mt-0 color="primary" :disabled="themThongSoSanPhamLoading" :loading="themThongSoSanPhamLoading" @click="themThongSoSanPham()" small>Thêm thông số</v-btn>
                    </v-flex>
                  </v-layout>
                </v-flex>
                <v-flex xs12>
                   <v-data-table :headers="headerTable"
                                :items="dsThongSoSanPham"
                                hide-actions
                                :loading="dsThongSoSanPhamLoading"
                                class="table-border table">
                          <template slot="items" slot-scope="props" style="sox-height:100px;">
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.TenLoaiThongSo }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;">
                              <v-edit-dialog
                                :return-value.sync="props.item.GiaTri"
                                large
                                lazy
                                persistent
                                @save="capNhatThongSoSanPham(props.item)"
                              >
                                <div class="text-xs-center">{{ props.item.GiaTri }}</div>
                                <template v-slot:input>
                                  <div class="mt-3 title">Cập nhật giá trị</div>
                                </template>
                                <template v-slot:input>
                                  <v-text-field
                                    v-model="props.item.GiaTri"
                                    label="Giá trị"
                                    autofocus
                                  ></v-text-field>
                                </template>
                              </v-edit-dialog>
                            </td>
                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                            <v-tooltip bottom>
                              <v-btn icon slot="activator" small @click="showModalXoa(props.item.ThongSoSanPhamId)" class="mx-0 my-0">
                                <v-icon color="pink" small>close</v-icon>
                              </v-btn>
                              <span>Xoá thông số sản phẩm</span>
                            </v-tooltip>
                            </td>
                          </template>
                          <template slot="no-data">
                            <div class="text-xs-center">Không có dữ liệu</div>
                          </template>
                        </v-data-table>
                </v-flex>
              </v-layout>
              <v-layout my-3 v-if="isUpdate">
                <v-divider></v-divider>
              </v-layout>
              <v-layout wrap v-if="isUpdate">
                <v-flex xs12 mb-4>
                  <v-layout>
                    <h3 style="margin-top:10px">Ảnh sản phẩm:</h3>
                    <v-spacer></v-spacer>
                    <v-btn mt-0 color="primary" :disabled="themAnhSanPhamLoading" :loading="themAnhSanPhamLoading" @click="$refs.inpFile.click()" small>Thêm ảnh</v-btn>
                    <input type="file" accept="image/*" style="display:none;"  ref="inpFile" id="File" @change="themAnhSanPham()" capture="camera">
                  </v-layout>
                </v-flex>
                <v-flex xs12 v-if="dsAnhSanPham.length == 0">
                  <p class="text-xs-center">Không có ảnh sản phẩm</p>
                </v-flex>
                <v-flex xs12 v-else>
                  <v-layout wrap>
                    <v-flex xs2 v-for="anhSanPham in dsAnhSanPham" :key="anhSanPham.AnhSanPhamId">
                      <div style="position: relative">
                        <img :src="API.BASE_API + 'file/anhsanpham/' + anhSanPham.Anh" style="width:100%; height: 200px; object-fit: cover"/>
                        <v-btn style="position: absolute;top: -20px;right: -20px;" color="error" @click="showModalXoa(anhSanPham.AnhSanPhamId)" small icon><v-icon small>close</v-icon></v-btn>
                      </div>
                    </v-flex>
                  </v-layout>
                </v-flex>
              </v-layout>
            </v-container>
          </form>
        </v-card-text>
        <v-card-actions v-if="!loadingModal">
          <v-spacer></v-spacer>
          <v-btn flat @click.native="hide">Hủy</v-btn>
          <v-btn color="blue darken-1" :loading="saving" :disabled="saving" flat @click.native="save">Lưu</v-btn>
        </v-card-actions>
      </v-card>
       <component :is="'ModalXoa'" ref="modalXoa" @xoa="xoa"/>
    </v-dialog>
</template>

<script lang="ts">
import DanhMucApi, { DanhMucApiSearchParams } from '@/apiResources/DanhMucApi'
import { DanhMuc } from '@/models/DanhMuc'
import SanPhamApi from '@/apiResources/SanPhamApi'
import ModalXoa from '../Commons/ModalXoa.vue'
import { SanPham } from '@/models/SanPham'
import { DanhMucSanPham } from '@/models/DanhMucSanPham'
import DanhMucSanPhamApi from '@/apiResources/DanhMucSanPhamApi'
import { AnhSanPham } from '@/models/AnhSanPham'
import { ThongSoSanPham } from '@/models/ThongSoSanPham'
import { HTTP } from '@/HTTPServices'
import API from '@/apisServer'

import Vue from 'vue'
export default Vue.extend({
  name: 'ModalThemSuaSanPham',
  components: {
    ModalXoa
  },
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      dsThongSo: [],
      dsThongSoLoading: false,
      dsThongSoSanPham: [] as ThongSoSanPham[],
      dsThongSoSanPhamLoading: false,
      headerTable: [
        { text: 'Tên thông số', align: 'center', value: 'TenSanPham', sortable: false },
        { text: 'Giá trị', align: 'left', value: 'SanPhamPID', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      thongSoSanPham: {} as ThongSoSanPham,
      API: API,
      saving: false,
      dialog: false,
      data: {} as SanPham,
      isUpdate: false,
      loadingModal: false,
      dsDanhMuc: [] as DanhMuc[],
      dsDanhMucLoading: false,
      searchParamsDanhMuc: { includeEntities: true, rowsPerPage: -1 } as DanhMucApiSearchParams,
      dsDanhMucSanPham: [] as number[],
      dsAnhSanPham: [] as AnhSanPham[],
      dsAnhSanPhamLoading: false,
      themAnhSanPhamLoading: false,
      sanPhamId: 0,
      selected: 0,
      themThongSoSanPhamLoading: false
    }
  },
  computed: {
  },
  methods: {
    hide () {
      this.dialog = false
    },
    capNhatThongSoSanPham (item: ThongSoSanPham) {
      HTTP.put('thongsosanpham/' + item.ThongSoSanPhamId, item).then(res => {
        this.getThongSoSanPham(this.sanPhamId)
        this.$notify({
          text: 'Cập nhật thành công',
          color: 'success'
        })
      }).catch(res => {
        this.$notify({
          text: 'Cập nhật thất bại',
          color: 'success'
        })
      })
    },
    themThongSoSanPham () {
      this.$validator.validateAll('formThongSo').then((res) => {
        if (res) {
          this.themThongSoSanPhamLoading = true
          this.thongSoSanPham.SanPhamId = this.sanPhamId
          HTTP.post('thongsosanpham', this.thongSoSanPham)
            .then(res => {
              this.getThongSoSanPham(this.sanPhamId)
              this.themThongSoSanPhamLoading = false
              this.thongSoSanPham = {} as ThongSoSanPham
              this.$notify({
                text: 'Thêm mới thành công',
                color: 'success'
              })
            })
            .catch(res => {
              this.themThongSoSanPhamLoading = false
              this.$notify({
                text: 'Thêm mới thất bại',
                color: 'error'
              })
            })
        } else {
          this.$notify({
            text: 'Vui lòng điền đủ dữ liệu',
            color: 'error'
          })
        }
      })
    },
    getThongSoSanPham (SanPhamID: number) {
      this.dsThongSoSanPhamLoading = true
      SanPhamApi.getDanhSachThongSoSanPham(SanPhamID)
        .then(res => {
          this.dsThongSoSanPhamLoading = false
          this.dsThongSoSanPham = res
        })
        .catch(res => {
          this.dsThongSoSanPhamLoading = false
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    getDanhMucSanPham (SanPhamID: number) {
      DanhMucSanPhamApi.getDanhSachTheoSanPham(SanPhamID)
        .then(res => {
          this.dsDanhMucSanPham = res
        })
        .catch(res => {
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    getLoaiThongSo () {
      this.dsThongSoLoading = true
      HTTP.get('loaithongso')
        .then(res => {
          this.dsThongSo = res.data
          this.dsThongSoLoading = false
        })
        .catch(res => {
          this.dsThongSoLoading = false
          this.$notify({
            text: 'Lấy dữ liệu thất bại thất bại',
            color: 'error'
          })
        })
    },
    getDanhSachAnhSanPham (SanPhamID: number) {
      this.dsAnhSanPhamLoading = true
      SanPhamApi.getAnhSanPham(SanPhamID).then(res => {
        this.dsAnhSanPham = res
        this.dsAnhSanPhamLoading = false
      })
        .catch(res => {
          this.dsAnhSanPhamLoading = false
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    showModalXoa (id: any, flag: string) {
      this.selected = id;
      (this.$refs.modalXoa as any).show(flag)
    },
    xoa (flag: string) {
      if (flag === 'AnhSanPham') {
        this.xoaAnhSanPham()
      } else {
        this.xoaThongSoSanPham()
      }
    },
    xoaThongSoSanPham () {
      HTTP.delete('thongsosanpham/' + this.selected)
        .then(res => {
          (this.$refs.modalXoa as any).hide()
          this.getThongSoSanPham(this.sanPhamId)
          this.$notify({
            text: 'Xóa thông số sản phẩm thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa thông số sản phẩm thành công thất bại',
            color: 'error'
          })
        })
    },
    xoaAnhSanPham () {
      HTTP.delete('anhsanpham/' + this.selected)
        .then(res => {
          (this.$refs.modalXoa as any).hide()
          this.getDanhSachAnhSanPham(this.sanPhamId)
          this.$notify({
            text: 'Xóa ảnh thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa ảnh thất bại',
            color: 'error'
          })
        })
    },
    themAnhSanPham () {
      if ((document.querySelector('#File') as any).files.length > 0) {
        let files = (document.querySelector('#File') as any).files[0]
        var re = /^\S+\.(?:jpg|png|gif|jpeg)$/g
        if (re.test(files.name)) {
          this.themAnhSanPhamLoading = true
          var Image = new FormData()
          Image.append('image', files)
          HTTP.post('anhsanpham/' + this.sanPhamId, Image, { headers: { 'Content-Type': 'multipart/form-data' } })
            .then(res => {
              (document.querySelector('#File') as any).files = null
              this.themAnhSanPhamLoading = false
              this.getDanhSachAnhSanPham(this.sanPhamId)
              this.$notify({
                text: 'Thêm ảnh thành công',
                color: 'success'
              })
            })
            .catch(res => {
              this.themAnhSanPhamLoading = false;
              (document.querySelector('#File') as any).files = null
              this.$notify({
                text: 'Thêm ảnh thất bại',
                color: 'error'
              })
            })
        } else {
          this.$notify({
            text: 'Ảnh không đúng định dạng',
            color: 'error'
          })
        }
      } else {
        this.$notify({
          text: 'Vui lòng chọn ảnh',
          color: 'error'
        })
      }
    },
    getData (SanPhamID: number) {
      this.loadingModal = true
      SanPhamApi.detail(SanPhamID)
        .then(res => {
          this.data = res
          this.loadingModal = false
        })
        .catch(res => {
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    getDanhMuc () {
      this.dsDanhMucLoading = true
      DanhMucApi.get()
        .then(res => {
          this.dsDanhMuc = res as any
          this.dsDanhMucLoading = false
        }).catch(res => {
          this.dsDanhMucLoading = false
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    save () {
      this.saving = true
      this.$validator.validateAll('formEdit').then((res) => {
        if (res) {
          if (this.isUpdate) {
            if (this.dsDanhMucSanPham != null && this.dsDanhMucSanPham.length > 0) {
              this.data.DanhMucSanPham = []
              this.dsDanhMucSanPham.forEach(element => {
                var danhMucSanPham = {} as DanhMucSanPham
                danhMucSanPham.DanhMucId = element
                danhMucSanPham.SanPhamId = this.data.SanPhamId
                this.data.DanhMucSanPham.push(danhMucSanPham)
              })
            }
          } else {
            if (this.dsDanhMucSanPham != null && this.dsDanhMucSanPham.length > 0) {
              this.data.DanhMucSanPham = []
              this.dsDanhMucSanPham.forEach(element => {
                var danhMucSanPham = {} as DanhMucSanPham
                danhMucSanPham.DanhMucId = element
                this.data.DanhMucSanPham.push(danhMucSanPham)
              })
            }
          }
          this.$emit('save', this.data, this.isUpdate)
        } else {
          this.saving = false
          this.$notify({
            text: 'Cần điền đủ thông tin',
            color: 'error'
          })
        }
      })
    },
    show (SanPhamID: any, isUpdate: boolean) {
      Object.assign(this.$data, (this.$options as any).data.apply(this))
      if (isUpdate) {
        this.sanPhamId = SanPhamID
        this.getData(SanPhamID)
        this.getDanhMucSanPham(SanPhamID)
        this.getDanhSachAnhSanPham(SanPhamID)
        this.getThongSoSanPham(SanPhamID)
      } else {
      }
      this.isUpdate = isUpdate
      this.dialog = true
      this.getDanhMuc()
      this.getLoaiThongSo()
    }
  }
})
</script>
