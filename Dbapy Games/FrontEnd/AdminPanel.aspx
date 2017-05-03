<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Dbapy_Games.BackEnd.AdminPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Dbapy Games - Admin Panel</title>
        <%=css %>

        <script type="text/javascript">
            function CheckDeleteUser()
            {
                var AreYouSure = confirm('WARNING : DELETING A USER WILL REMOVE ALL OF IT\'S COMMENTS AND GAMES \n Are you sure you would like to delete this user ?');
                if(! AreYouSure)
                {
                    return false;
                }

                if(document.forms["DeleteUserForm"]["username"].value.length > 32 || document.forms["DeleteUserForm"]["username"].value.length < 6)
                {
                    alert("Error in username selection");
                    return false;
                }
                return true;
            }

            function CheckChangeRank()
            {
                var form = document.forms["ChangeRankForm"];
                if(form["username"].value.length > 32 || form["username"].value.length < 6)
                {
                    alert("Error in username selection");
                    return false;
                }
                if(isNaN(parseInt(form["rank"].value , null)))
                {
                    alert("Error in rank selection");
                    return false;
                }
                return true;
            }

            function CheckDeleteGame()
            {
                if(isNaN(parseInt(document.forms["DeleteGameForm"]["game"].value , null)))
                {
                    alert("Error in game selection");
                    return false;
                }
                return true;
            }

            function CheckAddCategory()
            {
                if(document.forms["AddCategoryForm"]["categoryName"].value.length < 4)
                {
                    alert("Category name is too short");
                    return false;
                }
                return true;
            }

            function CheckRemoveCategory()
            {
                if(isNaN(parseInt(document.forms["RemoveCategoryForm"]["categoryId"].value, null)))
                {
                    alert("Error in cateogry selection");
                    return false;
                }
                return true;
            }

            function CheckEditGame()
            {
                if (isNaN(parseInt(document.forms["EditGameForm"]["gameId"].value , null)))
                {
                    alert("Error in game selection");
                    return false;
                }
                return true;
            }
        </script>
    </head>
    
    <body>
        <%=top %>
        
        <table width="100%">
            <tr>
                <td colspan="2">
                    <u><h2> Users Info</h2></u>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <%=usersTable %>
                </td>
            </tr>

            <tr><td><br /><br /><hr /></td></tr>

            <tr>
                <td colspan ="2">
                    <u><h2> Delete User </h2></u>
                    WARNING : DELETING A USER WILL REMOVE ALL OF IT'S COMMENTS AND GAMES
                </td>
            </tr>


            <tr>
                <td>
                    <form name = "DeleteUserForm" action="/BackEnd/DeleteUser.aspx" method="POST" onsubmit="return CheckDeleteUser()">
                        <input list="users" name="username">
                            <datalist id="users">
                                <%=users %>
                            </datalist>
                        <input type="submit" value="Delete">
                    </form>
                </td>
            </tr>

            <tr><td><br /><br /><hr /></td></tr>

            <tr>
                <td>
                    <u><h2>Change User Rank</h2></u>
                </td>
            </tr>

            <tr>
                <td>
                    <form name = "ChangeRankForm" method="post" action="/BackEnd/UpdateUserRank.aspx" onsubmit="return CheckChangeRank()">
                        <input list="users" name="username"/>
                        <datalist id="users">
                            <%=users %>
                        </datalist>

                        <input list="ranks" name="rank"/>
                        <datalist id="ranks">
                            <option value="0">Game Tester</option>
                            <option value="1">Game Developer</option>
                            <option value="2">Administrator</option>
                        </datalist>

                        <input type="submit" value="Update"/>
                    </form>
                </td>
            </tr>

            <tr><td><br /><br /><hr /></td></tr>

            <tr>
                <td>
                    <u><h2>Delete Game</h2></u><br />
                </td>
            </tr>

            <tr>
                <td>
                    <form name = "DeleteGameForm" action="/BackEnd/DeleteGame.aspx" method="post" onsubmit="return CheckDeleteGame()">
                        <input list="games" name="game"/>
                        <datalist id="games">
                            <%=games %>
                        </datalist>

                        <input type="submit" value="Delete"/>
                    </form>
                </td>
            </tr>

            <tr><td><br /><br /><hr /></td></tr>

            <tr>
                <td>
                    <h2><u>Add Category</u></h2>
                </td>
            </tr>

            <tr>
                <td>
                    <form name = "AddCategoryForm" method="post" action="/BackEnd/AddCategory.aspx" onsubmit="return CheckAddCategory()">
                        <input type="text" name="categoryName"/>
                        <input type="submit"/>
                    </form>
                </td>
            </tr>

            <tr><td><br /><br /><hr /></td></tr>

            <tr>
                <td>
                    <h2><u>Remove Category</u></h2>
                </td>
            </tr>

            <tr>
                <td>
                    <form name="RemoveCategoryForm" method="post" action="/BackEnd/RemoveCategory.aspx" onsubmit="return CheckRemoveCategory()">
                        <input list="categories" name="categoryId"/>
                        <datalist id="categories">
                            <%=categories %>
                        </datalist>
                        <input type="submit"/>
                    </form>
                </td>
            </tr>

            <tr><td><br /><br /><hr /></td></tr>

            <tr>
                <td>
                    <h2><u>Edit A Game</u></h2>
                </td>
            </tr>

            <tr>
                <td>
                    <form name="EditGameForm" method="post" action="/FrontEnd/EditGame.aspx" onsubmit="return CheckEditGame()">
                        <input list="games" name="GameId"/>
                        <datalist id="games">
                            <%=games %>
                        </datalist>
                        <input type="submit"/>
                    </form>
                </td>
            </tr>
        </table>
    
    </body>
</html>
