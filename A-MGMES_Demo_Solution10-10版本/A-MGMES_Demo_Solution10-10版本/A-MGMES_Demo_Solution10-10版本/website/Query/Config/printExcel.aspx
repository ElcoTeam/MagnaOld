<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printExcel.aspx.cs" Inherits="website.Query.Config.printExcel" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">  
<head>  
    <title>数据打印</title>  
      
    <style type="text/css">  
    body{background:white;margin:0px;padding:0px;font-size:13px;text-align:left;}  
.pb{font-size:13px;border-collapse:collapse;}  
.pb th{font-weight:bold;text-align:center;border:1px solid #333333;padding:2px;}  
.pb td{border:1px solid #333333;padding:2px;}  
</style>  
</head>  
<body>  
    <div id ="print">
     </div>
    <script type="text/javascript">
       
        window.print();
    </script>  
</body>  
</html>  
