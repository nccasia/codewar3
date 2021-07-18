<template>
    <v-dialog v-model="dialog" scrollable width="500px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật loại công việc' : 'Thêm mới loại công việc'}}</span>
          <v-spacer></v-spacer>
          <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
        </v-card-title>
        <v-card-text>
          <v-container v-if="loadingModal">
            <div class="text-xs-center">
              <v-progress-circular
                indeterminate
                color="primary"
                :size="70"
                :width=6
              ></v-progress-circular>
            </div>
          </v-container>
         <form scope="formEdit" v-else>
            <v-container grid-list-md pa-0>
               <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                  label="Tên loại công việc"
                  v-model="data.Name"
                  data-vv-name="Tên loại công việc"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Tên loại công việc')"
                  required
                  ></v-text-field>
                </v-flex>
              </v-layout>

               <v-layout wrap>
                <v-flex xs12>
                  <v-text-field
                  type="number"
                  label="Thời gian mặc định(giờ)"
                  v-model="data.DefaultTimeInHours"
                  data-vv-name="Thời gian mặc định"
                  data-vv-scope="formEdit"
                  v-validate="{required: true}"
                  :error-messages="errors.collect('Thời gian mặc định')"
                  required
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
                  <v-text-field
                    v-model="data.ColorCode"
                    label="Mã màu"
                    auto-grow
                    rows="4"
                    hide-details
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
                  <v-textarea
                    v-model="data.Description"
                    label="Mô tả"
                    auto-grow
                    rows="4"
                    hide-details
                  >
                  </v-textarea>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
                </v-flex>
              </v-layout>
            </v-container>
          </form>
        </v-card-text>
        <v-card-actions v-if="!loadingModal">
          <v-spacer></v-spacer>
          <v-btn flat @click.native="hide">Hủy</v-btn>
          <v-btn color="blue darken-1" :loading="saving" :disabled="saving" flat @click.native="save">Lưu</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  HTTP
} from '../../HTTPServices'
import { JobType } from '@/models/JobType'
import JobTypeApi from '@/apiResources/JobTypeApi'

export default Vue.extend({
  name: 'ModalInsertOrUpdateJobType',
  $_veeValidate: {
    validator: 'new'
  },
  data () {
    return {
      saving: false,
      dialog: false,
      data: {} as JobType,
      isUpdate: false,
      loadingModal: false
    }
  },
  computed: {
  },
  methods: {
    hide () {
      this.dialog = false
    },
    getData (JobTypeK: any) {
      this.loadingModal = true
      JobTypeApi.detail(JobTypeK)
        .then(res => {
          this.data = res
          this.loadingModal = false
        })
        .catch(res => {
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
    },
    save () {
      this.saving = true
      this.$validator.validateAll('formEdit').then((res) => {
        if (res) {
          this.$emit('save', this.data, this.isUpdate)
        } else {
          this.saving = false
          this.$notify({
            text: 'Cần điền đủ thông tin',
            color: 'error'
          })
        }
      })
    },
    show (JobTypeK: any, isUpdate: boolean) {
      Object.assign(this.$data, (this.$options as any).data.apply(this))
      if (isUpdate) {
        this.getData(JobTypeK)
      } else {
      }
      this.isUpdate = isUpdate
      this.dialog = true
    }
  }
})
</script>
