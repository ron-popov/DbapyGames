<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="Dbapy_Games.FrontEnd.Category" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%=css %>
    <title>Dbapy Games - Categories</title>
    <link rel="stylesheet" type="text/css" href="/slick/slick.css"/>
    <link rel="stylesheet" type="text/css" href="/slick/slick-theme.css"/>
    
    <!-- Required scripts for frontend -->
    <script type="text/javascript" src="/jquery/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="/jquery/jquery-ui.min.js"></script>
    <script type="text/javascript" src="/jquery/jquery-migrate-3.0.0.min.js"></script>
    <script type="text/javascript" src="/jquery/slick.min.js"></script>
    <script type="text/javascript" src="/jquery/jquery.simpler-sidebar.min.js"></script>

</head>
    <body>
        <%=top %>

        <div class="categories">
            <%=games %>
        </div>
        
        <br />
        <button type="button" id="prev-button"><img src="/Photos/LeftButton.png" height ="64px" width="64px"/></button> <button type="button" id="next-button"><img src="/Photos/RightButton.png" height ="64px" width="64px"/></button>
   
        
        <script> <!-- Games carousele -->
            $(document).ready(function(){
                $('.categories').slick({
                    infinite: true,
                    slidesToShow: 5,
                    slidesToScroll: 1,
                    arrows: true,
                    swipe: false,
                    nextArrow: document.getElementById('next-button'),
                    prevArrow: document.getElementById('prev-button'),
                });
            });
        </script>
    </body>
</html>
