<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Position"  ValidateRequest="false" Codebehind="Position.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;职位档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;企业职员职位信息
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 70px;">
                    职位名称：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="Button1" runat="server" Text="新增职位"  CssClass="addBtn" 
                        onclick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="grd_position" runat="server" AutoGenerateColumns="False" DataKeyNames="posi_id"
        CssClass="gridview" onpageindexchanging="grd_position_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="职位ID"> 
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("posi_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("posi_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="职位名称">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("posi_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("posi_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能">
                <ItemTemplate>
                    <asp:ImageButton ID="BtEdit" runat="server" ImageUrl="~/image/admin/pen.png" 
                        onclick="BtEdit_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtDel" runat="server" ImageUrl="~/image/admin/delete1.png" 
                        onclick="BtDel_Click" />
                </ItemTemplate>

                <EditItemTemplate>
                    <asp:ImageButton ID="BtSave" runat="server" 
                        ImageUrl="~/image/admin/save.png" onclick="BtSave_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtCancel" runat="server" 
                        ImageUrl="~/image/admin/undo.png" onclick="BtCancel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

