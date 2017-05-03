using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.FrontEnd
{
    public partial class EditGame : System.Web.UI.Page
    {
        public string css                       = "";
        public string top                       = "";
        public string oldGameName               = "";
        public string oldGameDifficulty         = "";
        public string oldGameCategories         = "";
        public string GameIdDisplay             = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string gamename                     = "";
            DataTable GameData                  = null;
            int userId                          = -1;
            int oldGameDifficultyValue          = -1;
            int gameId                          = -1;

            #region User Validation
            {
                if(Request.Form["GameId"] == null)
                {
                    Base.VoidAlert("An error occured");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
                else
                {
                    try
                    {
                        gameId = int.Parse(Request.Form["GameId"]);
                    }
                    catch
                    {
                        Base.VoidAlert("An error occured");
                        Base.VoidRedirectTo("/index.aspx");
                        return;
                    }
                }

                string sql = String.Format("SELECT tUsers.userName , tGames.gameName FROM tGames INNER JOIN tUsers ON tGames.gameUploaderId = tUsers.userId WHERE gameId = {0}" , gameId);
                DataTable temp = Base.GetDataBase(sql);
                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("An error occured : Game not found");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

                string username = temp.Rows[0]["userName"].ToString();

                if(Base.GetUserName() == null)
                {
                    Base.VoidAlert("You need to be logged in in order to visit this page");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
                else if (Base.GetUserName() != username && Base.GetUserStatus() != 2)
                {
                    Base.VoidAlert("Your rank is not high enough");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

                gamename = temp.Rows[0]["gameName"].ToString();
            }
            #endregion

            css = Base.PrintCss();
            top = Base.PrintTop();

            #region Fetching Game Data
            {
                string sql = String.Format("SELECT * FROM tGames WHERE gameName = '{0}'" , gamename);
                GameData = Base.GetDataBase(sql);
                if(GameData.Rows.Count != 1)
                {
                    Base.VoidAlert("An error occured : Game Not Found Error");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
                userId = int.Parse(GameData.Rows[0]["gameUploaderId"].ToString());
                oldGameName = GameData.Rows[0]["gameName"].ToString();
                GameIdDisplay = GameData.Rows[0]["gameId"].ToString();

                string[] difficulties = new string[] {"Very Easy" , "Easy" , "Medium" , "Hard" , "Very Hard"};
                oldGameDifficulty = GameData.Rows[0]["gameDifficulty"].ToString();
            }
            #endregion

            #region Fetching Game Categories
            {
                string sql = String.Format(
                    @"SELECT categoryName 
                    FROM tCategories
                    INNER JOIN tCategoryToGame
                    ON tCategories.categoryId = tCategoryToGame.categoryId
                    WHERE gameId = {0}" , gameId);
                DataTable temp = Base.GetDataBase(sql);

                List<string> gameCategories = new List<string>();
                foreach(DataRow r in temp.Rows)
                {
                    gameCategories.Add(r["categoryName"].ToString());
                }

                sql = String.Format("SELECT categoryName FROM tCategories");
                temp = Base.GetDataBase(sql);

                foreach(DataRow r in temp.Rows)
                {
                    string categoryName = r["categoryName"].ToString();
                    if(gameCategories.Contains(categoryName))
                    {
                        oldGameCategories += "<input type='checkbox' name='Category" + categoryName + "'checked>";
                    }
                    else
                    {
                        oldGameCategories += "<input type='checkbox' name='Category" + categoryName + "'>";
                    }
                    oldGameCategories += categoryName;
                    oldGameCategories += "<br/>";
                }
            }
            #endregion

        }
    }
}