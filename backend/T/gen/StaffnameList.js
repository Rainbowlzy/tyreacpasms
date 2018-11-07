  
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
			el:"#cnavStaffname",
			data:{
				request:GetRequest(),
				nav_list:[],
				current_menu:'员工'
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
				
				$('.excel-import-button-Staffname').click(function () {
						$('.table_add_modal').modal('hide');
						$('.table_excel_modal-Staffname').on('shown.bs.modal',function(){
							$('.table_excel_modal-Staffname .modal-body').html('<input class="excel_file input-file form-control" type="file">');
							$(".excel_file").fileinput({
								language: 'zh', //设置语言
								uploadUrl: "/T/ExcelImport.ashx?fileType=Staffname", //上传的地址
								allowedFileExtensions: ['xls','xlsx'],//接收的文件后缀,
								showUpload: true, //是否显示上传按钮
								dropZoneEnabled: true,
								showCaption: true,//是否显示标题
								browseClass: "btn btn-primary", //按钮样式             
								previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
								msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！"
							});
							$(".excel_file").on("fileuploaded",function(){
								//window.location.href = site_gen+"StaffnameList.html"
								if($table)$table.bootstrapTable('refresh');
							});
						})

						$('.table_excel_modal-Staffname').modal('show');
					})
				
				var $table = $("#tableStaffname")
				//表格传区域ID参数
				var url = "";
				$table.attr("data-url", "/T/DefaultHandler.ashx?method=getStaffnameList&data=" + JSON.stringify(GetRequest()) + "&");
				console.log($.buildColumns({"debug":null,"Columns":[{"column_name":"SJobNumber","column_description":"工号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"JobNumber"},{"column_name":"SName","column_description":"姓名","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Name"},{"column_name":"SAffiliate","column_description":"公司","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Affiliate"},{"column_name":"SDepartment","column_description":"部门","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Department"},{"column_name":"SPost","column_description":"职位","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Post"},{"column_name":"SCommonModeOfContact","column_description":"联系方式","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CommonModeOfContact"},{"column_name":"SAlternateContactMode","column_description":"备用联系方式","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"AlternateContactMode"},{"column_name":"STimeOfEntry","column_description":"入职时间","dbtype":"DATETIME","max_length":0,"pascal_column_name":"TimeOfEntry"},{"column_name":"SDepartureTime","column_description":"离职时间","dbtype":"DATETIME","max_length":0,"pascal_column_name":"DepartureTime"},{"column_name":"SCode","column_description":"密码","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Code"}],"Children":null,"Parent":null,"r":null,"table_name":"Staffname","table_name_en":"Staffname","table_name_ch":"员工","column_name":null,"column_description":null,"dbtype":null,"max_length":0}));
				$table.bootstrapTable({
					striped: true,
					height: getHeight(),
					columns: 
					$.buildColumns({"debug":null,"Columns":[{"column_name":"SJobNumber","column_description":"工号","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"JobNumber"},{"column_name":"SName","column_description":"姓名","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Name"},{"column_name":"SAffiliate","column_description":"公司","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Affiliate"},{"column_name":"SDepartment","column_description":"部门","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Department"},{"column_name":"SPost","column_description":"职位","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Post"},{"column_name":"SCommonModeOfContact","column_description":"联系方式","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"CommonModeOfContact"},{"column_name":"SAlternateContactMode","column_description":"备用联系方式","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"AlternateContactMode"},{"column_name":"STimeOfEntry","column_description":"入职时间","dbtype":"DATETIME","max_length":0,"pascal_column_name":"TimeOfEntry"},{"column_name":"SDepartureTime","column_description":"离职时间","dbtype":"DATETIME","max_length":0,"pascal_column_name":"DepartureTime"},{"column_name":"SCode","column_description":"密码","dbtype":"NVARCHAR(50)","max_length":0,"pascal_column_name":"Code"}],"Children":null,"Parent":null,"r":null,"table_name":"Staffname","table_name_en":"Staffname","table_name_ch":"员工","column_name":null,"column_description":null,"dbtype":null,"max_length":0}),
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
