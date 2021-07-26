<template>
  <div>
    <el-table :data="requests">
      <el-table-column label="Device Name" min-width="200" align="left">
        <template slot-scope="{row}">
          {{ row.deviceName }}
        </template>
      </el-table-column>
      <el-table-column label="Status" width="200" align="left">
        <template slot-scope="{row}">
          <el-tag :type="getStatusType(row.status)" size="mini">
            {{ row.status | requestStatusFilter }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="Action" width="100" align="center">
        <template slot-scope="{row}">
          <el-button
            type="danger"
            size="mini"
            @click="cancelRequest(row.id)"
            :disabled="!canDelete(row.status)"
          >
            Cancel
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      background
      layout="prev, pager, next"
      :total="1000"
      class="mt-10"
    >
    </el-pagination>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { ERequestStatus, IDeviceRequest } from '../types'
import { requestStatusFilter } from '../filters'

@Component({
  name: 'ReturnRequestTable',
  filters: { requestStatusFilter }
})
export default class ReturnRequestTable extends Vue {
  @Prop({ required: true }) private requests!: IDeviceRequest[]

  getStatusType(status: ERequestStatus) {
    switch (status) {
      case ERequestStatus.APPROVED:
        return 'success'
      case ERequestStatus.DELIVERED:
        return ''
      case ERequestStatus.PENDING:
        return 'warning'
      case ERequestStatus.REJECTED:
        return 'danger'
      case ERequestStatus.RETURNED:
        return 'info'
      default:
        return ''
    }
  }

  canDelete(status: ERequestStatus) {
    return status === ERequestStatus.PENDING
  }

  cancelRequest(id: string) {
    console.log(
      '🚀 ~ file: ReturnRequestTable.vue ~ line 38 ~ ReturnRequestTable ~ editRequest ~ id',
      id
    )
  }
}
</script>

<style lang="scss" scoped>
.mt-10 {
  margin-top: 10px;
}
</style>
