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
                <td><span class="title">&nbsp;库存发货</span> <span class="subDesc"></span>
                </td>
                
                <td style="width: 150px">
                    <a class="toppenBtn">订单库存发货</a>
                </td>
                
            </tr>
        </table>
    </div>
   
 
    <table id="orderTB" title="订单列表" style="width: 99%;">
    </table>
 

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
          
           
            //订单列表
            orderTB = $('#orderTB').datagrid({
                url: '/Services1008_CustomerOrder.ashx',
                rownumbers: true,
                pagination: true,
                singleSelect: true,
                collapsible: false,
                columns: [[
					{ field: 'OrderID', title: 'OrderID', width: 50, align: "center" },
					{ field: 'CustomerNumber', title: 'CustomerNumber', width: 100, align: "center" },
					{ field: 'JITCallNumber', title: 'JITCallNumber', width: 150, align: "center" },
					{ field: 'SerialNumber', title: 'SerialNumber', width: 150, align: "center" },
					{ field: 'SerialNumber_MES', title: 'SerialNumber_MES', width: 150, align: "center" },
					{ field: 'VinNumber', title: 'VinNumber', width: 200, align: "center" },
					{ field: 'PlanDeliverTime', title: 'PlanDeliverTime', width: 200, align: "center" },
					{ field: 'CreateTime', title: 'CreateTime', width: 200, align: "center" },
					{ field: 'OrderType', title: 'OrderType', width: 200, align: "center" },
					{ field: 'OrderState', title: 'OrderState', width: 200, align: "center" },
					{ field: 'ProductName', title: 'ProductName', width: 200, align: "center" },
                    { field: 'OrderIsHistory', title: 'OrderIsHistory', width: 100, align: "center" },
                ]]
            });
            cuttedGrid.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
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

            
            $('#w').window('open');
        }
      
    

        /****************       辅助业务程序          ***************/
      

    </script>

</asp:Content>
