using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.FrontEnd
{
    public partial class Profile : System.Web.UI.Page
    {
        public string title = "ERROR";
        public string css = "";
        public string top = "";
        public string userInfoPanel = "";
        public string userFavoritesPanel = "";
        public string userCommentsPanel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable table = null;
            string username = Request.QueryString["username"];
            if (username != null && username != "")
            {
                table = Base.GetDataBase("SELECT * FROM tUsers WHERE userName='" + username + "'");
                if(table.Rows.Count != 1)
                {
                    Response.Write(Base.Alert("Username Not Found"));
                    try
                    {
                        Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                    }
                    catch (NullReferenceException)
                    {
                        Response.Write(Base.RedirectTo("/index.aspx"));
                    }
                }
            }
            else
            {
                Response.Write(Base.Alert("No Username Specified"));
                try
                {
                    Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                }
                catch(NullReferenceException)
                {
                    Response.Write(Base.RedirectTo("/index.aspx"));
                }
                return;
            }


            css = Base.PrintCss();
            top = Base.PrintTop();

            //All code from this point on is responsible for displaying the profile.
            title = username;

            #region User Info Panel
            userInfoPanel += "<br/><img alt='User does not have a profile picture' src='/ProfilePictures/" + username + ".png' width=300px height=300px>";
            userInfoPanel += "<div text-align='left'>";
            DataTable temp = Base.GetDataBase("SELECT userRank , userSignUpDate FROM tUsers WHERE userName='" + username + "'");
            #region User Rank
            {
                if(temp.Rows.Count != 1)
                {
                    Response.Write(Base.Alert("An error occured while retrieving user rank"));
                    try
                    {
                        Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                    }
                    catch(NullReferenceException)
                    {
                        Response.Write(Base.RedirectTo("/index.aspx"));
                    }
                    return;
                }
                string rank = temp.Rows[0][temp.Columns["userRank"]].ToString();
                if(rank == "0")
                {
                    userInfoPanel += "<div text-color='#00ff00'><u>Gamer</u></div>";
                }
                else if(rank == "1")
                {
                    userInfoPanel += "<div text-color='#0000ff'><u>Content Creator</u></div>";
                }
                else if(rank == "2")
                {
                    userInfoPanel += "<div text-color='#ff0000'><u>Administrator</u></div>";
                }
                else
                {
                    userInfoPanel += "<div color='#ffffff'>Hacker</div>";
                }
            }
            #endregion

            #region User SignUp Date
            {
                DateTime date = (DateTime)(temp.Rows[0][temp.Columns["userSignUpDate"]]);
                userInfoPanel += "Has been a member for " + (DateTime.Now - date).Days + " Days";
            }
            #endregion

            userInfoPanel += "</div>";
            #endregion

            #region User Favorites Panel
            userFavoritesPanel += "<h3><u>Favorites</u></h3>";
            temp = Base.GetDataBase("SELECT tGames.gameName FROM ((tGames INNER JOIN tFavorite ON tFavorite.gameId = tGames.gameId ) INNER JOIN tUsers ON tUsers.userId = tFavorite.userId ) WHERE tUsers.userName = '" + username + "'");
            userFavoritesPanel += "<ul align='left'>";

            int counter = 0;
            foreach(DataRow r in temp.Rows)
            {
                if(counter == 10)
                {
                    break;
                }
                string gamename = r[temp.Columns["gameName"]].ToString();
                userFavoritesPanel += "<li><a href='/FrontEnd/Game.aspx?game=" + gamename + "'>" + gamename + "</a></li>";
                counter++;
            }

            userFavoritesPanel += "</ul>";
            #endregion

            #region User Comments Panel
            {
                temp = Base.GetDataBase("SELECT tComment.commentContent , tGames.gameName FROM ((tComment INNER JOIN tUsers ON tUsers.userId = tComment.userId) INNER JOIN tGames ON tComment.gameId = tGames.gameId) WHERE tUsers.userName = '" + username + "'");
                userCommentsPanel += "<h3><u>Recent Comments</u></h3>";
                userCommentsPanel += "<ul align='left'>";
                foreach(DataRow r in temp.Rows)
                {
                    userCommentsPanel += "<li><a href='/FrontEnd/Game.aspx?game=" + r[temp.Columns["gameName"]] + "'>" + r[temp.Columns["gameName"]] + "</a> - " + r[temp.Columns["commentContent"]].ToString() + "</li>";
                }
                userCommentsPanel += "</ul>";
            }
            #endregion

            #region User Settings
            {
                if(Base.GetUserName() != username)
                {
                    Control UserSettings = Page.FindControl("UserSettingsPanel");
                    UserSettings.Controls.Clear();
                }
            }
            #endregion

        }

        protected void ChangeImage(object sender, EventArgs e)
        {
            if(gameFile != null && gameFile.HasFile)
            {
                string username = Base.GetUserName();
                if(username == null)
                {
                    Base.VoidAlert("An error occured");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

                gameFile.SaveAs(Server.MapPath(String.Format("/ProfilePictures/{0}.png", username)));
            }
        }
    }
}