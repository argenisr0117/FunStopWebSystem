using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FunStop
{
    public partial class Caja : Page
    {
        #region Instances
        DataTable dt = new DataTable();
        Classes.ClsCar C = new Classes.ClsCar();
        Classes.ClsTicket T = new Classes.ClsTicket();
        #endregion

        #region Methods
        public void FillCarTypeDropd()
        {
            dt = C.GetCarTypeList();
            CarType_Dropd.DataTextField = "Description";
            CarType_Dropd.DataValueField = "CarTypeID";
            CarType_Dropd.DataSource = dt;
            CarType_Dropd.DataBind();
        }
        public void FillRateRadioB()
        {
            C.CarTypeID = Convert.ToInt16(CarType_Dropd.SelectedValue);
            dt = C.GetRateList();
            tarifaRb.DataTextField = "Minutes";
            tarifaRb.DataValueField = "Minutes";
            tarifaRb.DataSource = dt;
            tarifaRb.DataBind();
            for (int x = 0; x < tarifaRb.Items.Count; x++)
            {
                tarifaRb.Items[x].Text = tarifaRb.Items[x].Text + " mins";
            }
            tarifaRb.SelectedIndex = 0;
            GetRate();
        }
        public void GetRate()
        {
            C.CarTypeID = Convert.ToInt16(CarType_Dropd.SelectedValue);
            C.Minutes = Convert.ToInt16(tarifaRb.SelectedValue);
            dt = C.GetRate();
            tarifaTxt.Text = dt.Rows[0][1].ToString();
        }
        public void TicketRegister(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nombreTxt.Text) && !string.IsNullOrWhiteSpace(telefonoTxt.Text))
            {
                int Answer = 0;
                string Message = "";
                T.Customer = nombreTxt.Text;
                T.Phone = telefonoTxt.Text;
                T.Identification = "N/A";
                T.CarTypeID = Convert.ToInt32(CarType_Dropd.SelectedValue);
                T.UserID = ClsGlobal.UserID;
                T.TrackTime = Convert.ToInt32(tarifaRb.SelectedValue);
                T.Fare = Convert.ToDecimal(tarifaTxt.Text);
                T.Total = Convert.ToDecimal(tarifaTxt.Text);
                Answer = T.TicketRegister();
                if (Answer != 0)
                {
                    //Response.Write("<script>alert(Ticket #'" + Answer + "' Registrado!)</script>");
                    ClsGlobal.TicketID = Answer;
                    Message = "alert('Ticket # " + Answer + " Registrado!')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
                    //string url = "Report.aspx";
                    //string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
                    //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    //Response.Redirect("~/Report.aspx", false);
                    //Context.ApplicationInstance.CompleteRequest();
                }

            }
        }

        public void FillGridView()
        {
            dt = T.GetLastTickets();
            ticketsGrid.DataSource = dt;
            ticketsGrid.DataBind();
        }
        public void CleanTxts()
        {
            nombreTxt.Text = string.Empty;
            telefonoTxt.Text = string.Empty;
            totalTxt.Text = string.Empty;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //if (!multicket_chbox.Checked)
                //{
                //    CleanTxts();
                //}
                FillGridView();
            }
            else
            {
                FillCarTypeDropd();
                tarifaTxt.Text = "0.00";
                FillRateRadioB();
                tarifaRb.SelectedIndex = 0;
                FillGridView();
                //ticketsGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        #region EventMethods
        protected void CarType_Dropd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CarType_Dropd.Items.Count > 0)
            {
                FillRateRadioB();
            }
        }

        protected void tarifaRb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tarifaRb.Items.Count > 0)
            {
                GetRate();
            }
        }

        protected void RegistrarBtn_Click(object sender, EventArgs e)
        {
            TicketRegister(this, e);
            ScriptManager.RegisterStartupScript(Page, GetType(), "OpenWindow", "window.open('Report.aspx','mywindow','menubar=1,resizable=1,width=900,height=600');", true);
            FillGridView();
            if (!multicket_chbox.Checked)
            {
                CleanTxts();
            }
            else
            {
                decimal tarifa = Convert.ToDecimal(tarifaTxt.Text);
                decimal total = 0;
                totalTxt.Text = tarifa.ToString();
                if (string.IsNullOrWhiteSpace(totalTxt.Text))
                {
                    total = tarifa;
                    totalTxt.Text = total.ToString();
                }
                else
                {
                    total = Convert.ToDecimal(totalTxt.Text);
                    total = total + tarifa;
                    totalTxt.Text = total.ToString();
                }
            }
        }
        protected void LimpiarBtn_Click(object sender, EventArgs e)
        {
            CleanTxts();
        }

        protected void ticketsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;
            string TicketId = e.CommandArgument.ToString();
            int Answer = 0;

            switch (command)
            {
                case "Print":

                    ClsGlobal.TicketID = Convert.ToInt16(TicketId);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "OpenWindow", "window.open('Report.aspx','mywindow','menubar=1,resizable=1,width=900,height=600');", true);
                    break;

                case "Anular":
                    ClsGlobal.TicketID = Convert.ToInt16(TicketId);
                    T.TicketID = ClsGlobal.TicketID;
                    Answer = T.TicketCancel();
                    FillGridView();
                    //UpdatePanel5.Update();
                    string Message = "alert('Ticket # " + ClsGlobal.TicketID + " anulado!')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
                    break;
            }
            //ClsGlobal.TicketID = 0;
        }
        #endregion


    }
}