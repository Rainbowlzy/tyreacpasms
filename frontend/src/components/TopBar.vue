<template>
    <div>
        <div class="login-header">
            <h1>黄河电器公司进销存管理系统</h1>
        </div>
        <ul class="menu-bar">
            <li class="menu-item-selected">
                <router-link to="/index">首页</router-link>
            </li>
            <li class="menu-item"><a href="#" @click="exit">退出</a></li>
            <li class="menu-item"><router-link to="/pagelist/MenuConfiguration">测试列表</router-link></li>
            <li v-for="menu in menu_list" :key="menu.id"
                :class="$route.params.mccaption===menu.MCCaption?'menu-item-selected':'menu-item'">
                <router-link :to="menu.MCLink">{{menu.MCDisplayName}}</router-link>
                <!-- <a :href="menu.MCLink">{{menu.MCDisplayName}}</a> -->
                <!-- <a href="#" @click="goto(menu)">{{menu.MCDisplayName}}</a> -->
            </li>
        </ul>
        <div style="height:120px; width:100%; display:block;"></div>
    </div>
</template>

<script>
import router from "@/router";
import TopBar from "./TopBar.vue";

export default {
  name: "Index",
  components: { TopBar },
  data: function() {
    return {
      current_menu: "首页",
      menu_list: []
    };
  },
  methods: {
    goto(menu) {
      this.$router.push(menu.MCLink);
      this.$router.go(0);
    },
    exit: function() {
      this.$cookie.delete("auth_user");
      router.push("/login");
    }
  },
  mounted: function() {
    this.$http
      .get(
        "http://localhost/tyreacpasms/DefaultHandler.ashx?method=GetMenuConfigurationByAuth"
      )
      .then(function(response) {
        if (!response.data.success) {
          // alert(response.data.message);
          return;
        }
        this.$data.menu_list = response.data.topmenu;
      });
  }
};
</script>

<style scoped>
.menu-bar {
  list-style-type: none;
  width: 3000px;
  overflow: hidden;
  background: black;
  position: fixed;
  top: 60px;
}

.menu-item,
.menu-item-selected {
  float: left;
  margin: 0px 10px;
  font-size: 2em;
  line-height: 60px;
  height: 60px;
}

.menu-item > a {
  color: #ffffff;
}

.menu-item > a:hover {
  color: rgb(255, 165, 77);
}

.menu-item-selected > a {
  color: rgb(255, 165, 77);
}

.login-header {
  position: fixed;
  top: 0px;
  left: 0px;
  width: 100%;
  background: #2c3e50;
}

.login-header > h1 {
  height: 60px;
  line-height: 60px;
  color: white;
  font-size: 3em;
  text-shadow: 3in;
}
</style>
