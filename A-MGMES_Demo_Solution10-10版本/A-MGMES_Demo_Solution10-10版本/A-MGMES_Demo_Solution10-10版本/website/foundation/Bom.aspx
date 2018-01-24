<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Bom" ValidateRequest="false" Codebehind="Bom.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <style>
        #w td { padding: 5px 5px; text-align: left; vertical-align: middle; }
        #w .title { vertical-align: middle; text-align: right; width: 80px; background-color: #f9f9f9; }
    </style>

    <link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="submit" name="name" value="导出Excel" id="sub" hidden="hidden" />
    <div class="top">
         <table cellpadding="0" cellspacing="0" style="width: 97%">
            <tr>

<td style="width:97%;"><div><span class="title">(Bill of Material)&nbsp;零件档案</span> <span class="subDesc">生产座椅所需的物料零件，一种零件可能被多种部件用到</span><span style="padding-left:41px;"><a class="topaddBtn">新增档案</a>
                    <a class="toppenBtn">编辑所选</a>
                    <a class="topdelBtn">删除所选</a></span><span style="float:right;">
                        <input id="orderPn" style="width: 150px" type="text"> <a class="topsearchBtn" href="javascript:;" onclick="searchPn(2)">查询</a>
  <a style="font-size: 12px; font-weight: 700; color: #000000" class="easyui-linkbutton" href="javascript:;" onclick="excelForm()">导出Excel</a></span></div>
