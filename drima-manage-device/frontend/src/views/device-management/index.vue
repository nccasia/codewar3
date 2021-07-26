<template>
  <div class="app-container">
    <div class="header">
      <h1 class="page-title">Device Management</h1>
      <el-button type="primary" size="small" @click="toggleDialogVisible(true)">
        <i class="el-icon-circle-plus-outline" />
        <span class="hidden-sm-and-down">Device</span>
      </el-button>
    </div>
    <DeviceTable />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import DeviceTable from './components/DeviceTable.vue'
import { DeviceModule } from '@/store/modules/device'

@Component({
  name: 'DeviceList',
  components: {
    DeviceTable
  }
})
export default class extends Vue {
  private toggleDialogVisible(visible: boolean) {
    DeviceModule.setDialogVisible(visible)
  }
}
</script>

<style lang="scss" scoped>
.app-container {
  padding: 32px;
  background-color: rgb(240, 242, 245);
  position: relative;

  .header {
    display: flex;
    align-items: center;
    margin-bottom: 30px;

    .page-title {
      flex: 1;
    }
  }
}
</style>
