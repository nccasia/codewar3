<template>
  <div class="table-outer">
    <el-table :data="devices">
      <el-table-column min-width="110px" label="Device Name" align="left">
        <template slot-scope="{row}">
          {{ row.name }}
        </template>
      </el-table-column>

      <el-table-column
        min-width="110px"
        label="Description"
        align="left"
        v-if="!isMobile"
      >
        <template slot-scope="{row}">
          {{ row.description }}
        </template>
      </el-table-column>

      <el-table-column
        min-width="120px"
        label="Manufacture"
        align="left"
        v-if="!isMobile"
      >
        <template slot-scope="{row}">
          {{ row.manufacturer }}
        </template>
      </el-table-column>

      <el-table-column
        min-width="135px"
        label="Guarantee Date"
        align="left"
        v-if="!isMobile"
      >
        <template slot-scope="{row}">
          {{ new Date(row.guaranteeDate) | moment('MMMM Do YYYY') }}
        </template>
      </el-table-column>

      <el-table-column label="Status" align="left">
        <template slot-scope="{row}">
          <el-tag :type="getStatusType(row.status)" size="mini">
            {{ row.status | deviceStatus }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="Actions" align="left" min-width="140px">
        <template slot-scope="{row}">
          <el-button
            circle
            type="primary"
            size="mini"
            @click="viewDevice(row.id)"
          >
            <svg-icon name="eye-on" />
          </el-button>
          <el-button
            circle
            type="primary"
            size="mini"
            @click="editDevice(row.id)"
          >
            <i class="el-icon-edit"></i>
          </el-button>
          <el-button
            circle
            v-if="canDelete(row.status)"
            type="danger"
            size="mini"
            @click="deleteDevice(row.id)"
            :disabled="!canDelete(row.status)"
          >
            <i class="el-icon-delete"></i>
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      v-if="totalItems"
      background
      layout="prev, pager, next"
      :total="totalItems"
      :page-size="pageSize"
      class="mt-10"
      :current-page="currentPage"
      @current-change="changeCurrentPage"
    >
    </el-pagination>
    <CreateEditDeviceModal @onSubmit="fetchDevices" :device="currentDevice" />
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'
import { IDevice, EDeviceStatus, IDevicesParams } from '../types'
import {
  deleteDeviceByIdApi,
  getDeviceByIdApi,
  getDevicesApi
} from '@/api/device'
import { DeviceModule } from '@/store/modules/device'
import { AppModule, DeviceType } from '@/store/modules/app'
import CreateEditDeviceModal from './CreateEditDeviceModal.vue'

@Component({
  name: 'Empty',
  components: {
    CreateEditDeviceModal
  },
  filters: {
    deviceStatus: (status: EDeviceStatus) => {
      switch (status) {
        case EDeviceStatus.AVAILABLE:
          return 'Available'
        case EDeviceStatus.USING:
          return 'Using'
        case EDeviceStatus.BROKEN:
          return 'Broken'
        default:
          return ''
      }
    }
  }
})
export default class extends Vue {
  private devices: IDevice[] = []
  readonly pageSize = 10
  private currentPage = 1
  private totalItems = 0
  private currentDevice: IDevice | null = null

  get dialogVisible() {
    return DeviceModule.dialogVisible
  }

  get isMobile() {
    return AppModule.device === DeviceType.Mobile
  }

  @Watch('dialogVisible')
  onCloseDialog(val: boolean) {
    if (!val) {
      this.currentDevice = null
    }
  }

  mounted() {
    this.fetchDevices()
  }

  async fetchDevices(fetch = true) {
    if (!fetch) return
    const params: IDevicesParams = {
      skipCount: (this.currentPage - 1) * 10,
      maxResultCount: this.pageSize
      // searchText: '',
      // filterItems: [],
      // sort: 'desc',
      // sortDirection: 0
    }
    try {
      const { data } = await getDevicesApi(params)
      this.totalItems = data.result.totalCount
      this.devices = data.result.items
    } catch (error) {
      console.error(error)
    }
  }

  getStatusType(status: EDeviceStatus) {
    switch (status) {
      case EDeviceStatus.AVAILABLE:
        return 'success'
      case EDeviceStatus.USING:
        return 'warning'
      case EDeviceStatus.BROKEN:
        return 'danger'
      default:
        return ''
    }
  }

  canDelete(status: EDeviceStatus) {
    switch (status) {
      case EDeviceStatus.AVAILABLE:
      case EDeviceStatus.BROKEN:
        return true
      case EDeviceStatus.USING:
        return false
      default:
        return false
    }
  }

  viewDevice(id: string) {
    this.$router.push(`/device/details/${id}`)
  }

  async editDevice(id: string) {
    try {
      const { data } = await getDeviceByIdApi(id)
      this.currentDevice = data.result
      DeviceModule.setDialogVisible(true)
    } catch (error) {
      console.error(error)
    }
  }

  async deleteDevice(id: string) {
    try {
      await deleteDeviceByIdApi(id)
      this.fetchDevices()
    } catch (error) {
      console.error(error)
    }
  }

  changeCurrentPage(pageNumber: number) {
    this.currentPage = pageNumber
    this.fetchDevices()
  }
}
</script>

<style lang="scss" scoped>
.table-outer {
  background: #fff;
  padding: 15px;
  overflow: auto;
}
.mt-10 {
  margin-top: 10px;
}
</style>
