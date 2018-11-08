<template>
<div class="login-page">
    <div class="container">
        <div class="row">
            <div class="col-xs-2"><label for="UILoginName" class="form-label">登录名</label></div>
            <div class="col-xs-3">
                <input type="text" @keydown.enter="login" class="form-control" id="UILoginName" v-model="user.UILoginName" placeholder="请输入登录名"/>
            </div>
                <div class="col-xs-2"><label for="UICode" class="form-label">密码</label></div>
                <div class="col-xs-3">
                    <input type="password" @keydown.enter="login" class="form-control"  id="UICode" v-model="user.UICode"/>
            </div>
                    <div class="col-xs-2"><button class="btn btn-success" @click="login">登录</button></div>
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
          if (!response.data.success) alert(response.data.message);
          else {
            this.$cookie.set("auth_user", response.data.data, {
              expires: 999,
              domain: location.host.split(":")[0]
            });
            this.$router.push("../index");
          }
        });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->

<style scoped>
.container {
  position: fixed;
  left: 30%;
  top: 50%;
}

label {
  color: #ffffff;
}

.login-page {
  background: lightblue 0% 0% / cover no-repeat;
  position: fixed;
  top: 0px;
  left: 0px;
  width: 100%;
  height: 100%;
}
</style>
