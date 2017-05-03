using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class ToggleFavorite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int gameId = -1;
            int userId = -1;
            string username;
            string gamename;

            #region Data Validation
            if (Base.GetUserName() == null || Base.GetUserName() == "")
            {
                Base.VoidAlert("You need to be logged in in order to visit this page.");
                try
                {
                    Base.VoidRedirectTo(Request.UrlReferrer.ToString());
                }
                catch(NullReferenceException)
                {
                    Base.VoidRedirectTo("/index.aspx");
                }
                return;
            }

            username = Base.GetUserName();
            gamename = Request.Form["GameName"];

            #region Fetching User Id
            {
                string query = String.Format("SELECT userId FROM tUsers WHERE userName='{0}'" , username);
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
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

                userId = int.Parse(temp.Rows[0]["userId"].ToString());
            }
            #endregion

            #region Fetching Game Id
            {
                string query = String.Format("SELECT gameId FROM tGames WHERE gameName='{0}'" , gamename);
                DataTable temp = Base.GetDataBase(query);

                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("Game Not Found");
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

                gameId = int.Parse(temp.Rows[0]["gameId"].ToString());
            }
            #endregion

            #endregion

            #region Adding Or Removing Favorite
            {
                string query = String.Format("SELECT * FROM tFavorite WHERE (userId={0} AND gameId={1})" , userId , gameId);
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count == 1)
                {
                    string deleteQuery = String.Format("DELETE FROM tFavorite WHERE (userId={0} AND gameId={1})" , userId , gameId);
                    Base.ExecNonQuery(deleteQuery);
                }
                else
                {
                    string insertQuery = String.Format("INSERT INTO tFavorite (gameId , userId) VALUES ({0} , {1})" , gameId , userId);
                    Base.ExecNonQuery(insertQuery);
                }
            }
            #endregion

            #region Redirecting Back
            try
            {
                Base.VoidRedirectTo(Request.UrlReferrer.ToString());
            }
            catch (NullReferenceException)
            {
                Base.VoidRedirectTo("/index.aspx");
            }
            #endregion

        }
    }
}