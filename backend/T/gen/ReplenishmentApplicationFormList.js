  
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
			el:"#cnavReplenishmentApplicationForm",
			data:{
				request:GetRequest(),
				nav_list:[],
				current_menu:'补货申请单'
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
				
				$('.excel-import-button-ReplenishmentApplicationForm').click(function () {
						$('.table_add_modal').modal('hide');
						$('.table_excel_modal-ReplenishmentApplicationForm').on('shown.bs.modal',function(){
							$('.table_excel_modal-ReplenishmentApplicationForm .modal-body').html('<input class="excel_file input-file form-control" type="file">');
							$(".excel_file").fileinput({
								language: 'zh', //设置语言
								uploadUrl: "/T/ExcelImport.ashx?fileType=ReplenishmentApplicationForm", //上传的地址
								allowedFileExtensions: ['xls','xlsx'],//接收的文件后缀,
								showUpload: true, //是否显示上传按钮
								dropZoneEnabled: true,
								showCaption: true,//是否显示标题
								browseClass: "btn btn-primary", //按钮样式             
								previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
								msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！"
							});
							$(".excel_file").on("fileuploaded",function(){
								//window.location.href = site_gen+"ReplenishmentApplicationFormList.html"
								if($table)$table.bootstrapTable('refresh');
							});
						})

						$('.table_excel_modal-ReplenishmentApplicationForm').modal('show');
					})
				
				var $table = $("#tableReplenishmentApplicationForm")
				//表格传区域ID参数
				var url = "";
				$table.attr("data-url", "/T/DefaultHandler.ashx?method=getReplenishmentApplicationFormList&data=" + JSON.stringify(GetRequest()) + "&");
				console.log($.buildColumns({"debug":null,"Columns":[{"column_name":"RAFApplicationNumber","column_description":"申请单编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"ApplicationNumber"},{"column_name":"RAFShelfNumber","column_description":"货架编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"ShelfNumber"},{"column_name":"RAFWarehouseNumber","column_description":"仓库编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"WarehouseNumber"},{"column_name":"RAFApplicationManualNumber","column_description":"申请人工号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"ApplicationManualNumber"},{"column_name":"RAFCategoryNumberOfGoods","column_description":"货品种类编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CategoryNumberOfGoods"},{"column_name":"RAFQuantityOfGoods","column_description":"货品数量","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"QuantityOfGoods"},{"column_name":"RAFApplicationDate","column_description":"申请日期","dbtype":"DATETIME","max_length":0,"pascal_column_name":"ApplicationDate"},{"column_name":"RAFApplicationStatus","column_description":"申请单状态","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"ApplicationStatus"},{"column_name":"RAFRemarks","column_description":"备注","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Remarks"}],"Children":null,"Parent":null,"r":null,"table_name":"ReplenishmentApplicationForm","table_name_en":"ReplenishmentApplicationForm","table_name_ch":"补货申请单","column_name":null,"column_description":null,"dbtype":null,"max_length":0}));
				$table.bootstrapTable({
					striped: true,
					height: getHeight(),
					columns: 
					$.buildColumns({"debug":null,"Columns":[{"column_name":"RAFApplicationNumber","column_description":"申请单编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"ApplicationNumber"},{"column_name":"RAFShelfNumber","column_description":"货架编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"ShelfNumber"},{"column_name":"RAFWarehouseNumber","column_description":"仓库编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"WarehouseNumber"},{"column_name":"RAFApplicationManualNumber","column_description":"申请人工号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"ApplicationManualNumber"},{"column_name":"RAFCategoryNumberOfGoods","column_description":"货品种类编号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CategoryNumberOfGoods"},{"column_name":"RAFQuantityOfGoods","column_description":"货品数量","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"QuantityOfGoods"},{"column_name":"RAFApplicationDate","column_description":"申请日期","dbtype":"DATETIME","max_length":0,"pascal_column_name":"ApplicationDate"},{"column_name":"RAFApplicationStatus","column_description":"申请单状态","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"ApplicationStatus"},{"column_name":"RAFRemarks","column_description":"备注","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Remarks"}],"Children":null,"Parent":null,"r":null,"table_name":"ReplenishmentApplicationForm","table_name_en":"ReplenishmentApplicationForm","table_name_ch":"补货申请单","column_name":null,"column_description":null,"dbtype":null,"max_length":0}),
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
