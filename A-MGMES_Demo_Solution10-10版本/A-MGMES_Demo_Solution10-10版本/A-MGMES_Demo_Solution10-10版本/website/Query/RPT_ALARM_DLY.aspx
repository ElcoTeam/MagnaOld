<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master"  AutoEventWireup="true" CodeBehind="RPT_ALARM_DLY.aspx.cs" Inherits="website.Query.RPT_ALARM_DLY" %>
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
    <style type="text/css">
      
 #left{width: 50%;float: left; background: #ffffff;}

 #right{width: 50%;float: left;background: #ffffff;}
  
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
 <div id ="gridPanel">
    <div id="printArea" class="printArea" closed="true"> 
        <div id="left">
        <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 50%">
            <tr>
                <td class="title"  >
                    
                        选择日期：<br />
                    Choose the Date
                    
                </td>  
                <td style="width: 120px">
                    <input id="start_time" class="easyui-datebox" data-options="required:true,showTime:false" />
                </td>
                
                <td >
                  <a style="font-size:12px;font-weight:700;color:#000000" class="easyui-linkbutton btn btn-default" href="javascript:;" onclick="searchName()"">查询</a>
                </td>
                 <td>  
                  <input id="subs" type="submit"  value="导出Excel" hidden="hidden"/>
                  <a style="font-size:12px;font-weight:700;color:#000000" class="easyui-linkbutton btn btn-default" href="javascript:;" onclick ="excelFor()">导出</a>
                </td>
                 <td>  
                  <a style="font-size:12px;font-weight:700;color:#000000" class="easyui-linkbutton btn btn-default" href="javascript:;" onclick ="print()">打印</a>
                </td>
            </tr>
        </table>

    </div>
            <!-- 数据表格  -->
            <table id="gridTable"  style="width: 99%;">
            </table>
        </div>
        <div id="right" class="chart-Panel">
            <div id="label_time" style="width: 100%; font-size:14px;padding:5px;font-weight:700;color:#000000; text-align:center;margin: 0 auto;" >
               
            </div>
              <div id="upright_container" style="width: 100%; height: 50%; text-align:center;padding:15px;  margin: 0 auto;border:1px">

              </div>
              <div id="downright_container" style="width: 100%; height: 50%; text-align:center; padding:15px; margin: 0 auto;border:1px">
           
              </div>
         </div>
     </div> 
   </div>
    <script>



        /****************       全局变量          ***************/
        var dg = $('#gridTable');      //表格
        var queryParams;
        /****************       DOM加载          ***************/
        $(function () {
            var date_t = new Date();
            $("#start_time").datebox('setValue', date_t.toString());
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
            queryParams = 
                {
                    date_time: $("#start_time").datetimebox('getValue')
                }
            var col = $('#gridTable').width();
                dg = $('#gridTable').datagrid({
                    fitColumns: true,
                    nowrap: false,
                    striped: true,
                    collapsible: false,
                    url: '/HttpHandlers/RPT_ALARM_DLY.ashx?method=GetListNew',
                    showFooter:true,
                    sortName: 'stationNo',
                    sortOrder: 'asc',
                    queryParams:queryParams,
                    columns: [[
                                { field: 'id', title: '序号', align:'center' ,width:100,hidden:true},
                                {
                                    field: 'stationNo', title: 'Station\n\t工位', align: 'center', width: 100,
                                    styler:setStyler,
                                    
                             
                                },
                                { field: 'stationName', title: 'Station Name 工位名称', align: "center", width: 100, },
                                {
                                    field: 'material_num', title: 'Material 物料', align: "center", width: 100,
                                    styler: setMaterial_Styler,
                                },
                                {
                                    field: 'production_num', title: 'Production 生产', align: "center", width: 100,
                                    styler: setProduction_Styler,
                                },
                                {
                                    field: 'maintenance_num', title: 'Maintenance 维修', align: "center", width: 100,
                                    styler: setMaintenance_Styler,
                                },
                                {
                                    field: 'quality_num', title: 'Quality 质量', align: "center", width: 100,

                                    styler: setQuality_Styler,
                                },
                                { field: 'overcycle_num', title: 'Overcycle 超时', align: "center", width: 100, },
                                {
                                    field: 'total_num', title: 'Total 总计', align: "center", width: 100,
                                    styler: setTotal_Styler,
                                },
                               
                    ]],

                    rownumbers: false,
                    loadMsg: '正在加载数据...',
                    toolbar: '#navigationSearch',
                    pagination: true,
                    
                    pageSize: 30,
                    pageList: [30, 60, 90],
                    //loader: myLoader, //前端分页加载函数  
                    onLoadSuccess: function (data) {//表单加载完后再加载此方法
                        console.log(data);
                       // $("#gridTable").data().datagrid.cache = null;//清除datagrid 缓存，保证前台假分页；  
                       
                        
                        sumPrice();
                        paint();
                    }

                });

        }

        //实现假分页  
        function myLoader(param, success, error) {
            var that = $(this);
            var opts = that.datagrid("options");
            if (!opts.url) {
                return false;
            }
            var cache = that.data().datagrid.cache;
            if (!cache) {
                $.ajax({
                    type: opts.method,
                    url: opts.url,
                    data: param,
                    dataType: "json",
                    success: function (data) {
                        that.data().datagrid['cache'] = data;
                        success(bulidData(data));
                    },
                    error: function () {
                        error.apply(this, arguments);
                    }
                });
            } else {
                success(bulidData(cache));
            }

            function bulidData(data) {
                var temp = $.extend({}, data);
                var tempRows = [];
                var start = (param.page - 1) * parseInt(param.rows);
                var end = start + parseInt(param.rows);
                var rows = data.rows;
                for (var i = start; i < end; i++) {
                    if (rows[i]) {
                        tempRows.push(rows[i]);
                    } else {
                        break;
                    }
                }
                temp.rows = tempRows;
                return temp;
            }
        }

            function sumPrice() {
                //添加“合计”列
                var rows = $('#gridTable').datagrid('getFooterRows');
                rows[0]["material_num"] = compute("material_num");
                rows[0]["production_num"] = compute("production_num");
                rows[0]["maintenance_num"] = compute("maintenance_num");

                rows[0]["quality_num"] = compute("quality_num");
                rows[0]["overcycle_num"] = compute("overcycle_num");
                rows[0]["total_num"] = compute("total_num");
         
                rows[0]["stationNo"] = "summary";
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
                
            }


            function paint() {
                //横轴坐标 小时代号
                var lheight = $("#left").height();
                $("#right").height(lheight);
                var theight = $(".top").height();
                $("#lable_time").height(theight);
                $('#upright_container').height((lheight - theight-30) * 0.5);
                $('#downright_container').height((lheight - theight-30) * 0.45);
                var date_time = $('#start_time').datetimebox('getValue');
                date_time = date_time.substr(0, 10);
                $("#label_time").html(date_time + " 生产MES报警数据分析");
                 
                var x_categories = getCol('stationNo', false, '');

                var material_num = getCol('material_num', false, '');
                var production_num = getCol('production_num', false, '');
                
                var maintenance_num = getCol('maintenance_num', false, '');
                var quality_num = getCol('quality_num', false, '');
                
                var overcycle_num = getCol('overcycle_num', false, '');
               
                material_num = JSON.parse('[' + material_num + ']');
                production_num = JSON.parse('[' + production_num + ']');
                maintenance_num = JSON.parse('[' + maintenance_num + ']');
                quality_num = JSON.parse('[' + quality_num + ']');
                overcycle_num = JSON.parse('[' + overcycle_num + ']');
                $('#upright_container').highcharts({
                    chart: {
                        type:'column',
                        zoomType: 'xy',
                        borderColor: '#EBBA95',
                        borderWidth: 2,
                    },
                    title: {
                        text: '生产线MES 报警数据分析',
                        style: {
                            fontSize: "14px",
                            
                            fontWeight: 'bold'
                        }
                    },
                    subtitle: {
                        text: 'Production Line MES Alarm Data Analysis'
                    },
                    xAxis: [{
                        categories: x_categories,
                        crosshair: true
                    }],
                    yAxis: {
                        min: 0,
                        title: {
                            text: ''
                        },
                        stackLabels: {
                            enabled: true,
                            style: {
                                fontWeight: 'bold',
                                color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                            }
                        }
                    },
                    //legend: {
                    //    align: 'center',
                    //    x: -15,
                    //    verticalAlign: 'bottom',
                    //    y: -5,
                    //    floating: true,
                    //    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    //    borderColor: '#CCC',
                    //    borderWidth: 1,
                    //    shadow: false
                    //},
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.x + '</b><br/>' +
                                this.series.name + ': ' + this.y + '<br/>' +
                                '总量: ' + this.point.stackTotal;
                        }
                    },
                    plotOptions: {
                        column: {
                            stacking: 'normal',
                            dataLabels: {
                                enabled: true,
                                color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                                style: {
                                    textShadow: '0 0 3px black'
                                }
                            }
                        }
                    },
                    series: [{
                       
                        name: '超时',
                        data: overcycle_num,
                        
                    },
                             {
                                 name: '质量',
                                 data: quality_num,
                                 
                             }, {
                                 
                                 name: '维修',
                                 data: maintenance_num,
                             }, 
                             {
                                 name: '生产',
                                
                                 data: production_num,
                                 
                             }, {
                                 name: '物料',
                                 
                                 data: material_num,
                                
                             }
                    ]
                });
               
                var material_sum= compute("material_num");
                var production_sum = compute("production_num");
                var maintenance_sum = compute("maintenance_num");

                var quality_sum  = compute("quality_num");
                var overcycle_sum = compute("overcycle_num");
                material_sum = JSON.parse('[' + material_sum + ']');
                production_sum = JSON.parse('[' + production_sum + ']');
                maintenance_sum = JSON.parse('[' + maintenance_sum + ']');
                quality_sum = JSON.parse('[' + quality_sum + ']');
                overcycle_sum = JSON.parse('[' + overcycle_sum + ']');
                $('#downright_container').highcharts({
                    chart: {
                        type: 'bar',
                        borderColor: '#EBBA95',
                        borderWidth: 2,
                    },
                    title: {
                        text: '生产线MES报警数据分析',
                        style: {
                            fontSize: "14px",

                            fontWeight: 'bold'
                        }
                    },
                    subtitle: {
                        text: 'Production Line MES Alarm Data Anlaysis'
                    },
                    xAxis: {
                        categories: ['报警数目'],
                        gridLineWidth: 0,
                        title: {
                            text: date_time
                        }
                    },
                    yAxis: {
                        gridLineWidth: 0,
                        title: {
                            text: '报警',
                            align: 'high'
                        },
                    },
                    tooltip: {
                        valueSuffix: ' '
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: true,
                                allowOverlap: false
                            }
                        }
                    },
                    legend: {
                        layout: 'horizontal',
                        align: 'center',
                        verticalAlign: 'bottom',
                        floating: true,
                        borderWidth: 0,
                        backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                        shadow: false
                    },
                    credits: {
                        enabled: false
                    },
                    series: [{
                        name: '质量',
                        data: quality_sum
                    },
                             {
                                 name: '维修',
                                 data: maintenance_sum
                             },
                             {
                                 name: '生产',
                                 data: production_sum
                             },
                             {
                                 name: '物料',
                                 data: material_sum
                             }]
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
            function setStyler(value, row, index) {
               
                if(value.includes("FSA"))
                {
                    return 'background-color:green;';
                }
                else if(value.includes("FSB"))
                {
                    return 'background-color:#00b0f0;';//蓝色
                }
                else if (value.includes("FSC")) {
                    return 'background-color:#00b050;';//绿色
                }
                else if (value.includes("RSB")) {
                    return 'background-color:#ffc000;';//橙色
                }
                else if (value.includes("RSC")) {
                    return 'background-color:#00b050;';//绿色
                }
            }
            function setMaterial_Styler(value, row, index) {
                
                if (value ==0) {
                    return 'background-color:green;';
                }
                else if (value >=1 ) {
                    return 'background-color:#ff0000;';//红色
                }
                
            }
            function setProduction_Styler(value, row, index) {

                if (value == 0) {
                    return 'background-color:green;';
                }
                else if (value >= 1) {
                    return 'background-color:#ff0000;';//红色
                }

            }
            function setMaintenance_Styler(value, row, index) {

                if (value == 0) {
                    return 'background-color:green;';
                }
                else if (value ==1 ) {
                    return 'background-color:#ffff99;';
                }
                else if (value == 2) {
                    return 'background-color:#ffcc66;';
                }
                else if (value == 3) {
                    return 'background-color:#e26b0a;';
                }
                else if (value > 3) {
                    return 'background-color:#ff0000;';
                }

            }
            function setQuality_Styler(value, row, index) {

                if (value == 0) {
                    return 'background-color:green;';
                }
                else if (value == 1) {
                    return 'background-color:#ffff99;';
                }
                else if (value == 2) {
                    return 'background-color:#ffcc66;';
                }
                else if (value == 3) {
                    return 'background-color:#e26b0a;';
                }
                else if (value > 3) {
                    return 'background-color:#ff0000;';
                }

            }
            function setTotal_Styler(value, row, index) {

                if (value == 0) {
                    return 'background-color:green;';
                }
                else if (value == 1) {
                    return 'background-color:#ffff99;';
                }
                else if (value == 2) {
                    return 'background-color:#ffcc66;';
                }
                else if (value == 3) {
                    return 'background-color:#e26b0a;';
                }
                else if (value > 3) {
                    return 'background-color:#ff0000;';
                }

            }
       
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
           
            var date_time = $('#start_time').datetimebox('getValue');
           
            $('#gridTable').datagrid('reload', { method: "GetList", date_time:date_time});
        }
       
        function excelFor()
        {
            
                ////导出当前页面 begin 
            //Export('生产线报警报表', $('#gridTable'));
               // //导出当前页面end
                ////导出所有数据 begin
                var date_time = $('#start_time').datetimebox('getValue');
                $.ajax({
                    type: 'post',
                    url: '/HttpHandlers/RPT_ALARM_Dly.ashx?method=Export',
                    async: false,
                    cache: false,
                    dataType: 'json',
                    data: { "date_time": "" + date_time + "",  "method": "Export" },
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
                //导出所有数据end

            
        }
        function print()
        {
            
            CreateFormPage("生产线报警报表", $("#gridTable"));
           
        }
    </script>
</asp:Content>








