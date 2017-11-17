<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_User" ValidateRequest="false" Codebehind="User.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">用户档案</span> <span class="subDesc">编辑窗口右侧可进行系统菜单权限配置,勾选可见</span>
                </td>
                <td style="width: 120px">
                    <a class="topaddBtn">新增用户</a>
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
    <table id="tb" title="用户列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility:hidden" title="用户编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        所属部门：
                    </p>
                </td>
                <td>
                    <select id="user_depid" class="easyui-combobox" name="st_id" style="width: 230px; height: 25px;"
                        data-options="valueField: 'dep_id',textField: 'dep_name'">
                    </select>
                </td>
                <td rowspan="6" style="padding-left: 20px">
                    <div style="width: 300px; height: 250px; overflow-x: hidden; overflow-y: auto;">
                        <%--<ul id="menuTree">
                        </ul>--%>

                        <ul id="menuTree" class="easyui-tree" data-options="checkbox:true"></ul>

                    </div>
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        职位：
                    </p>
                </td>
                <td>
                    <select id="user_posiid" class="easyui-combobox" name="st_id" style="width: 230px; height: 25px;"
                        data-options="valueField: 'posi_id',textField: 'posi_name'">
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
                    <input id="user_name" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        性别：
                    </p>
                </td>
                <td>
                    <input id="user_sex" class="easyui-switchbutton" data-options="onText:'男',offText:'女',checked:true">
                </td>
            </tr>
             <tr>
                <td class="title">
                    <p>
                        密码：
                    </p>
                </td>
                <td>
                    <input id="user_pwd" class="text" style="width: 230px;">
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        是否为管理员：
                    </p>
                </td>
                <td>
                    <input id="user_isAdmin" class="easyui-switchbutton" data-options="onText:'是',offText:'否'">
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        e-mail：
                    </p>
                </td>
                <td>
                    <input id="user_email" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            
        </table>
    </div>
    <!-- 编辑窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility:hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
    <!-- 菜单权限展示 -->
    <div id="showw"  style="padding: 10px; visibility:hidden" title="菜单权限">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <ul id="menuTreeShow" class="easyui-tree" data-options="checkbox:true"></ul>
                </td>
            </tr>
        </table>
    </div>
    <script>

        /****************       全局变量          ***************/
        var userid;               //要编辑的操作员id
        var dg = $('#tb');      //表格
        var isEdit = false;     //是否为编辑状态
        var menuJSON;           //菜单数据

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
            $('.topdelBtn').first().click(function () {
                deleteInfos();
            });

            //保存操作工按钮
            $('#saveBtn').bind('click', function () {
                saveUser(isEdit);
            });

            //菜单权限树
            $('#menuTree').tree({
                data: menuJSON
            });
           
            //数据列表加载
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/UserHandler.ashx?method=queryUserList",
                rownumbers: true,
                pagination: true,
                rownumbers: true,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                columns: [[
                      //{ field: 'ck', checkbox: true },
                      { field: 'user_depid', title: '部门id', hidden: true },
                      { field: 'user_posiid', title: '职位id', hidden: true },
                      { field: 'user_isAdmin', title: '是否为管理员', hidden: true },
                      { field: 'user_sex', title: '性别', hidden: true },
                      //{ field: 'user_menuids', title: '菜单权限', hidden: true },

                      { field: 'user_id', title: 'id', width: 100, align: "center" },
                      { field: 'user_name', title: '姓名', width: 100, align: "center" },
                      { field: 'user_sex_name', title: '性别', width: 100, align: "center" },
                      { field: 'user_pwd', title: '密码', width: 100, align: "center" },
                      { field: 'user_depid_name', title: '部门', width: 100, align: "center" },
                      { field: 'user_posiid_name', title: '职位', width: 100, align: "center" },
                      { field: 'user_email', title: 'e-mail', width: 100, align: "center" },
                      { field: 'user_isAdmin_name', title: '是否为管理员', width: 100, align: "center" },
                        { field: 'user_menuids', title: '查看菜单权限', align: "center", width: 100, formatter: function (value, row, index) { return '<img src="/image/admin/chukoulist.png" style="height:16px;cursor:pointer" onclick="showMenuRole(\'' + value + '\');"/>'; }, }
                ]]
            });
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });


            //编辑窗口加载
            $('#w').window({
                modal:true,
                closed:true,
                minimizable:false,
                maximizable:false,
                collapsible:false,
                width:750,
                height:400,
                footer:'#ft',
                top:20,
                onBeforeClose:function(){clearw();},
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
            reloaddepid();  //部门下拉框数据加载
            reloadposiid()  //职位下拉框数据加载

            //权限展示窗口加载
            $('#showw').window({
                modal:true,
                closed:true,
                minimizable:false,
                maximizable:false,
                collapsible:false,
                width:250,
                height:450,
                top:20,
                onBeforeOpen: function () { $('#showw').css('visibility', 'visible'); }
            });

           

        });

        /****************       主要业务程序          ***************/

        //新增 / 编辑  操作工档案
        function saveUser() {

            var user_id = isEdit == true ? userid : 0;
            var user_depid = $('#user_depid').combo('getValue');
            var user_posiid = $('#user_posiid').combo('getValue');
            var user_name = $('#user_name').val();
            var user_email = $('#user_email').val();
            var user_pwd = $('#user_pwd').val();
            var user_sex = ($('#user_sex').switchbutton('options').checked == true) ? 1 : 0;
            var user_isAdmin = ($('#user_isAdmin').switchbutton('options').checked == true) ? 1 : 0;

            var menuidArr = new Array();
            var nodes = $('#menuTree').tree('getChecked');

            for (var i = 0; i < nodes.length; i++) {

                var len = nodes[i].id.indexOf('_');
                if (len > 0) {

                    var nodes_c = nodes[i].id;
                    nodes_c = nodes_c.substring(0, len);
                    if (menuidArr.indexOf(nodes_c) == -1) {

                        menuidArr.push(nodes_c);
                    }
                    menuidArr.push(nodes[i].id);
                }
                else {

                    if (menuidArr.indexOf(nodes[i].id) == -1) {

                        menuidArr.push(nodes[i].id);
                    }
                }

            }
            //alert(menuidArr.join(','));
            //return false;
            var user_menuids = menuidArr.join(',');

            var model = {
                user_id: user_id,
                user_depid: user_depid,
                user_pwd: user_pwd,
                user_posiid: user_posiid,
                user_name: user_name,
                user_email: user_email,
                user_sex: user_sex,
                user_isAdmin: user_isAdmin,
                user_menuids: user_menuids,
                method: 'saveUser'
            };

            saveTheUser(model);

        }
        function saveTheUser(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/UserHandler.ashx",
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
            userid = row.user_id;
            $('#user_depid').combobox('select', row.user_depid);
            $('#user_posiid').combobox('select', row.user_posiid);
            $('#user_name').val(row.user_name);
            $('#user_pwd').val(row.user_pwd);
            $('#user_email').val(row.user_email);
            if (row.user_sex == 1)
                $('#user_sex').switchbutton('check');
            else
                $('#user_sex').switchbutton('uncheck');
            if (row.user_isAdmin == 1)
                $('#user_isAdmin').switchbutton('check');
            else
                $('#user_isAdmin').switchbutton('uncheck');

            // menuTree    
            var menuidArr = row.user_menuids.split(',');
            var nodes = $('#menuTree').tree('getChildren');
            for (var i = 0; i < nodes.length; i++) {
                $('#menuTree').tree('uncheck', nodes[i].target);
                for (var j = 0; j < menuidArr.length; j++) {
                    if (nodes[i].id == menuidArr[j]) {
                        $('#menuTree').tree('check', nodes[i].target);
                        break;
                    }
                }
            }

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
                url: "/HttpHandlers/UserHandler.ashx",
                data: encodeURI("user_id=" + row.user_id + "&method=deleteUser"),
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

        //查看菜单权限
        function showMenuRole(value) {
            var menuidArr = value.split(',');
            $('#menuTreeShow').tree({
                data: menuJSON
            });
            //expendMenuTree();
            var nodes = $('#menuTreeShow').tree('getChildren');
            for (var i = 0; i < nodes.length; i++) {
                $('#menuTreeShow').tree('uncheck', nodes[i].target);
                $('#menuTreeShow').tree('expand', nodes[i].target);
                for (var j = 0; j < menuidArr.length; j++) {
                    if (nodes[i].id == menuidArr[j]) {
                        $('#menuTreeShow').tree('check', nodes[i].target);
                        break;
                    }
                }
            }
            $('#showw').window('open');
        }

        //加载部门信息
        function reloaddepid() {
            $('#user_depid').combobox('reload', '/HttpHandlers/UserHandler.ashx?method=queryDepartmentsForUser');
        }
        //加载职位信息
        function reloadposiid() {
            $('#user_posiid').combobox('reload', '/HttpHandlers/UserHandler.ashx?method=queryPositionsForUser');
        }

        //菜单json数据
        menuJSON = [
                     {
                         "id": "m1",
                         "text": "行政人事档案",
                         "state": "closed",
                         "checked": true,
                         "children": [
                             {
                                 "id": "m1_1",
                                 "text": "部门档案"
                             },
                             {
                                 "id": "m1_2",
                                 "text": "职位档案"
                             },
                             {
                                 "id": "m1_3",
                                 "text": "操作工档案"
                             },
                             {
                                 "id": "m1_4",
                                 "text": "用户权限管理"
                             },
                             {
                                 "id": "m1_5",
                                 "text": "口号宣传维护"
                             },
                             {
                                 "id": "m1_6",
                                 "text": "邮件档案"
                             }
                         ]
                     },
                     {
                         "id": "m2",
                         "text": "部件零件档案",
                         "state": "closed",
                         "checked": true,
                         "children": [
                             {
                                 "id": "m2_1",
                                 "text": "ALL 整车座椅"
                             },
                             {
                                 "id": "m2_2",
                                 "text": "POA 部件档案"
                             },
                             {
                                 "id": "m2_3",
                                 "text": "BOM 零件档案"
                             },
                             {
                                 "id": "m2_4",
                                 "text": "检测项配置"
                             },
                             {
                                 "id": "m2_5",
                                 "text": "检测项分组"
                             },
                             {
                                 "id": "m2_6",
                                 "text": "点检项"
                             },
                             {
                                 "id": "m2_7",
                                 "text": "点检站"
                             },
                             {
                                 "id": "m2_8",
                                 "text": "检测配件"
                             },
                             {
                                 "id": "m2_9",
                                 "text": "颜色配置"
                             },
                             {
                                 "id": "m2_10",
                                 "text": "返修项"
                             },
                             {
                                 "id": "m2_11",
                                 "text": "产品信息配置"
                             }
 
                         ]
                     },
                     {
                         "id": "m3",
                         "text": "生产线档案",
                         "state": "closed",
                         "checked": true,
                         "children": [
                             {
                                 "id": "m3_1",
                                 "text": "流水线档案"
                             },
                             {
                                 "id": "m3_2",
                                 "text": "工位档案"
                             },
                             {
                                 "id": "m3_3",
                                 "text": "工序步骤管理"
                             }
                             //{
                             //    "id": "m3_4",
                             //    "text": "班次档案"
                             //}
                         ]
                     },
                     {
                         "id": "m4",
                         "text": "订单管理",
                         "state": "closed",
                         "checked": true,
                         "children": [
                             {
                                 "id": "m4_1",
                                 "text": "销售订单"
                             },
                             {
                                 "id": "m4_3",
                                 "text": "紧急插入订单"
                             }
                             //{
                             //    "id": "m4_2",
                             //    "text": "生产通知单"
                             //}
                         ]
                     },
                     {
                         "id": "m5",
                         "text": "查询统计",
                         "state": "closed",
                         "checked": true,
                         "children": [
                             {
                                 "id": "m5_1",
                                 "text": "工序步骤日志查询"
                             },
                             {
                                 "id": "m5_2",
                                 "text": "产量报表查询"
                             },
                             {
                                 "id": "m5_3",
                                 "text": "报警信息查询"
                             },
                             {
                                 "id": "m5_4",
                                 "text": "时间信息查询"
                             },
                             {
                                 "id": "m5_5",
                                 "text": "扭矩/角度信息分析"
                             },
                             {
                                 "id": "m5_6",
                                 "text": "发运历史"
                             },
                             {
                                 "id": "m5_7",
                                 "text": "检测返修"
                             },
                             {
                                 "id": "m5_8",
                                 "text": "设备停机记录"
                             },
                             {
                                 "id": "m5_9",
                                 "text": "点检记录表"
                             },
                             {
                                 "id": "m5_10",
                                 "text": "客户订单报表"
                             },
                             {
                                 "id": "m5_11",
                                 "text": "FTT查询"
                             }
                         ]
                     },
                     {
                         "id": "m6",
                         "text": "MES 发运设置",
                         "state": "closed",
                         "checked": true,
                         "children": [
                             {
                                 "id": "m6_1",
                                 "text": "故障原因"
                             }
                             //{
                             //    "id": "m4_2",
                             //    "text": "生产通知单"
                             //}
                         ]
                     }
        ];



        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            //  menuTree    
            $('#user_depid').combobox('clear');
            $('#user_posiid').combobox('clear');
            $('#user_name').val('');
            $('#user_pwd').val('');
            $('#user_email').val('');
            $('#user_isAdmin').switchbutton('uncheck');
            $('#user_sex').switchbutton('check');
             
            collapseMenuTree();
        }
        function collapseMenuTree() {
            var node = $('#menuTree').tree('find', "m1");
            $('#menuTree').tree('check', node.target);
            $('#menuTree').tree('collapse', node.target);
            node = $('#menuTree').tree('find', "m2");
            $('#menuTree').tree('check', node.target);
            $('#menuTree').tree('collapse', node.target);
            node = $('#menuTree').tree('find', "m3");
            $('#menuTree').tree('check', node.target);
            $('#menuTree').tree('collapse', node.target);
            node = $('#menuTree').tree('find', "m4");
            $('#menuTree').tree('check', node.target);
            $('#menuTree').tree('collapse', node.target);
            node = $('#menuTree').tree('find', "m5");
            $('#menuTree').tree('check', node.target);
            $('#menuTree').tree('collapse', node.target);
        }
        function expendMenuTree() {
            var nodes = $('#menuTreeShow').tree('getChildren');
            for (var i = 0; i < nodes.length; i++) {
                $('#menuTreeShow').tree('uncheck', nodes[i].target);
                $('#menuTreeShow').tree('expand', nodes[i].target);
            }
        }
    </script>
</asp:Content>
