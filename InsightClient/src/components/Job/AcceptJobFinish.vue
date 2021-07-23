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
                            {{ props.item.Name}}
                          </td>
                          <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.JobType}}
                          </td>
                          <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.UserRegis}}
                          </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                            {{ props.item.Description}}
                          </td>
                          <td  class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;">
                          <v-tooltip bottom>
                            <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="showModalDecline(props.item)">
                              <v-icon color="teal" small>thumb_down_off_alt</v-icon>
                            </v-btn>
                            <span>Từ chối</span>
                          </v-tooltip>
                          <v-tooltip bottom>
                            <v-btn slot="activator" small icon class="mx-0 my-0" @click.stop="approve">
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
            <v-card-text class="py-3">Công việc {{selected.Name}} sẽ bị đánh dấu là chưa hoàn thành!</v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn flat @click.native="hide">Hủy</v-btn>
              <v-btn color="blue darken-1" :loading="declining" :disabled="declining" flat @click.native="decline">Lưu</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
    </v-flex>
</template>
<script>
import JobTypeApi from '../../apiResources/JobTypeApi'
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
  },
  data(){
    return {
      declining: false,
      dialog: false,
      selected: null,
      jobTypes: [],
      jobs: [],
      jobTypeLoading: false,
      jobTypeK: null,
      searchParamsJob: { includeEntities: true, rowsPerPage: 10 },
      loadingTable: false,
      tableHeader: [
        { text: 'Tên công việc', align: 'center', value: 'Name', sortable: false },
        { text: 'Loại công việc', align: 'center', value: 'JobType', sortable: false },
        { text: 'Người đăng ký', align: 'center', value: 'UserRegis', sortable: false },
        { text: 'Mô tả', align: 'center', value: 'Description', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
    }
  },
  methods: {
    decline(){
      console.log('TODO')
      this.dialog = false
      this.selected = null
    },
    approve(){
      console.log('TODO')
    },
    showModalDecline(item){
      this.selected = item
      this.dialog = true
    },
    hide(){
      this.dialog = false
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
    getDataFromApi(searchParamsJob){
      this.jobs = [
        {Name: "CV1", JobType: "BA", Description: "Mô tả", UserRegis: "User 1", IsAccepted: false},
        {Name: "CV2", JobType: "FE", Description: "Mô tả", UserRegis: "User 2", IsAccepted: true}
      ]
    }
  }
}
</script>
