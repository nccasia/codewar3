<template>
    <v-menu ref="menu"
            :close-on-content-click="false"
            v-model="menu"
            :nudge-right="40"
            :return-value.sync="time"
            lazy
            transition="scale-transition"
            offset-y
            full-width
            max-width="290px"
            min-width="290px"
            :disabled="disabled">
        <v-text-field slot="activator"
                      v-model="time"
                      :label="label"
                      :append-icon="appendIcon"
                      :prepend-icon="prependIcon"
                      readonly
                      :clearable="clearable"
                      @input="input"
                      :error-messages="errorMessages"
                      :hide-details="hideDetails"></v-text-field>
        <v-time-picker v-if="menu"
                       v-model="time"
                       full-width
                       format="24hr"
                      placeholder="Chọn giờ"
                       @change="$refs.menu.save(time)"
                       :min="valueMin"
                       :max="valueMax"></v-time-picker>
    </v-menu>
</template>
<script lang="ts">
import { Vue } from 'vue-property-decorator'
export default Vue.extend({
  components: {},
  props: {
    value: {
      type: [String, Date],
      default: null
    },
    max: {
      type: [String, Date],
      default: null
    },
    min: {
      type: [String, Date],
      default: null
    },
    clearable: {
      type: Boolean,
      default: false
    },
    formatInput: {
      type: String,
      default: 'hh:mm'
    },
    errorMessages: {
      default: null
    },
    appendIcon: {
      type: String,
      default: null
    },
    prependIcon: {
      type: String,
      default: null
    },
    disabled: {
      type: Boolean,
      default: false
    },
    hideDetails: {
      type: Boolean,
      default: false
    },
    label: {
      type: String,
      default: ''
    }
  },
  data () {
    return {
      menu: false,
      time: '',
      valueMax: undefined,
      valueMin: undefined
    }
  },
  computed: {
  },
  watch: {
    time (val, oldVal) {
      this.$emit('input', val)
    }
  },
  created () {
    if (this.value != null && this.value !== undefined) {
      this.time = this.$moment(this.value, this.formatInput).format('k:mm')
    }
    if (this.max != null) {
      this.valueMax = this.$moment(this.max, this.formatInput).format('k:mm')
    }
    if (this.min != null) {
      this.valueMin = this.$moment(this.min, this.formatInput).format('k:mm')
    }
  },
  methods: {
    input (val: any) {
      this.$emit('input', val)
    }
  }
})
</script>
