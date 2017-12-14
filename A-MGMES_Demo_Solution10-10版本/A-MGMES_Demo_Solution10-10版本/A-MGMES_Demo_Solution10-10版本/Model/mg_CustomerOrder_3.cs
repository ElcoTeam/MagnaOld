using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_CustomerOrder_3
    {
        #region Model
        private decimal? OrderID;

        public decimal? OrderID1
        {
            get { return OrderID; }
            set { OrderID = value; }
        }
        private string CustomerNumber;

        public string _CustomerNumber
        {
            get { return CustomerNumber; }
            set { CustomerNumber = value; }
        }
        private string JITCallNumber;

        public string _JITCallNumber
        {
            get { return JITCallNumber; }
            set { JITCallNumber = value; }
        }
        private string SerialNumber;

        public string _SerialNumber
        {
            get { return SerialNumber; }
            set { SerialNumber = value; }
        }
        private decimal? SerialNumber_Int;

        public decimal? _SerialNumber_Int
        {
            get { return SerialNumber_Int; }
            set { SerialNumber_Int = value; }
        }
        private int? SerialNumber_MES;

        public int? _SerialNumber_MES
        {
            get { return SerialNumber_MES; }
            set { SerialNumber_MES = value; }
        }
        private string VinNumber;

        public string _VinNumber
        {
            get { return VinNumber; }
            set { VinNumber = value; }
        }
        private string PlanDeliverTime;

        public string _PlanDeliverTime
        {
            get { return PlanDeliverTime; }
            set { PlanDeliverTime = value; }
        }
        private int? OrderType;

        public int? _OrderType
        {
            get { return OrderType; }
            set { OrderType = value; }
        }
        private int? OrderState;

        public int? _OrderState
        {
            get { return OrderState; }
            set { OrderState = value; }
        }
        private DateTime CreateTime;

        public DateTime _CreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }
        private DateTime StartTime;

        public DateTime _StartTime
        {
            get { return StartTime; }
            set { StartTime = value; }
        }
        private DateTime EndTime;

        public DateTime _EndTime
        {
            get { return EndTime; }
            set { EndTime = value; }
        }
        private int? OrderIsPrint;

        public int? _OrderIsPrint
        {
            get { return OrderIsPrint; }
            set { OrderIsPrint = value; }
        }
        private int? OrderIsHistory;

        public int? _OrderIsHistory
        {
            get { return OrderIsHistory; }
            set { OrderIsHistory = value; }
        }

       
        #endregion Model
    }
}
