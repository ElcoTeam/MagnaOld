<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="Check1.aspx.cs" Inherits="website.Query.Check1" %>
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
	    .calendar-day {
	        height:auto!important;
        }
	</style>
    <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" >
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../bootstrap/jqPaginator.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top">

        
    <style>
        #top_table > tbody > tr > td {
            border-top:0px;
        }
    </style>

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
                    <td>扭矩/角度信息分析</td>
                </tr>
            </thead>
            <tbody>
                <tr>
				    <td>
                        <ul id="search_li">
                            <li>
                                <span>开始时间</span>
                                <div>
                                    <input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false"/>
                                </div>
                            </li>
                            <li>
                                <span>结束时间</span>
                                <div>
                                    <input id="end_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false,onChange:function(){reloadpart_id_s();}" />
                                </div>
                            </li>
                            <li>
                                <span>订单号</span>
                                <div>
                                    <select id="OrderCode" class="easyui-combobox" style="width: 200px; height: 25px;"
						                data-options="valueField: 'OrderNo',textField: 'OrderNo'">
                                        <option value="">请选择</option>
					                </select>
                                </div>
                            </li>
                            <li>
                                <span>工位</span>
                                <div>
                                    <select id="st_id_s" class="easyui-combobox" style="width: 150px; height: 25px;">
                                        <option value="请选择" selected ="selected">请选择</option >
                                        <option value="FSA160">FSA160</option >
                                        <option value="FSA170">FSA170</option >
                                        <option value="FSA210">FSA210</option >
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
                        <a class="topsearchBtn" href="javascript:;">生成图表</a>
                        <asp:Button runat="server"  style="height:36px;" class="btn btn-default" Text="导出excel" OnClick="Button1_Click" />
                    </td>
                </tr>
            </tfoot>
			
		</table>
        
        <table class="table" id="top_table" style="margin-top:-24px;">
      <caption><h1>数据跟踪</h1></caption>
      <tbody>
        <tr>
          <td width="100">页：</td>
          <td><span id="page_show"> </span></td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td align="right" style="position:relative;">&nbsp;
              <img src="../image/logo1.png" style="width:354px;position:absolute;top:-49px;right:1px;"  />
          </td>
        </tr>
          <tr>
          <td>订单号：</td>
          <td><span id="order_show"> </span></td>
          <td></td>
          <td></td>
          <td></td>
          <td align="right"></td>
        </tr>
          <tr>
          <td>生产线：</td>
          <td><span id="line_show"> </span></td>
          <td></td>
          <td></td>
          <td></td>
          <td align="right">报表生成时间：<span id="date_show">2017-05-01 05:16:50</span></td>
        </tr>
         
      </tbody>
    </table>
		
         <input id="subs" type="submit" name="name" value="导出Excel" hidden="hidden"/>
	</div>
    <div class="top">
    <table class="table table-bordered table-striped">
          <thead>
            <tr>
              <th>序号</th>
              <th>工位</th>
              <th>操作工</th>
              <th><span class="neirong_1">内容</span></th>
              <th class="is_show"><span class="neirong_4">真实值</span></th>
              <th><span class="neirong_2">是否合格</span></th>
              <th><span class="neirong_3">检测时间</span></th>
            </tr>
          </thead>
          <tbody id="data_box">
            
          </tbody>
           <tfoot>
               <tr>
              <th colspan="7" id="page_">
                  <p id="p2"></p>
                    <ul class="pagination" id="pagination2"></ul>
              </th>
            </tr>
           </tfoot>
        </table>
    </div>
	<div class="easyui-layout" data-options="fit:true">
		<div id="chart_container" style="height:500px;width:100%">

		</div>
	</div>
	<script type="text/javascript" src="../js/echarts.js"></script>
    <script type="text/javascript">
    
