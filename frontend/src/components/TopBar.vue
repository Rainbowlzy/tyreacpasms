<template>
    <div>
        <div class="row">
            <div class="icon-bar"></div>
        </div>
        <div class="row">
            <ul class="menu-bar">
                <li class="menu-item-selected">
                    <router-link to="/index">首页</router-link>
                </li>
                <li v-for="menu in menu_list" :key="menu.id"
                    :class="$route.params.mccaption===menu.MCCaption?'menu-item-selected':'menu-item'">
                    <router-link :to="'/business/'+menu.MCCaption">{{menu.MCDisplayName}}</router-link>
                </li>
                <li class="menu-item"><a href="#" @click="exit">退出</a></li>
                <li class="menu-item"><router-link to="/pagelist/MenuConfiguration">测试列表</router-link></li>
            </ul>
        </div>
    </div>
</template>

<script>
    import router from '@/router'
    import TopBar from "./TopBar.vue"
    export default {
        name: 'Index',
        components: {TopBar},
        data: function () {
            return {
                current_menu: '首页',
                menu_list: []
            }
        },
        methods: {
            exit: function () {
                this.$cookie.delete("auth_user");
                router.push('/login')
            }
        },
        mounted: function () {
            this.$http.get('http://localhost/tyreacpasms/DefaultHandler.ashx?method=GetMenuConfigurationByAuth').then(function (response) {
                if (!response.data.success) alert(response.data.message);
                else {
                    this.$data.menu_list = response.data.topmenu;
                }
            })
        }
    }
</script>

<style scoped>

    .icon-bar {
        margin: 0 20px;
        line-height: 50px;
        width: 673px;
        height: 94px;
        background: lightblue no-repeat 0% 0%;
    }

    .menu-bar {
        list-style-type: none;
        width:3000px;
        overflow: hidden;
    }

    .menu-item, .menu-item-selected {
        float: left;
        margin: 10px;
        font-size: 3.5em;
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
</style>
