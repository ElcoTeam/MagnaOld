<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="foundation_User" ValidateRequest="false" %>

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
                columns: [[
                      //{ field: 'ck', checkbox: true },
                      { field: 'user_depid', title: '部门id', hidden: true },
                      { field: 'user_posiid', title: '职位id', hidden: true },
                      { field: 'user_isAdmin', title: '是否为管理员', hidden: true },
                      { field: 'user_sex', title: '性别', hidden: true },
                      //{ field: 'user_menuids', title: '菜单权限', hidden: true },

                      { field: 'user_id', title: 'id', width: 60, align: "center" },
                      { field: 'user_name', title: '姓名', width: 200, align: "center" },
                      { field: 'user_sex_name', title: '性别', width: 100, align: "center" },
                      { field: 'user_depid_name', title: '部门', width: 100, align: "center" },
                      { field: 'user_posiid_name', title: '职位', width: 100, align: "center" },
                      { field: 'user_email', title: 'e-mail', width: 200, align: "center" },
                      { field: 'user_isAdmin_name', title: '是否为管理员', align: "center" },
                        { field: 'user_menuids', title: '查看菜单权限', align: "center", width: 80, formatter: function (value, row, index) { return '<img src="/image/admin/chukoulist.png" style="height:16px;cursor:pointer" onclick="showMenuRole(\'' + value + '\');"/>'; }, }
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
            var user_sex = ($('#user_sex').switchbutton('options').checked == true) ? 1 : 0;
            var user_isAdmin = ($('#user_isAdmin').switchbutton('options').checked == true) ? 1 : 0;

            var menuidArr = new Array();
            var nodes = $('#menuTree').tree('getChecked');
            for (var i = 0; i < nodes.length; i++) {
                menuidArr.push(nodes[i].id);
            }
            //alert(menuidArr.join(','));
            //return false;
            var user_menuids = menuidArr.join(',');

            var model = {
                user_id: user_id,
                user_depid: user_depid,
                user_pwd: "1",
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
                         "text": "基础档案",
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
                             }
                         ]
                     },
                     {
                         "id": "m2",
                         "text": "部件档案",
                         "state": "closed",
                         "checked": true,
                         "children": [
                             {
                                 "id": "m2_1",
                                 "text": "整套座椅代号"
                             },
                             {
                                 "id": "m2_2",
                                 "text": "大部件座椅配置"
                             },
                             {
                                 "id": "m2_3",
                                 "text": "物料档案"
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
                             },
                             {
                                 "id": "m3_4",
                                 "text": "班次档案"
                             }
                         ]
                     },
                     {
                         "id": "m4",
                         "text": "生产订单管理",
                         "state": "closed",
                         "checked": true,
                         "children": [
                             {
                                 "id": "m4_1",
                                 "text": "客户订单"
                             },
                             {
                                 "id": "m4_2",
                                 "text": "生产通知单"
                             }
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
                                 "text": "工序步骤日志"
                             },
                             {
                                 "id": "m5_2",
                                 "text": "客户订单统计"
                             },
                             {
                                 "id": "m5_3",
                                 "text": "生产通知单统计"
                             }
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
            //var node = $('#menuTreeShow').tree('find', "m1");
            //$('#menuTreeShow').tree('expand', node.target);
            //node = $('#menuTreeShow').tree('find', "m2");
            //$('#menuTreeShow').tree('expand', node.target);
            //node = $('#menuTreeShow').tree('find', "m3");
            //$('#menuTreeShow').tree('expand', node.target);
            //node = $('#menuTreeShow').tree('find', "m4");
            //$('#menuTreeShow').tree('expand', node.target);
            //node = $('#menuTreeShow').tree('find', "m5");
            //$('#menuTreeShow').tree('expand', node.target);

            var nodes = $('#menuTreeShow').tree('getChildren');
            for (var i = 0; i < nodes.length; i++) {
                $('#menuTreeShow').tree('uncheck', nodes[i].target);
                $('#menuTreeShow').tree('expand', nodes[i].target);
            }
        }

    </script>


    <%--<div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;员工档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;员工信息
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100px;">
                    员工姓名：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px;">
                    登录密码：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_pwd" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px;">
                    员工RFID：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_rfid" runat="server" Width="200px"></asp:TextBox>
                </td> 
                <td style="width: 100px;">
                    员工Email：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_email" runat="server" Width="200px"></asp:TextBox>
                </td> 
            </tr>

            <tr>
                <td style="width: 100px;">
                    员工头像：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:FileUpload ID="Fld_pic" runat="server" />
                </td>
                <td style="width: 100px;">
                    所属部门：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="drp_depname" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px;">
                    所属职位：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="drp_posiname" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px;">
                    用户可见：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_menuids" runat="server" Width="200px"></asp:TextBox>
                </td> 
            </tr>
            <tr>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="BtSave" runat="server" Text="新增员工"  CssClass="addBtn" 
                        onclick="BtSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="user_id" CssClass="gridview" 
        onpageindexchanging="GridView1_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="员工ID"> 
                <ItemTemplate>
                    <asp:Label ID="lb_id" runat="server" Text='<%# Bind("user_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lb_eid" runat="server" Text='<%# Bind("user_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工姓名">
                <ItemTemplate>
                    <asp:Label ID="lb_name" runat="server" Text='<%# Bind("user_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_ename" runat="server" Text='<%# Bind("user_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="登录密码">
                <ItemTemplate>
                    <asp:Label ID="lb_pwd" runat="server" Text='<%# Bind("user_pwd") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_epwd" runat="server" Text='<%# Bind("user_pwd") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工RFID">
                <ItemTemplate>
                    <asp:Label ID="lb_rfid" runat="server" Text='<%# Bind("user_no") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_erfid" runat="server" Text='<%# Bind("user_no") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工Email">
                <ItemTemplate>
                    <asp:Label ID="lb_email" runat="server" Text='<%# Bind("user_email") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_eemail" runat="server" Text='<%# Bind("user_email") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工头像" ItemStyle-Width="120px">
                <ItemTemplate>
                    <asp:Label ID="lb_pic" runat="server" Text='<%# Bind("user_pic") %>' Visible="false"></asp:Label>
                    <img id ="img_pic" height="100px" width="60px" alt="<%#Eval("user_pic")%>" src="./image/user/<%#Eval("user_pic")%>" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:FileUpload ID="upd_pic" runat="server" /><asp:Label ID="lbl_opic" runat="server" Text='<%# Bind("user_pic") %>' Visible="false"></asp:Label><img id ="img_opic" height="100px" width="60px" alt="<%#Eval("user_pic")%>" src="./image/user/<%#Eval("user_pic")%>" />
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="所属部门">
                <ItemTemplate>
                    <asp:Label ID="lb_depname" runat="server" Text='<%# Bind("dep_name") %>'></asp:Label>
                    <asp:Label ID="lb_depno" runat="server" Text='<%# Bind("user_depid") %>' Visible ="false"></asp:Label>
                </ItemTemplate>

                <EditItemTemplate>
                    <asp:DropDownList ID="drp_edepname" runat="server" Width="200px">
                    </asp:DropDownList>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="所属职位">
                <ItemTemplate>
                    <asp:Label ID="lb_posiname" runat="server" Text='<%# Bind("posi_name") %>'></asp:Label>
                    <asp:Label ID="lb_posino" runat="server" Text='<%# Bind("user_posiid") %>' Visible ="false"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drp_eposiname" runat="server" Width="200px">
                    </asp:DropDownList>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户可见">
                <ItemTemplate>
                    <asp:Label ID="lb_menuids" runat="server" Text='<%# Bind("user_menuids") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_emenuids" runat="server" Text='<%# Bind("user_menuids") %>'></asp:TextBox>
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
                    <asp:ImageButton ID="BtCancel" runat="server" 
                        ImageUrl="~/image/admin/undo.png"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>--%>
</asp:Content>
