<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsUser.searchTerm"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @input="getDataFromApi(searchParamsUser)"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalInsertOrUpdateUser(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex>
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="users"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsUser"
                                :total-items="searchParamsUser.totalItems"
                                :loading="loadingTable"
                                class="table-border table">
                          <template slot="items" slot-scope="props" style="sox-height:100px;">
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.FirstName + " " + props.item.LastName}}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.Email }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.PhoneNumber }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.LastLoginDateUtc | moment('DD/MM/YYYY, HH:mm') }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                               <span xs12 v-if="props.item.Status === 1" style="color: #00a300">
                                 Kích hoạt
                               </span>
                               <span xs12 v-if="props.item.Status !== 1" style="color: #ff1f1f">
                                 Chưa kích hoạt
                               </span>
                            </td>
                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                              <v-tooltip bottom>
                                <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalInsertOrUpdateUser(props.item.Id, true)">
                                  <v-icon color="teal" small>edit</v-icon>
                                </v-btn>
                                <span>Cập nhật người dùng</span>
                              </v-tooltip>
                              <v-tooltip bottom>
                                <v-btn icon slot="activator" small @click="showModalXoa(props.item.Id)" class="mx-0 my-0">
                                  <v-icon color="pink" small>delete</v-icon>
                                </v-btn>
                                <span>Xoá người dùng</span>
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
        <component :is="'ModalInsertOrUpdateUser'" ref="modalInsertOrUpdateUser" @save="save"/>
    </v-flex>
</template>
<script>
import UserApi from '../../apiResources/UserApi'
import ModalXoa from '../Commons/ModalXoa.vue'
import ModalInsertOrUpdateUser from './ModalInsertOrUpdateUser.vue'
import Vue from 'vue'

  export default {
    $_veeValidate: {
      validator: 'new'
    },
    name: 'User',
    components:{
      ModalInsertOrUpdateUser,
      ModalXoa
    },
    data () {
      return {
        users: [],
        tableHeader: [
          { text: 'Tên người dùng', align: 'center', value: 'UserName', sortable: false },
          { text: 'Email', align: 'center', value: 'Email', sortable: false },
          { text: 'Số diện thoại', align: 'center', value: 'PhoneNumber', sortable: false },
          { text: 'Lần đăng nhập cuối cùng', align: 'center', value: 'LastLoginDateUtc', sortable: false },
          { text: 'Trạng thái', align: 'center', value: 'Status', sortable: false },
          { text: 'Thao tác', align: 'center', value: '', sortable: false }
        ],
        searchParamsUser: { includeEntities: true, rowsPerPage: 10 },
        loadingTable: false,
        selected: 0,
        dialogConfirmDelete: false
      }
    },
    watch: {
    },
    methods: {
      getDataFromApi (searchParamsUser){
        this.loadingTable = true
        UserApi.getUsers(searchParamsUser).then(res => {
          this.users = res.Data
          this.searchParamsUser.totalItems = res.Pagination.totalItems
          this.loadingTable = false
        }).catch(res => {
          this.loadingTable = false
          this.$notify({
            text: 'Lấy dữ liệu thất bại',
            color: 'error'
          })
        })
      },
      showModalInsertOrUpdateUser (id, isUpdate) {
        (this.$refs.modalInsertOrUpdateUser).show(id, isUpdate)
      },
      showModalXoa (id) {
        this.selected = id;
        this.$refs.modalXoa.show()
      },
      xoa () {
        if (this.selected == null) {
          return
        }
        UserApi.delete(this.selected)
          .then(res => {
            (this.$refs.modalXoa).hide()
            this.getDataFromApi(this.searchParamsUser)
            this.$notify({
              text: 'Xóa loại công việc thành công',
              color: 'success'
            })
          })
          .catch(res => {
            this.$notify({
              text: 'Xóa loại công việc thất bại',
              color: 'error'
            })
          })
      },
      save (item, isUpdate) {
        if (isUpdate) {
          UserApi.update(item.Id, item)
            .then(res => {
              this.getDataFromApi(this.searchParamsUser)
              this.$notify({
                text: 'Cập nhật loại công việc thành công',
                color: 'success'
              });
              (this.$refs.modalInsertOrUpdateUser).hide();
              (this.$refs.modalInsertOrUpdateUser).saving = false
            })
            .catch(res => {
              this.$notify({
                text: 'Cập nhật loại công việc thất bại',
                color: 'error'
              });
              (this.$refs.modalInsertOrUpdateUser).saving = false
            })
        } else {
          UserApi.insert(item)
            .then(res => {
              this.getDataFromApi(this.searchParamsUser)
              this.$notify({
                text: 'Thêm mới loại công việc thành công',
                color: 'success'
              });
              (this.$refs.modalInsertOrUpdateUser).hide();
              (this.$refs.modalInsertOrUpdateUser).saving = false
            })
            .catch(res => {
              this.$notify({
                text: 'Thêm mới loại công việc thất bại',
                color: 'error'
              });
              (this.$refs.modalInsertOrUpdateUser).saving = false
            })
        }
      }
    }
  }
</script>
