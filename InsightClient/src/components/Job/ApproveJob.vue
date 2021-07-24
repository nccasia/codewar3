<template>
    <v-flex xs12>
        <v-card>
            <v-toolbar color="primary" dark>
                <v-btn icon>
                    <v-icon>receipt</v-icon>
                </v-btn>
                <v-toolbar-title>Phê duyệt đăng ký</v-toolbar-title>
                <v-spacer></v-spacer>
            </v-toolbar>
            <v-card-text class="d-flex">
              <v-flex xs3>
                <v-layout wrap>
                  <v-flex xs12>
                    <v-text-field
                    v-model="searchParamsJob.searchTerm"
                    append-icon="search"
                    label="Tên người đăng ký"
                    single-line
                    clearable
                    ></v-text-field>
                  </v-flex>
                  <v-flex xs12>
                    <v-select
                      label="Loại công việc"
                      v-model="searchParamsJob.jobTypeK"
                      :items="jobTypes"
                      item-text="Description"
                      item-value="JobTypeK"
                      :loading="jobTypeLoading"
                      >
                    </v-select>
                  </v-flex>
                </v-layout>
                <v-layout>
                  <v-flex xs12 sm8 class="text-xs-right">
                    <v-btn @click="getDataFromApi(searchParamsJob)" color="primary" style="top: 15px;" dark small>Tìm kiếm</v-btn>
                  </v-flex>
                </v-layout>
              </v-flex>
              <v-flex xs9>
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
                            {{ props.item.JobName}}
                          </td>
                          <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.JobType}}
                          </td>
                          <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.UserName}}
                          </td>
                          <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.IsAccepted ? "Đã chấp thuận": "Chưa chấp thuận"  }}
                          </td>

                          <td  class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;">
                          <v-tooltip bottom v-if="!props.item.IsAccepted">
                            <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalApprove(props.item)">
                              <v-icon color="teal" small>how_to_reg</v-icon>
                            </v-btn>
                            <span>Phê duyệt</span>
                          </v-tooltip>
                          </td>
                        </template>
                        <template slot="no-data">
                          <div class="text-xs-center">Không có dữ liệu</div>
                        </template>
                      </v-data-table>
                  </v-flex>
              </v-layout>
              </v-flex>

            </v-card-text>
        </v-card>
        <v-dialog v-model="dialog" v-if="selected" max-width="350">
          <v-card>
            <v-card-title class="primary white--text">
              <span class="title">Phê duyệt</span>
              <v-spacer></v-spacer>
              <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
            </v-card-title>
            <v-card-text class="py-3">Bạn có chắc chắn muốn phê duyệt công việc <strong>{{selected.Name}}</strong> cho <strong>{{selected.UserRegis}}</strong> không!</v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn flat @click.native="hide">Hủy</v-btn>
              <v-btn color="blue darken-1" :loading="approving" :disabled="approving" flat @click.native="approve">Đăng ký</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
    </v-flex>
</template>
<script>
import JobApi from '../../apiResources/JobApi'
import JobTypeApi from '../../apiResources/JobTypeApi'
export default {
  name: 'ApproveJob',
  components: {
  },
  $_veeValidate: {
    validator: 'new'
  },
  mounted(){
    this.getJobTypes()
  },
  data(){
    return {
      approving: false,
      dialog: false,
      selected: null,
      jobTypes: [],
      jobs: [],
      jobTypeLoading: false,
      jobTypeK: null,
      searchParamsJob: { includeEntities: true, rowsPerPage: 10, jobTypeK: '' },
      loadingTable: false,
      tableHeader: [
        { text: 'Tên công việc', align: 'center', value: 'JobName', sortable: false },
        { text: 'Loại công việc', align: 'center', value: 'JobType', sortable: false },
        { text: 'Người đăng ký', align: 'center', value: 'UserName', sortable: false },
        { text: 'Trạng thái', align: 'center', value: 'IsAccepted', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
    }
  },
  methods: {
    showModalApprove(item){
      this.selected = item
      this.dialog = true
    },
    hide(){
      this.dialog = false
      this.selected = null
    },
    approve(){
      this.approving = true
      var RegistrationJob = this.selected.RegistrationJob;
      JobApi.aprovedRegistrationJob(RegistrationJob).then(res => {
        this.approving = false
        this.dialog = false
        this.$notify({
          text: 'Phê duyệt thành công',
          color: 'success'
        })
      }).catch(res => {
        this.approving = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    getJobTypes () {
      this.jobTypeLoading = true
      JobTypeApi.getAll()
        .then(res => {
          this.jobTypes = res
          this.jobTypeLoading = false
        })
        .catch(res => {
          this.jobTypeLoading = false
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    getDataFromApi(searchParamsJob){
      this.loadingTable = true
      JobApi.getAllJobCandicate(searchParamsJob)
        .then(res => {
          console.log(res)
          this.jobs = res.Data
          this.loadingTable = false
        })
        .catch(res => {
          this.loadingTable = false
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    }
  }
}
</script>
