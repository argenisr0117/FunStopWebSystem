using FunStop.Classes;
using System;
using System.Web.UI;

namespace FunStop
{
    public partial class Login : Page
    {
        public ClsUser U { get; set; } = new ClsUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            //int tm = 15;
            //if (!IsPostBack)
            //    Session["counter"] = tm;
        }

        //protected void Timer1_Tick1(object sender, EventArgs e)
        //{
        //    //timer.Update();
        //}

        //protected void timer_Load(object sender, EventArgs e)
        //{
        //    //int tm = 0;

        //    //if (Session["counter"] != null)
        //    //    Label2.Text =

        //    //    Convert.ToString(Convert.ToInt16(Session["counter"]) - 1);
        //    //Session["counter"] = Convert.ToString(Convert.ToInt16(Session["counter"]) - 1);


        //    //if (Convert.ToInt16(Label2.Text) == 0)
        //    //{
        //    //    tm = 20;
        //    //    Label2.Text = "HHHHHH";
        //    //}
        //    //for(int x=0; x <= 5; x++)
        //    //{
        //    //    Label3.Text = "Contraseña" + x.ToString();
        //    //}
        //}

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    if (!string.IsNullOrWhiteSpace(userTxt.Text) && !string.IsNullOrWhiteSpace(passwordTxt.Text))
                    {
                        LoginError.Text = string.Empty;
                        int Answer = 0;
                        U.UserLogin = userTxt.Text;
                        U.Password = passwordTxt.Text;
                        Answer = U.VerifyUserLogin();
                        //Answer = 1;
                        if (Answer != 0)
                        {
                            ClsGlobal.LoginName = userTxt.Text;
                            ClsGlobal.UserID = Answer;
                            string clientPCName = "";
                            string[] computer_name = System.Net.Dns.GetHostEntry(
                            Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                            clientPCName = computer_name[0].ToString();
                            U.UserID = ClsGlobal.UserID;
                            U.Device = clientPCName;
                            U.RegisterUserSystemLog();
                            Response.Redirect("~/Default.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            LoginError.Text = "Usuario o contraseña incorrecto";
                            LoginError.CssClass = "text-danger";
                            passwordTxt.Focus();

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //LoginError.Text = "asdasdasd" + ex.Message;
                //Type cstype = this.GetType();
                string Message = "alert('" + ex.Message+ "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
                //if (!ClientScript.IsStartupScriptRegistered("myErrorScript"))
                //{
                //    Page.ClientScript.RegisterStartupScript(cstype, "myErrorScript", script);
                //}
                //Response.Write("<script language='javascript'>alert('" +
                //Server.HtmlEncode(ex.Message) + "')</script>");

            }

        }
    }
}
