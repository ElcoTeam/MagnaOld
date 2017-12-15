<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Product" ValidateRequest="false" CodeBehind="Product.aspx.cs"  %>

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
                <td><span class="title">产品</span> 
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
    <table id="tb" title="产品列表"  style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding-left: 24px;;padding-top:35px; visibility: hidden" title="产品编辑">
        <table cellpadding="0" cellspacing="0" table-layout:fixed; >

            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        产品编号：
                    </p>
                </td>
                <td>
                    <input class="easyui-validatebox" id="ProductNo" type="text"  data-options="required:true,validType:['length[1,50]']" style="width: 230px; height: 30px;"/>
                </td>              
            </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        产品中文名：
                    </p>
                </td>
                <td>
                    <input class="easyui-validatebox" id="ProductName" type="text"  data-options="required:true,validType:['length[1,50]']" style="width: 230px; height: 30px;"/>
                </td>              
            </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        产品中文描述：
                    </p>
                </td>
                <td>
                    <input class="easyui-validatebox" id="ProductDesc" type="text"  data-options="required:true,validType:['length[1,500]']" style="width: 230px; height: 70px;"/>
                </td>              
            </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        产品类别：
                    </p>
                </td>
                <td>
                    <select id="com_ProductType" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="required:true">
                        <option value="1">前排主驾</option>
                        <option value="2">前排副驾</option>
                        <option value="3">后排</option>
                    </select></td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        启用停用：
                    </p>
                </td>
                <td>
                    <select id="com_IsUseing" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="required:true">
                        <option value="0">停用</option>
                        <option value="1">在用</option>
                        
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
        var pid;               //要编辑的id
        var dg;      //数据表格
        var isEdit = false;     //是否为编辑状态
       


        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            //新增按钮点击
            $('.topaddBtn').first().click(function () {
                isEdit = false;
                $('#com_ProductType').combobox('setValue', 1);
                $('#com_IsUseing').combobox('setValue', 1);
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
                saveProduct(isEdit);
            });
            
          
            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 550,
                height: 450,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
       
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/ProductHandler.ashx?method=QueryProductList",           //改动的地方
                rownumbers: true,
                pagination: true,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns:true,
                columns: [[
                      { field: 'ID', title: 'ID', align: "center", hidden: true },
                      { field: 'ProductNo', title: '产品编号', width:100,align: "center" },
                      { field: 'ProductName', title: '产品中文名', width: 100, align: "center" },
                      { field: 'ProductDesc', title: '产品中文描述', width: 100, align: "center" },
                      { field: 'ProductTypeName', title: '产品类别', width: 100, align: "center" },
                      { field: 'IsUseingName', title: '启用停用', width: 100, align: "center" },
                      { field: 'ProductType', title: 'ProductType', width: 100, align: "center", hidden: true },
                      { field: 'IsUseing', title: 'IsUseing', width: 100, align: "center", hidden: true },
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
        function saveProduct() {
            //                        

            var p_id = isEdit == true ? pid : 0;
            var ProductNo = $('#ProductNo').val();
            var ProductName = $('#ProductName').val();
            var ProductDesc = $('#ProductDesc').val();
            var ProductType = $('#com_ProductType').combo('getValue');
            var IsUseing = $('#com_IsUseing').combo('getValue');

            var model = {
                p_id: p_id,
                ProductNo: ProductNo,
                ProductName: ProductName,
                ProductDesc: ProductDesc,
                ProductType: ProductType,
                IsUseing:IsUseing,

                method: 'saveProduct'
            };
            var ispass = $("#w").form('validate');
           
            if (ispass != true)
            {
                return;
            }
            else
            {                                 
                    saveTheProduct(model);
             }
                
        }
        function saveTheProduct(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/ProductHandler.ashx",
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
            pid = row.p_id;
            $('#ProductNo').val(row.ProductNo);
            $('#ProductName').val(row.ProductName);
            $('#ProductDesc').val(row.ProductDesc);
            $('#com_ProductType').combobox('setValue', row.ProductType);
            $('#com_IsUseing').combobox('setValue', row.IsUseing);
                       
            
            

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
                url: "/HttpHandlers/ProductHandler.ashx",
                data: encodeURI("p_id=" + row.p_id + "&method=DeleteProduct"),
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
            $('#ProductNo').val('');
            $('#ProductName').val('');
            $('#ProductDesc').val('');
            $('#com_ProductType').combo('clear');
            $('#com_IsUseing').combo('clear');
        }
    </script>
</asp:Content>
