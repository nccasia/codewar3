<template>
  <div>
    <el-table :data="requests">
      <el-table-column label="Priority" width="100" align="left">
        <template slot-scope="{row}">
          <el-tag :type="getPriorityType(row.priority)" size="mini">
            {{ row.priority | requestPriorityFilter }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="Device Name" min-width="180" align="left">
        <template slot-scope="{row}">
          {{ row.deviceName }}
        </template>
      </el-table-column>
      <el-table-column label="Description" min-width="200" align="left">
        <template slot-scope="{row}">
          {{ row.description }}
        </template>
      </el-table-column>
      <el-table-column label="Reason" min-width="200" align="left">
        <template slot-scope="{row}">
          {{ row.reason }}
        </template>
      </el-table-column>
      <el-table-column label="Status" width="200" align="left">
        <template slot-scope="{row}">
          <el-tag :type="getStatusType(row.status)" size="mini">
            {{ row.status | requestStatusFilter }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="Actions" width="200" align="left">
        <template slot-scope="{row}">
          <el-button
            type="primary"
            size="mini"
            @click="editRequest(row.id)"
            :disabled="!canEdit(row.status)"
          >
            Edit
          </el-button>
          <el-button
            v-if="canEdit(row.status)"
            type="danger"
            size="mini"
            @click="deleteRequest(row.id)"
            :disabled="!canEdit(row.status)"
          >
            Delete
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
import { Component, Vue, Prop, Emit } from 'vue-property-decorator'
import { ERequestPriority, ERequestStatus, IDeviceRequest } from '../types'
import { requestStatusFilter, requestPriorityFilter } from '../filters'

@Component({
  name: 'AdditionRequestTable',
  filters: { requestStatusFilter, requestPriorityFilter }
})
export default class AdditionRequestTable extends Vue {
  @Prop({ required: true }) private requests!: IDeviceRequest[]

  getPriorityType(priority: ERequestPriority) {
    switch (priority) {
      case ERequestPriority.LOW:
        return 'primary'
      case ERequestPriority.MEDIUM:
        return 'warning'
      case ERequestPriority.CRITICAL:
        return 'danger'
      default:
        return 'prmary'
    }
  }

  getStatusType(status: ERequestStatus) {
    switch (status) {
      case ERequestStatus.APPROVED:
        return 'success'
      case ERequestStatus.DELIVERED:
        return 'info'
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

  canEdit(status: ERequestStatus) {
    return status === ERequestStatus.PENDING
  }

  @Emit('onEditRequest')
  editRequest(id: string) {
    return id
  }

  @Emit('onDeleteRequest')
  deleteRequest(id: string) {
    return id
  }
}
</script>

<style lang="scss" scoped>
.mt-10 {
  margin-top: 10px;
}
</style>
