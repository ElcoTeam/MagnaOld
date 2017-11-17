<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="timeReporter.aspx.cs" Inherits="website.Query.timeReporter" %>
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
    <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" >
    <script src="../bootstrap/css/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="top">
        <table class="table table-bordered table-striped">
          <thead>
            <tr>
              <th>#</th>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Username</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <th scope="row">1</th>
              <td>Mark</td>
              <td>Otto</td>
              <td>@mdo</td>
            </tr>
            <tr>
              <th scope="row">2</th>
              <td>Jacob</td>
              <td>Thornton</td>
              <td>@fat</td>
            </tr>
            <tr>
              <th scope="row">3</th>
              <td>Larry</td>
              <td>the Bird</td>
              <td>@twitter</td>
            </tr>
          </tbody>
        </table>
		<table cellpadding="0" cellspacing="0" style="width: 100%">

          <%--  string StartTime = request["StartTime"];
            string EndTime= request["EndTime"];
            string OrderCode = request["OrderCode"];
            string StationNo = request["StationNo"];--%>


			<tr>
				<td><span class="title">时间报表</span></td>
            <td style="width:150px;text-align: right;">订单号:</td>
                <td style="width: 150px;">
					<input id="OrderCode" /></td>
<%--				<td style="width:100px;text-align: right;">流水线:</td>
                <td style="width: 150px;">
					<select id="fl_id_s" class="easyui-combobox" style="width: 150px; height: 25px;"
						data-options="valueField: 'fl_id',textField: 'fl_name',onChange:reloadst_id_s">
					</select></td>--%>
                <td style="width:150px;text-align: right;">工位:</td>
				<td style="width: 150px;">
                    <input id="StationNo" />
				</td>
				<td style="width:150px;text-align: right;">开始时间:</td>
				<td style="width: 150px">
					<input id="start_time"/>
				</td>
				<td style="width:150px;text-align: right;">结束时间:</td>
				<td style="width: 150px">
					<input id="end_time" />
				</td>
				<td style="width:150px" hidden="hidden"  ><select id="reportType" style="display:none" class="easyui-combobox" style="width: 200px; height: 25px;"
						data-options="onChange:function(){ reportTypeChanged(); }">
						<option value="1" >按小时</option>
						<option value="2" selected >按天</option>
					</select></td>
				<td style="width: 150px;"><a class="topsearchBtn" href="javascript:;">生成图表</a></td>
<%--                <td style="width: 150px;"><a style="font-size:12px;font-weight:700;color:#000000" class="easyui-linkbutton" href="javascript:;" onclick ="excelFor()"></a></td>--%>

			</tr>
		</table>
         <input id="subs" type="submit" name="name" value="导出Excel" hidden="hidden"/>
	</div>
	<div class="easyui-layout" data-options="fit:true">
		<div id="chart_container" style="height:500px;width:100%">

		</div>
	</div>
	<script type="text/javascript" src="../js/echarts.js"></script>
	<script type="text/javascript">

	    function excelFor() {
	        //alert("----");
	        //$("#form1").submit();
	        $("#subs").click();
	        //alert("11111");
	    }



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

	        // var flag = $('#reportType').combo('getValue');
	        var flag = 2;
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
									'echarts/chart/bar',
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

	    $(function () {
	        require(
							[
									'echarts',
									'echarts/chart/bar',
							],
							function (ec) {
							    myChart = ec.init(document.getElementById('chart_container'));
							    window.onresize = myChart.resize
							}
			);
	        //搜索按钮
	        //$('.topsearchBtn').first().click(function () {
	        //    loadChart();
	        //});
	        //所属工位下拉框数据加载  
	        reloadfl_id_s();
	        reloadst_id_s();
	        reloadpart_id_s();
	        loadChart();
	    });

	    function loadChart() {

	        var OrderCode = $('#OrderCode').val();
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        var StationNo = $('#OrderCode').val();
	     
	        $.ajax({
	            type: 'get',
	            url: '/Services1004_Checks.ashx',
	            data: { StartTime: start_time, EndTime: end_time, OrderCode: OrderCode, StationNo: StationNo },
	            dataType: 'json',
	            cache: false,
	            success: function (data) {
	                
	            },
	            complete: function () {
	            }
	        });
	    }
        //在选定日期的期限内，选择订单
	    function reloadfl_id_s() {
	        $('#fl_id_s').combobox('reload', '/HttpHandlers/RepairHandler.ashx?method=get_fl_list');
	    }

	    function reloadfl_id_s() {
	        $('#fl_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_fl_list');
	    }

	    function reloadst_id_s() {
	        var fl_id = $('#fl_id_s').combobox('getValue');
	        $('#st_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler.ashx?method=get_st_list&fl_id=' + fl_id);
	    }
	</script>
</asp:Content>






        