</script>
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
	        
	        $('#start_time').datetimebox({ required: true });
	        $('#start_time').datetimebox('setValue', now.getFullYear() + '/' + (now.getMonth()+1) + '/' + (now.getDate()-1));
	        $('#end_time').datetimebox({ required: true });
	        $('#end_time').datetimebox('setValue', now.getFullYear() + '/' + (now.getMonth() + 1) + '/' + now.getDate());
	    }

	    $(function () {
	        //搜索按钮
	        $('.topsearchBtn').first().click(function () {
	            
	            loadChart();
	            var _number = $('.topsearchBtn').attr('data-number');
	            var _page = $('.topsearchBtn').attr('data-page');
	            if (!_page) {
	                _page = 1;
	            }
	            fn_init_page(_number, _page);
	        });

	        reportTypeChanged();

	        //$('#end_time').datetimebox({
	        //    stopFirstChangeEvent: true,
	        //    onChange: function () {
	        //        console.log(1);
	        //        reloadpart_id_s();
	        //    }
	        //});
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
	        //reloadfl_id_s();
	        //reloadst_id_s();
	        reloadpart_id_s();
	        
	        loadChart();

	        

	        var _totalPages = $('.topsearchBtn').attr('data-number');
	        

	        
	    });
	    
	    function loadChart() {

        
	        var OrderCode = $('#OrderCode').combo('getValue');
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        var page = $('.topsearchBtn').attr('data-page');
	        if (page) {
	            page = page;
	        } else {
	            page = 1;
	        }
	        var StationNo = $('#_easyui_textbox_input2').val();



	     
	        $.ajax({
	            type: 'get',
	            url: '/Services1004_Checks.ashx',
	            data: { StartTime: start_time, EndTime: end_time, OrderCode: OrderCode, StationNo: StationNo, page: page },
	            dataType: 'json',
	            async: false,
	            cache: false,
	            success: function (json) {


	                //console.log(json);

	                $('#data_box').html('');
	                var data = json.data;
	                var number = json.number;
	                var date = json.date;
	                var OrderNo = json.OrderNo;
	                var yee = json.yee;
	                var StationNo = json.StationNo;

	                var neirong = json.neirong;

	                $('.topsearchBtn').attr('data-number',number);

	                $('#date_show').html(date);
	                $('#order_show').html(OrderNo);
	                $('#page_show').html(yee);
	                $('#line_show').html(StationNo);
	                
	                
	                if (neirong == '检测') {
	                    $('.is_show').show();
	                    $('.neirong_1').html('检测内容');
	                    $('.neirong_4').html('真实值');
	                    $('.neirong_2').html('是否合格');
	                    $('.neirong_3').html('检测时间');
	                } else {
	                    $('.is_show').hide();
	                    $('.neirong_1').html('返修内容');
	                    $('.neirong_2').html('返修开始时间');
	                    $('.neirong_3').html('返修结束时间');
	                }

	                //console.log(number);

	                //-------------------------------------------------------------

	                var _str = "";
	                if (data.length > 0) {
	                    //console.log(data);
	                    $.each(data, function (key,value) {
	                        _str += "<tr>";
	                        _str += "<th scope='row'>" + (key + 1) + "</th>";
	                        





	                        if (neirong == '检测') {
	                            _str += "<td>" + value.stationno + "</td>";
	                        } else {
	                            _str += "<td>" + value.StationNo1 + "</td>";
	                        }

	                        _str += "<td>" + value.op_name + "</td>";

	                        if (neirong == '检测') {  //检测的了
	                            var Cts = value.TestCaption;
	                            if (Cts.indexOf("气囊电阻") >= 0) {
	                                _str += "<td>" + value.TestCaption + "【最小值：1.4，最大值：2.6】" + "</td>";
	                                _str += "<td>" + value.TestValue + "</td>";
	                                _str += "<td>" + value.IsQualified + "</td>";
	                                _str += "<td>" + value.CreateTime + "</td>";
	                            }
	                            else if (Cts.indexOf("SBR电阻") >= 0) {
	                                _str += "<td>" + value.TestCaption + "【最小值：0，最大值：400】" + "</td>";
	                                _str += "<td>" + value.TestValue + "</td>";
	                                _str += "<td>" + value.IsQualified + "</td>";
	                                _str += "<td>" + value.CreateTime + "</td>";
	                            }
	                            else if (Cts.indexOf("插入电流") >= 0) {
	                                _str += "<td>" + value.TestCaption + "【最小值：12，最大值：17】" + "</td>";
	                                _str += "<td>" + value.TestValue + "</td>";
	                                _str += "<td>" + value.IsQualified + "</td>";
	                                _str += "<td>" + value.CreateTime + "</td>";
	                            }
	                            else if (Cts.indexOf("拔出电流") >= 0) {
	                                _str += "<td>" + value.TestCaption + "【最小值：5，最大值：6.9】" + "</td>";
	                                _str += "<td>" + value.TestValue + "</td>";
	                                _str += "<td>" + value.IsQualified + "</td>";
	                                _str += "<td>" + value.CreateTime + "</td>";
	                            }
	                            else {
	                                _str += "<td>" + value.TestCaption + "</td>";
	                                _str += "<td></td>";
	                                _str += "<td>" + value.IsQualified + "</td>";
	                                _str += "<td>" + value.CreateTime + "</td>";
	                            }                            
	                        } else {    //返修的了
	                            _str += "<td>" + value.ItemCaption + "</td>";
	                            //_str += "<td></td>";
	                            _str += "<td>" + value.starttime + "</td>";
	                            _str += "<td>" + value.endtime + "</td>";
	                            
	                        }
	                        
	                        _str += "</tr>";
	                    });

	                    $('#data_box').html(_str);
	                }
	               // console.log(number);
	                var _str_2 = "";
	                if (number > 0) {
	                        _str_2 +='<nav aria-label="Page navigation">';
	                        _str_2 +='<ul class="pagination">';
	                        _str_2 +='<li>';
	                        _str_2 +='<a href="#" aria-label="Previous">';
	                        _str_2 +='<span aria-hidden="true">&laquo;</span>';
	                        _str_2 +='</a>';
	                        _str_2 += '</li>';
	                     for (var i = 1; i <= number; i++) {
	                         _str_2 += '<li><a href="javascript:alert(1);" class="page_btn">' + i + '</a></li>';
	                     }
	                        _str_2 +='<li>';
	                        _str_2 +='<a href="#" aria-label="Next">';
	                        _str_2 +='<span aria-hidden="true">&raquo;</span>';
	                        _str_2 +='</a>';
	                        _str_2 +='</li>';
	                        _str_2 +='</ul>';
	                        _str_2 += '</nav>';

	                        //$('#page_').html(_str_2);
	                }
	               // console.log(_str_2);

                    
	            },
	            complete: function () {
	            }
	        });
	    }
	    //function reloadfl_id_s() {
	    //    $('#fl_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandler1.ashx?method=get_order_list');
	    //}

	                                                                                                                       
        //根据日期显示订单号的
	    function reloadpart_id_s() {
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        $('#OrderCode').combobox('reload', '/HttpHandlers/TorqueReporterHandler1.ashx?method=get_order_list&StartTime=' + start_time + '&EndTime=' + end_time);
	        //$('#part_id_s').combobox('reload', '/HttpHandlers/TorqueReporterHandl

	    }
        //
	    function fn_init_page(number, page) {
	        
	        $.jqPaginator('#pagination2', {
	            totalPages: parseInt(number),
	            visiblePages: 10,
	            currentPage: parseInt(page),
	            prev: '<li class="prev"><a href="javascript:;">Previous</a></li>',
	            next: '<li class="next"><a href="javascript:;">Next</a></li>',
	            page: '<li class="page"><a href="javascript:loadChart();" class="page_btn">{{page}}</a></li>',
	            onPageChange: function (num, type) {
	                $('#p2').text(type + '：' + num);
	                
	                $('.topsearchBtn').attr('data-page', num);
	            }
	        });
	    }
	    
	</script>
</asp:Content>
