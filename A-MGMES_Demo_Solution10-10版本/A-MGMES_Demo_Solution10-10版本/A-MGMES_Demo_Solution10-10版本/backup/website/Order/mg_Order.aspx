<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="mg_Order.aspx.cs" Inherits="Order_mg_Order" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;生产通知单档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;生产通知单信息
                </td>
            </tr>
        </table>
        <div id="contenttop">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 500px;"></td>
                    <td style="width: 100px; text-align: right">
                        <asp:Button ID="Button1" runat="server" Text="生成生产通知单"  CssClass="addBtn" OnClick="Button1_Click"  />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="or_id" CssClass="gridview">
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("or_id") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />

            </asp:TemplateField>
            <asp:TemplateField HeaderText="订单号">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("co_no") %>'></asp:Label>
                </ItemTemplate>
                    <HeaderStyle Width="150px" />

            </asp:TemplateField>
            <asp:TemplateField HeaderText="生产单号">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("or_no") %>'></asp:Label>
                </ItemTemplate>
                    <HeaderStyle Width="150px" />

            </asp:TemplateField>
            <asp:TemplateField HeaderText="座椅代号">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("all_no") %>'></asp:Label>
                </ItemTemplate>
                    <HeaderStyle Width="150px" />

            </asp:TemplateField>
            <asp:TemplateField HeaderText="数量">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("or_count") %>'></asp:Label>
                </ItemTemplate>
                    <HeaderStyle Width="100px" />

            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>
</asp:Content>