<template>
  <div class="app-container">
    <div class="header">
      <h1 class="page-title">Request Management</h1>
      <el-button type="primary" size="small" @click="toggleDialogVisible(true)"
        >Request Device</el-button
      >
    </div>
    <el-tabs type="border-card">
      <el-tab-pane label="Addtion">
        <AdditionRequestTable
          :requests="addtionRequests"
          @onEditRequest="onEditRequest"
          @onDeleteRequest="onDeleteRequest"
        ></AdditionRequestTable>
      </el-tab-pane>
      <el-tab-pane label="Return">
        <ReturnRequestTable
          :requests="returnRequests"
          @onEditRequest="onEditRequest"></ReturnRequestTable>
      </el-tab-pane>
    </el-tabs>

    <create-edit-request :request="request" @onSubmit="onSubmit"></create-edit-request>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'
import { ERequestType, IDeviceRequest, IRequestsParams } from './types'
import { DEVICE_REQUESTS } from './mock'
import AdditionRequestTable from './components/AdditionRequestTable.vue'
import ReturnRequestTable from './components/ReturnRequestTable.vue'
import CreateEditRequest from './components/CreateEditRequest.vue'
import { RequestModule } from '@/store/modules/request'
import {
  getRequestsApi,
  getRequestByIdApi,
  deleteRequestByIdApi
} from '@/api/request'

@Component({
  name: 'Guide',
  components: {
    AdditionRequestTable,
    ReturnRequestTable,
    CreateEditRequest
  }
})
export default class RequestManagement extends Vue {
  private requests: IDeviceRequest | null = null
  addtionRequests: IDeviceRequest[] = []
  returnRequests: IDeviceRequest[] = []
  request: IDeviceRequest | null = null
  private currentPage = 1
  readonly pageSize = 10
  private totalItems = 0

  mounted() {
    this.fetchAddtionRequests()
    this.fetchReturnRequests()
  }

  get requestDialogVisible() {
    return RequestModule.createEditDialogVisible
  }

  async fetchAddtionRequests(fetch = true) {
    if (!fetch) return
    const params: IRequestsParams = {
      skipCount: this.currentPage - 1,
      maxResultCount: this.pageSize,
      // searchText: '',
      filterItems: [{
        propertyName: 'Type',
        value: ERequestType.ADDITION
      }]
      // sort: 'desc',
      // sortDirection: 0
    }
    try {
      const { data } = await getRequestsApi(params)
      this.totalItems = data.result.totalCount
      this.addtionRequests = data.result.items
    } catch (error) {
      console.error(error)
    }
  }

  async fetchReturnRequests(fetch = true) {
    if (!fetch) return
    const params: IRequestsParams = {
      skipCount: this.currentPage - 1,
      maxResultCount: this.pageSize,
      // searchText: '',
      filterItems: [{
        propertyName: 'Type',
        value: ERequestType.RETURN
      }]
      // sort: 'desc',
      // sortDirection: 0
    }
    try {
      const { data } = await getRequestsApi(params)
      this.totalItems = data.result.totalCount
      this.returnRequests = data.result.items
    } catch (error) {
      console.error(error)
    }
  }

  toggleDialogVisible(visible: boolean) {
    RequestModule.toggleCreateEditDialogVisible(visible)
    if (!visible) {
      this.request = null
    }
  }

  async onEditRequest(id: string) {
    try {
      const { data } = await getRequestByIdApi(id)
      this.request = data.result
      RequestModule.toggleCreateEditDialogVisible(true)
    } catch (error) {
      console.error(error)
    }
  }

  async onDeleteRequest(id: string) {
    try {
      await deleteRequestByIdApi(id)
      this.fetchAddtionRequests()
      this.fetchReturnRequests()
    } catch (error) {
      console.error(error)
    }
  }

  @Watch('requestDialogVisible')
  onCloseDialog(val: boolean) {
    if (!val) {
      this.request = null
    }
  }

  onSubmit() {
    this.fetchAddtionRequests()
    this.fetchReturnRequests()
  }
}
</script>

<style lang="scss" scoped>
.app-container {
  padding: 32px;
  background-color: rgb(240, 242, 245);
  position: relative;

  .header {
    display: flex;
    align-items: center;

    .page-title {
      flex: 1;
    }
  }
}
</style>
