<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Dbapy_Games.FrontEnd.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dbapy Games - Register</title>
    <%=css %>
</head>
<body>

    <%=top %>
    
    <h2 align="center"><u>Registration Form</u></h2>
    <table>
    <form name='RegisterForm' align="center" onsubmit='return ValidateForm()' action="/BackEnd/Register.aspx" method="post">
        <tr><td>Username :</td>
        <td><input type="text" name="username" minLength=6 maxLength=32 required/></td></tr>
        <tr><td>Password :</td>
        <td><input type="password" name="password" minLength=8 maxLength=32 required/></td></tr>
        <tr><td>Repeat Password :</td>
        <td><input type="password" name="rPassword" minLength=8 maxLength=32 required/></td></tr>
        <tr><td>Email :</td>
        <td><input type="email" name="userEmail" required/></td></tr>
        <tr><td colspan="2"><input type="submit"/></td></tr>
    </form>
    </table>

    <script type='text/javascript'>
	    function ValidateForm(){
		    var Password = document.forms["RegisterForm"]["Password"].value;
		    var rPassword = document.forms["RegisterForm"]["rPassword"].value;
            if (Password != rPassword)
            {
                alert("Passwords do not match");
                return false;
            }
		    return true;
	    }
    </script>
</body>
</html>
