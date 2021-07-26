<template>
  <div class="text-xs-center">
    <v-pagination
      v-model="page"
      :length="totalPages()"
      @input="onChangePaging"
    ></v-pagination>
  </div>
</template>
<script>
import TheLoaderVue from './TheLoader.vue'
export default {
  name: 'PaginationPanel',
  $_veeValidate: {
    validator: 'new'
  },
  data: function(){
    return{
      page: 1
    }
  },
  computed:{

  },
  props: {  
    rows: {
      type: Number,
      required: false,
      default: 0
    },
    perPage: {
      type: Number,
      required: false,
      default: 10
    },
    currentPage: {
      type: Number,
      required: false,
      default: 1
    }
  },
  mounted(){
    this.page = this.$props.currentPage
  },
  watch: {
    currentPage (val, oldVal) {
      if (val !== oldVal) {
        this.page = val
      }
    }
  },
  methods: {
    onChangePaging(current) {
      this.$emit('onChange', current)
    },
    totalPages(){
      return this.$props.rows > 0 ? Math.ceil(this.$props.rows/this.$props.perPage) : 1
    }
  }
}
</script>

