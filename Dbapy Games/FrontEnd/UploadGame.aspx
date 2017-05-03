<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadGame.aspx.cs" Inherits="Dbapy_Games.FrontEnd.UploadGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Dbapy Games - Upload Game</title>
        <%=css %>
    </head>

    <body>
        <%=top %>

        <table>
                <tr>
                    <td colspan="2"><h2><u>Uplaod A Game</u></h2></td>
                </tr>

            <form runat="server">
                <tr>
                    <td>
                        Game Name : 
                    </td>
                    <td>
                        <asp:TextBox ID="formGameName" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td>
                        Game File : 
                    </td>
                    <td>
                        <asp:FileUpload ID="gameFile" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td>
                        Game Difficulty :   
                    </td>
                    <td>
                        <asp:DropDownList id="Difficulty" 
                           runat="server">
                        
                            <asp:ListItem value="1">Very Easy</asp:ListItem>
                            <asp:ListItem value="2">Easy</asp:ListItem>
                            <asp:ListItem value="3" Selected>Medium</asp:ListItem>
                            <asp:ListItem value="4">Hard</asp:ListItem>
                            <asp:ListItem value="5">Very Hard</asp:ListItem>
                        
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td colspan="2"><h3><u>Game Categories</u></h3></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%=categories %>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnUpload" runat="server" onclick="OnUpload" Text="Upload"/>
                    </td>
                </tr>
            </form>
        
        </table>
    </body>
</html>
