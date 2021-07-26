<template>
    <v-dialog v-model="dialog" scrollable width="500px" persistent>
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">{{ isUpdate ? 'Cập nhật quyền người dùng' : 'Thêm mới quyền người dùng'}}</span>
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
                    label="Tên nhóm quyền"
                    v-model="data.Name"
                    data-vv-name="Tên nhóm quyền"
                    data-vv-scope="formEdit"
                    v-validate="{required: true}"
                    :error-messages="errors.collect('Tên nhóm quyền')"
                    required
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12>
                  <v-text-field
                    v-model="data.Description"
                    label="Mô tả"
                    hide-details
                  >
                  </v-text-field>
                </v-flex>
              </v-layout>
               <v-layout>
                <v-flex xs12>
                   <v-select
                    v-model="select"
                    :items="permissions"
                    attach
                    chips
                    label="Chức năng"
                    multiple
                  ></v-select>
                </v-flex>
              </v-layout>
               <v-layout>
                <v-flex xs12>
                  <v-checkbox
                    v-model="data.Status"
                    label="Kích hoạt"
                    auto-grow
                    rows="4"
                  >
                  </v-checkbox>
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

<script>
import Vue from 'vue'
import GroupApi from '../../apiResources/GroupApi'

export default {
  $_veeValidate: {
    validator: 'new'
  },
  name: 'ModalInsertOrUpdateUserAuthorization',
  data () {
    return {
      select: ["Cho phép người dùng phê duyệt đơn đăng ký công việc",
      "Cho phép người dùng xem đơn gửi kiểm tra hoàn thành công việc", 
      "Cho phép người dùng sử dụng toàn bộ chức năng kiểm tra hoàn thành công việc"],
      permissions: [],
      saving: false,
      dialog: false,
      data: {},
      isUpdate: false,
      loadingModal: false,
      loadingGroup: false,
      listCategoryGroups: []
    }
  },
  computed: {
  },
  methods: {
    hide () {
      this.dialog = false
      this.permissions = []
    },
    getData (GroupK) {
      this.loadingModal = true
      GroupApi.detail(GroupK)
        .then(res => {
          this.data = res
          this.loadingModal = false
        })
        .catch(res => {
          this.$notify({ text: 'Lấy dữ liệu thất bại', color: 'error' })
        })
        this.getAllPermission()
    },
    getAllPermission () {
      GroupApi.getPermissions()
        .then(res => {
          res.forEach(element => {
            this.permissions.push(element.Description);   
          });
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
    show (GroupK, isUpdate) {
      Object.assign(this.$data, (this.$options).data.apply(this))
      if (isUpdate) {
        this.getData(GroupK)
      } else {
        this.select = []
        this.getAllPermission()
      }
      this.isUpdate = isUpdate
      this.dialog = true
    }
  }
}
</script>
