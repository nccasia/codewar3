<template>
  <el-form ref="form" :model="form" :rules="rules" :disabled="isPreview">
    <el-form-item label="Device name" prop="name">
      <el-input v-model="form.name"></el-input>
    </el-form-item>
    <el-form-item label="Device description" prop="description">
      <el-input v-model="form.description"></el-input>
    </el-form-item>
    <el-form-item label="Manufacture" prop="manufacturer">
      <el-input v-model="form.manufacturer"></el-input>
    </el-form-item>

    <el-form-item label="Status" prop="status">
      <el-select v-model="form.status" placeholder="Please select status">
        <el-option
          v-for="option in deviceStatusOptions"
          :key="option.value"
          :label="option.label"
          :value="option.value"
        ></el-option>
      </el-select>
    </el-form-item>

    <el-form-item label="Guarantee Date" prop="guaranteeDate">
      <el-date-picker
        type="date"
        placeholder="Pick a date"
        v-model="form.guaranteeDate"
        style="width: 100%;"
      ></el-date-picker>
    </el-form-item>
  </el-form>
</template>

<script lang="ts">
import { Component, Prop, VModel, Vue } from 'vue-property-decorator'
import { DEVICE_STATUS_OPTIONS, IDevice } from '../types'

@Component({
  name: 'DeviceForm'
})
export default class extends Vue {
  @Prop() private isPreview!: boolean
  @VModel() private form!: IDevice
  deviceStatusOptions = DEVICE_STATUS_OPTIONS

  rules = {
    name: [
      {
        required: true,
        message: 'Device Name is required.',
        trigger: 'blur'
      }
    ],
    description: [
      {
        required: true,
        message: 'Device description is required.',
        trigger: 'blur'
      }
    ],
    manufacturer: [
      {
        required: true,
        message: 'Device manufacturer is required.',
        trigger: 'blur'
      }
    ],
    status: [
      {
        required: true,
        message: 'Device status is required.',
        trigger: 'blur'
      }
    ],
    guaranteeDate: [
      {
        required: true,
        message: 'Guarantee Date is required.',
        trigger: 'blur'
      }
    ]
  }
}
</script>

<style lang="scss" scoped></style>
