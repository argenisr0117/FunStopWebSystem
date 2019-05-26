using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FunStop
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserLogin_lb.Text = ClsGlobal.LoginName;
            Time_Timer_Tick(e, e);
        }

        protected void Time_Timer_Tick(object sender, EventArgs e)
        {
            Time_lb.Text = DateTime.Now.ToLongTimeString();
        }
    }
}