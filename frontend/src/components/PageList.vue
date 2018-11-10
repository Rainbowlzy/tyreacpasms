<template>
<div class="row">
    <TopBar></TopBar>
    <div class="row" style="position:fixed; top:120px; width:100%;">
      <div class="container-fluid">
          <div class="col-xs-10">
            <div class="row">
              <b-button>新增</b-button>
            </div>
                    <b-table hover 
                    id="my-table"
                    :per-page="5" 
                    v-model="items" 
                    :fields="fields">
            <template slot="操作" slot-scope="row">
                <b-button size="sm" @click.stop="edit(row)" class="mr-2">
                    编辑
                </b-button>
                <b-button size="sm" @click.stop="del(row)" class="mr-2">
                    删除
                </b-button>
            </template>
        </b-table>
          </div>
      </div>
    </div>
    <b-modal 
    ref="editmodal"
    :no-fade="true" size="lg" 
    hide-footer :title="'编辑'+table_name_ch">
      <div class="d-block text-center">
        <h3>编辑{{table_name_ch}}</h3>
      </div>
       <b-form @submit="onSubmit" @reset="onReset" v-if="current!==null">
        <b-form-group :id="'label'+col.column_name" v-for="col in columns" :key="col.id"
                      :label="col.column_description"
                      :label-for="col.column_name"
                      :description="col.column_description">
          <b-form-input 
                        type="text"
                        v-model="current.item[col.column_name]"
                        required
                        :placeholder="'请输入'+col.column_description">
          </b-form-input>
        </b-form-group>
      <b-button type="submit" variant="primary" @click="save">Submit</b-button>
    </b-form>
    </b-modal>

    
    <b-modal 
    ref="newmodal"
    :no-fade="true" size="lg" 
    hide-footer :title="'新增'+table_name_ch">
      <div class="d-block text-center">
        <h3>新增{{table_name_ch}}</h3>
      </div>
       <b-form @submit="onSubmit" @reset="onReset" v-if="current!==null">
        <b-form-group :id="'label'+col.column_name" v-for="col in columns" :key="col.id"
                      :label="col.column_description"
                      :label-for="col.column_name"
                      :description="col.column_description">
          <b-form-input 
                        type="text"
                        
                        required
                        :placeholder="'请输入'+col.column_description">
          </b-form-input>
        </b-form-group>
      <b-button type="submit" variant="primary" @click="save">Submit</b-button>
    </b-form>
    </b-modal>
</div>
</template>

<script>
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import Axios from "axios";
import TopBar from "@/components/TopBar.vue";
import metadata from "@/metadata";
import $ from "jquery";
import {
  mapState
  // mapMutations,
  // mapActions
} from "vuex";

Axios.defaults.withCredentials = true;

export default {
  components: {
    TopBar
  },
  computed: {
    columns() {
      return metadata[this.$route.params.mccaption].Columns;
    },
    caption() {
      return this.$route.params.mccaption;
    },
    modalShow() {
      return {
        modalShow: this.current !== null
      };
    },
    table_name_ch() {
      return metadata[this.$route.params.mccaption].table_name_ch;
    }
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      alert(JSON.stringify(this.form));
    },
    onReset(evt) {
      // evt.preventDefault();
      // /* Reset our form values */
      // this.form.email = '';
      // this.form.name = '';
      // this.form.food = null;
      // this.form.checked = [];
      // /* Trick to reset/clear native browser form validation state */
      // this.show = false;
      // this.$nextTick(() => { this.show = true });
    },
    hideModal() {
      this.$refs.editmodal.hide();
    },
    show() {},
    edit(row) {
      this.current = row;
      this.$refs.editmodal.show();
      // setTimeout(()=>$(".fade").removeClass("fade"), 100)
      console.log(row);
    },
    del(row) {
      let caption = this.$route.params.mccaption,
        token = this.$store.state.user.token,
        promise = Axios.post(
          `http://localhost/tyreacpasms/DefaultHandler.ashx?method=delete${caption}`,
          encodeURIComponent(JSON.stringify(row.item))
        );
      return promise
        .then(data => {
          return data.data.rows;
        })
        .catch(error => {
          return [];
        });
    },

    myProviderCallback(ctx, callback) {
      let vm = this,
        caption = this.$route.params.mccaption,
        token = this.$store.state.user.token,
        params = "?page=" + ctx.currentPage + "&size=" + ctx.perPage;

      vm.$http
        .get(
          `http://localhost/tyreacpasms/DefaultHandler.ashx?method=get${caption}list&${token}&offset=${
            ctx.currentPage
          }&limit=${ctx.perPage}`
        )
        .then(data => {
          // Pluck the array of items off our axios response
          let items = data.data.rows;
          // Provide the array of items to the callabck
          callback(items);
        })
        .catch(error => {
          callback([]);
        });

      // Must return null or undefined to signal b-table that callback is being used
      return null;
    },
    myProvider(ctx) {
      let vm = this,
        caption = this.$route.params.mccaption,
        token = this.$store.state.user.token,
        promise = Axios.get(
          `http://localhost/tyreacpasms/DefaultHandler.ashx?method=get${caption}list&${token}`
        );
      return promise
        .then(data => {
          vm.$data.items = data.data.rows;
          return data.data.rows;
        })
        .catch(error => {
          return [];
        });
    },
    reload() {
      console.log("reload");
      let caption = this.$route.params.mccaption,
        fields = [
          ...metadata[caption].Columns.map(col => ({
            key: col.column_name,
            label: col.column_description,
            sortable: true
          })),
          "操作"
        ];
      this.$data.fields = fields;
      this.$data.metadata = metadata[caption];
      this.$cookie.set("auth_user", this.$store.state.user.token, {
        expires: 999,
        domain: location.host.split(":")[0]
      });
      this.$root.$emit("bv::refresh::table", "my-table");
    }
  },
  data() {
    return {
      fields: [],
      current: null,
      metadata: {},
      items: []
    };
  },
  mounted() {
    console.log("mounted");
    this.reload();
  },
  watch: {
    $route() {
      this.pjtid = this.$route.params.mccaption;
    },
    pjtid() {
      this.reload();
    }
  }
};
</script>
