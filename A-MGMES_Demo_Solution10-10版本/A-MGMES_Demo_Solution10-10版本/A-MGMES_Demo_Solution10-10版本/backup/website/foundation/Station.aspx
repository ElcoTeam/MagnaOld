<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Station.aspx.cs" Inherits="foundation_Station" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
    <script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>
    <%--<script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>--%>

    <link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>

                <td><span class="title">工位档案</span> <span class="subDesc">拖拽数据行进行排序</span>
                </td>
                <td></td>
                <td style="width: 210px;">
                    <select id="fl_id_s" class="easyui-combobox" style="width: 200px; height: 25px;"
                        data-options="valueField: 'fl_id',textField: 'fl_name'">
                    </select></td>
                <td style="width: 110px;"><a class="topsearchBtn">筛选工位</a></td>

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
    <table id="tb" title="工位列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="工位编辑">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="title">
                    <p>
                        工位号：
                    </p>
                </td>
                <td colspan="2">
                    <input id="st_no" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        所属流水线：
                    </p>
                </td>
                <td colspan="2">
                    <select id="fl_id" class="easyui-combobox" style="width: 230px; height: 25px;"
                        data-options="valueField: 'fl_id',textField: 'fl_name'">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        工位类型：
                    </p>
                </td>
                <td colspan="2">
                    <select id="st_typeid" class="easyui-combobox" style="width: 230px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        工位描述：
                    </p>
                </td>
                <td colspan="2">
                    <input id="st_name" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        MAC地址：
                    </p>
                </td>
                <td colspan="2">
                    <input id="st_mac" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        首工位：
                    </p>
                </td>
                <td>
                    <input id="st_isfirst" class="easyui-switchbutton" data-options="onText:'是',offText:'否'">
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        末工位：
                    </p>
                </td>
                <td>
                    <input id="st_isend" class="easyui-switchbutton" data-options="onText:'是',offText:'否'">
                </td>
            </tr>
            <tr>
                <td class="title">ODS-PDF文件：
                </td>
                <td>
                    <input type="file" name="pdfuploadify" id="pdfuploadify" />
                    <div id="pdfuploadify-queue" style="height: 0px; width: 0px"></div>
                </td>
                <td style="text-align: left">
                    <img id="st_odsfile" src="/image/admin/OK.png" style="visibility: hidden" alt="" /><br />
                </td>
            </tr>

            <tr>
                <td class="title">目视图片：
                </td>
                <td colspan="2">&nbsp;&nbsp;<img id="st_mushifile" src="/image/admin/camera.png" style="height: 80px;" /><br />
                    <br />
                    <input type="file" name="uploadify" id="uploadify" />
                    <div id="uploadify-queue" style="height: 0px; width: 0px"></div>
                </td>
            </tr>
            <%-- <tr>
                <td class="title">
                    <p>
                        是否预装：
                    </p>
                </td>
                <td>
                    <input id="st_ispre" class="easyui-switchbutton" data-options="onText:'是',offText:'否',checked:true">
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        预装时间：
                    </p>
                </td>
                <td>
                    <input id="st_pretime" type="text" class="text" style="width: 230px;" />
                </td>st_no st_name st_mac fl_id st_typeid st_ispre st_pretime
            </tr>--%>
        </table>
    </div>
    <!-- 编辑窗口 - footer -->
    <div id="ft" style="padding: 10px; text-align: center; background-color: #f9f9f9; visibility: hidden">
        <img src="/image/admin/loading-ring.gif" class="loadinggif" id="loadinggif" /><a
            class="saveBtn" id="saveBtn">保存</a>
    </div>


    <script>

        /****************       全局变量          ***************/
        var stid;               //要编辑的id
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
            $('.topdelBtn').first().click(function () {
                deleteInfos();
            });

            //搜索按钮
            $('.topsearchBtn').first().click(function () {
                searchInfos();
            });

            //保存按钮
            $('#saveBtn').bind('click', function () {
                saveAllpart(isEdit);
            });



            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 450,
                height: 650,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });

            //所属工位下拉框数据加载  
            reloadst_typeid();
            reloadfl_id();
            reloadfl_ids();

            //数据列表加载
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/StationHandler.ashx?method=queryStationList",
                rownumbers: true,
                pagination: true,
                rownumbers: true,
                singleSelect: true,
                collapsible: false,
                columns: [[
                      //{ field: 'ck', checkbox: true },
                        { field: 'st_id', title: 'id', hidden: true },
                      { field: 'fl_id', title: '流水线id', hidden: true },
                      { field: 'st_typeid', title: '工位类型id', hidden: true },
                      { field: 'st_odsfile', title: 'PDF文件原路径', hidden: true },
                      { field: 'st_isfirst', title: '是首位吗', hidden: true },
                      { field: 'st_isend', title: '是末位吗', hidden: true },
                        { field: 'st_mushifile', title: '图片', formatter: function (value, row, index) { return '<img src="' + row.st_mushifile + '" style="height:80px;" />'; }, align: "center", width: 100 },
                        { field: 'fl_name', title: '所属流水线', width: 200, align: "center" },
                      { field: 'st_order', title: '排序序号', hidden: true },
                      { field: 'st_typename', title: '类型', width: 200, align: "center" },
                        { field: 'st_no', title: '工位号', width: 200, align: "center" },
                        { field: 'st_isfirstname', title: '首位', width: 200, align: "center" },
                        { field: 'st_isendname', title: '末位', width: 200, align: "center" },
                      { field: 'st_name', title: '工位描述', width: 200, align: "center" },
                      { field: 'st_mac', title: 'MAC地址', width: 200, align: "center" },
                { field: 'st_odsfilename', title: 'PDF文件' }
                ]],
                onLoadSuccess: function () {
                    $(this).datagrid('enableDnd');
                },
                onDrop: function (targetRow, sourceRow, point) {
                    sortingData(targetRow.st_id, targetRow.st_order, sourceRow.st_id, sourceRow.st_order, point);
                    //alert(targetRow.st_id);
                    //alert(sourceRow.st_id);
                    //alert(point);
                }
            });
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 10, 20, 30, 50],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });


            //图片上传
            $("#uploadify").uploadify({
                'swf': '/js/uploadify/uploadify.swf',
                'uploader': '/HttpHandlers/UploadfyHandler.ashx?method=uploadStationImg',
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
                }
            });

            //pdf上传
            $("#pdfuploadify").uploadify({
                'swf': '/js/uploadify/uploadify.swf',
                'uploader': '/HttpHandlers/UploadfyHandler.ashx?method=uploadStationPDF',
                'buttonText': '上传PDF',
                //'formData': { 'someKey': 'someValue', 'someOtherKey': 1 },
                //上传文件页面中，你想要用来作为文件队列的元素的id, 默认为false  自动生成,  不带#
                'queueID': 'fileQueue',
                //选择文件后自动上传
                'auto': true,
                //设置为true将允许多文件上传
                'multi': false,
                'onUploadSuccess': function (file, data, response) {
                    setPDF(file, data, response);
                    //alert('The file ' + file.name + ' was successfully uploaded with a response of ' + response + ':' + data);
                }
            });
        });


        /****************       主要业务程序          ***************/

        //排序
        function sortingData(t_st_id, t_st_order, s_st_id, s_st_order, point) {
            $.ajax({
                type: "POST",
                async: true,
                url: "/HttpHandlers/StationHandler.ashx",
                data: "method=sorting&st_id=" + s_st_id + "&st_order=" + t_st_order + "&point=" + point,
                success: function (data) {
                    if (data == 'true') {
                        dg.datagrid('reload');
                    }
                    else alert('操作失败');
                },
                error: function () {
                }
            });
        }

        //新增 / 编辑  
        function saveAllpart() {
            //            
            var st_id = isEdit == true ? stid : 0;
            //alert(all_id);
            var fl_id = $('#fl_id').combo('getValue');
            var st_typeid = $('#st_typeid').combo('getValue');
            var st_no = $('#st_no').val();
            var st_name = $('#st_name').val();
            var st_mac = $('#st_mac').val();
            var st_mushifile = $('#st_mushifile').attr('src');
            var st_odsfile = $('#st_odsfile').attr('alt');
            
            var st_isfirst = ($('#st_isfirst').switchbutton('options').checked == true) ? 1 : 0;
            var st_isend = ($('#st_isend').switchbutton('options').checked == true) ? 1 : 0;
            //alert('abc');

            var model = {
                st_id: st_id,
                fl_id: fl_id,
                st_typeid: st_typeid,
                st_no: st_no,
                st_name: st_name,
                st_mac: st_mac,
                st_mushifile: st_mushifile,
                st_odsfile: st_odsfile,
                st_isfirst: st_isfirst,
                st_isend: st_isend,
                method: 'saveStation'
            };
            
            saveTheAllPart(model);
        }
        function saveTheAllPart(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/StationHandler.ashx",
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
            stid = row.st_id;

            $('#st_odsfile').attr('alt', row.st_odsfile);
            $('#st_mushifile').attr('src', row.st_mushifile);

            $('#fl_id').combobox('select', row.fl_id);
            $('#st_typeid').combobox('select', row.st_typeid);
            $('#st_no').val(row.st_no);
            $('#st_name').val(row.st_name);
            $('#st_mac').val(row.st_mac);
             
             if (row.st_isfirst == 1)
                 $('#st_isfirst').switchbutton('check');
            else
                 $('#st_isfirst').switchbutton('uncheck');

             if (row.st_isend == 1)
                 $('#st_isend').switchbutton('check');
            else
                 $('#st_isend').switchbutton('uncheck');

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
                url: "/HttpHandlers/StationHandler.ashx",
                data: encodeURI("st_id=" + row.st_id + "&method=deleteStation"),
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
        function reloadst_typeid() {
            $('#st_typeid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryStationTypeForEditing');
        }
        function reloadfl_id() {
            $('#fl_id').combobox('reload', '/HttpHandlers/FlowlineHandler.ashx?method=queryFlowlingForEditing');
        }
        function reloadfl_ids() {
            $('#fl_id_s').combobox('reload', '/HttpHandlers/FlowlineHandler.ashx?method=queryFlowlingForEditing');
        }
        function searchInfos() {
            var fl_id = $('#fl_id_s').combo('getValue');
            var queryParams = {
                fl_id: fl_id
            };
            dg.datagrid({
                queryParams: queryParams
            });
            dg.datagrid('reload');
        }

        //上传完图片后，给编辑框中图片附链接
        function setImg(file, data, response) {
            $('#st_mushifile').attr('src', data);
            // $('#imgurl').val(data);
        }

        function setPDF(file, data, response) {
            $('#st_odsfile').attr('alt', data);
            $('#st_odsfile').css("visibility", "visible");
            // $('#imgurl').val(data);
        }

        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            $('#fl_id').combo('clear');
            $('#st_typeid').combo('clear');
            $('#st_no').val('');
            $('#st_name').val('');
            $('#st_mac').val('');
            $('#st_mushifile').attr('src', '/image/admin/camera.png');
            $('#st_odsfile').attr('alt', '');
            $('#st_odsfile').css("visibility", "hidden");
            $('#st_isfirst').switchbutton('uncheck');
            $('#st_isend').switchbutton('uncheck');

            //st_isfirst st_isend
        }
    </script>


    <%-- <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;工位档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;流水线工位信息
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 70px;">
                    工位号：&nbsp;
                </td>
                <td style="width: 80px;">
                    <asp:TextBox ID="txt_no" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style="width:20px;"></td>
                <td style="width: 70px;">
                    工位名称：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width:20px;"></td>
                <td style="width: 90px;">流水线名称：&nbsp;</td>
                <td style="width: 100px;">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" >
                    </asp:DropDownList>
                </td>
                <td style="width:20px;"></td>
                <td style="width: 120px;">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="是否为预装工位" />
                </td>
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="Button1" runat="server" Text="新增工位"  CssClass="addBtn" OnClick="Button1_Click"  />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="st_id" CssClass="gridview" OnPageIndexChanging="GridView1_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="工位ID"> 
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("st_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("st_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="工位号">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("st_no") %>' Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("st_no") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="工位名称">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("st_name") %>' Width="200px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("st_name") %>' Width="200px"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="流水线ID">
                <EditItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("fl_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("fl_id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="流水线名称">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="100px">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("fl_name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="是否为预装工位">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox3" runat="server" Text="是否为预装工位" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("st_pre") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能">
                <ItemTemplate>
                    <asp:ImageButton ID="BtEdit" runat="server" ImageUrl="~/image/admin/pen.png" OnClick="BtEdit_Click"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtDel" runat="server" ImageUrl="~/image/admin/delete1.png" OnClick="BtDel_Click"  />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="BtSave" runat="server" ImageUrl="~/image/admin/save.png" OnClick="BtSave_Click"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="BtCancel" runat="server" 
                        ImageUrl="~/image/admin/undo.png" OnClick="BtCancel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>--%>
</asp:Content>

