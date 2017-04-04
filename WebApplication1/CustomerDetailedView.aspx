<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerDetailedView.aspx.cs" Inherits="WebApplication1.CustomerDetailedView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Logged in</title>
    <link rel="stylesheet" href="Style/bootstrap.min.css" />
    <link rel="stylesheet" href="Style/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="Style/CustomerDetailedView.css" type="text/css" />
    <link rel="stylesheet" href="Style/SharedStyle.css" type="text/css" />
    <script src="scripts/jquery.min.js"></script>
    <script src="scripts/bootstrapjs.js"></script>
    <script src="scripts/jquery-2.1.1.min.js"></script>
    <style>
        body {
    font-family:'Titillium Web',sans-serif;
    
    background-size: 100%;
    padding: 0;
    margin: 0;
}
    </style>
    <script>
        function PrintCustomer() {
            window.print();
        }
    </script>
</head>

<body>
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
                <li><a runat="server" href="CustomersPreview.aspx" id="customersLink">Mušterije</a></li>
              </ul>
              <ul class="nav navbar-nav navbar-right">
                <li><asp:Button ID="logoutButton" Text="Odjava" runat="server" CssClass="btn btn-md btnTransparent" OnClick="logoutEventMethod" CausesValidation="false" /></li>
              </ul>
            </div>
          </div>
        </nav>
        <!-- NAVBAR END-->

        <div class="jumbotron">
            <div class="container">
                <a class="no-print" href="javascript:history.go(-1)">Nazad</a>
                &nbsp;
            </div>
        </div>
        <div class="col-sm-6 col-md-8 col-lg-6 text-center">
            <p align="center">
                <br /><br />
                <asp:Image ID="Image1" runat="server" class="img-rounded img-responsive no-print" Width="55%"  /><br />
            </p>
            <asp:GridView ID="GridView3" GridLines="None" runat="server" CssClass="grid" HorizontalAlign="Center" AutoGenerateColumns="False" DataKeyNames="AddressLabelId" DataSourceID="SqlDataSource1" Font-Size="Small">
                <Columns>
                    <asp:BoundField DataField="Address" HeaderText="Adresa" SortExpression="Address" />
                    <asp:BoundField DataField="ZipCode" HeaderText="Zip" SortExpression="ZipCode" />
                    <asp:BoundField DataField="City" HeaderText="Grad" SortExpression="City" />
                    <asp:BoundField DataField="AddressLabelName" HeaderText="Tip" SortExpression="AddressLabelName" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MyDBConnection %>" SelectCommand="SELECT * FROM [Addresses]
JOIN [AddressLabels] ON [Addresses].[LabelId] = [AddressLabels].[AddressLabelId]
WHERE ([CustomerId] = @CustomerId)">
                <SelectParameters>
                    <asp:SessionParameter Name="CustomerId" SessionField="CustomerId" />
                </SelectParameters>
            </asp:SqlDataSource>
            <p>
                <asp:GridView ID="GridView2" GridLines="None" runat="server" HorizontalAlign="Center" CssClass="grid" AutoGenerateColumns="False" DataKeyNames="EmailLabelId" DataSourceID="SqlDataSource2" Font-Size="Small">
                    <Columns>
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="EmailLabelName" HeaderText="Tip" SortExpression="EmailLabelName" />
                    </Columns>
                </asp:GridView>
            </p>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MyDBConnection %>" SelectCommand="SELECT * FROM [Emails]
JOIN [EmailLabels] ON [Emails].[LabelId] = [EmailLabels].[EmailLabelId]
WHERE ([CustomerId] = @CustomerId)">
                <SelectParameters>
                    <asp:SessionParameter Name="CustomerId" SessionField="CustomerId" />
                </SelectParameters>
            </asp:SqlDataSource>
            <p>
                <asp:GridView ID="GridView4" GridLines="None" CssClass="grid" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" DataKeyNames="PhoneLabelId" DataSourceID="SqlDataSource3" Font-Size="Small">
                    <Columns>
                        <asp:BoundField DataField="PhoneNumber" HeaderText="Broj Telefona" SortExpression="PhoneNumber" />
                        <asp:BoundField DataField="Local" HeaderText="Lokal" SortExpression="Local" />
                        <asp:BoundField DataField="PhoneLabelName" HeaderText="Tip" SortExpression="PhoneLabelName" />
                    </Columns>
                </asp:GridView>
            </p>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:MyDBConnection %>" SelectCommand="SELECT * FROM [Phones]
JOIN [PhoneLabels] ON [Phones].[LabelId] = [PhoneLabels].[PhoneLabelId]
WHERE ([CustomerId] = @CustomerId)">
                <SelectParameters>
                    <asp:SessionParameter Name="CustomerId" SessionField="CustomerId" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div class="col-sm-6 col-md-4 col-lg-6">
            <div class="text-center">
                <br /><br />
                <div class="boja">
                
                <p>
                    <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Text="Ime :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbFirstName" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox2" runat="server" BorderStyle="None" Text="Prezime :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbLastName" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" Text="Datum Rodjenja :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbBirthday" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox4" runat="server" BorderStyle="None" Text="Mesto Rodjenja :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbPlaceOfBirth" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox5" runat="server" BorderStyle="None" Text="Opština :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbMunicipality" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox6" runat="server" BorderStyle="None" Text="Ime roditelja :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbParentName" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox7" runat="server" BorderStyle="None" Text="JMBG :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbJMBG" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox8" runat="server" BorderStyle="None" Text="Broj lične karte :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbIdNumber" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox9" runat="server" BorderStyle="None" Text="Pol :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbGender" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                <p>
                    <asp:TextBox ID="TextBox10" runat="server" BorderStyle="None" Text="Beleška :"
                        ReadOnly="True" CssClass="textbox1" Style="padding-left: 10px"></asp:TextBox>
                    <asp:TextBox ID="tbNote" runat="server" BorderStyle="None"
                        ReadOnly="True" CssClass="textbox" Style="padding-left: 10px"></asp:TextBox>
                </p>
                    </div>
                <p style="text-align:center;">
                    &nbsp;
                    <br />
            <button id="btnPrint" class="buttoncustomer btn-lg no-print" onclick="PrintCustomer()"> Štampa</button>
                </p>
            </div>
        </div>
        <!--<div class="col-lg-1"></div>-->

    </form>
</body>
</html>