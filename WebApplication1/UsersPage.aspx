<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersPage.aspx.cs" Inherits="ProjectX.Web.LoggedIn" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logged in</title>
    <link rel="stylesheet" href="Style/bootstrap.min.css" />
    <link rel="stylesheet" href="Style/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="Style/UsersPage.css" type="text/css" />
    <link rel="stylesheet" href="Style/SharedStyle.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- NAVBAR BEGIN-->
        <nav class="navbar navbar-inverse">
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
                <li class="active"><a runat="server" href="UsersPage.aspx" id="usersLink">Korisnici</a></li>
                <li><a runat="server" href="CustomersPreview.aspx" id="customersLink">Mušterije</a></li>
              </ul>
              <ul class="nav navbar-nav navbar-right">
                <li><asp:Button ID="Button1" Text="Odjava" runat="server" CssClass="btn btn-md btnTransparent" OnClick="logoutEventMethod" CausesValidation="false" /></li>
              </ul>
            </div>
          </div>
        </nav>
        <!-- NAVBAR END-->

        <div class="container">
            <!--Start  Container-->
            <div class="col-lg-1"></div>
            <div class="col-sm-3 col-md-4 col-lg-3"><br />
                <p>
                    <!-- Dropdown list of users -->
                    Users: 
                <asp:DropDownList ID="UsersDropDown"
                    runat="server" CssClass="dropdown"
                    AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Text="Unos novog korisnika" Value="0" />
                </asp:DropDownList>
                </p>
                <p>
                    Ime:
                            <asp:TextBox ID="FirstNameTextBox" runat="server"
                                CssClass="textbox" Style="padding-left: 5px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="FirstNameTextBox" Font-Bold="true" ForeColor="Red" ErrorMessage="Obavezno ime!" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revFirstName" runat="server" ControlToValidate="FirstNameTextBox" ErrorMessage="Dozvoljena samo slova!" ValidationExpression="^([\sA-Za-z]+)$" Display="Dynamic"></asp:RegularExpressionValidator>
                </p>
                <p>
                    Prezime:
                            <asp:TextBox ID="LastNameTextBox" runat="server"
                                CssClass="textbox" Style="padding-left: 5px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="LastNameTextBox" Font-Bold="true" ForeColor="Red" ErrorMessage="Obavezno prezime!" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revLastName" runat="server" ControlToValidate="LastNameTextBox" ErrorMessage="Dozvoljena samo slova!" ValidationExpression="^([\sA-Za-z]+)$" Display="Dynamic"></asp:RegularExpressionValidator>
                </p>
                <p>
                    Username:
                            <asp:TextBox ID="UsernameTextBox" runat="server"
                                CssClass="textbox" Style="padding-left: 5px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="UsernameTextBox" Font-Bold="true" ForeColor="Red" ErrorMessage="Obavezan Username!" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvUsername" runat="server" ControlToValidate="UsernameTextBox" OnServerValidate="ValidationOfUser" ErrorMessage="Korisnik postoji!" Display="Dynamic"></asp:CustomValidator>
                </p>
                <p>
                    Password:
                            <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="textbox"
                                TextMode="Password" Style="padding-left: 5px;"
                                title="Polje ostaviti prazno u slucaju da ne zelite da resetujete password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="PasswordTextBox" Font-Bold="true" ForeColor="Red" ErrorMessage="Obavezan Password!" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPassword" ControlToValidate="PasswordTextBox" runat="server" ErrorMessage="Password mora imati barem 8 karaktera, jednu cifru i jedan specijalni znak!" Display="Dynamic" ValidationExpression="^.*(?=.{8,})(?=.*[\d])(?=.*[\W]).*$"></asp:RegularExpressionValidator>
                </p>
                <!-- Script and links for tooltip -->
                <link rel="stylesheet" href="Style/jquery-ui.css" />
                <script src="scripts/jquery-1.8.2.js"></script>
                <script src="scripts/jquery-ui.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $('#PasswordTextBox').tooltip({
                            track: true
                        });
                    });
                </script>
                <!-- end of script for tooltip -->
                <!-- Dropdown list of user Roles -->
                Role:
            <asp:DropDownList ID="UserRolesDropDown"
                runat="server" CssClass="dropdown">
                <asp:ListItem Text="Izaberite rolu" Value="0" />
            </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="UserRolesDropDown" Font-Bold="true" ForeColor="Red" ErrorMessage="Morate izabrati rolu!" InitialValue="0"></asp:RequiredFieldValidator>
                <br /><br />
                <p style="text-align:center">
                <asp:Button ID="ConfirmButton" runat="server" Text="Unesi" CssClass="buttoncustomer btn-md" style="margin-left:5px;"  OnClick="ConfirmButton_Click" />
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="buttoncustomer btn-md" Text="Izbrisi" />
                </p>
            </div>
            <div class="col-lg-1"></div>
            <div class="col-sm-9 col-md-8 col-lg-6">
                <p>&nbsp;</p>
                <p>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grid" DataKeyNames="ID" DataSourceID="SqlDataSource1" Style="margin:auto;width:90%;">
                        <Columns>
                            <asp:BoundField DataField="Ime" HeaderText="Ime" SortExpression="Ime" />
                            <asp:BoundField DataField="Prezime" HeaderText="Prezime" SortExpression="Prezime" />
                            <%-- header text da bude username ?? --%>
                            <asp:BoundField DataField="KorisnickoIme" HeaderText="Korisničko Ime" SortExpression="KorisnickoIme" />
                            <asp:BoundField DataField="Rank" HeaderText="Pravo Pristupa" SortExpression="Rank" />
                        </Columns>
                        <RowStyle HorizontalAlign="Center" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MyDBConnection %>" SelectCommand="SELECT u.UserId as ID, u.FirstName as Ime,
                     u.LastName as Prezime, u.Username as KorisnickoIme, r.RoleName as Rank FROM Users u JOIN Roles r ON u.Role = r.RoleId ORDER BY u.Role ASC"></asp:SqlDataSource>
                </p>
            </div>
            <div class="col-lg-1"></div>
        </div>
    </form>
    <script src="scripts/jquery.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
</body>
</html>
