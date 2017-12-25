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
     
  
  <script src="~/FELib/Jquery-plugins/jquery.printElement.js"></script>
<script language="javascript" type="text/javascript" src="~/My97DatePickerBeta/My97DatePicker/WdatePicker.js"></script>
<div class="widget-content" style="overflow-x:scroll;">
    <h2>物料排序单打印管理</h2>
    <hr />

    <form method="get" action="~/Materialsortprinting/Index">
        车身号：<input id="csh"  name="csh" type="text" value="@Model.stmt"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        时间选择：
        <input name="starttime" width="100" type="date" class='Wdate' onfocus="WdatePicker({lang:'zh-cn'})" value="" />&nbsp;至&nbsp;
        <input name="endtime" width="100" type="date" class='Wdate' onfocus="WdatePicker({lang:'zh-cn'})" value="" />
        &nbsp;&nbsp;&nbsp;
        <input type="submit" value="查询" />
    </form>
    <form method="get" action="~/Materialsortprinting/RePrintByCarID">
        车身号：<input id="carid" name="carid" type="text" value="" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
  
    <p>
      <a class="btn btn-primary" href="/Materialsortprinting/Create?Length=0">插入</a>
       <!--<a href="javascript:reload()" class="btn btn-primary">点了有惊喜！！！！！！！！！！！！！！！！！！！！！！</a>--> 
      <a class="btn btn-primary" href="/Materialsortprinting">刷新</a>
    </p>

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
               <a href="/Materialsortprinting/Edit/%E9%9D%A0%E8%83%8C%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">前排靠背面套</a>
            </th>
            <th colspan="2">
               <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">前排坐垫面套</a>
            </th>
            <th colspan="2">
                <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">前排坐盆骨架</a>
             
            </th>
            <th colspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">前排靠背骨架</a>
              
            </th>
            <th colspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">前排线束</a>
              
            </th>
            <th colspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">大背板</a>
              
            </th>


            <th rowspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">后排40%靠背面套</a>
             
            </th>
            <th rowspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">后排60%靠背面套</a>
              
            </th>
            <th rowspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">后排坐垫面套</a>
               
            </th>
            <th rowspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">后排中央扶手</a>
               
            </th>
            <th rowspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">后排中央头枕</a>
                
            </th>
            <th rowspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">后排40%侧头枕</a>
              
            </th>
            <th rowspan="2">
                   <a href="/Materialsortprinting/Edit/%E5%9D%90%E5%9E%AB%E9%9D%A2%E5%A5%97?zfj=%E5%89%8D%E6%8E%92">后排60%侧头枕</a>
              
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
        @foreach (var item in Model.pro)
            {
        <tr>
            <td rowspan="2">(@item.序号)</td>
            <td rowspan="2">@item.订单号</td>
            <td rowspan="2">@item.等级</td>
            <td style="@(item.靠背面套主驾下发)" class="isp@(item.靠背面套主驾打印)">@item.靠背面套主驾</td>
            <td style="@(item.靠背面套副驾下发)" class="isp@(item.靠背面套副驾打印)">@item.靠背面套副驾</td>
            <td style="@(item.坐垫面套主驾下发)" class="isp@(item.坐垫面套主驾打印)">@item.坐垫面套主驾</td>
            <td style="@(item.坐垫面套副驾下发)" class="isp@(item.坐垫面套副驾打印)">@item.坐垫面套副驾</td>
            <td style="@(item.坐垫骨架主驾下发)" class="isp@(item.坐垫骨架主驾打印)">@item.坐垫骨架主驾</td>
            <td style="@(item.坐垫骨架副驾下发)" class="isp@(item.坐垫骨架副驾打印)">@item.坐垫骨架副驾</td>
            <td style="@(item.靠背骨架主驾下发)" class="isp@(item.靠背骨架主驾打印)">@item.靠背骨架主驾</td>
            <td style="@(item.靠背骨架副驾下发)" class="isp@(item.靠背骨架副驾打印)">@item.靠背骨架副驾</td>
            <td style="@(item.线束主驾下发)" class="isp@(item.线束主驾打印)">@item.线束主驾    </td>
            <td style="@(item.线束副驾下发)" class="isp@(item.线束副驾打印)">@item.线束副驾    </td>
            <td style="@(item.大背板主驾下发)" class="isp@(item.大背板主驾打印)">@item.大背板主驾  </td>
            <td style="@(item.大背板副驾下发)" class="isp@(item.大背板副驾打印)">@item.大背板副驾  </td>
            <td style="@(item.靠背40下发)" class="isp@(item.靠背40打印)">@item.靠背40       </td>
            <td style="@(item.靠背60下发)" class="isp@(item.靠背60打印)">@item.靠背60       </td>
            <td style="@(item.后坐垫下发)" class="isp@(item.后坐垫打印)">@item.后坐垫       </td>
            <td style="@(item.后排中央扶手下发)" class="isp@(item.后排中央扶手打印)">@item.后排中央扶手 </td>
            <td style="@(item.后排中央头枕下发)" class="isp@(item.后排中央头枕打印)">@item.后排中央头枕 </td>
            <td style="@(item.侧头枕40下发)" class="isp@(item.侧头枕40打印)">@item.侧头枕40     </td>
            <td style="@(item.侧头枕60下发)" class="isp@(item.侧头枕60打印)">@item.侧头枕60     </td>
            <td rowspan="2">
                @Html.ActionLink("插单打印", "CDDY", new { id = item.订单号, idlevel=item.等级 }, htmlAttributes: new { @class = "btn btn-primary" })
            </td>
        </tr>
        <tr>
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
        </tr>
            }
    </table>
    <div class="pager" style="float:right">
        @Html.PageLink(Model.PagingInfo, s => Url.Action("Index", new { Model.stmt, Model.starttime ,Model.endtime,Page = s })) &nbsp; &nbsp;
    </div>
</div>

    <script>
        function printpdf() {
            $(element).find('#js-print').printElement();
        }
        function reload() {
            while (true) {
                alert("heheda");
            }
        }
        function GetList(Delorgantaxt) {
            var postdata = {
                PageSize: Delorgantaxt
            }

            $.post("/MaterialSortprinting/GetList", postdata, function (data) {
                alert(data);
                parent.location.reload();
            })
        }
        function up(Delorgantaxt) {
            var postdata = {
                id: Delorgantaxt
            }

            $.post("/MaterialSorting/up", postdata, function (data) {
                alert(data);
                parent.location.reload();
            })
        }
    </script>

    
</asp:Content>

