<template>
  <v-progress-circular
    v-if="loading"
    v-bind:class="[loading ? 'loading' : 'loading d-hide']"
    indeterminate
    :size="size"
    :width="width"
  ></v-progress-circular>
</template>

<script>
  export default {
    $_veeValidate: {
      validator: 'new'
    },
    name: 'theLoader',
    props: {
      loading: false,
      size: 50,
      width: 4,
    }
  }

</script>
<style lang="css" scoped>
  .loading{
    height: 70px;
    position: absolute;
    width: 70px;
    left: 50%;
    top: 50%;
    transform: translateX(-50%) translateY(-50%);
    z-index: 999;
    color: #cd5c5c;
  }
</style>
