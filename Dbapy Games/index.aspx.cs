using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games
{
    public partial class index : System.Web.UI.Page
    {
        public string topBar;
        public string cssTag;
        public string favorites;
        public string FrontPageGames;

        protected void Page_Load(object sender, EventArgs e)
        {
            topBar = Base.PrintTop();
            cssTag = Base.PrintCss();
            favorites = "";

            #region Favorites
            {
                if (!(Base.GetUserName().Equals("") || Base.GetUserName().Equals(null)))
                {
                    string username = Base.GetUserName();
                    string query = String.Format("SELECT tGames.gameName FROM ((tGames INNER JOIN tFavorite ON tGames.gameId = tFavorite.gameId) INNER JOIN tUsers ON tUsers.userId = tFavorite.userId) WHERE (tUsers.userName = '{0}')", username);
                    DataTable table = Base.GetDataBase(query);
                    if (table.Rows.Count > 0)
                    {
                        favorites = "<h2><u>Favorites</u></h2>";
                    }

                    //Going through each game and printing it.
                    foreach (DataRow r in table.Rows)
                    {
                        string gamename = r["gameName"].ToString();
                        favorites += String.Format("<a href='/FrontEnd/Game.aspx?game={0}'>{0}</a><br/>", gamename);
                    }
                }
            }
            #endregion

            #region Top Games
            {
                string sql = String.Format("SELECT gameName , COUNT(gameName) AS [Likes] FROM tGames INNER JOIN tFavorite ON tGames.gameId = tFavorite.gameId GROUP BY gameName ORDER BY COUNT(gameName) DESC");
                DataTable liked = Base.GetDataBase(sql);
                if(liked.Rows.Count < 5)
                {
                    for (int i = 0; i < liked.Rows.Count; i++)
                    {
                        FrontPageGames += "<a href='/FrontEnd/Game.aspx?game=" + liked.Rows[i]["gameName"] + "'>" + liked.Rows[i]["gameName"] + "</a><br/>";
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        FrontPageGames += "<a href='/FrontEnd/Game.aspx?game=" + liked.Rows[i]["gameName"] + "'>" + liked.Rows[i]["gameName"] + "</a><br/>";
                    }
                }
            }
            #endregion
        }
    }
}