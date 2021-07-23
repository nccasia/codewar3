<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsJobType.searchTerm"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @input="getDataFromApi(searchParamsJobType)"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalInsertOrUpdateJobType(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex>
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="jobTypes"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsJobType"
                                :total-items="searchParamsJobType.totalItems"
                                :loading="loadingTable"
                                class="table-border table">
                          <template slot="items" slot-scope="props" style="sox-height:100px;">
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.Name }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.Description }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.DefaultTimeInHours }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.ColorCode }}
                            </td>
                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                              <v-tooltip bottom>
                                <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalInsertOrUpdateJobType(props.item.JobTypeK, true)">
                                  <v-icon color="teal" small>edit</v-icon>
                                </v-btn>
                                <span>Cập nhật loại công việc</span>
                              </v-tooltip>
                              <v-tooltip bottom>
                                <v-btn icon slot="activator" small @click="showModalXoa(props.item.JobTypeK)" class="mx-0 my-0">
                                  <v-icon color="pink" small>delete</v-icon>
                                </v-btn>
                                <span>Xoá loại công việc</span>
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
        <component :is="'ModalInsertOrUpdateJobType'" ref="modalInsertOrUpdateJobType" @save="save"/>
    </v-flex>
</template>
<script>
import ListCategoryApi from '../../apiResources/ListCategoryApi'
import JobTypeApi from '../../apiResources/JobTypeApi'
import ModalXoa from '../Commons/ModalXoa.vue'
import ModalInsertOrUpdateJobType from './ModalInsertOrUpdateJobType.vue'
import Vue from 'vue'

  export default {
    $_veeValidate: {
      validator: 'new'
    },
    name: 'JobType',
    components:{
      ModalInsertOrUpdateJobType,
      ModalXoa
    },
    data () {
      return {
        jobTypeNames: [],
        jobTypes: [],
        tableHeader: [
          { text: 'Loại công việc', align: 'center', value: 'Name', sortable: false },
          { text: 'Mô tả', align: 'center', value: 'Description', sortable: false },
          { text: 'Thời gian', align: 'center', value: 'DefaultTimeInHours', sortable: false },
          { text: 'Màu sắc', align: 'center', value: 'ColorCode', sortable: false },
          { text: 'Thao tác', align: 'center', value: '', sortable: false }
        ],
        searchParamsJobType: { includeEntities: true, rowsPerPage: 10 },
        loadingTable: false,
        
        selected: 0,
        dialogConfirmDelete: false
      }
    },
    watch: {
    },
    methods: {
      getDataFromApi (searchParamsJobType){
        this.loadingTable = true
        JobTypeApi.getJobTypes(searchParamsJobType).then(res => {
          this.jobTypes = res.Data
          this.searchParamsJobType.totalItems = res.Pagination.totalItems
          this.loadingTable = false
        }).catch(res => {
          this.loadingTable = false
          this.$notify({
            text: 'Lấy dữ liệu thất bại',
            color: 'error'
          })
        })
      },

      showModalInsertOrUpdateJobType (JobTypeK, isUpdate) {
        (this.$refs.modalInsertOrUpdateJobType).show(JobTypeK, isUpdate)
      },
      showModalXoa (id) {
        this.selected = id;
        this.$refs.modalXoa.show()
      },
      xoa () {
        if (this.selected == null) {
          return
        }
        JobTypeApi.delete(this.selected)
          .then(res => {
            (this.$refs.modalXoa).hide()
            this.getDataFromApi(this.searchParamsJobType)
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
          JobTypeApi.update(item.JobTypeK, item)
            .then(res => {
              this.getDataFromApi(this.searchParamsJobType)
              this.$notify({
                text: 'Cập nhật loại công việc thành công',
                color: 'success'
              });
              (this.$refs.modalInsertOrUpdateJobType).hide();
              (this.$refs.modalInsertOrUpdateJobType).saving = false
            })
            .catch(res => {
              this.$notify({
                text: 'Cập nhật loại công việc thất bại',
                color: 'error'
              });
              (this.$refs.modalInsertOrUpdateJobType).saving = false
            })
        } else {
          JobTypeApi.insert(item)
            .then(res => {
              this.getDataFromApi(this.searchParamsJobType)
              this.$notify({
                text: 'Thêm mới loại công việc thành công',
                color: 'success'
              });
              (this.$refs.modalInsertOrUpdateJobType).hide();
              (this.$refs.modalInsertOrUpdateJobType).saving = false
            })
            .catch(res => {
              this.$notify({
                text: 'Thêm mới loại công việc thất bại',
                color: 'error'
              });
              (this.$refs.modalInsertOrUpdateJobType).saving = false
            })
        }
      }
    }
  }
</script>
