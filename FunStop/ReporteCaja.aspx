<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReporteCaja.aspx.cs" Inherits="FunStop.ReporteCaja" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid mt-3">
        <div class="row">
            <div class=" col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <h3>Filtros</h3>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Desde"></asp:Label>
                            <asp:TextBox ID="desdeDtp" CssClass="form-control" autofocus="autofocus" runat="server" required="Required" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="Hasta"></asp:Label>
                            <asp:TextBox ID="hastaDtp" CssClass="form-control" runat="server" TextMode="Date" required="Required"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button Text="Reporte" ID="reporteBtn" CssClass="btn btn-outline-success" OnClick="reporteBtn_Click" runat="server" />

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
            <div class=" col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="WhiteSmoke" Width="100%"></rsweb:ReportViewer>
            </div>
        </div>

    </div>

</asp:Content>
