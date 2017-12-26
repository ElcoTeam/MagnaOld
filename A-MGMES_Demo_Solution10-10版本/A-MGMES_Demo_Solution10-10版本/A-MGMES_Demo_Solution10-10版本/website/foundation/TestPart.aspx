<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_TestPart" ValidateRequest="false" Codebehind="TestPart.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <style>
        #w td { padding: 5px 5px; text-align: left; vertical-align: middle; }
        #w .title { vertical-align: middle; text-align: right; width: 80px; background-color: #f9f9f9; }
    </style>
    <style type="text/css">
		.drag-item{
            display:block;
            margin-left:1px;
			list-style-type:decimal;
            border:1px solid #ccc;
            background:#fafafa;
			color:#444;
            padding:5px;
		}
		.indicator{
			position:absolute;
            padding-left:2px;
			font-size:5px;
			width:30px;
			height:10px;
            display:list-item
		}
	</style>
    <script src="../js/validate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">检测配件</span> 
                </td>
                <td style="width: 120px">
                    <a class="topaddBtn">新增档案</a>
                </td>
                <td style="width: 120px">
                    <a class="toppenBtn">编辑所选</a>
                </td>
                <td style="width: 120px">
                    <a class="topdelBtn">删除所选</a>
                </td>
            </tr>
        </table>
    </div>

    <!-- 数据表格  -->
    <table id="tb" title="检测配件列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="检测配件编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title">
                    <p>
                        配件编号：
                    </p>
                </td>
                <td>
                     <select id="com_partid" data-options="valueField:'part_id',textField:'part_name'"  class="easyui-combobox" style="width: 130px; height: 30px;">
                    </select>
                </td>

                <td rowspan="6" style="width: 450px; padding-left: 20px; vertical-align: top">
                    <!--检测站数据表格  -->
                    <table id="testparttb" title="检测站" style="width: 100%; height: 430px;">
                    </table>

                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        检测站：
                    </p>
                </td>
                <td>
                    <select id="com_station" data-options="valueField:'st_id',textField:'st_no'"  class="easyui-combobox" style="width: 130px; height: 30px;">
                    </select>
                </td>
            </tr>
            <tr>
                <td  class="title"></td>
                <td> <span class="title"></span><span class="subDesc" style=" color:red;">请按正确次序填写检测项!</span></td>
            </tr>
            <tr>
                <td class="title"></td>
                <td>被以下检测项用到：</td>
            </tr>

            <tr>
                <td class="title"></td>
                <td>
                    <div style="height: 260px; border: solid 1px #e5e3e3; padding: 5px" id="div_pstation"></div>
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
        var pid;               //要编辑的id
        var dg;      //数据表格
        var testpartdg; //检测项表格
        var isEdit = false;     //是否为编辑状态
        var testIDArr = new Array();          //该站被哪些检测项用到用到


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
                isEdit = true;
                initEidtWidget();
            });

            //删除按钮
            $('.topdelBtn').first().click(function () {
                deleteInfos();
            });

            //保存按钮
            $('#saveBtn').bind('click', function () {
                saveTestPart(isEdit);
            });
            //配件下拉框
            $('#com_partid').combobox(
           {
               url: '/HttpHandlers/PartHandler.ashx?method=QueryPartForPartidList',
               onLoadSuccess: function () {
                   var val = $(this).combobox('getData');
                   $(this).combobox('select', val[0].part_id);
               }
           });
            $('#com_station').combobox(
          {
              url: '/HttpHandlers/StationHandler.ashx?method=QueryStationStnoList',
              onLoadSuccess: function () {
                  var val = $(this).combobox('getData');
                  $(this).combobox('select', val[0].st_id);
              }
          });

            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 850,
                height: 550,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });

            //数据列表加载
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/TestPartHandler.ashx?method=queryTestPartList",
                rownumbers: true,
                pagination: true,
                pageSize: 20,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                columns: [[
                      { field: 'p_id', title: 'ID', hidden: true },
                      { field: 'partid', title: '部件编号', width: 50, align: "center" },
                      { field: 'partname', title: '部件编号', width: 100, align: "center" },
                       { field: 'stationno', title: '站号', width: 100, align: "center" },
                       { field: 'testcaption', title: '检测项名称', width: 100, align: "center" },
                      { field: 'tIDS', title: '检测项id集合', hidden: true }
                      //{ field: 'sorting', title: '排序', width: 200, align: "center" },
                     
                ]]
            }); 
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });

            //检测配件加载
            testpartdg = $('#testparttb').datagrid({
                url: "/HttpHandlers/TestHandler.ashx?method=QueryTestList",
                rownumbers: true,
                collapsible: false,
                columns: [[
                      { field: 'ck', checkbox: true },
                      { field: 't_id', title: 'ID', align: "center", width: 50, hidden: "true" },
                      { field: 'testcaption', title: '检测项名称', align: "center", width: 200 },
                      
                ]],
                onCheck: function (index, row) {
                    refreshTestPartDiv();
                },
                onUncheck: function (index, row) {
                    refreshTestPartDiv();
                },
                onCheckAll: function (rows) {
                    refreshTestPartDiv();
                },
                onUncheckAll: function (rows) {
                    refreshTestPartDiv();
                }
            });
           
        });
            /****************       主要业务程序          ***************/

            //新增 / 编辑  
            function saveTestPart() {
                //            
                var p_id = isEdit == true ? pid : 0;
                var partid = $('#com_partid').combo('getValue');
                var stationno = $('#com_station').combo('getText');
                //var sorting = $('#sorting').val();
                var tIDS = testIDArr.join(',');
            
                //alert('abc');

                var model = {
                    p_id: p_id,
                    stationno: stationno,
                    partid: partid,
                    tIDS: tIDS,
                    method: 'saveTestPart'
                };

                saveTheTestPart(model);
            }
            function saveTheTestPart(model) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/HttpHandlers/TestPartHandler.ashx",
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
            
                // $('#div_allPart').html('');
                // allPartdg.datagrid('clearChecked');
                //窗体数据初始化
                var row = selRows[0];
                pid = row.p_id;
                $('#com_partid').combobox('select', row.partid);
                $('#com_station').combobox('select', row.stationno);
                //$('#sorting').val(row.sorting);

                var idArr = row.tIDS.toString().split(',');
               

                var rows = testpartdg.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    for (var j = 0; j < idArr.length; j++) {
                        if (idArr[j] == rows[i].t_id.toString()) {
                            testpartdg.datagrid('checkRow', i);
                            break;
                        }
                    }
                }

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
                    url: "/HttpHandlers/TestPartHandler.ashx",
                    data: encodeURI("p_id=" + row.p_id + "&method=deleteTestPart"),
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
            //加载分类信息
            function refreshTestPartDiv() {
                testIDArr.length = 0;
                var selRows = testpartdg.datagrid('getChecked');
                //alert(selRows[0].all_no);

                var htmlArr = new Array();
                htmlArr.push('<ul>');
                for (var i = 0; i < selRows.length; i++) {
                    testIDArr.push(selRows[i].t_id);
                    htmlArr.push("<li class='drag-item'>" + selRows[i].testcaption + "</li>");
                }
                htmlArr.push('</ul>');
                $('#div_pstation').html(htmlArr.join(''));

                $(function () {
                    var indicator = $('<div class="indicator">>></div>').appendTo('body');
                    $('.drag-item').draggable({
                        revert: true,
                        deltaX: 0,
                        deltaY: 0
                    }).droppable({
                        onDragOver: function (e, source) {

                            indicator.css({
                                display: 'list-item',
                                left: $(this).offset().left - 10,
                                top: $(this).offset().top + $(this).outerHeight() - 5
                            });
                        },
                        onDragLeave: function (e, source) {

                            //indicator.hide();
                        },
                        onDrop: function (e, source) {
                            $(source).insertAfter(this);
                            //$(source).insertAfter(this);
                            ////indicator.hide();
                        }
                    });
                });
            }

        
            /**********************************************/
            /*****************   窗体程序 *******************/
            /**********************************************/

            //编辑窗口关闭清空数据
            function clearw() {
                $('#com_partid').combo('clear');
                $('#com_station').combo('clear');;
                testpartdg.datagrid('clearChecked');
                //    allpartTB 
            } 
         </script>
</asp:Content>




