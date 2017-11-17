using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_BOM_MatchModel
    {
     
		#region Model
		private string _bomno;
		private bool _matchresult;
		private string _scancode;
		private string _tracecode;
		private DateTime? _recorddate;
		private string _uid;
		private string _vin;
		/// <summary>
		/// 
		/// </summary>
		public string BOMNO
		{
			set{ _bomno=value;}
			get{return _bomno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool MatchResult
		{
			set{ _matchresult=value;}
			get{return _matchresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScanCode
		{
			set{ _scancode=value;}
			get{return _scancode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TraceCode
		{
			set{ _tracecode=value;}
			get{return _tracecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VIN
		{
			set{ _vin=value;}
			get{return _vin;}
		}
		#endregion Model
    }
}
