using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace Model
{
    public class SPListViewModel
    {
        public List<GetSP> pro { get; set; }
        public List<mg_Product> procar { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string stmt { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
    }
 
    public class IndexListViewModel
    {
        public List<GetIndex2> pro { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }

    public class IndexcdViewModel
    {
        public List<CDPXModel> pro { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }

    public class ShowListWLModel
    {
        public List<GetIndex> pro { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
    public class GetSP
    {
        public string 序号 { get; set; }
        public string 订单号 { get; set; }
        public string 等级 { get; set; }
        public string 靠背面套主驾 { get; set; }
        public string 靠背面套副驾 { get; set; }
        public string 坐垫面套主驾 { get; set; }
        public string 坐垫面套副驾 { get; set; }
        public string 坐垫骨架主驾 { get; set; }
        public string 坐垫骨架副驾 { get; set; }
        public string 靠背骨架主驾 { get; set; }
        public string 靠背骨架副驾 { get; set; }
        public string 线束主驾 { get; set; }
        public string 线束副驾 { get; set; }
        public string 大背板主驾 { get; set; }
        public string 大背板副驾 { get; set; }
        public string 靠背40 { get; set; }
        public string 靠背60 { get; set; }
        public string 后坐垫 { get; set; }
        public string 后排中央扶手 { get; set; }
        public string 后排中央头枕 { get; set; }
        public string 侧头枕40 { get; set; }
        public string 侧头枕60 { get; set; }
        public int 靠背面套主驾打印 { get; set; }
        public int 靠背面套副驾打印 { get; set; }
        public int 坐垫面套主驾打印 { get; set; }
        public int 坐垫面套副驾打印 { get; set; }
        public int 坐垫骨架主驾打印 { get; set; }
        public int 坐垫骨架副驾打印 { get; set; }
        public int 靠背骨架主驾打印 { get; set; }
        public int 靠背骨架副驾打印 { get; set; }
        public int 线束主驾打印 { get; set; }
        public int 线束副驾打印 { get; set; }
        public int 大背板主驾打印 { get; set; }
        public int 大背板副驾打印 { get; set; }
        public int 靠背40打印 { get; set; }
        public int 靠背60打印 { get; set; }
        public int 后坐垫打印 { get; set; }
        public int 后排中央扶手打印 { get; set; }
        public int 后排中央头枕打印 { get; set; }
        public int 侧头枕40打印 { get; set; }
        public int 侧头枕60打印 { get; set; }
        //张天博
        public string 靠背面套主驾下发 { get; set; }
        public string 靠背面套副驾下发 { get; set; }
        public string 坐垫面套主驾下发 { get; set; }
        public string 坐垫面套副驾下发 { get; set; }
        public string 坐垫骨架主驾下发 { get; set; }
        public string 坐垫骨架副驾下发 { get; set; }
        public string 靠背骨架主驾下发 { get; set; }
        public string 靠背骨架副驾下发 { get; set; }
        public string 线束主驾下发 { get; set; }
        public string 线束副驾下发 { get; set; }
        public string 大背板主驾下发 { get; set; }
        public string 大背板副驾下发 { get; set; }
        public string 靠背40下发 { get; set; }
        public string 靠背60下发 { get; set; }
        public string 后坐垫下发 { get; set; }
        public string 后排中央扶手下发 { get; set; }
        public string 后排中央头枕下发 { get; set; }
        public string 侧头枕40下发 { get; set; }
        public string 侧头枕60下发 { get; set; }
    }
    public class PagingInfo
    {
        public int TotalItem { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get
            { return (int)Math.Ceiling((decimal)TotalItem / ItemPerPage); }
        }
    }
    public class View_px_AllList_DoHis
    {
        public string IsPrint { get; set; }
        public int? OrderIsHistory { get; set; }
        public string 主副驾 { get; set; }
        public string 车身号 { get; set; }
        public string 零件号描述 { get; set; }
        public string abb { get; set; }
        public string resultljh { get; set; }

    }
    public class mg_CustomerOrder_3_CreateTime
    {
        public int id { get; set; }
        public int PartOrderID { get; set; }
        public string 订单号 { get; set; }
        public string 车身号 { get; set; }

        public string IsPrint { get; set; }
        public string CreateTime { get; set; }

    }

    public class mg_PartOrde
    {
        public string id { get; set; }
        public string OrderIsPrintSYS { get; set; }

    }
    public class mg_Product
    {
        public int id { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }

        public int ProductType { get; set; }
        public string ProductPrintID { get; set; }

    }

    public class GetProductPrintID
    {
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public string ProductPrintID { get; set; }
    }
    public class GetpxPrintAll
    {
        public int? ID { get; set; }
        public string orderid { get; set; }
        public string XF { get; set; }
        public string ordername { get; set; }
        public string IsSendOk { get; set; }
    }
    public class GetpxPrint
    {
        public int? ID { get; set; }
        public string PXID { get; set; }
        public string IsSendOk { get; set; }
    }
    public class GetIndex
    {
        public int? mg_partorder_ordertype { get; set; }
        public int? ID { get; set; }
        public int? PartOrderID { get; set; }
        public string 订单号 { get; set; }
        public string 车身号 { get; set; }
        public string 等级 { get; set; }
        public string 零件号 { get; set; }
        public string 零件号描述 { get; set; }

        public string 大部件号 { get; set; }
        public string 主副驾 { get; set; }
        public Nullable<DateTime> co_starttime { get; set; }
        public Nullable<DateTime> co_endtime { get; set; }
        public string IsPrint { get; set; }

    }
    public class CDPXModel
    {
        public int 排序 { get; set; }
        public string 订单号 { get; set; }
        public string 等级 { get; set; }
        public string 零件号 { get; set; }
        public string 零件号描述 { get; set; }
        public string 大部件号 { get; set; }
        public string 主副驾 { get; set; }
        public Nullable<DateTime> co_starttime { get; set; }
        public Nullable<DateTime> co_endtime { get; set; }
        public string IsPrint { get; set; }

    }
    public class GetIndex2
    {
        public string 订单号 { get; set; }
        public string 等级 { get; set; }
        public string 零件号 { get; set; }
        public string 零件号描述 { get; set; }
        public string 大部件号 { get; set; }
        public string 主副驾 { get; set; }
        public string IsPrint { get; set; }

    }
    public class ShowListWL
    {
        public string 订单号 { get; set; }
        public string 等级 { get; set; }
        public string 零件号 { get; set; }
        public string 零件号描述 { get; set; }
        public string 大部件号 { get; set; }
        public string 主副驾 { get; set; }

    }
    //public class OrderList
    //{
    //    public string SortName { get; set; }
    //    public bool IsAutoSend { get; set; }
    //    public bool IsAutoPrint { get; set; }

    //    public bool Ascordesc { get; set; }
    //    public int wlprintindex { get; set; }
    //}
    public class getALLID
    {
        public int all_id { get; set; }
    }
    public class pm1
    {
        public string 车身号 { get; set; }//往自定义表里添加列头
        public string 车型 { get; set; }
        public string 零件号 { get; set; }
        public string 数量 { get; set; }
    }
    public class pm2
    {
        public string 车身号 { get; set; }//往自定义表里添加列头
        public string 车型 { get; set; }
        public string 主副驾 { get; set; }
        public string 零件号 { get; set; }
        public string 数量 { get; set; }
    }
    public class pmcd
    {
        public string 序号 { get; set; }//往自定义表里添加列头
        public string 物料名称 { get; set; }
        public string 零件号 { get; set; }
        public string 数量 { get; set; }
    }
    public class insertmodel
    {
        public string csh { get; set; }
        public string cx { get; set; }
        public string qkgz { get; set; }
        public string qkgf { get; set; }
        public string qxz { get; set; }
        public string qxf { get; set; }
        public string qzmz { get; set; }
        public string qzmf { get; set; }
        public string qzgz { get; set; }
        public string qzgf { get; set; }
        public string qkmz { get; set; }
        public string qkmf { get; set; }
        public string hzm { get; set; }
        public string h4km { get; set; }
        public string h6km { get; set; }
        public string dbbz { get; set; }
        public string dbbf { get; set; }
        public string hzf { get; set; }
        public string hzt { get; set; }
        public string h4c { get; set; }
        public string h6c { get; set; }
    }
    public class Createmodel
    {
        public string name { get; set; }
        public string value { get; set; }
        public string tablename { get; set; }
    }
    public class result
    {
        public bool isok { get; set; }
        public JObject serial { get; set; }
    }
 
    public class PT
    {
        public string PXID { get; set; }
        public DateTime? Time { get; set; }
    }
    public class ISPRINTMODEL
    {
        public string[] Title { get; set; }
        public List<PT> GetIndexes { get; set; }
    }
    public class ISPRINTMODELDI
    {
        public string[] Title { get; set; }
        public List<px_PrintModel> GetIndexes { get; set; }
    }


    public class CDDYMODEL
    {
        public string erweima { get; set; }
        public string[] Title { get; set; }

        //public List<GetIndex> GetIndexes { get; set; }

        public IndexcdViewModel GetIndexes { get; set; }

        public string ViewName { get; set; }

        public string idlevel { get; set; }
        public string orderno { get; set; }

    }



    public class WJListModelView
    {
        public string erweima { get; set; }
        public string erweima_abc { get; set; }
        public string[] Title { get; set; }

        public ShowListWLModel ShowListWL { get; set; }

        public string zfj { get; set; }


        public string wlname { get; set; }


    }
    public class CDDYPRINTTABLE
    {
        public string 序号 { get; set; }
        public string 物料货架名称 { get; set; }
        public string 对应物料类型 { get; set; }
        public string 数量 { get; set; }
    }
    public class mg_part_bom_rel
    {
        public Nullable<int> part_id { get; set; }
        public Nullable<int> bom_id { get; set; }
        public Nullable<int> bom_count { get; set; }
    }
    public class mg_part
    {
        public Nullable<int> part_id { get; set; }
        public string part_no { get; set; }
        public string part_name { get; set; }
        public string part_desc { get; set; }
        public Nullable<int> part_categoryid { get; set; }
        public string part_Category { get; set; }
    }
    public class MG_BOM
    {
        public Nullable<int> bom_id { get; set; }
        public string bom_PN { get; set; }
        public string bom_customerPN { get; set; }
        public Nullable<int> bom_isCustomerPN { get; set; }
        public Nullable<int> bom_leve { get; set; }
        public Nullable<int> bom_materialid { get; set; }
        public string bom_material { get; set; }
        public Nullable<int> bom_suppllerid { get; set; }
        public string bom_suppller { get; set; }
        public Nullable<int> bom_categoryid { get; set; }
        public string bom_category { get; set; }
        public Nullable<int> bom_colorid { get; set; }
        public string bom_colorname { get; set; }
        public string bom_profile { get; set; }
        public Nullable<int> bom_weight { get; set; }
        public string bom_desc { get; set; }
        public string bom_descCH { get; set; }
        public string bom_picture { get; set; }
        public string bom_rescanCode { get; set; }
        public Nullable<int> bom_storeid { get; set; }
        public string bom_storeName { get; set; }
        public string bom_name { get; set; }
    }
    public class QUERYMODEL
    {
        public DateTime? starttime { get; set; }
        public DateTime? endtime { get; set; }
        public List<List<string>> Listmodel { get; set; }
        public string csh { get; set; }

    }
    public class PrintModelNZF
    {
        public string 序号 { get; set; }
        public string 车身号 { get; set; }
        public string 车型 { get; set; }
        public string 零件号 { get; set; }
        public string 数量 { get; set; }
    }
    public class PrintModel
    {
        public int 序号 { get; set; }
        public string 车身号 { get; set; }
        public string 车型 { get; set; }
        public string 主副驾 { get; set; }
        public string 零件号 { get; set; }
        public string 数量 { get; set; }
    }
    public class classess
    {

        public int cl_id { get; set; }
        public string cl_name { get; set; }

        public DateTime cl_starttime { get; set; }

        public DateTime cl_endtime { get; set; }

        public Nullable<int> st_id { get; set; }
    }
}
