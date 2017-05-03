using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.FrontEnd
{
    public partial class Game : System.Web.UI.Page
    {
        public string title = "ERROR";
        public string top = "";
        public string css = "";

        public string game = "";
        public string sidebar = "";
        public string comments = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["game"] == null || Request.QueryString["game"] == "")
            {
                Response.Write(Base.Alert("No Game Specified"));
                try
                {
                    Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                }
                catch (NullReferenceException)
                {
                    Response.Write(Base.RedirectTo("/index.aspx"));
                }
                return;
            }

            { //Fetching game id
                DataTable inputValidation = Base.GetDataBase("SELECT gameId FROM tGames WHERE gameName='" + Request.QueryString["game"] + "'");
                if(inputValidation.Rows.Count != 1)
                {
                    Response.Write(Base.Alert("Game Not Found"));
                    try
                    {
                        Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                    }
                    catch (NullReferenceException)
                    {
                        Response.Write(Base.RedirectTo("/index.aspx"));
                    }
                    return;
                }
            }

            //From this point on all input is validated and only prints the page , game , etc ...
            string gamename = Request.QueryString["game"];
            title = gamename;
            css = Base.PrintCss();
            top = Base.PrintTop();

            #region Game
            {
                int width, height = 500;
                width = height * 4 / 3;
                game += "<embed src='/Games/" + gamename + ".swf' height=" + height + "px width=" + width + "px>";
            }
            #endregion

            #region Sidebar
            {
                string uploaderName = "";
                sidebar += "<h2><u>" + gamename + "</u></h2>";
                #region Game Uploader
                {
                    DataTable temp = Base.GetDataBase("SELECT tUsers.userName FROM tGames INNER JOIN tUsers ON tGames.gameUploaderId = tUsers.userId WHERE tgames.gameName = '" + gamename + "'");
                    uploaderName = temp.Rows[0][temp.Columns["userName"]].ToString();
                    sidebar += "Uploaded By <a href='/FrontEnd/Profile.aspx?username=" + uploaderName + "'>" + uploaderName + "</a><br/>";
                }
                #endregion

                #region Game Difficulty
                {
                    string query = String.Format("SELECT gameDifficulty FROM tGames WHERE gameName='{0}'" , gamename);
                    DataTable temp = Base.GetDataBase(query);
                    string difficulty = "";
                    switch(int.Parse(temp.Rows[0]["gameDifficulty"].ToString()))
                    {
                        case 1:
                            difficulty = "Very Easy";
                            break;
                        case 2:
                            difficulty = "Easy";
                            break;
                        case 3:
                            difficulty = "Medium";
                            break;
                        case 4:
                            difficulty = "Hard";
                            break;
                        case 5:
                            difficulty = "Very Hard";
                            break;
                        default:
                            difficulty = "ERROR";
                            break;
                    }


                    sidebar += "Difficulty Level : " + difficulty + "<br/>";
                }
                #endregion

                #region Favorites
                {
                    string query = "SELECT COUNT(tFavorite.userId) AS favorites FROM tFavorite INNER JOIN tGames ON tGames.gameId = tFavorite.gameId WHERE tGames.gameName = '" + gamename + "'";
                    DataTable temp = Base.GetDataBase(query);
                    sidebar += "Liked by " + temp.Rows[0][temp.Columns["favorites"]] + " users !<br/>";
                }
                #endregion

                #region Categories
                {
                    string query = "SELECT tCategories.categoryName FROM (( tCategories INNER JOIN tCategoryToGame ON tCategoryToGame.categoryId = tCategories.categoryId ) INNER JOIN tGames ON tGames.gameId = tCategoryToGame.gameId ) WHERE tGames.gameName = '" + gamename + "'";
                    DataTable temp = Base.GetDataBase(query);
                    sidebar += "<br/><u>Categories</u> <br/> <ul align='left'>";
                    foreach (DataRow r in temp.Rows)
                    {
                        string category = r[temp.Columns["categoryName"]].ToString();
                        sidebar += "<li><a href='/FrontEnd/Category.aspx?category=" + category + "'>" + category + "</a></li>";
                    }
                    sidebar += "</ul>";
                }
                #endregion

                #region Button
                {
                    //Fullscreen button
                    sidebar += "<form action='/FrontEnd/FullScreenGame.aspx' method='get'><input type='submit' value='FullScreen Mode'><input type='Hidden' value='" + gamename + "' name='game'></form>";
                    
                    #region Favorite
                    {
                        if (!(Base.GetUserName().Equals(null) || Base.GetUserName().Equals("")))
                        {
                            string query = String.Format("SELECT tFavorite.userId FROM (( tFavorite INNER JOIN tUsers ON tUsers.userId = tFavorite.userId ) INNER JOIN tGames ON tGames.gameId = tFavorite.gameId ) WHERE (tGames.gameName='{0}' AND tUsers.userName='{1}')", gamename, Base.GetUserName());
                            DataTable temp = Base.GetDataBase(query);
                            if (temp.Rows.Count == 1)
                            {
                                //User has favorited the game.
                                sidebar += "<form action='/BackEnd/ToggleFavorite.aspx' method='POST'><input type='submit' value='Unfavorite'><input type='hidden' name='GameName' value='" + gamename + "'></form>";
                            }
                            else
                            {
                                //User hasn't favorited the game.
                                sidebar += "<form action='/BackEnd/ToggleFavorite.aspx' method='POST'><input type='submit' value='Favorite'><input type='hidden' name='GameName' value='" + gamename + "'></form>";
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            #region Comments
            {
                string query = "SELECT tComment.commentContent , tComment.commentDate , tUsers.userName FROM (( tComment INNER JOIN tGames ON tGames.gameId = tComment.gameId ) INNER JOIN tUsers ON tUsers.userId = tComment.userId ) WHERE tGames.gameName = '" + gamename + "'";
                DataTable temp = Base.GetDataBase(query);
                foreach(DataRow r in temp.Rows)
                {
                    string content = r[temp.Columns["commentContent"]].ToString();
                    DateTime date = (DateTime)(r[temp.Columns["commentDate"]]);
                    string user = r[temp.Columns["userName"]].ToString();
                    string finalDate;
                    if (date.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                    {
                        finalDate = "Today at " + date.ToShortTimeString();
                    }
                    else
                    {
                        finalDate = date.ToShortDateString();
                    }

                    comments += "<div class = 'GameComment'>";

                    if(Base.GetUserStatus() == 2 || Base.GetUserName().Equals(user))
                    {
                        comments += "<form style='display:inline;' action='/BackEnd/DeleteComment.aspx' method='POST'><input type='submit' value = 'Delete Comment'><input type='hidden' name='Date' value='" + date + "'><input type='hidden' name='GameName' value='" + gamename + "'></form>&nbsp;";
                    }

                    comments += "<a href='/FrontEnd/Profile.aspx?username=" + user + "'>" + user + "</a>";
                    comments += " , " + finalDate + " : " + content + "</div><br/>";
                }

                comments += "<div class='GameComment'><form action='/BackEnd/Comment.aspx' method='POST'><TextArea name='CommentContent' style='resize:vertical;width:97%;'></TextArea><input type='hidden' name='GameName' value='" + gamename + "' ><br/><input type='submit'></form></div>";
            }
            #endregion
        }
    }
}