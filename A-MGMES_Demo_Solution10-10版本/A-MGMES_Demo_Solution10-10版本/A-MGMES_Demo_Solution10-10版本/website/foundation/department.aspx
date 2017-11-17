<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_department" ValidateRequest="false" Codebehind="department.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;部门档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;企业职员所属部门数据
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 70px;">
                    部门名称：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="Button1" runat="server" Text="新增部门" OnClick="Button1_Click" CssClass="addBtn" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="grd_department" runat="server" AutoGenerateColumns="False" DataKeyNames="dep_id"
        CssClass="gridview" onpageindexchanging="grd_department_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="部门ID">
                <EditItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("dep_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("dep_id") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部门">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("dep_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("dep_name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能">
                <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton11" runat="server" 
                        ImageUrl="~/image/admin/save.png" onclick="ImageButton11_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/image/admin/undo.png" onclick="ImageButton2_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image/admin/pen.png"
                        OnClick="ImageButton1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton23" runat="server" ImageUrl="~/image/admin/delete1.png"
                        OnClick="ImageButton23_Click" />
                </ItemTemplate>
                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
