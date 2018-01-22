<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Step" ValidateRequest="false" CodeBehind="Step.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<%--<link href="/css/foundation.css" rel="stylesheet" type="text/css" />--%>
	<%--<script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>--%>
	<script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
	<script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>
	<script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>
	<link href="/js/uploadify/uploadify.css" rel="stylesheet" />
	<script src="/js/uploadify/jquery.uploadify.min.js"></script>

	<style>
		#w td {
			padding: 5px 5px;
			text-align: left;
			vertical-align: middle;
		}

		#w .title {
			vertical-align: middle;
			text-align: right;
			width: 80px;
			background-color: #f9f9f9;
		}
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<div class="top">
		<table cellpadding="0" cellspacing="0" style="width: 100%">
			<tr>

				<td><span class="title">步骤档案</span> <span class="subDesc">拖拽数据行进行排序</span>
				</td>
				<td></td>
				<td style="width: 150px;">
					<select id="fl_id_s" class="easyui-combobox" style="width: 200px; height: 25px;"
						data-options="valueField: 'fl_id',textField: 'fl_name',onChange:reloadst_id_s">
					</select></td>
				<td style="width: 150px;">
					<select id="st_id_s" class="easyui-combobox" style="width: 200px; height: 25px;"
						data-options="valueField: 'st_id',textField: 'st_no',onChange:reloadpart_id_s">
					</select></td>
				<td style="width: 150px;">
					<select id="part_id_s" class="easyui-combobox" style="width: 250px; height: 25px;"
						data-options="valueField: 'part_id',textField: 'part_no'">
					</select></td>
				<td style="width: 120px;"><a class="topsearchBtn">筛选步骤</a></td>

				<td style="width: 100px">
					<a class="topaddBtn">新增档案</a>
				</td>
				<td style="width: 100px">
					<a class="toppenBtn">编辑所选</a>
				</td>
				<td style="width: 120px">
					<a class="topdelBtn">删除所选</a>
				</td>
				<td style="width: 100px">
					<a class="topexcelBtn">生成xls</a>
				</td>
			</tr>
		</table>
	</div>
	<!-- 数据表格  -->
	<table id="tb" title="工位列表" style="width: 98%;">
	</table>
	<!-- 加载窗口 -->
	<div id="excelw" style="padding: 10px; text-align: center; font-size: 22px; padding-top: 20px; vertical-align: middle; visibility: hidden" title="生成excel">
		<br />
		<h4>系统将生成9份Excel生产线文档</h4>
		<br />
		<h4 style="font-size: 16px;">请稍候...</h4>
		<br />
		<img src="/image/admin/15.gif" />
	</div>

	<!-- 编辑窗口 -->
	<div id="w" style="padding: 10px; visibility: hidden" title="步骤编辑">
		<table cellpadding="0" cellspacing="0">
			<tr>
				<td class="title">
					<p>
						步骤名称：
					</p>
				</td>
				<td>
					<input id="step_name" type="text" class="text" style="width: 230px;" />
				</td>
				<%-- <td rowspan="10" style="vertical-align: top; text-align: left">
                    <table id="odsdg" class="easyui-datagrid" title="ODS详细步骤" style="width: 440px; height: 460px"
                        data-options="iconCls: 'icon-edit',singleSelect: true, toolbar: '#tooltb',onClickCell: onClickCell">
                        <thead>
                            <tr>
                                <th data-options="field:'ods_name',width:200,editor:'textbox'">主要步骤</th>
                                <th data-options="field:'ods_keywords',width:200,editor:'textbox'">关键点</th>
                            </tr>
                        </thead>
                    </table>

                    <div id="tooltb" style="height: auto">
                        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="append()">增加步骤</a>
                        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true" onclick="removeit()">删除</a>
                        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true" onclick="accept()">完成编辑</a>
                    </div>

                </td>--%>
			</tr>
			<tr>
				<td class="title" style="width: 110px;">
					<p>
						所属流水线：
					</p>
				</td>
				<td>
					<select id="fl_id" class="easyui-combobox" style="width: 230px; height: 25px;"
						data-options="valueField: 'fl_id',textField: 'fl_name',onChange:function(){reloadst_id();}">
					</select>
				</td>
			</tr>
			<tr>
				<td class="title" style="width: 110px;">
					<p>
						所属工位：
					</p>
				</td>
				<td>
					<select id="st_id" class="easyui-combobox" style="width: 230px; height: 25px;"
						data-options="valueField: 'st_id',textField: 'st_no'">
					</select>
				</td>
			</tr>
			<tr>
				<td class="title" style="width: 110px;">
					<p>
						所属部件配置：
					</p>
				</td>
				<td>
					<select id="part_id" class="easyui-combobox" style="width: 230px; height: 25px;"
						data-options="valueField: 'part_id',textField: 'part_no',onChange:function(){reloadbom_id();}">
					</select>
				</td>
			</tr>
			<tr>
				<td class="title" style="width: 110px;">
					<p>
						所需零件：
					</p>
				</td>
				<td>
					<select id="bom_id" class="easyui-combobox" style="width: 230px; height: 25px;"
						data-options="valueField: 'bom_id',textField: 'bom_PN'">
					</select>
				</td>
			</tr>
			<tr>
				<td class="title">
					<p>
						零件数量：
					</p>
				</td>
				<td>
					<input id="bom_count" type="text" class="text" style="width: 130px;" />
				</td>
			</tr>
			<tr>
				<td class="title">
					<p>
						理想工时：
					</p>
				</td>
				<td>
					<input id="step_clock" type="text" class="text" style="width: 130px;" />
					S
				</td>
			</tr>
			<tr>
				<td class="title">
					<p>
						电气信号：
					</p>
				</td>
				<td>
					<input id="step_plccode" type="text" class="text" style="width: 130px;" />
				</td>
			</tr>
			<%--<tr>
				<td class="title">
					<p>
						条码匹配开始位：
					</p>
				</td>
				<td>
					<input id="barcode_start" type="text" class="text" style="width: 130px;" />
				</td>
			</tr>
			<tr>
				<td class="title">
					<p>
						条码匹配长度：
					</p>
				</td>
				<td>
					<input id="barcode_number" type="text" class="text" style="width: 130px;" />
				</td>
			</tr>--%>
			<tr>
				<td class="title">
					<p>步骤图示：</p>
				</td>
				<td>&nbsp;&nbsp;<img id="step_pic" src="/image/admin/Dashboard.png" style="height: 80px;"><br />
					<br />
					<input type="file" name="uploadify" id="uploadify" />
					<div id="uploadify-queue" style="height: 0px; width: 0px"></div>
				</td>
			</tr>
			<tr>
				<td class="title">
					<p>
						步骤描述：
					</p>
				</td>
				<td>
					<input id="step_desc" type="text" class="text" style="width: 230px;" />
				</td>
			</tr>

		</table>
	</div>
	<!-- 编辑窗口 - footer -->
	<div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility: hidden">
		<img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
			class="saveBtn" id="saveBtn">保存</a>
	</div>


	<script>

		/****************       全局变量          ***************/
		var stepid;               //要编辑的id
		var dg = $('#tb');      //表格
		var isEdit = false;     //是否为编辑状态
		var tmpNO;              //工号

		/****************       DOM加载          ***************/
		$(function () {
			$.ajaxSetup({
				cache: false //关闭AJAX缓存
			});
			//新增按钮点击
			$('.topaddBtn').first().click(function () {
				isEdit = false;
				$('#w').window('open');
			});

			//编辑按钮点击
			$('.toppenBtn').first().click(function () {
			    var selRows = dg.datagrid('getSelections');
			    if (selRows.length > 1) {
			        alert('每次只能删除一条记录，请重新选取');
			        return;
			    } else if (selRows.length == 0) {
			        alert('请选择一条记录进行删除');
			        return;
			    }
			    var row = selRows[0];
			    $('#part_id').combobox('select', row.part_id);


                //11111111
				isEdit = true;
				initEidtWidget();
			});

			//删除按钮
			$('.topdelBtn').first().click(function () {
				deleteInfos();
			});

			//搜索按钮
			$('.topsearchBtn').first().click(function () {
				searchInfos();
			});

			//保存按钮
			$('#saveBtn').bind('click', function () {
				saveStep(isEdit);
			});
			//excel按钮
			$('.topexcelBtn').first().click(function () {
				createExcel();
			});


			//excel窗口加载
			$('#excelw').window({
				modal: true,
				closed: true,
				minimizable: false,
				maximizable: false,
				collapsible: false,
				width: 450,
				height: 280,
				top: 20,
				onBeforeOpen: function () { $('#excelw').css('visibility', 'visible'); }
			});
			//编辑窗口加载
			$('#w').window({
				modal: true,
				closed: true,
				minimizable: false,
				maximizable: false,
				collapsible: false,
				width: 410,
				height: 580,
				footer: '#ft',
				top: 20,
				onBeforeClose: function () { clearw(); },
				onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
			});

			//所属工位下拉框数据加载  
			reloadst_id();
			reloadfl_id();
			reloadPart_id();
			reloadbom_id();
			//  reloadfl_ids();

			//数据列表加载
			dg = $('#tb').datagrid({
				url: "/HttpHandlers/StepHandler.ashx?method=queryStepList",
				rownumbers: true,
				pagination: true,
				rownumbers: true,
				singleSelect: true,
				collapsible: false,
				striped: true,
				fitColumns: true,
				columns: [[
						    { field: 'step_id', title: 'id', hidden: true },
							{ field: 'fl_id', title: '流水线id', hidden: true },
							{ field: 'st_id', title: '工位id', hidden: true },
							{ field: 'part_id', title: '部件id', hidden: true },
							{ field: 'bom_id', title: '零件id', hidden: true },
						    { field: 'step_pic', title: '图片', formatter: function (value, row, index)  { return '<img src="' + row.step_pic + '" style="height:80px;" />'; }, width: 100, align: "center" },
								 { field: 'step_order', title: '排序序号', hidden: true },
							{ field: 'step_name', title: '步骤名称', width: 200, align: "center" },

							{ field: 'fl_name', title: '所属流水线', width: 200, align: "center" },
				{ field: 'st_name', title: '所属工位', width: 200, align: "center" },
				{ field: 'part_no', title: '所属部件', width: 200, align: "center" },
				{ field: 'bom_PN', title: '所用零件', width: 200, align: "center" },
				{ field: 'bom_count', title: '零件数量', width: 80, align: "center" },
				 { field: 'step_clock', title: '理想工时', width: 80, align: "center" },
							{ field: 'step_plccode', title: '电气信号', width: 80, align: "center" },
							//{ field: 'barcode_start', title: '条码匹配开始位', width: 200, align: "center" },
							//{ field: 'barcode_number', title: '条码匹配长度', width: 200, align: "center" },
				{ field: 'step_desc', title: '步骤描述', width: 200, align: "center" }
				]],
				onLoadSuccess: function () {
					$(this).datagrid('enableDnd');
				},
				onDrop: function (targetRow, sourceRow, point) {
					sortingData(targetRow.step_id, targetRow.step_order, sourceRow.step_id, sourceRow.step_order, point);
				}
			});
			//数据列表分页
			dg.datagrid('getPager').pagination({
				pageList: [1, 10, 20, 30, 50],
				//layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
			});

			//图片上传
			$("#uploadify").uploadify({
				'swf': '/js/uploadify/uploadify.swf',
				'uploader': '/HttpHandlers/UploadfyHandler.ashx?method=uploadStepImg',
				'buttonText': '上传图片',
				'fileTypeDesc': 'Image Files',
				'fileTypeExts': '*.jpg; *.png',
				//'formData': { 'someKey': 'someValue', 'someOtherKey': 1 },
				//上传文件页面中，你想要用来作为文件队列的元素的id, 默认为false  自动生成,  不带#
				'queueID': 'fileQueue',
				//选择文件后自动上传
				'auto': true,
				//设置为true将允许多文件上传
				'multi': false,
				'onUploadSuccess': function (file, data, response) {
					setImg(file, data, response);
					//alert('The file ' + file.name + ' was successfully uploaded with a response of ' + response + ':' + data);
				}
			});
			reloadfl_id_s();
			reloadst_id_s();
			reloadpart_id_s();


		});


		/****************       主要业务程序          ***************/

		//生成excel
		function createExcel() {
			$('#excelw').window('open');
			$.ajax({
				type: "POST",
				async: true,
				url: "/HttpHandlers/ExcelHandler.ashx",
				data: "method=createCX11",
				success: function (data) {
				    
					if (data == 'true')
						alert('已生成');
					else
						alert('操作失败');
					$('#excelw').window('close');
				},
				error: function () {
					alert('生成过程中出现异常');
					$('#excelw').window('close');
				}
			});
		}

		//排序
		function sortingData(t_step_id, t_step_order, s_step_id, s_step_order, point) {
			$.ajax({
				type: "POST",
				async: true,
				url: "/HttpHandlers/StepHandler.ashx",
				data: "method=sorting&step_id=" + s_step_id + "&step_order=" + t_step_order + "&point=" + point,
				success: function (data) {
					if (data == 'true') {
						dg.datagrid('reload');
					}
					else alert('操作失败');
				},
				error: function () {
				}
			});
		}

		//新增 / 编辑  
		function saveStep() {
			//              odsdg ods_keywords
			//var rows = $('#odsdg').datagrid('getRows');
			// var columns = $('#odsdg').datagrid('getColumnFields');
			// alert(rows);
			//alert(rows.length);
			//var odsNameArr = new Array();
			//var odsKeywordArr = new Array();

			//for (var i = 0; i < rows.length; i++) {
			//    var ods_name = $('#odsdg').datagrid('getRows')[i]["ods_name"];
			//    var ods_keywords = $('#odsdg').datagrid('getRows')[i]["ods_keywords"];
			//    if (ods_name != undefined && ods_name != null && ods_name != '') {
			//        odsNameArr.push(ods_name);
			//        odsKeywordArr.push(ods_keywords);
			//    }

			//}
			//ods_name ods_keywords

			var step_id = isEdit == true ? stepid : 0;
			var fl_id = $('#fl_id').combo('getValue');
			var st_id = $('#st_id').combo('getValue');
			var bom_id = $('#bom_id').combo('getValue');
			var part_id = $('#part_id').combo('getValue');
			var step_name = $('#step_name').val();
			var step_clock = $('#step_clock').val();
			var step_plccode = $('#step_plccode').val();
			//var barcode_start = $('#barcode_start').val();
		    //var barcode_number = $('#barcode_number').val();
			var barcode_start = 0;
			var barcode_number = 0;
			var bom_count = $('#bom_count').val();
			var step_pic = $('#step_pic').attr('src');
			var step_desc = $('#step_desc').val();
			//var odsName = (odsNameArr.length > 0) ? odsNameArr.join('|') : "";
			//var odsKeyword = (odsKeywordArr.length > 0) ? odsKeywordArr.join('|') : "";
			//alert('abc');

			var model = {
				step_id: step_id,
				fl_id: fl_id,
				st_id: st_id,
				bom_id: bom_id,
				part_id: part_id,
				step_name: step_name,
				step_clock: step_clock,
				step_plccode: step_plccode,
				barcode_start: barcode_start,
				barcode_number: barcode_number,
				bom_count: bom_count,
				step_pic: step_pic,
				step_desc: step_desc,
				//odsName: odsName,
				//odsKeyword: odsKeyword,
				method: 'saveStep'
			};

			saveTheStep(model);
		}
		function saveTheStep(model) {
			$.ajax({
				type: "POST",
				async: false,
				url: "/HttpHandlers/StepHandler.ashx",
				data: model,
				success: function (data) {
					if (data == 'true') {
						alert('已保存');
						dg.datagrid('reload');
					}
					else alert('保存失败');
					$('#w').window('close');
				},
				error: function () {
				}
			});
		}

		//编辑时加载窗体数据
		function initEidtWidget() {
			var selRows = dg.datagrid('getSelections');
			if (selRows.length > 1) {
				alert('每次只能编辑一条记录，请重新选取');
				return;
			} else if (selRows.length == 0) {
				alert('请选择一条记录进行编辑');
				return;
			}
			//窗体数据初始化              
			var row = selRows[0];
			


			stepid = row.step_id;

			$('#fl_id').combobox('select', row.fl_id);
			$('#st_id').combobox('select', row.st_id);
			$('#bom_id').combobox('select', row.bom_id);
			$('#part_id').combobox('select', row.part_id);

			$('#step_name').val(row.step_name);
			$('#step_clock').val(row.step_clock);
			$('#step_plccode').val(row.step_plccode);
			//$('#barcode_start').val(0);
			//$('#barcode_number').val(0);
			$('#bom_count').val(row.bom_count);
			$('#step_desc').val(row.step_desc);

			$('#step_pic').attr('src', row.step_pic);


			//$.ajax({
			//    url: "/HttpHandlers/StepHandler.ashx",
			//    data: encodeURI("step_id=" + row.step_id + "&method=queryODSByStepid"),
			//    success: function (data) {
			//        var d = jQuery.parseJSON(data);
			//        $('#odsdg').datagrid('loadData', d);
			//    },
			//    error: function () {
			//    }
		    //});
			reloadst_id();
			reloadbom_id();
			$('#w').window('open');
		}
		function deleteInfos() {
			var selRows = dg.datagrid('getSelections');
			if (selRows.length > 1) {
				alert('每次只能删除一条记录，请重新选取');
				return;
			} else if (selRows.length == 0) {
				alert('请选择一条记录进行删除');
				return;
			}
			var row = selRows[0];
			$.ajax({
				url: "/HttpHandlers/StepHandler.ashx",
				data: encodeURI("step_id=" + row.step_id + "&method=deleteStep"),
				async: false,
				success: function (data) {
					if (data == 'true') {
						alert('已删除');
						dg.datagrid('reload');
					}
					else alert('删除失败');
				},
				error: function () {
				}
			});
		}


		/****************       辅助业务程序          ***************/

		function reloadfl_id() {
			$('#fl_id').combobox('reload', '/HttpHandlers/FlowlineHandler.ashx?method=queryFlowlingForStepEditing');
		}
		function reloadst_id() {
			$('#st_id').combobox('loadData', '[]');
			var fl_id = $('#fl_id').combo('getValue');
			$('#st_id').combobox('reload', '/HttpHandlers/StationHandler.ashx?method=queryStationForStepEditing&fl_id=' + fl_id);
		}
		function reloadPart_id() {
			$('#part_id').combobox('reload', '/HttpHandlers/PartHandler.ashx?method=queryPartForStepEditing');
		}

		function reloadbom_id() {
			$('#bom_id').combobox('loadData', '[]');
			var part_id = $('#part_id').combo('getValue');
			$('#bom_id').combobox('reload', '/HttpHandlers/BOMHandler.ashx?method=queryBOMForStepEditing&part_id=' + part_id);      //修改
		}

		function reloadfl_id_s() {
			$('#fl_id_s').combobox('reload', '/HttpHandlers/FlowlineHandler.ashx?method=queryFlowlingForEditing');
		}
		function reloadst_id_s() {
			$('#st_id_s').combobox('loadData', '[]');
			var fl_id = $('#fl_id_s').combo('getValue');
			$('#st_id_s').combobox('reload', '/HttpHandlers/StationHandler.ashx?method=queryStationForStepEditing&fl_id=' + fl_id);
		}
		function reloadpart_id_s() {
			$('#part_id_s').combobox('reload', '/HttpHandlers/PartHandler.ashx?method=queryPartForStepSearching');
		}

		function searchInfos() {

			var fl_id = $('#fl_id_s').combo('getValue');
			var st_id = $('#st_id_s').combo('getValue');
			var part_id = $('#part_id_s').combo('getValue');
			var queryParams = {
				fl_id: fl_id,
				part_id: part_id,
				st_id: st_id
			};
			dg.datagrid({
				queryParams: queryParams
			});
			dg.datagrid('reload');
		}
		//上传完图片后，给编辑框中图片附链接
		function setImg(file, data, response) {
			$('#step_pic').attr('src', data);
		}


		/**********************************************/
		/*****************   窗体程序 *******************/
		/**********************************************/
		var editIndex = undefined;
		function endEditing() {
			if (editIndex == undefined) { return true }
			if ($('#odsdg').datagrid('validateRow', editIndex)) {
				$('#odsdg').datagrid('endEdit', editIndex);
				//$('#odsdg').datagrid('refreshRow', editIndex);
				//alert($('#odsdg').datagrid('getRows')[editIndex]['ods_name']);
				//$('#odsdg').datagrid('getRows')[editIndex]['productname'] = productname;
				//$('#odsdg').datagrid('getRows')[editIndex]['productname'] = productname;
				editIndex = undefined;
				return true;
			} else {
				return false;
			}
		}
		function onClickCell(index, field) {
			if (editIndex != index) {
				if (endEditing()) {
					$('#odsdg').datagrid('selectRow', index)
									.datagrid('beginEdit', index);
					var ed = $('#odsdg').datagrid('getEditor', { index: index, field: field });
					if (ed) {
						($(ed.target).data('textbox') ? $(ed.target).textbox('textbox') : $(ed.target)).focus();
					}
					editIndex = index;
				} else {
					setTimeout(function () {
						$('#odsdg').datagrid('selectRow', editIndex);
					}, 0);
				}
			}
		}

		function append() {
			if (endEditing()) {
				$('#odsdg').datagrid('appendRow', { ods_name: "", ods_keywords: "" });
				editIndex = $('#odsdg').datagrid('getRows').length - 1;
				$('#odsdg').datagrid('selectRow', editIndex).datagrid('beginEdit', editIndex);
			}
		}
		function removeit() {
			if (editIndex == undefined) { return }
			$('#odsdg').datagrid('cancelEdit', editIndex)
							.datagrid('deleteRow', editIndex);
			editIndex = undefined;
		}

		function accept() {
			if (endEditing()) {
				$('#odsdg').datagrid('acceptChanges');
			}
		}

		//编辑窗口关闭清空数据
		function clearw() {
			$('#fl_id').combo('clear');
			$('#st_id').combo('clear');
			$('#bom_id').combo('clear');
			$('#part_id').combo('clear');
			$('#step_name').val('');
			$('#step_clock').val('');
			$('#step_plccode').val('');
			//$('#barcode_start').val('');
			//$('#barcode_number').val('');
			$('#bom_count').val('');
			$('#step_desc').val('');
			$('#step_pic').attr('src', '/image/admin/Dashboard.png');
			// $('#odsdg').datagrid('loadData', { total: 0, rows: [] });
		}
	</script>


