<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="AndonTaglines.aspx.cs" Inherits="website.foundation.AndonTaglines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top">
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <span class="title">口号宣传维护</span>
                    <span class="subDesc">可对相应位置的宣传口号进行编辑维护。</span>
                </td>
                <td style="width: 150px">
                    <a class="toppenBtn">编辑宣传口号</a>
                </td>
            </tr>
        </table>
    </div>

    <!-- 数据内容-->
    <table id="tb" title="宣传口号列表" style="width: 100%"></table>

    <!--编辑窗口-->
    <div id="w" style="padding: 10px; visibility: hidden;" title="口号内容编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title" style="width: 150px;">宣传口号位置:
                </td>
                <td>
                    <input id="EditTaglinesType" type="text" class="text" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 150px;">宣传口号内容:</td>
                <td>
                    <input id="EditTaglinesText" type="text" class="text" style="width: 400px;" />
                </td>
            </tr>
        </table>
    </div>

    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility: hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" />
        <a class="saveBtn" id="saveBtn">保存</a>
    </div>


    <script>

        //全局变量
        var taglinesId;     //要编辑的ID
        var dg;             //数据表格内容



        $(function () {
            $.ajaxSetup({
                cache: false//关闭AJAX缓存
            });


            //查询列表
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/AndonTaglinesHandler.ashx?method=QueryTaglinesList",
                rownumbers: true,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                columns: [[
                    { field: 'ID', title: 'ID', width: 20, align: "center" },
                    { field: 'TaglinesType', title: '宣传口号位置', width: 100, align: "center" },
                    { field: 'TaglinesText',width: 100, title: '宣传口号内容' }

                ]]
            });

            //编辑按钮点击事件
            $('.toppenBtn').first().click(function () {
                DoEditTaglines();
            });

            $('#saveBtn').bind('click', function () {
                DoSaveTaglines();
            });

            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collabsible: false,
                width: 600,
                height: 300,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () {
                    clearw();
                },
                onBeforeOpen: function () {
                    $('#w').css('visibility', 'visible');
                    $('#ft').css('visibility', 'visible');
                }
            });
            //编辑窗口关闭清空数据
            function clearw() {
                $('#EditTaglinesType').val('');
                $('#EditTaglinesText').val('');

                //collapseMenuTree();
            }
        });

        //编辑按钮点击事件
        function DoEditTaglines() {
            var selectRow = dg.datagrid('getSelections');
            if (selectRow.length > 1) {
                alert("每次只能编辑一条记录，请重新选择。");
                return;
            }
            else if (selectRow.length == 0) {
                alert('请选择一条记录进行编辑');
                return;
            }

            var row = selectRow[0];
            taglinesId = row.ID;

            $('#EditTaglinesType').val(row.TaglinesType);
            $('#EditTaglinesText').val(row.TaglinesText);
            $('#w').window('open');
        }

        function DoSaveTaglines() {
            var taglinesType = $('#EditTaglinesType').val();
            var taglinesText = $('#EditTaglinesText').val();

            var model = {
                taglinesId: taglinesId,
                taglinesType: taglinesType,
                taglinesText: taglinesText,
                method: 'SaveTaglines'
            };
            SaveTaglines(model);
        }

        function SaveTaglines(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/AndonTaglinesHandler.ashx",
                data: model,
                success: function (data) {
                    if (data == 'True') {
                        alert('已保存。');
                        dg.datagrid('reload');
                    } else {
                        alert('保存失败');
                    }
                    $('#w').window('close');
                }
            });
        }
    </script>

</asp:Content>
