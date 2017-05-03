using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.BackEnd
{
    public partial class Disconnect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Write(Base.RedirectTo(Request.UrlReferrer.ToString()));
        }
    }
}