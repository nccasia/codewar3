<template>
  <QrcodeStream @decode="onDecode" class="qr-code-stream" />
</template>

<script lang="js">
import { Component, Vue } from 'vue-property-decorator'
import { QrcodeStream } from 'vue-qrcode-reader'
// import { Message } from 'element-ui'

@Component({
  name: 'QRCodeReader',
  components: { QrcodeStream }
})
export default class extends Vue {
  onDecode(decodedString) {
    this.$router.push(`/device/details/${decodedString}`)
  }
}
</script>

<style lang="scss" scoped>
.qr-code-stream {
  height: calc(100vh - 50px);
}
</style>
