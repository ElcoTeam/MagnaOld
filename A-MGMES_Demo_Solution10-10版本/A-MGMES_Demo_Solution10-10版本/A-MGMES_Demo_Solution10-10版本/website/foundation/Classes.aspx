<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Classes" ValidateRequest="false" Codebehind="Classes.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;班次档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;班次信息
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100px;">
                    班次名称：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px;">
                    &nbsp;上班时间：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_starttime" runat="server" Width="200px" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>

                <td style="width: 100px;">
                    &nbsp;下班时间：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_endtime" runat="server" Width="200px" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="BtSave" runat="server" Text="新增班次"  CssClass="addBtn" 
                        onclick="BtSave_Click" />
                </td>
               <%-- <td style="width: 100px; text-align: right">
                    <a class="toppenBtn">编辑班次</a>
                </td>--%>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="cl_id" CssClass="gridview" 
        onpageindexchanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" >
        <Columns>
            <asp:TemplateField HeaderText="班次ID"> 
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("cl_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("cl_id") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="班次名称">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("cl_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cl_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="上班时间">
            <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("cl_starttime") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <%--<asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("cl_starttime") %>'></asp:TextBox>--%>
                    <asp:TextBox ID="TextBox2" runat="server" Width="200px" Text='<%# Bind("cl_starttime") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="下班时间">
            <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("cl_endtime") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <%--<asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("cl_endtime") %>'></asp:TextBox>--%>
                    <asp:TextBox ID="TextBox3" runat="server" Width="200px" Text='<%# Bind("cl_endtime") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
<%--            <asp:TemplateField HeaderText="所属工位ID">
            <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("st_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("st_id") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="功能">
                <ItemTemplate>
                     <asp:ImageButton ID="BtEdit" runat="server" ImageUrl="~/image/admin/pen.png" 
                        onclick="BtEdit_Click"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:ImageButton ID="BtDel" runat="server" ImageUrl="~/image/admin/delete1.png" 
                        onclick="BtDel_Click"  />
                </ItemTemplate>
                 <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/image/admin/save.png" onclick="BtSave_Click1" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtCancel" runat="server"  CommandName ="Cancel"
                        ImageUrl="~/image/admin/undo.png"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

   <%-- <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="颜色编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title">
                </td>
                <td>
                    <input id="cl_id" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        班次名称：
                    </p>
                </td>
                <td>
                    <input id="cl_name" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        上班时间：
                    </p>
                </td>
                <td>
                    <asp:TextBox ID="txt_startTime" runat="server" Width="200px" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        下班时间：
                    </p>
                </td>
                <td>
                    <asp:TextBox ID="txt_endTime" runat="server" Width="200px" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <!-- 编辑窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility:hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
    <script>
        var dg = $('#GridView1');      //表格

        //编辑按钮点击
        $('.toppenBtn').first().click(function () {
            isEdit = true;
            initEidtWidget();
        });

        //编辑窗口加载
        $('#w').window({
            modal: true,
            closed: true,
            minimizable: false,
            maximizable: false,
            collapsible: false,
            width: 530,
            height: 580,
            footer: '#ft',
            top: 20,
            onBeforeClose: function () { clearw(); },
            onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
        });

        //编辑时加载窗体数据
        function initEidtWidget() {
            var selRows = dg.datagrid('getSelections');
            if (selRows.length > 1) {
                alert('每次只能编辑一条记录，请重新选取');
                return;
            } else if (selRows.length == 0) {
                alert('请选择一条记录进行编辑');
                return;
            }

            //窗体数据初始化
            $('#cl_id').val(selRows[0].cl_id);
            $('#cl_name').val(selRows[0].cl_name);
            $('#cl_starttime').val(selRows[0].cl_starttime);
            $('#cl_endtime').val(selRows[0].cl_endtime);
            $('#cl_id').hide();
            $('#w').window('open');
        }
    </script>--%>
</asp:Content>
