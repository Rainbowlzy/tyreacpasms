<template>
  <div>
    
    <div class="login-header">
        <h1>黄河电器公司进销存管理系统</h1>
    </div>

    <div class="menu-bar">
      <TopBar></TopBar>
    </div>

  </div>
</template>

<script>

import Blocks from "@/components/Blocks.vue";
import Map from "@/components/Map.vue";
import TopBar from "./TopBar.vue";

export default {
  name: "Index",
  components: { TopBar, Blocks, Map }
};

</script>

<style scoped>
.login-header {
  position: fixed;
  top: 0px;
  left: 0px;
  width: 100%;
  background: #2c3e50;
}

.menu-bar {
  position: fixed;
  top: 60px;
}

.login-header > h1 {
  height: 60px;
  line-height: 60px;
  color: white;
  font-size: 3em;
  text-shadow: 3in;
}

ul {
  list-style-type: none;
}

.c-shadow {
  background-color: rgba(97, 255, 255, 0.15);
  border-radius: 10px;
  margin: 10px 10px;
}
</style>
