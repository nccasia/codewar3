<template>
  <v-container grid-list-md>
    <v-layout wrap>
      <v-flex xs12>
        <v-card>
          <v-toolbar color="primary" dark>
              <v-btn icon>
                  <v-icon>receipt</v-icon>
              </v-btn>
              <v-toolbar-title>Danh sách công việc</v-toolbar-title>
              <v-spacer></v-spacer>
          </v-toolbar>
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
          <v-layout wrap>
            <the-loader :loading="isLoading"></the-loader>

            <template v-for="job in jobs">
              <v-flex xs12 sm6 :key="job.JobK">
                <v-card> 
                  <v-card-title primary-title>
                    <div>
                      <h3 class="headline">{{job.Title}}</h3>
                      <div class="mt-3"><strong>Tên công việc: </strong> {{job.Name}}</div>
                      <div class="mt-2"><strong>Loại công việc: </strong> {{job.JobTypeName}}</div>
                      <div class="mt-2"><strong>Mô tả: </strong> {{ job.Description }} </div>
                      <div class="mt-2"><strong>Ngày hết hạn: </strong> {{ job.RegistrationDeadline | moment('DD-MM-YYYY') }} </div>
                    </div>
                  </v-card-title>

                  <v-card-actions>
                    <v-btn flat color="orange" @click.stop="showModalViewDetail(job.JobK)">Xem chi tiết</v-btn>
                    <v-btn flat color="orange" @click.stop="showModalRegister(job)">Đăng ký</v-btn>
                  </v-card-actions>
                </v-card>
              </v-flex>
            </template>
          </v-layout>
          </v-card>
      </v-flex>
    </v-layout>
    <pagination-panel
      :current-page="currentPage"
      :per-page="perPage"
      :rows="rows"
      @onChange="pageChange"
    />
    <component :is="'ModalViewDetailJob'" ref="modalViewDetailJob"/>
    <v-dialog v-model="dialog" v-if="selectedJob" max-width="350">
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">Đăng ký</span>
          <v-spacer></v-spacer>
          <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
        </v-card-title>
        <v-card-text class="py-3">Bạn có chắc chắn muốn đăng ký công việc: <strong>{{selectedJob.Name}}</strong> không!</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn flat @click.native="hide">Hủy</v-btn>
          <v-btn color="blue darken-1" :loading="registering" :disabled="registering" flat @click.native="registerJob">Đăng ký</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import JobApi  from '../../apiResources/JobApi'
import TheLoader from '../Commons/TheLoader.vue'
import ModalViewDetailJob from './ModalViewDetailJob.vue'
import PaginationPanel from '../Commons/PaginationPanel.vue'
export default {
  name: 'SearchJob',
  components: {
    TheLoader,
    ModalViewDetailJob,
    PaginationPanel
  },
  data () {
    return {
      selectedJob: null,
      registering: false,
      saving: false,
      dialog: false,
      currentPage: 1,
      perPage: 4,
      rows: 0,
      isLoading: false,
      lstTrangThai: [
        {icon: 'access_alarm', iconClass: 'amber white--text'},
        {icon: 'access_alarm', iconClass: 'red white--text'},
        {icon: 'check', iconClass: 'success white--text'}
      ],
      user: this.$store.state.user,
      userDetail: {},
      pagination: {},
      searchParamsJob: { includeEntities: true },
      jobs: []
    }
  },
  watch: {
  },
  created () {
    this.searchParamsJob.rowsPerPage = this.perPage
    this.getDataFromApi(this.searchParamsJob)
  },
  computed: {
    pages () {
      return Math.ceil(this.count / 5)
    }
  },
  methods: {
    showModalRegister(job){
      this.selectedJob = job,
      this.dialog = true
    },
    hide(){
      this.dialog = false
      this.selectedJob = null
    },
    registerJob(){
      this.registering = true;
      if(!this.selectedJob) return;
      var model = {
        JobFk: this.selectedJob.JobK,
      }
      this.isLoading = true
      JobApi.registerJob(model).then(res => {
        this.$notify({
          text: 'Đăng ký thành công',
          color: 'success'
        })
        window.location.reload();
        this.isLoading = false
      }).catch(res => {
        this.registering = false
        console.log(res)
        this.$notify({
          text: 'Đăng ký thất bại',
          color: 'error'
        })
      })
    },
    pageChange(page){
      this.searchParamsJob.page = page
      this.getDataFromApi(this.searchParamsJob)
    },
    showModalViewDetail (JobK) {
      (this.$refs.modalViewDetailJob).show(JobK)
    },
    getDataFromApi (searchParamsJob) {
      this.isLoading = true
      JobApi.getUnregistedJob(searchParamsJob).then(res => {
        this.jobs = res.Data
        this.rows = res.Pagination.totalItems
        this.isLoading = false
      }).catch(res => {
        this.isLoading = false
        console.log(res)
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    }
  }
}
</script>