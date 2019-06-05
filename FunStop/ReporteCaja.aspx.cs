using Microsoft.Reporting.WebForms;
using System;
using System.Web;
using System.Web.UI;

namespace FunStop
{
    public partial class ReporteCaja : Page
    {
        Classes.ClsTicket T = new Classes.ClsTicket();
        DateTime Desde = DateTime.Now;
        DateTime Hasta = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
            }
            else
            {
                desdeDtp.Text = DateTime.Now.ToString("yyyy-MM-dd");
                hastaDtp.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

        }

        protected void reporteBtn_Click(object sender, EventArgs e)
        {
            Desde = Convert.ToDateTime(desdeDtp.Text);
            Hasta = Convert.ToDateTime(hastaDtp.Text);

            if (Desde.Date <= Hasta.Date)
            {
                CajaReport();
            }

        }
        private void CajaReport()
        {
            FunStopDataSet ds = new FunStopDataSet();
            FunStopDataSetTableAdapters.Sp_ReportResumidoTableAdapter ta = new FunStopDataSetTableAdapters.Sp_ReportResumidoTableAdapter();
            FunStopDataSetTableAdapters.Sp_ReportDetalladoTableAdapter ta1 = new FunStopDataSetTableAdapters.Sp_ReportDetalladoTableAdapter();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport lc = ReportViewer1.LocalReport;
            string ruta = "Reports\\CierreCaja.rdlc";
            lc.ReportPath = ruta;
            ta.Fill(ds.Sp_ReportResumido,Desde,Hasta);
            ta1.Fill(ds.Sp_ReportDetallado, Desde, Hasta);
            ReportDataSource rds = new ReportDataSource();
            ReportDataSource rds1 = new ReportDataSource();
            ReportViewer1.LocalReport.DisplayName = "TICKET";
            rds.Name = "DataSet1";
            rds.Value = (ds.Tables["Sp_ReportResumido"]);
            rds1.Name = "DataSet2";
            rds1.Value = (ds.Tables["Sp_ReportDetallado"]);
            ReportViewer1.LocalReport.DataSources.Clear();
            lc.DataSources.Add(rds);
            lc.DataSources.Add(rds1);


            //Export the RDLC Report to Byte Array.
            //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out string contentType, out string encoding, out string extension, out string[] streamIds, out Warning[] warnings);

            ////Download the RDLC Report in Word, Excel, PDF and Image formats.
            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + "PDF");
            //Response.BinaryWrite(bytes);
            //Response.Flush();
            //Response.End();
            this.ReportViewer1.ShowPrintButton = true;
        }

    }
}