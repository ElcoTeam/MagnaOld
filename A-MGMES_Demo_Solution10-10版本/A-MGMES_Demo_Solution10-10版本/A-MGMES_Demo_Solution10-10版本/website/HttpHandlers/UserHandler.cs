using System;
using System.Web;
using Bll;
using Model;
using Tools;

public class UserHandler : IHttpHandler
{

    HttpRequest Request = null;
    HttpResponse Response = null;

    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";

        Request = context.Request;
        Response = context.Response;

        string method = Request.Params["method"];
        switch (method)
        {

            case "saveUser":
                saveUser();
                break;
            case "queryUserList":
                QueryUserList();
                break;
            case "deleteUser":
                DeleteUser();
                break;

            case "queryPositionsForUser":
                QueryPositionsForUser();
                break;
            case "queryDepartmentsForUser":
                QueryDepartmentsForUser();
                break;

            //case "getMenu":
            //    GetMenu();
            //    break;
            case "checkusereditpwd":
                Checkusereditpwd();
                break;

        }
    }
    void QueryPositionsForUser()
    {
        string json = mg_PositionBLL.QueryPositionsForUser();
        Response.Write(json);
        Response.End();
    }
    
    
    
    void QueryDepartmentsForUser()
    {
        string json = mg_DepartmentBLL.QueryDepartmentsForUser();
        Response.Write(json);
        Response.End();
    }



    void GetMenu()
    {
        string json = @"
                [
    {
        ""id"":""m1"",
        ""text"": ""基础档案"",
        ""children"": [
            {
                ""id"": ""m1-1"",
                ""text"": ""部门档案""
            },
            {
                ""id"": 3,
                ""text"": ""职位档案""
            },
            {
                ""id"": 4,
                ""text"": ""操作工档案""
            },
            {
                ""id"": 5,
                ""text"": ""用户权限管理""
            },
            {
               ""id"": 6,
                ""text"": ""邮件档案""
            }
        ]
    },
    {
        ""id"": 6,
        ""text"": ""部件档案"",
        ""children"": [
            {
                ""id"": 7,
                ""text"": ""整套座椅代号""
            },
            {
                ""id"": 8,
                ""text"": ""大部件座椅配置""
            },
            {
                ""id"": 9,
                ""text"": ""物料档案""
            }
        ]
    },
    {
        ""id"": 10,
        ""text"": ""生产线档案"",
        ""children"": [
            {
                ""id"": 11,
                ""text"": ""流水线档案""
            },
            {
                ""id"": 12,
                ""text"": ""工位档案""
            },
            {
                ""id"": 13,
                ""text"": ""工序步骤管理""
            },
            {
                ""id"": 14,
                ""text"": ""班次档案""
            }
        ]
    },
    {
        ""id"": 15,
        ""text"": ""生产订单管理"",
        ""children"": [
            {
                ""id"": 16,
                ""text"": ""客户订单""
            },
            {
                ""id"": 17,
                ""text"": ""生产通知单""
            }
        ]
    },
    {
        ""id"": 18,
        ""text"": ""查询统计"",
        ""children"": [
            {
                ""id"": 19,
                ""text"": ""工序步骤日志""
            },
            {
                ""id"": 20,
                ""text"": ""客户订单统计""
            },
            {
                ""id"": 21,
                ""text"": ""生产通知单统计""
            }
        ]
    }
]
                ";
        Response.Write(json);
        Response.End();
    }




    void saveUser()
    {
        string user_id = Request.Params["user_id"];
        string user_depid = Request.Params["user_depid"];
        string user_posiid = Request.Params["user_posiid"];
        string user_no = Request.Params["user_no"];
        string user_name = Request.Params["user_name"];
        string user_email = Request.Params["user_email"];
        string user_sex = Request.Params["user_sex"];
        string user_isAdmin = Request.Params["user_isAdmin"];
        string user_menuids = Request.Params["user_menuids"];
        string user_pwd = Request.Params["user_pwd"];
        string user_oldno = Request.Params["user_oldno"];

        mg_userModel model = new mg_userModel();
        model.user_id = user_id;
        model.user_depid = NumericParse.StringToInt(user_depid);
        model.user_posiid = NumericParse.StringToInt(user_posiid);
        model.user_no = user_no;
        model.user_oldno = user_oldno;
        model.user_name = user_name;
        model.user_pwd = user_pwd;
        model.user_email = user_email;
        model.user_sex = NumericParse.StringToInt(user_sex);
        model.user_isAdmin = NumericParse.StringToInt(user_isAdmin);
        model.user_menuids = user_menuids;
        string json = mg_UserBLL.saveUser(model);
        Response.Write(json);
        Response.End();
    }

    void DeleteUser()
    {
        string user_no = Request.Params["user_no"];

        string json = mg_UserBLL.DeleteUser(user_no);
        Response.Write(json);
        Response.End();
    }

    void QueryUserList()
    {
        string page = Request.Params["page"];
        string pagesize = Request.Params["rows"];

        if (string.IsNullOrEmpty(page))
        {
            page = "1";
        }
        if (string.IsNullOrEmpty(pagesize))
        {
            pagesize = "20";
        }
        string json = mg_UserBLL.QueryUserList(page, pagesize);
        Response.Write(json);
        Response.End();
    }

    void Checkusereditpwd()
    {
        string currentuser = Request.Params["currentuser"];
        string json = mg_UserBLL.CheckUserPwd(currentuser);
        Response.Write(json);
        Response.End();
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}