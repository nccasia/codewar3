<template>
  <div class="dashboard-container">
    <el-button
      type="primary"
      @click="goToDecoder"
    >Decode Device</el-button>
  </div>
</template>

<script lang="ts">
import 'echarts/theme/macarons.js' // Theme used in BarChart, LineChart, PieChart and RadarChart
import { Component, Vue } from 'vue-property-decorator'

@Component({
  name: 'DashboardAdmin',
  components: {}
})
export default class extends Vue {
  goToDecoder() {
    this.$router.push('/device/decode')
  }
}
</script>

<style lang="scss" scoped>
.dashboard-container {
  flex: 1;
  background-color: rgb(240, 242, 245);
  justify-content: center;
  align-items: center;
}
</style>
