using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class RemoveCategory : System.Web.UI.Page
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

            int categoryId = -1;

            #region Input Validation
            {
                string tempCategoryId = Request.Form["categoryId"];
                try
                {
                    categoryId = int.Parse(tempCategoryId);
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
            }
            #endregion

            #region Deleting The Games Of The Category
            {
                string sql = String.Format("DELETE FROM tCategoryToGames WHERE categoryId={0}" , categoryId);
            }
            #endregion

            #region Deleting The Category
            {
                string query = String.Format("DELETE FROM tCategories WHERE categoryId={0}" , categoryId.ToString());
                Base.ExecNonQuery(query);
                Base.VoidAlert("Category successfuly deleted");
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