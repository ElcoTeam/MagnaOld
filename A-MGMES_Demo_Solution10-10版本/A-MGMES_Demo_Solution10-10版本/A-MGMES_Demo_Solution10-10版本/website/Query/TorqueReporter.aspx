<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="TorqueReporter.aspx.cs" Inherits="website.Query.TorqueReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
    <link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>
    <script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>
    <style>
        html, body, #aspnetForm, .panel-noscroll{
            height: 100%;
        }

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
        .calendar-day {
	        height:auto!important;
        }
    </style>
     
    <script src="../My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top">


        <style>
            #search_li {
                margin: 0px;
                padding: 0px;
            }

                #search_li li {
                    display: block;
                    float: left;
                    height: 30px;
                    line-height: 30px;
                    margin: 0px 5px;
                }

                    #search_li li span {
                        display: block;
                        float: left;
                    }

                    #search_li li div {
                        float: left;
                    }
        </style>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <thead>
                <tr>
                    <td>扭矩/角度信息分析</td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <ul id="search_li">
                            <li>
                                <span>流水线</span>
                                <div>
                                    <select id="fl_id_s" class="easyui-combobox uservalue" 
                                      data-options="valueField: 'fl_id',textField: 'fl_name',onChange:function(){reloadst_no_s();}">
                                     <option value="">请选择</option>
                                    </select>
                                </div>
                            </li>
                            <li>
                                <span>工位</span>
                                <div>
                                    <select id="st_no_s" class="easyui-combobox uservalue" 
                                       data-options="valueField: 'st_no',textField: 'st_no',onChange:function(){reloadpart_id_s();}">
                                    <option value="">请选择</option>
                                    </select>
                                </div>
                            </li>
                            <li>
                                <span>部件</span>
                                <div>
                                    <select id="part_id_s" class="easyui-combobox uservalue" 
                                        data-options="valueField: 'part_no',textField: 'part_no'">
                                     <option value="">请选择</option>
                                    </select>
                                </div>
                            </li>
                            <li>
                                <span>开始时间</span>
                                <div>
                                    <%--<input id="start_time" class="easyui-datetimebox" data-options="showSeconds:false"/>--%>
                                    <input id="start_time"  type="text" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" class="Wdate" style="height:25px;" />
                                </div>
                            </li>
                            <li>
                                <span>结束时间</span>
                                <div>
                                    <%--<input id="end_time" class="easyui-datetimebox" data-options="showSeconds:false"/>--%>
                                    <input id="end_time"  type="text" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" class="Wdate" style="height:25px;" />
                                </div>
                            </li>
                            <li>
                                <div>
                                    <select id="data_type" class="easyui-combobox uservalue" 
                                        data-options="valueField: 'type_name',textField: 'type_name',onChange:function(){loadChart();}">
                                        <option value="扭矩" selected>扭矩</option>
                                        <option value="角度">角度</option>
                                    </select>
                                </div>
                            </li>
                        </ul>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td align="right">
                        <a class="topsearchBtn">生成图表</a>
                    </td>
                </tr>
            </tfoot>

        </table>

    </div>
    <div class="easyui-layout" data-options="fit:true">
        <div id="chart_container" style="height: 100%; width: 100%">
        </div>
    </div>
    <script type="text/javascript" src="../js/echarts.js"></script>
    <script type="text/javascript">
        var myChart;

        require.config({
            paths: {
                echarts: '/js'
            }
        });
        /**
  * 
  * 获取当前时间
  */
        function p(s) {
            return s < 10 ? '0' + s : s;
        }
        function nowtime() {
            var myDate = new Date();
            //获取当前年
            var year = myDate.getFullYear();
            //获取当前月
            var month = myDate.getMonth() + 1;
            //获取当前日
            var date = myDate.getDate();
            var h = myDate.getHours();       //获取当前小时数(0-23)
            var m = myDate.getMinutes();     //获取当前分钟数(0-59)
            var s = myDate.getSeconds();

            var now = year + '-' + p(month) + "-" + p(date) + " " + p(h) + ':' + p(m) + ":" + p(s);
            return now;
        }
        $(function () {
          
            $('#start_time').val(nowtime());
            $('#end_time').val(nowtime());
            require(
							[
									'echarts',
									'echarts/chart/line',
							],
							function (ec) {
							    myChart = ec.init(document.getElementById('chart_container'));
							    window.onresize = myChart.resize
							}
			);
            //搜索按钮
            $('.topsearchBtn').first().click(function () {
                loadChart();
            });

            
            //所属工位下拉框数据加载  
            reloadfl_id_s();
           
             //reloadst_no_s();
            //reloadpart_id_s();

            //loadChart();
            
           
        });

        function loadChart() {
            
             //myChart.showLoading({
             //    effect:'whirling'
             //});
            
            var fl_id = $('#fl_id_s').combo('getValue');
            var st_no = $('#st_no_s').combo('getValue');
            var part_no = $('#part_id_s').combo('getValue');
            var chart_Type = $('#data_type').combo('getValue');
            var starttime = $('#start_time').val().trim();
            var endtime = $('#end_time').val().trim();
            if (fl_id == "" || st_no == "" || part_no == "")
            {
                alert("请选择流水线，工位以及部件");
                return false;
            }
            if (fl_id == "请选择" || st_no == "请选择" || part_no == "请选择") {
                alert("请选择流水线，工位以及部件");
                return false;
            }

            $.ajax({
                type: 'get',
                url: '/HttpHandlers/TorqueReporterHandler.ashx',
                data: { fl_id: fl_id, st_no: st_no, part_no: part_no,starttime: starttime, endtime: endtime },
                dataType: 'json',
                cache: false,
                success: function (data) {
                    var option = {
                        title: {
                            text: chart_Type + '信息',
                            x: 'center'
                        },
                        tooltip: {
                            show: true,
                        },
                        toolbox: {
                            show: true,
                            feature: {
                                saveAsImage: { show: true }
                            }
                        },
                        legend: {
                            x: "80%",
                            y: "top",
                            itemWidth: 16,
                            itemHeight: 16,
                            itemGap: 20,
                            selectedMode: false,
                            textStyle: {
                                fontSize: 14,
                                align: "right"
                            }
                        },
                        xAxis: [
								{
								    type: 'category',
								    axisLine: {
								        show: false
								    },
								    splitLine: {
								        show: false
								    },
								    axisTick: false
								    //data : ["2012年下","2013年下","2014年","2015年","2016年1季度"]
								}
                        ],
                        yAxis: [
								{
								    type: 'value',
								    axisLine: {
								        show: false
								    },
								    splitLine: {
								        lineStyle: {
								            type: 'dotted',
								            width: 2,
								            color: "#9dbad1"
								        }
								    }
								}
                        ],
                        grid: {
                            borderWidth: 0,
                        },
                        series: []
                    };
                    var values = {};
                    var fl_name = '', part_no = '', st_no = '';
                    var legends = [];
                    var xAxis = [];
                    var series = [];
                    var currentSeries = null;
                    var newSeries = false;
                    var currentData = null;
                    for (var index = 0; index < data.length; index++) {
                        var item = data[index];
                        if (fl_name != item.fl_name) {
                            fl_name = item.fl_name;
                            part_no = item.part_no;
                            st_no = item.st_no;
                            newSeries = true;
                        } else if (part_no != item.part_no) {
                            part_no = item.part_no;
                            st_no = item.st_no;
                            newSeries = true;
                        } else if (st_no != item.st_no) {
                            st_no = item.st_no;
                            newSeries = true;
                        }

                        if (newSeries) {
                            var seriesName = fl_name + '|' + st_no + '|' + part_no;
                            currentSeries = {
                                name: seriesName + '|' + chart_Type + "图",
                                type: "line",
                                barMaxWidth: 60,
                                //data:[1200, 1200, 800, 500, 1340],
                                itemStyle: {
                                    normal: {
                                        label: {
                                            show: true,
                                            textStyle: {
                                                color: "#000"
                                            }
                                        },
                                        color: "#ffae00"
                                    }
                                }
                            }
                            currentData = [];
                            currentSeries.data = currentData;
                            legends.push(seriesName);
                            series.push(currentSeries);
                            newSeries = false;
                        }
                        if (chart_Type == '扭矩') {
                            currentData.push(item.TorqueResult);
                            // alert(currentData);
                        } else {
                            currentData.push(item.AngleResult);
                        }
                        if (!values[index]) {
                            xAxis.push(item.step_order);
                            values[index] = "1";
                        }
                    }
                    if (xAxis.length > 0) {
                        option.series = series;
                        option.xAxis[0].data = xAxis;
                        option.legend.data = legends;
                    }
                    myChart.setOption(option, true);
                    myChart.hideLoading();
                },
                complete: function () {
                    myChart.hideLoading();
                }
            });
           
            

        }

        function reloadfl_id_s() {
           
            $('#fl_id_s').combobox({
                    url: '/HttpHandlers/TorqueReporterHandler.ashx?method=get_fl_listforTor',
                    method: "post",
                   valueField: 'fl_id',
                   textField: 'fl_name',
                   onChange: function(){
                       reloadst_no_s();
                        },
                    onLoadSuccess: function () {
                        //var data = $(this).combobox("getData");
                        //if (data.length > 0) {
                        //    $('#fl_id_s').combobox('select', data[0].fl_id);
                          
                        //}
                        $('#fl_id_s').combobox('setValue', '请选择');
                       
                    }
                });
            
        }

        function reloadst_no_s() {
          
            var fl_id = $('#fl_id_s').combobox('getValue');
            if (fl_id == "" ) {
               
                return false;
            }
            if (fl_id == "请选择") {
              
                return false;
            }

            $('#st_no_s').combobox({
                url: '/HttpHandlers/TorqueReporterHandler.ashx?method=get_st_listforTorque&fl_id=' + fl_id,
                method: "post",
                valueField: 'st_no',
                textField: 'st_no',
                onChange: function(){
                    reloadpart_id_s();
                },
                onLoadSuccess: function () {
                    //var data = $(this).combobox("getData");
                    //if (data.length > 0) {
                    //    $('#st_no_s').combobox('select', data[0].st_no);
                       
                    //}
                }
            });
        }

        function reloadpart_id_s() {
           
            var fl_id = $('#fl_id_s').combobox('getValue');
            var st_no = $('#st_no_s').combobox('getValue');
            if (fl_id == "" || st_no == "") {
               
                return false;
            }
            if (fl_id == "请选择" || st_no == "请选择" ) {
               
                return false;
            }

            $('#part_id_s').combobox({
                url: '/HttpHandlers/TorqueReporterHandler.ashx?method=get_part_list&fl_id=' + fl_id + '&st_no=' + st_no,
                method: "post",
                valueField: 'part_no',
                textField: 'part_no',
                onLoadSuccess: function () {
                    //var data = $(this).combobox("getData");
                    //if (data.length > 0) {
                    //    $('#part_id_s').combobox('select', data[0].part_no);
                   
                    //}
                    
                }
            });

            //$('#part_id_s').combobox('clear');
            //var fl_id = $('#fl_id_s').combobox('getValue');
            //var st_id = $('#st_no_s').combobox('getValue');
            
            //$('#part_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_part_list&fl_id=' + fl_id + '&st_id=' + st_id);
            
        }
    </script>
</asp:Content>
