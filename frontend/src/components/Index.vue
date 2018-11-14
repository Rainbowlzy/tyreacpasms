<template>
<div>
    <div class="menu-bar">
        <TopBar></TopBar>
    </div>
    <v-chart :options="polar" />
</div>
</template>

<script>
import Blocks from "@/components/Blocks.vue";
import Map from "@/components/Map.vue";
import TopBar from "./TopBar.vue";

import ECharts from 'vue-echarts/components/ECharts'
import 'echarts/lib/chart/line'
import 'echarts/lib/component/polar'

export default {
    name: "Index",
    components: {
        TopBar,
        Blocks,
        Map,
        'v-chart': ECharts
    },
    mounted() {
        this.$cookie.set("auth_user", this.$store.state.user.token, {
            expires: 999,
            domain: location.host.split(":")[0]
        });
    },
    data: function () {
        let data = []

        for (let i = 0; i <= 360; i++) {
            let t = i / 180 * Math.PI
            let r = Math.sin(2 * t) * Math.cos(2 * t)
            data.push([r, i])
        }

        return {
            polar: {
                title: {
                    text: '极坐标双数值轴'
                },
                legend: {
                    data: ['line']
                },
                polar: {
                    center: ['50%', '54%']
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        type: 'cross'
                    }
                },
                angleAxis: {
                    type: 'value',
                    startAngle: 0
                },
                radiusAxis: {
                    min: 0
                },
                series: [{
                    coordinateSystem: 'polar',
                    name: 'line',
                    type: 'line',
                    showSymbol: false,
                    data: data
                }],
                animationDuration: 2000
            }
        }
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
