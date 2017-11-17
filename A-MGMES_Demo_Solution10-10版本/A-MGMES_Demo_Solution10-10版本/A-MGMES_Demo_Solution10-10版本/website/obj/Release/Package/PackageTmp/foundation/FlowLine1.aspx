<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_FlowLine1" ValidateRequest="false" CodeBehind="FlowLine1.aspx.cs"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <style>
        #w td { padding: 5px 5px; text-align: left; vertical-align: middle; }
        #w .title { vertical-align: middle; text-align: right; width: 80px; background-color: #f9f9f9; }
        p {padding:5px }
    </style>

    <link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>
    <script src="/js/validate.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0"  style="width: 100%">
            <tr>
                <td><span class="title">流水线</span> 
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
    <table id="tb" title="流水线列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding-left: 24px;;padding-top:35px; visibility: hidden" title="流水线编辑">
        <table cellpadding="0" cellspacing="0" table-layout:fixed; >

            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        流水线名称：
                    </p>
                </td>
                <td>
                    <input class="easyui-validatebox" id="fl_name" type="text"  data-options="required:true,validType:['length[1,50]']" style="width: 230px; height: 30px;"/>
                </td>              
            </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        产线类型：
                    </p>
                </td>
                <td>
                    <select id="com_FlowLineType" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="required:true">
                        <option value="1">前排</option>
                        <option value="2">后排</option>
                    </select></td>
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
        var flid;               //要编辑的id
        var dg;      //数据表格
        //var partdg;      //部件座椅表格
        var isEdit = false;     //是否为编辑状态
       


        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            //新增按钮点击
            $('.topaddBtn').first().click(function () {
                isEdit = false;
                $('#com_FlowLineType').combobox('setValue', 1);
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
                saveFlowLine1(isEdit);
            });
            //下拉框数据加载
            $('#com_FlowLineType').combobox('setValue', 1);
          
            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 550,
                height: 200,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
       
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/FlowLine1Handler.ashx?method=QueryFlowLine1List",           //改动的地方
                rownumbers: true,
                pagination: true,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                columns: [[
                      { field: 'fl_id', title: 'ID', align: "center", hidden:true},
                      { field: 'fl_name', title: '流水线名称', width:100, align: "center" },
                      { field: 'fltname', title: '产线类型', width: 100, align: "center" },
                      { field: 'flowlinetype', title: 'flowlinetype', width: 100, align: "center", hidden: true },
                ]]
            });
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });



        });

        /****************       主要业务程序          ***************/

        //新增 / 编辑  
        function saveFlowLine1() {
            //                        

            var fl_id = isEdit == true ? flid : 0;
            var fl_name = $('#fl_name').val();
            var flowlinetype = $('#com_FlowLineType').combo('getValue');

            var model = {
                fl_id: fl_id,
                fl_name: fl_name,
                flowlinetype:flowlinetype,

                method: 'saveFlowLine1'
            };
            var ispass = $("#w").form('validate');
           
            if (ispass != true)
            {
                return;
            }
            else
            {                                 
                    saveTheFlowLine1(model);
             }
                
        }
        function saveTheFlowLine1(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/FlowLine1Handler.ashx",
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
            var row = selRows[0];
            flid = row.fl_id;
            $('#fl_name').val(row.fl_name);
            
                $('#com_FlowLineType').combobox('select', row.flowlinetype);

            
            

            $(function () {
                $('input.easyui-validatebox').validatebox('disableValidation')//////////
                .focus(function () { $(this).validatebox('enableValidation'); })
                .blur(function () { $(this).validatebox('validate') });
            });

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
                url: "/HttpHandlers/FlowLine1Handler.ashx",
                data: encodeURI("fl_id=" + row.fl_id + "&method=DeleteFlowLine1"),
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
        
        

        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            //             
            $('#fl_name').val('');
            $('#com_FlowLineType').combo('clear');
        }
    </script>
</asp:Content>
