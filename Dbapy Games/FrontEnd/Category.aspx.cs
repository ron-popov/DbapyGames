using System;
using System.Data;

namespace Dbapy_Games.FrontEnd
{
    public partial class Category : System.Web.UI.Page
    {
        public string css;
        public string top;
        public string games;

        protected void Page_Load(object sender, EventArgs e)
        {
            css = Base.PrintCss();
            top = Base.PrintTop();
            games = "";


            string gameCss = "style='background-color:#4298f4 ;border:3px solid #ffffff ;height:80px ;text-align:center ;vertical-align:middle ;line-height: 80px; '";
            string category = Request.QueryString["category"];
            string query = "SELECT categoryId FROM tCategories WHERE categoryName='" + category + "'";
            DataTable table = Base.GetDataBase(query);
            if(table.Rows.Count == 0)
            {
                Response.Write(Base.Alert("Category Not found"));
                return;
            }
            else if(table.Rows.Count == 1)
            {
                int categoryId = -1;

                try
                {
                    categoryId = int.Parse(table.Rows[0]["categoryId"].ToString());
                }
                catch
                {
                    Base.VoidAlert("An error occured");
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

                query = String.Format("SELECT tGames.gameName FROM tGames INNER JOIN tCategoryToGame ON tGames.gameId = tCategoryToGame.gameId WHERE tCategoryToGame.categoryId={0}" , categoryId);
                table = Base.GetDataBase(query);
                foreach (DataRow r in table.Rows)
                {
                    string gameName = (r[table.Columns["gameName"]]).ToString();
                    games += "<div " + gameCss + " ><a href='/FrontEnd/Game.aspx?game=" + gameName + "'>" + gameName + "</a></div>";
                }
            }
            else
            {
                Base.VoidAlert("An error occured");
                Base.VoidRedirectTo("/index.aspx");
            }

        }
    }
}