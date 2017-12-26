<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_Part1" ValidateRequest="false" CodeBehind="Part1.aspx.cs"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/foundation.css" rel="stylesheet" type="text/css" />
    <style>
        #w td {
            padding: 5px 5px;
            text-align: left;
            vertical-align: middle;
        }

        #w .title {
            vertical-align: middle;
            text-align: right;
            width: 80px;
            background-color: #f9f9f9;
        }

        p {
            padding: 5px;
        }
    </style>

    <link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>
    <script src="/js/validate.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0"  style="width: 100%">
            <tr>
                <td><span class="title">部件档案</span> 
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
    <div id="w" style="padding-left: 24px;;padding-top:35px; visibility: hidden" title="部件编辑">
        <table cellpadding="0" cellspacing="0" table-layout:fixed; >
             <tr>
                <td class="title">
                    <p>
                        件号：
                    </p>
                </td>
                 
                <td>
                    <input id="part_no" type="text" class="easyui-validatebox" data-options="required:true,validType:['length[1,50]']" style="width: 230px;height: 25px;" />
                </td>
           </tr>
            <tr>
                <td class="title">
                    <p>
                        名称：
                    </p>
                </td>
                 
                <td>
                    <input id="part_name" type="text" class="easyui-validatebox" data-options="required:true,validType:['length[1,50]']" style="width: 230px;height: 25px;" />
                </td>
           </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        电气xls：
                    </p>
                </td>
                <td>
                    <select id="part_categoryid" data-options="valueField:'prop_id',textField:'prop_name',url:''"  class="easyui-combobox" style="width: 120px; height: 25px;">
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
                   <input id="part_desc" type="text" class="easyui-validatebox" data-options="required:true,validType:['length[1,50]']" style="width: 230px;height: 65px;" />
                </td>                
            </tr>
            <tr>
                <td class="title">
                    <p>
                        部件类别：
                    </p>
                </td>
                <td>
                    <select id="part_type" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="required:true">
                        <option value="1">主驾靠背</option>
                        <option value="4">副驾靠背</option>
                        <option value="2">主驾坐垫</option>
                        <option value="5">副驾坐垫</option>
                        <option value="3">主驾总</option>
                        <option value="6">副驾总</option>
                        <option value="7">后排40%</option>
                        <option value="8">后排60%</option>
                        <option value="9">后排100%</option>
                    </select></td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        流水线名称：
                    </p>
                </td>
                <td>
                    <select id="FlowLine" data-options="valueField:'fl_id',textField:'fl_name'"  class="easyui-combobox" style="width: 100px; height: 25px;">
                    </select>
                </td>               
            </tr>
            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        产品名称：
                    </p>
                </td>
                <td>
                    <select id="Product" data-options="valueField:'p_id',textField:'ProductName'"  class="easyui-combobox" style="width: 200px; height: 25px;">
                    </select>
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
        var pid;               //要编辑的id
        var dg;      //数据表格
        var isEdit = false;     //是否为编辑状态


        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            //新增按钮点击

            $('.topaddBtn').first().click(function () {
                isEdit = false;
                $('#part_type').combobox('setValue', 1);
                $('#part_categoryid').combobox(
           {
               url: '/HttpHandlers/PropertyHandler.ashx?method=queryCategoryForPart',
               onLoadSuccess: function () {
                   var val = $(this).combobox('getData');
                   $(this).combobox('select', val[0].prop_id);
               }
           });
                $('#FlowLine').combobox(
          {
              url: '/HttpHandlers/FlowLine1Handler.ashx?method=queryFlowLineidForPart',
              onLoadSuccess: function () {
                  var val = $(this).combobox('getData');
                  $(this).combobox('select', val[0].fl_id);
              }
          });
                $('#Product').combobox(
               {
                   url: '/HttpHandlers/ProductHandler.ashx?method=queryProductidForPart',
                   onLoadSuccess: function () {
                       var val = $(this).combobox('getData');
                       $(this).combobox('select', val[0].p_id);
                   }
               });
                
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
                savePart1(isEdit);
            });

            //下拉框数据加载
            $('#part_type').combobox('setValue', 1);
            $('#part_categoryid').combobox({
                url: '/HttpHandlers/PropertyHandler.ashx?method=queryCategoryForPart',
                onLoadSuccess: function () {
                    var val = $(this).combobox('getData');
                    $(this).combobox('select', val[0].prop_id);
                }
            });
            $('#FlowLine').combobox({
                url: '/HttpHandlers/FlowLine1Handler.ashx?method=queryFlowLineidForPart',
                onLoadSuccess: function () {
                    var val = $(this).combobox('getData');
                    $(this).combobox('select', val[0].fl_id);
                }
            });
            $('#Product').combobox(
           {
               url: '/HttpHandlers/ProductHandler.ashx?method=queryProductidForPart',
               onLoadSuccess: function () {
                   var val = $(this).combobox('getData');
                   $(this).combobox('select', val[0].p_id);
               }
           });


            //编辑窗口加载


            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 600,
                height: 500,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });

            //数据列表加载        bom_PN partTB bom_customerPN bom_picture/image/admin/system_run.png bom_isCustomerPN  bom_weight bom_leve bom_colorid bom_materialid bom_categoryid bom_suppllerid bom_profile bom_descCH
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/Part1Handler.ashx?method=QueryPart1List",           //改动的地方
                rownumbers: true,
                pagination: true,
                pageSize: 20,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                columns: [[
                      { field: 'part_id', title: 'ID', align: "center", hidden: "true" },
                      { field: 'part_name', title: '名称', align: "center", width: 100 },
                      { field: 'part_no', title: '件号', align: "center", width: 100 },
                      { field: 'part_desc', title: '描述', align: "center", width: 100 },
                      { field: 'parttypename', title: '部件类别', align: "center", width: 100 },
                       { field: 'propname', title: '电气xls', align: "center", width: 100 },
                      { field: 'pflowlinename', title: '流水线名称', align: "center", width: 100 },
                      { field: 'pproductname', title: '产品名称', align: "center", width: 100 },
                       { field: 'propid', title: 'propid', align: "center", hidden: "true" },
                        { field: 'part_type', title: 'part_type', align: "center", hidden: "true" },
                         { field: 'pflowlineid', title: 'pflowlineid', align: "center", hidden: "true" },
                          { field: 'pproductid', title: 'pproductid', align: "center", hidden: "true" },
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
        function savePart1() {
            //                        

            var part_id = isEdit == true ? pid : 0;

            var part_no = $('#part_no').val();
            var part_name = $('#part_name').val();
            var part_categoryid = $('#part_categoryid').combo('getValue');
            var part_desc = $('#part_desc').val();
            var part_type = $('#part_type').combo('getValue');
            var FlowLine = $('#FlowLine').combo('getValue');
            var Product = $('#Product').combo('getValue');



            var model = {
                part_id: part_id,
                part_no: part_no,
                part_name: part_name,
                part_categoryid: part_categoryid,
                part_desc: part_desc,
                part_type: part_type,
                FlowLine: FlowLine,
                Product: Product,
                method: 'savePart1'
            };
            var ispass = $("#w").form('validate');

            if (ispass != true) {
                return;
            }
            else if (ispass == true) {
                saveThePart1(model);
            }



        }
        function saveThePart1(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/Part1Handler.ashx",
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
            var row = selRows[0];
            pid = row.part_id;
            clearw();
            $('#part_no').val(row.part_no);
            $('#part_name').val(row.part_name);
            $('#part_desc').val(row.part_desc);
            
              
                if (row.propid != 0) {
                    $('#part_categoryid').combobox('select', row.propid);
                }
                if (row.part_type != 0) {
                    $('#part_type').combobox('select', row.part_type);
                }
                if (row.pflowlineid != 0) {
                    $('#FlowLine').combobox('select', row.pflowlineid);
                }
                if (row.pproductid != 0) {
                    $('#Product').combobox('select', row.pproductid);
                }
           
          
            //$('#part_type').combobox('setValue', row.part_type);
            //$('#FlowLine').combobox('setValue', row.pflowlineid);
            //$('#Product').combobox('setValue', row.pproductid);
            $(function () {
                $('input.easyui-validatebox').validatebox('disableValidation')//////////
                .focus(function () { $(this).validatebox('enableValidation'); })
                .blur(function () { $(this).validatebox('validate') });
            });

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
                url: "/HttpHandlers/Part1Handler.ashx",
                data: encodeURI("part_id=" + row.part_id + "&method=DeletePart1"),
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

        //    $('#com_testgroupid').combobox('reload', '/HttpHandlers/Part1GroupHandler.ashx?method=queryGroupidForPart1')
        //    $("#com_testgroupid").combobox("setValue", "ECU")

        //}


        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            //             
            $('#part_no').val('');
            $('#part_name').val('');
            $('#part_categoryid').combo('clear');
            $('#part_desc').val('');;
            $('#part_type').combo('clear');
            $('#FlowLine').combo('clear');
            $('#Product').combo('clear');
        }
    </script>
</asp:Content>
