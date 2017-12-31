<%@ Page Language="C#" AutoEventWireup="true" Inherits="AdminCMS_AdminLogin" Codebehind="AdminLogin.aspx.cs" %>

<!DOCTYPE>
<html>
<head runat="server">
    <title></title>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <style>
       /* body { padding: 0px; margikn: 0px; background-image: urlk(/image/bgimg.jpg); background-repeat: no-repeat; background-size: 100% 100%; font-family: 微软雅黑; } */
        body 
        { 
            padding: 0px;  
            margin: 0px;   
            font-family: 微软雅黑; 
            background-image: url(/image/magna.jpg); 
            background-repeat: no-repeat; 
            background-size: 100% 100%;
            height:920px;

        }
        .nav { margin: 0px;   background-color:White; padding:20px 0px; border-bottom:2px solid #C8C8C8; width:500px; }
        .top 
        { 
            position: absolute;
            top: 50%;
            left: 50%;
            margin-top: -218px;
            margin-left: -250px;
        }
        
        .loginTable { border: 0px solid #ccc; width: 500px; background-color:#f9f9f9;}
        
        .loginTable .lab { width: 20%; text-align: center; }
        .loginTable .lab img { height: 30px; }
        
        .loginTable .title { vertical-align: bottom; border: none; text-align: left; padding-left: 20px; padding-top: 30px; color:#ccc }
        .loginTable .title h4 { font-size: 22px;  }
        .loginTable .sub { background-color: #93B823; color: White; border: none; width: 90%; height: 40px; line-height: 40px; font-size: 1.1em; cursor: pointer; }
        .loginTable .sub:hover { background-color: #CDE77E; }
        
        .loginTable tr td { padding: 20px 10px; height: 70px; }
        
        .loginTable tr td input { background-color: #fff; border: 1px solid #ccc; }
        
        .loginTable .text { width: 90%; height: 40px; line-height: 40px; font-size: 16px; padding-left: 10px; color: #666; }
        
        .footer { height: 10%; bottom: 0px; position: fixed; width: 100%; }
    </style>
    
</head>
<body >
    <form id="form1" runat="server">
   

    <div class="top">
        <center>
            <div class="nav">
        <img src="image/logo.jpg"  style=" height:50px;"/><a>
    </div>
            <table class="loginTable" cellpadding="0" cellspacing="0">
              
                <tr>
                    <td colspan="2" style="padding: 0px; height: 60px;">
                    </td>
                </tr>
                <tr>
                    <td class="lab">
                        <img src="/image/user.png" />
                    </td>
                    <td>
                        <asp:TextBox ID="name" runat="server" CssClass="loginTable text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lab">
                        <img src="/image/lock.png" />
                    </td>
                    <td>
                        <asp:TextBox ID="pwd" runat="server" CssClass="loginTable text" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align: center; height: 120px; text-align: left">
                        <asp:Button ID="Button1" runat="server" Text="登录" CssClass="sub" 
                            onclick="Button1_Click" />
                    </td>
                </tr>
                  <tr>
                    <th colspan="2" class="loginTable title" style="  font-size:8px; padding-bottom:5px; height:30px; text-align:center; padding-top:10px;">
                        
                            麦格纳MES管理系统&nbsp;&nbsp;V1.0&nbsp;&nbsp;&nbsp;&nbsp;2016-6-6测试版本&nbsp;&nbsp;MES dep, &nbsp;Elco ltd,.
                    </th>
                </tr>
            </table>
        </center>
    </div>
    <div class="footer">
    </div>
    </form>
</body>
</html>
