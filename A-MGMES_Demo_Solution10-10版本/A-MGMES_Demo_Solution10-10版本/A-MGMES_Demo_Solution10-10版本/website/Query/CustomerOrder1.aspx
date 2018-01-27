<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="CustomerOrder1.aspx.cs" Inherits="website.Query.CustomerOrder1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>

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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <input type="submit" name="name" value="导出Excel" id="sub" hidden="hidden" />

    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 6%">
                    <span>订单类型：</span>
                </td>
                <td style="width: 8%">
                    <select id="OrderType" style="width: 110px;">
                        <option value="">请选择...</option>
                        <option value="1">DelJit订单</option>
                        <option value="2">SAP订单</option>
                        <option value="3">紧急插单</option>
                    </select>
                </td>
                <td style="width: 6%">
                    <span>SerialNumber：</span>
                </td>
                <td style="width: 8%">
                    <input id ="SerialNumber"/>
                </td>
                <td  style="width: 6%" >            
                       <span> 开始日期：</span>
   
                </td>  
                <td style="width: 120px">
                    <input id="start_time" class="easyui-datetimebox" data-options="required:true" />
                </td>
                <td style="width: 6%"  >                   
                       <span>结束日期： </span>      
                </td>  
                <td style="width: 120px">
                    <input id="end_time" class="easyui-datetimebox" data-options="required:true" />
                </td>
                <td style="width: 8%">
                    <input type="button" class="topsearchBtn" href="javascript:;" onclick="searchOrder(2)" value="查询"/>
                </td>
                <td >
                    <input type="button" class="topexcelBtn" value="导出Excel" OnClick="excelForm()" />
                </td>
            </tr>
        </table>

    </div>
    <!-- 数据表格  -->
    <table id="tb" title="客户订单报表" style="width: 99%;">
    </table>
    <table id="tb1" title="发运列表" style="width: 99%;">
    </table>
    <script>
        function excelForm() {
            var start_time= $("#start_time").datetimebox('getValue');
            var end_time = $("#end_time").datetimebox('getValue');
            var OrderType = $('#OrderType').val();
            var SerialNumber = $('#SerialNumber').val();
            var method = "Export";
            $.ajax({
                type: "POST",
                async: false,
                url: '/HttpHandlers/Services1008_CustomerOrder.ashx',
                data: { StartTime:start_time,EndTime:end_time,OrderType: OrderType, SerialNumber:SerialNumber,method: method },
                success: function (data) {
                    if (data == true) {
                        $("#sub").click();
                    }
                    else {
                        alert('操作失败');
                    }
                },
                error: function () {
                }
            });
            
            //alert("11111");
        }



        /****************       全局变量          ***************/
        var stepid;               //要编辑的id
        var dg = $('#tb');      //表格
        var dg1 = $('#tb1');
        var isEdit = false;     //是否为编辑状态
        var tmpNO;              //工号
        var queryParams;

        function searchOrder(num) {     //后台传值
            var start_time = $("#start_time").datetimebox('getValue');
            var end_time = $("#end_time").datetimebox('getValue');
            var SerialNumber = $('#SerialNumber').val();
            var OrderCode = $('#OrderCode').val();
            var CarType = $('#CarType').val();
            var Worker = $('#Worker').val();
            var OrderType = $('#OrderType').val();
            var queryParams = dg.datagrid('options').queryParams;
            var queryParams = {
                OrderCode: OrderCode,
                CarType: CarType,
                Worker: Worker,
                OrderType: OrderType,
                SerialNumber: SerialNumber,
                StartTime: start_time,
                EndTime: end_time,
                pageNumber: "1",
                method: "",
            }
            //queryParams.pageNumber = "1";
            //queryParams.OrderCode = OrderCode;
            //queryParams.CarType = CarType;
            //queryParams.Worker = Worker;
            //queryParams.OrderType = OrderType;
            //queryParams.SerialNumber = SerialNumber;
            //queryParams.StartTime = start_time;
            //queryParams.EndTime = end_time;
            //queryParams.method = "";
            
            dg.datagrid('load', {
                OrderCode: OrderCode,
                CarType: CarType,
                Worker: Worker,
                OrderType: OrderType,
                SerialNumber: SerialNumber,
                StartTime: start_time,
                EndTime: end_time,
                pageNumber: "1",
                method: "",
            });
        }

        function searchInfos(sort, num) {
            var start_time = $("#start_time").datetimebox('getValue');
            var end_time = $("#end_time").datetimebox('getValue');
            var SerialNumber = $('#SerialNumber').val();
            var OrderCode = $('#OrderCode').val();
            var CarType = $('#CarType').val();
            var Worker = $('#Worker').val();
            var OrderType = $('#OrderType').val();
            var queryParams = dg.datagrid('options').queryParams;
            queryParams.OrderCode = OrderCode;
            queryParams.CarType = CarType;
            queryParams.Worker = Worker;
            queryParams.OrderType = OrderType;
            queryParams.SerialNumber = SerialNumber;
            queryParams.StartTime = start_time;
            queryParams.EndTime = end_time;
            queryParams.method = "";
            dg.datagrid('reload');
            dg.datagrid('reload');
        }

        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            var date_t = new Date();
            $("#start_time").datetimebox('setValue', date_t.toString());
            $("#end_time").datetimebox('setValue', date_t.toString());
            //所属工位下拉框数据加载  
            //reloadst_id();
            //reloadfl_id();
            //reloadPart_id();
            //reloadbom_id();
            //  reloadfl_ids();
            var now = new Date();
            //var queryParams1 = {
            //    SortFlag: 1,
            //    StartTime: now.getFullYear() + "/" + now.getMonth() + "/" + now.getDay(),
            //    EndTime: now.getFullYear() + "/" + now.getMonth() + "/" + now.getDay(),

            //}
            //数据列表加载
            var start_time = $("#start_time").datetimebox('getValue');
            var end_time = $("#end_time").datetimebox('getValue');
            var OrderCode = $('#OrderCode').val();
            var CarType = $('#CarType').val();
            var Worker = $('#Worker').val();
            var OrderType = $('#OrderType').val();
            var SerialNumber = $('#SerialNumber').val();
            var queryParams = {
            OrderCode : OrderCode,
            CarType : CarType,
            Worker : Worker,
            OrderType: OrderType,
            SerialNumber: SerialNumber,
            StartTime: start_time,
                EndTime:end_time,
            method : "",
        }
            dg = $('#tb').datagrid({
                
                fitColumns: true,
                nowrap: false,
                striped: true,
                collapsible: false,
                url: '/HttpHandlers/Services1008_CustomerOrder.ashx',
                queryParams:queryParams,
                sortName: 'OrderID',
                sortOrder: 'asc',
                remoteSort: true,
                idField: 'ID',
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
					{ field: 'ID', title: 'ID', hidden: true },
					{ field: 'OrderID', title: 'OrderID', width: 200, align: "center" },
					{ field: 'CustomerNumber', title: 'CustomerNumber', width: 200, align: "center",hidden:true },
					{ field: 'JITCallNumber', title: 'JITCallNumber', width: 200, align: "center" },
					{ field: 'SerialNumber', title: 'SerialNumber', width: 200, align: "center" },
					{ field: 'SerialNumber_MES', title: 'SerialNumber_MES', width: 200, align: "center", hidden: true },
                    { field: 'MES_ORDER', title: 'SerialNumber_MES', width: 200, align: "center" },
					{ field: 'VinNumber', title: 'VinNumber', width: 200, align: "center" },
					{
					    field: 'PlanDeliverTime', title: 'PlanDeliverTime', width: 200, align: "center",
					    formatter: function (value, row, index) {
					        return DataStr(value);
					    }
					},
					{ field: 'CreateTime', title: 'CreateTime', width: 200, align: "center" },
					{ field: 'OrderType', title: 'OrderType', width: 200, align: "center" },
					{ field: 'OrderState', title: 'OrderState', width: 200, align: "center" },
					{ field: 'ProductName', title: 'ProductName', width: 200, align: "center" }
                ]],

                rownumbers: false,
                loadMsg: '正在加载数据...',
                toolbar: '#navigationSearch',
                pagination: true,
                pagination: true,
                pageNumber: 1,
                pageSize: 20,
                pageList: [10, 20, 30, 40],

                //当用户点击行时触发，参数如下：
                //rowIndex：被点击的行索引，从0开始。
                //rowData：对应于被点击的行的记录。
                onClickRow: function (rowIndex, rowData) {
                    $(this).datagrid('unselectRow', rowIndex);
                },

                onLoadSuccess: function () {
                    $(this).datagrid('enableDnd');

                },
                onDrop: function (targetRow, sourceRow, point) {
                    sortingData(targetRow.step_id, targetRow.step_order, sourceRow.step_id, sourceRow.step_order, point);
                },
                onLoadSuccess: function () {
                    $(this).datagrid("fixRownumber");
                }
            });
            //数据列表分页


            var grid = $('#tb');
            var p = grid.datagrid('getPager');
            $(p).pagination({
                beforePageText: '第',
                afterPageText: '页，共{pages}页',
                displayMsg: '当前显示从第{from}条到第{to}条 共{total}条记录',
                onBeforeRefresh: function () {

                }
            });



            //reloadfl_id_s();
            //reloadst_id_s();
            //reloadpart_id_s();

            //自定义宽度
            $.extend($.fn.datagrid.methods, {
                fixRownumber: function (jq) {
                    return jq.each(function () {
                        var panel = $(this).datagrid("getPanel");
                        var clone = $(".datagrid-cell-rownumber", panel).last().clone();
                        clone.css({
                            "position": "absolute",
                            left: -1000
                        }).appendTo("body");
                        var width = clone.width("auto").width();
                        if (width > 25) {
                            //多加5个像素,保持一点边距  
                            $(".datagrid-header-rownumber,.datagrid-cell-rownumber", panel).width(width + 50);
                            $(this).datagrid("resize");
                            //一些清理工作  
                            clone.remove();
                            clone = null;
                        } else {
                            //还原成默认状态  
                            $(".datagrid-header-rownumber,.datagrid-cell-rownumber", panel).removeAttr("style");
                        }
                    });
                }
            });

        });

        //排序
        function sortingData(t_step_id, t_step_order, s_step_id, s_step_order, point) {
            $.ajax({
                type: "POST",
                async: true,
                url: "/HttpHandlers/StepHandler.ashx",
                data: "method=sorting&step_id=" + s_step_id + "&step_order=" + t_step_order + "&point=" + point,
                success: function (data) {
                    if (data == 'true') {
                        dg.datagrid('reload');
                    }
                    else alert('操作失败');
                },
                error: function () {
                }
            });
        }

        /****************       辅助业务程序          ***************/

        function reloadfl_id() {
            $('#fl_id').combobox('reload', '/HttpHandlers/FlowlineHandler.ashx?method=queryFlowlingForStepEditing');
        }
        function reloadst_id() {
            $('#st_id').combobox('loadData', '[]');
            var fl_id = $('#fl_id').combo('getValue');
            if (fl_id == "请选择") {
                fl_id = "";
            }
            
            $('#st_id').combobox('reload', '/HttpHandlers/StationHandler.ashx?method=queryStationForStepEditing&fl_id=' + fl_id);
        }
        function reloadPart_id() {
            $('#part_id').combobox('reload', '/HttpHandlers/PartHandler.ashx?method=queryPartForStepEditing');
        }

        function reloadbom_id() {
            $('#bom_id').combobox('loadData', '[]');
            var part_id = $('#part_id').combo('getValue');
            $('#bom_id').combobox('reload', '/HttpHandlers/BOMHandler.ashx?method=queryBOMForStepEditing&part_id=' + part_id);
        }

        function reloadfl_id_s() {
            $('#fl_id_s').combobox('reload', '/HttpHandlers/FlowlineHandler.ashx?method=queryFlowlingForEditing');
        }
        function reloadst_id_s() {
            $('#st_id_s').combobox('loadData', '[]');
            var fl_id = $('#fl_id_s').combo('getValue');
            if (fl_id == "请选择") {
                fl_id = "";
            }
           
            $('#st_id_s').combobox('reload', '/HttpHandlers/StationHandler.ashx?method=queryStationForStepEditing&fl_id=' + fl_id);
        }
        function reloadpart_id_s() {
            $('#part_id_s').combobox('reload', '/HttpHandlers/PartHandler.ashx?method=queryPartForStepSearching');
        }
        function DataStr(value)
        {
           // console.log("value"+value);
            var str = value;
            var Arr = str.toString().split('.');
            //console.log("Arr"+Arr);
            var result = "";
            if (Arr.length < 1) {
                result = str;
            }
            else {
                //年月日
                if (Arr[0].length > 2)
                {
                    for (var i = 0; i <Arr.length; i++) {
                        if (i == Arr.length-1) {
                            result += Arr[i];
                        }
                        else {
                            result += Arr[i] + "-";
                        }

                    }
                }
                else//日，月，年
                {
                    for (var i = Arr.length - 1; i >= 0; i--) {
                        if (i == 0) {
                            result += Arr[i];
                        }
                        else {
                            result += Arr[i] + "-";
                        }

                    }
                }
                
            }
            
            //console.log("result"+result);
            return result;
        }

    </script>
</asp:Content>



