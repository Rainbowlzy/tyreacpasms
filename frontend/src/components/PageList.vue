<template>
    <div class="page-list">
        <div class="top-bar ">
            <TopBar style="width:3000px;"></TopBar>
        </div>
        <div id="pageList">
            <div class="container">
                <div id="myAlert" class="alert alert-warning" style="display:none;">
                    <a href="#" class="close" data-dismiss="alert">&times;</a>
                </div>

                <a href="#">
                    <h3>
                        {{table_name_ch}}
                    </h3>
                </a>
                <div class="table-top-buttons">
                    <a href="#" @click="goBack" id="table-back" class="btn btn-success">
                        <i class="glyphicon glyphicon-menu-left"></i>返回</a>
                    <router-link :to="'/pageone/'+$route.params.mccaption+'&{}'"
                                 class="btn btn-primary">
                        <i class="glyphicon glyphicon-plus"></i>新增
                    </router-link>
                    <button type="button" class="btn btn-info excel-import-button-<#=tbl#>">
                        <i class="glyphicon glyphicon-cloud-upload"></i>Excel导入
                    </button>
                    <a href="/XiangXi/ExportSchema.ashx?title=<#=tbl#>" class="btn btn-primary">Excel导出</a>
                </div>
                <table id="table"
                       data-search="true"
                       data-show-refresh="true"
                       data-show-columns="true"
                       data-pagination="true"
                       data-page-list="[10, 25, 50, 100, ALL]"
                       data-page-size="8"
                       data-id-field="id"
                       data-side-pagination="server"
                       :data-url="'http://122.193.9.83/XiangXi/DefaultHandler.ashx?method=get'+table_name+'List'"></table>
            </div>
            <div class="modal fade table_excel_modal-<#=tbl#>">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span
                                    aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Excel导入</h4>
                        </div>
                        <div class="modal-body"></div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                            <!--<button type="button" class="btn btn-success"><i class="glyphicon glyphicon-ok"></i>确定</button>-->
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="html-peek">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span
                                    aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">预览</h4>
                        </div>
                        <div class="modal-body"></div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import metadata from '@/metadata'
    import TopBar from '@/components/TopBar.vue'

    function Formatters(vm) {
        this.prototype = new Object();
        function strlimit(s, len) {
            if (!s || typeof(s) !== 'string' || s.length < len) return s;
            var slen = len / 2 - 2;
            return s.substr(0, slen) + '...' + s.substr(s.length - slen, slen);
        }
        this.prototype.detailFormatter = function detailFormatter(index, row) {
            var html = [];
            $.each(row, function (key, value) {
                html.push("<p><b>" + key + ":</b> " + value + "</p>");
            });
            return html.join("");
        };
        this.prototype.imgFormatter = function imgFormatter(col) {
            return function (value, row, index) {
                if (row[col] && row[col].indexOf(',') !== -1) return [row[col] ? row[col].split(',').map(function (i) {
                    return '<img src="' + i + '" width="40" height="40" />';
                }).join('') : ''];
                return [row[col] ? '<img src="http://122.193.9.83' + row[col] + '" width="40" height="40" />' : ''];
            };
        };
        this.prototype.linkFormatter = function linkFormatter(col) {
            return function (value, row, index) {
                ;
                return [row[col] ? $('<a></a>').attr('href', row[col]).text(strlimit(row[col], 30)).get(0).outerHTML : ''];
            };
        };
        this.prototype.limitFormatter = function limitFormatter(col) {
            return function (value, row, index) {
                return [row[col] ? strlimit(row[col], 20) : ''];
            };
        };
        this.prototype.operateFormatter = function operateFormatter(value, row, index) {
            var buttons = [];
            for (var p in row) {
                if (p.toLowerCase().indexOf('address') !== -1
                        || p.toLowerCase().indexOf('residence') !== -1
                        || p.toLowerCase().indexOf('tenant') !== -1) {
                    var k = p;
                    row.address = row[k];
                    buttons.push([
                        '<a class=" position am-btn"   href="', '/XiangXi/2_map/full_map.html?data=' + encodeURIComponent(JSON.stringify(row)),
                        '"  title="位置" >',
                        '<i class="glyphicon glyphicon-globe"></i>查看位置',
                        '</a>',
                        '<a class=" edit-position am-btn"   href="', '/XiangXi/0_common/map_editor_tool.html?addr=' + row.address,
                        '"  title="位置" >',
                        '<i class="glyphicon glyphicon-globe"></i>编辑位置',
                        '</a>'
                    ].join(""));
                    break;
                }
                if (p.indexOf("File") !== -1) {
                    buttons.push([
                        '<a class=" download am-btn"   href="', '#',
                        '"  title="下载" >',
                        '<i class="glyphicon glyphicon-globe"></i>下载',
                        '</a>'
                    ].join(""));
                    break;
                }
            }

            buttons.push([
                '<a class=" edit am-btn"   href="javascript:void(0)"  >',
                '<i class="glyphicon glyphicon-edit"></i>编辑',
                '</a>',
                '<a class="remove am-btn" href="javascript:void(0)">',
                '<i class="glyphicon glyphicon-remove"></i>删除',
                '</a>'
            ].join(' '));
            var html = buttons.join("");
            return html;
        };
        this.prototype.dateformat = function dateformat(value, row, index) {
            return [
                (changeDateFormat(row.date) || "").substr(0, 10)
            ].join("");
        };
        return this.prototype;
    }
    function setAssign(keys, format) {
        return keys.split(',').map(function (t) {
            var obj = {};
            obj[t] = format;
            return obj;
        }).reduce(Object.assign);
    }
    function FormatterSelector(col) {
        var formatters = new Formatters();
        var columnName = col.column_name;
        var i = 0;
        while (columnName[i] < 'a') ++i;
        columnName = columnName.substr(i - 1).toLowerCase();
        var config = [
            setAssign("content,remark,summary,text,description", formatters.limitFormatter),
            setAssign("image,picture", formatters.imgFormatter),
            setAssign("link", formatters.linkFormatter)].reduce(Object.assign)
        var assign = (config[columnName] || formatters.limitFormatter)(col.column_name);
        return assign;
    }
    export default {
        name: 'PageList',
        components: {
            TopBar
        },
        mounted: function () {
            var vm = this;
            var table_name = vm.table_name;
            var formatters = new Formatters(this);
            var visible_field_count = 0;
            var max_visible_field_count = 5;
            var events = {
                //编辑
                'click .edit': function(e, value, row, index) {
                    for (var p in row) {
                        if (row[p] && row[p][0] === '/' && row[p].indexOf('/Date') === 0) row[p] = slashDate2yyyyMMdd(row[p])
                    }
                    vm.$router.push('/pageone/'+table_name+'&'+JSON.stringify(row))
                },
                //删除
                'click .remove': function(e, value, row, index) {
                    //前台删除
                    $table.bootstrapTable("remove", {
                        field: "id",
                        values: [row.id]
                    });
                    //后台删除
                    vm.$http.get('http://122.193.9.83/XiangXi/DefaultHandler.ashx?method=delete'+table_name,{
                        params:{
                            data:JSON.stringify(row)
                        }
                    }).then(function (response) {
                        $table.bootstrapTable("refresh");
                    });
                }
            };
            var cols = [vm.$data.Columns.map(function (col) {
                return {
                    field: col.column_name,
                    title: col.column_description,
                    sortable: true,
                    visible: (visible_field_count++) < max_visible_field_count,
                    align: "center",
                    formatter: FormatterSelector(col),
                    events
                }
            }).concat([
                {
                    title: '操作',
                    sortable: true,
                    visible: true,
                    align: "center",
                    formatter: formatters.operateFormatter
                }
            ])];
            window.$table = $("#table");
            $("#table").bootstrapTable({
                striped: true,
                columns: cols
            })
        },
        methods: {
            goBack: function () {
                window.history.length > 1
                        ? this.$router.go(-1)
                        : this.$router.push('/')
            }
        },
        data: function () {
            var metadata2 = metadata[this.$route.params.mccaption];
            return Object.assign(metadata2, {
                nav_list: []
            });
        }
    }
</script>
<style scoped>
    .am-btn {
        margin: 0 10px;
    }

    .page-list {
        margin-top: 0;
        padding: 0;
    }
    #pageList{
        margin-top:10px;
    }
    .top-bar {
        overflow: hidden;
        background: url(../../public/i/metaicon/e.jpg) no-repeat;
        background-size: cover;
        z-index: -200;
    }

    .table-top-buttons, .table-top-buttons>*{
        float:left;
    }
    .table-top-buttons>*{
        margin:5px;
    }
</style>
