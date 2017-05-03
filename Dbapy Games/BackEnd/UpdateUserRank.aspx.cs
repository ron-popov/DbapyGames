using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class UpdateUserRank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request.Form["username"];
            int rank = -1;

            #region Input Type Validation
            try
            {
                rank = int.Parse(Request.Form["rank"]);
            }
            catch
            {
                Base.VoidAlert("Rank invalid");
                return;
            }
            #endregion
            
            #region Input Value Validation
            {
                string query = String.Format("SELECT userRank FROM tUsers WHERE userName='{0}'" , username);
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("User not found");
                    return;
                }

                if(temp.Rows[0]["userRank"].ToString() == rank.ToString())
                {
                    Base.VoidAlert("That is already the users rank");
                    return;
                }
            }
            #endregion

            #region Rank Update
            {
                string query = String.Format("UPDATE tUsers SET userRank='{0}' WHERE userName='{1}'" , rank.ToString() , username);
                Base.ExecNonQuery(query);
            }
            #endregion

            Base.VoidAlert("Rank successfully updated");
            Base.VoidRedirectTo("/FrontEnd/AdminPanel.aspx");
        }
    }
}