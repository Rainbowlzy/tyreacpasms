import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";

Vue.use(Vuex);
const baseUrl = "http://localhost/tyreacpasms/DefaultHandler.ashx";
export default new Vuex.Store({
  //   strict: true,
  state: {
    user: {
      UILoginName: "",
      UICode: "",
      token: "",
    }
  },
  mutations: {
    login(currentState, response) {
      Vue.set(currentState.user, 'token', response.data);
    }
  },
  actions: {
    async doLogin(context) {
      let response = (await Axios.get(`${baseUrl}?auth_user=${this.user.token}&method=login&data=${JSON.stringify(context.state.user)}`)).data,
        success = response.success;
      if (success) {
        context.commit("login", response);
        return response.data;
      } else {
        alert(response.message);
      }
      return success;
    },
    async getProductsAction(context) {
      (await Axios.get(baseUrl)).data.forEach(p =>
        context.commit("saveProduct", p)
      );
    },
    async saveProductAction(context, product) {
      let index = context.state.products.findIndex(p => p.id == product.id);
      if (index == -1) {
        await Axios.post(baseUrl, product);
      } else {
        await Axios.put(`${baseUrl}${product.id}`, product);
      }
      context.commit("saveProduct", product);
    },
    async deleteProductAction(context, product) {
      await Axios.delete(`${baseUrl}${product.id}`);
      context.commit("deleteProduct", product);
    }
  }
});