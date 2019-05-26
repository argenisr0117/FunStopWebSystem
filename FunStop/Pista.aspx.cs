using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FunStop
{
    public partial class Pista : Page
    {
        #region Instances
        DataTable dt = new DataTable();
        Classes.ClsCar C = new Classes.ClsCar();
        Classes.ClsTicket T = new Classes.ClsTicket();
        #endregion

        #region Methods    
        public void FillTicketsPendGridView()
        {
            dt = T.GetPendingTickets();
            ticketspendGrid.DataSource = dt;
            ticketspendGrid.DataBind();
        }

        public void FillAvailableCarsGridView()
        {
            carrosGrid.Columns[1].Visible = true;
            dt = C.GetAvailableCars();
            carrosGrid.DataSource = dt;
            carrosGrid.DataBind();
            carrosGrid.Columns[1].Visible = false;
        }

        public void RemoveSelectedRowClass(GridView grid)
        {
            foreach (GridViewRow row in grid.Rows)
            {
                row.CssClass = "";
            }
        }

        public void FillAssignedTicketsGrid()
        {
            dt = T.GetAssignedTickets();
            carrospistaGrid.DataSource = dt;
            carrospistaGrid.DataBind();
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            }
            else
            {
                FillTicketsPendGridView();
                FillAvailableCarsGridView();
                FillAssignedTicketsGrid();
                ticketTimer_Tick(e, e);
            }
        }
        #region EventMethods

        protected void ticketspendGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ticketspendGrid.PageIndex = e.NewPageIndex;
            FillTicketsPendGridView();
        }
        protected void ticketchkb_CheckedChanged(object sender, EventArgs e)
        {
            RemoveSelectedRowClass(ticketspendGrid);

            foreach (GridViewRow row in ticketspendGrid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("ticketChkb") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.CssClass = "bg-danger";
                        ClsGlobal.TicketID = Convert.ToInt32(row.Cells[1].Text.ToString());
                        ClsGlobal.Time = Convert.ToInt32(row.Cells[2].Text.ToString());
                        ClsGlobal.TicketTCar = row.Cells[4].Text.ToString();
                        //string Message = "alert('Ticket # " + ClsGlobal.TicketID + " seleccionado!')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
                        break;
                    }
                    else
                    {
                        ClsGlobal.TicketID = 0;
                        ClsGlobal.Time = 0;
                        ClsGlobal.TicketTCar = "";
                    }
                }
            }
        }
        protected void carChkb_CheckedChanged(object sender, EventArgs e)
        {
            RemoveSelectedRowClass(carrosGrid);

            foreach (GridViewRow row in carrosGrid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("carChkb") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.CssClass = "bg-danger";
                        ClsGlobal.CarID = Convert.ToInt32(row.Cells[1].Text.ToString());
                        ClsGlobal.CarType = row.Cells[3].Text.ToString();
                        //string Message = "alert('Ticket # " + ClsGlobal.CarID + " seleccionado!')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
                        break;
                    }
                    else
                    {
                        ClsGlobal.CarID = 0;
                        ClsGlobal.CarType = "";
                    }
                }
            }
        }
        protected void carpistaChkb_CheckedChanged(object sender, EventArgs e)
        {
            RemoveSelectedRowClass(carrospistaGrid);

            foreach (GridViewRow row in carrospistaGrid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("carpistaChkb") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.CssClass = "bg-danger";
                        ClsGlobal.CarID = Convert.ToInt32(row.Cells[2].Text.ToString());
                        ClsGlobal.TicketID = Convert.ToInt32(row.Cells[1].Text.ToString());
                        //string Message = "alert('Ticket # " + ClsGlobal.CarID + " seleccionado!')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
                        break;
                    }
                    else
                    {
                        ClsGlobal.CarID = 0;
                        ClsGlobal.TicketID = 0;
                    }
                }
            }
        }
        protected void asignarBtn_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.TicketID > 0 && ClsGlobal.CarID > 0)
            {
                if (ClsGlobal.TicketTCar.ToString() != ClsGlobal.CarType.ToString())
                {
                    string Message = "alert('Error,Tipo de carro selecionado no coincide con el del ticket.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
                }
                else
                {
                    int Answer = 0;
                    T.TicketID = ClsGlobal.TicketID;
                    T.CarID = ClsGlobal.CarID;
                    T.UserID = ClsGlobal.UserID;
                    T.TrackTime = ClsGlobal.Time;
                    Answer = T.TicketCarAssign();
                    if (Answer == 1)
                    {
                        FillAssignedTicketsGrid();
                        FillAvailableCarsGridView();
                        FillTicketsPendGridView();
                    }
                }

            }
        }
        protected void ticketTimer_Tick(object sender, EventArgs e)
        {
            if (carrospistaGrid.Rows.Count > 0)
            {
                T.UpdateTicketTime();
                FillAssignedTicketsGrid();
            }
        }

        protected void completarBtn_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.TicketID > 0 && ClsGlobal.CarID > 0)
            {
                int Answer = 0;
                T.TicketID = ClsGlobal.TicketID;
                T.CarID = ClsGlobal.CarID;
                Answer = T.CompleteTicket();
                if (Answer == 1)
                {
                    FillAssignedTicketsGrid();
                    FillAvailableCarsGridView();
                    FillTicketsPendGridView();
                }
            }
            else
            {
                string Message = "alert('Error,Debe seleccionar un Ticket.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Message, true);
            }
            ClsGlobal.CarID = 0;
            ClsGlobal.TicketID = 0;
        }
        #endregion

        protected void carrosGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            carrosGrid.PageIndex = e.NewPageIndex;
            FillAvailableCarsGridView();
        }
    }
}