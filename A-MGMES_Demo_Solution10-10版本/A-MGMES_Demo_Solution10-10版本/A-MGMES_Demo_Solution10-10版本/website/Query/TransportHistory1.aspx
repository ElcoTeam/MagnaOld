<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="TransportHistory1.aspx.cs" ValidateRequest="false" Inherits="website.Query.TransportHistory1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="/css/foundation.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>--%>
    <script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
    <%--<script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>--%>
    <%--<script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>--%>
    <%--<link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>--%>   

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
    <%--<input type="button" name="name" value="导出excel" runat="server" id="s1"/>--%>
    <%--<asp:Button runat="server" Text="Button" OnClick="Button1_Click" /> --%>

    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 16%;">
                    <span>订单编码：
                        <input id="OrderCode" type="text" />
                    </span>
                </td>
                <td style="width: 8%;">
                    <a class="topsearchBtn" href="javascript:;" onclick="searchOrder(2)">查询</a>
                    </td>
                    <td style="width: 76%;">
                    <input type="button"  Style="height: 28px; padding: 0px; width: 70px" class="btn btn-default" value="导出excel" OnClick="excelForm()" />
                </td>
            </tr>
        </table>

    </div>
    <!-- 数据表格  -->
    <table id="tb" title="发运列表" style="width: 99%;">
    </table>
    <table id="tb1" title="发运列表" style="width: 99%;">
    </table>
    <script>
        function excelForm() {
            var OrderCode = $('#OrderCode').val();
            var CarType = $('#CarType').val();
            var Worker = $('#Worker').val();
            var TransportType = $('#TransportType').val();
            var queryParams = Object;
            queryParams.OrderCode = OrderCode;
            queryParams.CarType = CarType;
            queryParams.Worker = Worker;
            queryParams.TransportType = TransportType;
            queryParams.method = "Export";
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/TransportHistory.ashx",
                data:queryParams,
                success: function (data) {
                    if (data == true) {
                        $("#sub").click();
                    }
                    else alert('导出失败');
                },
                error: function () {
                }
            });
            
            
        }



        /****************       全局变量          ***************/
        var stepid;               //要编辑的id
        var dg = $('#tb');      //表格
        var dg1 = $('#tb1');
        var isEdit = false;     //是否为编辑状态
        var tmpNO;              //工号
        var queryParams;

        function searchOrder(num) {     //后台传值
            var OrderCode = $('#OrderCode').val();
            var CarType = $('#CarType').val();
            var Worker = $('#Worker').val();
            var TransportType = $('#TransportType').val();
            var queryParams = dg.datagrid('options').queryParams;
            queryParams.OrderCode = OrderCode;
            queryParams.CarType = CarType;
            queryParams.Worker = Worker;
            queryParams.TransportType = TransportType;
            queryParams.method = "";
            dg.datagrid('reload');
        }

        function searchInfos(sort,num) {
            var OrderCode = $('#OrderCode').val();
            var CarType = $('#CarType').val();
            var Worker = $('#Worker').val();
            var TransportType = $('#TransportType').val();
            var queryParams = dg.datagrid('options').queryParams;
            queryParams.OrderCode = OrderCode;
            queryParams.CarType = CarType;
            queryParams.Worker = Worker;
            queryParams.TransportType = TransportType;
            queryParams.method = "";
            dg.datagrid('reload');
            dg.datagrid('reload');
        }

        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });

            //所属工位下拉框数据加载  
            var now = new Date();
            var OrderCode = $('#OrderCode').val();
            var CarType = $('#CarType').val();
            var Worker = $('#Worker').val();
            var TransportType = $('#TransportType').val();
            var queryParams = {
            OrderCode : OrderCode,
            CarType : CarType,
            Worker : Worker,
            TransportType : TransportType,
            method :""
            }
           
            dg = $('#tb').datagrid({
                fitColumns: true,
                nowrap: false,
                striped: true,
                collapsible: false,
                url: '/HttpHandlers/TransportHistory.ashx',
                sortName: 'ID',
                sortOrder: 'asc',
                remoteSort: true,
                idField: 'ID',
                queryParams:queryParams,
                columns: [[
					{ field: 'ID', title: 'ID', hidden: true },

					{ field: '订单编码', title: '订单编码',  align: "center" },
                    { field: '订单类型', title: '订单类型',  align: "center" },
                    { field: '下单时间', title: '下单时间',  align: "center" },
                    { field: '车型', title: '车型',  align: "center" },
                    { field: '订单状态', title: '订单状态', align: "center" },
					{ field: '前排故障历史', title: '前排故障历史', align: "center" },
					{ field: '主驾', title: '主驾', align: "center" },
					{ field: '主驾气囊', title: '主驾气囊',align: "center" },
					{ field: '副驾', title: '副驾', align: "center" },
					{ field: '副驾气囊', title: '副驾气囊', align: "center" },
                    { field: '百分之40', title: '40%',  align: "center" },
                    { field: '百分之60', title: '60%',  align: "center" },
                    { field: '百分之100', title: '100%',  align: "center" },
					{ field: '卷收器', title: '卷收器',  align: "center" },
					{ field: 'VINNumber', title: 'VINNumber', align: "center" },
					{ field: '回写SAP', title: '回写SAP', align: "center" }
                ]],

                rownumbers: true,
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
            $('#st_id_s').combobox('reload', '/HttpHandlers/StationHandler.ashx?method=queryStationForStepEditing&fl_id=' + fl_id);
        }
        function reloadpart_id_s() {
            $('#part_id_s').combobox('reload', '/HttpHandlers/PartHandler.ashx?method=queryPartForStepSearching');
        }


    </script>
</asp:Content>


