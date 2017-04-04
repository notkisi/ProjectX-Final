<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomersPage.aspx.cs" Inherits="ProjectX.Web.CustomersPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logged in</title>
    <!--Start stylesheets-->
    <link rel="stylesheet" href="Style/fonts.css" />

    <link rel="stylesheet" href="Style/bootstrap.min.css" />
    <link rel="stylesheet" href="Style/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="Style/CustomersPage.css" type="text/css" />
    <link rel="stylesheet" href="Style/SharedStyle.css" type="text/css" />
    <link rel="stylesheet" href="responsive-tables.css" />
    <script src="responsive-tables.js"></script>

    <script src="scripts/jquery.min.js"></script>
    <script src="scripts/bootstrapjs.js"></script>
    <script src="scripts/jquery-2.1.1.min.js"></script>
    <!--End stylesheets-->

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
                        <li>
                            <asp:Button ID="logoutButton" Text="Odjava" runat="server" CssClass="btn btn-md btnTransparent" OnClick="logoutEventMethod" CausesValidation="false" /></li>
                    </ul>
                </div>
            </div>
        </nav>
        <!-- NAVBAR END-->


        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                    <div style="margin-top: 70px;"></div>
                    <div class="col-sm-12 col-md-12 col-lg-6">
                        <p>
                            <strong>ID broj:</strong>
                            <asp:TextBox ID="tbIdNumber" runat="server" CssClass="textbox" Style="padding-left: 10px" placeholder="Broj Licne Karte"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvIdNumber" runat="server" ControlToValidate="tbIdNumber" Display="Dynamic" ErrorMessage="Obavezan broj licne karte" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvIdNumber" runat="server" ControlToValidate="tbIdNumber" OnServerValidate="CustomValidator1_ServerValidate" Display="Dynamic"></asp:CustomValidator>
                            <asp:RegularExpressionValidator ID="revIdNumber" runat="server" ControlToValidate="tbIdNumber" ErrorMessage="Dozvoljeni samo brojevi!" ValidationExpression="^\d+$" Display="Dynamic"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <strong>JMBG:</strong>
                            <asp:TextBox ID="tbJMBG" runat="server" CssClass="textbox" Style="padding-left: 10px" placeholder="JMBG"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvJMBG" runat="server" ControlToValidate="tbJmbg" Display="Dynamic" ErrorMessage="Obavezan JMBG" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvJMBG" runat="server" ControlToValidate="tbJMBG" OnServerValidate="CustomValidator1_ServerValidate" Display="Dynamic"></asp:CustomValidator>
                            <asp:RegularExpressionValidator ID="revJMBG" runat="server" ControlToValidate="tbJMBG" ErrorMessage="Dozvoljeni samo brojevi! (Tacno 13)" ValidationExpression="^\d{13}$" Display="Dynamic"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <strong>Ime:</strong>
                            <asp:TextBox ID="tbFirstName" runat="server" CssClass="textbox" Style="padding-left: 10px" placeholder="Ime"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="tbFirstName" Display="Dynamic" ErrorMessage="Obavezno ime" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revFirstName" runat="server" ControlToValidate="tbFirstName" Display="Dynamic" ValidationExpression="^([\sA-Za-z]+)$" ErrorMessage="Dozvoljena samo slova!"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <strong>Prezime:</strong>
                            <asp:TextBox ID="tbLastName" runat="server" CssClass="textbox" Style="padding-left: 10px" placeholder="Prezime"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="tbLastName" Display="Dynamic" ErrorMessage="Obavezno prezime" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revLastName" runat="server" ControlToValidate="tbLastName" Display="Dynamic" ValidationExpression="^([\sA-Za-z]+)$" ErrorMessage="Dozvoljena samo slova!"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <strong>Ime roditelja:</strong>
                            <asp:TextBox ID="tbParentName" runat="server" CssClass="textbox" Style="padding-left: 10px" placeholder="Ime Roditelja"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvParentName" runat="server" ControlToValidate="tbParentName" Display="Dynamic" ErrorMessage="Obavezno ime roditelja" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revParentName" runat="server" ControlToValidate="tbParentName" Display="Dynamic" ValidationExpression="^([\sA-Za-z]+)$" ErrorMessage="Dozvoljena samo slova!"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <strong>Datum rodjenja:</strong>
                            <asp:TextBox ID="tbBirthday" runat="server" CssClass="textbox" Style="padding-left: 10px" placeholder="Datum rodjenja"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBirthday" runat="server" ControlToValidate="tbBirthday" Display="Dynamic" ErrorMessage="Obavezan datum rodjenja" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="revBirthday" runat="server" ControlToValidate="tbBirthday" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ErrorMessage="Datum mora biti u formatu mm/dd/yyyy"></asp:CompareValidator>
                        </p>
                        <p>
                            <strong>Mesto rodjenja:</strong>
                            <asp:TextBox ID="tbPlaceOfBirth" runat="server" CssClass="textbox" Style="padding-left: 10px" placeholder="Mesto Rodjenja"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPlaceOfBirth" runat="server" ControlToValidate="tbPlaceOfBirth" Display="Dynamic" ErrorMessage="Obavezno mesto rodjenja" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPlaceOfBirth" runat="server" ControlToValidate="tbPlaceOfBirth" Display="Dynamic" ValidationExpression="^([\sA-Za-z]+)$" ErrorMessage="Dozvoljena samo slova!"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <strong>Opština:</strong>
                            <asp:TextBox ID="tbMunicipality" runat="server" CssClass="textbox" Style="padding-left: 10px" placeholder="Opstina"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMunicipality" runat="server" ControlToValidate="tbMunicipality" Display="Dynamic" ErrorMessage="Obavezno ime opstine" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMunicipality" runat="server" ControlToValidate="tbMunicipality" Display="Dynamic" ValidationExpression="^([\sA-Za-z]+)$" ErrorMessage="Dozvoljena samo slova!"></asp:RegularExpressionValidator>
                        </p>
                        <br />
                        <p align="center">
                            <asp:RadioButton ID="rbMale" runat="server" Text="&nbsp; Muško" AutoPostBack="True" OnCheckedChanged="rbMale_CheckedChanged" Checked="True" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbFemale" runat="server" Text="&nbsp; Žensko" AutoPostBack="True" OnCheckedChanged="rbFemale_CheckedChanged" />
                        </p>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-6">

                        <table style="border-spacing: 3px; border-collapse: separate; width: 100%;">
                            <tr>
                                <th>Email Adresa</th>
                                <th>Tip</th>
                            </tr>
                            <tr>
                                <td class="textboxmail">
                                    <asp:TextBox ID="tbEmail" runat="server" Style="padding-left: 10px" CssClass="dynamicTB" placeholder="Email"></asp:TextBox></td>
                                <td class="droplist">
                                    <asp:DropDownList ID="ddEmailLabel" runat="server" DataSourceID="SqlDataSource1" DataTextField="EmailLabelName" DataValueField="EmailLabelId" CssClass="dynamicDD"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MyDBConnection %>" SelectCommand="SELECT * FROM [EmailLabels]"></asp:SqlDataSource>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="tbEmail" ErrorMessage="Obavezan Email!" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail" ErrorMessage="Email u pogresnom formatu" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic"></asp:RegularExpressionValidator>

                        <asp:Table ID="EmailPanel" runat="server" Width="100%" Style="border-spacing: 3px; border-collapse: separate"></asp:Table>

                        <asp:Button ID="btnAddAnotherEmail" runat="server" CssClass="btn-sm signs" Text="+" OnClick="btnAddAnotherEmail_Click" CausesValidation="false" />
                        <asp:Button ID="btnRemoveAnotherEmail" runat="server" CssClass="btn-sm signs" Text="-" OnClick="btnRemoveAnotherEmail_Click" CausesValidation="false" />

                        <br />
                        <br />

                        <table style="border-spacing: 3px; border-collapse: separate; width: 100%;">
                            <tr>
                                <th>Adresa</th>
                                <th>Poštanski Broj</th>
                                <th>Grad</th>
                                <th>Tip</th>
                            </tr>
                            <tr>
                                <td class="textboxadresa">
                                    <asp:TextBox ID="tbAddress" runat="server" Style="padding-left: 10px" CssClass="dynamicTB" placeholder="Adresa"></asp:TextBox></td>
                                <td class="textboxzip">
                                    <asp:TextBox ID="tbZipCode" runat="server" Style="padding-left: 10px" CssClass="dynamicTB" placeholder="Postanski broj"></asp:TextBox></td>
                                <td class="textboxcity">
                                    <asp:TextBox ID="tbCity" runat="server" Style="padding-left: 10px" CssClass="dynamicTB" placeholder="Grad"></asp:TextBox></td>
                                <td class="droplist">
                                    <asp:DropDownList ID="ddAddressLabel" runat="server" DataSourceID="SqlDataSource2" DataTextField="AddressLabelName" DataValueField="AddressLabelId" CssClass="dynamicDD"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MyDBConnection %>" SelectCommand="SELECT * FROM [AddressLabels]"></asp:SqlDataSource>
                                </td>
                                <td></td>
                            </tr>
                        </table>

                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="tbAddress" Display="Dynamic" ErrorMessage="Obavezna adresa!" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="tbZipCode" Display="Dynamic" ErrorMessage="Obavezan Zip kod!" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="tbCity" Display="Dynamic" ErrorMessage="Obavezan grad!" ForeColor="Red"></asp:RequiredFieldValidator>

                        <asp:Table ID="AddressPanel" runat="server" Width="100%" Style="border-spacing: 3px; border-collapse: separate"></asp:Table>

                        <asp:Button ID="btnAddAnotherAddress" CssClass="btn-sm signs" runat="server" Text="+" OnClick="btnAddAnotherAddress_Click" CausesValidation="false" />
                        <asp:Button ID="btnRemoveAnotherAddress" CssClass="btn-sm signs" runat="server" Text="-" OnClick="btnRemoveAnotherAddress_Click" CausesValidation="false" />

                        <br />
                        <br />

                        <table style="border-spacing: 3px; border-collapse: separate; width: 100%;">
                            <tr>
                                <th>Telefon</th>
                                <th>Lokal</th>
                                <th>Tip</th>
                            </tr>
                            <tr>
                                <td class="textboxphone">
                                    <asp:TextBox ID="tbPhone" runat="server" Style="padding-left: 10px" CssClass="dynamicTB" placeholder="Telefon"></asp:TextBox></td>
                                <td class="textboxlokal">
                                    <asp:TextBox ID="tbLocal" runat="server" Style="padding-left: 10px" CssClass="dynamicTB" placeholder="Lokal"></asp:TextBox></td>
                                <td class="droplist">
                                    <asp:DropDownList ID="ddPhoneLabel" runat="server" DataSourceID="SqlDataSource3" DataTextField="PhoneLabelName" DataValueField="PhoneLabelId" CssClass="dynamicDD"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:MyDBConnection %>" SelectCommand="SELECT * FROM [PhoneLabels]"></asp:SqlDataSource>
                                </td>
                                <td></td>
                            </tr>
                        </table>

                        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="tbPhone" Display="Dynamic" ErrorMessage="Obavezan broj telefona!" ForeColor="Red"></asp:RequiredFieldValidator>

                        <asp:Table ID="PhonePanel" runat="server" Width="100%" Style="border-spacing: 3px; border-collapse: separate"></asp:Table>

                        <asp:Button ID="btnAddAnotherPhone" CssClass="btn-sm signs" runat="server" Text="+" OnClick="btnAddAnotherPhone_Click" CausesValidation="false" />
                        <asp:Button ID="btnRemoveAnotherPhone" CssClass="btn-sm signs" runat="server" Text="-" OnClick="btnRemoveAnotherPhone_Click" CausesValidation="false" />

                        <br />
                        <br />

                        <p>
                            <strong>Beleška</strong>
                            <asp:TextBox ID="tbNote" runat="server" CssClass="textbox" placeholder="Beleske" Style="padding-left: 10px"></asp:TextBox>
                        </p>
                        <asp:Image ID="Image1" runat="server" Height="100px" Width="100px" Style="border-radius: 5px" />
                        <br />
                        <br />

                        <asp:FileUpload CssClass="" ID="ImageUpload" runat="server" />
                        <asp:RegularExpressionValidator ID="revPicture" runat="server" ControlToValidate="ImageUpload" ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([pP][nN][gG])$)" ErrorMessage="Dozvoljeni formati: .jpg .png" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvPicture" runat="server" ControlToValidate="ImageUpload" Display="Dynamic" ErrorMessage="Obavezna slika!" ForeColor="Red"></asp:RequiredFieldValidator>

                        <br />
                        <br />
                        <p align="center">
                            <asp:Button ID="Button1" runat="server" CssClass="buttoncustomer btn-lg" Text="Unesi"/>
                        </p>
                </fieldset>

                </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Button1" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="col-lg-1">
        </div>
    </form>
</body>
</html>