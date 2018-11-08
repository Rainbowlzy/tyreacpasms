<template>
    <div>
        <div class="index-page"></div>
        <div class="col-xs-12">
            <TopBar style="width:3000px;"></TopBar>
        </div>
        <div class="row">
            <b-table striped hover :items="items"></b-table>
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
.index-page {
  overflow: hidden;
  background-color: lightblue;
  background-repeat: no-repeat;
  background-size: cover;
  width: 100%;
  height: 100%;
  position: fixed;
  top: 0px;
  left: 0px;
  z-index: -200;
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
