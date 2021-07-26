<template>
    <v-dialog v-model="dialog" scrollable width="1000px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">Thông tin công việc</span>
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
              <v-layout wrap>
                <v-flex xs12 sm6>
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
                <v-flex xs12 sm6>
                  <v-select
                    readonly
                    v-model="data.JobStatusFk"
                    :loading="jobStatusLoading"
                    :items="listJobStatuses"
                    item-text="Name"
                    item-value="ListCategoryK"
                    label="Trạng thái">
                  </v-select>
                </v-flex>
              </v-layout>

              <v-layout v-if="true">
                <v-flex xs12>
                  <v-select
                  readonly
                  :items="listJobWFStatuses"
                  label="Luồng công việc"
                  item-text="Name"
                  item-value="WorkFlowStatusK"
                  v-model="data.WorkflowStatusFk"
                  ></v-select>
                </v-flex>
              </v-layout>

              <v-layout>
                <v-flex xs12>
                  <v-text-field
                    readonly
                    v-model="data.Description"
                    label="Ghi chú"
                    auto-grow
                    rows="4"
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
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
            </v-container>
          </form>
        </v-card-text>
      </v-card>
    </v-dialog>
</template>
<script>
import JobApi from '../../apiResources/JobApi'
import JobTypeApi from '../../apiResources/JobTypeApi'
import WorkflowStatusApi from '../../apiResources/WorkflowStatusApi'
import ListCategoryApi from '../../apiResources/ListCategoryApi'
import moment from 'moment'
export default {
  name: 'ModalViewDetailJob',
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      menu1: false,
      isUpdate: false,
      saving: false,
      dialog: false,
      date: '',
      data: {},
      jobTypes: [],
      listJobStatuses: [],
      listJobWFStatuses: [],
      loadingModal: false,
      jobTypeLoading: false,
      jobStatusLoading: false,
      jobWFStatusLoading: false,
      searchParamsDanhMuc: { includeEntities: true, rowsPerPage: -1 },
      sanPhamId: 0,
      selected: 0
    }
  },
  watch: {
    date (val) {
      this.data.RegistrationDeadline = this.formatDate(this.date) || ''
    }
  },
  computed: {

  },
  methods: {
    formatDate (date) {
      if (!date) return null

      const [year, month, day] = date.split('-')
      return `${day}/${month}/${year}`
    },
    parseDate (date) {
      if (!date) return null

      const [day, month, year] = date.split('/')
      return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    },
    hide () {
      this.dialog = false
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
    getData (JobK) {
      this.loadingModal = true
      JobApi.detail(JobK)
        .then(res => {
          this.data = res
          this.data.RegistrationDeadline = moment(this.data.RegistrationDeadline).format('YYYY-MM-DD')
          this.loadingModal = false
        })
        .catch(res => {
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    save () {
      this.saving = true
      this.$validator.validateAll('formEdit').then((res) => {
        if (res) {
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
    show (JobK) {
      Object.assign(this.$data, (this.$options).data.apply(this))
      this.getData(JobK)
      this.dialog = true
      this.getJobTypes()
      this.getStatuses()
      this.getWFStatuses()
    }
  }
}
</script>
