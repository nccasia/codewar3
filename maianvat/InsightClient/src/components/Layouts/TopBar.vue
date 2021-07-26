<template>
  <div id="top_bar">
    <div class="md-top-bar">
        <ul id="menu_top" class="uk-clearfix">
            <li class="uk-hidden-small">
                <router-link to="/">
                <i class="material-icons">&#xE88A;</i>
               <span> Main</span>
                 </router-link>
            </li>
            <li data-uk-dropdown class="uk-hidden-small">
               <router-link to="dashboard">
                   <i class="material-icons">format_list_numbered</i>
                   <span> Task</span>
                   </router-link>
                <div class="uk-dropdown">
                    <ul class="uk-nav uk-nav-dropdown">
                        <li> <router-link to="dashboard">CCTV</router-link></li>
                        <li> <router-link to="dashboard">Indoor</router-link></li>
                    </ul>
                </div>
            </li>
            <li class="uk-hidden-small">
                <router-link to="community">
                    <i class="material-icons">forum</i> 
                   <span> Community</span>
                    </router-link>
                </li>
            <li class="uk-hidden-small">
                <router-link to="training">
                    <i class="material-icons">school</i> 
                    <span> Training</span>
                    </router-link>
                </li>
            <li class="uk-hidden-small">
                <router-link to="network">
                    <i class="material-icons">wifi_tethering</i> 
                    <span> Network</span>
                    </router-link>
            </li>
        </ul>
    </div>
</div>

</template>

<script>
export default {
  name: 'TopBar'
}
</script>
<style scoped>
#menu_top>li>a>span{
        font-size: 1.5em;
}
.sidebar_main_active #header_main, .sidebar_main_active #page_content, .sidebar_main_active #top_bar {
    margin: 0;
}
#top_bar, #top_bar *, #top_bar :after, #top_bar :before, #top_bar:after, #top_bar:before {
    box-sizing: border-box;
    z-index: 1213;
}
</style>
