<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeBehind="Materialsortprinting.aspx.cs" Inherits="website.SortManagent.Materialsortprinting"  ValidateRequest="false" %>

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
        .edcheckbox{
            width:40px;
            height:15px;
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
            <form method="get" action="~/Materialsortprinting/Index">
        车身号：<input id="csh"  name="csh" type="text" value=""/><%--<input id="csh"  name="csh" type="text" value="@Model.stmt"/>--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        时间选择：
        <input name="starttime" width="100" type="date" value="@Model.starttime" />&nbsp;至&nbsp;
        <input name="endtime" width="100" type="date" value="@Model.endtime" />
        &nbsp;&nbsp;&nbsp;
        <input type="submit" value="查询" />
    </form>
            <form method="get" action="~/Materialsortprinting/RePrintByCarID">
        车身号：<input id="carid" name="carid" type="text" value="" /><%--<input id="carid" name="carid" type="text" value="@Model.stmt" />--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        物料种类：
        <select id="selectwuliao" name="selectwuliao" style="width:200px;">
            <option value="前排-靠背面套">前排-靠背面套</option>
            <option value="前排-坐垫面套">前排-坐垫面套</option>
            <option value="前排-坐垫骨架">前排-坐垫骨架</option>
            <option value="前排-靠背骨架">前排-靠背骨架</option>
            <option value="前排-线束">前排-线束</option>
            <option value="前排-大背板">前排-大背板</option>
            <option value="后40-靠背面套">后40-靠背面套</option>
            <option value="后60-靠背面套">后60-靠背面套</option>
            <option value="后坐垫-坐垫面套">后坐垫-坐垫面套</option>
            <option value="后60-扶手">后60-扶手</option>
            <option value="后60-中头枕">后60-中头枕</option>
            <option value="后40-侧头枕">后40-侧头枕</option>
            <option value="后60-侧头枕">后60-侧头枕</option>
        </select>
        &nbsp;&nbsp;&nbsp;
        <input type="submit" value="补单打印" />
    </form>
    </div>
    
     <!-- 数据表格  -->
    <table title="手持仪配置管理" style="width: 99%;">
   
        <table class="table table-bordered table-striped">
        <tr>
            <th rowspan="2">
                序号
            </th>
            <th rowspan="2">
                车身号
            </th>
            <th rowspan="2">
                车型
            </th>
            <th colspan="2">
                <a href="Edit()" id="靠背面套" zfj="前排">前排靠背面套</a>
              
            </th>
            <th colspan="2">
                 <a href="Edit()" id="坐垫面套" zfj="前排">前排坐垫面套</a>
              
            </th>
            <th colspan="2">
                 <a href="Edit()" id="坐垫骨架" zfj="前排">前排坐盆骨架</a>
              
            </th>
            <th colspan="2">
                 <a href="Edit()" id="靠背骨架" zfj="前排">前排靠背骨架</a>
               
            </th>
            <th colspan="2">
                 <a href="Edit()" id="线束" zfj="前排">前排线束</a>
              
            </th>
            <th colspan="2">
                 <a href="Edit()" id="大背板" zfj="前排">大背板</a>
              
            </th>


            <th rowspan="2">
                 <a href="Edit()" id="靠背面套" zfj="后40">后排40%靠背面套</a>
               
            </th>
            <th rowspan="2">
                 <a href="Edit()" id="靠背面套" zfj="后60">后排60%靠背面套</a>
              
            </th>
            <th rowspan="2">
                 <a href="Edit()" id="坐垫面套" zfj="后坐垫">后排坐垫面套</a>
               
            </th>
            <th rowspan="2">
                 <a href="Edit()" id="扶手" zfj="后60">后排中央扶手</a>
               
            </th>
            <th rowspan="2">
                 <a href="Edit()" id="中头枕" zfj="后60">后排中央头枕</a>
              
            </th>
            <th rowspan="2">
                 <a href="Edit()" id="侧头枕" zfj="后40">后排40%侧头枕</a>
              
            </th>
            <th rowspan="2">
                 <a href="Edit()" id="侧头枕" zfj="后60">后排60%侧头枕</a>
              
            </th>
            <th rowspan="2">
                操作
            </th>
        </tr>
        <tr>
            <th>主驾</th>
            <th>副驾</th>
            <th>主驾</th>
            <th>副驾</th>
            <th>主驾</th>
            <th>副驾</th>
            <th>主驾</th>
            <th>副驾</th>
            <th>主驾</th>
            <th>副驾</th>
            <th>主驾</th>
            <th>副驾</th>
        </tr>
       
        <tr  >
           
              <%--  <td rowspan="2">
                @Html.ActionLink("插单打印", "CDDY", new { id = item.订单号, idlevel=item.等级 }, htmlAttributes: new { @class = "btn btn-primary" })
            </td>--%>
        </tr>
      <%--  <tr>
            <td>@para.FirstOrDefault(s=>s.Name.Equals(  "靠背面套")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("靠背面套")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("坐垫面套")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("坐垫面套")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("坐垫骨架")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("坐垫骨架")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("靠背骨架")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("靠背骨架")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("线束")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("线束")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("大背板")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("大背板")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("40靠背")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("60靠背")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("后坐垫")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("后排中央扶手")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("后排中央头枕")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("40侧头枕")).SerialID</td>
            <td>@para.FirstOrDefault(s => s.Name.Equals("60侧头枕")).SerialID</td>
        </tr>--%>
         
    </table>
        
         </table>
     <table id="tb" title="手持仪配置管理1" style="width:100%;overflow:auto;overflow-x:scroll;white-space:nowrap;"></table>
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
                    <input id="SName" type="text" class="text"  style="width: 230px;" />
                </td>
            </tr>
            <tr>
                <td class="title" style="width: 110px;">
                    <p>
                        手持端IP ：
                    </p>
                </td>
                <td>
                    <input id="ClientIP" type="text" class="text"  style="width: 230px;" />
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
            url: "/HttpHandlers/Materialsortprinting.ashx?method=queryMaterialsortprintingList",
            rownumbers: true,
            pagination: true,
            singleSelect: true,
            collapsible: false,
            striped: true,
            fitColumns: true,
            nowrap: false,
            pageSize: 20,
            columns: [[

                  { field: '序号', title: '序号', width: 152,align: "center" },
                  { field: '订单号', title: '车身号 ', width: 180, align: "center" },
                  { field: '等级', title: '车型', width: 182,  align: "center" },
                  //{ field: 'ck', checkbox: true },

                 
                  { field: '靠背面套主驾', title: '靠背面套主驾', width: 182, align: "center" },
                  { field: '靠背面套副驾', title: '靠背面套副驾', width: 182, align: "center" },
                  { field: '坐垫面套主驾', title: '坐垫面套主驾', width: 182, align: "center" },
                  { field: '坐垫面套副驾', title: '坐垫面套副驾', width: 182, align: "center" },
                  { field: '坐垫骨架主驾', title: '坐垫骨架主驾', width: 182, align: "center" },
                  { field: '坐垫骨架副驾', title: '坐垫骨架副驾', width: 182, align: "center" },
                  { field: '靠背骨架主驾', title: '靠背骨架主驾', width: 182, align: "center" },
                  { field: '靠背骨架副驾', title: '靠背骨架副驾', width: 182, align: "center" },
                  { field: '线束主驾', title: '线束主驾', width: 182, align: "center" },
                  { field: '线束副驾', title: '线束副驾', width: 182, align: "center" },
                  { field: '大背板主驾', title: '大背板主驾', width: 182, align: "center" },
                  { field: '大背板副驾', title: '大背板副驾', width: 182, align: "center" },
                  { field: '靠背40', title: '靠背40', width: 182, align: "center" },
                  { field: '靠背60', title: '靠背60', width: 182, align: "center" },
                  { field: '后坐垫', title: '后坐垫', width: 182,  align: "center" },
                  { field: '后排中央扶手', title: '后排中央扶手', width: 182, align: "center" },
                  { field: '后排中央头枕', title: '后排中央头枕', width: 182, align: "center" },
                  { field: '侧头枕40', title: '侧头枕40', width: 182, align: "center" },
                  { field: '侧头枕60', title: '侧头枕60', width: 182, align: "center" },
                  { field: 'SerialID', title: 'SerialID', width: 100, align: "center", formatter: function (value, row, index) { return '<a class="fbtn" style="height:16px;cursor:pointer" onclick="CDDY(\'' + row.订单号 + '\',\'' + row.等级 + '\');"> 插单打印</a>'; }, },


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
        var s_id;               //要编辑的id
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
            var SID = isEdit == true ? s_id : 0;
            // alert(SID);
            var SName = $('#SName').val();
            var ClientIP = $('#ClientIP').val();
            var SRole = "";
            $(".edcheckbox").each(function (index, element) {
                var a = $(this).prop("checked");
                if (a == true) {
                    if (SRole == "") {

                        SRole = $(this).val();
                    }
                    else {
                        SRole = SRole + ";" + $(this).val();
                    }

                }
            });
            if (SRole != "") {
                SRole = SRole + ";";
            }
            //alert(SRole);
            var model = {
                SID: SID,
                SName: SName,
                ClientIP: ClientIP,
                SRole: SRole,
                method: 'saveShowChiClient'
            };

            saveTheAllPart(model);
        }
        function saveTheAllPart(model) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/HttpHandlers/ShowChiClient.ashx",
                data: model,
                success: function (data) {
                    if (data == 'true') {
                        alert('已保存');
                        dg.datagrid('reload');

                        $('#w').window('close');
                    }
                    else {
                        alert('保存失败');
                    }

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
            s_id = row.SID;
            $('#SName').val(row.SName);
            $('#ClientIP').val(row.ClientIP);
            var role = row.SRole;
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
                url: "/HttpHandlers/ShowChiClient.ashx",
                data: encodeURI("SID=" + row.SID + "&method=deleteShowChiClient"),
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
            $('#SName').val('');
            $('#ClientIP').val('');
            $("input[type='checkbox']").removeAttr("checked");
        }
    </script>
    
</asp:Content>

