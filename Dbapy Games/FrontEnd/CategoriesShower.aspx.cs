using System;
using System.Data;

namespace Dbapy_Games.FrontEnd
{
    public partial class CategoriesShower : System.Web.UI.Page
    {
        public string css;
        public string top;
        public string links;

        protected void Page_Load(object sender, EventArgs e)
        {
            css = Base.PrintCss();
            top = Base.PrintTop();


            string style = "style='background-color:#680f0f ;border:3px solid #73AD21 ;height:100px ;text-align:center ;vertical-align:middle ;line-height: 50px; '";
            string sort = "";
            string sortGetValue = Request.QueryString["sort"];

            if(sortGetValue != null)
            {
                if (Request.QueryString["sort"].Equals("games"))
                {
                    sort = "ORDER BY COUNT(tCategoryToGame.gameId)";
                }
                else if (Request.QueryString["sort"].Equals("name"))
                {
                    sort = "ORDER BY categoryName";
                }
            }

            string query = String.Format(@"SELECT tCategories.categoryName , COUNT(tCategoryToGame.gameId) AS [gamesPerCategory] 
                                           FROM tCategories 
                                           INNER JOIN tCategoryToGame ON tCategories.categoryId = tCategoryToGame.categoryId 
                                           GROUP BY tCategories.categoryName 
                                           {0}" , sort);
            DataTable table = Base.GetDataBase(query);

            foreach (DataRow row in table.Rows)
            {
                links += "<div " + style + " ><span><a href='/FrontEnd/Category.aspx?category=" + row["categoryName"] + "'>" + row["categoryName"] + "</a></span><br/><span>Games : " + row["gamesPerCategory"] + "</span></div>";
            }
        }
    }
}