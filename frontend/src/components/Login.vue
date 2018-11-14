<template>
<div class="login-page">
    <div class="login-header">
        <h1>黄河电器公司进销存管理系统</h1>
    </div>
    <div class="container">
            <b-alert :show="dismissCountDown" dismissible fade variant="success" @dismissed="dismissCountDown=0" @dismiss-count-down="val=>this.dismissCountDown=val">
                {{message}}
            </b-alert>
            <b-alert :show="errorCountDown" dismissible fade variant="danger" @dismissed="errorCountDown=0" @dismiss-count-down="val=>this.errorCountDown=val">
                {{message}}
            </b-alert>
        <div class="row">

            <div class="col-xs-1"><label for="UILoginName" class="control-label">登录名</label></div>
            <div class="col-xs-3">
                <input type="text" @keydown.enter="login" class="form-control" id="UILoginName" v-model="user.UILoginName" placeholder="请输入登录名"/>
            </div>
                <div class="col-xs-1"><label for="UICode" class="control-label">密码</label></div>
                <div class="col-xs-3">
                    <input type="password" @keydown.enter="login" class="form-control"  id="UICode" v-model="user.UICode"/>
            </div>
                    <div class="col-xs-1"><button class="btn btn-success" @click="login">登录</button></div>
                </div>
            </div>
        </div>
        
</template>

<script>
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import { mapState, mapMutations, mapActions } from "vuex";

export default {
  name: "Login",
  computed: {
    ...mapState(["user"])
  },
  data() {
    return {
      dismissSecs: 5,
      errorSecs: 5,
      dismissCountDown: 0,
      errorCountDown: 0,
      showDismissibleAlert: false,
      message: "请初始化系统消息或使用success方法"
    };
  },
  methods: {
    error(message) {
      this.message = message;
      this.errorCountDown = this.errorSecs;
    },
    success(message) {
      this.message = message;
      this.dismissCountDown = this.dismissSecs;
    },
    login: function() {
      let vm = this;
      this.$http
        .get("http://localhost/tyreacpasms/DefaultHandler.ashx", {
          params: {
            method: "login",
            data: JSON.stringify(this.user)
          }
        })
        .then(function(response) {
          let resp = response.data,
            { success, message } = resp;
          if (!success) {
            vm.error(message);
            return;
          }
          this.$store.state.user.token = response.data.data;
          this.$cookie.set("auth_user", this.$store.state.user.token, {
            expires: 999,
            domain: location.host.split(":")[0]
          });
          this.$router.push("../index");
          vm.success("登录成功");
        });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->

<style scoped>
.login-header > h1 {
  position: fixed;
  top: 30%;
  left: 38%;
  color: white;
  font-size: 4em;
  text-shadow: 3in;
}
.login-header {
  position: fixed;
  top: 0px;
  left: 0px;
  height: 70%;
  width: 100%;
  background: #2c3e50;
}
.container {
  position: fixed;
  left: 30%;
  top: 80%;
}
label {
  height: 30px;
  line-height: 30px;
}
</style>
