<template>
    <v-flex xs12>
        <v-card>
            <v-toolbar color="primary" dark>
                <v-btn icon>
                    <v-icon>receipt</v-icon>
                </v-btn>
                <v-toolbar-title>Phê duyệt hoàn thành công việc</v-toolbar-title>
                <v-spacer></v-spacer>
            </v-toolbar>
            <v-card-text class="d-flex">
              <v-flex xs3>
                <v-layout wrap>
                  <v-flex xs12>
                    <v-text-field
                    v-model="searchParamsJob.searchTerm"
                    append-icon="search"
                    label="Tên công việc"
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
                            Hoàn thành
                          </td>
                          <td  class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;">
                          <v-tooltip bottom>
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
              <span class="title">Từ chối</span>
              <v-spacer></v-spacer>
              <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
            </v-card-title>
            <v-card-text class="py-3">
              Công việc <strong>{{selected.Name}}</strong> sẽ bị đánh dấu là chưa hoàn thành!
              <v-layout wrap>
                <v-flex xs12>
                <v-select
                  v-model="rejectData.ReasonFK"
                  :items="rejectReasons"
                  item-text="Description"
                  item-value="ListCategoryK"
                  :loading="jobRejectReasonLoading"
                  required
                  data-vv-name="Lý do"
                  data-vv-scope="formReject"
                  :error-messages="errors.collect('Lý do')"
                  label="Lý do">
                </v-select>
                  <v-text-field
                  label="Ghi chú"
                  v-model="rejectData.reason"
                  data-vv-name="Ghi chú"
                  data-vv-scope="formReject"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Ghi chú')"
                  required
                  ></v-text-field>
                </v-flex>
                <v-flex xs12>

                </v-flex>
              </v-layout>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn flat @click.native="hide">Hủy</v-btn>
              <v-btn color="blue darken-1" :loading="declining" :disabled="declining" flat @click.native="decline">Lưu</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogAccept" v-if="selected" max-width="350">
          <v-card>
            <v-card-title class="primary white--text">
              <span class="title">Xác nhận</span>
              <v-spacer></v-spacer>
              <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
            </v-card-title>
            <v-card-text class="py-3">Công việc <strong>{{selected.Name}}</strong> sẽ được đánh dấu là hoàn thành!</v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn flat @click.native="hide">Hủy</v-btn>
              <v-btn color="blue darken-1" :loading="declining" :disabled="declining" flat @click.native="approve">Lưu</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
    </v-flex>
</template>
<script>
import JobTypeApi from '../../apiResources/JobTypeApi'
import JobApi from '../../apiResources/JobApi'
import ListCategoryApi from '../../apiResources/ListCategoryApi'
import TheLoaderVue from '../Commons/TheLoader.vue'
export default {
  name: 'AcceptJobFinish',
  components: {
  },
  $_veeValidate: {
    validator: 'new'
  },
  mounted(){
    this.getJobTypes()
    this.getDataFromApi(this.searchParamsJob)
    this.getRejectReason()
  },
  data(){
    return {
      rejectData: {},
      loadingReasonStatus: false,
      declining: false,
      dialogAccept: false,
      dialog: false,
      selected: null,
      jobTypes: [],
      jobs: [],
      rejectReasons: [],
      jobTypeLoading: false,
      jobRejectReasonLoading: false,
      jobTypeK: null,
      searchParamsJob: { includeEntities: true, rowsPerPage: 10 },
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
    decline(e){
      e.preventDefault();
      this.declining = true
      this.$validator.validateAll('formReject').then((res) => {
        if (res) {
        } else {
          this.declining = false
          this.$notify({
            text: 'Cần điền đủ thông tin',
            color: 'error'
          })
        }
      });

    },
    approve(){
      console.log('TODO')
    },
    showModalDecline(item){
      this.selected = item
      this.dialog = true
    },
    showModalAccept(item){
      this.selected = item
      this.dialogAccept = true
    },
    hide(){
      this.dialog = false
      this.dialogAccept = false
      this.rejectData = {}
      this.selected = null
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
    getRejectReason(){
      this.jobRejectReasonLoading = true
      ListCategoryApi.getRejectReason()
        .then(res => {
          this.rejectReasons = res
          this.jobRejectReasonLoading = false
        })
        .catch(res => {
          this.jobRejectReasonLoading = false
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    getDataFromApi(searchParamsJob){
      this.loadingTable = true
      JobApi.getAllJobFinished(searchParamsJob)
        .then(res => {
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
