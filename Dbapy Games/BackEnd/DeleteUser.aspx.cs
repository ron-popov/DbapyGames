using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class DeleteUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request.Form["username"];
            int userId = -1;


            #region Admin Validation
            {
                if(Base.GetUserName().Equals("") || Base.GetUserName().Equals(null))
                {
                    Base.VoidAlert("You need to be an admin in order to do that.");
                    return;
                }

                if (!(Base.GetUserStatus() == 2))
                {
                    Base.VoidAlert("You need to be an admin in order to do that.");
                    return;
                }
            }
            #endregion


            #region User Validation
            {
                string query = String.Format("SELECT * FROM tUsers WHERE userName='{0}'" , username);
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("User not found");
                    return;
                }
                userId = int.Parse(temp.Rows[0]["userId"].ToString());
            }
            #endregion


            #region Game Deletion
            {
                string sql = String.Format("SELECT gameId FROM tGames WHERE gameUploaderId={0}" , userId);
                DataTable temp = Base.GetDataBase(sql);
                foreach(DataRow r in temp.Rows)
                {
                    int gameId = int.Parse(r["gameId"].ToString());
                    string sql1 = String.Format("DELETE FROM tFavorite WHERE gameId={0}" , gameId);
                    Base.ExecNonQuery(sql);

                    string sql2 = String.Format("DELETE FROM tComment WHERE gameId={0}" , gameId);
                    Base.ExecNonQuery(sql);

                    string sql4 = String.Format("DELETE FROM tCategoryToGame WHERE gameId={0}" , gameId);
                    Base.ExecNonQuery(sql4);

                    string sql3 = String.Format("DELETE FROM tGames WHERE gameId={0}" , gameId);
                    Base.ExecNonQuery(sql3);

                }
            }
            #endregion

            #region Favorite Deletion
            {
                string sql = String.Format("DELETE FROM tFavorite WHERE userId={0}" , userId);
                Base.ExecNonQuery(sql);
            }
            #endregion

            #region Comment Deletion
            {
                string sql = String.Format("DELETE FROM tComment WHERE userId={0}" , userId);
                Base.ExecNonQuery(sql);
            }
            #endregion

            #region User Deletion
            {
                string query = String.Format("DELETE FROM tUsers WHERE userName='{0}'" , username);
                Base.ExecNonQuery(query);
            }
            #endregion

            Base.VoidAlert("User Deleted !");
            Base.VoidRedirectTo("/FrontEnd/AdminPanel.aspx");
        }
    }
}