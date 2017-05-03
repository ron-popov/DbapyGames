<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditGame.aspx.cs" Inherits="Dbapy_Games.FrontEnd.EditGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Dbapy Games - Edit A Game</title>
        <%=css %>

        <script type="text/javascript">
            function CheckForm()
            {
                var form = document.forms["EditGameForm"];
                if(isNaN(TryParseInt(form["GameDifficulty"].value , null)))
                {
                    alert("Error in difficulty value");
                    return false;
                }
                return true;
            }
        </script>
    </head>
    
    
    <body>
        <%=top %>
        <h2><u>Edit A Game : <%=oldGameName %></u></h2>
        <form name = "EditGameForm" action="/BackEnd/EditGame.aspx" method="post" onsubmit="return CheckForm()">
            Game Difficulty : <input name="GameDifficulty" list="Difficulty" value="<%=oldGameDifficulty%>"/>
            <datalist id="Difficulty">
                <option value="1">Very Easy</option>
                <option value="2">Easy</option>
                <option value="3">Medium</option>
                <option value="4">Hard</option>
                <option value="5">Very Hard</option>
            </datalist>
            <br />
            <br />

            <h3><u>Game Categories</u></h3>
            <%=oldGameCategories %>

            <input type="hidden" name="GameId" value="<%=GameIdDisplay %>"/>

            <input type="submit" value="Update"/>
        </form>
    </body>
</html>
