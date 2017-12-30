<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="EditPsw.aspx.cs" Inherits="website.foundation.EditPsw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/register.js"></script>
    <script>
         var UserID="";
         $(function () {
            
             UserID = '<%= Request.Cookies["admininfo"]["name"] %>';
             $("#adminNo").val(UserID);
             $("#save").click(function () {
                 AcceptClick();
             })
         });
            
         //保存表单
         function AcceptClick() {

             var OldPsw = $("#oldpassword").val();
             var NewPsw = $("#password").val();
             var ReNewPaw = $('#rePassword').val();
             if (NewPsw != ReNewPaw)
             {
                 alert("两次密码不一致");
                 return false;
             }
             //if (!verifyCheck._click()) return;
             if (!verifyCheck._click()) return false;

             $.ajax({
                 url: "/HttpHandlers/EditPassword.ashx",
                 data: {
                     Action: "EditPsw",
                     UserID: UserID,
                     OldPsw: OldPsw,
                     NewPsw: NewPsw
                 },
                 async: true,
                 type: "post",
                 datatype: "json",
                 success: function (data) {
                    
                     data = JSON.parse(data);
                     if (data.result == "success") {
                        alert("保存成功");
                         
                     }
                     else if (data.result == "failed") {
                         alert(data.msg);
                     }
                 }
                 
             });
         }
    </script>
   
    <div class="reg-box" id="verifyCheck" style="margin-left: 10px; margin-top: 20px; margin-right: 30px;">
            <div class="part1">                	
                   <div class="item col-xs-12" style="float: none;">
                       <span class="intelligent-label f-fl"><b class="ftx04">*</b>用户名：</span>    
                       <div class="f-fl item-ifo">
                           <input type="text" maxlength="20" class="txt03 f-r3 required"tabindex="1"  id="adminNo" data-valid="isNonEmpty" data-error="用户名不能为空" readonly/>                        
                           <span class="ie8 icon-close close hide"></span>
                           <label class="icon-sucessfill blank hide"></label>
                           <label class="focus"><span>当前账号</span></label>
                           <label class="focus valid"></label>
                       </div>
                   </div>
                      <div class="item col-xs-12" style="float: none;">
                       <span class="intelligent-label f-fl"><b class="ftx04">*</b>旧密码：</span>    
                       <div class="f-fl item-ifo">
                           <input type="password" id="oldpassword" maxlength="20" class="txt03 f-r3required" tabindex="3" style="ime-mode:disabled;" onpaste="return  false"autocomplete="off" data-valid="isNonEmpty" data-error="密码不能为空" />
                           <span class="ie8 icon-close close hide" style="right:55px"></span>
                           <span class="showpwd" data-eye="password"></span>                        
                           <label class="icon-sucessfill blank hide"></label>
                           <label class="focus">请输入原密码</label>
                           <label class="focus valid"></label>
                           <span class="clearfix"></span>
                       </div>
                   </div>

                   <div class="item col-xs-12" style="float: none;">
                       <span class="intelligent-label f-fl"><b class="ftx04">*</b>新密码：</span>    
                       <div class="f-fl item-ifo">
                           <input type="password" id="password" maxlength="20" class="txt03 f-r3required" tabindex="3" style="ime-mode:disabled;" onpaste="return  false"autocomplete="off" data-valid="isNonEmpty||between:1-20||level:2"  /> 
                           <span class="ie8 icon-close close hide" style="right:55px"></span>
                           <span class="showpwd" data-eye="password"></span>                        
                           <label class="icon-sucessfill blank hide"></label>
                           <label class="focus">请输入新密码</label>
                           <label class="focus valid"></label>
                           <span class="clearfix"></span>
                           <%--<label class="strength">
                           	<span class="f-fl f-size12">安全程度：</span>
                           	<b><i>弱</i><i>中</i><i>强</i></b>
                           </label>    --%>
                       </div>
                   </div>
                   <div class="item col-xs-12" style="float: none;">
                       <span class="intelligent-label f-fl"><b class="ftx04">*</b>确认密码：</span>   
                       <div class="f-fl item-ifo">
                           <input type="password" maxlength="20" class="txt03 f-r3 required"tabindex="4" style="ime-mode:disabled;" onpaste="return  false"autocomplete="off" data-valid="isNonEmpty||between:1-20||isRepeat:password" id="rePassword" />
                           <span class="ie8 icon-close close hide" style="right:55px"></span>
                           <span class="showpwd" data-eye="rePassword"></span>
                           <label class="icon-sucessfill blank hide"></label>
                           <label class="focus">请再输入一遍上面的密码</label> 
                           <label class="focus valid"></label>                          
                       </div>
                   </div>
                  <div>
                      <input id="save" type="button" value="保存"/>
                  </div>
           </div>
      </div>
    <style>
        .item col-xs-12{
            width:100%;
        }
    </style>
</asp:Content>