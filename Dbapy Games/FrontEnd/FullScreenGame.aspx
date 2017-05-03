<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullScreenGame.aspx.cs" Inherits="Dbapy_Games.FrontEnd.FullScreenGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Dbapy Games</title>
        <%=css %>
    </head>

    <body onload="onload()">
        
        <script type="text/javascript">
            function onload()
            {
                var game = document.getElementById('Game');
                var width = game.getAttribute('width');
                var heigth = game.getAttribute('height');
                var ratio = parseFloat(width) / parseFloat(heigth);

                var referenceHeigth = screen.height * 0.75;
                game.setAttribute('height', referenceHeigth);
                game.setAttribute('width', referenceHeigth * ratio);
            }
        </script>
        
        <%=game %>


        <%=backButton %>
    </body>
</html>
