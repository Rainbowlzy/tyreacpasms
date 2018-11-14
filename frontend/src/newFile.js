import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App.vue';
import VueResource from 'vue-resource';
import VueCookie from 'vue-cookie';
import BootstrapVue from 'bootstrap-vue';
import Vuex from 'vuex';
Vue.use(BootstrapVue);
Vue.use(VueResource);
Vue.use(VueCookie);
Vue.use(VueRouter);
Vue.use(Vuex);
Vue.http.options.emulateJSON = true;
Vue.config.productionTip = false;
Vue.http.interceptors.push((request, next) => {
  request.credentials = true;
  next();
});
new Vue({
  render: h => h(App),
  store,
  router
}).$mount('#app');