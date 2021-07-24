<template>
  <el-dialog :title="dialogTitle" :visible.sync="visible" :width="dialogWidth">
    <DeviceForm v-model="form" />

    <span slot="footer" class="dialog-footer">
      <el-button @click="toggleDialogVisible(false)">Cancel</el-button>
      <el-button type="primary" @click="onSubmit">
        Confirm
      </el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import { Component, Emit, Prop, Vue, Watch } from 'vue-property-decorator'
import DeviceForm from './DeviceForm.vue'
import { DeviceModule } from '@/store/modules/device'
import { DEFAULT_FORM, IDevice } from '../types'
import { Message } from 'element-ui'
import { createDeviceApi } from '@/api/device'

@Component({
  name: 'CreateEditDeviceModal',
  components: {
    DeviceForm
  }
})
export default class extends Vue {
  @Prop() private device!: IDevice
  get visible() {
    return DeviceModule.dialogVisible
  }

  set visible(visible: boolean) {
    DeviceModule.setDialogVisible(visible)
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

  dialogTitle = this.device ? 'Edit Device' : 'Import New Device'
  form = { ...DEFAULT_FORM }

  mounted() {
    if (this.device && this.device.id !== this.form.id) {
      this.form = { ...this.device }
    }
  }

  beforeUpdate() {
    if (this.device && this.device.id !== this.form.id) {
      this.form = { ...this.device }
    }
  }

  @Watch('visible')
  onCloseDialog(val: boolean) {
    if (!val) {
      this.form = { ...DEFAULT_FORM }
    }
  }

  toggleDialogVisible(visible: boolean) {
    DeviceModule.setDialogVisible(visible)
  }

  @Emit('onSubmit')
  async onSubmit() {
    this.toggleDialogVisible(false)
    try {
      await createDeviceApi(this.form)
      return true
    } catch (error) {
      Message.error(error)
      return false
    }
  }
}
</script>

<style lang="scss" scoped></style>
