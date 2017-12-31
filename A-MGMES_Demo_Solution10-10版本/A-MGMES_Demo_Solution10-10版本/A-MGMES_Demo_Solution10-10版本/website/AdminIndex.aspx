<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="AdminCMS_AdminIndex" CodeBehind="AdminIndex.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        a{
            text-decoration:none;
            color:white;
         }
        .clear {
            clear: both;
        }

        .left {
            border-right: 0px solid #f0f0f0;
            float: left;
            height: 800px;
            background-color: #31353E;
            overflow: auto;
        }

            .left .logo {
                background-color: #fafafa;
                height: 20px; 
                width: 180px;
                border: none;
                border-bottom: 0px solid #ccc;
                padding: 20px 0px 20px 20px;
            }

                .left .logo h4 {
                    font-size: 16px;
                    color: #F7FFFF;
                    font-weight: bold;
                }

            .left .menu {
                border: 0px;
                padding: 20px 0px 0px 0px;
                margin: 0px;
                width: 180px;
                padding: 0px;
                background-color: #31353E;
                padding-top: 0px;
                font-size: 14px;
                letter-spacing: 1px;
                padding-left: 10px;
                padding-top: 20px;
            }

                .left .menu .first {
                    color: #74859F;
                    width: 150px;
                }

                    .left .menu .first li {
                        margin-bottom: 20px;
                        width: 150px;
                        padding: 0px;
                    }

                        .left .menu .first li span {
                            margin-left: 20px;
                        }

                    .left .menu .first .second {
                        color: #FCFAFB;
                        margin-top: 15px;
                    }

                        .left .menu .first .second li {
                            padding: 5px 5px;
                            display: block;
                            margin: 0px;
                        }

                            .left .menu .first .second li:hover {
                                background-color: #31353E;
                                cursor: pointer;
                                color: #ccc;
                            }

                            .left .menu .first .second li img {
                                height: 14px;
                                vertical-align: middle;
                            }

                            .left .menu .first .second li a {
                                line-height: 20px;
                                margin-left: 10px;
                                height: 16px;
                                vertical-align: middle;
                            }

        .right {
            float: left;
            padding: 0px;
            background-color: #fff;
            border-width: 0px;
        }

        .tabs-panels {
            border-width: 0px;
            padding: 10px 5px 0px 10px;
        }

        .tabs-wrap {
            padding-top: 15px;
        }

        .tabs {
            padding-left: 20px;
        }

            .tabs li {
                margin-right: 8px;
            }

                .tabs li a.tabs-inner {
                    padding: 0px 20px;
                    background-color: #bbb;
                }

        .tabs-title {
            font-size: 14px;
        }

        .tabs-header {
            border-left-width: 0px;
            border-right-width: 0px;
            border-top-width: 0px;
        }

        .tabs-header,
        .tabs-scroller-left,
        .tabs-scroller-right,
        .tabs-tool,
        .tabs,
        .tabs-panels,
        .tabs li a.tabs-inner {
            border-color: #3187C2;
        }

        .panel-tool {
            visibility: hidden;
        }

        .easyui-accordion .accordion {
            border-color: #31353E;
            background-color: #31353E;
        }

        .accordion {
            border-color: #31353E;
            background-color: #31353E;
        }

        .accordion-body {
            background-color: #262931;
            border-color: #262931;
            overflow: hidden;
        }

        .accordion .accordion-header {
            font-weight: normal;
            background-color: #31353E;
            border-width: 0 0 1px;
            border-color: #31353E;
            padding: 10px 0px;
            padding-left: 10px;
            color: #fff;
        }

            .accordion .accordion-header:hover {
                background-color: #262931;
            }

        .accordion .accordion-header-selected {
            background-color: #262931;
            border-width: 0 0 1px;
            border-color: #31353E;
        }

            .accordion .accordion-header-selected .panel-title {
                color: #ccc;
            }

        .panel-title {
            color: #eee;
            font-weight: normal;
        }

            .panel-title:hover {
                color: #fff;
            }

        .userarea {
            padding-left: 15px;
            padding-top: 15px;
            color: White;
        }

            .userarea h4 {
                font-size: 12px;
                margin-top: 3px;
            }

        .panel {
            margin: 5px 0px;
        }

        .panel-body .panel-body-noheader .panel-body-noborder {
            border: 0px;
        }
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
            <h4>注销：&nbsp;&nbsp;[&nbsp;<a href="javascript:void(0)" onclick="aClick()">退出系统</a>&nbsp;]
            </h4>
        </div>
        <div class="clear">
        </div>
        <div class="menu">
            <div class="easyui-accordion" style="width: 180px;" id="menu">
            </div>
        </div>
    </div>
    <div class="right">
        <div id="aaa">
            <div title="服务器信息" style="padding: 10px;">
                <%--<p style="font-size: 14px">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </p>--%>
               <%--<img src="image/logo1.png" style="height: 50%;width:50%" />--%>
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

            var _html = '';
            var currentuser = '<%= Request.Cookies["admininfo"]["userno"] %>'
            $.ajax({
                url: "Menu/GetMenuList.ashx",
                type: "post",
                dataType: "json",
                data: {
                    "menuid": "",
                    "ACTION": "usermenulist",
                    "currentuser": currentuser
                },
                success: function (data) {
                    console.log(data);
                    $.each(data, function (i, n) {
                        //显示第一个一级菜单下的二级菜单  
                        $('#menu').accordion('add', {
                            title: n.MenuName,
                            iconCls: n.Image,
                            selected: true,
                            content: '<div style="padding:10px" style="overflow: hidden; padding: 0px 10px; margin: 0px 0px 0px 0px"><ul class="first" name="' + n.MenuName + '"></ul></div>',
                        });
                        //$('#menu').accordion('select', 0)//选择第一个
                        
                    })
                   
                    $('#menu').accordion({
                        onSelect: function (title, index) {
                            
                            $('ul[name=' + title + ']').empty();
                            $.ajax({
                                type: "GET",
                                dataType: "json",
                                url: 'Menu/GetMenuList.ashx',
                                data: {
                                    "menuid": title,
                                    "ACTION": "usermenulist",
                                    "currentuser": currentuser
                                },
                                success: function (data) {
                                    var ahtml = '<ul  class="second">';
                                    $.each(data, function (i, n) {

                                        ahtml += '<li url="' + n.MenuAddr + '"><img src="' + n.Image + '" /><a>' + n.MenuName + '</a></li>';

                                    });
                                    ahtml += '</ul>';
                                              
                                    $('ul[name=' + title + ']').append(ahtml);
                                    var lis = $(' .first .second li');

                                    lis.click(function () {
                                        
                                        showTab(this);

                                    });
                                }
                            });
                        }
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    dialogMsg(errorThrown, -1);
                }
            });

        });


        function closetab(title, index) {

            tabIndexArr.splice($.inArray(title, tabIndexArr), 1);

        }



        function showTab(liobj) {


            var title = $(liobj).find('a').first().text();

            var url = $(liobj).attr('url');

            var hasShow = false;

            //if (title == "退出系统")
            //{
                
            //    window.location = "AdminLogin.aspx"
            //}
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

        function aClick() {
            window.location = "AdminLogin.aspx";
        }
        
    </script>
</asp:Content>
