<template>
    <v-snackbar
        :timeout="timeout"
        :top="y === 'top'"
        :bottom="y === 'bottom'"
        :right="x === 'right'"
        :left="x === 'left'"
        :multi-line="mode === 'multi-line'"
        :vertical="mode === 'vertical'"
        v-model="snackbar"
        :color="color"
    >
        {{ text }}
        <v-btn dark flat fab @click.native="snackbar = false">
          <v-icon small>close</v-icon>
        </v-btn>
    </v-snackbar>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({
  data () {
    return {
      snackbar: false,
      y: 'top',
      x: null,
      mode: '',
      timeout: 3000,
      text: '',
      color: 'success'
    }
  },
  methods: {
    show (params: any) {
      if (this.snackbar) {
        this.snackbar = false
      }
      this.text = params.text
      if (params.color !== undefined) {
        this.color = params.color
      }
      this.snackbar = true
    },
    hide () {
      this.snackbar = false
    }
  }
})
</script>
