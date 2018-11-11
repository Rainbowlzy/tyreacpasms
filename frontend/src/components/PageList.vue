<template>
<div id="page-list">
    <TopBar></TopBar>
    <div class="row" style="position:fixed; top:130px; width:100%;">
        <div class="container">

            <b-alert :show="dismissCountDown" dismissible fade variant="success" @dismissed="dismissCountDown=0" @dismiss-count-down="countDownChanged">
                {{message}}
            </b-alert>

            <b-alert :show="errorCountDown" dismissible fade variant="alert" @dismissed="errorCountDown=0" @dismiss-count-down="countDownChanged">
                {{message}}
            </b-alert>

            <div class="row">
                <b-btn v-b-modal.newmodal>新增</b-btn>
            </div>
            <b-table hover striped id="my-table" :per-page="pageSize" :current-page="pageIndex" :items="myProviderCallback" :fields="fields">
                <template slot="操作" slot-scope="row">
                    <b-button size="sm" @click.stop="edit(row)" class="mr-2">
                        编辑
                    </b-button>
                    <b-button size="sm" @click.stop="del(row)" class="mr-2">
                        删除
                    </b-button>
                </template>
            </b-table>
            <b-row>
                <b-col md="6" class="my-1">
                    <b-pagination :total-rows="total" :per-page="pageSize" v-model="pageIndex" class="my-0" />
                </b-col>
            </b-row>
        </div>
    </div>
    <b-modal ref="editmodal" :no-fade="true" size="lg" hide-footer :title="'编辑'+table_name_ch">
        <b-form @submit="onSubmit" @reset="onReset" v-if="current!==null">
            <b-form-group v-for="col in columns" :key="col.id" :id="'label'+col.column_name" :label="col.column_description" :label-for="col.column_name">
                <b-form-input type="text" v-model="current.item[col.column_name]" :placeholder="'请输入'+col.column_description">
                </b-form-input>
            </b-form-group>
            <b-button type="submit" variant="primary">保存</b-button>
        </b-form>
    </b-modal>

    <b-modal id="newmodal" ref="newmodal" :no-fade="true" size="lg" hide-footer :title="'新增'+table_name_ch">
        <b-form @submit="onSubmit" @reset="onReset">
            <b-form-group :id="'label'+col.column_name+'new'" v-for="col in columns" :key="col.id" :label="col.column_description" :label-for="'new'+col.column_name">
                <b-form-input :id="'new'+col.column_name" v-model="newOne[col.column_name]" type="text" :placeholder="'请输入'+col.column_description">
                </b-form-input>
            </b-form-group>
            <b-button type="submit" variant="primary">保存</b-button>
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
        newOne() {
            if (this.$data.currentOne) return this.$data.currentOne;
            let vm = this,
                caption = this.$route.params.mccaption,
                o = {},
                columns = metadata[caption].Columns;
            for (var k in columns) {
                o[columns[k].column_name] = '';
            }
            this.$data.currentOne = o;
            return this.$data.currentOne;
        },
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
            let vm = this,
                caption = this.$route.params.mccaption;
            evt.preventDefault();
            if (this.$data.current) {
                this.$http.post(`http://localhost/tyreacpasms/DefaultHandler.ashx`, {
                        method: `save${caption}`,
                        data: encodeURIComponent(JSON.stringify(this.$data.current))
                    }, {
                        credentials: true
                    })
                    .then(data => {
                        if (!data || !data.data) {
                            vm.showAlert("系统错误");
                            return;
                        }
                        if (!data.data.success) {
                            vm.showAlert("保存成功");
                            return;
                        }
                    })
                return;
            }
            if (this.$data.currentOne) {
                this.$http.post(`http://localhost/tyreacpasms/DefaultHandler.ashx`, {
                        method: `save${caption}`,
                        data: encodeURIComponent(JSON.stringify(this.$data.currentOne))
                    }, {
                        credentials: true
                    })
                    .then(data => {
                        if (!data || !data.data) {
                            vm.showAlert("系统错误");
                            return;
                        }
                        if (!data.data.success) {
                            vm.showAlert("保存成功");
                            return;
                        }
                    })
            }
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
            let vm = this,
                caption = this.$route.params.mccaption,
                token = this.$store.state.user.token;
            return Axios.post(
                    `http://localhost/tyreacpasms/DefaultHandler.ashx`, `method=delete${caption}&data=${encodeURIComponent(JSON.stringify(row.item))}`
                )
                .then(data => {
                    vm.showAlert("删除成功");
                    return data.data.rows;
                })
                .catch(error => {
                    return vm.error(error);
                });
        },
        myProviderCallback(ctx, callback) {
            let vm = this,
                caption = this.$route.params.mccaption,
                token = this.$store.state.user.token,
                result = vm.$http
                .get(
                    `http://localhost/tyreacpasms/DefaultHandler.ashx?method=get${caption}list&auth_user=${token}&offset=${(ctx.currentPage-1)*ctx.perPage}&limit=${ctx.perPage}&sort=${ctx.sortBy}&order=${ctx.sortDesc?'desc':'asc'}`
                )
                .then(data => {
                    console.log(ctx)
                    let items = data.data.rows;
                    vm.$data.total = data.data.total;
                    vm.$data.pageIndex = ctx.currentPage;
                    vm.$data.pageSize = ctx.perPage;
                    callback(items);
                })
                .catch(error => {
                    callback([]);
                });
        },
        reload() {
            let vm = this,
                caption = this.$route.params.mccaption,
                token = this.$store.state.user.token,
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
        },
        countDownChanged(dismissCountDown) {
            this.dismissCountDown = dismissCountDown
        },
        error(message) {
            this.message = message;
            this.errorCountDown = this.errorSecs
        },
        showAlert(message) {
            this.message = message;
            this.dismissCountDown = this.dismissSecs
        }
    },
    data() {
        return {
            fields: [],
            current: null,
            metadata: {},
            items: [],
            pageIndex: 1,
            pageSize: 5,
            total: 0,
            dismissSecs: 5,
            errorSecs: 5,
            dismissCountDown: 0,
            errorCountDown: 0,
            showDismissibleAlert: false,
            message: '请初始化系统消息或使用showAlert方法'
        };
    },
    mounted() {
        console.log("PageList mounted");
        this.reload();
    },
    watch: {
        $route() {
            console.log("watch $route changed.");
            this.reload();
        },
        pjtid() {
            this.reload();
        }
    }
};
</script>

<style scoped>
#page-list {}

.row {
    margin: 10px 0px;
}
</style>
