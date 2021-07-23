<template>
  <div class="login_page_wrapper">
    <div class="md-card" id="login_card">
      <div class="md-card-content large-padding" id="login_form">
        <p style="font-size: 24px;">404 Page not found</p>
        <p style="font-size: 16px;">The requested URL /some_url was not found on this server.</p>
      </div>
    </div>
  </div>
</template>
<script>
  export default {
    name: 'ErrorPage'
  }
</script>
<style lang="css" scoped>
  body {
      font-size: 15px;
      padding: 64px 24px;
      overflow-y: auto;
      height: auto;
      min-height: 100%;
  }
  .login_page_wrapper {
      width: 360px;
      max-width: 100%;
      margin: 0 auto;
      -webkit-transition: all 280ms cubic-bezier(.4,0,.2,1);
      transition: all 280ms cubic-bezier(.4,0,.2,1);
  }
  .md-input-container input, .md-input-container textarea {
    height: auto;
  }
  #page_content{
    margin-right:0
  }
</style>
