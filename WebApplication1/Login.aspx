<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjectX.Web.Login" %>
 
<!doctype html>
<html lang="en" style="background: url(style/bg.jpg) no-repeat center center fixed; -webkit-background-size: cover; -moz-background-size: cover; -o-background-size: cover; background-size: cover;">
<head>
    <title>LogIn</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
 
 
    <link href="Style/bootstrap.css" rel="stylesheet" />
 
    <link href="Style/login-register.css" rel="stylesheet" />
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css">
 
    <script src="scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="scripts/bootstrap.js" type="text/javascript"></script>
    <script src="scripts/login-register.js" type="text/javascript"></script>
 
    <script type="text/javascript">
        function shakeModal() {
            $('#loginModal .modal-dialog').addClass('shake');
            $('input[type="password"]').val('');
            setTimeout(function () {
                $('#loginModal .modal-dialog').removeClass('shake');
            }, 500);
        }
    </script>
 
</head>
<body onload="openLoginModal();">
    <div class="container">
        <div class="row">
            <div class="col-sm-4"></div>
            <div class="col-sm-4">
            <div class="col-sm-4"></div>
        </div>
         <div class="modal fade login" id="loginModal" data-toggle="loginModal" data-target="#loginModal" data-backdrop="static" data-keyboard="false">
         <div class="modal-dialog login animated">
         <div class="modal-content">
         <div class="modal-body">
         <div class="box">
         <div class="content">
         <div class="division">
         </div>
         <div class="form loginBox"> 
             <form method="post" accept-charset="UTF-8" runat="server">
           
               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                   <ContentTemplate>
                       <fieldset>
              <asp:TextBox ID="usernameTextBox" CssClass="form-control" placeholder="Korisnicko ime"
                           Style="padding-left: 10px" runat="server" /><br />
 
              <asp:TextBox ID="passwordTextBox" TextMode="Password" CssClass="form-control"
                           Style="padding-left: 10px" placeholder="Lozinka" runat="server" />
 
            <div class="login-btn "> <br />
                <p align="center">
                   <asp:Button ID="Button1" Text="Prijava"  runat="server" CssClass="btn btn-login btn-default"
                              OnClick="submitEventMethod"  /></p>
                       </fieldset>
                       </ContentTemplate>
                       </asp:UpdatePanel>
            </form>                                        
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
</body>
</html>