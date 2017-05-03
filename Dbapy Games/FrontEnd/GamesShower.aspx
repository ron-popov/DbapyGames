<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GamesShower.aspx.cs" Inherits="Dbapy_Games.FrontEnd.GamesShower" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dbapy Games - Games</title>
    <link rel="stylesheet" type="text/css" href="/slick/slick.css"/>
    <link rel="stylesheet" type="text/css" href="/slick/slick-theme.css"/>
    
    <!-- Required scripts for frontend -->
    <script type="text/javascript" src="/jquery/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="/jquery/jquery-ui.min.js"></script>
    <script type="text/javascript" src="/jquery/jquery-migrate-3.0.0.min.js"></script>
    <script type="text/javascript" src="/jquery/slick.min.js"></script>
    <script type="text/javascript" src="/jquery/jquery.simpler-sidebar.min.js"></script>

    <%=css %>

    </head>

    <body>
        <%=top %>

        Sort by : <a href="/FrontEnd/GamesShower.aspx?sort=name">Name</a> , <a href="/FrontEnd/GamesShower.aspx?sort=popularity">Popularity</a> , <a href="/FrontEnd/GamesShower.aspx?sort=uploadDate">Upload Date</a>
        <br />
        <br />

        <div class="games">
            <%=links %>
        </div>

        <button type="button" id="prev-button"><img src="/Photos/LeftButton.png" height ="64px" width="64px"/></button> <button type="button" id="next-button"><img src="/Photos/RightButton.png" height ="64px" width="64px"/></button>

        <script> <!-- Games carousele -->
            $(document).ready(function(){
                $('.games').slick({
                    infinite: true,
                    slidesToShow: 5,
                    slidesToScroll: 1,
                    arrows: true,
                    swipe: false,
                    dots: false,
                    nextArrow: document.getElementById('next-button'),
                    prevArrow: document.getElementById('prev-button'),
                    appendDots: document.getElementById('slick-dots'),
                });
            });
        </script>

    </body>
</html>
