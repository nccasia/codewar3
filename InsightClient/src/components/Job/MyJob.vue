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
                              {{ props.item.Description }}
                            </td>
                            <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                              {{ props.item.IsAccepted ? "Đã chấp thuận" : "Đang chờ chấp thuận" }}
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
    </v-flex>
</template>
<script>
import JobApi  from '../../apiResources/JobApi'
export default {
  name: 'MyJob',
  components: {
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
        { text: 'Mô tả', align: 'center', value: 'MoTa', sortable: false },
        { text: 'Trạng thái', align: 'center', value: 'Satus', sortable: false },
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
      JobApi.getMyJob(searchParamsJob).then(res => {
        this.jobs = res.Data
        this.rows = res.Pagination.totalItems
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
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
