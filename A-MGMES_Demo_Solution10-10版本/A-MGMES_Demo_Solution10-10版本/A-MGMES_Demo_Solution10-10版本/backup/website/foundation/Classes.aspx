<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Classes.aspx.cs" Inherits="foundation_Classes" ValidateRequest="false" %>
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
                    <asp:TextBox ID="txt_starttime" runat="server" Width="200px"></asp:TextBox>
                </td>

                <td style="width: 100px;">
                    &nbsp;下班时间：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_endtime" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="BtSave" runat="server" Text="新增班次"  CssClass="addBtn" 
                        onclick="BtSave_Click" />
                </td>
            </tr>
<%--            <tr>
                <td style="width: 100px;">
                    所属工位：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="drp_stname" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>

            </tr>--%>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="cl_id" CssClass="gridview" 
        onpageindexchanging="GridView1_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="班次ID"> 
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("cl_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("cl_id") %>'></asp:Label>
                </EditItemTemplate>
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
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("cl_starttime") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="下班时间">
            <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("cl_endtime") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("cl_endtime") %>'></asp:TextBox>
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
                    <asp:ImageButton ID="BtCancel" runat="server" 
                        ImageUrl="~/image/admin/undo.png"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
