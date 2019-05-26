using System.Collections.Generic;
using System.Data;

namespace FunStop.Classes
{
    public class ClsCar
    {
        ClsDBConn Conn = new ClsDBConn();

        #region Properties
        public int CarID { get; set; }
        public int CarTypeID { get; set; }
        public int Minutes { get; set; }
        #endregion

        #region Methods
        public DataTable GetCarTypeList()
        {
            DataTable dt = new DataTable();
            List<ClsParams> lst = new List<ClsParams>();
            return dt = Conn.Listado("Sp_GetCarTypeList", lst);
        }

        public DataTable GetRate()
        {
            DataTable dt = new DataTable();
            List<ClsParams> lst = new List<ClsParams>();
            lst.Add(new ClsParams("@CarTypeID", CarTypeID));
            lst.Add(new ClsParams("@Minutes", Minutes));
            return dt = Conn.Listado("Sp_GetRate", lst);
        }

        public DataTable GetRateList()
        {
            DataTable dt = new DataTable();
            List<ClsParams> lst = new List<ClsParams>();
            lst.Add(new ClsParams("@CarTypeID", CarTypeID));
            return dt = Conn.Listado("Sp_GetRateList", lst);
        }

        public DataTable GetAvailableCars()
        {
            DataTable dt = new DataTable();
            List<ClsParams> lst = new List<ClsParams>();
            return dt = Conn.Listado("Sp_GetAvailableCars", lst);
        }
        #endregion

    }
}