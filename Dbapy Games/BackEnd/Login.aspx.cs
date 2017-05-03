using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class Login : System.Web.UI.Page
    {
        //Will be called once login form is sent.
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            if(username == null || password == null)
            {
                Response.Write(Base.Alert("Username or password error"));
                Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
            }
            else
            {
                password = Base.CalculateHash(password);
                string query = "SELECT userRank FROM tUsers WHERE (userName = \'" + username + "\' AND userPass = \'" + password + "\')";
                DataTable table = Base.GetDataBase(query);
                if(table.Rows.Count == 1)
                {
                    Session.Add("UserName" , username);
                    Session.Add("Status" , table.Rows[0]["userRank"]);
                    Response.Write(Base.Alert("You have successfuly logged in"));
                    Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                }
                else
                {
                    Response.Write(Base.Alert("An error occured"));
                    Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
                }
            }
        }
    }
}