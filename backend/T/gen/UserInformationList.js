  
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
			el:"#cnavUserInformation",
			data:{
				request:GetRequest(),
				nav_list:[],
				current_menu:'用户信息'
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
				
				$('.excel-import-button-UserInformation').click(function () {
						$('.table_add_modal').modal('hide');
						$('.table_excel_modal-UserInformation').on('shown.bs.modal',function(){
							$('.table_excel_modal-UserInformation .modal-body').html('<input class="excel_file input-file form-control" type="file">');
							$(".excel_file").fileinput({
								language: 'zh', //设置语言
								uploadUrl: "/T/ExcelImport.ashx?fileType=UserInformation", //上传的地址
								allowedFileExtensions: ['xls','xlsx'],//接收的文件后缀,
								showUpload: true, //是否显示上传按钮
								dropZoneEnabled: true,
								showCaption: true,//是否显示标题
								browseClass: "btn btn-primary", //按钮样式             
								previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
								msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！"
							});
							$(".excel_file").on("fileuploaded",function(){
								//window.location.href = site_gen+"UserInformationList.html"
								if($table)$table.bootstrapTable('refresh');
							});
						})

						$('.table_excel_modal-UserInformation').modal('show');
					})
				
				var $table = $("#tableUserInformation")
				//表格传区域ID参数
				var url = "";
				$table.attr("data-url", "/T/DefaultHandler.ashx?method=getUserInformationList&data=" + JSON.stringify(GetRequest()) + "&");
				console.log($.buildColumns({"debug":null,"Columns":[{"column_name":"UILoginName","column_description":"登录名","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"LoginName"},{"column_name":"UINickname","column_description":"昵称","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Nickname"},{"column_name":"UIRealName","column_description":"真实姓名","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"RealName"},{"column_name":"UIHeadPortrait","column_description":"头像","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"HeadPortrait"},{"column_name":"UISubordinateDepartment","column_description":"所属部门","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"SubordinateDepartment"},{"column_name":"UIBooth","column_description":"电话","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Booth"},{"column_name":"UIPhoto","column_description":"照片","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Photo"},{"column_name":"UICustomerType","column_description":"用户类型","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CustomerType"},{"column_name":"UIUserLevel","column_description":"用户级别","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"UserLevel"},{"column_name":"UICode","column_description":"密码","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Code"}],"Children":null,"Parent":null,"r":null,"table_name":"UserInformation","table_name_en":"UserInformation","table_name_ch":"用户信息","column_name":null,"column_description":null,"dbtype":null,"max_length":0}));
				$table.bootstrapTable({
					striped: true,
					height: getHeight(),
					columns: 
					$.buildColumns({"debug":null,"Columns":[{"column_name":"UILoginName","column_description":"登录名","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"LoginName"},{"column_name":"UINickname","column_description":"昵称","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Nickname"},{"column_name":"UIRealName","column_description":"真实姓名","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"RealName"},{"column_name":"UIHeadPortrait","column_description":"头像","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"HeadPortrait"},{"column_name":"UISubordinateDepartment","column_description":"所属部门","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"SubordinateDepartment"},{"column_name":"UIBooth","column_description":"电话","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Booth"},{"column_name":"UIPhoto","column_description":"照片","dbtype":"NVARCHAR(4000)","max_length":0,"pascal_column_name":"Photo"},{"column_name":"UICustomerType","column_description":"用户类型","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CustomerType"},{"column_name":"UIUserLevel","column_description":"用户级别","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"UserLevel"},{"column_name":"UICode","column_description":"密码","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Code"}],"Children":null,"Parent":null,"r":null,"table_name":"UserInformation","table_name_en":"UserInformation","table_name_ch":"用户信息","column_name":null,"column_description":null,"dbtype":null,"max_length":0}),
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
