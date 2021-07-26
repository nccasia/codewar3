<template>
    <v-dialog v-model="dialog" max-width="350">
      <v-card>
        <v-card-title class="primary white--text">
          <span class="title">Xác nhận xóa</span>
          <v-spacer></v-spacer>
          <v-btn class="white--text ma-0" small icon @click="hide"><v-icon>close</v-icon></v-btn>
        </v-card-title>
        <v-card-text class="py-3">Bạn có chắc chắn muốn xóa bản ghi này?</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn @click.native="hide" small flat>Hủy</v-btn>
          <v-btn color="primary" @click.native="xoa" small flat>Xác Nhận</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
</template>

<script>
export default {
  name: 'ModalXoa',
  data () {
    return {
      dialog: false,
      nameArr: ''
    }
  },
  methods: {
    show (nameArr) {
      this.dialog = true
      this.nameArr = nameArr
    },
    xoa () {
      this.$emit('xoa', this.nameArr)
    },
    hide () {
      this.dialog = false
    }
  }
}
</script>
