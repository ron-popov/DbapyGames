using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class Register : System.Web.UI.Page
    {
        //Will run once register form is sent.
        protected void Page_Load(object sender, EventArgs e)
        {
            //Variables
            string username         = Request.Form["username"];
            string password         = Request.Form["password"];
            string rPassword        = Request.Form["rPassword"];
            string userEmail        = Request.Form["userEmail"];

            #region Input Validation
            {
                if(rPassword == null || password == null || userEmail == null || username == null)
                {
                    Response.Write(Base.Alert("An error occured"));
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

                if (rPassword != password)
                {
                    Response.Write(Base.Alert("passwords do not match"));
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
                else if (username.Length > Base.maxUsernameLength || username.Length < Base.minUsernameLength)
                {
                    Response.Write(Base.Alert("Username length is incompatible"));
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
                else if (password.Length > Base.maxPasswordLength || password.Length < Base.minPasswordLength)
                {
                    Response.Write(Base.Alert("Password length is incompatible"));
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
                else if (!userEmail.Contains("@"))
                {
                    Response.Write(Base.Alert("Email is icompatible"));
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }
            }
            #endregion

            #region User Addition
            {
                string sqlQuery = String.Format("INSERT INTO tUsers (userName , userPass , userSignUpDate , userEmail , userRank) VALUES " +
                "('{0}' , '{1}' , '{2}' , '{3}' , '0')", username, Base.CalculateHash(password), DateTime.Now.ToString(), userEmail);

                Base.ExecNonQuery(sqlQuery);
            }
            #endregion

            Base.VoidAlert("Successfuly registered");
            Base.VoidRedirectTo("/index.aspx");
        }
    }
}