<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="Query_FTTQuery" ValidateRequest="false" CodeBehind="FTTQuery.aspx.cs" %>

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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<input type="submit" name="name" value="导出Excel" id="sub" hidden="hidden" />--%>
    <%--<input type="button" name="name" value="导出excel" runat="server" id="s1"/>--%>
    <%--<asp:Button runat="server" Text="Button" OnClick="Button1_Click" /> --%>

    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 70%">
            <tr>
                <td class="title" style="width: 50px; " >
                    
                        班次名：
                    
                </td>             
                <td style="width: 165px;">
                    <input id="clnameid" type="text" />

                </td>
                <td class="title" style="width: 60px;" >

                       起始时间：
                    
                </td>          
                <td style="width: 120px">
                    <input id="start_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                <td class="title" style="width: 60px; " >
                    
                        结束时间：
                    
                </td>       
                <td style="width: 120px">
                    <input id="end_time" class="easyui-datetimebox" data-options="required:true,showSeconds:false" />
                </td>
                
                <td style="position:absolute;right:20px; margin-top:5px;">
                  <a class="topsearchBtn" href="javascript:;" onclick="searchName()"">查询</a>
                </td>
                
            </tr>
        </table>

    </div>
    <!-- 数据表格  -->
    <table id="tb" title="FTT列表" style="width: 99%;">
    </table>
    <script>
       


        /****************       全局变量          ***************/
        var stepid;               //要编辑的id
        var dg = $('#tb');      //表格
        //var dg1 = $('#tb1');
        var isEdit = false;     //是否为编辑状态
        var tmpNO;              //工号
        var queryParams;

       

        

        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });

            
            $('.topsearchBtn').click(function () {
                searchName();
            });
           
            //数据列表加载
      

            dg = $('#tb').datagrid({
               
                fitColumns: true,
                nowrap: false,
                striped: true,
                collapsible: false,
                url: '/HttpHandlers/Services1006_FTT.ashx',
                
                sortName: 'id',
                sortOrder: 'asc',
                columns: [[
							{ field: 'id', title: 'id', hidden: true },
							{ field: 'cl_name', title: '班次名', width: 200, align: "center",sortable:true },
							{ field: 'FTT', title: 'FTT值', width: 200, align: "center", sortable: true },
							{ field: 'cl_starttime', title: '开始时间', width: 200, align: "center", sortable: true },
							{ field: 'cl_endtime', title: '结束时间', width: 200, align: "center", sortable: true },
                ]],

                rownumbers: true,
                loadMsg: '正在加载数据...',
                toolbar: '#navigationSearch',
                pagination: true,
                pageNumber: 1,
                pageSize: 10,
                pageList: [10, 15, 20, 30],

                
            });
            //数据列表分页


            var grid = $('#tb');
            var p = grid.datagrid('getPager');
            $(p).pagination({
                beforePageText: '第',
                afterPageText: '页，共{pages}页',
                displayMsg: '当前显示从第{from}条到第{to}条 共{total}条记录',
                onBeforeRefresh: function () {

                }
            });           

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

        });

        //查询业务
        function searchName() {
            var clnameid = $('#clnameid').val();
            var start_time = $('#start_time').datetimebox('getValue');
            var end_time = $('#end_time').datetimebox('getValue');

            $('#tb').datagrid('reload', { clnameid: clnameid, start_time: start_time, end_time: end_time });

            //$.ajax({
            //    type: 'post',
            //    url: '../Services1006_FTT.ashx',
            //    async: true,
            //    data: { clnameid: clnameid, start_time: start_time, end_time: end_time },
            //    dataType: 'json',
            //    cache: false,
            //    success: function (data) {
            //        if (data = 'true') {
            //            $("#tb").datagrid('reload', {
            //                clnameid: clnameid, start_time: start_time, end_time: end_time

            //            });
            //        }
            //        else {
            //            alert('您所查询的数据并不存在！');
            //        }
            //    }
            //});

        }

        
    </script>
</asp:Content>










