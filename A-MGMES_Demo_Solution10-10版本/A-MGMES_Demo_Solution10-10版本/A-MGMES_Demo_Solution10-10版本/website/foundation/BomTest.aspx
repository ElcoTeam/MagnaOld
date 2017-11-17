<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" Inherits="foundation_BomTest" ValidateRequest="false" CodeBehind="BomTest.aspx.cs"  %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0"  style="width: 100%">
            <tr>
                <td><span class="title">零件检测</span> 
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
    <table id="tb" title="零件检测列表" style="width: 99%;">
    </table>
    <!-- 编辑窗口 -->
    <div id="w" style="padding-left: 24px;;padding-top:35px; visibility: hidden" title="零件测试编辑">
        <table cellpadding="0" cellspacing="0" table-layout:fixed; >

            <tr>
                <td class="title" style="width: 110px;" >
                    <p>
                        检测分组ID：
                    </p>
                </td>
                <td>
                    <select id="com_testgroupid" data-options="valueField:'group_id',textField:'groupname'"  class="easyui-combobox" style="width: 100px; height: 25px;">
                    </select>
                </td>
               
            </tr>
            <tr>
                <td class="title">
                    <p>
                        标识页：
                    </p>
                </td>
                <td>
                    <select id="com_testpage" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="valueField:'id',textField:'text'">
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select>
                </td>
                
            </tr>
            <tr>
                <td class="title">
                    <p>
                        检测类型：
                    </p>
                </td>
                <td>
                   <select id="com_testtype" class="easyui-combobox" style="width: 100px; height: 25px;"
                       data-options="required:true">
                       <option value="1">自动测试</option>
                       <option value="2">手动测试</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        计算类别：
                    </p>
                </td>
                <td>
                    <select id="com_testcalculatetype" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="required:true">
                       <option value="1">根据配置最大值最小值自动计算</option>
                       <option value="2">程序中逻辑处理</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        检测内容：
                    </p>
                </td>
                <td>
                    <input class="easyui-validatebox" id="testcaption" type="text"  data-options="required:true,validType:['length[1,50]']" style="width: 230px; height: 30px;"/>
                </td>
            </tr>
            <tr>
                <td class="title" style="word-break:keep-all">
                    <p>
                        检测内容最小值：
                    </p>
                </td> 
                <td >
                    <input class="easyui-validatebox" id="testvaluemin" type="text" data-options="required:true, validType:['length[1,20]']" style="width: 230px; height: 30px;"/>
                </td>
            </tr>
            <tr>
                <td class="title" style="word-break:keep-all">
                    <p>
                        检测内容最大值：
                    </p>
                </td>
                <td>
                    <input class="easyui-validatebox" id="testvaluemax" type="text"  data-options="required:true, validType:['length[1,20]']" style="width: 230px; height: 30px;"/>
                </td>
            </tr>
            <tr>
                <td class="title"style="word-break:keep-all">
                    <p>
                        是否包含最大值和最小值：
                    </p>
                </td>
                <td>
                    <select  class="easyui-combobox" id="com_testvalueiscontain"   data-options="required:true" style="width: 230px; height: 25px;">
                       <option value="0">包含</option>
                       <option value="1">不包含</option>
                    </select>
                </td>


            </tr>

            <tr>
                <td class="title">
                    <p>
                        取值范围单位：
                    </p>
                </td>
                <td>
                    <input  id="testvalueunit" type="text"  style="width: 230px; height: 30px;"  />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        PLC名称：
                    </p>
                </td>
                <td>
                    <input class="easyui-validatebox" id="plcname" type="text" data-options="required:true, validType:['length[1,50]']" style="width: 230px; height: 30px;"  />
                </td>
            </tr>
           <tr>
                <td class="title">
                    <p>
                        PLC数据点类型：
                    </p>
                </td>
                <td>
                    <select id="com_plcvaluetype" class="easyui-combobox" style="width: 100px; height: 25px;"
                        data-options="required:true">
                       <option value="Boolean">Boolean</option>
                       <option value="Word">Word</option>
                       <option value="String">String</option>
                       <option value="Byte">Byte</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <p>
                        PLC输出值：
                    </p>
                </td>
                <td>
                    <input  id="plcoutmultiple" type="text"  style="width: 230px; height: 30px;"  />
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
        var testid;               //要编辑的id
        var dg;      //数据表格
        //var partdg;      //部件座椅表格
        var isEdit = false;     //是否为编辑状态
        //var partIDArr = new Array();       //该零件被哪些部件座椅用到


        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });
            //新增按钮点击
            $('.topaddBtn').first().click(function () {
                isEdit = false;
                
                $('#com_testgroupid').combobox('setValue', 1);            
                $('#com_testpage').combobox('setValue', 1);           
                $('#com_testtype').combobox('setValue', 1);              
                $('#com_testcalculatetype').combobox('setValue', 1);
                $('#com_testvalueiscontain').combobox('setValue', 1);                
                $('#com_plcvaluetype').combobox('setValue', "Boolean");
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
                saveBOMTest(isEdit);
            });

            //下拉框数据加载
            //reloadcom_testgroupid();
            $('#com_testgroupid').combobox(
           {
               url: '/HttpHandlers/BOMTestGroupHandler.ashx?method=queryGroupidForBOMTest',
               //valueField: 'group_id',
               //textField: 'groupname',
               onLoadSuccess: function () {
                   var val = $(this).combobox('getData');
                   $(this).combobox('select', val[0].testgroupid);
               }
           });
            
            //编辑窗口加载
           

            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 650,
                height: 650,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
            });
       
            //数据列表加载        bom_PN partTB bom_customerPN bom_picture/image/admin/system_run.png bom_isCustomerPN  bom_weight bom_leve bom_colorid bom_materialid bom_categoryid bom_suppllerid bom_profile bom_descCH
            dg = $('#tb').datagrid({
                url: "/HttpHandlers/BOMTestHandler.ashx?method=QueryBOMTestList",           //改动的地方
                rownumbers: true,
                pagination: true,
                singleSelect: true,
                collapsible: false,
                striped: true,
                fitColumns: true,
                columns: [[
                      { field: 'test_id', title: 'ID', align: "center", width: 100, hidden: "true" },
                      { field: 'testgroupname', title: '分组名称', align: "center", width: 100 },
                      { field: 'testpage', title: '检测页', align: "center", width: 100 },
                      { field: 'testtypename', title: '检测类型', align: "center", width: 100 },
                      { field: 'testcalculatetypename', title: '计算类别', align: "center", width: 100 },
                      { field: 'testcaption', title: '检测内容', align: "center", width: 100 },
                      { field: 'testvaluemin', title: '检测内容最小值', align: "center", width: 100 },
                      { field: 'testvaluemax', title: '检测内容最大值', align: "center", width: 100 },
                      { field: 'testvalueiscontainname', title: '是否包含最大值最小值', align: "center", width: 100 },
                      { field: 'testvalueunit', title: '取值范围单位', align: "center", width: 100 },
                      { field: 'plcname', title: 'PLC名称', align: "center", width: 100 },
                      { field: 'plcvaluetype', title: 'PLC数据点类型',align: "center", width: 100 },
                      { field: 'plcoutmultiple', title: 'PLC输出值', align: "center", width: 100,},
                      { field: 'testtype', title: 'testtype', align: "center", width: 100, hidden: "true" },
                      { field: 'testcalculatetype', title: 'testcalculatetype', align: "center", width: 100, hidden: "true" },
                      { field: 'testvalueiscontain', title: 'testvalueiscontain', align: "center", width: 100, hidden: "true" },
                      { field: 'testgroupid', title: 'testgroupid', align: "center", width: 100, hidden: "true" },
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
        function saveBOMTest() {
            //                        

            var test_id = isEdit == true ? testid : 0;
            var testgroupid = $('#com_testgroupid').combo('getValue');
            var testpage = $('#com_testpage').combo('getValue');
            var testtype = $('#com_testtype').combo('getValue');
            var testcalculatetype = $('#com_testcalculatetype').combo('getValue');
            var testcaption = $('#testcaption').val();
            var testvaluemin = $('#testvaluemin').val();
            var testvaluemax = $('#testvaluemax').val();
            var testvalueiscontain = $('#com_testvalueiscontain').combo('getValue');
            var testvalueunit = $('#testvalueunit').val();
            var plcname = $('#plcname').val();
            var plcvaluetype = $('#com_plcvaluetype').combo('getValue');
            var plcoutmultiple = $('#plcoutmultiple').val();
            

            var model = {
                test_id: test_id,
                testgroupid: testgroupid,
                testpage: testpage,
                testtype: testtype,
                testcalculatetype: testcalculatetype,
                testcaption: testcaption,
                testvaluemin: testvaluemin,
                testvaluemax: testvaluemax,
                testvalueiscontain: testvalueiscontain,
                testvalueunit: testvalueunit,
                plcname: plcname,
                plcvaluetype: plcvaluetype,
                plcoutmultiple: plcoutmultiple,
                method: 'saveBOMTest'
            };
            var ispass = $("#w").form('validate');
           
            if (ispass != true)
            {
                return;
            }
            else if (ispass == true)
            {
                if (testvalueunit.length <= 5&&testvalueunit.length>0)
                {
                    if (plcoutmultiple.length <= 50 && plcoutmultiple.length > 0)
                    {
                        saveTheBOMTest(model);
                    }
                    else if (plcoutmultiple.length == 0 || plcoutmultiple == "") {
                        saveTheBOMTest(model);
                    }
                    else
                    {
                        alert('PLC输出值请输入正确长度')
                    }
                }
                else if (testvalueunit.length == 0||testvalueunit=="") {
                    if (plcoutmultiple.length <= 50 && plcoutmultiple.length > 0) {
                        saveTheBOMTest(model);
                    }
                    else if (plcoutmultiple.length == 0 || plcoutmultiple == "") {
                        saveTheBOMTest(model);
                    }
                    else {
                        alert('PLC输出值请输入正确长度')
                    }
                }
                else
                {
                    alert('取值范围单位请输入正确长度')
                }
                
            }
        }
        function saveTheBOMTest(model) {
            $.ajax({
                type: "POST",
                async: true,
                url: "/HttpHandlers/BOMTestHandler.ashx",
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
            testid = row.test_id;
            $('#com_testgroupid').combobox('setValue', row.testgroupid);
            $('#com_testpage').combobox('select', row.testpage);
            $('#com_testtype').combobox('setValue', row.testtype);
            //$('#com_testtype').combobox('setValue',1);
            $('#com_testcalculatetype').combobox('setValue', row.testcalculatetype);
            $('#testcaption').val(row.testcaption);
            $('#testvaluemin').val(row.testvaluemin);
            $('#testvaluemax').val(row.testvaluemax);
            $('#com_testvalueiscontain').combobox('setValue', row.testvalueiscontain);
            $('#testvalueunit').val(row.testvalueunit);
            $('#plcname').val(row.plcname);
            $('#com_plcvaluetype').combobox('select', row.plcvaluetype);
            $('#plcoutmultiple').val(row.plcoutmultiple);
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
                url: "/HttpHandlers/BOMTestHandler.ashx",
                data: encodeURI("test_id=" + row.test_id + "&method=DeleteBOMTest"),
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
           
        //    $('#com_testgroupid').combobox('reload', '/HttpHandlers/BOMTestGroupHandler.ashx?method=queryGroupidForBOMTest')
        //    $("#com_testgroupid").combobox("setValue", "ECU")
           
        //}
        

        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            //             
            $('#com_testgroupid').combo('clear');
            $('#com_testpage').combo('clear');
            $('#com_testtype').combo('clear');
            $('#com_testcalculatetype').combo('clear');
            $('#com_testvalueiscontain').combo('clear');
            $('#testcaption').val('');
            $('#testvaluemin').val('');
            $('#testvaluemax').val('');
            $('#testvalueiscontain').val('');
            $('#testvalueunit').val('');
            $('#plcname').val('');
            $('#com_plcvaluetype').combo('clear');
            $('#plcoutmultiple').val('');
        }
    </script>
</asp:Content>