</asp:Content>








<%-- <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;步骤档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;步骤信息
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100px;">
                    步骤名称：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>

                                <td style="width: 100px;">
                    所属工位：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="drp_stname" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                                <td style="width: 100px;">
                    部件名称：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="drp_bomanme" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                                <td style="width: 100px;">
                    部件数量：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_bomcount" runat="server" Width="200px"></asp:TextBox>
                </td>

            </tr>


            <tr>
                <td style="width: 100px;">
                    步骤节拍：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_clock" runat="server" Width="200px"></asp:TextBox>
                </td>
                                <td style="width: 100px;">
                    步骤描述：&nbsp;
                </td>
                <td style="width: 200px;">
                   <asp:TextBox ID="txt_desc" runat="server" Width="200px"></asp:TextBox>
                </td>
                                <td style="width: 100px;">
                    上传图片：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
                                <td style="width: 100px; text-align: right">
                    <asp:Button ID="BtSave" runat="server" Text="新增步骤"  CssClass="addBtn" 
                        onclick="BtSave_Click" />
                </td>
            </tr>

        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="step_id" CssClass="gridview" 
        onpageindexchanging="GridView1_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="步骤ID"> 
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("step_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("step_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="步骤名称">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("step_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_ename" runat="server" Text='<%# Bind("step_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="所属工位">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("st_name") %>'></asp:Label>
                    <asp:Label ID="lb_stid" runat="server" Text='<%# Bind("st_id") %>' Visible="false"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drp_stname" runat="server" Width="200px">
                    </asp:DropDownList>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部件名称">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("bom_name") %>'></asp:Label>
                    <asp:Label ID="lb_bomid" runat="server" Text='<%# Bind("bom_id") %>' Visible="false"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drp_bomname" runat="server" Width="200px">
                    </asp:DropDownList>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部件数量">
            <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("bom_count") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_ecount" runat="server" Text='<%# Bind("bom_count") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="步骤节拍">
            <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("step_clock") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_eclock" runat="server" Text='<%# Bind("step_clock") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="步骤描述">
            <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("step_desc") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_edesc" runat="server" Text='<%# Bind("step_desc") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="图片" ItemStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_pic1" runat="server" Text='<%# Bind("step_pic") %>' Visible="false"></asp:Label>
                                                <img id ="img_pic" height="100px" width="60px" alt="<%#Eval("step_pic")%>" src="./image/step/<%#Eval("step_pic")%>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                <EditItemTemplate>
                    <asp:FileUpload ID="upd_pic" runat="server" /><asp:Label ID="lbl_pic" runat="server" Text='<%# Bind("step_pic") %>' Visible="false"></asp:Label><img id ="img_pic" height="100px" width="60px" alt="<%#Eval("step_pic")%>" src="./image/step/<%#Eval("step_pic")%>" />
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能">
                <ItemTemplate>
                    <asp:ImageButton ID="BtEdit" runat="server" ImageUrl="~/image/admin/pen.png" 
                        onclick="BtEdit_Click"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtDel" runat="server" ImageUrl="~/image/admin/delete1.png" 
                        onclick="BtDel_Click"  />
                </ItemTemplate>

                <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/image/admin/save.png" onclick="BtSave_Click1" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtCancel" runat="server" 
                        ImageUrl="~/image/admin/undo.png"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>--%>

