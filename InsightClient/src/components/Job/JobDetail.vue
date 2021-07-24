<template>
  <div>
    <v-card>
      <v-card-title class="primary white--text">
        <span class="title">Thông tin công việc</span>
        <v-spacer></v-spacer>
      </v-card-title>
      <v-card-text>
        <the-loader :loading="isLoading"></the-loader>
        <form scope="formEdit" v-if="!isLoading">
          <v-container grid-list-lg pa-0>
            <v-layout wrap>
              <v-flex xs12 sm6>
                <v-text-field
                readonly
                label="Tên công việc"
                v-model="data.Name"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 sm6>
                <v-text-field
                readonly
                label="Tiêu đề"
                v-model="data.Title"
                ></v-text-field>
              </v-flex>
            </v-layout>

            <v-layout wrap>
              <v-flex xs12 sm6>
                <v-text-field
                readonly
                label="Khách hàng"
                v-model="data.CustomerOrder"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 sm6>
                <v-text-field
                  v-model="data.RegistrationDeadline"
                  label="Ngày hết hạn"
                  append-icon="event"
                  readonly
                ></v-text-field>
              </v-flex>
            </v-layout>
            <v-layout>
              <v-flex xs12 sm6>
                <v-text-field
                  readonly
                  v-model="data.Description"
                  label="Ghi chú"
                  auto-grow
                  rows="4"
                >
                </v-text-field>
              </v-flex>
              <v-flex xs12 sm6>
                <v-checkbox
                  disabled
                  v-model="data.IsActivated"
                  label="Kích hoạt"
                  auto-grow
                  rows="4"
                >
                </v-checkbox>
              </v-flex>              
            </v-layout>

            <v-layout wrap>
              <v-flex xs12>
                <v-select
                  readonly
                  v-model="data.JobTypeFk"
                  :items="jobTypes"
                  item-text="Name"
                  item-value="JobTypeK"
                  :loading="jobTypeLoading"
                  label="Loại công việc">
                </v-select>
              </v-flex>
              <v-flex xs12 v-if="data.WorkflowStatusFk">
                <v-select
                readonly
                :items="listJobWFStatuses"
                label="Luồng công việc"
                item-text="Description"
                item-value="WorkFlowStatusK"
                v-model="data.WorkflowStatusFk"
                ></v-select>
              </v-flex>
            </v-layout>
            <v-layout wrap v-if="currentUser && currentUser.UserId != 1 && checkCandicate">
              <v-flex xs12>
                <v-select
                :items="listJobWFStatuses"
                label="Cập nhật luồng công việc"
                item-text="Description"
                item-value="WorkFlowStatusK"
                data-vv-name="Cập nhật luồng công việc"
                v-model="newWorkflowStatusFk"
                data-vv-scope="formEdit"
                required
                :error-messages="errors.collect('Cập nhật luồng công việc')"
                ></v-select>
              </v-flex>
            </v-layout>

          </v-container>
        </form>
      </v-card-text>
      <v-card-actions v-if="!loadingModal">
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" :loading="saving" :disabled="saving" @click="update" flat >Lưu</v-btn>
        <v-btn color="blue darken-1" v-if="data.WorkflowStatusFk == '06cb4c50-2cea-eb11-ba93-b42e99af4c47'" :loading="saving" :disabled="saving" @click="submit" flat >Gửi</v-btn>
      </v-card-actions>
    </v-card>
    <job-subscriber v-if="currentUser && currentUser.UserId == 1"></job-subscriber>
  </div>
