import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App.vue'
import router from '@/router'
import VueResource from 'vue-resource'
import VueCookie from 'vue-cookie'
import BootstrapVue from 'bootstrap-vue'
import Vuex from 'vuex'


//import AMap from 'vue-amap'

//Vue.use(AMap);
//AMap.initAMapApiLoader({
//  key: 'd19bd922e0ef902491adfea1eb684502',
//  plugin: ['AMap.Autocomplete', 'AMap.PlaceSearch', 'AMap.Scale', 'AMap.OverView', 'AMap.ToolBar', 'AMap.MapType', 'AMap.PolyEditor', 'AMap.CircleEditor']
//});


Vue.use(BootstrapVue);
Vue.use(VueResource);
Vue.use(VueCookie);
Vue.use(VueRouter);
Vue.use(Vuex);

Vue.config.productionTip = false;

new Vue({
  render: h => h(App),
  router
}).$mount('#app');
