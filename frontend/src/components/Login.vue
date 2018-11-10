<template>
<div class="login-page">
    <div class="login-header">
        <h1>黄河电器公司进销存管理系统</h1>
    </div>
    <div class="container">
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
  methods: {
    login: function() {
      this.$http
        .get("http://localhost/tyreacpasms/DefaultHandler.ashx", {
          params: {
            method: "login",
            data: JSON.stringify(this.user)
          }
        })
        .then(function(response) {
          if (!response.data.success) {
            // alert(response.data.message);
            return;
          }
          this.$store.state.user.token = response.data.data;
          this.$cookie.set("auth_user", this.$store.state.user.token, {
            expires: 999,
            domain: location.host.split(":")[0]
          });
          this.$router.push("../index");
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
