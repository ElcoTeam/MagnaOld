<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="Query_StepQuery" ValidateRequest="false" CodeBehind="StepQuery.aspx.cs" %>

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

                <td><span class="title">步骤日志查询</span> <%--<span class="subDesc">拖拽数据行进行排序</span>--%>
                </td>
                <td></td>
                <td style="width: 150px">
                    <input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                <td style="width: 150px">
                    <input id="end_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                <%--<td style="width: 150px;">
					<select id="fl_id_s" class="easyui-combobox" style="width: 200px; height: 25px;"
						data-options="valueField: 'fl_id',textField: 'fl_name',onChange:reloadst_id_s">
					</select></td>
				<td style="width: 150px;">
					<select id="st_id_s" class="easyui-combobox" style="width: 200px; height: 25px;"
						data-options="valueField: 'st_id',textField: 'st_no',onChange:reloadpart_id_s">
					</select></td>
				<td style="width: 150px;">
					<select id="part_id_s" class="easyui-combobox" style="width: 250px; height: 25px;"
						data-options="valueField: 'part_id',textField: 'part_no'">
					</select></td>--%>
                <td style="width: 150px;"><a class="topsearchBtn" href="javascript:;" onclick="searchInfos(1,1)">查询(按工位排序)</a></td>

                <td style="width: 150px;"><a class="topsearchBtn" href="javascript:;" onclick="searchInfos(2,1)">查询(按订单排序)</a></td>

                <td style="width: 150px;"><a style="font-size: 12px; font-weight: 700; color: #000000" class="easyui-linkbutton" href="javascript:;" onclick="excelForm()">导出Excel</a></td>


                <%--                <td style="width: 150px;"><a class="topsearchBtn" href="javascript:;" onclick="searchInfos(3)">导出Excel</a></td>--%>
            </tr>
            <tr>
                <td style="width: 150px;"></td>
                <td style="width: 150px;"></td>
                <td style="width: 150px;">
                    
                    <div>
                        <input id="orderid" type="text" /></div>
                </td>
                <td style="width: 150px;">
                    <a class="topsearchBtn" href="javascript:;" onclick="searchOrder(2)">输入订单号并查询</a>
                </td>
            </tr>
        </table>

    </div>
    <!-- 数据表格  -->
    <table id="tb" title="工位列表" style="width: 99%;">
    </table>
    <table id="tb1" title="工位列表" style="width: 99%;">
    </table>
    <script>
        function excelForm() {
            //alert("----");
            //$("#form1").submit();
            $("#sub").click();
            //alert("11111");
        }



        /****************       全局变量          ***************/
        var stepid;               //要编辑的id
        var dg = $('#tb');      //表格
        var dg1 = $('#tb1');
        var isEdit = false;     //是否为编辑状态
        var tmpNO;              //工号
        var queryParams;

        function searchOrder(num) {
            var orderid = $('#orderid').val();
            var queryParams = dg.datagrid('options').queryParams;
            queryParams.OrderId = orderid;
            queryParams.Num = num;
            dg.datagrid('reload');
        }

        function searchInfos(sort,num) {
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');
            var index = start_time.lastIndexOf(':');
            start_time = start_time.substr(0, index) + ':00:00';
            index = end_time.lastIndexOf(':');
            end_time = end_time.substr(0, index) + ':00:00';
            //alert(start_time);
            //var fl_id = $('#fl_id_s').combo('getValue');
            //var st_id = $('#st_id_s').combo('getValue');
            //var part_id = $('#part_id_s').combo('getValue');

            var queryParams = dg.datagrid('options').queryParams;
            //var queryParams = {
            //	//fl_id: fl_id,
            //	//part_id: part_id,
            //	//st_id: st_id,
            //	SortFlat: sort,
            //	StartTime: start_time,
            //	EndTime: end_time
            //};
            queryParams.SortFlag = sort;
            queryParams.Num = num;
            queryParams.StartTime = start_time;
            queryParams.EndTime = end_time;
            //$('#tb').datagrid({
            //	queryParams: queryParams
            //});
            dg.datagrid('reload');
        }

        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });

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
      

            dg = $('#tb').datagrid({
                //url: "/Services1000_SysLog.ashx",
                //url: "/HttpHandlers/StepHandler.ashx?method=queryStepList",

                fitColumns: true,
                nowrap: false,
                striped: true,
                collapsible: false,
                url: '/HttpHandlers/TransportHistory.ashx',
                sortName: 'sortNumber',
                sortOrder: 'asc',
                remoteSort: true,
                idField: 'id',
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
							{ field: 'ID', title: 'ID', hidden: true },

							{ field: '订单编码', title: '订单编码', width: 200, align: "center" },
							{ field: 'ID', title: '车型', width: 200, align: "center" },
							{ field: 'part1', title: '主驾型号', width: 200, align: "center" },
							{ field: 'user1', title: '匹配人', width: 200, align: "center" },
							{ field: 'time1', title: '匹配时间', width: 200, align: "center" },
							{ field: 'part2', title: '副驾型号', width: 200, align: "center" },
							{ field: 'user2', title: '匹配人', width: 200, align: "center" },
							{ field: 'time2', title: '匹配时间', width: 200, align: "center" },
                            { field: 'part3', title: '后排40%', width: 200, align: "center" },
							{ field: 'user3', title: '匹配人', width: 200, align: "center" },
							{ field: 'time3', title: '匹配时间', width: 200, align: "center" },
                            { field: 'part4', title: '后排60%', width: 200, align: "center" },
							{ field: 'user4', title: '匹配人', width: 200, align: "center" },
							{ field: 'time4', title: '匹配时间', width: 200, align: "center" },
                            { field: 'part5', title: '后排100%', width: 200, align: "center" },
							{ field: 'user5', title: '匹配人', width: 200, align: "center" },
							{ field: 'time5', title: '匹配时间', width: 200, align: "center" }
                ]],

                rownumbers: true,
                loadMsg: '正在加载数据...',
                toolbar: '#navigationSearch',
                pagination: true,
                pagination: true,
                pageNumber: 2,
                pageSize: 10,
                pageList: [10, 20, 30, 40],

                //			    当用户点击行时触发，参数如下：
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