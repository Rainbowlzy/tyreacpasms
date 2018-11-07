<template>
    <div class="index-page">
        <div class="col-xs-12">
            <TopBar></TopBar>
        </div>
        <div class="container">
            <Blocks :mccaption="this.$route.params.mccaption"></Blocks>
        </div>
    </div>
</template>
<script>
    import TopBar from "@/components/TopBar.vue"
    import Blocks from "@/components/Blocks.vue"
    export default {
        name: 'Business',
        components: {TopBar,Blocks},
        methods: {
            flush_menu: function () {
            }
        },
        data: function () {
            return {
                menu_list: []
            }
        },
        watch: {
            '$route': function (to, from) {
                this.flush_menu();
            }
        },
        mounted: function () {
            this.flush_menu();
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .block {
        background: transparent;
        border-radius: 10px;
        width: 80%;
        height: 80%;
        margin: 10px;
    }

    .index-page {
        overflow: hidden;
        background-image: url(../../public/i/metaicon/e.jpg);
        background-repeat: no-repeat;
        background-size: cover;
        width: 100%;
        height: 100%;
        position: fixed;
        top: 0px;
        left: 0px;
    }
</style>
