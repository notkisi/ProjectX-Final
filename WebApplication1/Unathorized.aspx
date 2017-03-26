<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unathorized.aspx.cs" Inherits="WebApplication1.Unathorized" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Neautorizovan pristup!</title>

    <link href="Style/bootstrap.css" rel="stylesheet" />
    <link href="Style/error.css" rel="stylesheet" />
    <link href="Style/SharedStyle.css" rel="stylesheet" />
    <script src="scripts/jquery-3.2.0.min.js"></script>
    <script src="scripts/bootstrap.js"></script> 
</head>
<body>

  <div class="page-lock">
    <div class="page-body">
        <div class="lock-body">                    
            <form class="lock-form pull-left" runat="server" autocomplete="off" style="width: 100%;text-align:center;color:white">
                <h1 style="font-size:4em;margin-bottom:30px">Neautorizovan pristup</h1>
                <h5 style="margin-bottom:40px">Istekla vam je sesija ili ste pokušali da pristupite strani za koju Vam nije omogućen pristup!</h5>                
                <div class="form-actions" style="margin-bottom:60px">  
                    <a href="CustomersPreview.aspx" class="btn red uppercase" style="width:70%">Povratak na početnu stranu</a> 
                    <h5> ILI </h5>
                    <a href="Login.aspx" class="btn red uppercase" style="width:70%">Ponovna prijava</a> 
                </div>
            </form>
        </div>
    </div>
  </div>
      
</body>
</html>


