using System;
using System.Web;
using Bll;
using Model;
using Tools;

public class StepHandler : IHttpHandler
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


			case "queryODSByStepid":
				QueryODSByStepid();
				break;
			case "saveStep":
				SaveStep();
				break;

			case "queryStepList":
				QueryStepList();
				break;

			case "deleteStep":
				DeleteStep();
				break;

			case "sorting":
				Sorting();
				break;
		}
	}
	void QueryODSByStepid()
	{
		string step_id = Request.Params["step_id"];


		string json = mg_StepBLL.QueryODSByStepid(step_id);
		Response.Write(json);
		Response.End();
	}



	void Sorting()
	{
		string step_id = Request.Params["step_id"];
		string step_order = Request.Params["step_order"];
		string point = Request.Params["point"];

		string json = mg_StepBLL.Sorting(step_id, step_order, point);
		Response.Write(json);
		Response.End();
	}


	void DeleteStep()
	{
		string step_id = Request.Params["step_id"];

		string json = mg_StepBLL.DeleteStep(step_id);
		Response.Write(json);
		Response.End();
	}

	void QueryStepList()
	{
		string fl_id = Request.Params["fl_id"];
		string st_id = Request.Params["st_id"];
		string part_id = Request.Params["part_id"];
		string page = Request.Params["page"];
		string pagesize = Request.Params["rows"];
		if (string.IsNullOrEmpty(page))
		{
			page = "1";
		}
		if (string.IsNullOrEmpty(pagesize))
		{
			pagesize = "15";
		}

		string json = mg_StepBLL.QueryStepList(page, pagesize, fl_id, st_id, part_id);
		Response.Write(json);
		Response.End();
	}


	void SaveStep()
	{

		string step_id = Request.Params["step_id"];
		string fl_id = Request.Params["fl_id"];
		string st_id = Request.Params["st_id"];
		string bom_id = Request.Params["bom_id"];
		string part_id = Request.Params["part_id"];
		string step_name = Request.Params["step_name"];
		string step_clock = Request.Params["step_clock"];
		string step_plccode = Request.Params["step_plccode"];
        string barcode_start = Request.Params["barcode_start"];
        string barcode_number = Request.Params["barcode_number"];
		string bom_count = Request.Params["bom_count"];
		string step_pic = Request.Params["step_pic"];
		string step_desc = Request.Params["step_desc"];
		//string odsName = Request.Params["odsName"];
		//string odsKeyword = Request.Params["odsKeyword"];



		mg_StepModel model = new mg_StepModel();
		model.step_id = NumericParse.StringToInt(step_id);
		model.fl_id = NumericParse.StringToInt(fl_id);
		model.st_id = NumericParse.StringToInt(st_id);
		model.bom_id = NumericParse.StringToInt(bom_id);
		model.part_id = NumericParse.StringToInt(part_id);
		model.step_name = step_name;
		model.step_clock = NumericParse.StringToInt(step_clock);
		model.step_plccode = NumericParse.StringToInt(step_plccode);
        model.barcode_start = NumericParse.StringToInt(barcode_start);
        model.barcode_number = NumericParse.StringToInt(barcode_number);

		model.bom_count = NumericParse.StringToInt(bom_count);
		model.step_pic = step_pic;
		model.step_desc = step_desc;
		//model.odsName = odsName;
		//model.odsKeyword = odsKeyword;

		//string json = mg_StepBLL.saveStepAndODS(model);
		string json = mg_StepBLL.saveStep(model);
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