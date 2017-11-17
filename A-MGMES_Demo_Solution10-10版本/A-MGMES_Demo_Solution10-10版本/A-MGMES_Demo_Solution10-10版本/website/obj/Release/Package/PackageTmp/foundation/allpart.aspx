<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_allpart" ValidateRequest="false" Codebehind="allpart.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">整车座椅档案</span> <span class="subDesc">一套整车座椅包含多个不同的部件</span>
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
    <table id="tb" title="整车座椅列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="整车座椅编辑">
        <table cellpadding="0" cellspacing="0">

            <tr>
                <td class="title">
                    <p>
                        订单代号：
                    </p>
                </td>
                <td>
                    <input id="all_no" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        车型：
                    </p>
                </td>
                <td>
                    <select id="all_rateid" class="easyui-combobox" style="width: 230px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        颜色：
                    </p>
                </td>
                <td>
                    <select id="all_colorid" class="easyui-combobox" name="st_id" style="width: 230px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        材质：
                    </p>
                </td>
                <td>
                    <select id="all_metaid" class="easyui-combobox" name="st_id" style="width: 230px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        备注：
                    </p>
                </td>
                <td>
                    <input id="all_desc" type="text" class="text" style="width: 230px;" />

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
    var allid;               //要编辑的id
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
            isEdit = true;
            initEidtWidget();
        });

        //删除按钮
        $('.topdelBtn').first().click(function () {
            deleteInfos();
        });

        //保存按钮
        $('#saveBtn').bind('click', function () {
            saveAllpart(isEdit);
        });

        //编辑窗口加载
        $('#w').window({
            modal: true,
            closed: true,
            minimizable: false,
            maximizable: false,
            collapsible: false,
            width: 450,
            height: 350,
            footer: '#ft',
            top: 20,
            onBeforeClose: function () { clearw(); },
            onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
        });
        //所属工位下拉框数据加载
        reloadall_rateid();
        reloadall_colorid();
        reloadall_metaid();
              
        //数据列表加载
        dg = $('#tb').datagrid({
            url: "/HttpHandlers/AllPartHandler.ashx?method=queryAllPartList",
            rownumbers: true,
            pagination: true,
            rownumbers: true,   //写了两遍
            singleSelect: true,
            collapsible: false,
            striped: true,
            fitColumns: true,
            columns: [[
                  //{ field: 'ck', checkbox: true },
                  { field: 'all_rateid', title: '车型id', hidden: true },
                  { field: 'all_colorid', title: '颜色id', hidden: true },
                  { field: 'all_metaid', title: '材质id', hidden: true },
                  { field: 'all_no', title: '订单代号', width: 100, align: "center" },
                  { field: 'all_ratename', title: '车型', width: 100, align: "center" },
                  { field: 'all_colorname', title: '颜色', width: 100, align: "center" },
                  { field: 'all_metaname', title: '材质', width: 100, align: "center" },
                  { field: 'all_desc', title: '备注', width: 100, align: "center" },
                  { field: 'all_id', title: '查看关系网', align: "center", width: 100, formatter: function (value, row, index) { return '<img src="/image/admin/chukoulist.png" style="height:16px;cursor:pointer" onclick="showRelation(\'' + value + '\');"/>'; }, }
            ]]
        });
        //数据列表分页
        dg.datagrid('getPager').pagination({
            pageList: [1, 5, 10, 15, 20],
            layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
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
    function saveAllpart() {
        //        
        var all_id = isEdit == true ? allid : 0;
        //alert(all_id);

        var all_rateid = $('#all_rateid').combo('getValue');
        var all_colorid = $('#all_colorid').combo('getValue');
        var all_metaid = $('#all_metaid').combo('getValue');
        var all_no = $('#all_no').val();
        var all_desc = $('#all_desc').val();
        //alert('abc');

        var model = {
            all_id: all_id,
            all_rateid: all_rateid,
            all_colorid: all_colorid,
            all_metaid: all_metaid,
            all_no: all_no,
            all_desc: all_desc,
            method: 'saveAllPart'
        };

        saveTheAllPart(model);
    }
    function saveTheAllPart(model) {
        $.ajax({
            type: "POST",
            async: false,
            url: "/HttpHandlers/AllPartHandler.ashx",
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
        //alert(selRows.length.toString());     //点击编辑之后加载的
        if (selRows.length > 1) {
            alert('每次只能编辑一条记录，请重新选取');
            return;
        } else if (selRows.length == 0) {
            alert('请选择一条记录进行编辑');
            return;
        }
   
        //窗体数据初始化
        var row = selRows[0];
        allid = row.all_id;
        $('#all_rateid').combobox('select', row.all_rateid);
        $('#all_colorid').combobox('select', row.all_colorid);
        $('#all_metaid').combobox('select', row.all_metaid);
        $('#all_no').val(row.all_no);
        $('#all_desc').val(row.all_desc);

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
            url: "/HttpHandlers/AllPartHandler.ashx",
            data: encodeURI("all_id=" + row.all_id + "&method=deleteAllPart"),
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
    //加载车型信息
    function reloadall_rateid() {
        $('#all_rateid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryRateForEditing');
    }
    function reloadall_colorid() {
        $('#all_colorid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryColorForEditing');
    }
    function reloadall_metaid() {
        $('#all_metaid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryMetaForEditing');
    }
    //关系网
    function showRelation(bom_id) {
        //alert(bom_id);
        //  $('#tt').tree('loadData', "[]");

        //$.ajax({
        //    url: '/HttpHandlers/BOMHandler.ashx?method=getBomRelation&id=' + bom_id,
        //    dataType:'json',
        //    success: function (data) {
        //        //alert(JSON.parse(data)[0].text);
        //        $('#tt').tree('loadData',data);
        //    },
        //    error: function () {
        //    }
        //});


        $('#tt').tree({
            url: '/HttpHandlers/BOMHandler.ashx?method=getAllPartRelation&id=' + bom_id
        });
        $('#tt').tree('reload');
        $('#treew').window('open');
    }
                
    /**********************************************/
    /*****************   窗体程序 *******************/
    /**********************************************/

    //编辑窗口关闭清空数据
    function clearw() {
        $('#all_rateid').combo('clear');
        $('#all_colorid').combo('clear');
        $('#all_metaid').combo('clear');
        $('#all_no').val('');
        $('#all_desc').val('');
    }
</script>

</asp:Content>
