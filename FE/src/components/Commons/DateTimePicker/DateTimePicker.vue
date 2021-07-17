<template>
    <v-layout wrap style="position:relative;">
        <v-flex xs12 style="position:absolute; top:-8px;color:#666">
            {{label}}
        </v-flex>
        <v-flex xs7>
            <v-text-field :value="dateString"
                          single-line
                          mask="date"
                          :prepend-icon="prependIcon"
                          :append-icon="appendIcon"
                          :clearable="clearable"
                          :hide-details="hideDetails"
                          placeholder="dd/mm/yyyy"
                          :error-messages="errorMessages"
                          @click:append="showMenuDate"
                          :disabled="disabled"
                          @input="dateInput"></v-text-field>
                <v-dialog ref="dialogDate"
                          v-model="menuDate"
                          :return-value.sync="selectedDate"
                          lazy
                          full-width
                          width="290px">
                    <v-date-picker
                                   v-model="selectedDate"
                                   :max="valueMax" locale="vi-vn"
                                   :min="valueMin">
                        <v-spacer></v-spacer>
                        <v-btn flat color="primary" @click="menuDate = false">Hủy</v-btn>
                        <v-btn flat color="primary" @click="$refs.dialogDate.save(selectedDate); saveDate(selectedDate)">OK</v-btn>
                    </v-date-picker>
            </v-dialog>
        </v-flex>
        <v-flex xs5>
            <v-text-field
                          :value="timeString"
                          mask="time"
                          single-line
                          append-icon="access_time"
                          :clearable="clearable"
                          :error-messages="errorMessages"
                          placeholder="hh:mm"
                          :hide-details="hideDetails"
                          @click:append="showMenuTime"
                          :disabled="disabled"
                          @input="timeInput"></v-text-field>
                <v-dialog ref="dialogTime"
                          v-model="menuTime"
                          :return-value.sync="selectedTime"
                          lazy
                          full-width
                          width="290px">
                    <v-time-picker v-if="menuTime"
                                   v-model="selectedTime"
                                   full-width
                                   format="24hr"
                                   locale="vi-vn">
                        <v-spacer></v-spacer>
                        <v-btn flat color="primary" @click="menuTime = false">Hủy</v-btn>
                        <v-btn flat color="primary" @click="$refs.dialogTime.save(selectedTime); saveTime(selectedTime)">OK</v-btn>
                    </v-time-picker>
            </v-dialog>
        </v-flex>
    </v-layout>
</template>
<script lang="ts">
import { Vue } from 'vue-property-decorator'
export default Vue.extend({
  props: {
    value: {
      type: [Date, String],
      default: null
    },
    label: {
      type: String,
      default: ''
    },
    clearable: {
      type: Boolean,
      default: false
    },
    format: {
      type: String,
      default: 'DD/MM/YYYY'
    },
    formatInput: {
      type: String,
      default: 'YYYY-MM-DD'
    },
    max: {
      type: [Date, String],
      default: null
    },
    min: {
      type: [Date, String],
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
    appendIcon: {
      type: String,
      default: 'event'
    },
    prependIcon: {
      type: String,
      default: null
    },
    errorMessages: {
      default: null
    }
  },
  data () {
    return {
      menuDate: false,
      menuTime: false,
      selectedDate: null,
      selectedTime: null,
      valueMax: null,
      valueMin: null,
      x: 0,
      y: 0
    }
  },
  computed: {
    dateTime (): any {
      if (this.selectedTime !== undefined && this.selectedTime != null &&
                    this.selectedDate !== undefined && this.selectedDate != null) {
        return this.$moment(this.selectedDate + 'T' + this.selectedTime).format()
      }
      if (this.selectedDate !== undefined && this.selectedDate != null) {
        return this.$moment(this.selectedDate).format()
      } else {
        return null
      }
    },
    timeString (): any {
      if (this.dateTime != null && this.dateTime !== undefined) { return this.$moment(this.dateTime).format('HH:mm') }
      return null
    },
    dateString (): any {
      if (this.dateTime != null && this.dateTime !== undefined) { return this.$moment(this.dateTime).format('DD/MM/YYYY') }
      return null
    }
  },
  methods: {
    dateInput (val: any) {
      if (val.length === 8 && this.$moment(val, 'DDMMYYYY').isValid()) {
        this.selectedDate = this.$moment(val, 'DDMMYYYY').format('YYYY-MM-DD')
        if (this.max !== null && this.$moment(this.dateTime) > this.$moment(this.max)) {
          this.selectedDate = this.$moment(this.max).format('YYYY-MM-DD')
          this.selectedTime = this.$moment(this.max).format('HH:mm')
        } else {
          this.$emit('input', this.dateTime)
        }
      }
    },
    timeInput (val: any) {
      if (val.length === 4 && this.$moment(val, 'HHmm').isValid()) {
        this.selectedTime = this.$moment(val, 'HHmm').format('HH:mm')
        if (this.max !== null && this.$moment(this.dateTime) > this.$moment(this.max)) {
          this.selectedDate = this.$moment(this.max).format('YYYY-MM-DD')
          this.selectedTime = this.$moment(this.max).format('HH:mm')
        } else {
          this.$emit('input', this.dateTime)
        }
      }
    },
    showMenuDate () {
      this.menuDate = true
    },
    showMenuTime () {
      this.menuTime = true
    },
    saveDate (date: any) {
      this.$emit('input', this.dateTime)
      this.menuDate = false
    },
    saveTime (time: any) {
      this.$emit('input', this.dateTime)
      this.menuTime = false
    },
    inputDate (val: any) {
      this.$emit('input', this.dateTime)
      this.menuDate = false
    },
    inputTime (val: any) {
      this.$emit('input', this.dateTime)
    }
  },
  watch: {
    // selectedDatetime(val) {
    //    this.dateString = this.$moment(val).format(this.format)
    // },
    max (val) {
      this.valueMax = this.max === null ? null : this.$moment(this.max, this.formatInput).format('YYYY-MM-DD')
    },
    min (val) {
      this.valueMin = this.min === null ? null : this.$moment(this.min, this.formatInput).format('YYYY-MM-DD')
    },
    value (val) {
      if (val != null && val !== undefined && val !== this.dateTime) {
        var date = val
        this.selectedDate = this.$moment(date).format('YYYY-MM-DD')
        this.selectedTime = this.$moment(date).format('HH:mm')
      } else if (val !== this.dateTime) {
        this.selectedDate = null
        this.selectedTime = null
      }
    }
  },
  mounted () {
    let val = this.value
    if (val != null && val !== undefined && val !== this.dateTime) {
      var date = val
      this.selectedDate = this.$moment(date).format('YYYY-MM-DD')
      this.selectedTime = this.$moment(date).format('HH:mm')
    } else if (val !== this.dateTime) {
      this.selectedDate = null
      this.selectedTime = null
    }
  }
})
</script>
