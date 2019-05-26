using Microsoft.Reporting.WebForms;
using System;
using System.Web.UI;

namespace FunStop
{
    public partial class Report : Page
    {
        #region Methods

        private void CustomerTicket()
        {
            FunStopDataSet ds = new FunStopDataSet();
            FunStopDataSetTableAdapters.Sp_TicketReportTableAdapter ta = new FunStopDataSetTableAdapters.Sp_TicketReportTableAdapter();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport lc = ReportViewer1.LocalReport;
            string ruta = "Reports\\RptTicket.rdlc";
            lc.ReportPath = ruta;
            ta.Fill(ds.Sp_TicketReport, ClsGlobal.TicketID);
            ReportDataSource rds = new ReportDataSource();
            ReportViewer1.LocalReport.DisplayName = "TICKET";
            rds.Name = "DataSet1";
            rds.Value = (ds.Tables["Sp_TicketReport"]);
            ReportViewer1.LocalReport.DataSources.Clear();
            lc.DataSources.Add(rds);
            lc.PrintToPrinter();
            //this.ReportViewer1.ShowPrintButton = true;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClsGlobal.TicketID != 0)
            {
                if (!IsPostBack)
                {
                    CustomerTicket();
                    Response.Write("<script>javascript:window.close();</script>");

                }
            }
            else
            {
                Response.Redirect("~/Caja.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            //System.Threading.Thread.Sleep(2000);
            ClsGlobal.TicketID = 0;
        }
    }
}