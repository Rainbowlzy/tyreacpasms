<template>
    <div class="">
        <div v-if="menu_list && menu_list.length>0">
            <div class="col-xs-3" v-for="menu in menu_list" :key="menu.id">
                <router-link :to="menu.MCLink?parseLink(menu.MCLink):('/business/'+menu.MCCaption)">
                    <img :src="get_src(menu)"
                         :alt="menu.MCCaption"
                         :class="menu.MCPicture?'block':'block-generated'"/>
                </router-link>
            </div>
        </div>
    </div>
</template>
<script>

    const colorlist = "CCDB4C#A8B81F#739DFB#5381E7#BB8BC6#A077A9#FB9A73#E87C51#E2767D#D83D47#5F9FC9#4B83A7#82DC66#54B935#5F9FC9#4B83A7#D04AD5#BB28C0".split('#').reverse()
    export default {
        name: 'Blocks',
        props: {
            mccaption: String
        },
        methods: {
            get_src: function (menu) {
                var rd = menu.MCCaption.charCodeAt(0)*31;
                var len = colorlist.length;
                var idx = (parseInt(rd + rd % 2) % len);
                return menu.MCPicture
                        ? ('http://122.193.9.83/' + menu.MCPicture)
                        : '/ImageHandler.ashx?label=' + menu.MCCaption + '&bgcolor1=' + colorlist[idx+1] + '&bgcolor2=' + colorlist[idx] + '&width=200&height=200&icon=' + menu.MCCaption + '.png&shape=trirect&fontSize=16&noshadow=noshadow'
            },
            parseLink:function(link){
                return '/pagelist/'+link.replace(/(\/XiangXi\/gen\/)|(List\.html)/g,'');
            },
            flush_menu: function () {
                this.$http.get('/DefaultHandler.ashx?method=GetMenuConfigurationByAuth&key=' + this.mccaption).then(function (response) {
                    if (!response.data.success) alert(response.data.message);
                    else {
                        this.$data.menu_list = response.data.topmenu;
                    }
                })
            }
        },
        data: function () {
            return {
                menu_list: []
            }
        },
        watch: {
            'mccaption': function (to, from) {
                this.flush_menu();
            }
        },
        mounted: function () {
            this.flush_menu();
        }
    }
</script>
<style scoped>
    .block, .block-generated {
        border-radius: 10px;
        width: 80%;
        height: 80%;
        margin: 10px;
    }

    .block-generated {
        border: 3px solid rgb(255, 255, 255);
    }
</style>
