<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="FlowLine.aspx.cs" Inherits="foundation_FlowLine" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;流水线档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;目前有 前排坐垫（FSC）、前排靠背（FSB）、前排座椅组合（FS）、后排坐垫靠背（RSB、RSC）
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 80px;">
                    流水线名称：
                </td>
                <td style="width: 230px;">
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="BtSave" runat="server" Text="新增流水线"  CssClass="addBtn" 
                        onclick="BtSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="fl_id" CssClass="gridview" 
        onpageindexchanging="GridView1_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="流水线ID"> 
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("fl_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("fl_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="流水线名称">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("fl_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fl_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能">
                <ItemTemplate>
                    <asp:ImageButton ID="BtEdit" runat="server" ImageUrl="~/image/admin/pen.png" 
                        onclick="BtEdit_Click"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtDel" runat="server" ImageUrl="~/image/admin/delete1.png" 
                        onclick="BtDel_Click"  />
                </ItemTemplate>

                <EditItemTemplate>
                    <asp:ImageButton ID="BtSave" runat="server" 
                        ImageUrl="~/image/admin/save.png" onclick="BtSave_Click1" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtCancel" runat="server" 
                        ImageUrl="~/image/admin/undo.png" OnClick="BtCancel_Click"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>