</template>
<script>
import TheLoader from '../Commons/TheLoader.vue'
import JobSubscriber from './JobSubscriber.vue'
import JobApi  from '../../apiResources/JobApi'
import JobTypeApi  from '../../apiResources/JobTypeApi'
import ListCategoryApi  from '../../apiResources/ListCategoryApi'
import WorkflowStatusApi  from '../../apiResources/WorkflowStatusApi'
import moment from 'moment'
export default {
  name: 'JobDetail',
  components: {
    JobSubscriber,
    TheLoader
  },
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      checkCandicate: false,
      currentUser: null,
      loadingModal: false,
      newWorkflowStatusFk: null,
      data: {},
      jobTypes: [],
      listJobStatuses: [],
      listJobWFStatuses: [],
      JobK: null,
      isLoading: false,
      jobTypeLoading: false,
      jobStatusLoading: false,
      jobWFStatusLoading: false,
      saving: false
    }
  },
  mounted() {
    this.JobK = this.$route.params.JobK
    this.getJob()
    this.getJobTypes()
    this.getStatuses()
    this.getWFStatuses()
    this.getCurrentUser()
    if(this.currentUser && this.currentUser.UserId != 1){
      this.checkCandicateFunction()
    }
  },
  watch: {
  },
  methods: {
    update(){
      if(!this.newWorkflowStatusFk) return
      var data = {...this.data};
      data.WorkflowStatusFk = this.newWorkflowStatusFk
      data.RegistrationDeadline = moment(data.RegistrationDeadline, 'DD-MM-YYYY').format('YYYY-MM-DD')
      JobApi.update(data.JobK,data)
        .then(res => {
          window.location.reload()
          this.$notify({
            text: 'Cập nhật công việc thành công',
            color: 'success'
          });
        })
        .catch(res => {
          this.$notify({
            text: 'Cập nhật công việc thất bại',
            color: 'error'
          });
        })
    },
    submit(){
      var data = {...this.data};
      data.RegistrationDeadline = moment(data.RegistrationDeadline, 'DD-MM-YYYY').format('YYYY-MM-DD')
      JobApi.submit(data)
        .then(res => {
          window.location.reload()
          this.$notify({
            text: 'Cập nhật công việc thành công',
            color: 'success'
          });
        })
        .catch(res => {
          this.$notify({
            text: 'Cập nhật công việc thất bại',
            color: 'error'
          });
        })
    },
    getJob () {
      this.isLoading = true
      JobApi.detail(this.JobK).then(res => {
        this.data = res
        this.data.RegistrationDeadline = moment(this.data.RegistrationDeadline).format('DD-MM-YYYY')
        this.isLoading = false
      }).catch(res => {
        this.isLoading = false
        console.log(res)
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
    getStatuses () {
      this.jobStatusLoading = true
      ListCategoryApi.getAllJobStatuses()
        .then(res => {
          this.listJobStatuses = res
          this.jobStatusLoading = false
        })
        .catch(res => {
          this.jobStatusLoading = false
          this.$notify({
            text: 'Lấy dữ liệu thất bại thất bại',
            color: 'error'
          })
        })
    },
    approveJob(){
      this.jobStatusLoading = true
      ListCategoryApi.getAllJobStatuses()
        .then(res => {
          this.listJobStatuses = res
          this.jobStatusLoading = false
        })
        .catch(res => {
          this.jobStatusLoading = false
          this.$notify({
            text: 'Lấy dữ liệu thất bại thất bại',
            color: 'error'
          })
        })
    },
    getWFStatuses () {
      this.jobWFStatusLoading = true
      WorkflowStatusApi.getAll()
        .then(res => {
          this.listJobWFStatuses = res
          this.jobWFStatusLoading = false
        })
        .catch(res => {
          this.jobWFStatusLoading = false
          this.$notify({
            text: 'Lấy dữ liệu thất bại thất bại',
            color: 'error'
          })
        })
    },
    checkCandicateFunction () {
      var JobK = this.JobK;
      JobApi.checkCandicate(JobK)
        .then(res => {
          this.checkCandicate = res
        })
        .catch(res => {
          this.$notify({
            text: 'Lấy dữ liệu thất bại thất bại',
            color: 'error'
          })
        })
    },
    getCurrentUser(){
      this.currentUser = this.$store.state.user.User
    }
  }
}
</script>
