<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>
<script>
    import 'bootstrap/dist/css/bootstrap.css'
    import 'bootstrap-vue/dist/bootstrap-vue.css'
    import store from "./store";
    export default {
        name: 'App',
        store,
        mounted: function () {
            if (!this.$cookie.get('auth_user') && location.href.indexOf("/login") === -1) {
                this.$router.push('/login');
                return;
            }
            this.$router.push('/index')
        }
    }
</script>

<style>

    #app {
        font-family: 'Avenir', Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
    }
</style>
