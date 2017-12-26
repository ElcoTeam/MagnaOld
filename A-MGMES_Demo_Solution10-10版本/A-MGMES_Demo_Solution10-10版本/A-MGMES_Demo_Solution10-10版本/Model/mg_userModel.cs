using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_userModel
    {
        public mg_userModel()
        { }
        #region Model
        private string _user_id;
        private string _user_pwd;
        private string _user_name;
        private string _user_no;
        private string _user_pic;
        private string _posi_id;
        private string _user_email;
        private int? _user_depid;
        private int? _user_posiid;
        private string _user_menuids;
        private int? _user_sex;
        private int? _user_isadmin;
        private string _machineno;
        private int? _gongwei;
        private string _st_no;
        private string _user_NewPassword;
        private string _active_flag;
        public string user_NewPassword
        {
            get { return _user_NewPassword; }
            set { _user_NewPassword = value; }
        }
        public string st_no
        {
            get { return _st_no; }
            set { _st_no = value; }
        }

        public int? gongwei
        {
            get { return _gongwei; }
            set { _gongwei = value; }
        }


        public string machineNO
        {
            get { return _machineno; }
            set { _machineno = value; }
        }


        private string _user_depid_name;

        public string user_depid_name
        {
            get { return _user_depid_name; }
            set { _user_depid_name = value; }
        }
        private string _user_posiid_name;

        public string user_posiid_name
        {
            get { return _user_posiid_name; }
            set { _user_posiid_name = value; }
        }

        private string _user_sex_name;
        public string user_sex_name
        {
            get { return _user_sex_name; }
            set { _user_sex_name = value; }
        }
        private string _user_isAdmin_name;

        public string user_isAdmin_name
        {
            get { return _user_isAdmin_name; }
            set { _user_isAdmin_name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string user_pwd
        {
            set { _user_pwd = value; }
            get { return _user_pwd; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 用户号
        /// </summary>
        public string user_no
        {
            set { _user_no = value; }
            get { return _user_no; }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string user_pic
        {
            set { _user_pic = value; }
            get { return _user_pic; }
        }
        /// <summary>
        /// 工位号
        /// </summary>
        public string posi_id
        {
            set { _posi_id = value; }
            get { return _posi_id; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string user_email
        {
            set { _user_email = value; }
            get { return _user_email; }
        }
        /// <summary>
        /// 所属部门id
        /// </summary>
        public int? user_depid
        {
            set { _user_depid = value; }
            get { return _user_depid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? user_posiid
        {
            set { _user_posiid = value; }
            get { return _user_posiid; }
        }
        /// <summary>
        /// 菜单可见权限
        /// </summary>
        public string user_menuids
        {
            set { _user_menuids = value; }
            get { return _user_menuids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? user_sex
        {
            set { _user_sex = value; }
            get { return _user_sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? user_isAdmin
        {
            set { _user_isadmin = value; }
            get { return _user_isadmin; }
        }

        public string active_flag
        {
            set { _active_flag = value; }
            get { return _active_flag; }
        }
        #endregion Model
    }
}
