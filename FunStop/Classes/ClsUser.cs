using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FunStop.Classes
{
    public class ClsUser
    {
        ClsDBConn Conn = new ClsDBConn();

        #region Properties
        public int UserID { get; set; }
        public string UserLogin { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }

        public string Device { get; set; }
        #endregion

        #region Methods
        public int VerifyUserLogin()
        {
            int Msj = 0;
            List<ClsParams> lst = new List<ClsParams>();
            lst.Add(new ClsParams("@Msj", "", SqlDbType.Int, ParameterDirection.Output, 1));
            lst.Add(new ClsParams("@UserLogin", UserLogin));
            lst.Add(new ClsParams("@Password", Password));
            Conn.EjecutarSP("Sp_VerifyUserLogin", ref lst);
            Msj = Convert.ToInt32(lst[0].Valor);
            return Msj;
        }

        public void RegisterUserSystemLog()
        {
            List<ClsParams> lst = new List<ClsParams>();
            lst.Add(new ClsParams("@UserID", UserID));
            lst.Add(new ClsParams("@Device", Device));
            Conn.EjecutarSP("Sp_SystemLogRegister", ref lst);
        }

        public void CreateBackup()
        {
            List<ClsParams> lst = new List<ClsParams>();
            Conn.EjecutarSP("Sp_CreateBackup", ref lst);
        }
        #endregion


    }
}