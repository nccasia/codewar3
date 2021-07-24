<template>
  <el-dialog :title="dialogTitle" :visible.sync="visible" :width="dialogWidth">
    <el-form ref="form" :model="form" :rules="rules">
      <el-form-item label="Request Type" prop="type">
        <el-select
          v-model="form.type"
          placeholder="Please select a request type"
        >
          <el-option
            v-for="option in requestOptions"
            :key="option.value"
            :label="option.label"
            :value="option.value"
          ></el-option>
        </el-select>
      </el-form-item>

      <template v-if="isAddtionalRequest">
        <el-form-item label="Device Name" prop="deviceName">
          <el-input
            placeholder="Enter device name"
            v-model="form.deviceName"
          ></el-input>
        </el-form-item>

        <el-form-item label="Device Description" prop="description">
          <el-input
            type="textarea"
            placeholder="Enter Device Description"
            v-model="form.description"
          ></el-input>
        </el-form-item>

        <el-form-item label="Reason" prop="reason">
          <el-input
            type="textarea"
            placeholder="Enter Reason"
            v-model="form.reason"
          ></el-input>
        </el-form-item>

        <el-form-item label="Request Priority" prop="priority">
          <el-select
            v-model="form.priority"
            placeholder="Please select a request priority"
          >
            <el-option
              v-for="option in priorityOptions"
              :key="option.value"
              :label="option.label"
              :value="option.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-if="form.type === ERequestType.RETURN">
        <!-- TODO -->
      </template>
    </el-form>

    <span slot="footer" class="dialog-footer">
      <el-button @click="toggleDialogVisible(false)">Cancel</el-button>
      <el-button type="primary" @click="toggleDialogVisible(false)">
        <el-button type="primary" @click="onSubmit">
        Confirm
        </el-button>
      </el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import { Component, Vue, Prop, Emit, Watch } from 'vue-property-decorator'
import {
  ERequestPriority,
  ERequestStatus,
  ERequestType,
  IDeviceRequest,
  REQUEST_PRIORITY_OPTIONS,
  REQUEST_TYPE_OPTIONS
} from '../types'
import { RequestModule } from '@/store/modules/request'
import { createRequestApi } from '@/api/request'
import { Message } from 'element-ui'

const DEFAULT_REQUEST_FORM: IDeviceRequest = {
  id: '',
  type: ERequestType.ADDITION,
  deviceName: '',
  description: '',
  reason: '',
  priority: ERequestPriority.MEDIUM,
  status: ERequestStatus.PENDING,
  deviceId: ''
}

@Component({
  name: 'CreateEditRequest'
})
export default class CreateEditRequest extends Vue {
  @Prop({ required: false }) private request!: IDeviceRequest
  private ERequestType = ERequestType
  private form = { ...DEFAULT_REQUEST_FORM }
  private requestOptions = [...REQUEST_TYPE_OPTIONS]
  private priorityOptions = [...REQUEST_PRIORITY_OPTIONS]

  get visible() {
    return RequestModule.createEditDialogVisible
  }

  set visible(visible: boolean) {
    RequestModule.toggleCreateEditDialogVisible(visible)
  }

  get dialogTitle() {
    if (this.request) {
      return 'Edit Request'
    }
    return 'Create New Request'
  }

  get isAddtionalRequest() {
    return this.form.type === ERequestType.ADDITION
  }

  get dialogWidth() {
    if (window.innerWidth < 576) {
      return '100%'
    }
    if (window.innerWidth < 780) {
      return '80%'
    }
    return '50%'
  }

  mounted() {
    if (this.request && this.request.id !== this.form.id) {
      this.form = { ...this.request }
    }
  }

  beforeUpdate() {
    if (this.request && this.request.id !== this.form.id) {
      this.form = { ...this.request }
    }
  }

  toggleDialogVisible(visible: boolean) {
    RequestModule.toggleCreateEditDialogVisible(visible)
  }

  rules = {
    type: [
      {
        required: true,
        message: 'Request type is required.',
        trigger: 'blur'
      }
    ],
    deviceName: [
      {
        required: this.isAddtionalRequest,
        message: 'Device name is required.',
        trigger: 'blur'
      }
    ],
    reason: [
      {
        required: this.isAddtionalRequest,
        message: 'Reason is required.',
        trigger: 'blur'
      }
    ],
    description: [
      {
        required: this.isAddtionalRequest,
        message: 'Device description is required.',
        trigger: 'blur'
      }
    ],
    priority: [
      {
        required: this.isAddtionalRequest,
        message: 'Priority is required.',
        trigger: 'blur'
      }
    ]
  }

  @Watch('visible')
  onCloseDialog(val: boolean) {
    if (!val) {
      this.form = { ...DEFAULT_REQUEST_FORM }
    }
  }

  @Emit('onSubmit')
  async onSubmit() {
    this.toggleDialogVisible(false)
    try {
      await createRequestApi(this.form)
      return true
    } catch (error) {
      Message.error(error)
      return false
    }
  }
}
</script>

<style lang="sass"></style>
