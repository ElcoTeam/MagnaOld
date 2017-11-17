<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="CustomerOrder.aspx.cs" Inherits="Order_CustomerOrder" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <style>
        #w td { padding: 5px 5px; text-align: left; vertical-align: middle; }
        #w .title { vertical-align: middle; text-align: right; width: 80px; background-color: #f9f9f9; }
    </style>
    <script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>
    <script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">&nbsp;销售订单</span> <span class="subDesc"></span>
                </td>
                <td style="width: 120px">
                    <a class="topaddBtn">新增订单</a>
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

    <div class="easyui-tabs" style="width: 100%; height: 700px">
        <div title="未拆单" style="padding: 10px">
            <table id="uncuttedTB" title="未拆单列表" style="width: 99%;">
            </table>
        </div>
        <div title="已拆单" style="padding: 10px">
            <table id="cutedTB" title="已拆单列表" style="width: 99%;">
            </table>
        </div>
    </div>


    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="销售订单编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title" style="width: 100px">
                    <p>
                        销售订单号：
                    </p>
                </td>
                <td>
                    <input id="co_no" type="text" class="text" style="width: 230px;" />
                </td>
                <td rowspan="4" style="width: 450px; padding-left: 20px; vertical-align: top">
                    <!-- 整车座椅数据表格  -->
                    <table id="allpartTB" title="整车座椅选取" style="width: 100%; height: 430px;">
                    </table>

                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        客户：
                    </p>
                </td>
                <td>
                    <select id="co_cutomerid" class="easyui-combobox" style="width: 230px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        立即拆单：
                    </p>
                </td>
                <td>
                    <input id="co_isCutted" class="easyui-switchbutton" data-options="onText:'是',offText:'否'">
                </td>
            </tr>
            <tr>
                <td class="title"></td>
                <td>
                    <!-- 所选整车座椅数据表格  -->
                    <table id="selectedAllpartTB" title="所选整车座椅" style="width: 100%; height: 320px;">
                    </table>

                </td>
            </tr>

        </table>
    </div>
    <!-- 编辑窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility: hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
    <!-- 关系窗口 -->
    <div id="treew" style="padding: 10px; visibility: hidden" title="整车-部件-零件关系树">
        <ul id="tt"></ul>
    </div>

    <script>

        /****************       全局变量          ***************/
        var coid;               //要编辑的id
        var uncuttedGrid;      //未拆单表格
        var cuttedGrid;      //已拆弹表格
        var allPartdg;      //整车座椅表格
        var selectedAllPartdg;      //所选整车座椅表格
        var isEdit = false;     //是否为编辑状态
        var allpartIDArr = new Array();       //该部件被哪些整车座椅用到
        var countArr;           //座椅数量
        var isEditInit = false;

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
                isEditInit = true;
                initEidtWidget();
            });

            //删除按钮
            $('.topdelBtn').first().click(function () {
                deleteInfos();
            });

            //保存按钮
            $('#saveBtn').bind('click', function () {
                savePart(isEdit);
            });

            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 850,
                height: 560,
                footer: '#ft',
                top: 20,
                border: 'thin',
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
            //客户下拉框数据加载
            reloadco_cutomerid();

            //未拆单列表
            uncuttedGrid = $('#uncuttedTB').datagrid({
                url: "/HttpHandlers/CustomerOrderHandler.ashx?method=queryUnCuttedList",
                rownumbers: true,
                pagination: true,
                singleSelect: true,
                collapsible: false,
                columns: [[
                      //{ field: 'ck', checkbox: true },
    //  ,[]
    //  ,[]
                      //{ field: 'allpartNOs', title: '整车座椅NO集合', hidden: true },
                      { field: 'all_ids', title: '整车id集合', hidden: true },
                      { field: 'co_counts', title: '整车数量集合', hidden: true },
                      { field: 'co_cutomerid', title: '客户id', hidden: true },
                      { field: 'co_order', title: '排序序号', hidden: true },
                      { field: 'co_state', title: '订单状态', hidden: true },
                      { field: 'co_isCutted', title: '是否拆单', hidden: true },
                      { field: 'idcounts', title: '整车id，数量集合', hidden: true },
                      { field: 'co_no', title: '销售订单号', width: 150, align: "center" },
                      { field: 'co_customer', title: '客户名称', width: 100, align: "center" },
                      { field: 'appPartdesc', title: '详情', width: 500, align: "center" },
                      {
                          field: 'co_id', title: '拆单', align: "center", width: 80, formatter: function (value, row, index) { return '<img src="/image/admin/archives.png" style="height:16px;cursor:pointer" onclick="cuttingOrder(\'' + value + '\');"/>'; }
                      }
                ]]
            });
            uncuttedGrid.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });

            //已拆单列表
            cuttedGrid = $('#cutedTB').datagrid({
                url: "/HttpHandlers/CustomerOrderHandler.ashx?method=queryCuttedList",
                rownumbers: true,
                pagination: true,
                singleSelect: true,
                collapsible: false,
                columns: [[
                      //{ field: 'ck', checkbox: true },
                      { field: 'all_ids', title: '整车id集合', hidden: true },
                      { field: 'co_counts', title: '整车数量集合', hidden: true },
                      { field: 'co_cutomerid', title: '客户id', hidden: true },
                      { field: 'co_order', title: '排序序号', hidden: true },
                      { field: 'co_state', title: '订单状态', hidden: true },
                      { field: 'co_isCutted', title: '是否拆单', hidden: true },
                      { field: 'idcounts', title: '整车id，数量集合', hidden: true },
                      { field: 'co_no', title: '销售订单号', width: 150, align: "center" },
                      { field: 'co_customer', title: '客户名称', width: 100, align: "center" },
                      { field: 'appPartdesc', title: '详情', width: 500, align: "center" }

                ]]
            });
            cuttedGrid.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });

            //整车座椅加载
            allPartdg = $('#allpartTB').datagrid({
                url: "/HttpHandlers/AllPartHandler.ashx?method=queryAllPartListForPart",
                rownumbers: true,
                collapsible: false,
                columns: [[
                      { field: 'ck', checkbox: true },
                      { field: 'all_id', title: 'id', hidden: true },
                      { field: 'all_rateid', title: '等级id', hidden: true },
                      { field: 'all_colorid', title: '颜色id', hidden: true },
                      { field: 'all_metaid', title: '材质id', hidden: true },
                      { field: 'all_no', title: '代号', align: "center" },
                      { field: 'all_ratename', title: '等级', align: "center" },
                      { field: 'all_colorname', title: '颜色', align: "center" },
                      { field: 'all_metaname', title: '材质', align: "center" },
                      { field: 'all_desc', title: '备注', align: "center" }
                ]],
                onCheck: function (index, row) {
                    refreshSelectedAllpartTB(index, row);
                },
                onUncheck: function (index, row) {
                    refreshSelectedAllpartTB(index, row);
                },
                onCheckAll: function (rows) {
                    refreshCheckSelectedAllpartTB(rows);
                },
                onUncheckAll: function (rows) {
                    refreshUncheckSelectedAllpartTB(rows);
                }
            });


            selectedAllPartdg = $('#selectedAllpartTB').datagrid({
                collapsible: false,
                singleSelect: true,
                columns: [[
                      { field: 'all_id', title: 'id', hidden: true },
                      { field: 'all_no', title: '整车代号', align: "center" },
                      { field: 'co_count', title: '订单数量', align: "center", editor: 'numberbox' }
                ]]
            });
            selectedAllPartdg.datagrid('enableCellEditing').datagrid('gotoCell', {
                index: 0,
                field: 'co_count'
            });

            //关系窗口加载
            $('#treew').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 250,
                height: 450,
                top: 20,
                onBeforeOpen: function () { $('#treew').css('visibility', 'visible'); }
            });

        });

        /****************       主要业务程序          ***************/

        //新增 / 编辑  
        function savePart() {
            //       
            if (endEditing()) {
                selectedAllPartdg.datagrid('acceptChanges');
            }


            // co_no    co_cutomerid     selectedAllpartTB
            var rows = selectedAllPartdg.datagrid('getRows');

            var all_idArr = new Array();
            var co_countArr = new Array();

            for (var i = 0; i < rows.length; i++) {
                var all_id = selectedAllPartdg.datagrid('getRows')[i]["all_id"];
                var co_count = selectedAllPartdg.datagrid('getRows')[i]["co_count"];
                all_idArr.push(all_id);
                co_countArr.push(co_count);
            }
            // alert(all_idArr);

            var co_id = isEdit == true ? coid : 0;
            var co_cutomerid = $('#co_cutomerid').combo('getValue');
            var co_no = $('#co_no').val();
            var all_ids = (all_idArr.length > 0) ? all_idArr.join('|') : "";
            var co_counts = (co_countArr.length > 0) ? co_countArr.join('|') : "";
            var co_isCutted = ($('#co_isCutted').switchbutton('options').checked == true) ? 1 : 0;
            //alert(all_ids);
            //}co_isCutted
            //alert('abc');
           // alert(all_ids);
            var model = {
                co_id: co_id,
                co_cutomerid: co_cutomerid,
                co_no: co_no,
                all_ids: all_ids,
                co_counts: co_counts,
                co_isCutted: co_isCutted,
                method: 'saveCustomerOrder'
            };

           saveTheCustomerOrder(model);
        }
        function saveTheCustomerOrder(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/CustomerOrderHandler.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                        uncuttedGrid.datagrid('reload');
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
            var selRows = uncuttedGrid.datagrid('getSelections');
            if (selRows.length > 1) {
                alert('每次只能编辑一条记录，请重新选取');
                return;
            } else if (selRows.length == 0) {
                alert('请选择一条记录进行编辑');
                return;
            }

            //    selectedAllpartTB allpartTB
            //窗体数据初始化
            var row = selRows[0];
            coid = row.co_id;
            $('#co_cutomerid').combobox('select', row.co_cutomerid);
            $('#co_no').val(row.co_no);
            if (row.co_isCutted == 1)
                $('#co_isCutted').switchbutton('check');
            else
                $('#co_isCutted').switchbutton('uncheck');

            var idArr = row.all_ids.toString().split('|');
            countArr = row.idcounts.toString().split('|');

            for (var i = 0; i < countArr.length; i++) {

                var arr = countArr[i].split(',');
                var allid = arr[0];
                var allno = arr[2];
                var cou = arr[1];

                selectedAllPartdg.datagrid('appendRow', {
                    all_id: allid,
                    all_no: allno,
                    co_count: cou
                });
            }


            var rows = allPartdg.datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {
                for (var j = 0; j < idArr.length; j++) {
                    if (idArr[j] == rows[i].all_id.toString()) {
                        allPartdg.datagrid('checkRow', i);
                        break;
                    }
                }
            }
            isEditInit = false;
            $('#w').window('open');
        }
        function deleteInfos() {
            var selRows = uncuttedGrid.datagrid('getSelections');
            if (selRows.length > 1) {
                alert('每次只能删除一条记录，请重新选取');
                return;
            } else if (selRows.length == 0) {
                alert('请选择一条记录进行删除');
                return;
            }
            var row = selRows[0];
            $.ajax({
                url: "/HttpHandlers/CustomerOrderHandler.ashx",
                data: encodeURI("co_id=" + row.co_id + "&method=deleteCustomerOrder"),
                async: false,
                success: function (data) {
                    if (data == 'true') {
                        alert('已删除');
                        uncuttedGrid.datagrid('reload');
                    }
                    else alert('删除失败');
                },
                error: function () {
                }
            });
        }

        //拆单
        function cuttingOrder(value) {
            var id = value;

            $.ajax({
                url: "/HttpHandlers/CustomerOrderHandler.ashx",
                data: encodeURI("co_id=" + id + "&method=cuttingOrder"),
                async: false,
                success: function (data) {
                    if (data == 'true') {
                        alert('已拆单');
                        uncuttedGrid.datagrid('reload');
                        cuttedGrid.datagrid('reload');
                    }
                    else alert('操作失败');
                },
                error: function () {
                }
            });
        }


        /****************       辅助业务程序          ***************/
        //加载客户信息
        function reloadco_cutomerid() {
            $('#co_cutomerid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryCustomer');
        }

        function refreshSelectedAllpartTB(index, row) {
            allpartIDArr.length = 0;
            var allRows = selectedAllPartdg.datagrid('getRows');
            var hasExits = false;
            for (var j = 0; j < allRows.length; j++) {
                if (allRows[j].all_id == row.all_id) {
                    hasExits = true;
                    if (!isEditInit) {
                        selectedAllPartdg.datagrid('deleteRow', j);
                    }
                    break;
                }
            }
            if (!hasExits) {

                selectedAllPartdg.datagrid('appendRow', {
                    all_id: row.all_id,
                    all_no: row.all_no,
                    co_count: row.co_count
                });
            }
        }

        function refreshCheckSelectedAllpartTB(rows) {
            var allRows = selectedAllPartdg.datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {
                var hasExits = false;
                for (var j = 0; j < allRows.length; j++) {
                    if (allRows[j].all_id == rows[i].all_id) {
                        hasExits = true;
                        break;
                    }
                }
                if (!hasExits) {

                    selectedAllPartdg.datagrid('appendRow', {
                        all_id: rows[i].all_id,
                        all_no: rows[i].all_no,
                        co_count: rows[i].co_count
                    });
                }
            }
        }

        function refreshUncheckSelectedAllpartTB(rows) {
            selectedAllPartdg.datagrid('loadData', { total: 0, rows: [] });
        }




        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if (allPartdg.datagrid('validateRow', editIndex)) {
                allPartdg.datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        //co_no co_cutomerid  selectedAllpartTB
        //编辑窗口关闭清空数据
        function clearw() {
            //   allpartTB
            $('#co_no').val('');
            $('#co_cutomerid').combo('clear');
            allPartdg.datagrid('clearChecked');
            refreshUncheckSelectedAllpartTB();
            countArr = [];
            isEditInit = false;
        }
    </script>

</asp:Content>


