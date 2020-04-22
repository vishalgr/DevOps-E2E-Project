<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginP.aspx.cs" Inherits="WebApplication1.LoginP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body{
            background-color:aliceblue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="margin:auto;border:5px solid white">
                <tr>
                  <td>
                      <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label></td>
                  <td>
                      <asp:TextBox ID="Username" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                  <td>
                      <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label></td>
                  <td>
                      <asp:TextBox ID="Password" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                  <td> 
                  </td>
                  <td>
                      <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /></td>    
                </tr>
                <tr>
                  <td>
                      <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                  
                </tr>
        </div>
    </form>
</body>
</html>
