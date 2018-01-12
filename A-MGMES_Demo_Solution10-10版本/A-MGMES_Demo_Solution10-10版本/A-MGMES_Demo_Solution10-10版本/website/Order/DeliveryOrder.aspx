<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="DeliveryOrder.aspx.cs" ValidateRequest="false" Inherits="website.Order.DeliveryOrder" %>

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
                <td><span class="title">&nbsp;库存发货</span> <span class="subDesc">支持订单号模糊查询</span>
                </td>
                <td></td>
                <td style="width: 210px;">
                     <span>订单号：</span>
                     <input id="orderid" class="uservalue"  type="text" />
                </td>
                <td style="width: 110px;"><a class="topsearchBtn">筛选订单</a></td>
                <td style="width: 150px">
                    <a class="toppenBtn">订单库存发货</a>
                </td>
                
            </tr>
        </table>
    </div>
   
 
    <table id="orderTB" title="订单列表" style="width: 99%;">
    </table>
 

     <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="订单编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title" style="width: 100px">
                    <p>
                        订单号：
                    </p>
                </td>
                <td>
                    <input id="PRODN" type="text" class="text" style="width: 230px;" disabled />
                </td>
                
            </tr>
            
            <tr>
                <td class="title">
                    <p>
                        进入生产：
                    </p>
                </td>
                <td>
                    <select id="OrderIsHistory" class="easyui-combobox" style="width: 230px; height: 25px;">
                        <option value="0">进入生产</option>
                        <option value="1">不进入生产</option>
                    </select>
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
        var orderTB = $('#orderTB');      //表格
      

        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
           

            //编辑按钮点击
            $('.toppenBtn').first().click(function () {
                initEidtWidget();
            });

            //搜索按钮
            $('.topsearchBtn').first().click(function () {
                searchInfos();
            });

            //保存按钮
            $('#saveBtn').bind('click', function () {
                savePart();
            });

            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 450,
                height: 260,
                footer: '#ft',
                top: 20,
                border: 'thin',
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
          
           
            //订单列表
            orderTB = $('#orderTB').datagrid({
                url: '/HttpHandlers/DeliveryOrder.ashx?method=queryOrder',
                rownumbers: true,
                pagination: true,
                pageSize: 20,
                singleSelect: true,
                collapsible: false,
                onLoadSuccess: function (data) {
                    
                    if (data.total == 0) {
                        //添加一个新数据行，第一列的值为你需要的提示信息，然后将其他列合并到第一列来，注意修改colspan参数为你columns配置的总列数
                        $(this).datagrid('appendRow', { PARTN: '<div style="text-align:center;color:red">没有相关记录！</div>' }).datagrid('mergeCells', { index: 0, field: 'PARTN', colspan: 4 })
                        //隐藏分页导航条，这个需要熟悉datagrid的html结构，直接用jquery操作DOM对象，easyui datagrid没有提供相关方法隐藏导航条
                        $(this).closest('div.datagrid-wrap').find('div.datagrid-pager').hide();
                    }
                        //如果通过调用reload方法重新加载数据有数据时显示出分页导航容器
                    else $(this).closest('div.datagrid-wrap').find('div.datagrid-pager').show();
                },
                columns: [[
					{ field: 'PARTN', title: 'PARTN', width: 150, align: "center" },
					{ field: 'PRODN', title: 'PRODN', width: 250, align: "center" },
                    //{ field: 'VEHID', title: 'VEHID', width: 250, align: "center" },
					{ field: 'PDATUM', title: 'PDATUM', width: 250, align: "center" },
					{ field: 'CreateTime', title: 'CreateTime', width: 450, align: "center" },
					
                    { field: 'OrderIsHistory', title: 'OrderIsHistory', width: 100, align: "center", formatter: fInfo },
                ]]
            });
            orderTB.datagrid('getPager').pagination({
                pageList: [20,30,40],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });

        });

        //格式化列
        function fInfo(val, row) {
            if (val==1) {
                return '<span style="color:red;">不生产</span>';
            } else {
                return '<span style="color:green;">生产</span>';
            }
        }
        /****************       主要业务程序          ***************/
       
        //编辑  
        function savePart() {
            var PRODN = $("#PRODN").val();
            var OrderIsHistory = $("#OrderIsHistory").combo('getValue');
            var model = {
                PRODN: PRODN,
                OrderIsHistory: OrderIsHistory,
                method: 'editDeliveryOrder'
            };
            editDeliveryOrder(model);
        }


        function editDeliveryOrder(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/DeliveryOrder.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                        orderTB.datagrid('reload');
                    }
                    else if (data == 'exsit') {
                        alert('该订单已经生产');
                        orderTB.datagrid('reload');
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
            var selRows = orderTB.datagrid('getSelections');
            if (selRows.length > 1) {
                alert('每次只能编辑一条记录，请重新选取');
                return;
            } else if (selRows.length == 0) {
                alert('请选择一条记录进行编辑');
                return;
            }
            var row = selRows[0];
            console.log(row);
            $("#PRODN").val(row.PRODN);
            $('#OrderIsHistory').combobox('select', row.OrderIsHistory);
            $('#w').window('open');
        }
      
    

        /****************       辅助业务程序          ***************/
      
        //编辑窗口关闭清空数据
        function clearw() {
            $("#OrderID").val();
            $("#VinNumber").val();
            $('#OrderIsHistory').combobox('select', 0);
        }

        ///查询内容信息
        function searchInfos() {
            var orderid = $('#orderid').val();
            var queryParams = {
                orderid: orderid
            };
            $('#orderTB').datagrid({
                queryParams: queryParams
            });
            $('#orderTB').datagrid('reload');
        }
    </script>

</asp:Content>
