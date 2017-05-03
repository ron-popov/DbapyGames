using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.FrontEnd
{
    public partial class FullScreenGame : System.Web.UI.Page
    {
        public string css = "";
        public string top = "";
        public string game = "";
        public string backButton = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string gamename = Request.QueryString["game"];

            #region Input Validation
            {
                string query = String.Format("SELECT * FROM tGames WHERE gameName='{0}'" , gamename);
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("Game not found");
                    try
                    {
                        Base.VoidRedirectTo(Request.UrlReferrer.ToString());
                    }
                    catch
                    {
                        Base.VoidRedirectTo("/index.aspx");
                    }
                    return;
                }
            }
            #endregion

            top = Base.PrintTop();
            css = Base.PrintCss();


            #region Displaying Game
            {
                int width, height = 90;
                width = height * 4 / 3;
                game = String.Format("<embed id='Game' src='/Games/{0}.swf' width='{1}vw' height='{2}vh' >" , gamename , width , height);
            }
            #endregion

            #region Back Button
            {
                backButton = "<form method='Get' action='/FrontEnd/Game.aspx'><input type='Hidden' name='game' value='" + gamename + "'><input type='submit' value='Back To Game Page'></form>";
            }
            #endregion
        }
    }
}