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

 #left{width: 50%;float: left; background: #fff;}

 #right{width: 40%;float: left;background: #fff;}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 70%">
            <tr>生产线报警报表</tr>
            <tr>
                <td class="title"  >
                    
                        选择日期：<br />
                    Choose the Date
                    
                </td>  
                <td style="width: 120px">
                    <input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                
                <td >
                  <a style="font-size:12px;font-weight:700;color:#000000" class="easyui-linkbutton btn btn-default" href="javascript:;" onclick="searchName()"">查询</a>
                </td>
                 <td>  
                  <input id="sub" type="submit"  value="导出Excel" hidden="hidden"/>
                  <a style="font-size:12px;font-weight:700;color:#000000" class="easyui-linkbutton btn btn-default" href="javascript:;" onclick ="excelFor()">导出</a>
                </td>
                 <td>  
                  <a style="font-size:12px;font-weight:700;color:#000000" class="easyui-linkbutton btn btn-default" href="javascript:;" onclick ="print()">打印</a>
                </td>
                <td>  
                  <span id="label_time"></span> 生产MES报警数据分析
                </td>
            </tr>
        </table>

    </div>
 <div id ="gridPanel">
    <div id="printArea" class="printArea" closed="true"> 
        <div id="left">
            <!-- 数据表格  -->
            <table id="gridTable" title="生产线MES报警" style="width: 99%;">
            </table>
        </div>
        <div id="right" class="chart-Panel">
              <div id="upright_container" style="width: 100%; height: 50%; text-align:center;  margin: 0 auto;border:1px">

              </div>
              <div id="downright_container" style="width: 100%; height: 50%; text-align:center;  margin: 0 auto;border:1px">
           
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
            var col = $('#gridTable').width();
                dg = $('#gridTable').datagrid({
                    fitColumns: true,
                    nowrap: false,
                    striped: true,
                    collapsible: false,
                    url: '/HttpHandlers/RPT_ALARM_DLY.ashx?method=GetList',
                    showFooter:true,
                    sortName: 'stationNo',
                    sortOrder: 'asc',
                    columns: [[
                                { field: 'id', title: '序号', align:'center' ,width:100,hidden:true},
                                {
                                    field: 'stationNo', title: 'Station\n\t工位', align: 'center',width:100,
                             
                                },
                                { field: 'stationName', title: 'Station Name 工位名称', align: "center", width: 100, },
                                { field: 'material_num', title: 'Material 物料', align: "center", width: 100, },
                                { field: 'production_num', title: 'Production 生产', align: "center", width: 100, },
                                { field: 'maintenance_num', title: 'Maintenance 维修', align: "center", width: 100, },
                                {
                                    field: 'quality_num', title: 'Quality 质量',  align: "center",width:100,
                                   
                                },
                                { field: 'overcycle_num', title: 'Overcycle 超时', align: "center", width: 100, },
                                {
                                    field: 'total_num', title: 'Total 总计', align: "center", width: 100,
                                    
                                },
                               
                    ]],

                    rownumbers: false,
                    loadMsg: '正在加载数据...',
                    toolbar: '#navigationSearch',
                    pagination: true,
                    
                    pageSize: 30,
                    pageList: [30, 60, 90],
                    onLoadSuccess: function (data) {//表单加载完后再加载此方法
                        console.log(data);
                       
                       
                        
                        sumPrice();
                        paint();
                    }

                });

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
                        zoomType: 'xy'
                    },
                    title: {
                        text: '生产线MES 报警数据分析'
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
                var date_time = $('#start_time').datetimebox('getValue');
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
                        type: 'bar'
                    },
                    title: {
                        text: '生产线MES报警数据分析'
                    },
                    subtitle: {
                        text: 'Production Line MES Alarm Data Anlaysis'
                    },
                    xAxis: {
                        categories: [],
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
            Export('生产线报警报表', $('#gridTable'));
        }
        function print()
        {
            
            CreateFormPage("生产线报警报表", $("#gridTable"));
           
        }
    </script>
</asp:Content>








