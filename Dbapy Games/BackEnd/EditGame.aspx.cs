using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class EditGame : System.Web.UI.Page
    {
        //Checks if the given category has been selected
        private bool IsSelected(string categoryName)
        {
            if (Request.Form["Category" + categoryName] == null)
            {
                return false;
            }
            else if (Request.Form["Category" + categoryName].Equals("on"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int GameDiffiulty           = -1;
            int GameId                  = -1;

            #region Input Validation
            {
                try
                {
                    GameDiffiulty = int.Parse(Request.Form["GameDifficulty"]);
                }
                catch
                {
                    Base.VoidAlert("An error occured : Game Difficulty Parsing Error : '" + Request.Form["GameDifficulty"] + "'");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

                try
                {
                    GameId = int.Parse(Request.Form["GameId"]);
                }
                catch
                {
                    Base.VoidAlert("An error occured : Game Id Parsing Error");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

            }
            #endregion

            #region User Rank Validation
            {
                if (Base.GetUserName() == null || Base.GetUserStatus() == 0)
                {
                    Base.VoidAlert("You are not allowed here");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

                string sql = String.Format("SELECT * FROM tUsers INNER JOIN tGames ON tUsers.userId = tGames.gameUploaderId WHERE gameId = {0}" , GameId);
                DataTable temp = Base.GetDataBase(sql);
                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("An error occured");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
                
                if(Base.GetUserName() != temp.Rows[0]["userName"].ToString() && Base.GetUserStatus() != 2)
                {
                    Base.VoidAlert("You are not allowed here");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }


            }
            #endregion

            #region Editing Game
            {
                string sql = String.Format("UPDATE tGames SET gameDifficulty = '{0}' WHERE gameId = {1}" , GameDiffiulty , GameId);
                Base.ExecNonQuery(sql);
            }
            #endregion

            #region Editing Categories
            {
                string DeleteSql = String.Format("DELETE FROM tCategoryToGame WHERE gameId = {0}" , GameId);
                Base.ExecNonQuery(DeleteSql);

                string CategoriesSql = String.Format("SELECT * FROM tCategories");
                DataTable categories = Base.GetDataBase(CategoriesSql);

                foreach(DataRow r in categories.Rows)
                {
                    string CategoryName = r["categoryName"].ToString();
                    if(IsSelected(CategoryName))
                    {
                        Base.VoidAlert(CategoryName);
                        string InsertionSql = String.Format("INSERT INTO tCategoryToGame (categoryId , gameId) VALUES ({0} , {1})" , r["categoryId"] , GameId);
                        Base.ExecNonQuery(InsertionSql);
                    }
                }
            }
            #endregion

            Base.VoidAlert("Succesfully Changed Data");
            Base.VoidRedirectTo("/FrontEnd/AdminPanel.aspx");
        }
    }
}