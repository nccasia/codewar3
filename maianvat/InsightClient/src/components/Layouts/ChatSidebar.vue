<template>
  <div class="scrollbar-inner">
        <ul class="md-list md-list-addon list-chatboxes class-list" id="chatboxes" style="padding-left:10px">
          <li v-for="user in usersOnline" :key="user.username" @click="addChatbox(user.username)">
              <div class="md-list-addon-element">
                  <span class="element-status element-status-success"></span>
                  <img class="md-user-image md-list-addon-avatar" :src="user.photo + '?' + new Date().getTime()" alt="Photo">
              </div>
              <div class="md-list-content">
                  <span class="md-list-heading">
                     {{user.fullName}}
                  </span>
                  <span class="uk-text-small uk-text-muted uk-text-truncate">
              {{user.username}}
              </span>
              </div>
          </li>
        </ul>
      </div>
</template>
<script>
import {
  HTTP
} from '@/http-services'
// import APIS from '../../apis-const'
export default {
  name: 'ChatSidebar',
  data() {
    return {
      connection: null,
      messages: [],
      auth: this.$store.state.auth,
      userLogin: this.$store.state.user,
      usersOnline: [],
      connectionId: null,
      chatbox: []
    }
  },
  methods: {
    sendMessage(sendTo, message) {
      this.connection.invoke('sendMessage', {
        receiver: sendTo,
        message: message
      })
    },
    connect () {
      this.connection.on('newUserConnected', data => {
        this.usersOnline.push(data)
      })
      this.connection.start()
      .then(() => this.connectAPI())
    },
    connectAPI () {
      HTTP.get('/Messages/connect', {
        params: {
          connectionId: this.connection.connection.connectionId
        }
      })
        .then(response => {
          // this.usersOnline = response.data.usersOnline
        })
        .catch(e => {
          this.errors.push(e)
        })
    },
    addChatbox(data) {
      var ok = true
      for (var i = 0; i < this.chatbox.length; i++) {
        if (this.chatbox[i] === data) {
          ok = false
          break
        }
      }
      if (ok) {
        this.chatbox.push(data)
      }
    },
    closeBox(data) {
      for (var i = 0; i < this.chatbox.length; i++) {
        if (this.chatbox[i] === data) {
          this.chatbox.splice(i, 1)
          break
        }
      }
    }
  },
  created() {
    this.connection = new this.$signalR.HubConnection('http://localhost:58493/chathub?token=' + this.auth.accessToken)
    this.connect()
  },
  mounted() {
  }
}
</script>
