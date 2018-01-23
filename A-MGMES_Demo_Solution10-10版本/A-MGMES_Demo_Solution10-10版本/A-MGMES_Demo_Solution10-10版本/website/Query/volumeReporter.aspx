<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="volumeReporter.aspx.cs" Inherits="website.Query.volumeReporter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
	<link href="/js/uploadify/uploadify.css" rel="stylesheet" />
	<script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>
	<script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>
    <script src="/js/echarts.js"></script>
	<style>
		html, body,#aspnetForm{
			height: 100%
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
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="subs" type="submit"  value="导出Excel" hidden="hidden"/>
	<div class="top">

        <style>
            #search_li {
                margin:0px;padding:0px;
            }
            #search_li li {
                display:block;float:left;
                height:30px;line-height:30px;margin:0px 5px;
            }
            #search_li li span {
                display:block;float:left;
            }
            #search_li li div {
                float:left;
            }
        </style>
  
		<table cellpadding="0" cellspacing="0" style="width: 100%">
            <thead>
                <tr>
                    <td>产量报表</td>
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
						                data-options="valueField: 'fl_id',textField: 'fl_name',onChange:reloadst_id_s">
					                </select>
                                </div>
                            </li>
                            <li>
                                <span>工位</span>
                                <div>
                                    <select id="st_id_s" class="easyui-combobox uservalue" 
						                data-options="valueField: 'st_no',textField: 'st_no'">
					                </select>
                                </div>
                            </li>
                            <li>
                                <span>开始时间</span>
                                <div>
                                    <input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false"/>
                                </div>
                            </li>
                            <li>
                                <span>结束时间</span>
                                <div>
                                    <input id="end_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                                </div>
                            </li>
                            <li>
                                <div>
                                    <select id="reportType" class="easyui-combobox uservalue" 
						                data-options="onChange:function(){ reportTypeChanged(); }">
						                <option value="1">按小时</option>
						                <option value="2" selected>按天</option>
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
                        <a class="topexcelBtn" href="javascript:;" onclick ="excelFor()">导出Excel</a>
                        <a class="topsearchBtn" href="javascript:;">生成图表</a>
                    </td>
                </tr>
            </tfoot>
			
		</table>
      
         
	</div>
	<div class="easyui-layout" data-options="fit:true">
		<div id="chart_container" style="height:650px;width:100%">

		</div>
	</div>
	<script type="text/javascript" src="../js/echarts.js"></script>
	<script type="text/javascript">

	    function excelFor() {
	        var st_no = $('#st_id_s').combobox('getText');
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        var flag = $('#reportType').combo('getValue');
	        if (flag == '1') {  //1是按小时的
	            var index = start_time.lastIndexOf(':');
	            start_time = start_time.substr(0, index) + ':00:00';
	            index = end_time.lastIndexOf(':');
	            end_time = end_time.substr(0, index) + ':00:00';
	        }
	        method = "Export";
	        $.ajax({
	            type: 'get',
	            url: '/HttpHandlers/Services1001_AddupProduct.ashx',
	            data: { st_no: st_no, StartTime: start_time, EndTime: end_time, Flag: flag,method:method },
	            dataType: 'json',
	            cache: false,
	            success: function (data) {
	                if (data == true) {
	                    $("#subs").click();
	                }
	                else {
	                    alert("导出失败");
	                }
	            }
	        });
	    }


	    var myChart;

		require.config({
			paths: {
				echarts: '/js'
			}
		});
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
	    myChart = require('echarts').init(document.getElementById('chart_container'));
		function reportTypeChanged() {
		    var now = new Date();
		    var start_time = $('#start_time').datetimebox('getValue');
		    if (start_time.length < 1) {
		        $('#start_time').datetimebox('setValue', now.getFullYear() + '/' + now.getMonth() + '/' + now.getDay() + ' ' + now.getHours() + ':' + now.getMinutes());
		        start_time = $('#start_time').datetimebox('getValue');
		    }
		    var end_time = $('#end_time').datetimebox('getValue');
		    if (end_time.length < 1) {
		        $('#end_time').datetimebox('setValue', now.getFullYear() + '/' + now.getMonth() + '/' + now.getDay() + ' ' + now.getHours() + ':' + now.getMinutes());
		        end_time = $('#end_time').datetimebox('getValue');
		    }
		    var flag = $('#reportType').combo('getValue');
		    //var flag = 2;
		    if (flag == '2') {
		        $('#end_time').datebox({ required: true });
		        $('#end_time').datebox('setValue', end_time.substr(0, 10));
		        $('#start_time').datebox({ required: true });
		        $('#start_time').datebox('setValue', start_time.substr(0, 10));
		    } else {
		        $('#start_time').datetimebox({ required: true, showSeconds: false });
		        $('#start_time').datetimebox('setValue', start_time.substr(0, 16));
		        $('#end_time').datetimebox({ required: true, showSeconds: false });
		        $('#end_time').datetimebox('setValue', end_time.substr(0, 16));
		    }
		}

		$(function () {
		    $('.topsearchBtn').first().click(function () {
		        var start_time = $('#start_time').datetimebox('getValue');
		        var end_time = $('#end_time').datetimebox('getValue');
		        if (start_time.length == 0 || end_time.length == 0) {
		            alert("时间信息必须输入");
		            return false;
		        }
		        else {
		            loadChart();
		        }
		    });
		    reportTypeChanged();
		    //所属工位下拉框数据加载  
		    reloadfl_id_s();
		    reloadst_id_s();
		   
		    loadChart();
		});   //echart

		function loadChart() {
		    //var myChart = require('echarts').init(document.getElementById('chart_container'));
			myChart.showLoading({
				effect: 'whirling'
			});
			var st_no = $('#st_id_s').combobox('getText');
			var start_time = $('#start_time').datetimebox('getValue');
			var end_time = $('#end_time').datetimebox('getValue');
			var flag = $('#reportType').combo('getValue');
			if (flag == '1') {  //1是按小时的
			    var index = start_time.lastIndexOf(':');
			    start_time = start_time.substr(0, index) + ':00:00';
			    index = end_time.lastIndexOf(':');
			    end_time = end_time.substr(0, index) + ':00:00';
			}
			$.ajax({
				type: 'get',
				url: '/HttpHandlers/Services1001_AddupProduct.ashx',
				data: { st_no: st_no, StartTime: start_time, EndTime: end_time, Flag: flag ,method:""},
				dataType: 'json',
                cache:false,
                success: function (data) {
                    //var myChart = require('echarts').init(document.getElementById('chart_container'));
					var option = {
						title: {
							text: '产量报表',
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
								    show: false,
									type: 'category',
									axisLine: {
										show: false
									},
									splitLine: {
										show: false
									},
									axisTick: false,
								    //data : ["2012年下","2013年下","2014年","2015年","2016年1季度"]
									axisLabel: {                   //增加的
                                        interval:0
									}
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
					var values = [];
					var legends = ['产量报表（' + (flag == 1 ? '小时' : '日') + '）'];
					var xAxis = [];
					var series = [{
						name: '产量报表（' + (flag == 1 ? '小时' : '日') + '）',
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
					}];
					option.series = series;
					option.legend.data = legends;
					option.xAxis[0].data = xAxis;
					//console.log(data);
					for (var index = 0; index < data.length; index++) {
						var item = data[index];
						var dayTime = item['dayTime'];
						var count = item['c'];
						if (flag == '1') {
						    //var index11 = dayTime.lastIndexOf(' ');
						    //dayTime = dayTime.substr(index11 + 1, 2);
						    dayTime = dayTime;
						}
						var times = dayTime.split("-").join("\n");
						xAxis.push(times);
						values.push(count);
					}
					//console.log(xAxis);
					//console.log(values);
					series[0].data = values;
					myChart.setOption(option, true);
					myChart.hideLoading();
				},
                complete: function () {
                    //var myChart = require('echarts').init(document.getElementById('chart_container'));
					myChart.hideLoading();
				}
			});
		}   //echart
		//function reloadfl_id_s() {
		//    $('#fl_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_fl_list');
		//}

		//function reloadst_id_s() {
		//    var fl_id = $('#fl_id_s').combobox('getValue');
		//    $('#st_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_st_listForVolume&fl_id=' + fl_id);
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
		    $('#st_id_s').combobox('loadData', {});
		    var fl_id = $('#fl_id_s').combobox('getValue');
		    $('#st_id_s').combobox({
		        url: '/HttpHandlers/TorqueReporterHandler.ashx?method=get_st_listForVolume&fl_id=' + fl_id,
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
		    $('#part_id_s').combobox('loadData', {});
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
