  
/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/07/2018 19:52:09
 * 生成版本：11/07/2018 19:51:53 
 * 作者：路正遥
 * ------------------------------------------------------------ */
console.group("正在加载脚本")
require([
	"vue", 
	"jquery", 
	"jquery.cookie", 
	"bootstrap", 
	"bootstrap-table", 
	"bootstrap-select", 
    "bootstrap-table-zh-CN",
	"bootstrap-datetimepicker", 
//	"bootstrap-datetimepicker.zh-CN",
	"fileinput",
	"fileinput_locale_zh",
	"cmodules"
], function(Vue,$) {
console.log("正在加载自定义逻辑")
$(document).ready(function(){
		var vm = new Vue({
			el:"#cnavCustomertype",
			data:{
				request:GetRequest(),
				nav_list:[],
				current_menu:'客户'
			},
			computed:{
			},
			methods:{
				setNavi:function(link,id){
					return setNavi(link,id);
				},
				parseEmpty: function(nav){
					return "javascript:parent.location.href='/T/1_index/business.html?data="+encodeURIComponent(JSON.stringify(nav))+"'"
				}
			},
			mounted:function(){
				
				$('.excel-import-button-Customertype').click(function () {
						$('.table_add_modal').modal('hide');
						$('.table_excel_modal-Customertype').on('shown.bs.modal',function(){
							$('.table_excel_modal-Customertype .modal-body').html('<input class="excel_file input-file form-control" type="file">');
							$(".excel_file").fileinput({
								language: 'zh', //设置语言
								uploadUrl: "/T/ExcelImport.ashx?fileType=Customertype", //上传的地址
								allowedFileExtensions: ['xls','xlsx'],//接收的文件后缀,
								showUpload: true, //是否显示上传按钮
								dropZoneEnabled: true,
								showCaption: true,//是否显示标题
								browseClass: "btn btn-primary", //按钮样式             
								previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
								msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！"
							});
							$(".excel_file").on("fileuploaded",function(){
								//window.location.href = site_gen+"CustomertypeList.html"
								if($table)$table.bootstrapTable('refresh');
							});
						})

						$('.table_excel_modal-Customertype').modal('show');
					})
				
				var $table = $("#tableCustomertype")
				//表格传区域ID参数
				var url = "";
				$table.attr("data-url", "/T/DefaultHandler.ashx?method=getCustomertypeList&data=" + JSON.stringify(GetRequest()) + "&");
				console.log($.buildColumns({"debug":null,"Columns":[{"column_name":"CCustomerNumber","column_description":"客户编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CustomerNumber"},{"column_name":"CName","column_description":"姓名","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Name"},{"column_name":"CChairperson","column_description":"性别","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Chairperson"},{"column_name":"CCall","column_description":"称呼","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Call"},{"column_name":"CCommonModeOfContact","column_description":"联系方式","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CommonModeOfContact"},{"column_name":"CAddress","column_description":"地址","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Address"}],"Children":null,"Parent":null,"r":null,"table_name":"Customertype","table_name_en":"Customertype","table_name_ch":"客户","column_name":null,"column_description":null,"dbtype":null,"max_length":0}));
				$table.bootstrapTable({
					striped: true,
					height: getHeight(),
					columns: 
					$.buildColumns({"debug":null,"Columns":[{"column_name":"CCustomerNumber","column_description":"客户编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CustomerNumber"},{"column_name":"CName","column_description":"姓名","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Name"},{"column_name":"CChairperson","column_description":"性别","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Chairperson"},{"column_name":"CCall","column_description":"称呼","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Call"},{"column_name":"CCommonModeOfContact","column_description":"联系方式","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CommonModeOfContact"},{"column_name":"CAddress","column_description":"地址","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Address"}],"Children":null,"Parent":null,"r":null,"table_name":"Customertype","table_name_en":"Customertype","table_name_ch":"客户","column_name":null,"column_description":null,"dbtype":null,"max_length":0}),
				});
		
				$table.bootstrapTable('refresh');
				$("input[type=text]:first").focus();
				setTimeout(function(){
				$table.bootstrapTable('refresh');},5000)
				
			}
		})
		
		var request = GetRequest();
		if(request && request.nav){
			try{

				$.call("querynav2",JSON.parse(request.nav),function(resp){
					vm.$data.nav_list = resp
				})
			}catch (e) {
				console.error(e)
			}
		}
	})
	
	console.log("加载自定义逻辑完毕")
})
console.log("加载脚本完毕")
console.groupEnd();
