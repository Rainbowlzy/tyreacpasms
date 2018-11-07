
/* ------------------------------------------------------------ *
 * 此文件由生成器引擎根据既有规则生成，所有手工的更改将会被覆盖
 * 生成时间：11/07/2018 19:52:11
 * 生成版本：11/07/2018 19:51:53 
 * 作者：路正遥
 * ------------------------------------------------------------ */
require([
    "vue",
    "jquery",
    "jquery.cookie",
    "bootstrap",
    "bootstrap-table",
    "bootstrap-select",
    "bootstrap-datetimepicker",
    "bootstrap-datetimepicker.zh-CN",
    "fileinput",
    "fileinput_locale_zh",
	"cmodules"
], function(Vue, $) {
    $(document).ready(function() {
        var request = GetRequest();
		if(request)
		for(var p in request){
			if(request[p].indexOf('%')!==-1)request[p] = decodeURIComponent(request[p])
		}
		var vm = {};
        var created = function () {
            $(".form_datetime").datetimepicker({
                format: "yyyy/mm/dd",
                language: "zh-CN",
                weekStart: 1,
                minView: 2,
                maxView: 4,
                startView: 4,
                autoclose: true
            })
            .on('changeDate', function (ev) {
				vm[$(this).attr('id')] = $(this).val();
            });
            $(".portrait").fileinput({
                language: "zh", //设置语言
                uploadUrl: "../ImageUpload.ashx?", //上传的地址
                allowedFileExtensions: ["jpg", "png", "gif", "JPEG"], //接收的文件后缀,
                showUpload: true, //是否显示上传按钮
                dropZoneEnabled: false,
                showCaption: true, //是否显示标题
                browseClass: "btn btn-primary", //按钮样式             
                previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
                msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！"
            })
			$(".portrait").on("fileuploaded", function(event, data, previewId, index) {
                $(".imageURL").val(data.response.imageURL);
				vm[$(this).attr('id')] = data.response.imageURL;
            });
			$("input[type=text]:first").focus();
			$('.excel-import-button-GoodsShelves').click(function () {
				$('.table_add_modal').modal('hide');
				$('.table_excel_modal-GoodsShelves').on('shown.bs.modal',function(){
					$('.table_excel_modal-GoodsShelves .modal-body').html('<input class="excel_file input-file form-control" type="file">');
					$(".excel_file").fileinput({
						language: 'zh', //设置语言
						uploadUrl: "/T/ExcelImport.ashx?fileType=GoodsShelves", //上传的地址
						allowedFileExtensions: ['xls','xlsx'],//接收的文件后缀,
						showUpload: true, //是否显示上传按钮
						dropZoneEnabled: true,
						showCaption: true,//是否显示标题
						browseClass: "btn btn-primary", //按钮样式             
						previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
						msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！"
					});
					$(".excel_file").on("fileuploaded",function(){
						window.location.href = site_gen+"GoodsShelvesList.html"
					});
				})

				$('.table_excel_modal-GoodsShelves').modal('show');
			})
			$("#GoodsShelves").show();
        };
        var method = {
			log(o){
				(o)
				return o;
			},
			
            save() {
                var datum = this.$data;
				datum.districtID = $.cookie("JTZH_districtID");
				datum.VersionNo = parseInt(datum.VersionNo)
                $.call("SaveGoodsShelves", JSON.parse(JSON.stringify(datum)), function(data) {
					if(!data) return alert('返回数据为空')
					if(!data.success) return alert(data.message)
					alert('保存成功！')
					history.go(-1)
                });
            },
            save_as_public() {
				this.$data.DataLevel = '019999'
				this.save();
            },

			back(){
				window.location.href = site_gen+"GoodsShelves.html?table=GoodsShelves"
			},

			changeSelect(val){
				selector = 'option.'+val.target.value;
				var s = ''
				// N级联动，子集选择控件设置为空
				$(selector).parent().val('')
				// 子集中所有option单位隐藏
				$(selector).parent().children().hide()
				// 显示联动部分
				$(selector).show()
				// 刷新父控件
				$(selector).parent().selectpicker('refresh').selectpicker('toggle')
				vm[$(selector).parent().attr("id")]=val.target.value
			}
        };
			if(request && request.data) request = JSON.parse(decodeURIComponent(decodeURIComponent((request||{}).data)));
			if (!request || request.id === undefined) {
				$.call("GetGoodsShelvesEmpty", {}, function(data) {
					data = data||{};
					data.table = 'GoodsShelves'
					vm = new Vue({
						el: "#GoodsShelves",
						data: data||{},
						computed:{
							request:function(){
								return GetRequest();
							}
						},
						methods: method,
						mounted:created
					});
					$(".selectpicker1").selectpicker('refresh')
					console.log('picker');
				});
				return;
			}else{
				$.call("GetGoodsShelvesList", {SearchKey: request.id}, function(data) {
					if(!data){
						alert(data);
						return;
					}
					if(!data.success){
						alert(data.message);
						return;
					}
					if((data.rows||[]).length===0){
						alert('异常，未找到该数据');
						return;
					}
					data = data.rows[0];
					data.table = 'GoodsShelves'
					vm = new Vue({
						el: "#GoodsShelves",
						data: data||{},
						computed:{
							request:function(){
								return GetRequest();
							}
						},
						methods: method,
						mounted:created
					});
					$(".selectpicker1").selectpicker('refresh')
					console.log('picker');
				});
				$(".selectpicker1").selectpicker('refresh')
			}
    });
});

