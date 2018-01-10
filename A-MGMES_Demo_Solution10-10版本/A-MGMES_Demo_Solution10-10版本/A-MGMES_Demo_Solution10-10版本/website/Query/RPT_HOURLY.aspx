<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master"  AutoEventWireup="true" CodeBehind="RPT_HOURLY.aspx.cs" Inherits="website.Query.RPT_HOURLY" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
	<link href="/js/uploadify/uploadify.css" rel="stylesheet" />
	<script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>
	<script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>
    <script src="../js/highcharts/highcharts.js"></script>
    <script src="../js/highcharts/modules/exporting.js"></script>
    <script src="../js/easyui.datagrid.export.js"></script>
    <script src="../js/easyui.datagrid.print.js"></script>
    <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" /s>
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../bootstrap/jqPaginator.js"></script>
    <script src="../js/highcharts/highcharts.js"></script>
    <script src="../js/highcharts/modules/exporting.js"></script>
    <link href="../css/foundation.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="top"  style="width: 65%;height:10%">
        <table cellpadding="0" cellspacing="0" style="width: 85%">
            <tr>
                <td  >
                    
                       <span>选择班次：</span> 
                    
                </td>  
                           
                <td >
                    <select id="clnameid" />

                </td>
             </tr>
             <tr>
                <td  >

                      <span>开始时间：</span> 
                    
                </td>          
                <td >
                    <input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                <td   >
                    
                        <span>结束时间： </span>                  
                </td>       
                <td style="width: 120px">
                    <input id="end_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                
                <td >
                  <a class="topsearchBtn" href="javascript:;" onclick="searchName()"">查询</a>
                </td>
                 <td>  
                  <input id="subs" type="submit"  value="导出Excel" hidden="hidden"/>
                  <a  class="topexcelBtn" href="javascript:;" onclick ="excelFor()">导出</a>
                </td>
                 <td>  
                  <a class="topprintBtn" href="javascript:;" onclick ="print()">打印</a>
                </td>
            </tr>
        </table>

    </div>
    <div id ="gridPanel"  style="width: 99%;height:90%">
    <div id="printArea" class="printArea" closed="true" style="width: 99%;height:inherit"> 
    <!-- 数据表格  -->
    <table id="gridTable" title="FTT列表" style="width: 99%;height:50%">
    </table>
    <div class="center-Panel" style="width: 99%;height:50%">
                    <div class="panel-Title">统计信息柱状图</div>
                     <div id="container" style="width: 100%; height: 400px; text-align:center;  margin: 0 auto">
           
                     </div>
     </div>
     </div> 
   </div>
    <script>



        /****************       全局变量          ***************/
        var stepid;               //要编辑的id
        var dg = $('#gridTable');      //表格
        var isEdit = false;     //是否为编辑状态
        var tmpNO;              //工号
        var queryParams;





        /****************       DOM加载          ***************/
        $(function () {
            var date_t = new Date();
            $("#start_time").datetimebox('setValue', date_t.toString('yyyy-MM-dd HH:mm'));
            $("#end_time").datetimebox('setValue', date_t.toString('yyyy-MM-dd HH:mm'));
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            InitialPage();

            CreateSelect();
            GetGrid();
        });
            
        //初始化页面
        function InitialPage() {
           
        }

        function GetGrid() {
            queryParams = {
                start_time: $("#start_time").datebox('getValue'),
                end_time: $("#end_time").datebox('getValue'),
                clnameid: $("#clnameid option:selected").val(),
                clname: $("#clnameid option:selected").text()
            }
            var col = $('#gridTable').width();
                dg = $('#gridTable').datagrid({
                    fitColumns: true,
                    nowrap: false,
                    striped: true,
                    collapsible: false,
                    url: '/HttpHandlers/Service1006_RPT_HOURLY.ashx?method=GetListNew',
                    queryParams:queryParams,
                    showFooter:true,
                    sortName: 'id',
                    sortOrder: 'asc',
                    columns: [[
                                { field: 'id', title: '序号', align:'center' ,width:100,},
                                {
                                    field: 'product_date', title: '生产日期', align: 'center',width:100,
                                    //formatter: function (value, row, index) {
                                    //    return (value==null? null:data_string(value).substr(0,10));
                                    //}
                                },
                                { field: 'cl_name', title: '生产班次', align: "center", width: 100, },
                                { field: 'hourid', title: '小时代号', align: "center", width: 100, },
                                { field: 'customer_num', title: '客户需求（套）', align: "center", width: 100, },
                                { field: 'real_num', title: '实际发运（套）', align: "center", width: 100, },
                                {
                                    field: 'real_customer', title: 'GAP 1',  align: "center",width:100,
                                    //formatter: function (value, row, index) {
                                    //    return (row.real_num - row.customer_num);
                                    //}
                                },
                                { field: 'plan_pro_num', title: '计划生产（套）', align: "center", width: 100, },
                                { field: 'real_pro_num', title: '实际生产（套）', align: "center", width: 100, },
                                {
                                    field: 'real_plan', title: 'GAP 2', align: "center", width: 100,
                                    //formatter: function (value, row, index) {
                                    //    if (row.plan_pro_num > 0) {
                                    //        return (row.real_pro_num - row.plan_pro_num);
                                    //    }else
                                    //    {
                                    //        return "0";
                                    //    }
                                    //}
                                },

                                {
                                    field: 'real_biplan', title: '完成率（%）', align: "center", width: 100,
                                    formatter: function (value, row, index) {
                                        return (value==null? null:parseFloat(value*100).toFixed(1)+"%");
                                    }
                                    //formatter: function (value, row, index) {
                                    //    if (row.plan_pro_num > 0) {
                                    //        return (row.real_pro_num / row.plan_pro_num) + "%";
                                    //    }
                                    //    else
                                    //    {
                                    //        return "0.0%";
                                    //    }
                                    //}
                                },
                                { field: 'line1_finish_num', title: '生产线1完成订单数', align: "center", width: 100, },
                                { field: 'line1_repair_num', title: '生产线1返修数', align: "center", width: 100, },
                                {
                                    field: 'line1_FTT', title: '生产线1FTT（%）', align: "center", width: 100,
                                    formatter: function (value, row, index) {
                                        return (value==null?null:parseFloat(value*100).toFixed(1)+"%");
                                    }
                                    //formatter: function (value, row, index) {
                                    //    if (row.line1_finish_num > 0) {
                                    //        return ((row.line1_finish_num - row.line1_repair_num) * 100.00 / row.line1_finish_num) + "%";
                                    //    }else
                                    //    {
                                    //        return "0.0%";
                                    //    }
                                    //}
                                },
                                { field: 'line2_finish_num', title: '生产线2完成订单数', align: "center", width: 100, },
                                { field: 'line2_repair_num', title: '生产线2返修数', align: "center", width: 100, },
                                {
                                    field: 'line2_FTT', title: '生产线2FTT（%）', align: "center", width: 100,
                                    formatter: function (value, row, index) {
                                        return (value == null ? null : parseFloat(value * 100).toFixed(1) + "%");
                                    }
                                    //formatter: function (value, row, index) {
                                    //    if (row.line2_finish_num > 0)
                                    //    {
                                    //        return ((row.line2_finish_num - row.line2_repair_num) * 100.00 / row.line2_finish_num) + "%";
                                    //    }
                                    //    else
                                    //    {
                                    //        return "0.0%";
                                    //    }
                                    
                                    //}
                                },
                                { field: 'line3_finish_num', title: '生产线3完成订单数', align: "center", width: 100, },
                                { field: 'line3_repair_num', title: '生产线3返修数', align: "center", width: 100, },
                                {
                                    field: 'line3_FTT', title: '生产线3FTT（%）', align: "center", width: 100,
                                    formatter: function (value, row, index) {
                                        return (value == null ? null : parseFloat(value * 100).toFixed(1) + "%");
                                    }
                                    //formatter: function (value, row, index) {
                                    //    if (row.line3_finish_num > 0) {
                                    //        return ((row.line3_finish_num - row.line3_repair_num) * 100.00 / row.line3_finish_num) + "%";
                                    //    }
                                    //    else {
                                    //        return "0.0%";
                                    //    }
                                    //}
                                },
                                { field: 'line4_finish_num', title: '生产线4完成订单数', align: "center", width: 100, },
                                { field: 'line4_repair_num', title: '生产线4返修数', align: "center", width: 100, },
                                {
                                    field: 'line4_FTT', title: '生产线4FTT（%）', align: "center", width: 100,
                                    formatter: function (value, row, index) {
                                        return (value == null ? null : parseFloat(value * 100).toFixed(1) + "%");
                                    }
                                    //formatter: function (value, row, index) {
                                    //    if (row.line4_finish_num > 0) {
                                    //        return ((row.line4_finish_num - row.line4_repair_num) * 100.00 / row.line4_finish_num)+"%";
                                    //    }
                                    //    else {
                                    //        return "0.0%";
                                    //    }
                                    //}
                                },
                                 { field: 'line5_finish_num', title: '生产线5完成订单数', align: "center", width: 100, },
                                { field: 'line5_repair_num', title: '生产线5返修数', align: "center", width: 100, },
                                {
                                    field: 'line5_FTT', title: '生产线5FTT（%）', align: "center", width: 100,
                                    formatter: function (value, row, index) {
                                        return (value == null ? null : parseFloat(value * 100).toFixed(1) + "%");
                                    }
                                    //formatter: function (value, row, index) {
                                    //    if (row.line5_finish_num > 0) {
                                    //        return ((row.line5_finish_num - row.line5_repair_num) * 100.00 / row.line5_finish_num) + "%";
                                    //    }
                                    //    else {
                                    //        return "0.0%";
                                    //    }
                                    //}
                                },
                                 { field: 'line6_finish_num', title: '生产线6完成订单数', align: "center", width: 100, },
                                { field: 'line6_repair_num', title: '生产线6返修数', align: "center", width: 100, },
                                {
                                    field: 'line6_FTT', title: '生产线6FTT（%）', align: "center", width: 100,
                                    formatter: function (value, row, index) {
                                        return (value == null ? null : parseFloat(value * 100).toFixed(1) + "%");
                                    }
                                    //formatter: function (value, row, index) {
                                    //    if (row.line6_finish_num > 0) {
                                    //        return ((row.line6_finish_num - row.line6_repair_num) * 100.00 / row.line6_finish_num) + "%";
                                    //    }
                                    //    else {
                                    //        return "0.0%";
                                    //    }
                                    //}
                                },
                    ]],

                    rownumbers: false,
                    loadMsg: '正在加载数据...',
                    toolbar: '#navigationSearch',
                    pagination: true,
                    
                    pageSize: 15,
                    pageList: [15,30, 60, 90],
                    onLoadSuccess: function (data) {//表单加载完后再加载此方法
                        console.log(data);
                       
                       
                        paint();
                        sumPrice();
                    }

                });

            }
            function sumPrice() {
                //添加“合计”列
                var rows = $('#gridTable').datagrid('getFooterRows');
                rows[0]["customer_num"] = compute("customer_num");
                rows[0]["real_num"] = compute("real_num");
                rows[0]["real_customer"] = compute("real_customer");

                rows[0]["plan_pro_num"] = compute("plan_pro_num");
                rows[0]["real_pro_num"] = compute("real_pro_num");
                rows[0]["real_pan"] = compute("real_pan");

                rows[0]["line1_finish_num"] = compute("line1_finish_num");
                rows[0]["line1_repair_num"] = compute("line1_repair_num");

                rows[0]["line2_finish_num"] = compute("line2_finish_num");
                rows[0]["line2_repair_num"] = compute("line2_repair_num");

                rows[0]["line3_finish_num"] = compute("line3_finish_num");
                rows[0]["line3_repair_num"] = compute("line3_repair_num");

                rows[0]["line4_finish_num"] = compute("line4_finish_num");
                rows[0]["line4_repair_num"] = compute("line4_repair_num");
                rows[0]["line5_finish_num"] = compute("line5_finish_num");
                rows[0]["line5_repair_num"] = compute("line5_repair_num");
                rows[0]["line6_finish_num"] = compute("line6_finish_num");
                rows[0]["line6_repair_num"] = compute("line6_repair_num");
                rows[0]["id"] = "汇总";
                $('#gridTable').datagrid('reloadFooter');
            }
           //获取所有列的值 
            function getCol(colName,boolflag,str)
            {
                var rows = $('#gridTable').datagrid('getRows');
                var arr = new Array();
                for (var i = 0; i < rows.length; i++) {
                    arr.push(rows[i][colName]);
                }
                return arr;
            }
            //指定列求和
            function compute(colName) {
                var rows = $('#gridTable').datagrid('getRows');
                var total = 0;
                for (var i = 0; i < rows.length; i++) {
                    total += parseFloat(rows[i][colName]);
                }
                return total;
            }
            //数据列表分页


            var grid = $('#gridTable');
            var p = grid.datagrid('getPager');
            $(p).pagination({
                beforePageText: '第',
                afterPageText: '页，共{pages}页',
                displayMsg: '当前显示从第{from}条到第{to}条 共{total}条记录',
                onBeforeRefresh: function () {

                }
            });
            //填充下拉框内容
            function CreateSelect() {
                $("#clnameid").empty();
                var optionstring = "";

                $.ajax({
                    url: "/HttpHandlers/Service1006_RPT_HOURLY.ashx?method=GetClassInfo",
                    type: "post",
                    dataType: "json",

                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        var data1 = data;
                        var i = 0;
                        for (i in data1) {
                            optionstring += "<option value=\"" + data1[i].cl_id + "\" >" + data1[i].cl_name.trim() + "</option>";
                        }
                        $("#clnameid").html("<option value=''>请选择...</option> " + optionstring);
                    },
                    error: function (msg) {
                        dialogMsg("数据访问异常", -1);
                    }
                });
            }


            function paint() {
                //横轴坐标 小时代号
                var x_categories = getCol('hourid', false, '');

                var customer_num = getCol('customer_num', false, '');
                var real_num = getCol('real_num', false, '');
                //var real_customer = getCol('real_customer', false, '');
                var plan_pro_num = getCol('plan_pro_num', false, '');
                var real_pro_num = getCol('real_pro_num', false, '');
                //var real_plan = getCol('real_plan', false, '');
                var line1_finish_num = getCol('line1_finish_num', false, '');
                //var line1_repair_num = getCol('line1_repair_num', false, '');
                var line2_finish_num = getCol('line2_finish_num', false, '');
                //var line2_repair_num = getCol('line2_repair_num', false, '');
                var line3_finish_num = getCol('line3_finish_num', false, '');
                //var line3_repair_num = getCol('line3_repair_num', false, '');
                var line4_finish_num = getCol('line4_finish_num', false, '');
                //var line4_repair_num = getCol('line4_repair_num', false, '');
                var line5_finish_num = getCol('line5_finish_num', false, '');
                //var line5_repair_num = getCol('line5_repair_num', false, '');
                var line6_finish_num = getCol('line6_finish_num', false, '');
                //var line6_repair_num = getCol('line6_repair_num', false, '');
                customer_num = JSON.parse('[' + customer_num + ']');
                real_num = JSON.parse('[' + real_num + ']');
                plan_pro_num = JSON.parse('[' + plan_pro_num + ']');
                real_pro_num = JSON.parse('[' + real_pro_num + ']');
                line1_finish_num = JSON.parse('[' + line1_finish_num + ']');
                line2_finish_num = JSON.parse('[' + line2_finish_num + ']');
                line3_finish_num = JSON.parse('[' + line3_finish_num + ']');
                line4_finish_num = JSON.parse('[' + line4_finish_num + ']');
                $('#container').highcharts({
                    chart: {
                        zoomType: 'xy'
                    },
                    title: {
                        text: '生产报表'
                    },
                    subtitle: {
                        text: '生产报表'
                    },
                    xAxis: [{
                        categories: x_categories,
                        crosshair: true
                    }],
                    yAxis: [{ // Primary yAxis
                        labels: {
                            format: '{value}套',
                            style: {
                                color: Highcharts.getOptions().colors[2]
                            }
                        },
                        title: {
                            text: '套数',
                            style: {
                                color: Highcharts.getOptions().colors[2]
                            }
                        },
                        opposite: true
                    }, { // Secondary yAxis
                        gridLineWidth: 0,
                        title: {
                            text: '',
                            style: {
                                color: Highcharts.getOptions().colors[0]
                            }
                        },
                        labels: {
                            format: '{value} ',
                            style: {
                                color: Highcharts.getOptions().colors[0]
                            }
                        }
                    }, { // Tertiary yAxis
                        gridLineWidth: 0,
                        title: {
                            text: '',
                            style: {
                                color: Highcharts.getOptions().colors[1]
                            }
                        },
                        labels: {
                            format: '{value} ',
                            style: {
                                color: Highcharts.getOptions().colors[1]
                            }
                        },
                        opposite: true
                    }],
                    tooltip: {
                        shared: true
                    },
                    series: [{
                        type: 'column',
                        yAxis: 2,
                        name: '实际发运（套）',
                        data: real_num,
                        tooltip: {
                            valueSuffix: ' 套'
                        }
                    },
                             {
                                 name: '实际生产（套）',
                                 data: real_pro_num,
                                 type: 'column',
                                 yAxis: 2,
                                 tooltip: {
                                     valueSuffix: ' 套'
                                 }
                             }, {
                                 type: 'column',
                                 yAxis: 2,
                                 name: '生产线1完成订单数',
                                 data: line1_finish_num,
                             }, {
                                 type: 'column',
                                 yAxis: 2,
                                 name: '生产线2完成订单数',
                                 data: line2_finish_num
                             }, {
                                 type: 'column',
                                 yAxis: 2,
                                 name: '生产线3完成订单数',
                                 data: line3_finish_num
                             },
                             {
                                 type: 'column',
                                 yAxis: 2,
                                 name: '生产线4完成订单数',
                                 data: line4_finish_num
                             },
                             {
                                 name: '客户需求(套)',
                                 type: 'line',
                                 yAxis: 1,
                                 data: customer_num,
                                 tooltip: {
                                     valueSuffix: ' 套'
                                 }
                             }, {
                                 name: '计划生产（套）',
                                 type: 'line',
                                 yAxis: 1,
                                 data: plan_pro_num,
                                 marker: {
                                     enabled: false
                                 },
                                 tooltip: {
                                     valueSuffix: ' 套'
                                 }
                             }
                    ]
                });

            }

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

       
        //格式化时间字符串
        function data_string(str) {
            var d = eval('new ' + str.substr(1, str.length - 2));
            var ar_date = [d.getFullYear(), d.getMonth() + 1, d.getDate(), d.getHours(), d.getMinutes(), d.getSeconds()];
            for (var i = 0; i < ar_date.length; i++) ar_date[i] = dFormat(ar_date[i]);
            return ar_date.slice(0, 3).join('-') + ' ' + ar_date.slice(3).join(':');

            function dFormat(i) { return i < 10 ? "0" + i.toString() : i; }
        }
        //查询业务
        function searchName() {
            var clnameid = $('#clnameid option:selected').val();
            var clname = $('#clnameid option:selected').text();
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');

            $('#gridTable').datagrid('reload', { method: "GetList", clnameid: clnameid, clname:clname,start_time: start_time, end_time: end_time });
        }
        //function excelFor()
        //{
        //    var clnameid = $('#clnameid').val();
        //    var start_time = $('#start_time').datetimebox('getValue');
        //    var end_time = $('#end_time').datetimebox('getValue');
            
        //    var queryParams = {
        //        clnameid: clnameid,
        //        start_time:start_time,
        //        end_time:end_time,
        //        method:"Export"
        //    }

        //    $.ajax({
        //        type: "get",
        //        async: false,
        //        url: "hs/Service1006_RPT_HOURLY.ashx",
        //        data: queryParams,
        //        success: function (data) {
        //            console.log(data);
        //            //alert(data.Result);
        //            if (data.Result == "true") {

        //                alert('导出成功');
        //                $("#sub").click();
        //                //dg.datagrid('reload');
        //            }
        //            else alert('导出失败');
        //            $('#w').window('close');
        //        },
        //        error: function () {
        //        }
        //    });
        //}
        function excelFor()
        { //导出当前页面数据 begin
            //Export('生产报表', $('#gridTable'));
            //导出当前页面end 
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');
            var clnameid = $('#clnameid option:selected').val();
            var clname = $('#clnameid option:selected').text();
            $.ajax({
                type: 'post',
                url: '/HttpHandlers/Service1006_RPT_HOURLY.ashx?method=Export',
                async: false,
                cache: false,
                dataType: 'json',
                data: { "clname": "" + clname + "", "clnameid": "" + clnameid + "", "start_time": "" + start_time + "", "end_time": "" + end_time + "", "method": "Export" },
                cache: false,
                success: function (data) {
                    if (data.Result == "true") {
                        $("#subs").click();
                    }
                    else {
                        alert("导出失败");
                    }
                }
            });
        }
        function print()
        {
            //$('#gridPanel').printTable(gridPanel);
            //CreateFormPage("生产报表", $("#gridTable"));
            // $('#printArea').print();
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');
            var clnameid = $('#clnameid option:selected').val();
            var clname = $('#clnameid option:selected').text();
            $.ajax({
                type: 'post',
                url: '/HttpHandlers/Service1006_RPT_HOURLY.ashx?method=Print',
                async: false,
                cache: false,
                dataType: 'json',
                data: { "clname": "" + clname + "", "clnameid": "" + clnameid + "", "start_time": "" + start_time + "", "end_time": "" + end_time + "", "method": "Print" },
                cache: false,
                success: function (data) {
                    if (data.Result == "true") {
                        var html = data.Html;
                        console.log(html);
                        var opt = $.extend({
                            debug: false,
                            preview: true,     // 是否预览
                            table: true,       // 是否打印table
                            usePageStyle: true  // 是否使用页面中的样式
                        }, opt);
                        var _self = opt, timer, firstCall, win, $html = $(html);
                        var $iframe = $("<iframe  />");

                        if (!opt.debug) { $iframe.css({ position: "absolute", width: "0px", height: "0px", left: "-600px", top: "-600px" }); }

                        $iframe.appendTo("body");
                        win = $iframe[0].contentWindow;

                        $(win.document.body).append($html);
                        //console.log($html);
                        win.print();
                    }
                    else {
                        alert("导出失败");
                    }
                }
            });
        }
    </script>
</asp:Content>








