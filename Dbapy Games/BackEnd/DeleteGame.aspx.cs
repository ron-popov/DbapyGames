using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class DeleteGame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Admin Validation
            {
                if (Base.GetUserName().Equals("") || Base.GetUserName().Equals(null) || Base.GetUserStatus() != 2)
                {
                    Base.VoidAlert("You need to be an admin in order to visit this page");
                    try
                    {
                        Base.VoidRedirectTo(Request.UrlReferrer.ToString());
                    }
                    catch (NullReferenceException)
                    {
                        Base.VoidRedirectTo("/index.aspx");
                    }
                    return;
                }
            }
            #endregion

            int gameId = -1;

            #region Input Validation
            {
                try
                {
                    gameId = int.Parse(Request.Form["game"]);
                }
                catch
                {
                    Base.VoidAlert("An error occured");
                    try
                    {
                        Base.VoidRedirectTo(Request.UrlReferrer.ToString());
                    }
                    catch (NullReferenceException)
                    {
                        Base.VoidRedirectTo("/index.aspx");
                    }
                    return;
                }

                string query = String.Format("SELECT * FROM tGames WHERE gameId={0}" , gameId.ToString());
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("Game not found");
                    try
                    {
                        Base.VoidRedirectTo(Request.UrlReferrer.ToString());
                    }
                    catch (NullReferenceException)
                    {
                        Base.VoidRedirectTo("/index.aspx");
                    }
                    return;
                }
            }
            #endregion

            #region Favorites Deletion
            {
                string sql = String.Format("DELETE FROM tFavorite WHERE gameId={0}" , gameId);
                Base.ExecNonQuery(sql);
            }
            #endregion

            #region Categories Deletion
            {
                string sql = String.Format("DELETE FROM tCategoryToGame WHERE gameId={0}" , gameId);
                Base.ExecNonQuery(sql);
            }
            #endregion

            #region Game Deletion
            {
                string query = String.Format("DELETE FROM tGames WHERE gameId={0}" , gameId);
                Base.ExecNonQuery(query);
                Base.VoidAlert("Game succesfuly deleted");
            }
            #endregion

            try
            {
                Base.VoidRedirectTo(Request.UrlReferrer.ToString());
            }
            catch (NullReferenceException)
            {
                Base.VoidRedirectTo("/index.aspx");
            }
        }
    }
}