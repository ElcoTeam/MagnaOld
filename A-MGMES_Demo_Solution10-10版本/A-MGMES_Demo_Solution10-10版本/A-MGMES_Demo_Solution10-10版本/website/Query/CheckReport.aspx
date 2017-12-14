<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="website.Query.CheckReport" %>

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

                <td><span class="title">点检记录表查询</span> <%--<span class="subDesc">拖拽数据行进行排序</span>--%>
                </td>
                <td style="width: 150px">
                    <span>&nbsp;流水线</span>
                    <select id="fl_id_s" class="easyui-combobox" style="width: 150px; height: 25px;"
                        data-options="valueField: 'fl_id',textField: 'fl_name',onChange:reloadst_id_s">
                    </select>
                </td>
                <td style="width: 150px">
                    <span>工位</span>
                    <select id="st_id_s" class="easyui-combobox" style="width: 150px; height: 25px;"
                        data-options="valueField: 'st_no',textField: 'st_no',onChange:function(){reloadpart_id_s();}">
                    </select>
                </td>
                <td style="width: 150px">
                    <span>开始时间：</span>
                    <input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                <td style="width: 150px">
                    <span>结束时间：</span>
                    <input id="end_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                <td style="width: 150px;"><a class="topsearchBtn" href="javascript:;" onclick="searchInfos(1,1)">查询</a></td>

                <td style="width: 150px;"><a style="font-size: 12px; font-weight: 700; color: #000000" class="easyui-linkbutton" href="javascript:;" onclick="excelForm()">导出Excel</a></td>
            </tr>
            <tr>
                <td style="width: 150px;"></td>
                <td style="width: 150px;"></td>
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
            var fl_id = $('#fl_id_s').combo('getValue');
            //alert(fl_id);
            var fl_name = $('#fl_id_s').combobox('getText');
            //alert(fl_name);
            var st_no = $('#st_id_s').combo('getValue');
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');
            var queryParams =
                {
      
             fl_id : fl_id,
             fl_name : fl_name,
             st_no : st_no,
             StartTime : start_time,
             EndTime : end_time,
                }
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/Services1007_CheckReport.ashx?method=Export",
                data: queryParams,
                success: function (data) {
                    if (data == "true") {
                        $("#sub").click();
                    }
                    else alert('操作失败');
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

        function searchOrder(num) {
            var orderid = $('#orderid').val();
            var queryParams = dg.datagrid('options').queryParams;
            queryParams.OrderId = orderid;
            queryParams.Num = num;
            dg.datagrid('reload');
        }

        function searchInfos(sort, num) {
            var fl_id = $('#fl_id_s').combo('getValue');
            //alert(fl_id);
            var fl_name = $('#fl_id_s').combobox('getText');
            //alert(fl_name);
            var st_no = $('#st_id_s').combo('getValue');
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');
            //var index = start_time.lastIndexOf(':');
            //start_time = start_time.substr(0, index) + ':00:00';
            //index = end_time.lastIndexOf(':');
            //end_time = end_time.substr(0, index) + ':00:00';

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
            queryParams.fl_id = fl_id;
            queryParams.fl_name = fl_name;
            queryParams.st_no = st_no;
            queryParams.StartTime = start_time;
            queryParams.EndTime = end_time;
            //$('#tb').datagrid({
            //	queryParams: queryParams
            //});
            dg.datagrid('reload');
        }

        /****************       DOM加载          ***************/
        $(function () {

            $('#start_time').datetimebox('setValue', Date.now.toString());
            $('#end_time').datetimebox('setValue', Date.now.toString());
            reloadfl_id_s();
            reloadst_id_s();
            //reloadpart_id_s();



            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            var now = new Date();
            var fl_id = $('#fl_id_s').combo('getValue');
            //alert(fl_id);
            var fl_name = $('#fl_id_s').combobox('getText');
            //alert(fl_name);
            var st_no = $('#st_id_s').combo('getValue');
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');
            var queryParams =
                {

                    fl_id: fl_id,
                    fl_name: fl_name,
                    st_no: st_no,
                    StartTime: start_time,
                    EndTime: end_time,
                }
            dg = $('#tb').datagrid({
                fitColumns: true,
                nowrap: false,
                striped: true,
                collapsible: false,
                queryParams:queryParams,
                url: '/HttpHandlers/Services1007_CheckReport.ashx?method=SelectCheckReport',
                sortName: 'ID',
                sortOrder: 'asc',
                remoteSort: true,
                idField: 'id',
                columns: [[
							{ field: 'step_id', title: 'id', hidden: true },
							{ field: 'fl_id', title: '流水线id', hidden: true },
							{ field: 'st_id', title: '工位Id', hidden: true },

							{ field: 'ID', title: '序号', width: 200, align: "center" },
							{ field: 'StationNO', title: '工位', width: 200, align: "center" },
							{ field: 'op_name', title: '操作工', width: 200, align: "center" },
							{ field: 'PI_Item', title: '点检项', width: 200, align: "center" },
							{ field: 'IsPass', title: '点检结果', width: 200, align: "center" },
							{ field: 'CreateTime', title: '点检时间', width: 200, align: "center" },
                ]],

                rownumbers: true,
                loadMsg: '正在加载数据...',
                toolbar: '#navigationSearch',
                pagination: true,
                pagination: true,
                pageNumber: 1,
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

        //function reloadfl_id_s() {
        //    $('#fl_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_fl_list');
        //}

        //function reloadst_id_s() {
        //    var fl_id = $('#fl_id_s').combobox('getValue');
        //    $('#st_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_st_listforcheck&fl_id=' + fl_id);
        //}

        //function reloadpart_id_s() {
        //    var fl_id = $('#fl_id_s').combobox('getValue');
        //    var st_no = $('#st_id_s').combobox('getValue');
        //    $('#part_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_part_list&fl_id=' + fl_id + '&st_no=' + st_no);
        //}

        function reloadfl_id_s() {
            //$('#fl_id_s').combobox('clear')
            //$('#fl_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_fl_list');
            //var data = $('#fl_id_s').combobox("getData");
            //if (data.length > 0) {
            //    $('#fl_id_s').combobox('select', data[0].fl_id);
            //}
            $('#fl_id_s').combobox({
                url: '/HttpHandlers/TorqueReporterHandler.ashx?method=get_fl_list',
                method: "post",
                valueField: 'fl_id',
                textField: 'fl_name',
                onChange: function () {
                    reloadst_id_s();
                },
                onLoadSuccess: function () {
                    var data = $(this).combobox("getData");
                    if (data.length > 0) {
                        $('#fl_id_s').combobox('select', data[0].fl_id);

                    }
                }
            });

        }

        function reloadst_id_s() {
            $('#st_id_s').combobox('clear');
            var fl_id = $('#fl_id_s').combobox('getValue');
            $('#st_id_s').combobox({
                url: '/HttpHandlers/TorqueReporterHandler.ashx?method=get_st_listforcheck&fl_id=' + fl_id,
                method: "post",
                valueField: 'st_no',
                textField: 'st_no',
                onChange: function () {
                    reloadpart_id_s();
                },
                onLoadSuccess: function () {
                    var data = $(this).combobox("getData");
                    if (data.length > 0) {
                        $('#st_id_s').combobox('select', data[0].st_no);

                    }
                }
            });
        }

        function reloadpart_id_s() {
            $('#part_id_s').combobox('clear');
            var fl_id = $('#fl_id_s').combobox('getValue');
            var st_no = $('#st_id_s').combobox('getValue');
            $('#part_id_s').combobox({
                url: '/HttpHandlers/TorqueReporterHandler.ashx?method=get_part_list&fl_id=' + fl_id + '&st_no=' + st_no,
                method: "post",
                valueField: 'part_no',
                textField: 'part_no',
                onLoadSuccess: function () {
                    var data = $(this).combobox("getData");
                    if (data.length > 0) {
                        $('#part_id_s').combobox('select', data[0].part_no);

                    }

                }
            });

            //$('#part_id_s').combobox('clear');
            //var fl_id = $('#fl_id_s').combobox('getValue');
            //var st_id = $('#st_no_s').combobox('getValue');

            //$('#part_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_part_list&fl_id=' + fl_id + '&st_id=' + st_id);

        }
    </script>
</asp:Content>




















