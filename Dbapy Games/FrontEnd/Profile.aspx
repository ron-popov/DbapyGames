<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Dbapy_Games.FrontEnd.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dbapy Games - <%=title%></title>
    <%=css %>
</head>
    <body onresize="onresize()">
        <%=top %>

        <h2><u><%=title %></u></h2>

        <table width="100%">
            <tr>
                <td>
                    <div class="UserInfoPanel"><%=userInfoPanel %></div>
                </td>
                <td>
                    <div class="UserFavoritesPanel"><%=userFavoritesPanel %></div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div class="UserCommentsPanel" width="100%">
                        <%=userCommentsPanel %>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div id="UserSettingsPanel" width="100%" runat="server">
                        <form action="/BackEnd/ChangePassword.aspx" method="post">
                            <h3><u>Change Password</u></h3>
                            <label for='OldPassword'>Old Password : </label><input id='OldPassword' name='OldPassword' type='password'><br/>
                            <label for='NewPassword'>New Password : </label><input id='NewPassword' name='NewPassword' type='password'><br/>
                            <label for='RepNewPassword'>Repeat New Password : </label><input id='RepNewPassword' name='RepNewPassword' type='password'><br/>
                            <input type = 'Submit' value = 'Change'>
                        </form>

                        <form runat="server" id="UploadImageForm">
                            <h3><u>Change Image</u></h3>
                            <asp:FileUpload ID="gameFile" runat="server" /><br />
                            <asp:Button runat="server" OnClick="ChangeImage" Text="Upload"/>
                        </form>
                    </div>
                </td>
            </tr>
        </table>
    </body>
</html>