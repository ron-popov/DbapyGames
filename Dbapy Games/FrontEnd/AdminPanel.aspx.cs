using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Dbapy_Games.BackEnd
{
    public partial class AdminPanel : System.Web.UI.Page
    {

        public string top           = "";
        public string css           = "";
        public string users         = "";
        public string games         = "";
        public string categories    = "";
        public string usersTable    = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            #region Admin Validation
            if(Base.GetUserStatus() != 2)
            {
                Base.VoidAlert("You need to be an admin in order to visit this page !");
                Base.VoidRedirectTo("/index.aspx");

                return;
            }
            #endregion

            top = Base.PrintTop();
            css = Base.PrintCss();

            #region User Info Table
            {
                string sql = String.Format("SELECT * FROM tUsers");
                DataTable temp = Base.GetDataBase(sql);
                usersTable += "<table border='1'><tr><td><u>Username</u></td><td><u>Sign Up Date</u></td><td><u>Rank</u></td><tr>";
                foreach(DataRow r in temp.Rows)
                {
                    usersTable += "<tr>";
                    usersTable += "<td>" + r["userName"].ToString() + "</td>";
                    usersTable += "<td>" + ((DateTime)r["userSignUpDate"]).ToShortDateString() + "</td>";

                    int rank = int.Parse(r["userRank"].ToString());
                    if (rank == 0)
                        usersTable += "<td>Game Tester</td>";
                    else if (rank == 1)
                        usersTable += "<td>Game Developer</td>";
                    else if (rank == 2)
                        usersTable += "<td>Admin</td>";
                    else
                        throw new Exception("DataBase internal error");

                    usersTable += "</tr>";
                }
                usersTable += "</table>";
            }
            #endregion

            #region Delete User
            {
                string query = String.Format("SELECT userName FROM tUsers WHERE (NOT (userRank = '2' OR userName = '{0}'))" , Base.GetUserName());
                DataTable temp = Base.GetDataBase(query);

                foreach(DataRow r in temp.Rows)
                {
                    users += "<option value='" + r["userName"] + "'>";
                }

            }
            #endregion

            #region Delete Game
            {
                string query = String.Format("SELECT * FROM tGames");
                DataTable temp = Base.GetDataBase(query);

                foreach(DataRow r in temp.Rows)
                {
                    games += "<option value='" + r["gameId"] + "'>" + r["gameName"] + "</option>";
                }
            }
            #endregion

            #region Delete Category
            {
                string query = String.Format("SELECT * FROM tCategories");
                DataTable temp = Base.GetDataBase(query);
                foreach(DataRow r in temp.Rows)
                {
                    categories += "<option value='" + r["categoryId"] + "'>" + r["categoryName"] + "</option>";
                }
            }
            #endregion
        }
    }
}