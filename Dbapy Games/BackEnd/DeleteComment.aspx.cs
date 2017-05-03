using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class DeleteComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime date           = Convert.ToDateTime(Request.Form["Date"]);
            string gamename         = Request.Form["GameName"];
            string username         = Base.GetUserName();
            int gameid              = -1;
            int userid              = -1;
            bool shouldRedirect     = true;


            #region Input Validation

            #region Fetching Game Id
            {
                string query = "SELECT gameId FROM tGames WHERE gameName = '" + gamename + "'";
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
                {
                    Response.Write(Base.Alert("Game Not Found : " + gamename));
                    if(shouldRedirect)
                    {
                        try
                        {
                            Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                        }
                        catch (NullReferenceException)
                        {
                            Response.Write(Base.RedirectTo("/index.aspx"));
                        }
                    }
                    return;
                }

                gameid = int.Parse(temp.Rows[0][temp.Columns["gameId"]].ToString());
            }
            #endregion


            #region Fetching User Id
            {
                string query = "SELECT userId FROM tUsers WHERE userName = '" + username + "'";
                DataTable temp = Base.GetDataBase(query);
                if (temp.Rows.Count != 1)
                {
                    if(shouldRedirect)
                    {
                        Response.Write(Base.Alert("User Not Found"));
                        try
                        {
                            Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                        }
                        catch (NullReferenceException)
                        {
                            Response.Write(Base.RedirectTo("/index.aspx"));
                        }
                    }
                    return;
                }

                userid = int.Parse(temp.Rows[0][temp.Columns["userId"]].ToString());
            }
            #endregion

            #endregion

            #region Delete Query
            {
                string query = String.Format("DELETE FROM tComment WHERE (gameId={0}) AND (userId={1}) AND (commentDate=CDate('{2}'))", gameid.ToString(), userid.ToString(), date);

                try
                {
                    Base.ExecNonQuery(query);
                    Response.Write(Base.Alert("Comment succesfuly deleted !"));
                }
                catch
                {
                    Base.VoidAlert("An error occured");
                }



                if(shouldRedirect)
                {
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
            #endregion

            #region Redirection
            if(shouldRedirect)
            {
                try
                {
                    Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                }
                catch (NullReferenceException)
                {
                    Response.Write(Base.RedirectTo("/index.aspx"));
                }
            }
            #endregion

        }
    }
}