<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="MaterialSorting.aspx.cs" Inherits="website.SortManagent.MaterialSortingindex"  ValidateRequest="false" %>

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
                <%--<td><span class="title">物料排序单参数配置</span> <span class="subDesc">拖拽数据进行排序</span>
                </td>--%>
              <%--  <td style="width: 120px">
                    <a class="topaddBtn">新增档案</a>
                </td>
                <td style="width: 120px">
                    <a class="toppenBtn">编辑所选</a>
                </td>
                <td style="width: 120px">
                    <a class="topdelBtn">删除所选</a>
                </td>--%>
            </tr>
        </table>
    </div>
    
     <!-- 数据表格  -->
    <table id="tb" title="物料排序单参数配置" style="width: 99%;">
    </table>
     
       <!-- 编辑窗口 -->
    <div id="w" style="padding: 10px; visibility: hidden" title="整车座椅编辑">
        <table cellpadding="0" cellspacing="0">

            <tr>
                <td class="title">
                    <p>
                        物料货架名称 ：
                    </p>
                </td>
                <td>
                    <input id="Name" type="text" class="text" readonly="readonly" style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        摆放数量 ：
                    </p>
                </td>
                <td>
                    <input id="Number" type="text" class="text"  style="width: 230px;" />
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
            url: "/HttpHandlers/MaterialSorting.ashx?method=queryPanrameterList",
            rownumbers: true,
            pagination: true,          
            singleSelect: true,
            collapsible: false,
            striped: true,
            fitColumns: true,
            pageSize: 20,
            emptyMsg: '<span>没有找到相关记录<span>',
            columns: [[
                  //{ field: 'ck', checkbox: true },
                  { field: 'SerialID', title: '序号', width: 100, align: "center" },                  
                  { field: 'Name', title: '物料名称', width: 100, align: "center" },
                  { field: 'Number', title: '摆放数量', width: 100, align: "center" },
                  { field: 'cz', title: '操作', width: 100, align: "center", formatter: function (value, row, index) { return '<a class="fbtn" style="height:16px;cursor:pointer" onclick="edit(\'' + row.SerialID + '\',\'' + row.Name + '\',\'' + row.Number + '\');">编辑</a>'; }, },
                  { field: 'tz', title: '调整顺序', width: 100, align: "center", formatter: function (value, row, index) { return '<a class="fbtn" style="height:16px;cursor:pointer" onclick="up(\'' + row.SerialID + '\',\'' + row.Name + '\');">上升</a>&nbsp;&nbsp;<a class="fbtn" style="height:16px;cursor:pointer" onclick="down(\'' + row.SerialID + '\',\'' + row.Name + '\');">下降</a>'; }, },
                  { field: 'IsAutoSend', title: '自动下发', width: 100, align: "center", formatter: function (value, row, index) { return '<a class="fbtn" style="height:16px;cursor:pointer" onclick="send(\'' + row.SerialID + '\',\'' + row.Name + '\');"> ' + row.IsAutoSend1 + '</a>'; }, },
                  { field: 'IsAutoPrint', title: '自动打印', width: 100, align: "center", formatter: function (value, row, index) { return '<a class="fbtn" style="height:16px;cursor:pointer" onclick="print(\'' + row.SerialID + '\',\'' + row.Name + '\');">' + row.IsAutoPrint1 + '</a>'; }, },
                  { field: 'Ascordesc', title: '是否倒序打印', width: 100, align: "center", formatter: function (value, row, index) { return '<a class="fbtn" style="height:16px;cursor:pointer" onclick="ascdesc(\'' + row.SerialID + '\',\'' + row.Name + '\');">' + row.Ascordesc1 + '</a>'; }, },
                 
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
        var Serial_ID;               //要编辑的id
        var dg = $('#tb');      //表格
        var isEdit = false;     //是否为编辑状态
        var tmpNO;              //工号


        /****************       DOM加载          ***************/
        $(function () {
            $.ajaxSetup({
                cache: false //关闭AJAX缓存
            });        

            //保存按钮
            $('#saveBtn').bind('click', function () {
                saveMaterialSorting(isEdit);
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
        function saveMaterialSorting() {
            //        
            var SerialID = isEdit == true ? Serial_ID : 0;
           // alert(SerialID);

            var Name = $('#Name').val();
            var Number = $('#Number').val();          

            var model = {
                SerialID:SerialID,
                Name: Name,
                Number: Number,              
                method: 'savePanrameter'
            };

            saveTheAllPart(model);
        }
        function saveTheAllPart(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/MaterialSorting.ashx",
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
      //  function initEidtWidget(id,name,num) {

           // var selRows = dg.datagrid('getSelections');
            //alert(selRows.length.toString());     //点击编辑之后加载的
            //if (id =="") {
            //    alert('每次只能编辑一条记录，请重新选取');
            //    return;
            //} else if (selRows.length == 0) {
            //    alert('请选择一条记录进行编辑');
            //    return;
            //}

            //窗体数据初始化
          //  var row = selRows[0];
          //  SerialID = id;
           // alert(id + name + num);
          //  $('#Name').val(name);
          //  $('#Number').val(num);
            //$('#w').window('open');
       // }

        //删除所选数据，目前不需要此功能
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
                url: "/HttpHandlers/MaterialSortingHandler.ashx",
                data: encodeURI("SerialID=" + row.SerialID + "&method=deletePanrameter"),
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

      
        //上调数据
        function up(id,name) {
            var postdata = {
                id:id,
                name: name
            }

            $.post("/HttpHandlers/MaterialSorting.ashx?method=UpPanrameter", postdata, function (data) {               
                alert(data);               
                dg.datagrid('reload');
            })
        }
        //下调数据
        function down(id, name) {
            var postdata = {
                id: id,
                name: name
            }

            $.post("/HttpHandlers/MaterialSorting.ashx?method=DownPanrameter", postdata, function (data) {               
                alert(data);               
                dg.datagrid('reload');
            })
        }
        //编辑按钮点击
        function edit(id,name,num) {          
            isEdit = true;
            Serial_ID = id;
            $('#Name').val(name);
            $('#Number').val(num);
            $('#w').window('open');

        }
        //自动下发
        function send(id,name) {
            var postdata = {
                id: id,
                name:name
            }
            $.post("/HttpHandlers/MaterialSorting.ashx?method=sendPanrameter", postdata, function (data) {
                alert(data);
                dg.datagrid('reload');
                //parent.location.reload();
            })
        }
        //自动打印
        function print(id,name) {
            var postdata = {
                id: id,
                name:name
            }
            $.post("/HttpHandlers/MaterialSorting.ashx?method=printPanrameter", postdata, function (data) {
                alert(data);
                dg.datagrid('reload');
            })
        }
        //是否倒序打印
        function ascdesc(id,name) {
            var postdata = {
                id: id,
                name:name
            }
            $.post("/HttpHandlers/MaterialSorting.ashx?method=ascdescPanrameter", postdata, function (data) {
                alert(data);
                dg.datagrid('reload');
            })
        }
       
       
        /**********************************************/
        /*****************   窗体程序 *******************/
        /**********************************************/

        //编辑窗口关闭清空数据
        function clearw() {
            $('#Name').val('');
            $('#Number').val('');
        }
    </script>
    
</asp:Content>

