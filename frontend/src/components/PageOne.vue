<template>
    <div class="page-one">
        <div class="top-bar">
            <TopBar style="width:3000px;"></TopBar>
        </div>
        <div class="container">
            <a href="#">
                <h3>
                    {{table_name_ch}}
                </h3>
            </a>
            <div class="row">
                <div class="table-top-buttons">
                    <a href="#" @click="goBack" id="table-back" class="btn btn-primary">
                        <i class="glyphicon glyphicon-menu-left"></i>返回</a>
                    <button type="button" id="submit" class="btn  btn-info" @click="save"><i class="glyphicon glyphicon-circle-arrow-up"></i>提交</button>
                    <button type="button" class="btn btn-info excel-import-button-<#=tbl#>">
                        <i class="glyphicon glyphicon-cloud-upload"></i>Excel导入
                    </button>
                    <a href="/XiangXi/ExportSchema.ashx?title=<#=tbl#>" class="btn btn-primary">Excel导出</a>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-6 form-group" v-for="col in Columns">
                    <label class="col-xs-3 control-label" :for="col.column_name">{{col.column_description}}</label>
                    <div class="col-xs-8"><input type="text" class="form-control" :id="col.column_name" v-model="entity[col.column_name]"/></div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import metadata from '@/metadata'
    import TopBar from '@/components/TopBar.vue'

    export default {
        name: 'PageList',
        components: {
            TopBar
        },
        mounted: function () {
        },
        methods: {
            goBack: function () {
                window.history.length > 1
                        ? this.$router.go(-1)
                        : this.$router.push('/')
            },
            save: function () {
                this.$http.get("http://122.193.9.83/XiangXi/DefaultHandler.ashx?method=save"+this.table_name,{
                    params:{
                        data:JSON.stringify(this.entity)
                    }
                }).then(function (response) {
                    var data = response.body;
                    if(data && data.success) this.$router.go(-1);
                    else if(!data) alert(data);
                    else if(data.message) {
                        alert(data.message);
                    }
                })
            }
        },
        data: function () {
            var metadata2 = metadata[this.$route.params.mccaption];
            function buildDefaultObject(){
                var default_object = {};
                var log = metadata2.Columns.map(function (t) {
                    default_object[t.column_name]  = null;
                    return default_object;
                });
                return default_object;
            }
            return Object.assign(metadata2, {
                entity:this.$route.params.data||buildDefaultObject()
            });
        }
    }
</script>
<style scoped>
    .am-btn {
        margin: 0px 10px;
    }

    .page-list {
        margin-top: 0px;
        padding: 0px;
    }
    #pageList{
        margin-top:10px;
    }
    .top-bar {
        overflow: hidden;
        background-image: url(../../public/i/metaicon/e.jpg);
        background-repeat: no-repeat;
        background-size: cover;
        z-index: -200;
    }
    .table-top-buttons, .table-top-buttons>*{
        float:left;
    }
    .container{
        margin-top:10px;
    }
    .table-top-buttons>*{
        margin:5px;
    }
</style>