</td>

            </tr>
        </table>
    </div>
    <!-- 数据表格  -->
    <table id="tb" title="零件列表" style="width: 98%;white-space:nowrap;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding: 0px; visibility: hidden" title="零件编辑">
        <table cellpadding="0" cellspacing="0" >

            <tr>
                <td class="title" style="width: 110px">
                    <p >
                        麦格纳零件号：
                    </p>
                </td>
                <td>
                    <input id="bom_PN" type="text" class="text" style="width: 230px;" />
                </td>
                <td style="vertical-align: top; width: 5px;"></td>
                <td style="width: 150px">零件图片：
                </td>
                <td rowspan="13" style="width: 440px; padding-left: 20px; vertical-align: top">
                    <!-- 数据表格  -->
                    <table id="partTB" title="POA部件座椅选取" style="width: 100%; height: 510px;">
                    </table>

                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        客户零件号：
                    </p>
                </td>
                <td>
                    <input id="bom_customerPN" type="text" class="text" style="width: 230px;" />
                </td>
                <td style="vertical-align: top; width: 5px;" rowspan="4"></td>
                <td rowspan="4">&nbsp;&nbsp;<img id="bom_picture" src="/image/admin/system_run.png" style="height: 80px;" /><br />
                    <br />
                    <input type="file" name="uploadify" id="uploadify" />
                    <div id="uploadify-queue" style="height: 0px; width: 0px"></div>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        客户零件号为特征码：
                    </p>
                </td>
                <td>
                    <input id="bom_isCustomerPN" class="easyui-switchbutton" data-options="onText:'是',offText:'否'">
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        重量：
                    </p>
                </td>
                <td>
                   <input id="bom_weight" type="text" class="text" style="width: 50px;" />&nbsp; g
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        level&nbsp;等级：
                    </p>
                </td>
                <td>
                    <select id="bom_leve" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
            </tr>
            <tr> 
                <td class="title">
                    <p>
                        颜色：
                    </p>
                </td>
                <td>
                    <select id="bom_colorid" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
                <td style="width: 5px;"></td>
                <td>被以下部件座椅用到：</td>

            </tr>
            <tr>
                <td class="title">
                    <p>
                        材质：
                    </p>
                </td>
                <td>
                    <select id="bom_materialid" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>

                <td rowspan="7" style="width: 5px;"></td>
                <td rowspan="7">
                    <div style="height: 270px; border: solid 1px #e5e3e3; padding: 5px" id="div_Part"></div>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        category分类：
                    </p>
                </td>
                <td>
                    <select id="bom_categoryid" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>


            </tr>

            <tr>
                <td class="title">
                    <p>
                        料箱料架号：
                    </p>
                </td>
                <td>
                    <select id="bom_storeid" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>


            </tr>

            <tr>
                <td class="title">
                    <p>
                        供应商：
                    </p>
                </td>
                <td>
                    <select id="bom_suppllerid" class="easyui-combobox" style="width: 230px; height: 25px;"
                        data-options="valueField: 'prop_id',textField: 'prop_name'">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        概述：
                    </p>
                </td>
                <td>
                    <input id="bom_profile" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>

            <tr>
                <td class="title">
                    <p>
                        描述(英文)：
                    </p>
                </td>
                <td>
                    <input id="bom_desc" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        描述(中文)：
                    </p>
                </td>
                <td>
                    <input id="bom_descCH" type="text" class="text" style="width: 230px;" />
                </td>
            </tr>
            <tr>
				<td class="title">
					<p>
						条码匹配开始位：
					</p>
				</td>
				<td>
					<input id="barcode_start" type="text" class="text" style="width: 100px;" />
				</td>
                <td class="title">
					<p style="width: 60px;">
						条码匹配长度：
					</p>
				</td>
				<td>
					<input id="barcode_number" type="text" class="text" style="width: 140px;" />
				</td>
			</tr>
		<%--	<tr>
				<td class="title">
					<p>
						条码匹配长度：
					</p>
				</td>
				<td>
					<input id="barcode_number" type="text" class="text" style="width: 130px;" />
				</td>
			</tr>--%>
            <%--<tr>
                <td class="title">图片：
                </td>
                <td>&nbsp;&nbsp;<img id="bom_picture" src="/image/admin/user_male.png" style="height: 80px;"><br />
                    <br />
                    <input type="file" name="uploadify" id="uploadify" />
                    <div id="uploadify-queue" style="height: 0px; width: 0px"></div>
                </td>
            </tr>
            <tr>
                <td class="title"></td>
                <td>被以下整车座椅用到：</td>
            </tr>

            <tr>
                <td class="title"></td>
                <td>
                    <div style="height: 240px; border: solid 1px #e5e3e3; padding: 5px" id="div_Part"></div>
                </td>
            </tr>--%>
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
        function excelForm() {
            //alert("----");
            //$("#form1").submit();
            $("#sub").click();
            //alert("11111");
        }
        /****************       全局变量          ***************/
        var bomid;               //要编辑的id
        var dg;      //数据表格
        var partdg;      //部件座椅表格
        var isEdit = false;     //是否为编辑状态
        var partIDArr = new Array();       //该零件被哪些部件座椅用到

        function searchPn(num) {
            var orderPn = $('#orderPn').val();
            var queryParams = dg.datagrid('options').queryParams;
            queryParams.orderPn = orderPn;
            queryParams.Num = num;
            dg.datagrid('reload');
        }
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
                saveBOM(isEdit);
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

            //下拉框数据加载
            //    
            reloadbom_leve();
            reloadbom_colorid();
            reloadbom_materialid();
            reloadbom_categoryid();
            reloadbom_suppllerid();
            reloadbom_bom_storeidrid();
            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 1050,
                height: 630,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
            //图片上传
            $("#uploadify").uploadify({
                'swf': '/js/uploadify/uploadify.swf',
                'uploader': '/HttpHandlers/UploadfyHandler.ashx?method=uploadBOMImg',
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



            //数据列表加载        bom_PN partTB bom_customerPN bom_picture/image/admin/system_run.png bom_isCustomerPN  bom_weight bom_leve bom_colorid bom_materialid bom_categoryid bom_suppllerid bom_profile bom_descCH
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/BOMHandler.ashx?method=queryBOMList",           //改动的地方
                rownumbers: true,
                pagination: true,
                singleSelect: true,
                collapsible: false,
                striped: true,
                nowrap:false,
                // fitColumns: true,
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
                      //{ field: 'ck', checkbox: true },
                      { field: 'bom_id', title: '查看关系网', align: "center", width: 80, formatter: function (value, row, index) { return '<img src="/image/admin/chukoulist.png" style="height:16px;cursor:pointer" onclick="showRelation(\'' + value + '\');"/>'; }, },
                      { field: 'partIDs', title: '部件座椅id集合', hidden: true },
                      { field: 'bom_isCustomerPN', title: '是否为客户特征码', hidden: true },
                      { field: 'bom_colorid', title: '颜色id', hidden: true },
                      { field: 'bom_materialid', title: '材质id', hidden: true },
                      { field: 'bom_categoryid', title: '分类id', hidden: true },
                      { field: 'bom_storeid', title: '料架号id', hidden: true },
                      { field: 'bom_suppllerid', title: '供应商id', hidden: true },
                       { field: 'bom_leve', title: '等级level', align: "center", hidden: true },
                      //{ field: 'allpartNOs', title: '整车座椅NO集合', hidden: true },
                       { field: 'bom_picture', title: '图片', formatter: function (value, row, index) { return '<img src="' + row.bom_picture + '" style="height:80px;" />'; }, align: "center", width: 100 },
                       { field: 'bom_PN', title: '麦格纳件号PN', align: "center", width: 100 },
                       { field: 'bom_customerPN', title: '客户件号', align: "center", width: 100 },
                       { field: 'bom_weight', title: '重量g', align: "center", width: 50 },
                       { field: 'bom_isCustomerPNName', title: '是否为客户特征码', align: "center", width: 120 },
                       { field: 'bom_leveName', title: '等级level', align: "center", width: 80 },
                        { field: 'bom_colorname', title: '颜色', align: "center", width: 100 },
                       { field: 'bom_material', title: '材质', align: "center", width: 100 },
                       { field: 'bom_category', title: '分类', align: "center", width: 100 },
                       { field: 'bom_storeName', title: '料架号', align: "center", width: 100 },
                       { field: 'bom_suppller', title: '供应商', align: "center", width: 200 },
                       { field: 'bom_profile', title: '概述', align: "center", width: 200 },
                       { field: 'bom_desc', title: '描述(英文)', align: "center", width: 200 },
                       { field: 'bom_descCH', title: '描述(中文)', align: "center", width: 200 },
                       { field: 'partNOs', title: '部件座椅NO集合', align: "center", width: 300 },
                       { field: 'barcode_start', title: '条码匹配开始位', align: "center", width: 100 },
                       { field: 'barcode_number', title: '条码匹配长度', align: "center", width: 100 }
                ]]
            });
            //数据列表分页
            dg.datagrid('getPager').pagination({
                pageList: [1, 5, 10, 15, 20],
                layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
            });


            //部件座椅加载
            partdg = $('#partTB').datagrid({
                url: "/HttpHandlers/PartHandler.ashx?method=queryPartListForBOM",
                rownumbers: true,
                collapsible: false,
                emptyMsg: '<span>没有找到相关记录<span>',
                columns: [[
                      { field: 'ck', checkbox: true },
                      { field: 'allpartIDs', title: '整车座椅id集合', hidden: true },
                      //{ field: 'allpartNOs', title: '整车座椅NO集合', hidden: true },
                      { field: 'part_categoryid', title: '分类id', hidden: true },
                      { field: 'part_id', title: 'id', hidden: true },
                      { field: 'part_no', title: '代号', align: "center" },
                      { field: 'part_name', title: '名称', align: "center" },
                        { field: 'part_Category', title: '分类', align: "center" },
                       { field: 'allpartNOs', title: '整车座椅订单代号', align: "center" },
                      { field: 'part_desc', title: '描述', align: "center" }
                ]],
                onCheck: function (index, row) {
                    refreshPartDiv();
                },
                onUncheck: function (index, row) {
                    refreshPartDiv();
                },
                onCheckAll: function (rows) {
                    refreshPartDiv();
                },
                onUncheckAll: function (rows) {
                    refreshPartDiv();
                }
            });

        });

        /****************       主要业务程序          ***************/

        //新增 / 编辑  
        function saveBOM() {
            //                        

            var bom_id = isEdit == true ? bomid : 0;
            var bom_PN = $('#bom_PN').val();
            var bom_customerPN = $('#bom_customerPN').val();
            var bom_isCustomerPN = ($('#bom_isCustomerPN').switchbutton('options').checked == true) ? 1 : 0;
            var bom_picture = $('#bom_picture').attr('src');
            var bom_weight = $('#bom_weight').val();
            var bom_leve = $('#bom_leve').combo('getValue');
            var bom_colorid = $('#bom_colorid').combo('getValue');
            var bom_materialid = $('#bom_materialid').combo('getValue');
            var bom_categoryid = $('#bom_categoryid').combo('getValue');
            var bom_storeid = $('#bom_storeid').combo('getValue');
            var bom_suppllerid = $('#bom_suppllerid').combo('getValue');
            var bom_profile = $('#bom_profile').val();
            var bom_descCH = $('#bom_descCH').val();
            var bom_desc = $('#bom_desc').val();
            var barcode_start = $('#barcode_start').val();
            var barcode_number = $('#barcode_number').val();
            var partIDs = partIDArr.join(',');

            var model = {
                bom_id: bom_id,
                bom_PN: bom_PN,
                bom_customerPN: bom_customerPN,
                bom_isCustomerPN: bom_isCustomerPN,
                bom_picture: bom_picture,
                bom_weight: bom_weight,
                bom_leve: bom_leve,
                bom_colorid: bom_colorid,
                bom_materialid: bom_materialid,
                bom_categoryid: bom_categoryid,
                bom_storeid: bom_storeid,
                bom_suppllerid: bom_suppllerid,
                bom_profile: bom_profile,
                bom_descCH: bom_descCH,
                bom_desc: bom_desc,
                partIDs: partIDs,
                barcode_start: barcode_start,
                barcode_number: barcode_number,
                method: 'saveBOM'
            };

            saveTheBOM(model);
        }
        function saveTheBOM(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/BOMHandler.ashx",
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
            // $('#div_Part').html(''); 
            // partdg.datagrid('clearChecked');
            //窗体数据初始化
            var row = selRows[0];
            bomid = row.bom_id;
            $('#bom_PN').val(row.bom_PN);
            $('#bom_customerPN').val(row.bom_customerPN);
            if (row.bom_isCustomerPN == 1)
                $('#bom_isCustomerPN').switchbutton('check');
            else
                $('#bom_isCustomerPN').switchbutton('uncheck');
            $('#bom_picture').attr('src', row.bom_picture);
            $('#bom_weight').val(row.bom_weight);

            $('#bom_leve').combobox('select', row.bom_leve);
            $('#bom_colorid').combobox('select', row.bom_colorid);
            $('#bom_materialid').combobox('select', row.bom_materialid);
            $('#bom_categoryid').combobox('select', row.bom_categoryid);
            $('#bom_storeid').combobox('select', row.bom_storeid);
            $('#bom_suppllerid').combobox('select', row.bom_suppllerid);

            $('#bom_profile').val(row.bom_profile);
            $('#bom_descCH').val(row.bom_descCH);
            $('#bom_desc').val(row.bom_desc);
            $('#barcode_start').val(row.barcode_start);
            $('#barcode_number').val(row.barcode_number);


            var idArr = row.partIDs.toString().split(',');
            var noArr = row.partNOs.toString().split(',');

            var rows = partdg.datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {
                for (var j = 0; j < idArr.length; j++) {
                    if (idArr[j] == rows[i].part_id.toString()) {
                        partdg.datagrid('checkRow', i);
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
                url: "/HttpHandlers/BOMHandler.ashx",
                data: encodeURI("bom_id=" + row.bom_id + "&method=deleteBOM"),
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
                url: '/HttpHandlers/BOMHandler.ashx?method=getBomRelation&id=' + bom_id
            });
            $('#tt').tree('reload');
            $('#treew').window('open');
        }


        //加载下拉信息
        function reloadbom_leve() {
            $('#bom_leve').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryLeveForBOM');
        }
        function reloadbom_colorid() {
            $('#bom_colorid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryColorForBOM');
        }
        function reloadbom_materialid() {
            $('#bom_materialid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryMaterialForBOM');
        }
        function reloadbom_categoryid() {
            $('#bom_categoryid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryCategoryForBOM');
        }
        function reloadbom_suppllerid() {
            $('#bom_suppllerid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=querySupplierForBOM');
        }
        function reloadbom_bom_storeidrid() {
            $('#bom_storeid').combobox('reload', '/HttpHandlers/PropertyHandler.ashx?method=queryStoreForBOM');
        }


        function refreshPartDiv() {
            partIDArr.length = 0;
            var selRows = partdg.datagrid('getChecked');
            //alert(selRows[0].all_no);

            var htmlArr = new Array();
            htmlArr.push('<ul>');
            for (var i = 0; i < selRows.length; i++) {
                partIDArr.push(selRows[i].part_id);
                htmlArr.push("<li>" + selRows[i].part_no + "</li>");
            }
            htmlArr.push('</ul>');
            $('#div_Part').html(htmlArr.join(''));
        }

        //上传完图片后，给编辑框中图片附链接
        function setImg(file, data, response) {
            $('#bom_picture').attr('src', data);
            // $('#imgurl').val(data);
        }

        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            //             
            $('#bom_PN').val('');
            $('#bom_customerPN').val('');
            $('#bom_weight').val('');
            $('#bom_picture').attr('src','/image/admin/system_run.png');
            $('#bom_isCustomerPN').switchbutton('uncheck');
            $('#bom_leve').combo('clear');
            $('#bom_colorid').combo('clear');
            $('#bom_materialid').combo('clear');
            $('#bom_categoryid').combo('clear');
            $('#bom_suppllerid').combo('clear');
            $('#bom_storeid').combo('clear');
            $('#bom_profile').val('');
            $('#bom_descCH').val('');
            $('#bom_desc').val('');
            $('#barcode_start').val('');
            $('#barcode_number').val('');
            partdg.datagrid('clearChecked');
            //    partTB 
        }
    </script>
