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
                <td><span class="title">操作工档案</span> <span class="subDesc"><%--工位机上的操作人员，不可彻底删除，离职或专职后只能冻结该用户。另：--%>员工号需系统自动生成</span>
                </td>
                <td style="width: 120px">
                    <a class="topaddBtn">新增员工</a>
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
    <table id="tb" title="操作人员列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="操作工编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        所属工位：
                    </p>
                </td>
                <td>
                     <!-- 修改时间：2017年5月9日 -->
                     <!-- 修改人：冉守旭 -->
                     <!-- 修改内容：combobox实现多选功能，增加multiple:true属性-->
                    <select id="st_id" class="easyui-combobox" name="st_id" style="width: 230px; height: 25px;"
                        data-options="
                        valueField: 'st_id',
                        textField: 'st_name',
                        multiple:true
                        ">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        姓名：
                    </p>
                </td>
                <td>
                    <input id="op_name" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>

            <tr>
                <td class="title">
                    <p>
                        性别：
                    </p>
                </td>
                <td>
                    <input id="op_sex" class="easyui-switchbutton" data-options="onText:'男',offText:'女',checked:true"/>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        工位管理员：
                    </p>
                </td>
                <td>
                    <input id="op_isoperator" class="easyui-switchbutton" data-options="onText:'是',offText:'否'"/>
                </td>
            </tr>
            <tr>
                <td class="title">员工照片：
                </td>
                <td>&nbsp;&nbsp;<img id="op_pic" src="/image/admin/user_male.png" style="height: 80px;"><br />
                    <br />
                    <input type="file" name="uploadify" id="uploadify" /><%--<a class="uploadBtn">上传头像</a>--%>
                    <div id="uploadify-queue" style="height: 0px; width: 0px"></div>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        工号：
                    </p>
                </td>
                <td>
                    <input id="op_no" type="text" class="text" style="width: 100px;" />&nbsp;&nbsp;&nbsp;&nbsp;<a class="wrightBtn">生成工号</a>
                </td>
            </tr>

            <tr>
                <td class="title">
                    <p>
                        &nbsp;
                    </p>
                </td>
                <td>
                    <font style="color: red">在生成工号前，请确保读卡器连接正常，且卡片放置在读卡器上面！<br/><br/>若编辑状态下更改工号，则必须进行写卡，不更改工号则不用写卡！</font>
                </td>
            </tr>
        </table>
    </div>
    <!-- 编辑窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility:hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
    <div id="dlg" class="easyui-dialog" title="删除确认" style="width:150px;height:150px;padding:10px" 
			data-options="
				iconCls: 'icon-del',
				buttons: [{
					text:'Ok',
					iconCls:'icon-ok',
					handler:function(){
						deleteInfos();
                        $('#dlg').dialog('close');
					}
				},{
					text:'Cancel',
					handler:function(){
						 $('#dlg').dialog('close');
					}
				}]
			">
		<%--是否确认永久删除该行信息！--%>
	</div>
    <object classid="clsid:730BF2F0-EAE2-46C5-BA06-5ABFC9AB8A0A" width="0" height="0" align="center" codebase="zx_32.CAB#version=1,0,0,3" id="CZx_32Ctrl" hspace="0" vspace="0"></object>
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
                $('#w').window('open');
            });

            //编辑按钮点击
            $('.toppenBtn').first().click(function () {
                isEdit = true;
                initEidtWidget();
            });


            //删除按钮
            $('.topdelBtn').first().click(function (e) {
                $('#dlg').dialog('open');
                //if (confirm("删除是不可恢复的，你确认要删除吗？")) {
                //   
                //}
            });
            $('#dlg').dialog('close');
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
                url: "/HttpHandlers/OperatorHandler.ashx?method=queryOperatorList",
                rownumbers: true,
                pagination: true,
                rownumbers: true,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
                      //{ field: 'ck', checkbox: true },
                      { field: 'list_st_id', title: '工位id', hidden: true },
                      { field: 'op_isoperator', title: '是否为管理员', hidden: true },
                      { field: 'op_sex', title: '性别', hidden: true },
                      { field: 'op_pic', title: '图片', formatter: function (value, row, index) { return '<img src="' + row.op_pic + '" style="height:80px;" />'; }, align: "center" },
                      { field: 'op_id', title: 'id', width: 60, align: "center" },
                      { field: 'op_name', title: '姓名', width: 200, align: "center" },
                      { field: 'op_sex_name', title: '性别', width: 100, align: "center" },
                      { field: 'op_no', title: '工号', width: 200, align: "center" },
                      { field: 'list_st_no', title: '所属工位', width: 300, align: "center" },
                      { field: 'op_isoperator_name', title: '是否为管理员', align: "center" },
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
            try {
                // Select1 name sex webChatNum tel address pCode
                var op_id = isEdit == true ? opid : 0;
                //修改时间2017年5月9日冉守旭
                //修改开始
                var st_id = $("#st_id").combobox('getValues');
                if (st_id == "") {
                    alert("请选择所属工位");
                    return false;
                }
                //var st_id = $('#st_id').combo('getValue');
                //修改结束
                var op_name = $('#op_name').val();
                var op_no = $('#op_no').val();
                var op_sex = ($('#op_sex').switchbutton('options').checked == true) ? 1 : 0;
                var op_isoperator = ($('#op_isoperator').switchbutton('options').checked == true) ? 1 : 0;
                var op_pic = $('#op_pic').attr('src');
                //alert('abc');
                var model = {
                    op_id: op_id,
                    st_id: st_id,
                    op_name: op_name,
                    op_no: op_no,
                    op_sex: op_sex,
                    op_isoperator: op_isoperator,
                    op_pic: op_pic,
                    method: 'saveOperator'
                };

                //keytypem1 1 ...PwdM1 FFFFFFFFFFFF ... 
                //DevOpen  RfCard RfAuthenticationKey RfWrite

                //tmpNO = op_no;//测试用
                if (tmpNO != op_no) {   //是否新生成工号
                    if (DevOpen()) {
                        if (RfCard()) {
                            if (RfAuthenticationKey()) {
                                if (RfWrite(op_no)) {
                                    saveTheOperator(model);
                                }
                            }
                        }
                    }
                } else {
                    saveTheOperator(model);
                }

            } catch (e) {
                alert(e);
            }
          
        }
        function saveTheOperator(model) {
            try {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/HttpHandlers/OperatorHandler.ashx",
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
            } catch (e) {
                alert(e);
            }
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
            opid = row.op_id;
            var group = row.list_st_id;
            $('#st_id').combobox('setValues', group.split(","));
            $('#op_name').val(row.op_name);
            $('#op_no').val(row.op_no);
            tmpNO = row.op_no;
            $('#op_pic').attr('src', row.op_pic)

            if (row.op_sex == 1)
                $('#op_sex').switchbutton('check');
            else
                $('#op_sex').switchbutton('uncheck');
            if (row.op_isoperator == 1)
                $('#op_isoperator').switchbutton('check');
            else
                $('#op_isoperator').switchbutton('uncheck');

            $('#w').window('open');
        }
        function deleteInfos() {
            if (event.srcElement.outerText == "删除") {
                event.returnValue = confirm("删除是不可恢复的，你确认要删除吗？");
            }
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
                url: "/HttpHandlers/OperatorHandler.ashx",
                data: encodeURI("op_id=" + row.op_id + "&method=deleteOperator"),
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
            try {
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
            } catch (e) {
                alert(e);
                return false;
            }
          
        }
        function RfCard() {
            try {
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
            } catch (e) {
                alert(e);
                return false;
            }
         
        }
        function RfAuthenticationKey() {
            try {
                var ret = CZx_32Ctrl.RfAuthenticationKey(DeviceHandleValue, "1", "8", "FFFFFFFFFFFF");//8为块地址,原来为10
                if (CZx_32Ctrl.lErrorCode == 0) {
                    //alert("校验密码成功");
                    return true;
                }
                else {
                    alert("校验密码失败，错误码为：" + CZx_32Ctrl.lErrorCode);
                    return false;
                }
            } catch (e) {
                alert(e);
                return false;
            }
         
        }
        /*修改时间2017年6月27日
          修改人：冉手旭
          修改内容：补0位数修改
        */
        function RfWrite(op_no) {
            try {
                var additionStr = "0000000000000000000000000";//25个0+7,7为工号如：A000001，共32位
                var no = additionStr + op_no;
                var ret = CZx_32Ctrl.RfWrite(DeviceHandleValue, "8", no);//8为块地址,原来为10
                if (ret == 0) {
                    alert("写数据成功");
                    return true;
                }
                else {
                    alert("写数据失败，错误码为：" + CZx_32Ctrl.lErrorCode);
                    return false;
                }
            } catch (e) {
                alert(e);
                return false;
            }
          
        }
    </script>
</asp:Content>

