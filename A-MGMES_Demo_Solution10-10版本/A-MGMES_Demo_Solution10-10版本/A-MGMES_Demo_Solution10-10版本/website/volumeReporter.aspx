<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="volumeReporter.aspx.cs" Inherits="website.Query.volumeReporter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
	<link href="/js/uploadify/uploadify.css" rel="stylesheet" />
	<script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>
	<script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>
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
	<div class="top">
		<table cellpadding="0" cellspacing="0" style="width: 100%">
			<tr>
				<td><span class="title">产量报表</span></td>
				<td></td>
				<td style="width:150px;text-align: right;">开始时间</td>
				<td style="width: 150px">
					<input id="start_time"/>
				</td>
				<td style="width:150px;text-align: right;">结束时间</td>
				<td style="width: 150px">
					<input id="end_time" />
				</td>
				<td style="width:150px"><select id="reportType" class="easyui-combobox" style="width: 200px; height: 25px;"
						data-options="onChange:function(){ reportTypeChanged(); }">
						<option value="1">按小时</option>
						<option value="2" selected>按天</option>
					</select></td>
				<td style="width: 150px;"><a class="topsearchBtn" href="javascript:;">生成图表</a></td>
			</tr>
		</table>
	</div>
	<div class="easyui-layout" data-options="fit:true">
		<div id="chart_container" style="height:100%;width:100%">

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

		function reportTypeChanged() {
		    var now = new Date();
		    var parent = $('#start_time').parent().get(0);
		    parent.innerHTML = '';
		    var start_time = document.createElement('input');
		    start_time.id = 'start_time';
		    parent.appendChild(start_time);
				
		    parent = $('#end_time').parent().get(0);
		    parent.innerHTML = '';
		    var end_time = document.createElement('input');
		    end_time.id = 'end_time';
		    parent.appendChild(end_time);

		    var flag = $('#reportType').combo('getValue');
		    if (flag == '2') {
		        $(end_time).datebox({ required: true });
		        $(end_time).datebox('setValue', now.getFullYear() + '/' + now.getMonth() + '/' + now.getDay());
		        $(start_time).datebox({ required: true });
		        $(start_time).datebox('setValue', now.getFullYear() + '/' + now.getMonth() + '/' + now.getDay());
		    } else {
		        $(start_time).datetimebox({ required: true, showSeconds: false });
		        $(start_time).datetimebox('setValue', now.getFullYear() + '/' + now.getMonth() + '/' + now.getDay() + ' ' + now.getHours() + ':' + now.getMinutes());
		        $(end_time).datetimebox({ required: true, showSeconds: false });
		        $(end_time).datetimebox('setValue', now.getFullYear() + '/' + now.getMonth() + '/' + now.getDay() + ' ' + now.getHours() + ':' + now.getMinutes());
		    }
		}

		$(function () {
			require(
							[
									'echarts',
									'echarts/chart/line',
							],
							function (ec) {
								myChart = ec.init(document.getElementById('chart_container'));
								window.onresize = myChart.resize
								loadChart();
							}
			);
			//搜索按钮
			$('.topsearchBtn').first().click(function () {
				loadChart();
			});
			reportTypeChanged();
		});

		function loadChart() {

			myChart.showLoading({
				effect: 'whirling'
			});
			var start_time = $('#start_time').datetimebox('getValue');
			var end_time = $('#end_time').datetimebox('getValue');
			var flag = $('#reportType').combo('getValue');
			if (flag == '1') {
			    var index = start_time.lastIndexOf(':');
			    start_time = start_time.substr(0, index) + ':00:00';
			    index = end_time.lastIndexOf(':');
			    end_time = end_time.substr(0, index) + ':00:00';
			}
			$.ajax({
				type: 'get',
				url: '/Services1001_AddupProduct.ashx',
				data: { StartTime: start_time, EndTime: end_time, Flag: flag },
				dataType: 'json',
                cache:false,
				success: function (data) {
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
									type: 'category',
									axisLine: {
										show: false
									},
									splitLine: {
										show: false
									},
									axisTick: false,
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
					for (var index = 0; index < data.length; index++) {
						var item = data[index];
						var time = item['time'];
						var count = item['c'];
						if (flag == '1') {
							var index = time.lastIndexOf(' ');
							time = time.substr(index + 1, 2);
						}
						xAxis.push(time);
						values.push(count);
					}
					series[0].data = values;
					myChart.setOption(option, true);
					myChart.hideLoading();
				},
				complete: function () {
					myChart.hideLoading();
				}
			});
		}
	</script>
</asp:Content>
