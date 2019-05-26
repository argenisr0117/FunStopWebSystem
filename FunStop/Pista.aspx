<%@ Page Title="Pista - FunStop" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pista.aspx.cs" Inherits="FunStop.Pista" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h2 class="mt-3"><%: Title %></h2>
        <hr />
    </div>
    <div class="container-fluid mb-3">
        <div class="row">
            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                <ContentTemplate>
                    <asp:Timer ID="ticketTimer" runat="server" Interval="5000" OnTick="ticketTimer_Tick">
                    </asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class=" col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <h4>Tickets Pendientes</h4>
                        <hr />
                        <div class="table-responsive-sm">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="ticketspendGrid" runat="server"
                                        BorderColor="#CCCCCC"
                                        BorderWidth="0px"
                                        CellPadding="3" GridLines="Horizontal"
                                        CssClass="table table-sm table-hover" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="ticketspendGrid_PageIndexChanging">
                                        <HeaderStyle CssClass="thead-dark"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ticketChkb" runat="server" onclick="CheckTicketPend(this);"
                                                        OnCheckedChanged="ticketchkb_CheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TicketID" HeaderText="TicketID" />
                                            <asp:BoundField DataField="Tiempo" HeaderText="Minutos" />
                                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                            <asp:BoundField DataField="TipoCarro" HeaderText="Tipo Carro" />
                                        </Columns>
                                        <PagerSettings NextPageText="&amp;lt;Siguiente&amp;gt;" PreviousPageText="&amp;lt;Anterior&amp;gt;" />
                                        <RowStyle BorderColor="#999999" Height="10px" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="d-flex justify-content-center align-items-center">
                            <asp:Button Text="Asignar" ToolTip="Asignar" OnClick="asignarBtn_Click" ID="asignarBtn" runat="server" CssClass="btn btn-success btn-block" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <h4>Carros Disponibles</h4>
                <hr />
                <div class="table-responsive-sm">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <asp:GridView ID="carrosGrid" runat="server"
                                BorderColor="#CCCCCC"
                                BorderWidth="0px"
                                CellPadding="3" GridLines="Horizontal"
                                CssClass="table table-sm table-hover" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="carrosGrid_PageIndexChanging">
                                <HeaderStyle CssClass="thead-dark"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="carChkb" runat="server" onclick="CheckAvailableCar(this);"
                                                OnCheckedChanged="carChkb_CheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CarID" HeaderText="CarID" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:BoundField DataField="TipoCarro" HeaderText="Tipo Carro" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BorderColor="#999999" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <h4>Carros en Pista</h4>
                <hr />
                <div class="table-responsive-sm">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                        <ContentTemplate>
                            <asp:GridView ID="carrospistaGrid" runat="server"
                                BorderColor="#CCCCCC"
                                BorderWidth="0px"
                                CellPadding="3" GridLines="Horizontal"
                                CssClass="table table-sm table-hover" AutoGenerateColumns="False">
                                <HeaderStyle CssClass="thead-dark"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="carpistaChkb" runat="server" onclick="CheckFinishedTicket(this);"
                                                AutoPostBack="true" OnCheckedChanged="carpistaChkb_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TicketID" HeaderText="TicketID" />
                                    <asp:BoundField DataField="CarID" HeaderText="CarID" />
                                    <asp:BoundField DataField="Carro" HeaderText="Carro" />
                                    <asp:BoundField DataField="TiempoR" HeaderText="T. Restante" />
                                    <asp:BoundField DataField="TiempoE" HeaderText="T. Exced" />
                                </Columns>
                                <RowStyle BorderColor="#999999" />
                            </asp:GridView>
                            <div class="d-flex justify-content-center align-items-center">
                                <asp:Button Text="Completar" ID="completarBtn" OnClick="completarBtn_Click" UseSubmitBehavior="False" CausesValidation="false" ToolTip="Completar" runat="server" CssClass="btn btn-danger btn-block" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <%--   <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row justify-content-lg-center">
                    <div class="col-12 col-md-6 col-sm-6 col-lg-6 justify-content-lg-center">
                        <div class="btn-group btn-group-lg">
                        </div>
                        <div class="btn-group btn-group-lg">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>

        <script type="text/javascript"> 

            function CheckTicketPend(source) {
                var isChecked = source.checked;
                $("#<%=ticketspendGrid.ClientID%> input[id*='ticketChkb']").each(function (index) {
                    $(this).attr('checked', false);
                });
                source.checked = isChecked;
            }

            function CheckAvailableCar(source) {
                var isChecked = source.checked;
                $("#<%=carrosGrid.ClientID%> input[id*='carChkb']").each(function (index) {
                    $(this).attr('checked', false);
                });
                source.checked = isChecked;
                //source.addClass('bg-danger');
            }
            function CheckFinishedTicket(source) {
                var isChecked = source.checked;
                $("#<%=carrospistaGrid.ClientID%> input[id*='carpistaChkb']").each(function (index) {
                    $(this).attr('checked', false);
                });
                source.checked = isChecked;
                //source.addClass('bg-danger');
            }
            /*Prueba de como acceder a controles asp.net con jquery*/
            //$(function () {
            //    $("[id*=RegistrarBtn]").click(function () {
            //        var totalRowCount = $("[id*=ticketspendGrid] tr").length;
            //        var rowCount = $("[id*=ticketspendGrid] td").closest("tr").length;
            //        var message = "Total Row Count: " + totalRowCount;
            //        message += "\nRow Count: " + rowCount;
            //        alert(message);
            //        return false;
            //    });
            //});
        </script>
    </div>

</asp:Content>
