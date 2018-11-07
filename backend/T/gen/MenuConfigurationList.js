  
/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/07/2018 17:07:07
 * 生成版本：11/07/2018 17:06:29 
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
			el:"#cnavMenuConfiguration",
			data:{
				request:GetRequest(),
				nav_list:[],
				current_menu:'菜单配置'
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
				
				$('.excel-import-button-MenuConfiguration').click(function () {
						$('.table_add_modal').modal('hide');
						$('.table_excel_modal-MenuConfiguration').on('shown.bs.modal',function(){
							$('.table_excel_modal-MenuConfiguration .modal-body').html('<input class="excel_file input-file form-control" type="file">');
							$(".excel_file").fileinput({
								language: 'zh', //设置语言
								uploadUrl: "/T/ExcelImport.ashx?fileType=MenuConfiguration", //上传的地址
								allowedFileExtensions: ['xls','xlsx'],//接收的文件后缀,
								showUpload: true, //是否显示上传按钮
								dropZoneEnabled: true,
								showCaption: true,//是否显示标题
								browseClass: "btn btn-primary", //按钮样式             
								previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
								msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！"
							});
							$(".excel_file").on("fileuploaded",function(){
								//window.location.href = site_gen+"MenuConfigurationList.html"
								if($table)$table.bootstrapTable('refresh');
							});
						})

						$('.table_excel_modal-MenuConfiguration').modal('show');
					})
				
				var $table = $("#tableMenuConfiguration")
				//表格传区域ID参数
				var url = "";
				$table.attr("data-url", "/T/DefaultHandler.ashx?method=getMenuConfigurationList&data=" + JSON.stringify(GetRequest()) + "&");
				console.log($.buildColumns({"debug":null,"Columns":[{"column_name":"MCCaption","column_description":"标题","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Caption"},{"column_name":"MCParentTitle","column_description":"父级标题","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"ParentTitle"},{"column_name":"MCLink","column_description":"链接","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Link"},{"column_name":"MCMenuType","column_description":"菜单类型","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"MenuType"},{"column_name":"MCSequence","column_description":"顺序","dbtype":"INT","max_length":0,"pascal_column_name":"Sequence"},{"column_name":"MCDisplayName","column_description":"显示名称","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"DisplayName"},{"column_name":"MCPicture","column_description":"图片","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Picture"}],"Children":null,"Parent":null,"r":null,"table_name":"MenuConfiguration","table_name_en":"MenuConfiguration","table_name_ch":"菜单配置","column_name":null,"column_description":null,"dbtype":null,"max_length":0}));
				$table.bootstrapTable({
					striped: true,
					height: getHeight(),
					columns: 
					$.buildColumns({"debug":null,"Columns":[{"column_name":"MCCaption","column_description":"标题","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Caption"},{"column_name":"MCParentTitle","column_description":"父级标题","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"ParentTitle"},{"column_name":"MCLink","column_description":"链接","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Link"},{"column_name":"MCMenuType","column_description":"菜单类型","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"MenuType"},{"column_name":"MCSequence","column_description":"顺序","dbtype":"INT","max_length":0,"pascal_column_name":"Sequence"},{"column_name":"MCDisplayName","column_description":"显示名称","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"DisplayName"},{"column_name":"MCPicture","column_description":"图片","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Picture"}],"Children":null,"Parent":null,"r":null,"table_name":"MenuConfiguration","table_name_en":"MenuConfiguration","table_name_ch":"菜单配置","column_name":null,"column_description":null,"dbtype":null,"max_length":0}),
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
