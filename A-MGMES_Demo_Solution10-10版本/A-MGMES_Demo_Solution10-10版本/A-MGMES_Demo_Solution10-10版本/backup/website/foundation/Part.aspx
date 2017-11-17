<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Part.aspx.cs" Inherits="foundation_Part" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <style>
        #w td { padding: 5px 5px; text-align: left; vertical-align: middle; }
        #w .title { vertical-align: middle; text-align: right; width: 80px; background-color: #f9f9f9; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">(Part of all)&nbsp;部件档案</span> <span class="subDesc">整车座椅中的一部分座椅，如：前排主驾,一种部件可能存在于不同整车座椅中</span>
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
    <table id="tb" title="部件列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="部件编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title">
                    <p>
                        件号：
                    </p>
                </td>
                <td>
                    <input id="part_no" type="text" class="text" style="width: 230px;" />
                </td>
                <td rowspan="6" style="width: 450px; padding-left: 20px; vertical-align: top">
                    <!-- 数据表格  -->
                    <table id="allpartTB" title="整车座椅选取" style="width: 100%; height: 430px;">
                    </table>

                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        名称：
                    </p>
                </td>
                <td>
                    <input id="part_name" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        电气xls：
                    </p>
                </td>
                <td>
                    <select id="part_categoryid" class="easyui-combobox" style="width: 230px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        描述：
                    </p>
                </td>
                <td>
                    <input id="part_desc" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title"></td>
                <td>被以下整车座椅用到：</td>
            </tr>

            <tr>
                <td class="title"></td>
                <td>
                    <div style="height: 240px; border: solid 1px #e5e3e3; padding: 5px" id="div_allPart"></div>
                </td>
            </tr>

        </table>
    </div>
    <!-- 编辑窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility: hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>
      <!-- 关系窗口 -->
    <div id="treew" style="padding: 10px; visibility: hidden" title="整车-部件-零件关系树">
        <ul id="tt"></ul>
    </div>

    <script>

        /****************       全局变量          ***************/
        var partid;               //要编辑的id
        var dg;      //数据表格
        var allPartdg;      //整车座椅表格
        var isEdit = false;     //是否为编辑状态
        var allpartIDArr = new Array();       //该部件被哪些整车座椅用到


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

            //保存按钮
            $('#saveBtn').bind('click', function () {
                savePart(isEdit);
            });

            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 850,
                height: 550,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
            //所属分类下拉框数据加载
            reloadpart_categoryid();

            //数据列表加载
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/PartHandler.ashx?method=queryAllPartList",
                rownumbers: true,
                pagination: true,
                singleSelect: true,
                collapsible: false,
                columns: [[
                      //{ field: 'ck', checkbox: true },
                      { field: 'allpartIDs', title: '整车座椅id集合', hidden: true },
                      //{ field: 'allpartNOs', title: '整车座椅NO集合', hidden: true },
                      { field: 'part_categoryid', title: '分类id', hidden: true },
                      { field: 'part_no', title: '件号', width: 200, align: "center" },
                      { field: 'part_name', title: '名称', width: 200, align: "center" },
                        { field: 'part_Category', title: '电气xls', width: 100, align: "center" },
                       { field: 'allpartNOs', title: '整车座椅订单代号', width: 300, align: "center" },
                      { field: 'part_desc', title: '描述', width: 300, align: "center" },
                      { field: 'part_id', title: '查看关系网', align: "center", width: 80, formatter: function (value, row, index) { return '<img src="/image/admin/chukoulist.png" style="height:16px;cursor:pointer" onclick="showRelation(\'' + value + '\');"/>'; }, }
                ]]
            }); 
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });


            //整车座椅加载
            allPartdg = $('#allpartTB').datagrid({
                url: "/HttpHandlers/AllPartHandler.ashx?method=queryAllPartListForPart",
                rownumbers: true,
                collapsible: false,
                columns: [[
                      { field: 'ck', checkbox: true },
                      { field: 'all_id', title: 'id', hidden: true },
                      { field: 'all_rateid', title: '等级id', hidden: true },
                      { field: 'all_colorid', title: '颜色id', hidden: true },
                      { field: 'all_metaid', title: '材质id', hidden: true },
                      { field: 'all_no', title: '代号', align: "center" },
                      { field: 'all_ratename', title: '等级', align: "center" },
                      { field: 'all_colorname', title: '颜色', align: "center" },
                      { field: 'all_metaname', title: '材质', align: "center" },
                      { field: 'all_desc', title: '备注', align: "center" }
                ]],
                onCheck: function (index, row) {
                    refreshAllPartDiv();
                },
                onUncheck: function (index, row) {
                    refreshAllPartDiv();
                },
                onCheckAll: function (rows) {
                    refreshAllPartDiv();
                },
                onUncheckAll: function (rows) {
                    refreshAllPartDiv();
                }
            });

            //关系窗口加载
            $('#treew').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 250,
                height: 450,
                top: 20,
                onBeforeOpen: function () { $('#treew').css('visibility', 'visible'); }
            });

        });

        /****************       主要业务程序          ***************/

        //新增 / 编辑  
        function savePart() {
            //            
            var part_id = isEdit == true ? partid : 0;
            //alert(all_id);
            var part_categoryid = $('#part_categoryid').combo('getValue');
            var part_no = $('#part_no').val();
            var part_name = $('#part_name').val();
            var allpartIDs = allpartIDArr.join(',');
            var part_desc = $('#part_desc').val();
            
            //alert('abc');

            var model = {
                part_id: part_id,
                part_categoryid: part_categoryid,
                part_no: part_no,
                part_desc: part_desc,
                part_name: part_name,
                allpartIDs: allpartIDs,
                method: 'savePart'
            };

            saveTheAllPart(model);
        }
        function saveTheAllPart(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/PartHandler.ashx",
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
            
           // $('#div_allPart').html('');
           // allPartdg.datagrid('clearChecked');
            //窗体数据初始化
            var row = selRows[0];
            partid = row.part_id;
            $('#part_categoryid').combobox('select', row.part_categoryid);
            $('#part_name').val(row.part_name);
            $('#part_desc').val(row.part_desc);
            $('#part_no').val(row.part_no);

            var idArr = row.allpartIDs.toString().split(',');
            var noArr = row.allpartNOs.toString().split(',');

            //var htmlArr = new Array();
            //htmlArr.push('<ul>');
            //for (var i = 0; i < noArr.length; i++) {
            //    htmlArr.push("<li>" + noArr[i] + "</li>");
            //}
            //htmlArr.push('</ul>');
            //$('#div_allPart').html(htmlArr.join(''));

            //for (var i = 0; i < idArr.length; i++) {
            //    allpartIDArr.push(idArr[i]);
            //}

            var rows = allPartdg.datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {
                for (var j = 0; j < idArr.length; j++) {
                    if (idArr[j] == rows[i].all_id.toString()) {
                        allPartdg.datagrid('checkRow', i);
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
                url: "/HttpHandlers/PartHandler.ashx",
                data: encodeURI("part_id=" + row.part_id + "&method=deletePart"),
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
        //加载分类信息
        function reloadpart_categoryid() {
            $('#part_categoryid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryCategoryForPart');
        }
        function refreshAllPartDiv() {
            allpartIDArr.length = 0;
            var selRows = allPartdg.datagrid('getChecked');
            //alert(selRows[0].all_no);

            var htmlArr = new Array();
            htmlArr.push('<ul>');
            for (var i = 0; i < selRows.length; i++) {
                allpartIDArr.push(selRows[i].all_id);
                htmlArr.push("<li>" + selRows[i].all_no + "</li>");
            }
            htmlArr.push('</ul>');
            $('#div_allPart').html(htmlArr.join(''));
        }

        //关系网
        function showRelation(bom_id) {
            //alert(bom_id);
            //  $('#tt').tree('loadData', "[]");

            //$.ajax({
            //    url: '/HttpHandlers/BOMHandler.ashx?method=getBomRelation&id=' + bom_id,
            //    dataType:'json',
            //    success: function (data) {
            //        //alert(JSON.parse(data)[0].text);
            //        $('#tt').tree('loadData',data);
            //    },
            //    error: function () {
            //    }
            //});


            $('#tt').tree({
                url: '/HttpHandlers/BOMHandler.ashx?method=getPartRelation&id=' + bom_id
            });
            $('#tt').tree('reload');
            $('#treew').window('open');
        }
        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            $('#part_no').val('');
            $('#part_desc').val('');
            $('#part_name').val('');
            $('#div_allPart').html('');
            $('#part_categoryid').combo('clear');
            allPartdg.datagrid('clearChecked');
            //    allpartTB 
        } 
    </script>
</asp:Content>



<%--
       part_id
        <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 70px;">
                    大部件号：&nbsp;
                </td>
                <td style="width: 80px;">
                    <asp:TextBox ID="txt_no" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style="width:20px;"></td>
                <td style="width: 100px;">
                    大部件名称：&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width:20px;"></td>
                <td style="width: 100px;">大部件描述：&nbsp;</td>
                <td>
                    <asp:TextBox ID="txt_desc" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width:20px;"></td>
                <td style="width:70px;">座椅号：&nbsp;</td>
                <td style="width:100px;">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px"></asp:DropDownList>
                </td>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="Button1" runat="server" Text="新增大部件"  CssClass="addBtn" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="part_id" CssClass="gridview" OnPageIndexChanging="GridView1_PageIndexChanging"  >
        <Columns>
            <asp:TemplateField HeaderText="座椅ID">
                <EditItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("all_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("all_id") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="座椅号">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="100px">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("all_no") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="大部件ID">
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("part_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("part_id") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="大部件号">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("part_no") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("part_no") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="大部件名称">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("part_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("part_name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="大部件描述">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("part_desc") %>' Width="200px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("part_desc") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="300px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能">
                <ItemTemplate>
                    <asp:ImageButton ID="BtEdit" runat="server" ImageUrl="~/image/admin/pen.png" OnClick="BtEdit_Click"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtDel" runat="server" ImageUrl="~/image/admin/delete1.png" OnClick="BtDel_Click" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="BtSave" runat="server" ImageUrl="~/image/admin/save.png" OnClick="BtSave_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtCancel" runat="server" 
                        ImageUrl="~/image/admin/undo.png" OnClick="BtCancel_Click"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>--%>
