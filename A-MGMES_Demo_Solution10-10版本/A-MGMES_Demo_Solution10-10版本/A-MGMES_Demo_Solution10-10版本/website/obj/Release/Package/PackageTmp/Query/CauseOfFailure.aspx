<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Operator" ValidateRequest="false" Codebehind="Operator.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <%--防止浏览器CSS缓存，JS也可用--%>
    <script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
    <%--<link href="/css/foundation.css" rel="stylesheet" type="text/css" />--%>
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>
    <style>
        .uploadify-progress { visibility: hidden; }
        .uploadify-queue { visibility: hidden; }
        #uploadify-queue { visibility: hidden; height: 0px; width: 0px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">故障原因</span>
                </td>
                <td style="width: 140px">
                    <a class="topaddBtn">新增故障原因</a>
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
    <table id="tb" title="故障原因列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="故障原因编辑">
        <table cellpadding="0" cellspacing="0">
           
            <tr>
                <td class="title">
                    <p>
                        故障原因：
                    </p>
                </td>
                <td>
                    <input id="Name" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            
            <tr>
                <td class="title">
                    <p>
                        错误代码：
                    </p>
                </td>
                <td>
                    <input id="Code" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>

            <tr style="display:none;">
                <td class="title">
                    <p>
                        ID：
                    </p>
                </td>
                <td>
                    <input id="ID" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
           
        </table>
    </div>
    <!-- 编辑窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility:hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
    <object classid="clsid:730BF2F0-EAE2-46C5-BA06-5ABFC9AB8A0A" width="0" height="0" align="center" codebase="zx_32.CAB#version=1,0,0,1" id="CZx_32Ctrl" hspace="0" vspace="0"></object>
    <script>

        /****************       全局变量          ***************/
        var opid;               //要编辑的操作员id
        var dg = $('#tb');      //表格
        var isEdit = false;     //是否为编辑状态
        var tmpNO;              //工号


        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            //新增按钮点击
            $('.topaddBtn').first().click(function () {
                isEdit = false;

                $('#ID').val('');
                $('#Name').val('');
                $('#Code').val('');

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

            //工号生成按钮
            $('.wrightBtn').first().click(function () {
                generateNO();
            });

            //保存操作工按钮
            $('#saveBtn').bind('click', function () {
                saveOperator(isEdit);
            });

            //图片上传
            $("#uploadify").uploadify({
                'swf': '/js/uploadify/uploadify.swf',
                'uploader': '/HttpHandlers/UploadfyHandler.ashx?method=uploadOperatorImg',
                'buttonText': '上传图片',
                'fileTypeDesc': 'Image Files',
                'fileTypeExts': '*.jpg; *.png',
                //'formData': { 'someKey': 'someValue', 'someOtherKey': 1 },
                //上传文件页面中，你想要用来作为文件队列的元素的id, 默认为false  自动生成,  不带#
                'queueID': 'fileQueue',
                //选择文件后自动上传
                'auto': true,
                //设置为true将允许多文件上传
                'multi': false,
                'onUploadSuccess': function (file, data, response) {
                    setImg(file, data, response);
                    //alert('The file ' + file.name + ' was successfully uploaded with a response of ' + response + ':' + data);
                }
            });

            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 530,
                height: 580,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
            //所属工位下拉框数据加载
            reloadst_id();

            //数据列表加载
            dg = $('#tb').datagrid({
                ///Services1004_Checks.ashx
                url: "/Services1005_CauseOfFailure.ashx?method=SelectCauseOfFailure",
                rownumbers: true,
                pagination: false,
                rownumbers: true,
                singleSelect: true,
                collapsible: false,
                columns: [[
                      //{ field: 'ck', checkbox: true },
                      { field: 'ID', title: '工位id', hidden: true },

                      { field: 'Name', title: '故障原因', width: 60, align: "center" },
                      { field: 'Code', title: '错误代码', width: 200, align: "center" },
                ]]
            });
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });

        });

        /****************       主要业务程序          ***************/

        //新增 / 编辑  操作工档案
        function saveOperator() {
            // Select1 name sex webChatNum tel address pCode
            //var op_id = isEdit == true ? opid : 0;
            var ID = $('#ID').val();
            var Name = $('#Name').val();
            var Code = $('#Code').val();
            //alert('abc');

            var model = {
                ID: ID,
                Name: Name,
                Code: Code,
                method: 'AddCauseOfFailure'
            };

            //keytypem1 1 ...PwdM1 FFFFFFFFFFFF ... 
            //DevOpen  RfCard RfAuthenticationKey RfWrite

              
            saveData(model);

        }
        function saveData(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/Services1005_CauseOfFailure.ashx",
                data: model,
                success: function (data) {
                    var _data = JSON.parse(data);
                    if (_data.Result == 'True') {
                        alert('成功');
                        dg.datagrid('reload');
                    } else {
                        alert('error');
                    }
                    $('#w').window('close');
                },
                error: function () {

                }
            });
        }

        //编辑时加载窗体数据
        function initEidtWidget() {
            var selRows = dg.datagrid('getSelections');
            if (selRows.length > 1) {
                alert('每次只能编辑一条记录，请重新选取');
                return;
            } else if (selRows.length == 0) {
                alert('请选择一条记录进行编辑');
                return;
            }

            //窗体数据初始化
            var row = selRows[0];

            $('#Name').val(row.Name);
            $('#Code').val(row.Code);
            $('#ID').val(row.ID);

            $('#w').window('open');
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
                url: "/Services1005_CauseOfFailure.ashx",
                data: encodeURI("ID=" + row.ID + "&method=DelectCauseOfFailure"),
                async: false,
                success: function (data) {

                    var _data = JSON.parse(data);
                    if (_data.Result == 'True') {
                        alert('成功');
                        dg.datagrid('reload');
                    } else {
                        alert('error');
                    }
                },
                error: function () {
                }
            });
        }


        /****************       辅助业务程序          ***************/
        //生成工号
        function generateNO() {
            $.ajax({
                url: "/HttpHandlers/OperatorHandler.ashx?method=generateNO",
                success: function (data) {
                    if (data != '') {
                        $('#op_no').val(data);
                    }
                },
                error: function () {
                }
            });
        }
        //上传完图片后，给编辑框中图片附链接
        function setImg(file, data, response) {
            $('#op_pic').attr('src', data);
            // $('#imgurl').val(data);
        }

        //加载工位信息
        function reloadst_id() {
            $('#st_id').combobox('reload', '/HttpHandlers/StationHandler.ashx?method=queryStationForOperatorEditing');
        }


        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            //st_id op_name op_sex op_isoperator op_pic op_no  
            $('#st_id').combobox('clear');
            $('#op_name').val('');
            $('#op_no').val('');
            $('#op_isoperator').switchbutton('uncheck');
            $('#op_sex').switchbutton('check');
            $('#op_pic').attr('src', '/image/admin/user_male.png');
        }
        /*
        支持IE 浏览器 ocx插件  的  写卡程序，
        */
        var DeviceHandleValue = -1;
        var CZx_32Ctrl = document.getElementById("CZx_32Ctrl");
        function DevOpen()//打开读卡器设备
        {
            var st = CZx_32Ctrl.OpenDevice();
            if ((st == 0 || st < 0) && CZx_32Ctrl.lErrorCode != 0) {
                alert("打开读卡器设备失败");
                return false;
            }
            else {
                DeviceHandleValue = st;
                //alert("打开设备成功");
                return true;
            }
        }
        function RfCard() {
            var ret = CZx_32Ctrl.RfCard(DeviceHandleValue, 0);
            if (CZx_32Ctrl.lErrorCode == 0) {
                //UidM1.value = CZx_32Ctrl.HexAsc(ret,4);
                //alert("寻卡成功");
                return true;
            }
            else {
                alert("请将卡片放在读卡器上面！ （寻卡失败，错误码为：" + CZx_32Ctrl.lErrorCode + "）");
                return false;
            }
        }
        function RfAuthenticationKey() {
            var ret = CZx_32Ctrl.RfAuthenticationKey(DeviceHandleValue, "1", "10", "FFFFFFFFFFFF");
            if (CZx_32Ctrl.lErrorCode == 0) {
                //alert("校验密码成功");
                return true;
            }
            else {
                alert("校验密码失败，错误码为：" + CZx_32Ctrl.lErrorCode);
                return false;
            }
        }
        function RfWrite(op_no) {
            var additionStr = "00000000000000000000000000";
            var no = additionStr + op_no;
            var ret = CZx_32Ctrl.RfWrite(DeviceHandleValue, "10", no);
            if (ret == 0) {
                alert("写数据成功");
                return true;
            }
            else {
                alert("写数据失败，错误码为：" + CZx_32Ctrl.lErrorCode);
                return false;
            }
        }
    </script>
</asp:Content>

