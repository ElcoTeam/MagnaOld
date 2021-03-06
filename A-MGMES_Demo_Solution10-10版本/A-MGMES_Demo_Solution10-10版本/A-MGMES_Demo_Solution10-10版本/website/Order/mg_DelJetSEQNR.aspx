﻿<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.master"  AutoEventWireup="true" CodeBehind="mg_DelJetSEQNR.aspx.cs" Inherits="website.Order.mg_DelJetSEQNR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/foundation.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    &nbsp;DelJet订单的顺序号 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="contenttop">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td >
                    DelJet订单的顺序号：&nbsp;
                </td>
                <td >
                    <input type ="text" id="seqnr" Width="200px" ></input>
                </td>
                <td >
                    <input type="button" class="penBtn" id="edit"  value="修改顺序号"/>
                </td>
                <td >
                    <input type="button" id="select" class="searchBtn" value="查询"/>
                </td>
            </tr>
        </table>
    </div>
   
    <!-- 数据表格  -->
    <table id="tb" title="订单序号" style="width: 99%;">
    </table>
    <script>
        $(function () {
            //查询按钮
            $('#select').bind('click', function () {
                $('#tb').datagrid('reload', {
                    method: "select",
                    seqnrnum:""
                });
            });

            //编辑按钮点击
            $('#edit').first().click(function () {
                var seq = $('#seqnr').val();
                if(seq.length <0)
                {
                    alert("请输入订单顺序号");
                    return false;
                }
                else
                {
                    $('#tb').datagrid('reload', {
                        method: "edit",
                        seqnrnum: seq,
                        });
                }
               
            });
            dg = $('#tb').datagrid({

                url: '/HttpHandlers/DelJetSEQNR.ashx',   //从远程站点请求数据的 URL。
                emptyMsg: '<span>没有找到相关记录<span>',
                queryParams:
                    {
                        method: "select",
                    },
                columns: [[ //数据表格列配置对象，查看列属性以获取更多细节
							{ field: 'SEQNR', title: '订单序号' },
                ]],

                onLoadSuccess: function (data) {
                    console.log(data);
                  
                },
            });
        });
    </script>
</asp:Content>
