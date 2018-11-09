<template>
    <div class="map-page">
        <div class="map_top_nav"
             style="font-size: 12px; position:fixed ; z-index: 2000; margin:20px 80px;"
             v-show="categories && categories.length>0">
            <div v-for="category in categories" style="margin: 20px 20px;" :key="category.id">
                <a href="javascript:void(0)" @click="toggle(category)">
                    <div><img :src="'http://localhost/'+category.data.MCPicture" width="32"></div>
                    <div>
                        <span style="color:#000" class="map_span">
                            <!--{{category.title}}-->
                        </span>
                    </div>
                </a>
            </div>
        </div>
        <div class="map_right_nav" style="display:none; position: fixed; z-index:2000;"
             v-show="sub_categories && sub_categories.length">
            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                <div class="panel panel-default" v-for="sub in sub_categories" :key="sub.id">
                    <div class="panel-heading" role="tab" id="heading">
                        <h4 class="panel-title">
                            <a role="button"
                               href="javascript:void(0)"
                               type="population"
                               @click="toggle_sub(sub)"
                               :class="sub.title"
                               aria-expanded="true">
                                <i class="glyphicon glyphicon-chevron-right"></i>{{sub.title}}
                            </a>
                        </h4>
                    </div>
                </div>
            </div>
        </div>
        <div class="wrapper">
            <div id="amap"
                 style="width:98%;height:98%;overflow: hidden;border-radius:10px;margin:10px 10px;padding-bottom:80%"></div>
        </div>
    </div>
