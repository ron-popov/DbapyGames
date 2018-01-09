using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class Comment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string commentContent       = Request.Form["CommentContent"];
            string gamename             = Request.Form["GameName"];
            string username             = Base.GetUserName();
            int gameId                  = -1;
            int userId                  = -1;

            if (commentContent == null || commentContent == "")
            {
                Response.Write(Base.Alert("An Error Occured"));
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

            if(username == null || username == "")
            {
                Response.Write(Base.Alert("An Error Occured"));
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
            else
            {
                string query = "SELECT userId FROM tUsers WHERE userName = '" + username + "'";
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
                {
                    Response.Write(Base.Alert("An Error Occured"));
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

                userId = int.Parse(temp.Rows[0][temp.Columns["userId"]].ToString());
            }


            { //Fetching the gameId from the database.
                string query = "SELECT gameId FROM tGames WHERE gameName='" + gamename + "'";
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
                {
                    Response.Write(Base.Alert("An Error Occured"));
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

                gameId = (int)(temp.Rows[0][temp.Columns["gameId"]]);
            }


            //From this point and on all input is verified and allowed.
            {
                string query = String.Format("INSERT INTO tComment (gameId , userId , commentDate , commentContent) VALUES ({0} , {1} , '{2}' , '{3}')" , gameId , userId , DateTime.Now , commentContent);
                try
                {
                    Base.ExecNonQuery(query);
                }
                catch
                {
                    Response.Write(Base.Alert("An Error Occured"));
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



            Response.Write(Base.Alert("Your comment has been added succesfully"));
            //Sending the user back where he came from.
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
}