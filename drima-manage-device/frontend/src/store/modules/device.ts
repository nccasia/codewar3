import { IDevice } from './../../views/device-management/types'
import {
  VuexModule,
  Module,
  Mutation,
  Action,
  getModule
} from 'vuex-module-decorators'
import store from '@/store'

export interface IDeviceState {
  currentDevice: IDevice | null
  dialogVisible: boolean
}

@Module({ dynamic: true, store, name: 'device' })
class Device extends VuexModule implements IDeviceState {
  currentDevice: IDevice | null = null
  dialogVisible = false

  @Mutation
  private UPDATE_CURRENT_DEVICE(device: IDevice) {
    this.currentDevice = device
  }

  @Action
  public setCurrentDevice(device: IDevice) {
    this.UPDATE_CURRENT_DEVICE(device)
  }

  @Mutation
  private UPDATE_DIALOG_VISIBLE(visible: boolean) {
    this.dialogVisible = visible
  }

  @Action
  public setDialogVisible(visible: boolean) {
    this.UPDATE_DIALOG_VISIBLE(visible)
  }
}

export const DeviceModule = getModule(Device)
