<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Piistation" ValidateRequest="false" Codebehind="Piistation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <style>
        #w td { padding: 5px 5px; text-align: left; vertical-align: middle; }
        #w .title { vertical-align: middle; text-align: right; width: 80px; background-color: #f9f9f9; }
    </style>
    <script src="../js/validate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">点检站</span> 
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
    <table id="tb" title="点检站列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="点检站编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title">
                    <p>
                        站号：
                    </p>
                </td>
                <td>
                    <input id="station_no" class="easyui-validatebox" type="text" data-options="required:true,validType:['length[1,50]']" style="width: 230px; height:30px;" />
                </td>
                <td rowspan="6" style="width: 450px; padding-left: 20px; vertical-align: top">
                    <!-- 数据表格  -->
                    <table id="piitemtb" title="点检项" style="width: 100%; height: 430px;">
                    </table>

                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        排序：
                    </p>
                </td>
                <td>
                    <input id="sorting" type="text" class="easyui-validatebox" data-options="required:true,validType:['length[1,4]']" style="width: 230px; height:30px;" />
                </td>
            </tr>
            <tr>
                <td class="title"></td>
                <td>被以下点检站用到：</td>
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
        var psid;               //要编辑的id
        var dg;      //数据表格
        var pitemdg;      //点检项表格
        var isEdit = false;     //是否为编辑状态
        var pitemIDArr = new Array();       //该站被哪些点检项用到


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
                savePiiStation(isEdit);
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
                url: "/HttpHandlers/PiistationHandler.ashx?method=queryPiistationList",
                rownumbers: true,
                pagination: true,
                pageSize: 20,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
                      //{ field: 'ck', checkbox: true },
                      { field: 'piIDs', title: '点检项id集合', hidden: true },
                      { field: 'station_no', title: '站号', width: 100, align: "center" },
                      { field: 'sorting', title: '排序', width: 100, align: "center" },
                      { field: 'piitem', title: '点检项', width: 100, align: "center" },
                      { field: 'ps_id', title: 'ID', hidden:true}
                ]]
            }); 
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });


            //点检项加载
            pitemdg = $('#piitemtb').datagrid({
                url: "/HttpHandlers/PoInspectItemHandler.ashx?method=QueryPoInspectItemListALL",
                rownumbers: true,
                collapsible: false,
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
                      { field: 'ck', checkbox: true },
                      { field: 'pi_id', title: 'ID', align: "center", width: 50, hidden: "true" },
                      { field: 'piitem', title: '点检项名称', align: "center", width: 130 },
                      { field: 'piitemdescribe', title: '点检功能描述', align: "center", width: 150 },
                ]],
                onCheck: function (index, row) {
                    refreshPitemDiv();
                },
                onUncheck: function (index, row) {
                    refreshPitemDiv();
                },
                onCheckAll: function (rows) {
                    refreshPitemDiv();
                },
                onUncheckAll: function (rows) {
                    refreshPitemDiv();
                }
            });

           
        });
            /****************       主要业务程序          ***************/

            //新增 / 编辑  
            function savePiiStation() {
                //            
                var ps_id = isEdit == true ? psid : 0;
                var station_no = $('#station_no').val();
                var sorting = $('#sorting').val();
                var piIDs = pitemIDArr.join(',');
            
                //alert('abc');

                var model = {
                    ps_id: ps_id,
                    station_no: station_no,
                    sorting: sorting,
                    piIDs: piIDs,
                    method: 'savePiiStation'
                };

                saveThePiiStation(model);
            }
            function saveThePiiStation(model) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/HttpHandlers/PiistationHandler.ashx",
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
                psid = row.ps_id;
                $('#station_no').val(row.station_no);
                $('#sorting').val(row.sorting);

                var idArr = row.piIDs.toString().split(',');
                //var noArr = row.allpartNOs.toString().split(',');

                //var htmlArr = new Array();
                //htmlArr.push('<ul>');
                //for (var i = 0; i < noArr.length; i++) {
                //    htmlArr.push("<li>" + noArr[i] + "</li>");
                //}
                //htmlArr.push('</ul>');
                //$('#div_allPart').html(htmlArr.join(''));

                //for (var i = 0; i < idArr.length; i++) {
                //    allpartIDArr.push(idArr[i]);
                //}

                var rows = pitemdg.datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    for (var j = 0; j < idArr.length; j++) {
                        if (idArr[j] == rows[i].pi_id.toString()) {
                            pitemdg.datagrid('checkRow', i);
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
                    url: "/HttpHandlers/PiistationHandler.ashx",
                    data: encodeURI("ps_id=" + row.ps_id + "&method=deletePiistation"),
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
            function refreshPitemDiv() {
                pitemIDArr.length = 0;
                var selRows = pitemdg.datagrid('getChecked');
                //alert(selRows[0].all_no);

                var htmlArr = new Array();
                htmlArr.push('<ul>');
                for (var i = 0; i < selRows.length; i++) {
                    pitemIDArr.push(selRows[i].pi_id);
                    htmlArr.push("<li>" + selRows[i].piitem + "</li>");
                }
                htmlArr.push('</ul>');
                $('#div_pstation').html(htmlArr.join(''));
            }

        
            /**********************************************/
            /*****************   窗体程序 *******************/
            /**********************************************/

            //编辑窗口关闭清空数据
            function clearw() {
                $('#station_no').val('');
                $('#sorting').val('');
                pitemdg.datagrid('clearChecked');
                //    allpartTB 
            } 
         </script>
</asp:Content>




