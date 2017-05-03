using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string OldPassword = Request.Form["OldPassword"];
            string NewPassword = Request.Form["NewPassword"];
            string RepNewPassword = Request.Form["RepNewPassword"];
            string username = Base.GetUserName();

            #region Logged In Validation
            {
                if(username == null || username == "")
                {
                    Base.VoidAlert("You need to be logged in in order to be here");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
            }
            #endregion

            #region Input Validation
            {
                if (OldPassword == null || NewPassword == null || RepNewPassword == null)
                {
                    Base.VoidAlert("An error occured");
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

                if (NewPassword != RepNewPassword)
                {
                    Base.VoidAlert("Passwords do not match");
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

                if (NewPassword.Length < Base.minPasswordLength || NewPassword.Length > Base.maxPasswordLength)
                {
                    Base.VoidAlert("Password length is incompatible");
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
            #endregion

            #region Changing The Password
            {
                string sql = String.Format("UPDATE tUsers SET userPass = '{0}' WHERE userName = '{1}'" , Base.CalculateHash(NewPassword) , username);
                Base.ExecNonQuery(sql);
            }
            #endregion

            Base.VoidAlert("Password successfuly changed !");
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
}