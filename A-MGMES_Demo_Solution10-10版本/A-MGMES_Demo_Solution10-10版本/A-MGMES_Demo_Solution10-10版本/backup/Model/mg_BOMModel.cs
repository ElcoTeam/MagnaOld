using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_BOMModel
    {
        public mg_BOMModel()
        { }
        #region Model
        private int? _bom_id;
        private string _bom_pn;
        private string _bom_customerpn;
        private int? _bom_iscustomerpn;
        private int? _bom_leve;
        private int? _bom_materialid;
        private string _bom_material;
        private int? _bom_suppllerid;
        private string _bom_suppller;
        private int? _bom_categoryid;
        private string _bom_category;
        private int? _bom_colorid;
        private string _bom_colorname;
        private string _bom_profile;
        private int? _bom_weight;
        private string _bom_desc;
        private string _bom_descch;
        private string _bom_picture;
        private string _bom_rescancode;

        private int _bom_storeid;
        private string _bom_storeName;

        public string bom_storeName
        {
            get { return _bom_storeName; }
            set { _bom_storeName = value; }
        }

        public int bom_storeid
        {
            get { return _bom_storeid; }
            set { _bom_storeid = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_id
        {
            set { _bom_id = value; }
            get { return _bom_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_PN
        {
            set { _bom_pn = value; }
            get { return _bom_pn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_customerPN
        {
            set { _bom_customerpn = value; }
            get { return _bom_customerpn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_isCustomerPN
        {
            set { _bom_iscustomerpn = value; }
            get { return _bom_iscustomerpn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_leve
        {
            set { _bom_leve = value; }
            get { return _bom_leve; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_materialid
        {
            set { _bom_materialid = value; }
            get { return _bom_materialid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_material
        {
            set { _bom_material = value; }
            get { return _bom_material; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_suppllerid
        {
            set { _bom_suppllerid = value; }
            get { return _bom_suppllerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_suppller
        {
            set { _bom_suppller = value; }
            get { return _bom_suppller; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_categoryid
        {
            set { _bom_categoryid = value; }
            get { return _bom_categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_category
        {
            set { _bom_category = value; }
            get { return _bom_category; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_colorid
        {
            set { _bom_colorid = value; }
            get { return _bom_colorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_colorname
        {
            set { _bom_colorname = value; }
            get { return _bom_colorname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_profile
        {
            set { _bom_profile = value; }
            get { return _bom_profile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_weight
        {
            set { _bom_weight = value; }
            get { return _bom_weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_desc
        {
            set { _bom_desc = value; }
            get { return _bom_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_descCH
        {
            set { _bom_descch = value; }
            get { return _bom_descch; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_picture
        {
            set { _bom_picture = value; }
            get { return _bom_picture; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bom_rescanCode
        {
            set { _bom_rescancode = value; }
            get { return _bom_rescancode; }
        }


        private string _partIDs;

        public string partIDs
        {
            get { return _partIDs; }
            set { _partIDs = value; }
        }


        private string _bom_isCustomerPNName;

        public string bom_isCustomerPNName
        {
            get { return _bom_isCustomerPNName; }
            set { _bom_isCustomerPNName = value; }
        }

        private string _partNOs;

        public string partNOs
        {
            get { return _partNOs; }
            set { _partNOs = value; }
        }


        private string _bom_leveName;

        public string bom_leveName
        {
            get { return _bom_leveName; }
            set { _bom_leveName = value; }
        }
        #endregion Model


    }
}
