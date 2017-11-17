<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="InsertCustomOrder.aspx.cs" Inherits="website.Order.InsertCustomOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
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
    <script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>
    <script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">&nbsp;销售订单</span> <span class="subDesc"></span>
                </td>
                <td style="width: 120px">
                    <a class="topaddBtn">新增订单</a>
                </td>
                <!--<td style="width: 120px">
                    <a class="toppenBtn">编辑所选</a>
                </td>-->
                <!--<td style="width: 120px">
                    <a class="topdelBtn">删除所选</a>
                </td>-->
            </tr>
        </table>
    </div>

    <div class="top">
        <table id="CustomOrder" class="easyui-datagrid" title="紧急订单" style="width: 100%;"
            rownumbers="true" pagination="true" data-options="
                    url: '/HttpHandlers/InsertCustomOrderHandler.ashx?method=queryList'
            ">
            <thead>
                <tr>
                    <th field="OrderID" width="150px" align="center">销售订单号</th>
                    <th field="CustomerNumber" width="100px" align="center">客户名称</th>
                    <th field="JITCallNumber" width="100px" align="center">JITCallNo.</th>
                    <th field="SerialNumber" width="100px" align="center">SerialNo.</th>
                    <th field="PlanDeliverTime" width="100px" align="center">计划发货</th>
                    <th field="ProductNo" width="150" align="center">产品NO.</th>
                    <th field="ProductName" width="200" align="center">产品名称</th>
                </tr>
            </thead>
        </table>
    </div>

    <div id="EditWin" class="easyui-window" title="紧急插单编辑"
        closed="true" modal="true" minimizable="false" maximizable="false" border="thin" style="width: 850px; height: 560px; padding: 0px;"
        data-options="onClose:function(){ ClearEditWin(); }">
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'west'" style="width: 320px; padding: 20px">
                <div style="margin-bottom: 12px">
                    <span style="float: left; vertical-align: middle; height: 24px; line-height: 24px; width: 70px">客户编号</span>
                    <input id="CustomerNumber" class="easyui-textbox" style="width: 200px; height: 24px" />
                </div>
                <div style="margin-bottom: 12px">
                    <span style="float: left; vertical-align: middle; height: 24px; line-height: 24px; width: 70px">JITCallNo.</span>
                    <input id="JITCallNumber" class="easyui-textbox" style="width: 200px; height: 24px" />
                </div>
                <div style="margin-bottom: 12px">
                    <span style="float: left; vertical-align: middle; height: 24px; line-height: 24px; width: 70px">SerialNo.</span>
                    <input id="SerialNumber" class="easyui-textbox" style="width: 200px; height: 24px" />
                </div>
                <div style="margin-bottom: 12px">
                    <span style="float: left; vertical-align: middle; height: 24px; line-height: 24px; width: 70px">VINNo.</span>
                    <input id="VINNumber" class="easyui-textbox" style="width: 200px; height: 24px" />
                </div>
                <div style="margin-bottom: 12px">
                    <span style="float: left; vertical-align: middle; height: 24px; line-height: 24px; width: 70px">计划日期</span>
                    <input id="PlanDeliverTime" class="easyui-datebox" style="width: 200px; height: 24px" />
                </div>
                <div style="margin-bottom: 12px">
                    <span style="float: left; vertical-align: middle; height: 24px; line-height: 24px; width: 70px">产品</span>
                    <input id="ProductSelect" class="easyui-combobox" style="width: 200px; height: 24px" data-options="
                            valueField:'id',
                            textField:'text',
                            url:'/HttpHandlers/InsertCustomOrderHandler.ashx?method=queryProduct',
                            editable:false,
                            multiple:true" />
                </div>

            </div>
            <div data-options="region:'center'">
                <table id="PartSelect" class="easyui-datagrid" title="Part选择" style="width: 100%;"
                    rownumbers="true" idfield="part_id" striped="true">
                    <thead>
                        <tr>
                            <th field="ck" checkbox="true"></th>
                            <th field="part_id" hidden="true">part_id</th>
                            <th field="part_no" width="100px" align="center">部件编号</th>
                            <th field="part_name" width="150px" align="center">部件名称</th>
                            <th field="part_desc" width="180px" align="center">部件说明</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div style="height: 70px" data-options="region:'south'">
                <div style="padding: 20px 0; text-align: center">
                    <button id="Save" class="easyui-linkbutton" iconcls="icon-save" style="width: 100px; margin-right: 30px; display: inline">保存</button>
                    <button id="Cancel" class="easyui-linkbutton" iconcls="icon-cancel" style="width: 100px; margin-right: 30px; display: inline">取消</button>
                    <button id="Reset" class="easyui-linkbutton" iconcls="icon-reload" style="width: 100px; display: inline">清除</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });

            //新增按钮点击
            $('.topaddBtn').first().click(function () {
                $('#EditWin').window('open');
            });
            $('#ProductSelect').combobox({ onHidePanel: onProductSelectChange });
            $('#Save').bind('click', function () {
                var mCustomerNumber = $('#CustomerNumber').textbox('getValue');
                var mJITCallNumber = $('#JITCallNumber').textbox('getValue');
                var mSerialNumber = $('#SerialNumber').textbox('getValue');
                var mVINNumber = $('#VINNumber').textbox('getValue');
                var mPlanDeliverTime = $('#PlanDeliverTime').textbox('getValue');
                var rows = $('#PartSelect').datagrid('getSelections');

                if (rows.length == 0) {
                    alert("必须选择部件！");
                    return;
                }

                var mPartIDList = '';
                for (i = 0; i < rows.length; i++) {
                    mPartIDList += '<item><partid>' + rows[i].part_id + '</partid></item>';
                }

                mPartIDList = '<root>' + mPartIDList + '</root>';
                var model = {
                    CustomerNumber: mCustomerNumber,
                    JITCallNumber: mJITCallNumber,
                    SerialNumber: mSerialNumber,
                    VINNumber: mVINNumber,
                    PlanDeliverTime: mPlanDeliverTime,
                    PartIDList: mPartIDList,
                    method: 'save'
                };

                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/HttpHandlers/InsertCustomOrderHandler.ashx",
                    data: model,
                    success: function (data) {
                        if (data == 'true') {
                            $('#EditWin').window('close');
                            $('#CustomOrder').datagrid('reload');
                            alert('已保存');
                        }
                        else
                            alert('保存失败');
                    },
                    error: function () {
                    }
                });


            });
            $('#Cancel').bind('click', function () {
                $('#EditWin').window('close');
            });
            $('#Reset').bind('click', function () {
                ClearEditWin();
            });

        });

        //product选择完成，刷新part表格
        function onProductSelectChange() {
            values = $(this).combobox('getValues');
            $('#PartSelect').datagrid({ url: '/HttpHandlers/InsertCustomOrderHandler.ashx?method=queryPart&ProductList=' + values.join(',') }).datagrid({ hideColumn: 'part_id' })
        }

        //编辑窗数据复位
        function ClearEditWin() {
            $('#CustomerNumber').textbox('clear');
            $('#JITCallNumber').textbox('clear');
            $('#SerialNumber').textbox('clear');
            $('#VINNumber').textbox('clear');
            $('#PlanDeliverTime').textbox('clear');
            $('#ProductSelect').combobox('clear');
            $('#PartSelect').datagrid('loadData', { 'total': '0', 'rows': [] }).datagrid('clearSelections').datagrid('clearChecked');
        }

    </script>
</asp:Content>
