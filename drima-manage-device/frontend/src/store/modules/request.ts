import {
  VuexModule,
  Module,
  Mutation,
  Action,
  getModule
} from 'vuex-module-decorators'
import store from '@/store'

export interface IRequestState {
  createEditDialogVisible: boolean
}

@Module({ dynamic: true, store, name: 'request' })
class Request extends VuexModule implements IRequestState {
  createEditDialogVisible = false

  @Mutation
  private TOGGLE_DIALOG_VISIBLE(visible: boolean) {
    this.createEditDialogVisible = visible
  }

  @Action
  public toggleCreateEditDialogVisible(visible: boolean) {
    this.TOGGLE_DIALOG_VISIBLE(visible)
  }
}

export const RequestModule = getModule(Request)
