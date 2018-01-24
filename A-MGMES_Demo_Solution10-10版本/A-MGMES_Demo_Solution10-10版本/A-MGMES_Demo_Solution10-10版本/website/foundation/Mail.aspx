<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Mail" ValidateRequest="false" CodeBehind="Mail.aspx.cs"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <style>
        #w td { padding: 5px 5px; text-align: left; vertical-align: middle; }
        #w .title { vertical-align: middle; text-align: right; width: 80px; background-color: #f9f9f9; }
        p {padding:5px }
    </style>

    <link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>
    <script src="/js/validate.js" type="text/javascript"></script>
    <script src="../js/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../js/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0"  style="width: 100%">
            <tr>
                <td><span class="title">邮件档案</span> 
                </td>
                <td style="width: 120px">
                    <a class="topaddBtn">新增档案</a>
                </td>
                <td style="width: 120px">
                    <a class="toppenBtn">编辑所选</a>
                </td>
                <td style="width: 120px">
                    <a class="topdelBtn">删除所选</a>
                </td>
            </tr>
        </table>
    </div>
    <!-- 数据表格  -->
    <table id="tb" title="邮件列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding-left: 24px;padding-top:35px; visibility: hidden" title="邮件编辑">
        <table cellpadding="0" cellspacing="0" table-layout:fixed; id="table1">

            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        收件人类型：
                    </p>
                </td>
                <td style="padding-left: 10px;">
                     <select id="com_ReceiptType" class="easyui-combobox" style="width: 230px; height: 25px;"
                        data-options="required:true">
                        <%--<option value="1">LineUp</option>
                        <option value="2">Delgit</option>
                        <option value="3">回冲</option>--%>
                         	<option value="1">LineUpTxt加载失败</option>
			                <option value="2">LineUp订单的ProductNo在MES系统中不匹配或为空</option>
			                <option value="3">DelJetTxt加载失败</option>
			                <option value="4">DelJet订单的在LineUp订单中不匹配或对应的ProductNo不匹配</option>
			                <option value="5">DelJet订单自动拆单失败</option>
			                <option value="6">SAP手动插单Txt加载失败</option>
			                <option value="7">SAP手动插单订单自动拆单失败</option>
			                <option value="8">SAP手动插单订单的ProductNo在MES系统中不匹配或为空</option>
			                <option value="9">DelJet订单的SEQNR不连续</option>
			                <option value="10">DelJet缓存文件夹写入失败</option>
                    </select>
                </td>
               
            </tr>
             <tr>
                <td class="title" style="width: 150px;">
                    <p>
                        是否收件人抄送人：
                    </p>
                </td>
                <td style="padding-left: 10px;">
                   <select id="com_RecipientType" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="required:true">
                       <option value="1">收件人</option>
                       <option value="2">抄送人</option>
                    </select>
                </td>
            </tr>
            <tr id="t">
                <td class="title" style="width: 110px;">
                    <p>
                        收件人邮箱：
                    </p>
                </td>
                <td id="main">
                   <input  id="MailRecipient0" name="MailRecipient" type="text" class="easyui-validatebox" data-options="required:true,validType:['email']"  style="width: 230px; height: 30px;margin-left:5px;"/>
                </td>
                <td  id="childadd">
                    <a id="add" class="addbtn" style="border:0; cursor:pointer;"><img src="../image/添加.png"/></a>
                </td>
            </tr>
        </table>
    </div>
    <!-- 编辑窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility: hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
    <script>

        /****************       全局变量          ***************/
        var mailid;               //要编辑的id
        var dg;      //数据表格
        //var partdg;      //部件座椅表格
        var isEdit = false;     //是否为编辑状态
       
              


        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            //新增按钮点击
            $('.topaddBtn').first().click(function () {
                isEdit = false;
                $('#childadd').remove();
                $('#main').remove();
                $('#t').append(' <td id="main"><input  id="MailRecipient0" name="MailRecipient" type="text" class="easyui-validatebox" data-options="required:true,validType:[\'email\']"  style="width: 230px; height: 30px;margin-left:5px;"/></td><td id="childadd" style="position:absolute;right:168px;top:140px;"><a id="add" class="addbtn" onclick="addmail()" style="border:0; cursor:pointer;"><img src="../image/添加.png"/></a></td>');
                $('#com_ReceiptType').combobox('setValue', 1);
                $('#com_RecipientType').combobox('setValue', 1);
                $('#w').window('open');
               
            });

            //编辑按钮点击
            $('.toppenBtn').first().click(function () {
                isEdit = true;
                initEidtWidget();
            });

            //删除按钮
            $('.topdelBtn').first().click(function () {
                deleteInfos();
            });

            //保存按钮
            $('#saveBtn').bind('click', function () {
                saveMail(isEdit);
            });
            //编辑窗口中的添加按钮
            $('#add').click(function () {
                addmail();
                
            });

            //下拉框数据加载
            $('#com_ReceiptType').combobox('setValue', 1);
            $('#com_RecipientType').combobox('setValue', 1);
          
            //编辑窗口加载
           
            ('input[type!="hidden"],select,textarea', $("#formId")).each(function () {
                $(this).validatebox();
            });
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 650,
                height: 550,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
                
            });
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/MailHandler.ashx?method=QueryMailList",           //改动的地方
                rownumbers: true,
                pagination: true,
                pageSize: 20,
                singleSelect: true,
                collapsible: false,
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
                      { field: 'mail_id', title: 'ID', align: "center", width: 50, hidden: "true" },
                      { field: 'ReceiptTypeName', title: '收件人类别', align: "left", width: 530 },
                      { field: 'MailRecipient', title: '收件人邮箱', align: "center", width: 330 },
                      { field: 'RecipientTypeName', title: '是否收件人', align: "center", width: 330 },
                      { field: 'ReceiptType', title: 'ReceiptType', align: "center", width: 330, hidden: "true" },
                      { field: 'RecipientType', title: 'RecipientType', align: "center", width: 330, hidden: "true" },
                ]]
            });
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });
        });

        /****************       主要业务程序          ***************/

        //新增 / 编辑  
        function saveMail() {
            //                        
            
            
            var mail_id = isEdit == true ? mailid : 0;
            var ReceiptType = $('#com_ReceiptType').combo('getValue');
            var RecipientType = $('#com_RecipientType').combo('getValue');
            var count1 = $("#main input[name=MailRecipient]").length;
            var MailName = new Array(count1);
           
            if (count1> 0) {

                for (var i = 0; i < count1; i++) {
                    if ($("#MailRecipient"+ i).val() != null && $("#MailRecipient" + i).val() != "") {
                        MailName[i] = $("#MailRecipient" + i).val();
                        
                    }

                }
                        
            }
            var Mail = MailName.join(',');
            
            
            

            var model = {
                mail_id: mail_id,
                ReceiptType: ReceiptType,
                RecipientType: RecipientType,
                Mail: Mail,
                method: 'saveMail'
            };
            var ispass = $("#w").form('validate');
           
            if (ispass != true)
            {
                return;
            }
            else if (ispass == true)
            {
                    
              saveTheMail(model);
                           
            }
        }
        function saveTheMail(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/MailHandler.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                        dg.datagrid('reload');
                    }
                    else alert('保存失败');
                    $('#w').window('close');
                },
                error: function () {
                }
            });
        }

        //编辑时加载窗体数据
        function initEidtWidget() {
            $('#childadd').remove();
            $('#main').remove();
            $('#t').append('<td id="main"><input  id="MailRecipient0" name="MailRecipient" type="text" class="easyui-validatebox" data-options="required:true,validType:[\'email\']"  style="width: 230px; height: 30px;margin-left:5px;"/></td>');
            var selRows = dg.datagrid('getSelections');
            if (selRows.length > 1) {
                alert('每次只能编辑一条记录，请重新选取');
                return;
            } else if (selRows.length == 0) {
                alert('请选择一条记录进行编辑');
                return;
            }
            var row = selRows[0];
            mailid = row.mail_id;
            $('#com_ReceiptType').combobox('setValue', row.ReceiptType);
            $('#com_RecipientType').combobox('setValue', row.RecipientType);
            $('#MailRecipient0').val(row.MailRecipient);
           
   
            $('#w').window('open');
          
        }
        function addmail()
        {
           
            //获取已有的添加框长度
            var count1 = $("#main input[name=MailRecipient]").length;
            var count2 = 0;
            var count3 = 0;
            //循环每个输入框
            $("#main input[name=MailRecipient]").each(function (index, item) {
                count2++;
                //如果相等那么是最后一个输入框
                if (count2 == count1) {
                    //count3是最后一个输入框的id的后面的数字
                    count3 = this.id.split("MailRecipient")[1];
                }
            });
            var count4 = parseInt(count3) + 1;
            $("#main").append('<tr id="tr' + count2 + '"><td><input type="text" id="MailRecipient' + count4 + '" name="MailRecipient" class="easyui-validatebox" data-options="required:true,validType:[\'email\']" style="height:30px;width:230px;margin-top:2px""/></td><td style="padding-top:8px;padding-right:50px"><a class="easyui-linkbutton" style="cursor:pointer;" onclick="delrow(\'tr' + count2 + '\')" id="del-text+count2+"><img src="/image/删除.png"></a></td></tr>');
        }
        function delrow(id) {
            $("#" + id + "").remove();
            
            var count1 = $("#main input[name=MailRecipient]").length;
            var count2 = 0;
            var count3 = 0;
            //循环每个输入框
            $("#main input[name=MailRecipient]").each(function (index, item) {
                count2++;
                //如果相等那么是最后一个输入框
                if (count2 == count1) {
                    //count3是最后一个输入框的id的后面的数字
                    count3 = this.id.split("MailRecipient")[1];
                }
            });
            
        }
       
        
        function deleteInfos() {
            var selRows = dg.datagrid('getSelections');
            if (selRows.length > 1) {
                alert('每次只能删除一条记录，请重新选取');
                return;
            } else if (selRows.length == 0) {
                alert('请选择一条记录进行删除');
                return;
            }
            var row = selRows[0];
            $.ajax({
                url: "/HttpHandlers/MailHandler.ashx",
                data: encodeURI("mail_id=" + row.mail_id + "&method=DeleteMail"),
                async: false,
                success: function (data) {
                    if (data == 'true') {
                        alert('已删除');
                        dg.datagrid('reload');
                    }
                    else alert('删除失败');
                },
                error: function () {
                }
            });
        }


        /****************       辅助业务程序          ***************/
        
        //加载下拉信息
        //function reloadcom_testgroupid() {
           
        //    $('#com_testgroupid').combobox('reload', '/HttpHandlers/MailGroupHandler.ashx?method=queryGroupidForMail')
        //    $("#com_testgroupid").combobox("setValue", "ECU")
           
        //}
        

        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            //             
            $('#com_ReceiptType').combo('clear');
            $('#com_RecipientType').combo('clear');
            var count1 = $("#main input[name=MailRecipient]").length;
            var MailName = new Array(count1);
            if (count1 > 0) {
                for (var i = 0; i < count1; i++) {
                    if ($("#MailRecipient" + i).val() != null && $("#MailRecipient" + i).val() != "") {
                        MailName[i] = $("#MailRecipient" + i).val('');

                    }

                }

            }
            //$('#childadd').remove();
            //$('#main').remove();
        
            //for (var i = 1; i < count1; i++) {

            //    $("#Mail" + i).remove();

            //}
           
        }
    </script>
</asp:Content>
