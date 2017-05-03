<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="Dbapy_Games.FrontEnd.Game" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Dbapy Games - <%=title %></title>
        <%=css %>
    </head>
    
    <body>
        <%=top %>
        <table>
            <tr>
                <td>
                    <div class="GameSidebar">
                        <%=sidebar %>
                    </div>
                </td>
                <td>
                    <%=game %>
                </td>
            </tr>
            <tr>
                <td colspan ="2">
                    <h2><u>Comments</u></h2>
                    <%=comments %>
                </td>
            </tr>
        </table>
    </body>
</html>
