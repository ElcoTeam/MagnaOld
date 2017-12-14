<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="website.Query.warningReporter" ValidateRequest="false" CodeBehind="warningReporter.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<%--<link href="/css/foundation.css" rel="stylesheet" type="text/css" />--%>
	<%--<script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>--%>
	<script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
	<%--<script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>--%>
	<%--<script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>--%>
	<%--<link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>--%>

	<style>
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


		td.err-OK {
            text-align:center;
			height:80px;
			/*background:#4e976a;*/
		}
        /*超时*/
		td.err-1 {
            text-align:center;
			height:80px;
			background:#f9f9f9;   /*黄色*/
		}
        /*生产                   ***************/
		td.err-2 {
            text-align:center;
			height:80px;
			background:#995AB6;   /*紫色*/
		}
        /*维修*/
		td.err-3 {
            text-align:center;
			height:80px;
			background:#606CC5; /*蓝色*/
		}
        /*质量*/
		td.err-4 {
            text-align:center;
			height:80px;
			background:#FFC42E;
		}
        /*物料*/   /*灰色*/
		td.err-5 {
            text-align:center;
			height:80px;
			background:#9FB6CD;
		}
        /*急停*/
		td.err-6 {    
            text-align:center;
			height:80px;
			background:#FF0012;  /*红色*/
		}
	    #tb tbody td {
            text-align:center;
            font-size:38px;
	    }
	</style>
            <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" >
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../bootstrap/jqPaginator.js"></script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<div class="top">
		<table cellpadding="0" cellspacing="0" style="width: 100%">
           
			<tr>

				<td><span class="title">报警信息查询</span></td>
                <td style="width:150px;text-align: right;">开始时间:</td>
				<td style="width: 150px">
					<input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
				</td>
                <td style="width:150px;text-align: right;">结束时间:</td>
				<td style="width: 150px">
					<input id="end_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
				</td>
				
				<td style="width: 150px;"><a class="topsearchBtn" id="q1">按时间进行查询</a></td>
              <td>  
<%--                  <asp:Button runat="server" Text="导出excel" OnClick="Button1_Click" />--%>
                  <input id="subs" type="submit"  value="导出Excel" hidden="hidden"/>
                  <a style="font-size:12px;font-weight:700;color:#000000" class="easyui-linkbutton btn btn-default" href="javascript:;" onclick ="excelFor()">导出Excel</a>
              </td>
			</tr>
		</table>
	</div>
	<!-- 数据表格  -->
	<%--<table id="tb" title="工位列表" style="width: 99%;">
		<tbody></tbody>
	</table>--%>
    <table id="tb" title="工位列表" style="width: 99%;" border="1">
        <tr>
            <td>

            </td>
        </tr>
        <tbody id="tds"></tbody>
	</table>

	<script>

	    /****************       全局变量          ***************/
	    var stepid;               //要编辑的id
	    var dg;      //表格

	    /****************       DOM加载          ***************/
	    $(function () {
	        $('#start_time').datetimebox('setValue', Date.now.toString());
	        $('#end_time').datetimebox('setValue', Date.now.toString());
	        dg = $('#tb');
	        $.ajaxSetup({
	            cache: false //关闭AJAX缓存
	        });

	        $('.topsearch').click(function () {
	            loadInfo();
	        });
	        loadInfo();
	    });

	    function excelFor() {
	        console.log(Date.now());
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        var index = start_time.lastIndexOf(':');
	        start_time = start_time.substr(0, index) + ':00:00';
	        index = end_time.lastIndexOf(':');
	        end_time = end_time.substr(0, index) + ':00:00';
	        $.ajax({
	            type: 'post',
	            url: '/HttpHandlers/Services1002_WaringList.ashx',
	            async:false,
	            cache: false,
	            dataType: 'json',
	            data: { "StartTime": "" + start_time + "", "EndTime": "" + end_time + "", "method": "Export" },
	            cache: false,
	            success: function (data) {
	                if (data == true) {
	                    console.log()
	                    $("#subs").click();
	                }
	                else {
	                    alert("导出失败");
	                }
	            }
	        });
	    }
	    $('#q1').click(function () {
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        var index = start_time.lastIndexOf(':');
	        start_time = start_time.substr(0, index) + ':00:00';
	        index = end_time.lastIndexOf(':');
	        end_time = end_time.substr(0, index) + ':00:00';
	        $.ajax({
	            type: 'post',
	            url: '/HttpHandlers/Services1002_WaringList.ashx',
	            cache: false,
	            async: false,
	            dataType: 'json',
	            data: { "StartTime": "" + start_time + "", "EndTime": "" + end_time + "", "method": "" },
	            success: function (list) {
	                console.log(list);
	                var tbody = dg.children('tbody').get(0);
	                tbody.innerHTML = '';
	                var counter = 0;
	                var tr = "<tr>";
	                var str = "";
	                for (var i = 0 ; i < list.length; i++) {
	                    if (counter == 0) {
	                        str = "";
	                    }
	                    switch (list[i].AlarmType) {
	                        case 0:
	                            str += "<td class='err-OK'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 1:
	                            str += "<td class='err-1'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 2:
	                            str += "<td class='err-2'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 3:
	                            str += "<td class='err-3'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 4:
	                            str += "<td class='err-4'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 5:
	                            str += "<td class='err-5'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 6:
	                            str += "<td class='err-6'>" + list[i].AlarmStation + "</td>";
	                            break;
	                    }
	                    counter++;
	                    if (counter == 5) {
	                        tr += str + "</tr>";
	                        //alert(tr)
	                        dg.append(tr);
	                        tr = "<tr>";
	                        counter = 0;
	                    }
	                }
	            }
	        });
	    });

	    function loadInfo() {
	        var start_time = $('#start_time').datetimebox('getValue');
	        var end_time = $('#end_time').datetimebox('getValue');
	        var index = start_time.lastIndexOf(':');
	        start_time = start_time.substr(0, index) + ':00:00';
	        index = end_time.lastIndexOf(':');
	        end_time = end_time.substr(0, index) + ':00:00';
	        method = "";
	        $.ajax({
	            type: 'post',
	            url: '/HttpHandlers/Services1002_WaringList.ashx',
	            cache: false,
	            async: false,
	            dataType: 'json',
	            data: { "StartTime": "" + start_time + "", "EndTime": "" + end_time + "", "method": "" },
	            success: function (list) {
	                console.log(list);
	                var tbody = dg.children('tbody').get(0);
	                tbody.innerHTML = '';
	                var counter = 0;
	                var tr = "<tr>";
	                var str = "";
	                for (var i = 0 ; i < list.length; i++) {
	                    if (counter == 0) {
	                        str = "";
	                    }
	                    switch (list[i].AlarmType) {
	                        case 0:
	                            str += "<td class='err-OK'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 1:
	                            str += "<td class='err-1'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 2:
	                            str += "<td class='err-2'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 3:
	                            str += "<td class='err-3'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 4:
	                            str += "<td class='err-4'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 5:
	                            str += "<td class='err-5'>" + list[i].AlarmStation + "</td>";
	                            break;
	                        case 6:
	                            str += "<td class='err-6'>" + list[i].AlarmStation + "</td>";
	                            break;
	                    }
	                    counter++;
	                    if (counter == 5) {
	                        tr += str + "</tr>";
	                        //alert(tr)
	                        dg.append(tr);
	                        tr = "<tr>";
	                        counter = 0;
	                    }
	                }
	            }
	        });
	    }
    </script>
</asp:Content>