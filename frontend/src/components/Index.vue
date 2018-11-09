<template>
  <div>
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
  components: { TopBar, Blocks, Map },
  mounted(){
    this.$cookie.set("auth_user", this.$store.state.user.token, {
      expires: 999,
      domain: location.host.split(":")[0]
    });
  }
};
</script>

<style scoped>
ul {
  list-style-type: none;
}

.c-shadow {
  background-color: rgba(97, 255, 255, 0.15);
  border-radius: 10px;
  margin: 10px 10px;
}
</style>
