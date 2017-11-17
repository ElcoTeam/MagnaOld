<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="AdminIndex.aspx.cs" Inherits="AdminCMS_AdminIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .clear { clear: both; }

        .left { border-right: 0px solid #f0f0f0; float: left; height: 800px; background-color: #31353E; overflow: auto; }
        .left .logo { background-color: #fafafa; height: 20px; width: 180px; border: none; border-bottom: 0px solid #ccc; padding: 20px 0px 20px 20px; }
        .left .logo h4 { font-size: 16px; color: #F7FFFF; font-weight: bold; }

        .left .menu { border: 0px; padding: 20px 0px 0px 0px; margin: 0px; width: 180px; padding: 0px; background-color: #31353E; padding-top: 0px; font-size: 14px; letter-spacing: 1px; padding-left: 10px; padding-top: 20px; }
        .left .menu .first { color: #74859F; width: 150px; }
        .left .menu .first li { margin-bottom: 20px; width: 150px; padding: 0px; }
        .left .menu .first li span { margin-left: 20px; }
        .left .menu .first .second { color: #FCFAFB; margin-top: 15px; }
        .left .menu .first .second li { padding: 5px 5px; display: block; margin: 0px; }
        .left .menu .first .second li:hover { background-color: #31353E; cursor: pointer; color: #ccc; }

        .left .menu .first .second li img { height: 14px; vertical-align: middle; }
        .left .menu .first .second li a { line-height: 20px; margin-left: 10px; height: 16px; vertical-align: middle; }

        .right { float: left; padding: 0px; background-color: #fff; border-width: 0px; }
        .tabs-panels { border-width: 0px; padding: 10px 5px 0px 10px; }
        .tabs-wrap { padding-top: 15px; }
        .tabs { padding-left: 20px; }
        .tabs li { margin-right: 8px; }
        .tabs li a.tabs-inner { padding: 0px 20px; background-color: #bbb; }
        .tabs-title { font-size: 14px; }
        .tabs-header { border-left-width: 0px; border-right-width: 0px; border-top-width: 0px; }

        .tabs-header,
        .tabs-scroller-left,
        .tabs-scroller-right,
        .tabs-tool,
        .tabs,
        .tabs-panels,
        .tabs li a.tabs-inner { border-color: #3187C2; }

        .panel-tool { visibility: hidden; }
        .easyui-accordion .accordion { border-color: #31353E; background-color: #31353E; }
        .accordion { border-color: #31353E; background-color: #31353E; }
        .accordion-body { background-color: #262931; border-color: #262931; overflow: hidden; }
        .accordion .accordion-header { font-weight: normal; background-color: #31353E; border-width: 0 0 1px; border-color: #31353E; padding: 10px 0px; padding-left: 10px; color: #fff; }
        .accordion .accordion-header:hover { background-color: #262931; }
        .accordion .accordion-header-selected { background-color: #262931; border-width: 0 0 1px; border-color: #31353E; }
        .accordion .accordion-header-selected .panel-title { color: #ccc; }

        .panel-title { color: #eee; font-weight: normal; }
        .panel-title:hover { color: #fff; }
        .userarea { padding-left: 15px; padding-top: 15px; color: White; }
        .userarea h4 { font-size: 12px; margin-top: 3px; }
        .panel { margin: 5px 0px; }

        .panel-body .panel-body-noheader .panel-body-noborder { border: 0px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="left">
        <div class="logo">
            <%--   <h4>
                微信营销管理系统</h4>--%>
            <img src="image/logo1.png" style="height: 30px;" />
        </div>
        <div class="userarea">
            <h4>用户：&nbsp;&nbsp;[&nbsp;<asp:Literal ID="namelit" runat="server"></asp:Literal>&nbsp;]
            </h4>
            <h4>职位：&nbsp;&nbsp;[&nbsp;<asp:Literal ID="tellit" runat="server"></asp:Literal>&nbsp;]
            </h4>
        </div>
        <div class="clear">
        </div>
        <div class="menu">
            <div class="easyui-accordion" style="width: 180px;">
                <div title="行政人事档案" data-options="iconCls:'folder_edit_16px'" style="overflow: hidden; padding: 0px 10px; margin: 0px 0px 0px 0px"
                    id="m1" runat="server">
                    <ul class="first">
                        <li>
                            <ul class="second">
                                <li url='../foundation/department.aspx' id="m1_1" runat="server">
                                    <img src="/image/admin/1.png" /><a>部门档案</a></li>
                                <li url='../foundation/Position.aspx' id="m1_2" runat="server">
                                    <img src="/image/admin/2.png" /><a>职位档案</a></li>
                                <%--<li url='../foundation/User.aspx' id="li18" runat="server">
                                    <img src="/image/admin/4.png" /><a>用户档案</a></li>--%>
                                <li url='../foundation/Operator.aspx' id="m1_3" runat="server">
                                    <img src="/image/admin/5.png" /><a>操作工档案</a></li>
                                <li url='../foundation/User.aspx' id="m1_4" runat="server">
                                    <img src="/image/admin/3.png" /><a>用户权限管理</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div title="部件零件档案" data-options="iconCls:'puzzle_15'" style="overflow: hidden; padding: 0px 10px; margin: 0px 0px 0px 0px"
                    id="m2" runat="server">
                    <ul class="first">
                        <li>
                            <ul class="second">
                                <li id="m2_1" runat="server" url='../foundation/allpart.aspx'>
                                    <img src="/image/admin/1.png" /><a>ALL&nbsp;整车座椅</a></li>
                                <%--<li id="li8" runat="server" url='#'>
                                    <img src="/image/admin/2.png" /><a>部分座椅配置</a></li>--%>
                                <%--<li id="li9" runat="server" url='../foundation/Part.aspx'>
                                    <img src="/image/admin/3.png" /><a>部分座椅配置</a></li>--%>
                                <li id="m2_2" runat="server" url='../foundation/Part.aspx'>
                                    <img src="/image/admin/3.png" /><a>POA&nbsp;部件档案</a></li>
                                <li id="m2_3" runat="server" url='../foundation/Bom.aspx'>
                                    <img src="/image/admin/4.png" /><a>BOM&nbsp;零件档案</a></li>
                            </ul>
                        </li>
                </div>
                <div title="生产线档案" data-options="iconCls:'computer_17'" style="overflow: hidden; padding: 0px 10px; margin: 0px 0px 0px 0px" id="m3" runat="server">
                    <ul class="first">
                        <li>
                            <ul class="second">
                                <li id="m3_1" runat="server" url='../foundation/FlowLine.aspx'>
                                    <img src="/image/admin/1.png" /><a>流水线档案</a></li>
                                <li id="m3_2" runat="server" url='../foundation/Station.aspx'>
                                    <img src="/image/admin/2.png" /><a>工位档案</a></li>
                                <li id="m3_3" runat="server" url='../foundation/Step.aspx'>
                                    <img src="/image/admin/3.png" /><a>工序步骤管理</a></li>
                                <%--<li id="m3_4" runat="server" url='../foundation/Classes.aspx'>
                                    <img src="/image/admin/4.png" /><a>班次档案</a></li>--%>
                            </ul>
                        </li>
                </div>
                <div title="订单管理" data-options="iconCls:'list_12'" style="overflow: hidden; padding: 0px 10px; margin: 0px 0px 0px 0px" id="m4" runat="server">
                    <ul class="first">
                        <li>
                            <ul class="second">
                                <li id="m4_1" runat="server" url='../Order/CustomerOrder.aspx'>
                                    <img src="/image/admin/1.png" /><a>销售订单</a></li>
                                <li id="m4_2" runat="server" url='../Order/mg_Order.aspx'>
                                    <img src="/image/admin/2.png" /><a>生产通知单</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div title="查询统计" data-options="iconCls:'icon-search'" style="overflow: hidden; padding: 0px 10px; margin: 0px 0px 0px 0px" id="m5" runat="server">
                    <ul class="first">
                        <li>
                            <ul class="second">
                                <li id="m5_1" runat="server" url='../Query/StepQuery.aspx'>
                                    <img src="/image/admin/1.png" /><a>工序步骤日志查询</a></li>
                                <li id="m5_2" runat="server" url='#'>
                                    <img src="/image/admin/2.png" /><a>客户订单统计</a></li>
                                <li id="m5_3" runat="server" url='#'>
                                    <img src="/image/admin/3.png" /><a>生产通知单统计</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div title="注销" data-options="iconCls:'dialog_shutdown_16px'" style="overflow: hidden; padding: 0px 10px; margin: 0px 0px 0px 0px">
                    <ul class="first">
                        <li>
                            <ul class="second">
                                <li id="li3" runat="server" url='System/adminpwd.aspx'>
                                    <img src="/image/admin/1.png" /><a>修改密码</a></li>
                                <li url=''>
                                    <img src="/image/admin/banner.png" /><a><asp:Button ID="Button1" runat="server" Text="退出系统"
                                        OnClick="Button1_Click" BorderWidth="0" BackColor="#31353e" ForeColor="#ffffff"
                                        Font-Names="微软雅黑" Font-Size="14px" /></a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

        </div>
    </div>
    <div class="right">
        <div id="aaa">
            <div title="服务器信息" style="padding: 10px;">
                <p style="font-size: 14px">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </p>
            </div>
        </div>
    </div>
    <script>

        var iframe = '<iframe src="$$$"  style="width:100%;border:0px;height:99%;border:0px" scrolling="auto" seamless="seamless" frameborder=0 border=0> ';

        var leftHeight = 0;

        var leftWidth = 0;

        var rightHeight = 0;

        var rightWidth = 0;

        var heightClip = 10;

        var widthClip = 20;

        var tabIndexArr = new Array();


        $(function () {


            var leftDiv = $('.left').first();

            var rightDiv = $('.right').first();

            var tabDiv = $('.easyui-tabs').first();


            var height = $(window).height();

            var width = $(window).width();


            leftDiv.height(height);

            rightDiv.height(height - heightClip);

            rightDiv.width(width - leftDiv.width() - widthClip);


            $('#aaa').tabs({

                width: rightDiv.width(),

                height: rightDiv.height(),

                tabHeight: 44,

                onClose: function (title, index) { closetab(title, index); }

            });


            $(window).resize(function () {

                resize();

            });


            var lis = $(' .first .second li');

            lis.click(function () {

                showTab(this);

            });

        });


        function closetab(title, index) {

            tabIndexArr.splice($.inArray(title, tabIndexArr), 1);

        }



        function showTab(liobj) {


            var title = $(liobj).find('a').first().text();

            var url = $(liobj).attr('url');

            var hasShow = false;


            if (url != null && url != '') {

                if (tabIndexArr.length > 0) {


                    $(tabIndexArr).each(function () {


                        if (this == title) {

                            $('#aaa').tabs('select', title);

                            hasShow = true;

                            return false;

                        }

                    });


                    if (!hasShow) {

                        addTabPanel(title, url);

                    }

                } else {

                    addTabPanel(title, url);

                }

            }

        }


        function addTabPanel(title, url) {

            $('#aaa').tabs('add', {

                title: title,

                closable: true

            });

            var panel = $('#aaa').tabs('getTab', title);

            var newiframe = iframe.replace('$$$', url);

            $(panel).html(newiframe);

            tabIndexArr.push(title);

        }


        function resize() {

            var leftDiv = $('.left').first();

            var rightDiv = $('.right').first();

            var tabDiv = $('.easyui-tabs').first();


            var height = $(window).height();

            var width = $(window).width();


            leftDiv.height(height);

            rightDiv.height(height - widthClip);

            rightDiv.width(width - leftDiv.width() - heightClip);


            $('#aaa').tabs({

                width: rightDiv.width(),

                height: rightDiv.height()

            });


            $('#aaa').tabs('resize');

        }

    </script>
</asp:Content>
