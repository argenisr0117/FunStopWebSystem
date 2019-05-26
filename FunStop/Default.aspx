<%@ Page Title="Inicio - FunStop" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FunStop._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <asp:Timer ID="Time_Timer" runat="server" Interval="1000" OnTick="Time_Timer_Tick">
        </asp:Timer>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <img src="/Images/funstoplogo.JPG" alt="Alternate Text" class="img-fluid" />
                <div class="row justify-content-center align-items-center h-100">
                    <div >
                        <asp:Label ID="UserLogin_lb" runat="server" Text="" CssClass="time"></asp:Label>
                    </div>
                    <div >
                        <asp:Label ID="Time_lb" runat="server" Text="" CssClass="time"></asp:Label>
                    </div>
                </div>
                <%-- <div class="row justify-content-center align-items-center h-100">
                    
                </div>    --%>
            </ContentTemplate>
            <Triggers>

                <asp:AsyncPostBackTrigger ControlID="Time_Timer" EventName="Tick" />

            </Triggers>
        </asp:UpdatePanel>

    </div>

</asp:Content>
