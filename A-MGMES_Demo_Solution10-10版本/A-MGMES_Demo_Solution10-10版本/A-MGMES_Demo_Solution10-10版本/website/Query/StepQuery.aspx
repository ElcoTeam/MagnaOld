<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="Query_StepQuery" ValidateRequest="false" CodeBehind="StepQuery.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <input type="submit" name="name" value="导出Excel" id="sub" hidden="hidden" />
    <div class="userarea">
        <h4 style="display: none">
            <span id="edit_name">
                <asp:Literal ID="namelit" runat="server"></asp:Literal>
            </span>
        </h4>
        <h4 style="display: none">职位：&nbsp;&nbsp;[&nbsp;<asp:Literal ID="tellit" runat="server"></asp:Literal>&nbsp;]
        </h4>
    </div>
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                 <td style="width:7.5%"><span class="title">步骤日志查询</span> <%--<span class="subDesc">拖拽数据行进行排序</span>--%>
                </td>
            </tr>
            <tr>
                <td style="width: 12.5%">
                    <span style="margin-left: 10px;">流水线：</span>
                    <select id="fl_id_s" class="easyui-combobox uservalue" 
                        data-options="valueField: 'fl_id',textField: 'fl_name',onChange:reloadst_id_s">
                    </select>
                </td>
                <td style="width: 12.5%">
                    <span>工位：</span>
                    <select id="st_id_s" class="easyui-combobox uservalue" 
                        data-options="valueField: 'st_no',textField: 'st_no'">
                    </select>
                </td>
                <td style="width:15%">
                    <span>订单号：</span>
                        <input id="orderid" class="uservalue"  type="text" />
                </td>
                
                </tr>
              <tr>
               <%-- <td style="width:7.5%"><span class="title"></span> 
                </td>--%>
                <td style="width:12.5%">
                    <span style="margin-left: 10px;">扫描值：</span>
                    <input id="scancodenum" class="uservalue"  type="text" />
                </td>
                <td style="width: 10%">
                     <span>开始时间：</span>
                    <input id="start_time" class="easyui-datetimebox uservalue" data-options="required:true,showSeconds:false"  />
                </td>
                <td style="width: 10%">
                     <span>结束时间：</span>
                    <input id="end_time" class="easyui-datetimebox uservalue" data-options="required:true,showSeconds:false"  />
                </td>
                <td style="width: 5%;">
                    <input  type="button" class="topsearchBtn"  onclick="searchInfos(1,1)" value="查询"/></td>
                <td style="width: 5%">
                    <input type="button" class="toppenBtn"  value="编辑"/>
                </td>
                <td style="width: 5%;">
                    <a  class="topexcelBtn" href="javascript:;" onclick="excelForm()">导出Excel</a>
                </td>
            </tr>
        </table>

    </div>


    <!-- 数据表格  -->
    <table id="tb" title="工位列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="编辑">
        <table cellpadding="0" cellspacing="0">

            <tr>
                <td class="title">
                    <p>
                        扭矩角：
                    </p>
                </td>
                <td>
                    <input id="AngleResult" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        扭矩值：
                    </p>
                </td>
                <td>
                    <input id="TorqueResult" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        条码值：
                    </p>
                </td>
                <td>
                    <input id="scanCode" type="text" class="text" style="width: 230px;" />
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
        var stepid;               //要编辑的id
        var dg = $('#tb');      //表格
        var isEdit = false;     //是否为编辑状态
        var tmpNO;              //工号
        var queryParams;
        var sort;
        var order;
       

        /****************       DOM加载          ***************/
        $(function () {
            var editRow = undefined; //定义全局变量：当前编辑的行

            reloadfl_id_s();
            reloadst_id_s();
            $('#start_time').datetimebox('setValue', Date.now.toString());
            $('#end_time').datetimebox('setValue', Date.now.toString());
  $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });

            //保存按钮
            $('#saveBtn').bind('click', function () {
                saveAllpart(isEdit);
            });

            //编辑按钮点击
            $('.toppenBtn').first().click(function () {
                isEdit = true;
                initEidtWidget();
            });
            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 450,
                height: 350,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
            var now = new Date();
            var fl_name = $('#fl_id_s').combobox('getText');
            //alert(fl_name);
            var st_no = $('#st_id_s').combobox('getText');
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');

            var orderid = $('#orderid').val();
            var scancodenum = $('#scancodenum').val();
            
            dg = $('#tb').datagrid({

                url: '/HttpHandlers/Services1000_SysLog.ashx',   //从远程站点请求数据的 URL。
                rownumbers: true,
                pagination: true,
                pageSize: 20,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                sortName: "st_no, step_startTime",
                sortOrder: "asc",
                queryParams:
                    {
                        fl_name: fl_name,
                        st_no: st_no,
                        StartTime: start_time,
                        EndTime: end_time,
                        scancodenum:scancodenum,
                        OrderId: orderid,
                        method:""
                    },
                columns: [[ //数据表格列配置对象，查看列属性以获取更多细节
							{ field: 'sys_id', title: 'id', hidden: true },
							{ field: 'fl_id', title: '流水线id', hidden: true },
							{ field: 'st_id', title: '工位Id', hidden: true },

							{ field: 'fl_name', title: '流水线名称', width: 200, align: "center" },
							{ field: 'st_no', title: '工位号', width: 200, align: "center" },
							{ field: 'or_no', title: '订单号', width: 260, align: "center" },
							{ field: 'part_no', title: '部件号', width: 200, align: "center" },
							{ field: 'step_order', title: '步骤号', width: 200, align: "center" },
							{ field: 'step_startTime', title: '开始时间', width: 200, align: "center" },
							{ field: 'step_endTime', title: '结束时间', width: 200, align: "center" },
							{ field: 'step_duringtime', title: '持续时间', width: 200, align: "center" },
                            {
                                field: 'AngleResult', title: '扭矩角', width: 100, align: "center", sortable: true,
                                editor: { type: 'validatebox', options: { required: false } }
                            },
                            {
                                field: 'TorqueResult', title: '扭矩值', width: 100, align: "center", sortable: true,
                                editor: { type: 'validatebox', options: { required: false } }
                            },
                            {
                                field: 'scanCode', title: '条码值', width: 230, align: "center", sortable: true,
                                editor: { type: 'validatebox', options: { required: false } }
                            },
                            {
                                field: 'MenderName', title: '修改人', width: 230, align: "center", sortable: true,
                                editor: { type: 'validatebox', options: { required: false } }
                            },
                            {
                                field: 'ReviseTime', title: '修改时间', width: 230, align: "center", sortable: true,
                                editor: { type: 'validatebox', options: { required: false } }
                            }
                ]],
                
                onLoadSuccess:function(data)
                {
                    console.log(data);
                },
                onSortColum: function (sort, order) {
                    $('#tb').datagrid('reload', {
                        sort: sort,
                        order: order
                    });
                }
            });
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });

            var grid = $('#tb');
            var p = grid.datagrid('getPager');
            $(p).pagination({
                beforePageText: '第',
                afterPageText: '页，共{pages}页',
                displayMsg: '当前显示从第{from}条到第{to}条 共{total}条记录',
                onBeforeRefresh: function () {

                }
            });

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
        function searchInfos(sort, num) {
            var fl_name = $('#fl_id_s').combobox('getText');
            //alert(fl_name);
            var st_no = $('#st_id_s').combobox('getText');
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');

            var orderid = $('#orderid').val();
            var scancodenum = $('#scancodenum').val();
            var queryParams = dg.datagrid('options').queryParams;
            queryParams.fl_name = fl_name;
            queryParams.st_no = st_no;
            queryParams.StartTime = start_time;
            queryParams.EndTime = end_time;
            queryParams.scancodenum = scancodenum;
            queryParams.OrderId = orderid;
            queryParams.method = "";

            dg.datagrid('reload');
        }
        //新增 / 编辑  
        function saveAllpart() {
            var step_id = isEdit == true ? stepid : 0;

            var AngleResult = $('#AngleResult').val();
            var TorqueResult = $('#TorqueResult').val();
            var scanCode = $('#scanCode').val();
            var edit_name = document.getElementById("edit_name").innerText;
            //alert(edit_name); 修改人名称：admin
            var model = {
                edit_name: edit_name,
                step_id: step_id,
                AngleResult: AngleResult,
                TorqueResult: TorqueResult,
                scanCode: scanCode,
                method: 'saveAllPart'
            };
            saveTheAllPart(model);
        }
        function saveTheAllPart(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/Services1000_SysLog_Add.ashx",
                data: model,
                success: function (data) {
                    console.log(data);
                    //alert(data.Result);
                    if (data.Result=="true") {
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
        //排序    好像没什么用
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

            //窗体数据初始化
            var row = selRows[0];
            //alert(JSON.stringify(row));
            stepid = row.sys_id;

            $('#AngleResult').val(row.AngleResult);
            $('#TorqueResult').val(row.TorqueResult);
            $('#scanCode').val(row.scanCode);

            $('#w').window('open');
        }

        //编辑窗口关闭清空数据
        function clearw() {
            $('#AngleResult').val('');
            $('#TorqueResult').val('');
            $('#scanCode').val('');
        }

        function tronmousedown(obj) {
            for (var o = 0; o < trs.length; o++) {
                if (trs[o] == obj) {
                    trs[o].style.backgroundColor = '#DFEBF2';
                } else {
                    trs[o].style.backgroundColor = '';
                }
            }
        }


        function excelForm() {
            // $("#sub").click();
            var fl_name = $('#fl_id_s').combobox('getText');
            //alert(fl_name);
            var st_no = $('#st_id_s').combobox('getText');
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');

            var orderid = $('#orderid').val();

            var queryParams = dg.datagrid('options').queryParams;
            queryParams.fl_name = fl_name;
            queryParams.st_no = st_no;
            queryParams.StartTime = start_time;
            queryParams.EndTime = end_time;
            queryParams.scancodenum = $('#scancodenum').val();
            queryParams.OrderId = orderid;
            
            queryParams.method = "Export";

            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/Services1000_SysLog.ashx",
                data: queryParams,
                success: function (data) {
                    console.log(data);
                    //alert(data.Result);
                    if (data.Result == "true") {
                        
                        alert('导出成功');
                        $("#sub").click();
                        //dg.datagrid('reload');
                    }
                    else alert('导出失败');
                    $('#w').window('close');
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
        //    $('#st_id_s').combobox('clear');
        //    var fl_id = $('#fl_id_s').combobox('getValue');
        //    $('#st_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_st_listForStep&fl_id=' + fl_id);
        //}

        //function reloadpart_id_s() {
        //    var st_no = $('#st_id_s').combobox('getValue');
        //    var fl_id = $('#fl_id_s').combobox('getValue');
        //    var st_id = $('#st_id_s').combobox('getValue');
        //    $('#part_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_part_list&fl_id=' + fl_id + '&st_no=' + st_no);
        //}
        function reloadfl_id_s() {
           
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
                url: '/HttpHandlers/TorqueReporterHandler.ashx?method=get_st_listForStep&fl_id=' + fl_id,
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
        }

    </script>
</asp:Content>
