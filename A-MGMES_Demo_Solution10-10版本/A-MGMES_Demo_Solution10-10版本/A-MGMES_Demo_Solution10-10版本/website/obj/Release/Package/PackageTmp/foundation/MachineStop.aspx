<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_MachineStop" ValidateRequest="false" Codebehind="MachineStop.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 100px;
            height: 40px;
        }
        .auto-style2 {
            width: 200px;
            height: 40px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;停机记录 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;设备停机信息
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0" style="background-color: aliceblue">
            <tr><td colspan="6" style="color:red">提示：填写表格后点击新增记录即可完成新增</td></tr>
            <tr>
                <td class="auto-style1">
                    工站：&nbsp;
                </td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddl_st_no" Width="200px" runat="server" Height="30px"></asp:DropDownList>
                </td>               
                <td class="auto-style1">
                    &nbsp;停机开始时间：&nbsp;
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txt_starttime" runat="server" Width="200px" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" height="23px"></asp:TextBox>
                </td> 
                <td style="width: 100px;">
                    &nbsp;停机结束时间：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_endtime" runat="server" Width="200px" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" height="23px"></asp:TextBox>
                </td> 
            </tr>
            <tr>
                <td class="auto-style1">
                    停机原因：&nbsp;
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txt_reason" runat="server" Width="200px" height="23px"></asp:TextBox>
                </td>
                
                <td style="width: 100px;">
                    备注：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_memo" runat="server" Width="200px" height="23px"></asp:TextBox>
                </td>
                <td style="text-align: right">
                         <asp:Button ID="btnSearch" runat="server" Text="查询"  CssClass="searchBtn" 
                        onclick="BtnSearch_Click" Font-Size="Small" Font-Bold="True" Font-Underline="False" height="26px" />  
                </td>
                <td  style="text-align: left">
                    
                    
                    <asp:Button ID="BtSave" runat="server" Text="新增记录"  CssClass="addBtn" 
                        onclick="BtSave_Click" Font-Size="Small" Font-Bold="True" Font-Underline="False" height="26px" /> 
                   
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="machineStop_id" CssClass="gridview" 
        onpageindexchanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound" >
        <Columns>
            <asp:BoundField DataField="orderNo" HeaderText="序号" ReadOnly="True">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="工站"> 
                <EditItemTemplate>
                    <asp:DropDownList ID="gv_st_no" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lab_st_no" runat="server" Text='<%# Bind("st_no") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="停机原因">
                <ItemTemplate>
                    <asp:Label ID="lab_reason" runat="server" Text='<%# Bind("machineStop_reason") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_reason" runat="server" Text='<%# Bind("machineStop_reason") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="停机开始时间">
            <ItemTemplate>
                    <asp:Label ID="lab_starttime" runat="server" Text='<%# Bind("start_time") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <%--<asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("cl_starttime") %>'></asp:TextBox>--%>
                    <asp:TextBox ID="txt_starttime" runat="server" Width="200px" Text='<%# Bind("start_time") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="停机结束时间">
            <ItemTemplate>
                    <asp:Label ID="lab_end_time" runat="server" Text='<%# Bind("end_time") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <%--<asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("cl_endtime") %>'></asp:TextBox>--%>
                    <asp:TextBox ID="txt_endtime" runat="server" Width="200px" Text='<%# Bind("end_time") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
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
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:Label ID="lab_memo" runat="server" Text='<%# Bind("memo") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_memo" runat="server" Text='<%# Bind("memo") %>'></asp:TextBox>
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
            <asp:TemplateField HeaderText="停机ID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lab_id" runat="server" Text='<%# Bind("machineStop_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lab_id" runat="server" Text='<%# Bind("machineStop_id") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
