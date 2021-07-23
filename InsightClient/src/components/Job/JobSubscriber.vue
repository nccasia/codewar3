<template>
  <v-card class="pt-3 mt-5">
    <v-card-title class="primary white--text">
      <span class="title">Danh sách ứng viên</span>
      <v-spacer></v-spacer>
    </v-card-title>
      <v-card-text>
        <v-layout wrap>
          <v-flex xs12 sm4>
            <v-text-field
            v-model="searchParamSubscriber.searchTerm"
            append-icon="search"
            label="Tìm kiếm..."
            single-line
            clearable
            @input="getDataFromApi(searchParamSubscriber)"
            ></v-text-field>
          </v-flex>
        </v-layout>
          <v-layout row wrap>
              <v-flex xs12>
              <v-data-table :headers="tableHeader"
                          :items="subscribers"
                          @update:pagination="getDataFromApi" :pagination.sync="searchParamSubscriber"
                          :total-items="searchParamSubscriber.totalItems"
                          :loading="loadingTable"
                          class="table-border table">
                    <template slot="items" slot-scope="props" style="sox-height:100px;">
                      <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.UserName }}
                      </td>
                        <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.Email}}
                      </td>
                        <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.CreateTime || moment('DD/MM/YYYY')}}
                      </td>
                        <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.Email}}
                      </td>
                      <td class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;">
                      <v-tooltip bottom>
                        <v-btn slot="activator" small icon class="mx-0 my-0">
                          <v-icon color="teal" small>info</v-icon>
                        </v-btn>
                        <span>Chi tiết</span>
                      </v-tooltip>
                      <v-tooltip bottom>
                        <v-btn icon slot="activator" small class="mx-0 my-0">
                          <v-icon color="pink" small>check</v-icon>
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
      </v-card-text>
  </v-card>
</template>
<script>
import JobApi from '../../apiResources/JobApi'
import moment from 'moment'
export default {
  name: 'JobSubscriber',
  components: {
  },
  $_veeValidate: {
    validator: 'new'
  },
  mounted(){
    this.JobK = this.$route.params.JobK
  },
  data () {
    return {
      JobK: null,
      subscribers: [],
      tableHeader: [
        { text: 'Họ tên', align: 'center', value: 'UserName', sortable: false },
        { text: 'Email', align: 'center', value: 'Email', sortable: false },
        { text: 'Ngày tạo yêu cầu', align: 'center', value: 'CreateTime', sortable: false },
        { text: 'Trạng thái', align: 'center', value: 'IsAccepted', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      searchParamSubscriber: { includeEntities: true, rowsPerPage: 10 },
      loadingTable: false,
      selected: 0
    }
  },
  watch: {
  },
  methods: {
    getDataFromApi (searchParamSubscriber) {
      var JobK = this.$route.params.JobK
      searchParamSubscriber.JobK = JobK
      this.loadingTable = true
      JobApi.getJobCandicate(searchParamSubscriber).then(res => {
        this.subscribers = res.Data
        this.loadingTable = false
      }).catch(res => {
        this.loadingTable = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    }
  }
}
</script>
