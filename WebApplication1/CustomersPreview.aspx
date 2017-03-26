<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomersPreview.aspx.cs" Inherits="ProjectX.Web.CustomersPreview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="font-family: 'Titillium Web'">
<head runat="server">
    <title>Logged in</title>
    <link rel="stylesheet" href="Style/fonts.css" />
    <link rel="stylesheet" href="Style/bootstrap.min.css"/>
    <link rel="stylesheet" href="Style/bootstrap-theme.min.css"/>
    <link rel="stylesheet" href="Style/CustomersPreview.css" type="text/css" />
    <link rel="stylesheet" href="Style/SharedStyle.css" type="text/css" />
    <link href="Style/fresh-bootstrap-table.css" rel="stylesheet" />

    <script src="scripts/jquery.min.js"></script>
    <script src="scripts/bootstrapjs.js"></script>
    <script src="scripts/jquery-2.1.1.min.js"></script>
</head>
<body>

<style type="text/css">
    @font-face {
        font-family: 'Titillium Web';
        font-style: normal;
        font-weight: 400;
        src: url(http://fonts.gstatic.com/s/titilliumweb/v4/7XUFZ5tgS-tD6QamInJTcQU404wzEgdBsmVXv_TVccA.eot);
    }
    body{
        font-family:'Titillium Web';
    }
</style>

    <form id="form1" runat="server">

<!-- NAVBAR BEGIN-->
<nav class="navbar navbar-inverse navbar-fixed-top">
  <div class="container-fluid">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-collapse">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>                        
      </button>
      <a class="navbar-brand" href="#">ProjectX</a>
    </div>
    <div class="collapse navbar-collapse" id="navbar-collapse">
      <ul class="nav navbar-nav">
        <li><a runat="server" href="UsersPage.aspx" id="usersLink">Korisnici</a></li>
        <li class="active"><a runat="server" href="CustomersPreview.aspx" id="customersLink">Mušterije</a></li>
      </ul>
      <ul class="nav navbar-nav navbar-right">
        <li><asp:Button ID="logoutButton" Text="Odjava" runat="server" CssClass="btn btn-md btnTransparent" OnClick="logoutEventMethod" CausesValidation="false" /></li>
      </ul>
    </div>
  </div>
</nav>
<!-- NAVBAR END-->
        <div class="jumbotron" style="margin-bottom:30px;">
                            <asp:Button ID="btnAddNewCustomer" CssClass="buttoncustomer btn-sm" runat="server" Text="Dodaj Novog" OnClick="btnAddNewCustomer_Click" ToolTip="Dodaj novog" />
        </div>
        <br />
        <div class="wrapper" style="text-align: center">
             <div class="fresh-table  full-screen-table" style="font-family: 'Titillium Web'; display:inline-block">
                <table id="fresh-table" class="table">
                    <asp:GridView ID="GridView1" runat="server" CssClass="grid"  AutoGenerateColumns="False" DataKeyNames="IDNumber" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="Izbrisi"/>
                            <asp:BoundField DataField="IDNumber" HeaderText="Broj LK" HeaderStyle-CssClass="dataTable" ReadOnly="True" SortExpression="IDNumber" Visible="False" />
                            <asp:BoundField DataField="FirstName" HeaderText="Ime" SortExpression="FirstName" />
                            <asp:BoundField DataField="LastName" HeaderText="Prezime"  SortExpression="LastName" />
                            <asp:BoundField DataField="Birthday" HeaderText="Datum Rodjenja"  SortExpression="Birthday" />
                            <asp:HyperLinkField DataNavigateUrlFields="IDNumber" DataNavigateUrlFormatString="CustomersPage.aspx?IDNumber={0}" ShowHeader="False" Text="Izmeni" />
                            <asp:HyperLinkField DataNavigateUrlFields="IDNumber" DataNavigateUrlFormatString="CustomerDetailedView.aspx?IDNumber={0}" Text="Detaljnije" />
                        </Columns>
                    </asp:GridView>
                </table>
            </div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MyDBConnection %>" SelectCommand="SELECT [IDNumber], [FirstName], [LastName], [Birthday], [Gender] FROM [CustomerInfo]" DeleteCommand="DELETE FROM CustomerInfo WHERE IDNumber = @IDNumber"></asp:SqlDataSource>
        
    </form>
</body>
</html>