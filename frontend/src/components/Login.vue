<template>
    <div class="login-page">
        <div class="container">
            <div class="row">
                <div class="col-xs-2"><label for="UILoginName" class="form-label">登录名</label></div>
                <div class="col-xs-3"><input type="text" @keydown.enter="doLogin" class="form-control" id="UILoginName" v-model="user.UILoginName" placeholder="请输入登录名"/></div>
                <div class="col-xs-2"><label for="UICode" class="form-label">密码</label></div>
                <div class="col-xs-3"><input type="password" @keydown.enter="doLogin" class="form-control"  id="UICode" v-model="user.UICode"/></div>
                <div class="col-xs-2"><button class="btn btn-primary" @click="doLogin">登录</button></div>
            </div>
        </div>
    </div>
</template>

<script>
import { mapState, mapMutations, mapActions } from "vuex";

export default {
  name: "Login",
  computed: {
    ...mapState(["user"])
  },
  data: function() {
    return {
      UILoginName: "",
      UICode: ""
    };
  },
  methods: {
    ...mapMutations(["login"]),
    ...mapActions(["doLogin"])
  },
  mounted: function() {}
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.container {
  position: fixed;
  left: 20%;
  top: 90%;
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
