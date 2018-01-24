<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_PointIspectionItem" ValidateRequest="false" CodeBehind="PointIspectionItem.aspx.cs"  %>

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
                <td><span class="title">点检项</span> 
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
    <table id="tb" title="点检项列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding-left: 24px;;padding-top:35px; visibility: hidden" title="点检项编辑">
        <table cellpadding="0" cellspacing="0" table-layout:fixed; >

            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        点检项名称：
                    </p>
                </td>
                <td>
                    <input class="easyui-validatebox" id="piitem" type="text"  data-options="required:true,validType:['length[1,50]']" style="width: 230px; height: 30px;"/>
                </td>              
            </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        点检功能描述：
                    </p>
                </td>
                <td>
                    <input  id="piitemdescribe" type="text"   style="width: 230px; height: 30px;"/>
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
        var piid;               //要编辑的id
        var dg;      //数据表格
        //var partdg;      //部件座椅表格
        var isEdit = false;     //是否为编辑状态
        //var partIDArr = new Array();       //该零件被哪些部件座椅用到


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
                saveBOMTestGroup(isEdit);
            });

            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 550,
                height: 300,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
            //数据表加载
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/PoInspectItemHandler.ashx?method=QueryPoInspectItemList",           //改动的地方
                rownumbers: true,
                pagination: true,
                pageSize: 20,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
                      { field: 'pi_id', title: 'ID', align: "center",  hidden: "true" },
                      { field: 'piitem', title: '点检项名称', align: "center",width: 100},
                      { field: 'piitemdescribe', title: '点检功能描述', align: "center", width: 100 },
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
        function saveBOMTestGroup() {
            //                        

            var pi_id = isEdit == true ? piid : 0;
            var piitem = $('#piitem').val();
            var piitemdescribe = $('#piitemdescribe').val();

            var model = {
                pi_id: pi_id,
                piitem: piitem,
                piitemdescribe: piitemdescribe,
                method: 'savePoInstpectItem'
            };
            var ispass = $("#w").form('validate');
           
            if (ispass != true)
            {
                return;
            }
            else
            {
                if (piitemdescribe == "" || piitemdescribe, length == 0)
                {
                    saveThePoInstpectItem(model);
                }
                else if(piitemdescribe.length>0&&piitemdescribe.length<50)
                {
                    saveThePoInstpectItem(model);
                }
                else
                {
                    alert('请输入正确的字符长度');
                }
             }
                
        }
        function saveThePoInstpectItem(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/PoInspectItemHandler.ashx",
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
            piid = row.pi_id;
            $('#piitem').val(row.piitem);
            $('#piitemdescribe').val(row.piitemdescribe);
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
                url: "/HttpHandlers/PoInspectItemHandler.ashx",
                data: encodeURI("pi_id=" + row.pi_id + "&method=DeletePoInspectItem"),
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
            $('#piitem').val('');
            $('#piitemdescribe').val('');
        }
    </script>
</asp:Content>
