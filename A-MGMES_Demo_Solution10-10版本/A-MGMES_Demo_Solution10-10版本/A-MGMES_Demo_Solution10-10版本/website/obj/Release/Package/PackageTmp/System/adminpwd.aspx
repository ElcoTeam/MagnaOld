<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="AdminCMS_System_adminpwd" ValidateRequest="false" Codebehind="adminpwd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        body { font-size: 12px; color: Black; }
        
        .window-mask { background-color: #555; }
        
        .top { top: 20px; padding: 0px; border: 1px solid #CBD0D4; width: 99%; background-color: #F0F3F8; margin-bottom: 10px; }
        .top table { border: 0px; }
        .top table tr td { padding: 5px 0px; }
        .top table tr td h4 { font-size: 12px; color: #333; }
        
        
        .top .searchBtn, .addBtn, .delBtn, saveBtn, .penBtn, .saveBtn { line-height: 25px; height: 25px; padding: 3px 10px 3px 26px; border: 1px solid #ccc; color: #333; font-size: 14px; cursor: pointer; background-repeat: no-repeat; background-position: 5px 5px; background-color: #fff; }
        .top .searchBtn { background-image: url('/image/admin/Search.png'); }
        .top .searchBtn:hover { border: 1px solid #999; background-color: #eee; }
        .top .addBtn { background-image: url('/image/admin/add.png'); }
        .top .addBtn:hover { border: 1px solid #999; background-color: #eee; }
        .top .delBtn { background-image: url('/image/admin/Delete.png'); }
        .top .delBtn:hover { border: 1px solid #999; background-color: #eee; }
        .top .penBtn { background-image: url('/image/admin/pen.png'); }
        .top .penBtn:hover { border: 1px solid #999; background-color: #eee; }
        .top .saveBtn { background-image: url('/image/admin/save.png'); }
        .top .saveBtn:hover { border: 1px solid #999; background-color: #eee; }
        .top table tr td input { line-height: 22px; height: 22px; }
        
        .fixtop { top: 0px; height: 50px; padding: 0px; background-color: #F0F3F8; border: 1px solid #CBD0D4; width: 99%; }
        .fixtop table { border: 0px; }
        .fixtop table tr td { padding: 10px; }
        .fixtop table tr td h4 { font-size: 16px; color: #333; }
        
        #ft .saveBtn { background-image: url('/image/admin/save.png'); line-height: 25px; height: 25px; padding: 3px 20px 3px 36px; border: 1px solid #ccc; color: #333; font-size: 14px; cursor: pointer; background-repeat: no-repeat; background-position: 5px 5px; background-color: #fff; }
        #ft .saveBtn:hover { border: 1px solid #999; background-color: #eee; }
        #w td { padding: 10px 5px; text-align: left; vertical-align: top; }
        #w .title { vertical-align: top; text-align: right; width: 80px; background-color: #f9f9f9; }
        #w p { font-size: 14px; margin: 0px; color: #222; }
        #w .text { width: 100%; height: 25px; line-height: 25px; border: 1px solid #D4D4D4; }
        #w .text:hover { border-color: #999999; }
        
        #eft .saveBtn { background-image: url('/image/admin/save.png'); line-height: 25px; height: 25px; padding: 3px 20px 3px 36px; border: 1px solid #ccc; color: #333; font-size: 14px; cursor: pointer; background-repeat: no-repeat; background-position: 5px 5px; background-color: #fff; margin-right: 10px; }
        #eft .saveBtn:hover { border: 1px solid #999; background-color: #eee; }
        #eft .delBtn { background-image: url('/image/admin/delete1.png'); line-height: 25px; height: 25px; padding: 3px 20px 3px 36px; border: 1px solid #ccc; color: #333; font-size: 14px; cursor: pointer; background-repeat: no-repeat; background-position: 5px 5px; background-color: #fff; }
        #eft .delBtn:hover { border: 1px solid #999; background-color: #eee; }
        
        #ew td { padding: 10px 5px; text-align: left; vertical-align: top; }
        #ew .title { vertical-align: top; text-align: right; width: 80px; background-color: #f9f9f9; }
        #ew p { font-size: 14px; margin: 0px; color: #222; }
        #ew .text { width: 100%; height: 25px; line-height: 25px; border: 1px solid #D4D4D4; }
        #ew .text:hover { border-color: #999999; }
        
        .loadinggif { height: 25px; vertical-align: bottom; margin-right: 20px; display: none; }
    </style>
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>
    <script src="../js/jqeury1.7.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- 顶部工具栏  -->
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp; &nbsp;修改登录密码&nbsp; &nbsp;&nbsp; &nbsp;若后期忘记登录密码，请与管理员联系。
                </td>
            </tr>
        </table>
    </div>
   
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <table cellpadding="0" cellspacing="0">
         
        <%--<tr>
            <td class="title" style="width: 100px;">
                <p>
                    原密码：</p>
            </td>
            <td>
                <asp:TextBox ID="pwd1" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td class="title">
                <p>
                    新密码：</p>
                
            </td>
            <td>
                <input id="pwd" type="text" class="text" runat="server" style="width: 230px;" />
                 <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility: hidden">
               <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
            </td>
            
        </tr>
    </table>
    
</asp:Content>
