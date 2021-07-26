<template>
  <div class="app-container">
    <el-row type="flex" justify="center">
      <el-col :xs="24" :sm="20" :md="20" :lg="12">
        <el-card>
          <div class="header" slot="header">
            <h1>{{ currentDevice.name }}</h1>
            <el-button
              v-if="isPreview"
              type="primary"
              @click="isPreview = false"
              >Update</el-button
            >
          </div>
          <DeviceForm v-model="currentDevice" :isPreview="isPreview" />
          <div class="footer">
            <el-button v-if="!isPreview" type="default" @click="onCancel">
              Cancel
            </el-button>
            <el-button v-if="!isPreview" type="primary" @click="onSubmit">
              Submit
            </el-button>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts">
import { createDeviceApi, getDeviceByIdApi } from '@/api/device'
import { Component, Vue } from 'vue-property-decorator'
import { DEFAULT_FORM, IDevice } from '../types'
import DeviceForm from '../components/DeviceForm.vue'

@Component({
  name: 'DeviceDetails',
  components: {
    DeviceForm
  }
})
export default class extends Vue {
  private isPreview = true
  private currentDevice: IDevice = DEFAULT_FORM
  private originalDevice: IDevice = DEFAULT_FORM

  async mounted() {
    const id = this.$route.params && this.$route.params.id
    try {
      const { data } = await getDeviceByIdApi(id)
      this.currentDevice = data.result
      this.originalDevice = { ...data.result }
    } catch (error) {}
  }

  onCancel() {
    this.isPreview = true
    this.currentDevice = { ...this.originalDevice }
  }

  async onSubmit() {
    try {
      await createDeviceApi(this.currentDevice)
      this.isPreview = true
    } catch (error) {}
  }
}
</script>

<style lang="scss" scoped>
.header {
  display: flex;
  align-items: center;

  h1 {
    margin-right: auto;
  }
}

.footer {
  display: flex;
  justify-content: space-between;
  margin-top: 40px;
}
</style>
