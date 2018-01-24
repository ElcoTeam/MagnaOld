<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="InternetPrinters.aspx.cs" Inherits="website.SortManagent.InternetPrinters"  ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<%--<link href="/css/foundation.css" rel="stylesheet" type="text/css" />--%>
	<%--<script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>--%>
	<script>document.write(" <link href='/css/foundation.css?rnd= " + Math.random() + "' rel='stylesheet' type='text/css'>");</script>
	<%--<script src="/js/jquery-easyui-1.4.3/datagrid-dnd.js"></script>--%>
	<%--<script src="/js/jquery-easyui-1.4.3/jquery.edatagrid.js"></script>--%>
	<%--<link href="/js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/js/uploadify/jquery.uploadify.min.js"></script>--%>

	<style>
        /*功能按钮*/
	    .fbtn {            
            color: #fff;
            text-shadow: 0 -1px 0 rgba(0,0,0,0.25);
            background-color: #006dcc;
            background-repeat: repeat-x;
            border-color: #04c #04c #002a80;
            display: inline-block;
            padding: 0px 12px;
            margin-bottom: 0;
	    }
	    
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


		td.err-OK {
            text-align:center;
			height:80px;
			/*background:#4e976a;*/
		}
        /*超时*/
		td.err-1 {
            text-align:center;
			height:80px;
			background:#f9f9f9;   /*黄色*/
		}
        /*生产                   ***************/
		td.err-2 {
            text-align:center;
			height:80px;
			background:#995AB6;   /*紫色*/
		}
        /*维修*/
		td.err-3 {
            text-align:center;
			height:80px;
			background:#606CC5; /*蓝色*/
		}
        /*质量*/
		td.err-4 {
            text-align:center;
			height:80px;
			background:#FFC42E;
		}
        /*物料*/   /*灰色*/
		td.err-5 {
            text-align:center;
			height:80px;
			background:#9FB6CD;
		}
        /*急停*/
		td.err-6 {    
            text-align:center;
			height:80px;
			background:#FF0012;  /*红色*/
		}
	    #tb tbody td {
            text-align:center;
            font-size:38px;
	    }
	</style>
            <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" >
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../bootstrap/jqPaginator.js"></script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td><span class="title">手持仪配置管理</span> <%--<span class="subDesc">拖拽数据进行排序</span>--%>
                </td>
                <td style="width: 120px">
                    <a class="topaddBtn">新增配置</a>
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
    <table id="tb" title="手持仪配置管理" style="width: 99%;">
    </table>
     
       <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="整车座椅编辑">
        <table cellpadding="0" cellspacing="0">

            <tr>
                <td class="title">
                    <p>
                        手持端名称 ：
                    </p>
                </td>
                <td>
                    <input id="IName" type="text" class="text"  style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        手持端IP ：
                    </p>
                </td>
                <td>
                    <input id="PrintIP" type="text" class="text"  style="width: 230px;" />
                </td>
            </tr>         
            
             <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        手持端权限 ：
                    </p>
                </td>
                <td id="role">                   
                   <p>前排坐垫面套物料排序单;<input class="edcheckbox" id="checkbox_1" name="checkbox_1" value="前排坐垫面套" type="checkbox"/>  </p>  
                     <p>前排靠背面套物料排序单;<input class="edcheckbox" id="checkbox_2" name="checkbox_2" value="前排靠背面套" type="checkbox"/>  </p> 
                     <p>前排坐垫骨架物料排序单;<input class="edcheckbox" id="checkbox_3" name="checkbox_3" value="前排坐垫骨架" type="checkbox"/>  </p> 
                     <p>前排靠背骨架物料排序单;<input class="edcheckbox" id="checkbox_4" name="checkbox_4" value="前排靠背骨架" type="checkbox"/>  </p> 
                     <p>插单物料排序单;<input class="edcheckbox" id="checkbox_5" name="checkbox_5" value="插单物料排序单" type="checkbox"/>  </p> 
                     <p>前排线束物料排序单;<input class="edcheckbox" id="checkbox_6" name="checkbox_6" value="前排线束" type="checkbox"/>  </p> 
                     <p>大背板物料排序单;<input class="edcheckbox" id="checkbox_7" name="checkbox_7" value="前排大背板" type="checkbox"/>  </p> 
                     <p>后排40%靠背面套物料排序单;<input class="edcheckbox" id="checkbox_8" name="checkbox_8" value="后40靠背面套" type="checkbox"/>  </p> 
                     <p>后排60%靠背面套物料排序单;<input class="edcheckbox" id="checkbox_9" name="checkbox_9" value="后60靠背面套" type="checkbox"/>  </p> 
                     <p>后排坐垫面套物料排序单;<input class="edcheckbox" id="checkbox_10" name="checkbox_10" value="后坐垫坐垫面套" type="checkbox"/>  </p> 
                     <p>后排中央扶手物料排序单;<input class="edcheckbox" id="checkbox_11" name="checkbox_11" value="后60扶手" type="checkbox"/>  </p> 
                     <p>后排中央头枕物料排序单;<input class="edcheckbox" id="checkbox_12" name="checkbox_12" value="后60中头枕" type="checkbox"/>  </p> 
                     <p>后排40%侧头枕物料排序单;<input class="edcheckbox" id="checkbox_13" name="checkbox_13" value="后40侧头枕" type="checkbox"/>  </p> 
                     <p>后排60%侧头枕物料排序单;<input class="edcheckbox" id="checkbox_14" name="checkbox_14" value="后60侧头枕" type="checkbox"/>  </p>                  
                                
               </td>
            </tr>
             <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        打印机说明 ：
                    </p>
                </td>
                <td>
                    <input id="IRamark" type="text" class="text"  style="width: 230px;" />
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
       
     

        //数据列表加载
        dg = $('#tb').datagrid({
            url: "/HttpHandlers/InternetPrinters.ashx?method=queryInternetPrintersList",
            rownumbers: true,
            pagination: true,          
            singleSelect: true,
            collapsible: false,
            striped: true,
            fitColumns: true,
            nowrap:false,
            pageSize: 20,
            emptyMsg: '<span>没有找到相关记录<span>',
            columns: [[
                  //{ field: 'ck', checkbox: true },
                  { field: 'IName', title: '机器编号', width: 150, align: "center" },
                  { field: 'PrintIP', title: '机器地址', width: 120, align: "center" },
                  { field: 'IAddTime', title: '添加时间', width: 182, align: "center" },
                  { field: 'IRole', title: '打印权限', width: 600, align: "center" },
                  { field: 'IRamark', title: '打印机说明', width: 100, align: "center" },
                 
                 
            ]]
        });
        //数据列表分页
        dg.datagrid('getPager').pagination({
            pageList: [1, 5, 10, 15, 20],
            layout: ['list', 'sep', 'first', 'prev', 'sep', 'links', 'sep', 'next', 'last', 'sep', 'refresh']
        });


    </script>


    <script>

        /****************       全局变量          ***************/
        var i_id;               //要编辑的id
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


            //保存按钮
            $('#saveBtn').bind('click', function () {
                saveAllpart(isEdit);
            });

            //删除按钮
            $('.topdelBtn').first().click(function () {
                deleteInfos();
            });
            //编辑窗口加载
            $('#w').window({
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                width: 450,
                height: 350,
                footer: '#ft',
                top: 20,
                onBeforeClose: function () { clearw(); },
                onBeforeOpen: function () { $('#w').css('visibility', 'visible'); $('#ft').css('visibility', 'visible'); }
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
        function saveAllpart() {
            //        
            var IID = isEdit == true ? i_id : 0;
            //alert(SID);
            var IName = $('#IName').val();
            var PrintIP = $('#PrintIP').val();
            var IRamark = $('#IRamark').val();         
            var IRole = "";
            $(".edcheckbox").each(function (index, element) {
                var a = $(this).prop("checked");
                if (a == true) {
                    if (IRole == "") {

                        IRole = $(this).val();
                    }
                    else {
                        IRole = IRole + ";" + $(this).val();
                    }

                }
            });
            if (IRole != "") {
                IRole = IRole + ";";
            }
            //alert(SRole);
            var model = {
                IID: IID,
                IName: IName,
                PrintIP: PrintIP,
                IRole: IRole,
                IRamark: IRamark,
                method: 'saveInternetPrinters'
            };

            saveTheAllPart(model);
        }
        function saveTheAllPart(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/InternetPrinters.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                        dg.datagrid('reload');
                    }
                    else {
                        alert('保存失败');
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
            //alert(selRows.length.toString());     //点击编辑之后加载的
            if (selRows.length > 1) {
                alert('每次只能编辑一条记录，请重新选取');
                return;
            } else if (selRows.length == 0) {
                alert('请选择一条记录进行编辑');
                return;
            }

            //窗体数据初始化
            var row = selRows[0];
            i_id = row.IID;
            $('#IName').val(row.IName);
            $('#PrintIP').val(row.PrintIP);
            $('#IRamark').val(row.IRamark);
            var role = row.IRole;
            $(".edcheckbox").each(function (index, element) {

                if (role.indexOf($(this).val()) >= 0) {
                    $(this).prop("checked", true);
                }
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
            // alert(row.SID);
            $.ajax({
                url: "/HttpHandlers/InternetPrinters.ashx",
                data: encodeURI("IID=" + row.IID + "&method=deleteInternetPrinters"),
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



        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            //  $('#w').form('reset');//EasyUI自带的方法无法解决清空数据问题
            $('#IName').val('');
            $('#PrintIP').val('');
            $('#IRamark').val('');
            $("input[type='checkbox']").removeAttr("checked");
        }
    </script>
    
</asp:Content>

