<%@ Page Title="Facturación de Tickets - FunStop" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Caja.aspx.cs" Inherits="FunStop.Caja" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h2 class="mt-3"><%: Title %></h2>
        <hr />
    </div>
    <div class="container-fluid mb-3">
        <div class="row">
            <div class=" col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <h3>Datos Cliente</h3>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="nombreTxt" CssClass="form-control" autofocus="autofocus" runat="server" required="Required" AutoCompleteType="Disabled" TextMode="SingleLine"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="telefonoTxt" CssClass="form-control" runat="server" TextMode="Phone" AutoCompleteType="Disabled" required="Required"></asp:TextBox>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="Tipo Carro"></asp:Label>
                                    <asp:DropDownList runat="server" EnableViewState="true" CssClass="form-control" ID="CarType_Dropd" OnSelectedIndexChanged="CarType_Dropd_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group radio">
                                    <asp:Label ID="Label2" runat="server" Text="Tiempo"></asp:Label>
                                    <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="tarifaRb" OnSelectedIndexChanged="tarifaRb_SelectedIndexChanged" AutoPostBack="true">
                                        <%--<asp:ListItem Text="15 mins" Value="15" />
                                <asp:ListItem Text="25 mins" Value="25" />
                                <asp:ListItem Text="30 mins" Value="30" />
                                <asp:ListItem Text="45 mins" Value="45" />--%>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Tarifa"></asp:Label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">RD$</span>
                                        </div>
                                        <asp:TextBox ID="tarifaTxt" CssClass="form-control text-danger font-weight-bolder h1" runat="server" TextMode="Number" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox Text="Multiples Tickets" ID="multicket_chbox" CssClass="checkbox checkbox-success" runat="server" />
                                </div>
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CarType_Dropd" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group">
                    <asp:Label ID="Label6" runat="server" Text="Total"></asp:Label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text">RD$</span>
                        </div>
                        <asp:TextBox ID="totalTxt" CssClass="form-control text-danger font-weight-bolder h1" runat="server" TextMode="Number" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-7 col-md-7 col-lg-7 col-xl-7">
                <h3>Ultimos Tickets</h3>
                <div class="table-responsive-sm">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="ticketsGrid" runat="server"
                                BorderColor="#CCCCCC"
                                BorderWidth="0px"
                                CellPadding="3" GridLines="Horizontal"
                                CssClass="table table-sm table-hover" AutoGenerateColumns="False" OnRowCommand="ticketsGrid_RowCommand">
                                <HeaderStyle CssClass="thead-dark"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="TicketID" HeaderText="TicketID" />
                                    <asp:BoundField DataField="Tiempo" HeaderText="Minutos" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="TipoCarro" HeaderText="Tipo Carro" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("TicketID") %>' ToolTip="Anular" CommandName="Anular" ID="anularBtn" CausesValidation="false" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Desea anular el ticket seleccionado?');"><i class='fa fa-close'></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("TicketID") %>' ToolTip="Reimprimir" CommandName="Print" ID="reimprimirBtn" CausesValidation="false" CssClass="btn btn-sm btn-outline-success"><i class='fa fa-print'></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--                    <asp:BoundField DataField="TicketID" HeaderText="TicketID" SortExpression="TicketID" />
                        <asp:BoundField DataField="Customer" HeaderText="Cliente" SortExpression="Customer" />
                        <asp:BoundField DataField="TrackTime" HeaderText="Tiempo" SortExpression="TrackTime" />--%>
                                </Columns>
                                <RowStyle BorderColor="#999999" />
                            </asp:GridView>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ticketsGrid" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>

            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row mt-3">
                    <div class="col-12 col-md-6 col-sm-6 col-lg-6">
                        <div class="btn-group btn-group-lg">
                            <asp:Button Text="Registrar" ToolTip="Registrar" ID="RegistrarBtn" runat="server" CssClass="btn btn-outline-success btn-block" OnClientClick="AcumulateTotal()" OnClick="RegistrarBtn_Click" />
                        </div>
                        <div class="btn-group btn-group-lg">
                            <asp:Button Text="Limpiar" ID="LimpiarBtn" UseSubmitBehavior="False" CausesValidation="false" ToolTip="Limpiar" runat="server" CssClass="btn  btn-outline-danger btn-block" OnClick="LimpiarBtn_Click" OnClientClick="ClearTxts()" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            function ClearTxts() {
                $("#<%= nombreTxt.ClientID %>").val('');
                $("#<%= telefonoTxt.ClientID %>").val('');
                $("#<%= totalTxt.ClientID %>").val('');
                $("#<%= multicket_chbox.ClientID %>").prop('checked', false);
            }

            function AcumulateTotal() {
                if ($("#<%= nombreTxt.ClientID %>").val() != '' && $("#<%= telefonoTxt.ClientID %>").val() != '') {
                    var check = $("#<%= multicket_chbox.ClientID %>").is(':checked');
                    var total = $("#<%= tarifaTxt.ClientID %>").val();
                    var totalf = $("#<%= totalTxt.ClientID %>").val();
                    var totalff = 0;
                    //alert(totalf);

                    if (check) {
                        if (totalf > 0) {
                            //alert('entre' + totalf + total);
                            totalff = parseFloat(totalf) + parseFloat(total);
                            //alert(totalff);
                            $("#<%= totalTxt.ClientID %>").val(totalff.toFixed(2));
                        }
                        else {
                            $("#<%= totalTxt.ClientID %>").val(total);
                        }
                    }
                    else {
                        $("#<%= totalTxt.ClientID %>").val(total);
                    }
                }

            }
        </script>
    </div>
</asp:Content>
