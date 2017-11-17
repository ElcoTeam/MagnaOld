using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;

namespace BLL
{
    public class mg_BomBLL
    {
        public static bool AddBomByName(string name, string no, int level, string desc, string pic, string material, string profile, int weight, string supplier, string category, string comments)
        {
            return mg_BomDAL.AddBomByName(name, no, level, desc, pic, material, profile, weight, supplier, category, comments) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_BomDAL.GetAllData();
        }

        public static bool UpdateBomByName(int id, string name, string no, int level, string desc, string material, string profile, int weight, string supplier, string category, string comments, string pic)
        {
            return mg_BomDAL.UpdateBomByName(id, name, no, level, desc, material, profile, weight, supplier, category, comments, pic) > 0 ? true : false;
        }

        public static bool DelBomByName(int bom_id)
        {
            return mg_BomDAL.DelBomByName(bom_id) > 0 ? true : false;
        }

        public static bool CheckBomByName(int a, int bomid, string name)
        {
            return mg_BomDAL.CheckBomByName(a, bomid, name) == 0 ? true : false;
        }

        public static DataTable GetStationID()
        {
            return mg_BomDAL.GetStationID();
        }

        public static DataTable GetBomName()
        {
            return mg_BomDAL.GetBomName();
        }

        public static bool CheckPicName(string name)
        {
            return mg_BomDAL.CheckPicName(name);
        }





        /*
         * 
       *      姜任鹏
       * 
       */


        public static string SaveBOM(mg_BOMModel model)
        {
            return model.bom_id == 0 ? AddBOM(model) : UpdateBOM(model);
        }
        private static string UpdateBOM(mg_BOMModel model)
        {
            bool flag = mg_BomDAL.UpdateBOM(model);
            return flag ? "true" : "false";
        }

        private static string AddBOM(mg_BOMModel model)
        {
            bool flag = mg_BomDAL.AddBOM(model);
            return flag ? "true" : "false";
        }

        public static string QueryBOMList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_BOMModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_BOMPageModel model = new mg_BOMPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_BOMPageModel>(model);
            return jsonStr;

        }
        private static List<mg_BOMModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_BOMModel> list = mg_BomDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_BOMModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_BOMModel> list = mg_BomDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }

        public static string DeleteBOM(string bom_id)
        {
            int count = mg_BomDAL.DeleteBOM(bom_id);
            return count > 0 ? "true" : "false";
        }

        public static string GetRelation(string key, string id)
        {
            string jsonStr = "[]";
            List<mg_treeModel> list = null;
            DataTable dt = mg_BomDAL.GetRelationTable(key, id); //取数据
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_treeModel>();

                DataView allPartdv = new DataView(dt);
                DataTable newdt = allPartdv.ToTable(true, "all_id", "all_no");

                //遍历整车
                foreach (DataRow allRow in newdt.Rows)
                {
                    string all_id = DataHelper.GetCellDataToStr(allRow, "all_id");
                    string all_no = DataHelper.GetCellDataToStr(allRow, "all_no");
                    mg_treeModel allModel = new mg_treeModel();
                    allModel.text = all_no;

                    List<mg_treeModel> partList = null;
                    //遍历部件
                    DataRow[] partRows = dt.Select("all_id=" + all_id);
                    if (partRows.Length > 0)
                    {
                        partList = new List<mg_treeModel>();
                        DataTable partDT = new DataTable();
                        DataColumn dtc = new DataColumn("part_id", typeof(Int32));
                        partDT.Columns.Add(dtc);
                        dtc = new DataColumn("part_no", typeof(string));
                        partDT.Columns.Add(dtc);

                        foreach (DataRow partRow in partRows)
                        {
                            string part_id = DataHelper.GetCellDataToStr(partRow, "part_id");
                            string part_no = DataHelper.GetCellDataToStr(partRow, "part_no");

                            DataRow dr = partDT.NewRow();
                            dr["part_id"] = part_id;
                            dr["part_no"] = part_no;
                            partDT.Rows.Add(dr);

                            mg_treeModel partModel = new mg_treeModel();
                            partModel.text = part_no;

                            List<mg_treeModel> bomList = null;
                            DataRow[] bomRows = dt.Select("all_id=" + all_id + " and part_id=" + part_id);
                            //遍历零件
                            if (bomRows.Length>0)
                            {
                                bomList = new List<mg_treeModel>();
                                foreach (DataRow bomRow in bomRows)
                                {
                                    string bom_id = DataHelper.GetCellDataToStr(bomRow, "bom_id");
                                    string bom_PN = DataHelper.GetCellDataToStr(bomRow, "bom_PN");
                                    string bom_customerPN = DataHelper.GetCellDataToStr(bomRow, "bom_customerPN");
                                    mg_treeModel bomModel = new mg_treeModel();
                                    bomModel.text = bom_PN + "  /  " + bom_customerPN;
                                    bomList.Add(bomModel);
                                }
                                partModel.children = bomList;
                            }


                            partList.Add(partModel);
                        }
                        allModel.children = partList;

                    }

                    list.Add(allModel);
                }

            }

            jsonStr = JSONTools.ScriptSerialize<List<mg_treeModel>>(list);
            return jsonStr;
        }

        public static string QueryBOMForStepEditing(string part_id)
        {
            string jsonStr = "[]";
            List<mg_BOMModel> list = mg_BomDAL.QueryBOMForStepEditing(part_id);
            jsonStr = JSONTools.ScriptSerialize<List<mg_BOMModel>>(list);
            return jsonStr;
        }
    }

    class mg_treeModel
    {
        public string text;
        public List<mg_treeModel> children;
        public string state = "open";
    }


    class mg_BOMPageModel
    {
        public string total;
        public List<mg_BOMModel> rows;
    }
}
