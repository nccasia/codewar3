<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm4>
                  <v-text-field
                  v-model="searchParamsJob.searchTerm"
                  append-icon="search"
                  label="Tìm kiếm..."
                  single-line
                  clearable
                  @input="getDataFromApi(searchParamsJob)"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm8 class="text-xs-right">
                  <v-btn @click="showModalInsertOrUpdateJob(0, false)" color="primary" style="top: 15px;" dark small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-flex>
              </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                    <v-data-table :headers="tableHeader"
                                :items="jobs"
                                @update:pagination="getDataFromApi" :pagination.sync="searchParamsJob"
                                :total-items="searchParamsJob.totalItems"
                                :loading="loadingTable"
                                class="table-border table">
                          <template slot="items" slot-scope="props" style="sox-height:100px;">
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              <router-link :to="'/chi-tiet-cong-viec/'+props.item.JobK">{{props.item.Name}}</router-link>
                            </td>
                             <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.Title}}
                            </td>
                             <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.CustomerOrder}}
                            </td>
                             <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.JobTypeName}}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.WorkFlowStatus }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.Description }}
                            </td>

                            <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;"> <!--aaaa -->
                            <v-tooltip bottom>
                              <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalInsertOrUpdateJob(props.item.JobK, true)">
                                <v-icon color="teal" small>edit</v-icon>
                              </v-btn>
                              <span>Cập nhật công việc</span>
                            </v-tooltip>
                            <v-tooltip bottom>
                              <v-btn icon slot="activator" small @click="showModalXoa(props.item.JobK)" class="mx-0 my-0">
                                <v-icon color="pink" small>delete</v-icon>
                              </v-btn>
                              <span>Xoá công việc</span>
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
        <component :is="'ModalInsertOrUpdateJob'" ref="modalThemSuaCongViec" @save="save"/>
    </v-flex>
</template>
<script>
import JobApi  from '../../apiResources/JobApi'
import ModalXoa from '../Commons/ModalXoa.vue'
import ModalInsertOrUpdateJob from './ModalInsertOrUpdateJob.vue'
import Vue from 'vue'
export default {
  name: 'Job',
  components: {
    ModalInsertOrUpdateJob,
    ModalXoa
  },
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      jobs: [],
      tableHeader: [
        { text: 'Tên công việc', align: 'center', value: 'Name', sortable: false },
        { text: 'Tiêu đề', align: 'center', value: 'Title', sortable: false },
        { text: 'Khách hàng', align: 'center', value: 'CustomerOrder', sortable: false },
        { text: 'Loại công việc', align: 'center', value: 'JobType', sortable: false },
        { text: 'Trạng thái', align: 'center', value: 'WorkFlowStatus', sortable: false },
        { text: 'Mô tả', align: 'center', value: 'MoTa', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      searchParamsJob: { includeEntities: true, rowsPerPage: 10 },
      loadingTable: false,
      selected: 0,
      dialogConfirmDelete: false
    }
  },
  watch: {
  },
  methods: {
    getDataFromApi (searchParamsJob) {
      this.loadingTable = true
      JobApi.getJobs(searchParamsJob).then(res => {
        this.jobs = res.Data
        this.searchParamsJob.totalItems = res.Pagination.totalItems
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    showModalInsertOrUpdateJob (JobK, isUpdate) {
      (this.$refs.modalThemSuaCongViec).show(JobK, isUpdate)
    },
    showModalXoa (id) {
      this.selected = id;
      (this.$refs.modalXoa).show()
    },
    xoa () {
      if (this.selected == null) {
        return
      }
      JobApi.delete(this.selected)
        .then(res => {
          (this.$refs.modalXoa).hide()
          this.getDataFromApi(this.searchParamsJob)
          this.$notify({
            text: 'Xóa công việc thành công',
            color: 'success'
          })
        })
        .catch(res => {
          this.$notify({
            text: 'Xóa công việc thất bại',
            color: 'error'
          })
        })
    },
    save (item, isUpdate) {
      if (isUpdate) {
        JobApi.update(item.JobK, item)
          .then(res => {
            this.getDataFromApi(this.searchParamsJob)
            this.$notify({
              text: 'Cập nhật công việc thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaCongViec).hide();
            (this.$refs.modalThemSuaCongViec).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Cập nhật công việc thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaCongViec).saving = false
          })
      } else {
        JobApi.insert(item)
          .then(res => {
            this.getDataFromApi(this.searchParamsJob)
            this.$notify({
              text: 'Thêm mới công việc thành công',
              color: 'success'
            });
            (this.$refs.modalThemSuaCongViec).hide();
            (this.$refs.modalThemSuaCongViec).saving = false
          })
          .catch(res => {
            this.$notify({
              text: 'Thêm mới công việc thất bại',
              color: 'error'
            });
            (this.$refs.modalThemSuaCongViec).saving = false
          })
      }
    }
  }
}
</script>
