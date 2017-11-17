<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_mg_allpart_part_rel" ValidateRequest="false" Codebehind="mg_allpart_part_rel.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;整体座椅与大部件的关联档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;整体座椅与大部件的关联信息
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 70px;">座椅编号：&nbsp;</td>
                <td style="width: 150px;">
                    <asp:DropDownList ID="ddlAll" runat="server" Width="150px"></asp:DropDownList>
                </td>
                <td style="width:20px;"></td>
                <td style="width: 120px;">大部件名称：&nbsp;</td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="ddlPart" runat="server" Width="200px"></asp:DropDownList>
                </td>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="btAdd" runat="server" Text="新增对应"  CssClass="addBtn" OnClick="btAdd_Click"  />
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="all_id" CssClass="gridview" >
            <Columns>
                <asp:TemplateField HeaderText="座椅编号">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="150px">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("all_no") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="大部件名称">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="200px">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("part_name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="功能">
                    <ItemTemplate>
                    <asp:ImageButton ID="BtEdit" runat="server" ImageUrl="~/image/admin/pen.png"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtDel" runat="server" ImageUrl="~/image/admin/delete1.png"  />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="BtSave" runat="server" ImageUrl="~/image/admin/save.png" Height="16px"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtCancel" runat="server" 
                        ImageUrl="~/image/admin/undo.png"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
    </div>
</asp:Content>