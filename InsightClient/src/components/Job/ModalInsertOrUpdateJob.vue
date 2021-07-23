<template>
    <v-dialog v-model="dialog" scrollable width="1000px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật công việc' : 'Thêm mới công việc'}}</span>
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
                  label="Tên công việc"
                  v-model="data.Name"
                  data-vv-name="Tên công việc"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Tên công việc')"
                  required
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6>
                  <v-text-field
                  label="Tiêu đề"
                  v-model="data.Title"
                  data-vv-name="Tiêu đề"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Tiêu đề')"
                  required
                  ></v-text-field>
                </v-flex>
              </v-layout>

              <v-layout wrap>
                <v-flex xs12 sm6>
                  <v-text-field
                  label="Khách hàng"
                  v-model="data.CustomerOrder"
                  data-vv-name="Khách hàng"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Khách hàng')"
                  required
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 sm6>
                  <v-menu
                    ref="menu1"
                    :close-on-content-click="false"
                    v-model="menu1"
                    :nudge-right="40"
                    lazy
                    transition="scale-transition"
                    offset-y
                    full-width
                    min-width="290px"
                  >
                    <v-text-field
                      slot="activator"
                      v-model="data.RegistrationDeadline"
                      label="Ngày hết hạn"
                      data-vv-name="Ngày hết hạn"
                      append-icon="event"
                      :error-messages="errors.collect('Ngày hết hạn')"
                      readonly
                    ></v-text-field>
                    <v-date-picker
                      ref="picker"
                      locale="vi-VN"
                      v-validate="{required: true}"
                      v-model="data.RegistrationDeadline"
                      data-vv-scope="formEdit"
                      :max="new Date().toISOString().substr(0, 10)"
                      min="1900-01-01"
                    ></v-date-picker>
                  </v-menu>
                </v-flex>

              </v-layout>
              <v-layout wrap>
                <v-flex xs12 sm6>
                  <v-select
                    v-model="data.JobTypeFk"
                    :items="jobTypes"
                    item-text="Description"
                    item-value="JobTypeK"
                    :loading="jobTypeLoading"
                    required
                    data-vv-name="Loại công việc"
                    data-vv-scope="formEdit"
                    :error-messages="errors.collect('Loại công việc')"
                    label="Loại công việc">
                  </v-select>
                </v-flex>
                <v-flex xs12 sm6>
                  <v-text-field
                    v-model="data.Description"
                    label="Ghi chú"
                    auto-grow
                    rows="4"
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>

              <v-layout>

              </v-layout>
              <v-layout>
                <v-flex xs12>
                  <v-checkbox
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
        <v-card-actions v-if="!loadingModal">
          <v-spacer></v-spacer>
          <v-btn flat @click.native="hide">Hủy</v-btn>
          <v-btn color="blue darken-1" :loading="saving" :disabled="saving" flat @click.native="save">Lưu</v-btn>
        </v-card-actions>
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
  name: 'ModalInsertOrUpdateJob',
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
          this.date = moment(this.data.RegistrationDeadline).format('YYYY-MM-DD')
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
    show (JobK, isUpdate) {
      Object.assign(this.$data, (this.$options).data.apply(this))
      if (isUpdate) {
        this.getData(JobK)
      } else {
      }
      this.isUpdate = isUpdate
      this.dialog = true
      this.getJobTypes()
      this.getStatuses()
      this.getWFStatuses()
    }
  }
}
</script>
