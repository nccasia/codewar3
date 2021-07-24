<template>
  <!-- secondary sidebar -->
  <div>
    <aside id="sidebar_secondary" class="tabbed_sidebar">
      <ul class="uk-tab uk-tab-icons uk-tab-grid" data-uk-tab="{connect:'#dashboard_sidebar_tabs', animation:'slide-horizontal'}">
        <li class="uk-width-1-3 chat_sidebar_tab">
          <a href="/#">
            <i class="material-icons">&#xE0B7;</i>
          </a>
        </li>
        <li class="uk-width-1-3">
          <a href="/#">
            <i class="material-icons">&#xE8B9;</i>
          </a>
        </li>
      </ul>
      <div class="scrollbar-inner">
        <ul class="md-list md-list-addon list-chatboxes class-list" id="chatboxes" style="padding-left:10px">
          <li v-for="user in usersOnline" :key="user.username" @click="addChatbox(user)">
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
      
    </aside>
      <!-- secondary sidebar end -->
      <div id="chatbox_wrapper" style="z-index:9999">
        <div :id="index" :ref="cb.username" :class="['chatbox', cb.focused? 'cb_active': '']" v-for="(cb, index) in chatbox" :key="index" @click="chatBoxClicked(cb)">
          <div class="chatbox_header">
            <span class="header_name">
              {{cb.fullName}}
            </span>
            <div class="header_actions">
              <div class="actions_dropdown" data-uk-dropdown="{pos:'bottom-right',mode:'click'}">
                <a >
                  <i class="material-icons">&#xE8B9;</i>
                </a>
                <!-- <div class="uk-dropdown uk-dropdown-small">
                  <ul class="uk-nav uk-nav-dropdown">
                    <li>
                      <a href="#">Show full conversation</a>
                    </li>
                    <li>
                      <a href="#">Block messages</a>
                    </li>
                    <li>
                      <a href="#">Report</a>
                    </li>
                  </ul>
                </div> -->
              </div>
              <a href="#" class="chatbox_close" @click.prevent="closeBox(cb)">
                <i class="material-icons">&#xE14C;</i>
              </a>
            </div>
          </div>
          <div class="chatbox_content" v-chat-scroll>
            <div :class="['chatbox_message', msg.senderId===userLogin.staffId?'own':'']" v-for="(msg,index) in cb.messages" :key="index">
                <ul class="chatbox_message_content">
                  <li>
                    <span>
                      {{ msg.message }}
                    </span>
                  </li>
                </ul>
            </div>
          </div>
          <div class="chatbox_footer">
            <input type="text" @keyup.enter="sendMessage(cb)" v-model="cb.writingMessage" placeholder="Write message..." class="message_input">
          </div>
        </div>
        </div>
    </div>
</template>
<script>
import {
  HTTP
} from '@/http-services'
import ChatSidebar from './ChatSidebar'
import APIS from '../../apis-const'
export default {
  name: 'RightSidebar',
  components: { ChatSidebar },
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
    sendMessage(cb, event) {
      if (cb.writingMessage !== '') {
        cb.messages.push({
          message: cb.writingMessage,
          sender: this.userLogin.username,
          receiver: cb.username,
          senderId: this.userLogin.staffId
        })
        this.connection.invoke('sendMessage', {
          receiver: cb.username,
          message: cb.writingMessage,
          senderId: this.userLogin.staffId
        })
        cb.writingMessage = ''
        var tmp = this.chatbox.pop()
        this.chatbox.push(tmp)
      }
    },
    scrollBottom(username) {
      var chatboxContent = this.$el.querySelector('#' + username + ' .chatbox_content')
      chatboxContent.scrollTop = chatboxContent.scrollHeight
    },
    connect () {
      this.connection.on('contacts', data => {
        this.usersOnline = data
      })
      this.connection.on('newUserConnected', data => {
        this.usersOnline.push(data)
      })
      this.connection.on('userDisconnected', data => {
        for (var i = 0; i < this.usersOnline.length; i++) {
          if (this.usersOnline[i].username === data.username) {
            this.usersOnline.splice(i, 1)
            break
          }
        }
      })
      this.connection.on('newMessage', data => {
        var ok = true
        for (var i = 0; i < this.chatbox.length; i++) {
          if (this.chatbox[i].username === data.sender) {
            this.chatbox[i].messages.push(data)
            ok = false
          }
        }
        if (ok) {
          for (var j = 0; j < this.usersOnline.length; j++) {
            if (this.usersOnline[j].username === data.sender) {
              var cb = this.usersOnline[j]
              cb.messages = []
              cb.messages.push(data)
              this.chatbox.push(cb)
              break
            }
          }
        } else {
          var tmp = this.chatbox.pop()
          this.chatbox.push(tmp)
        }
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
        this.chatbox[i].focused = false
        if (this.chatbox[i].username === data.username) {
          this.chatbox[i].focused = true
          ok = false
        }
      }
      if (ok) {
        data.focused = true
        data.messages = []
        this.chatbox.push(data)
        this.getConversation(data)
      } else {
        var tmp = this.chatbox.pop()
        this.chatbox.push(tmp)
      }
    },
    getConversation(data) {
      HTTP.get('/Messages/getConversation?accReceiver=' + data.username)
      .then(function(respone) {
        if (respone.data == null || respone.data.length === 0) {
          data.messages = []
          return
        } else {
          data.messages = respone.data
        }
      })
    },
    closeBox(data) {
      for (var i = 0; i < this.chatbox.length; i++) {
        if (this.chatbox[i].username === data.username) {
          this.chatbox.splice(i, 1)
          break
        }
      }
    },
    chatBoxClicked(data) {
      var ok = false
      for (var i = 0; i < this.chatbox.length; i++) {
        this.chatbox[i].focused = false
        if (this.chatbox[i].username === data.username) {
          this.chatbox[i].focused = true
          ok = true
        }
      }
      if (ok) {
        var tmp = this.chatbox.pop()
        this.chatbox.push(tmp)
      }
    }
  },
  created() {
    this.connection = new this.$signalR.HubConnection(APIS.HOST + '/chathub?token=' + this.auth.accessToken)
    this.connect()
  },
  mounted() {
  }
}
</script>
