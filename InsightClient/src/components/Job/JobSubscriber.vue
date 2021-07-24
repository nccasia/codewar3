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
                        {{ props.item.CreateTime || moment('DD/MM/YYYY, HH:mm')}}
                      </td>
                      <td style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                        {{ props.item.IsAccepted ? "Đã chấp thuận": "Chưa chấp thuận"  }}
                      </td>

                      <td v-if="!isAccepted" class="justify-center px-0 text-md-center" style="width: 8%;white-space: nowrap;">
                      <v-tooltip bottom>
                        <v-btn slot="activator" small icon class="mx-0 my-0">
                          <v-icon color="teal" small>how_to_reg</v-icon>
                        </v-btn>
                        <span>Chi tiết</span>
                      </v-tooltip>
                      <v-tooltip bottom>
                        <v-btn icon slot="activator" small class="mx-0 my-0" @click.native="openModalApprove(props.item)">
                          <v-icon color="pink" small>check</v-icon>
                        </v-btn>
                        <span>Phê duyệt</span>
                      </v-tooltip>
                      </td>
                      <td v-else style="width text-overflow: ellipsis; overflow: hidden;" class="text-xs-center">
                      </td>
                    </template>
                    <template slot="no-data">
                      <div class="text-xs-center">Không có dữ liệu</div>
                    </template>
                  </v-data-table>
              </v-flex>
          </v-layout>
      </v-card-text>
        <v-dialog v-model="dialog" v-if="selectedItem" max-width="350">
          <v-card>
            <v-card-title class="primary white--text">
              <span class="title">Phê duyệt</span>
              <v-spacer></v-spacer>
              <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
            </v-card-title>
            <v-card-text class="py-3">Bạn có chắc chắn muốn phê duyệt công việc <strong>{{selectedItem.Name}}</strong> cho <strong>{{selectedItem.UserRegis}}</strong> không!</v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn flat @click.native="hide">Hủy</v-btn>
              <v-btn color="blue darken-1" :loading="approving" :disabled="approving" flat @click.native="approve">Đăng ký</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
  </v-card>
</template>
<script>
import JobApi from '../../apiResources/JobApi'
// import moment from 'moment'
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
      isAccepted: false,
      tableHeader: [
        { text: 'Họ tên', align: 'center', value: 'UserName', sortable: false },
        { text: 'Email', align: 'center', value: 'Email', sortable: false },
        { text: 'Ngày tạo yêu cầu', align: 'center', value: 'CreateTime', sortable: false },
        { text: 'Trạng thái', align: 'center', value: 'IsAccepted', sortable: false },
        { text: 'Thao tác', align: 'center', value: '', sortable: false }
      ],
      approving: false,
      searchParamSubscriber: { includeEntities: true, rowsPerPage: 10 },
      loadingTable: false,
      selected: 0,
      selectedItem: null
    }
  },
  watch: {
  },
  methods: {
    openModalApprove(item){
      this.selectedItem = item
      this.dialog = true
    },
    hide(){
      this.dialog = false,
      this.selectedItem = null
    },
    approve(){
      this.approving = true
      var RegistrationJob = this.selectedItem.RegistrationJob;
      JobApi.aprovedRegistrationJob(RegistrationJob).then(res => {
        this.approving = false
        this.dialog = false
        this.$notify({
          text: 'Phê duyệt thành công',
          color: 'success'
        })
        window.location.reload()
      }).catch(res => {
        this.approving = false
        this.$notify({
          text: 'Lấy dữ liệu thất bại',
          color: 'error'
        })
      })
    },
    getDataFromApi (searchParamSubscriber) {
      var JobK = this.$route.params.JobK
      searchParamSubscriber.JobK = JobK
      this.loadingTable = true
      JobApi.getJobCandicate(searchParamSubscriber).then(res => {
        this.subscribers = res.Data
        for(let i=0; i< this.subscribers.length ; i++){
          if(this.subscribers[i].IsAccepted){
            this.isAccepted = true;
            break;
          }
        }
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
