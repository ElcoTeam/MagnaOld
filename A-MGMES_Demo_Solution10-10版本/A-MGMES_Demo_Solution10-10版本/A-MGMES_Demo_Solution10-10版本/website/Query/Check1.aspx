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
    
        #top_table > tbody > tr > td {
            border-top:0px;
        }

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
    <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" />
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../bootstrap/jqPaginator.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top"> 
		<table cellpadding="0" cellspacing="0" style="width: 100%">
            <thead>
                <tr>
                    <td class="title">检测返修报表</td>
                </tr>
            </thead>
            <tbody>
                <tr>
				    <td>
                        <ul id="search_li">
                            <li>
                                <span>开始时间</span>
                                <div>
                                    <input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false,onChange:function(){reloadpart_id_s();}"/>
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
                                    <select id="OrderCode" class="easyui-combobox uservalue" 
						                data-options="valueField: 'OrderNo',textField: 'OrderNo'">
                                        <option value="">请选择</option>
					                </select>
                                </div>
                            </li>
                            <li>
                                <span>工位</span>
                                <div>
                                    <select id="st_id_s" class="easyui-combobox uservalue" >
                                        <option value="请选择" selected ="selected">请选择</option >
                                        <option value="FSA160">FSA160</option >
                                        <option value="FSA170">FSA170</option >
                                        <option value="FSA210" selected="selected">FSA210</option >
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
                        <input type="button"  class="topsearchBtn" value="生成图表" onclick="loadTable()"/>
                        <input type="button"  class="topexcelBtn" value="导出Excel" onclick="excelFor()" />
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
          <td align="right">报表生成时间：<span id="date_show"></span></td>
        </tr>
         
      </tbody>
    </table>
		
       <input id="subs" type="submit" name="name" value="导出Excel" hidden="hidden"/>
	</div>
    <div id ="gridTablediv">
            <table id="gridTable"  style="width: 99%;">
            </table>
        </div>
    <div id ="gridTablediv1">
            <table id="gridTable1"  style="width: 99%;">
            </table>
    </div>
	<script type="text/javascript">
	    
	    $(function () {
	        var date_t = new Date();
	        var s_t = date_t.setMonth(date_t.getMonth() - 2);
	        $("#start_time").datebox('setValue', s_t.toString());
	        $("#end_time").datebox('setValue', date_t.toString());
	        $.ajaxSetup({
	            cache: false //关闭AJAX缓存
	        });       
	    });
	    function excelFor() {
	        var OrderCode = $('#OrderCode').combobox('getValue');
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        var StationNo = $('#st_id_s').combobox('getValue');
	        if (StationNo == "请选择" || StationNo.length < 1) {
	            StationNo = "FSA210";
	        }
	        var method = "Export";
	        $.ajax({
	            type: 'post',
	            url: '/HttpHandlers/Services1004_Checks.ashx',
	            data: { StartTime: start_time, EndTime: end_time, OrderCode: OrderCode, StationNo: StationNo,method:method},
	            dataType: 'json',
	            async: false,
	            cache: false,
	            success: function (data) {
	                if (data == true) {
	                    $("#subs").click();
	                } else {
	                    console.log(data);
	                    alert("导出失败");
	                    
	                }

	            }
	        });
	    }

	    function InitTime() {
	        var date_t = new Date();
	        var s_t = date_t.setMonth(date_t.getMonth() - 1);
	        $("#start_time").datetimebox('setValue', s_t.toString());
	        $("#end_time").datetimebox('setValue', date_t.toString());
	    }
	    
	    
	    function loadTable() {

	        var method = "GetListNew";
	        var OrderCode = $('#OrderCode').combobox('getValue');
	        var StartTime = $('#start_time').datetimebox('getValue');
	        var EndTime = $('#end_time').datetimebox('getValue');
	        var StationNo = $('#st_id_s').combobox('getValue');
	        var OrderNo = OrderCode;
	        if (StartTime.length < 1 || EndTime.length < 1)
	        {
	            alert("请输入时间信息");
	            return;
	        }
	        if (StationNo == "请选择" || StationNo.length<1)
	        {
	            StationNo = "FSA210";
	        }
	        var queryParams =
                {
                    method: method,
                    OrderCode: OrderCode,
                    StartTime: StartTime,
                    EndTime: EndTime,
                    StationNo:StationNo
                }
	        if (StationNo == 'FSA210') {
	            
	            $('#gridTablediv1').hide();
	            $('#gridTablediv').show();
	           var  dg = $('#gridTable').datagrid({
	                fitColumns: true,
	                nowrap: false,
	                striped: true,
	                collapsible: false,
	                url: '/HttpHandlers/Services1004_Checks.ashx',
	                showFooter: true,
	                sortName: 'StartTime',
	                sortOrder: 'asc',
	                queryParams: queryParams,
	                emptyMsg: '<span>没有找到相关记录<span>',
	                columns: [[
                                { field: 'rowid', title: '序号', align: 'center', width: 100, },
                                {
                                    field: 'StationNo', title: '工位', align: 'center', width: 100,

                                },
                                {
                                    field: 'op_name', title: '操作工', align: "center", width: 100,

                                },
                                {
                                    field: 'ItemCaption', title: '返修内容', align: "center", width: 100,
                                   
                                },
                                {
                                    field: 'StartTime', title: '返修开始时间', align: "center", width: 100,
                                   
                                },
                                {
                                    field: 'EndTime', title: '返修结束时间', align: "center", width: 100,
                                    
                                },
                                {
                                    field: 'OrderNo', title: '订单号', align: "center", width: 100,

                                },
                                

	                ]],

	                rownumbers: false,
	                loadMsg: '正在加载数据...',
	                toolbar: '#navigationSearch',
	                pagination: true,

	                pageSize: 20,
	                pageList: [20, 30, 60, 90],
	                //loader: myLoader, //前端分页加载函数  
	                onLoadSuccess: function (data) {//表单加载完后再加载此方法
	                    // $("#gridTable").data().datagrid.cache = null;//清除datagrid 缓存，保证前台假分页；  
	                   // console.log(data);
	                    $('#date_show').html(new Date().toString("yyyy-MM-dd hh:mm:ss"));
	                    var options = $("#gridTable").datagrid("getPager").data("pagination").options;
	                    var curr = options.pageNumber; 
	                    $('#order_show').html(OrderNo);
	                    $('#page_show').html(curr);
	                    $('#line_show').html(StationNo);
	                    //sumPrice();
	                    //paint();
	                }

	            });
	        }
	        else
	        {
	            $('#gridTablediv').hide();
	            $('#gridTablediv1').show();
	          var  dg1 = $('#gridTable1').datagrid({
	                fitColumns: true,
	                nowrap: false,
	                striped: true,
	                collapsible: false,
	                url: '/HttpHandlers/Services1004_Checks.ashx',
	                showFooter: true,
	                sortName: 'CreateTime',
	                sortOrder: 'asc',
	                emptyMsg: '<span>没有找到相关记录<span>',
	                queryParams: queryParams,
	                columns: [[
                                { field: 'rowid', title: '序号', align: 'center', width: 100, },
                                {
                                    field: 'StationNo', title: '工位', align: 'center', width: 100,

                                },
                                {
                                    field: 'op_name', title: '操作工', align: "center", width: 100,

                                },
                                {
                                    field: 'ItemCaption', title: '检测内容', align: "center", width: 100,
                                    formatter: function (value, row, index) {
                                        if (row.TestValueMin < row.TestValueMax)
                                        {
                                            return row.TestCaption + "【最小值：" + row.TestValueMin + ", 最大值：" + row.TestValueMax + "】";
                                        }
                                        
                                        else {
                                            return row.TestCaption;

                                        }
                                    }

                                },
                                {
                                    field: 'TestValue', title: '真实值', align: "center", width: 100,

                                },
                                {
                                    field: 'IsQualifiedstr', title: '是否合格', align: "center", width: 100,

                                },
                                {
                                    field: 'CreateTime', title: '检测时间', align: "center", width: 100,

                                },
                                {
                                    field: 'OrderNo', title: '订单号', align: "center", width: 100,

                                },

	                ]],

	                rownumbers: false,
	                loadMsg: '正在加载数据...',
	                toolbar: '#navigationSearch',
	                pagination: true,

	                pageSize: 20,
	                pageList: [20, 30, 60, 90],
	                //loader: myLoader, //前端分页加载函数  
	                onLoadSuccess: function (data) {//表单加载完后再加载此方法
	                    // $("#gridTable").data().datagrid.cache = null;//清除datagrid 缓存，保证前台假分页；  
	                   // console.log(data);
	                    $('#date_show').html(new Date().toString("yyyy-MM-dd hh:mm:ss"));
	                    var options = $("#gridTable1").datagrid("getPager").data("pagination").options;
	                    var curr = options.pageNumber; console.log(curr);
	                    $('#order_show').html(OrderNo);
	                    $('#page_show').html(curr);
	                    $('#line_show').html(StationNo);
	                    //sumPrice();
	                    //paint();
	                }

	            });
	        }


	   
	    }

	   
	                                                                                                                       
        //根据日期显示订单号的
	    function reloadpart_id_s() {
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        if (start_time.length < 1 || end_time.length < 1) {
	            return false;
	        }
	        else {
	            $('#OrderCode').combobox('reload', '/HttpHandlers/TorqueReporterHandler1.ashx?method=get_order_list&StartTime=' + start_time + '&EndTime=' + end_time);
	        }

	    }
        //
	   



	</script>
</asp:Content>
