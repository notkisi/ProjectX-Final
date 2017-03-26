<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WebApplication1.Error" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Greška</title>

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
                <h1 style="font-size:4em;margin-bottom:30px">Oops, došlo je do greške...</h1>
                <h5 style="margin-bottom:40px">Pronašli ste homepage našeg junior developera!</h5>                
                <div class="form-actions" style="margin-bottom:60px">  
                    <a href="CustomersPreview.aspx" class="btn blue uppercase" style="width:70%">Povratak na početnu stranu</a>
                </div>
            </form>
        </div>
    </div>
  </div>
      
</body>
</html>