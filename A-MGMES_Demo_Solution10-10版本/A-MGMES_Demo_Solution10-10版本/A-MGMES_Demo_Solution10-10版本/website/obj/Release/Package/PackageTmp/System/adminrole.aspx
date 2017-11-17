<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="AdminCMS_System_adminrole" ValidateRequest="false" Codebehind="adminrole.aspx.cs" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- 顶部工具栏  -->
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp; &nbsp;用户权限&nbsp; &nbsp;&nbsp; &nbsp;在用户和权限数据列表中，鼠标右键进行操作。
                </td>
            </tr>
        </table>
    </div>
    <!-- 数据表格  -->
    <input id="rowidhid" type="hidden" />
    <table style="width: 100%">
        <tr>
            <td style="width: 50%; vertical-align: top; text-align: left">
                <table id="tb" class="easyui-datagrid" data-options="title:'用户列表',url:'/HttpHandlers/AdminHandler.ashx?method=queryAdminList',singleSelect:true,onRowContextMenu: function (e, rowIndex, rowData) {onRowContextMenu(e, rowIndex, rowData);},onClickRow:function(index,row){loadrole(index,row);}">
                    <thead>
                        <tr>
                            <th data-options="field:'id', hidden: true">
                                用户id
                            </th>
                            <th data-options="field:'name'">
                                姓名
                            </th>
                            <th data-options="field:'tel'">
                                手机号
                            </th>
                            <th data-options="field:'password'">
                                密码
                            </th>
                            <th data-options="field:'webChatName'">
                                企业微信号
                            </th>
                            <th data-options="field:'tel1'">
                                企业手机
                            </th>
                            <th data-options="field:'isFreezed', hidden: true">
                                是否停用
                            </th>
                            <th data-options="field:'isFreezedName'">
                                是否停用
                            </th>
                            <th data-options="field:'creatDate'">
                                登记日期
                            </th>
                            <th data-options="field:'updateDate'">
                                更新日期
                            </th>
                        </tr>
                    </thead>
                </table>
            </td>
            <td style="vertical-align: top; text-align: left">
                <table id="roletb" class="easyui-datagrid" data-options="title:'权限列表',rownumbers:true,onRowContextMenu: function (e, rowIndex, rowData) {onRowRightContextMenu(e, rowIndex, rowData);}">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true">
                            </th>
                            <th data-options="field:'id'">
                                id
                            </th>
                            <th data-options="field:'name'">
                                菜单
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                            </td>
                            <td>
                                1
                            </td>
                            <td>
                                客户管理
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                5
                            </td>
                            <td>
                                客户档案
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                2
                            </td>
                            <td>
                                账务管理
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                6
                            </td>
                            <td>
                                客户审核
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                7
                            </td>
                            <td>
                                账面流水
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                8
                            </td>
                            <td>
                                发货登记
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                9
                            </td>
                            <td>
                                发货流水
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                3
                            </td>
                            <td>
                                查询统计
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                10
                            </td>
                            <td>
                                客户统计
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                11
                            </td>
                            <td>
                                账面统计
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                12
                            </td>
                            <td>
                                发货统计
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                4
                            </td>
                            <td>
                                系统设置
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                13
                            </td>
                            <td>
                                用户权限
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                14
                            </td>
                            <td>
                                修改密码
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <!-- 用户编辑窗口 -->
    <input type="hidden" value="" id="idhid">
    <div id="w" class="easyui-window" data-options="modal:true,closed:true,minimizable:false,collapsible:false,width:350,height:350,footer:'#ft',top:20,onBeforeClose:function(){clearw();}"
        style="padding: 10px;" title="用户编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title" style="width: 100px;">
                    <p>
                        姓名：</p>
                </td>
                <td>
                    <input type="text" class="text" id="name" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        个人手机：</p>
                </td>
                <td>
                    <input type="text" class="text" id="tel" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        企业微信号：</p>
                </td>
                <td>
                    <input type="text" class="text" id="webChatName" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        企业手机号：</p>
                </td>
                <td>
                    <input type="text" class="text" id="tel1" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        是否停用：</p>
                </td>
                <td>
                    <input id="sb" class="easyui-switchbutton" data-options="onText:'是',offText:'否'">
                </td>
            </tr>
        </table>
    </div>
    <!-- 新建窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
    <!-- 右键菜单--左 -->
    <div id="mm" class="easyui-menu" style="width: 120px;">
        <div data-options="iconCls:'icon-add'" onclick="openAdminWindow('0');">
            新增</div>
        <div data-options="iconCls:'icon-edit'" onclick="openAdminWindow('1');">
            编辑</div>
        <div data-options="iconCls:'icon-back'" onclick="freezingAdmin(0);">
            启用用户</div>
        <div data-options="iconCls:'icon-remove'" onclick="freezingAdmin(1);">
            停用用户</div>
        <div data-options="iconCls:'icon-undo'" onclick="resetPWD();">
            密码归零</div>
        <div class="menu-sep">
        </div>
        <div data-options="iconCls:'icon-reload'" onclick="$('#tb').datagrid('reload');">
            刷新数据</div>
    </div>
    <!-- 右键菜单--右 -->
    <div id="mmr" class="easyui-menu" style="width: 120px;">
        <div data-options="iconCls:'icon-save'" onclick="saverole();">
            保存设置</div>
    </div>
    <script type="text/javascript">

        var dg;

        function saverole() {
            var id = $('#rowidhid').val();
            if (id == '') {
                alert('请指定一位用户');
                return false;
            }

            var rows = $('#roletb').datagrid('getSelections');
            var arr = new Array();

            $(rows).each(function (index, row) {
                arr.push(row.id);
            });
            var ids = arr.join(',');
            var model = {
                adminid: id,
                menuids: ids,
                method: 'saverole'
            };

            $.ajax({
                type: "POST",
                async: true,
                url: "/HttpHandlers/AdminHandler.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                    }
                    else alert('保存失败');
                },
                error: function () {
                    alert('保存失败');
                }
            });

        }

        function loadrole(index, row) {

            $('#roletb').datagrid('uncheckAll');
            $('#rowidhid').val(row.id);
            var id = row.id;

            $.ajax({
                url: "/HttpHandlers/AdminHandler.ashx",
                data: encodeURI("method=getroleids&id=" + id),
                success: function (data) {
                    var ids = eval(data);
                    var rows = $('#roletb').datagrid('getRows');
                    $(ids).each(function (index, id) {
                        $(rows).each(function (index, row) {
                            if (id == row.id) {
                                $('#roletb').datagrid('checkRow', index);
                            }
                        });
                    });
                },
                error: function () {
                }
            });
        }
        function onRowRightContextMenu(e, rowIndex, rowData) {
            e.preventDefault();
            $('#mmr').menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }




        function resetPWD() {
            var rows = $('#tb').datagrid('getRows');
            var row = rows[parseInt($('#idhid').val())];
            $('#idhid').val('');
            var id = row.id;

            var model = {
                id: id,
                method: 'resetPWD'
            };

            $.ajax({
                type: "POST",
                async: true,
                url: "/HttpHandlers/AdminHandler.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                        $('#tb').datagrid('reload');

                    }
                    else alert('保存失败');
                },
                error: function () {
                    alert('保存失败');
                }
            });

        }


        function freezingAdmin(isfreeze) {
            var rows = $('#tb').datagrid('getRows');
            var row = rows[parseInt($('#idhid').val())];
            $('#idhid').val('');
            var id = row.id;

            var model = {
                id: id,
                isFreezed: isfreeze,
                method: 'freezingAdmin'
            };

            $.ajax({
                type: "POST",
                async: true,
                url: "/HttpHandlers/AdminHandler.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                        $('#tb').datagrid('reload');

                    }
                    else alert('保存失败');
                },
                error: function () {
                    alert('保存失败');
                }
            });
        }


        function openAdminWindow(item) {
            if (item == '0') {
                $('#idhid').val('');
            } if (item == '1') {

                var rows = $('#tb').datagrid('getRows');
                var row = rows[parseInt($('#idhid').val())];

                $('#idhid').val(row.id);
                $('#name').val(row.name);
                $('#webChatName').val(row.webChatName);
                $('#tel').val(row.tel);
                $('#tel1').val(row.tel1);
                if (row.isFreezed == 1)
                    $('#sb').switchbutton('check');
            }

            $('#w').window('open');
        }

        function onRowContextMenu(e, rowIndex, rowData) {
            e.preventDefault();
            $('#idhid').val(rowIndex);
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }

        $(function () {

            //新增按钮点击
            $('.saveBtn').first().click(function () {
                editInfo();
            });
        });

        function editInfo() {
            var id = $('#idhid').val();
            var name = $('#name').val();
            var webChatName = $('#webChatName').val();
            var tel = $('#tel').val();
            var tel1 = $('#tel1').val();
            var isFreezed = ($('#sb').switchbutton('options').checked == true) ? 1 : 0;

            var model = {
                id: id,
                name: name,
                webChatName: webChatName,
                tel: tel,
                tel1: tel1,
                isFreezed: isFreezed,
                method: 'editAdmin'
            };

            $('#loadinggif').first().toggle();
            $('#saveBtn').toggle();

            $.ajax({
                type: "POST",
                async: true,
                url: "/HttpHandlers/AdminHandler.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                        $('#tb').datagrid('reload');

                    }
                    else alert('保存失败');
                    $('#loadinggif').first().toggle();
                    $('#saveBtn').toggle();
                    $('#w').window('close');
                },
                error: function () {
                    $('#loadinggif').first().toggle();
                    $('#saveBtn').toggle();
                }
            });
        }


        function clearw() {
            $('#idhid').val('');
            $('#name').val('');
            $('#webChatName').val('');
            $('#tel').val('');
            $('#tel1').val('');
            $('#tel1').val('');
            $('#sb').switchbutton('uncheck');
        }
    </script>
</asp:Content>