</template>
<script>
    import $ from 'jquery'
    import 'bootstrap'

    function calcCenter(points) {
        if (!points || points.length === 0) return [0, 0];
        var minx, miny, maxx, maxy;
        minx = miny = 4000;
        maxx = maxy = -4000;
        for (var j = 0; j < points.length; ++j) {
            var cur = points[j];
            var x = cur[0];
            var y = cur[1];
            if (x < minx) {
                minx = x;
            }
            if (x > maxx) {
                maxx = x;
            }
            if (y < miny) {
                miny = y;
            }
            if (y > maxy) {
                maxy = y;
            }
        }
        var center = [(maxx - minx) / 2 + minx, (maxy - miny) / 2 + miny];
        return center;
    }
    Array.prototype.groupby = function (key, fn) {
        var obj = {};
        $.each(this, function () {
            obj[this[key]] = obj[this[key]] || [];
            if (fn) {
                obj[this[key]].push(fn(this));
            } else {
                obj[this[key]].push(this);
            }
        });
        return obj;
    };

    export default {
        name: 'Business',
        mounted: function () {
            var vm = this;
            this.$http.get('http://localhost/tyreacpasms/DefaultHandler.ashx?method=GetMapCategoryEvaluator').then(function (categories) {
                vm.$data.categories = categories.body;
            });
            var map = this.map = new AMap.Map('amap', {
                resizeEnable: true,
                zoom: 14,
                layers: [
//                    new AMap.TileLayer()//高德默认标准图层
                ],
                center: [120.511929, 31.252366]
            });
            // 绘制香溪社区边框图开始
            // 据说要不到点位，所以随便画好了。
            var address = "香溪社区外框图";
            this.$http.get("http://localhost/tyreacpasms/DefaultHandler.ashx?method=getpoilist", {
                params: {
                    data: {search: address, limit: 4000, sort: 'ord'}
                }
            }).then(function (resp) {
                if (!resp) return;
                if (!resp.rows) return;
                var marker = new AMap.Marker({
                    map: map,
                    position: [120.511929, 31.252366],
                    title: "香溪社区",
                    icon: "../../public/i/mapicon/cangf.png"
                });

                marker.on('click',
                        function () {
                            var summary = '<p class="MsoNormal">	<b></b></p><p class="MsoNormal" style="text-indent:24.1pt;">	<span style="font-size:18px;">2003年，金星村与镇区三个居委会合并改名为香溪社区。香溪社区位于木渎中心地段，紧靠镇郊和灵岩山风景区，社区所辖面积1.8平方公里，现有总户数2003户，户籍人口6159户，党员220人。一直以来，社区党委始终坚持以经济发展为中心，以“为民服务”为宗旨，创新发展，转型升级，加强服务，规范管理，全面提升社区的综合实力和服务水平。</span></p><p class="MsoNormal" style="text-indent:24.1pt;">	<span style="font-size:18px;">为加快社区城乡一体化建设，2001年成立江苏省首家社区资产股份合作社，十多年来，社区始终坚持科学发展观，积极探索农村股份合作改革，不断做大做强集体经济，保障村民收益。2017年，社区三个股份合作社固定资产达5亿元，集体收益3657万元，每个股民分红13400元，位居苏州前列。</span></p><p class="MsoNormal" style="text-indent:24.1pt;">	<span style="font-size:18px;">在大力发展经济的同时，社区党委坚持走群众路线，加大投入实施民生工程，真正为民办实事，做好事，受到了上级的肯定和群众的拥护。近几年来，先后获得“全国和谐示范社区、江苏省民主法治示范社区、江苏省科普示范社区，江苏省社会主义新农村建设先进村，苏州市十佳农村新型合作经济组织、苏州市绿色社区”等荣誉称号。</span></p>';
                            this.openWindow('<div style="width:800px;"><a href="/XiangXi/0_common/map_editor_tool.html?addr=香溪社区外框图&lnglat=' + this.getPosition() + '"><b><span style="font-size:18px;">香溪社区简介</span></b></a>' + summary + '</div>');
                        });
                map.setFitView();

                var summary = '<p class="MsoNormal">	<b></b></p><p class="MsoNormal" style="text-indent:24.1pt;">	<span style="font-size:18px;">2003年，金星村与镇区三个居委会合并改名为香溪社区。香溪社区位于木渎中心地段，紧靠镇郊和灵岩山风景区，社区所辖面积1.8平方公里，现有总户数2003户，户籍人口6159户，党员220人。一直以来，社区党委始终坚持以经济发展为中心，以“为民服务”为宗旨，创新发展，转型升级，加强服务，规范管理，全面提升社区的综合实力和服务水平。</span></p><p class="MsoNormal" style="text-indent:24.1pt;">	<span style="font-size:18px;">为加快社区城乡一体化建设，2001年成立江苏省首家社区资产股份合作社，十多年来，社区始终坚持科学发展观，积极探索农村股份合作改革，不断做大做强集体经济，保障村民收益。2017年，社区三个股份合作社固定资产达5亿元，集体收益3657万元，每个股民分红13400元，位居苏州前列。</span></p><p class="MsoNormal" style="text-indent:24.1pt;">	<span style="font-size:18px;">在大力发展经济的同时，社区党委坚持走群众路线，加大投入实施民生工程，真正为民办实事，做好事，受到了上级的肯定和群众的拥护。近几年来，先后获得“全国和谐示范社区、江苏省民主法治示范社区、江苏省科普示范社区，江苏省社会主义新农村建设先进村，苏州市十佳农村新型合作经济组织、苏州市绿色社区”等荣誉称号。</span></p>';
                marker.openWindow('<div style="width:800px;"><a href="/XiangXi/0_common/map_editor_tool.html?addr=香溪社区外框图&lnglat=' + marker.getPosition() + '"><b><span style="font-size:18px;">香溪社区简介</span></b></a>' + summary + '</div>');
                map.setZoom(17);

            });

            AMap.Marker.prototype.openWindow = function (content) {
                var map = this.getMap();
                var position = this.getPosition();
                position = [position.lng, position.lat];
                var lnglat = new AMap.LngLat(position[0], position[1]);
                var recalc_center = lnglat.offset(0, 140);
                var w = new AMap.InfoWindow({
                    content: content || '<iframe frameborder="0" src="../2_map/inner_population.html?address=' + this.getTitle() + '" width="500" height="380"></iframe>',
                    showShadow: true
                });
                w.open(map, position);
                map.panTo(recalc_center);
                map.setZoom(18);
            };

            AMap.plugin(['AMap.ToolBar', 'AMap.Scale', 'AMap.OverView'], function () {
                map.addControl(new AMap.ToolBar());
                map.addControl(new AMap.Scale());
                map.addControl(new AMap.OverView({isOpen: true}));
            })
        },
        data: function () {
            return {
                categories: [],
                sub_categories: [],
                current_menu: "首页",
                secondmenu: [],
                thirdmenu: [],
                map:null,
                svcHeader: 'http://localhost',
                imglist: '../../public/i/emptyimage/1.png,../../public/i/emptyimage/11.png,../../public/i/emptyimage/111.png,../../public/i/emptyimage/123.png,../../public/i/emptyimage/12312315.png,../../public/i/emptyimage/2.png,../../public/i/emptyimage/22 (2).png,../../public/i/emptyimage/22.png,../../public/i/emptyimage/222 (2).png,../../public/i/emptyimage/222.png,../../public/i/emptyimage/2231.png,../../public/i/emptyimage/256.png,../../public/i/emptyimage/3.png,../../public/i/emptyimage/33 (2).png,../../public/i/emptyimage/33.png,../../public/i/emptyimage/44.png,../../public/i/emptyimage/45463.png,../../public/i/emptyimage/45465.png,../../public/i/emptyimage/5456.png,../../public/i/emptyimage/55.png,../../public/i/emptyimage/78.png,../../public/i/emptyimage/78423.png,../../public/i/emptyimage/888.png,../../public/i/emptyimage/895.png,../../public/i/emptyimage/denglurizhi.png,../../public/i/emptyimage/list.txt,../../public/i/emptyimage/tu (2).png,../../public/i/emptyimage/tu.png,../../public/i/emptyimage/tutututu.png,../../public/i/emptyimage/tututuu.png,../../public/i/emptyimage/组1拷贝19.png'.split(","),
                colorlist: "CCDB4C#A8B81F#739DFB#5381E7#BB8BC6#A077A9#FB9A73#E87C51#E2767D#D83D47#5F9FC9#4B83A7#82DC66#54B935#5F9FC9#4B83A7#D04AD5#BB28C0#CCDB4C#A8B81F#739DFB#5381E7#BB8BC6#A077A9#FB9A73#E87C51#E2767D#D83D47#5F9FC9#4B83A7#82DC66#54B935#5F9FC9#4B83A7#D04AD5#BB28C0".split('#').reverse()
            }
        },
        state: {
            switch: false
        },
        methods: {
            exit: function () {
                $.cookie("auth_user").then({path: "/"});
                location.href = "../4_login/login.html";
            },
            random: function random() {
                return Math.random() * this.imglist.length;
            },
            isdirect: function isdirect(row) {
                return row && (row.MCLink || row.link) && (row.MCLink || row.link).indexOf('business.html') === -1;
            },
            switch_layer: function () {
                if (this.current_layer) {
                    this.map.remove(this.current_layer);
                    this.current_layer = null;
                    $(".map_span").css({'color': '#000'});
                    return;
                }
                this.current_layer = new AMap.TileLayer.Satellite();
                this.map.add(this.current_layer);
                $(".map_span").css({'color': '#fff'});
            },
            toggle: function (category) {
                if (!category.children || category.children.length <= 0) {
                    this.toggle_sub(category);
                    return;
                }
                this.$data.sub_categories = category.children;
                $('.map_right_nav').toggle('toggle');
            },
            toggle_sub: function (sub) {
                var label = sub.title.split(' ')[0];
                if (label === '切换地图') {
                    this.switch_layer();
                    return;
                }
                var options = {
                    全部: {
                        method: "MapQueryPopulation",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/renkou.png",
                        page: "../2_map/inner_population.html"
                    },
                    党员: {
                        method: "MapQueryPartyMember",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/dangyuan.png",
                        page: "../2_map/inner_population.html"
                    },
                    老年人: {
                        method: "MapQueryOldMann",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/laonian.png",
                        page: "../2_map/inner_population.html"
                    },
                    残疾人: {
                        method: "MapQueryDisabledMan",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/canji.png",
                        page: "../2_map/inner_population.html"
                    },
                    民兵: {
                        method: "MapQueryMilitia",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/minbing.png",
                        page: "../2_map/inner_population.html"
                    },
                    干部: {
                        method: "MapQueryCadre",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/ganbu.png",
                        page: "../2_map/inner_population.html"
                    },
                    厂房: {
                        params: {BIndustrialParkName: label},
                        method: "MapQueryBuilding",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/cangf.png",
                        page: "../2_map/inner_building.html"
                    },
                    观山工业园: {
                        params: {BIndustrialParkName: label},
                        method: "MapQueryBuilding",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/cangf.png",
                        page: "../2_map/inner_building.html"
                    },
                    横泾工业园二期: {
                        params: {BIndustrialParkName: label},
                        summary: '<pre><span>横泾工业园二期</span></pre></pre><pre>2012年6月1日收购，占地90亩，一期建筑面积3.2万平方米，二期建筑面积2.9万平方米，总面积6.1万平方米，总投资约为1.25亿元。</pre>',
                        method: "MapQueryBuilding",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/cangf.png",
                        page: "../2_map/inner_building.html"
                    },
                    横泾工业园一期: {
                        params: {BIndustrialParkName: label},
                        summary: '<pre><span>横泾工业园一期</span></pre></pre><pre>2012年6月1日收购，占地90亩，一期建筑面积3.2万平方米，二期建筑面积2.9万平方米，总面积6.1万平方米，总投资约为1.25亿元。</pre>',
                        method: "MapQueryBuilding",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/cangf.png",
                        page: "../2_map/inner_building.html"
                    },
                    马舍工业园: {
                        params: {BIndustrialParkName: label},
                        method: "MapQueryBuilding",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/cangf.png",
                        page: "../2_map/inner_building.html"
                    },
                    摄像头: {
                        // method: "MapQueryCamera", icon: "../../public/i/mapicon/jiank.png", page: "/XiangXi/2_map/RTSP.aspx"
                        method: "MapQueryNewCamera",
                        icon: "http://localhost/XiangXi/assets/i/mapicon/jiank.png",
                        page: "../2_map/hls.html"
                    }
                };

                var option = options[label];
                if (!option) {
                    alert("未找到标签" + label);
                    return;
                }

                var map = this.map;
                $('.map_right_nav').hide();
                $('.map_right_com').hide();
                this.$http.get('http://localhost/tyreacpasms/DefaultHandler.ashx?method='+option.method, {
                    params:{
                        data:$.extend({limit: 4000}, option.params)
                    }
                }).then(function (response) {
                    var data = response.body;
                    if (!data || data.length === 0) {
                        alert('未找到数据');
                        return;
                    }
                    $('.map_right_nav').hide();
                    map.clearMap();
                    var recalc_center = null;
                    var groups = (data.rows || data).groupby('POIAddress');

                    function paint_ent(map, k, ent) {
                        let points = ent.map(function (e) {
                            return [parseFloat(e.Longitude || '0'), parseFloat(e.Latitude || '0')]
                        }).filter(function (w) {
                            return w[0] && w[1];
                        });
                        if (!points || points.length <= 0) return;
                        var marker = null;
//                        var lnglat = null;
//                        var recalc_center = points.length > 1?lnglat.offset(0, -50):lnglat.offset(0, -80);

                        var viewpoint = null
                        if (points.length > 1) {
                            let viewpoint = calcCenter(points);
                            var title = k;
                            new AMap.Polygon({
                                map: map,
                                path: points,
                                fillColor: 'blue',
                                fillOpacity: 0.3
                            });
                            marker = new AMap.Marker({
                                map: map,
                                position: viewpoint,
                                title: title,
                                icon: option.icon
                            });
                            if (option.summary) marker.openWindow(option.summary)
                        } else {
                            var viewpoint = points[0];
                            marker = new AMap.Marker({
                                map: map,
                                position: viewpoint,
                                title: k,
                                icon: option.icon
                            });
                        }
                        if(viewpoint)recalc_center = new AMap.LngLat(viewpoint[0], viewpoint[1]).offset(0, 140);
                        marker.ent = ent;
                        marker.on('click', function () {
                            var markerTitle = this.getTitle();
                            var position = this.getPosition();
                            var ifurl = 'http://localhost/XiangXi/gen/'+option.page + '?url=&VPBIAddress=' + markerTitle + '&address=' + markerTitle + '&position=' + position;

                            var width = "1000";
                            var height = "800";
                            if (this.ent && this.ent[0]) {
                                ifurl =  'http://localhost/XiangXi/gen/'+option.page + '?url=' + this.ent[0].url
                            }
                            this.openWindow('<iframe frameborder="0" src=' + ifurl + ' width=' + width + ' height=' + height + '></iframe>');
                        });
                    }

                    let count = 0, max=10;
                    if (groups)
                        for (var k in groups) {
                            paint_ent(map, k, groups[k])
                            if(++count>max)break;
                        }
                    map.panTo(recalc_center || [120.511929, 31.252366]);
                    map.setFitView()
                });
            },
            redirect: function (li) {
                var link = li.MCLink;
                if (link.indexOf('business.html') !== -1) parent.location.href = link;
                location.href = link
            },
            editorswitch: function () {
                this.state.switch = !this.state.switch;
            }
        }
    }
</script>
<style scoped>
    .map_right_nav {
        position: fixed;
        /*top: 120px;*/
        /*right: 580px;*/
        /*z-index: 1002;*/
        margin: 80px 80px;
        width: 238px;
    }

    .map_right_com {
        position: fixed;
        top: 120px;
        right: 580px;
        z-index: 1002;
        width: 150px;
    }

    .map_left_nav {
        position: fixed;
        top: 120px;
        left: 800px;
        z-index: 1002;
    }

    .map_left_nav div {
        width: 64px;
        text-align: center;
    }

    .map_left_nav div span {
        color: #ffffff;
    }

</style>
