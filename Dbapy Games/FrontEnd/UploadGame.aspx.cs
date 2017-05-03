using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dbapy_Games.FrontEnd
{
    public partial class UploadGame : System.Web.UI.Page
    {
        public string top;
        public string css;
        public string categories;

        //Once the page has been loaded
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Rank Validation
            if (Base.GetUserStatus() == 0 || Base.GetUserName() == null)
            {
                Base.VoidAlert("You need to be an admin in order to visit this page !");
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
            #endregion

            top = Base.PrintTop();
            css = Base.PrintCss();

            #region Categories
            {
                string query = String.Format("SELECT categoryName FROM tCategories");
                DataTable temp = Base.GetDataBase(query);
                foreach(DataRow r in temp.Rows)
                {
                    categories += String.Format(@"<input id='Category{0}' type='checkbox' name='Category{0}' /><label for='Category{0}'>{0}</label><br/>" , r["categoryName"]);
                }
            }
            #endregion
        }

        //Checks if the given category has been selected
        private bool IsSelected(string categoryName)
        {
            if(Request.Form["Category" + categoryName] == null)
            {
                return false;
            }
            else if (Request.Form["Category" + categoryName].Equals("on"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Once the user submitted a game to upload
        protected void OnUpload(object sender, EventArgs e)
        {
            string gameName = formGameName.Text;
            int difficulty = -1;
            int userId = -1;
            int newGameId = -1;

            #region Rank Validation
            {
                if (Base.GetUserStatus() == 0 || Base.GetUserName() == null)
                {
                    Base.VoidAlert("You need to be a game uploader or higher in order to visit this page !");
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

                string query = String.Format("SELECT userId FROM tUsers WHERE userName='{0}'" , Base.GetUserName());
                DataTable temp = Base.GetDataBase(query);
                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("An error occured");
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
                else
                {
                    userId = int.Parse(temp.Rows[0]["userId"].ToString());
                }

            }
            #endregion

            #region Input Validation
            {
                if(gameName.Length < 2 || gameName.Length > 32 || gameName.Contains("'") || gameName.Contains(" "))
                {
                    Base.VoidAlert("Game name is not valid : " + gameName);
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

                int tempDifficulty;
                try
                {
                    tempDifficulty = int.Parse(Difficulty.Text);
                    if(tempDifficulty > 5 || tempDifficulty < 1)
                    {
                        Base.VoidAlert("Game Difficulty invalid");
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
                catch
                {
                    Base.VoidAlert("Game Difficulty invalid");
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

                difficulty = tempDifficulty;

                if (!gameFile.HasFile)
                {
                    Base.VoidAlert("Please attach a file");
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

            #region Game Addition
            {
                string sql = String.Format("INSERT INTO tGames (gameUploaderId , gameName , gameDifficulty) VALUES ({0} , '{1}' , '{2}')", userId, gameName, difficulty);
                try
                {
                    Base.ExecNonQuery(sql);
                }
                catch
                {
                    Base.VoidAlert("An error occured");
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

            #region Fetching Game Id
            {
                string sql = String.Format("SELECT gameId FROM tGames WHERE gameName = '{0}'" , gameName);
                DataTable temp = Base.GetDataBase(sql);
                if(temp.Rows.Count != 1)
                {
                    Base.VoidAlert("An error occured please contact an administrator");
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

                newGameId = int.Parse(temp.Rows[0]["gameId"].ToString());
            }
            #endregion

            #region Categories Addition
            {
                string sql = String.Format ("SELECT * FROM tCategories");
                DataTable temp = Base.GetDataBase(sql);

                foreach(DataRow r in temp.Rows)
                {
                    if(IsSelected(r["categoryName"].ToString()))
                    {
                        string sql2 = String.Format("INSERT INTO tCategoryToGame (categoryId , gameId) VALUES ({0} , {1})" , r["categoryId"] , newGameId);
                        Base.ExecNonQuery(sql2);
                    }
                }
            }
            #endregion

            #region File Upload
            {
                try
                {
                    gameFile.SaveAs(Server.MapPath(String.Format("/Games/{0}.swf", gameName)));
                }
                catch
                {
                    Base.VoidAlert("There was a problem while uploading the game file , please contact an administrator");
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

                Base.VoidAlert("Game has been succesfully uploaded");
                Base.VoidRedirectTo("/index.aspx");
            }
            #endregion
        }
    }
}