</asp:Content>



<%--<div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;部件档案 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;部件信息
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100px;">
                    部件名称：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_name" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px;">
                    部件编号：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_no" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px;">
                    部件等级：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_level" runat="server" Width="200px"></asp:TextBox>
                </td> 
                <td style="width: 100px;">
                    部件描述：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_desc" runat="server" Width="200px"></asp:TextBox>
                </td> 
            </tr>

            <tr>
                <td style="width: 100px;">
                    部件图片：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:FileUpload ID="Fld_pic" runat="server" />
                </td>
                <td style="width: 100px;">
                    部件材质：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_material" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px;">
                    部件说明：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_profile" runat="server" Width="200px"></asp:TextBox>
                </td> 
                <td style="width: 100px;">
                    部件重量：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_weight" runat="server" Width="200px"></asp:TextBox>
                </td> 
            </tr>
            <tr>
                <td style="width: 100px;">
                    部件供应商：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_supplier" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px;">
                    部件品类：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_category" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 100px;">
                    备注：&nbsp;
                </td>
                <td style="width: 200px;">
                    <asp:TextBox ID="txt_comments" runat="server" Width="200px"></asp:TextBox>
                </td> 
                <td style="width: 100px; text-align: right">
                    <asp:Button ID="BtSave" runat="server" Text="新增部件"  CssClass="addBtn" 
                        onclick="BtSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="bom_id" CssClass="gridview" 
        onpageindexchanging="GridView1_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="部件ID"> 
                <ItemTemplate>
                    <asp:Label ID="lb_id" runat="server" Text='<%# Bind("bom_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lb_eid" runat="server" Text='<%# Bind("bom_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部件名称">
                <ItemTemplate>
                    <asp:Label ID="lb_name" runat="server" Text='<%# Bind("bom_name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_ename" runat="server" Text='<%# Bind("bom_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部件编号">
                <ItemTemplate>
                    <asp:Label ID="lb_no" runat="server" Text='<%# Bind("bom_no") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_eno" runat="server" Text='<%# Bind("bom_no") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部件等级">
                <ItemTemplate>
                    <asp:Label ID="lb_level" runat="server" Text='<%# Bind("bom_leve") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_elevel" runat="server" Text='<%# Bind("bom_leve") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部件描述">
                <ItemTemplate>
                    <asp:Label ID="lb_desc" runat="server" Text='<%# Bind("bom_desc") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_edesc" runat="server" Text='<%# Bind("bom_desc") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部件图片" ItemStyle-Width="120px">
                <ItemTemplate>
                    <asp:Label ID="lb_pic" runat="server" Text='<%# Bind("bom_picture") %>' Visible="false"></asp:Label>
                    <img id ="img_pic" height="100px" width="60px" alt="<%#Eval("bom_picture")%>" src="/image/bom/<%#Eval("bom_picture")%>" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:FileUpload ID="upd_pic" runat="server" /><asp:Label ID="lbl_opic" runat="server" Text='<%# Bind("bom_picture") %>' Visible="false"></asp:Label><img id ="img_opic" height="100px" width="60px" alt="<%#Eval("bom_picture")%>" src="./image/bom/<%#Eval("bom_picture")%>" />
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="部件材质">
                <ItemTemplate>
                    <asp:Label ID="lb_material" runat="server" Text='<%# Bind("bom_material") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_ematerial" runat="server" Text='<%# Bind("bom_material") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="部件说明">
                <ItemTemplate>
                    <asp:Label ID="lb_profile" runat="server" Text='<%# Bind("bom_profile") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_eprofile" runat="server" Text='<%# Bind("bom_profile") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="部件重量">
                <ItemTemplate>
                    <asp:Label ID="lb_weight" runat="server" Text='<%# Bind("bom_weight") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_eweight" runat="server" Text='<%# Bind("bom_weight") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="部件供应商">
                <ItemTemplate>
                    <asp:Label ID="lb_suppller" runat="server" Text='<%# Bind("bom_suppller") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_esuppller" runat="server" Text='<%# Bind("bom_suppller") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="部件品类">
                <ItemTemplate>
                    <asp:Label ID="lb_category" runat="server" Text='<%# Bind("bom_category") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_ecategory" runat="server" Text='<%# Bind("bom_category") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:Label ID="lb_comments" runat="server" Text='<%# Bind("bom_comments") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_ecomments" runat="server" Text='<%# Bind("bom_comments") %>'></asp:TextBox>
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

