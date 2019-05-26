using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FunStop
{
    public partial class SiteMaster : MasterPage
    {
        Classes.ClsUser U = new Classes.ClsUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClsGlobal.UserID == 0)
            {
                Response.Redirect("Login.aspx");
                

            }
            //menucaja.Visible = false;
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            ClsGlobal.UserID = 0;
            Response.Redirect("Login.aspx");

        }

        protected void backupBtn_Click(object sender, EventArgs e)
        {
            U.CreateBackup();
            string Message = "alert('Backup realizado satisfactoriamente')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
        }
    }
}