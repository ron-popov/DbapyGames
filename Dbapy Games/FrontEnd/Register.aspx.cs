using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.FrontEnd
{
    public partial class Register : System.Web.UI.Page
    {
        public string top = "";
        public string css = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            css = Base.PrintCss();
            top = Base.PrintTop();
        }
    }
}