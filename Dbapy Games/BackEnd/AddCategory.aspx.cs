using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class AddCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Admin Validation
            {
                if(Base.GetUserName().Equals("") || Base.GetUserName().Equals(null) || Base.GetUserStatus() != 2)
                {
                    Base.VoidAlert("You need to be an admin in order to visit this page");
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
            }
            #endregion

            string categoryName = Request.Form["categoryName"];

            #region Input Validation
            {
                if(categoryName.Equals(null) || categoryName.Equals(""))
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

                string query = String.Format("SELECT * FROM tCategories WHERE categoryName = '{0}'" , categoryName);
                DataTable temp = Base.GetDataBase(query);

                if(temp.Rows.Count == 1)
                {
                    Base.VoidAlert("Category Name is already taken");
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

            #region Adding Category
            {
                string query = String.Format("INSERT INTO tCategories (categoryName) VALUES ('{0}')" , categoryName);
                Base.ExecNonQuery(query);
                Base.VoidAlert("Category Successfully added");
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