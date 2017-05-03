using System;
using System.Data;
using System.Collections.Generic;

namespace Dbapy_Games.FrontEnd
{
    public partial class GamesShower : System.Web.UI.Page
    {
        public string top = "";
        public string css = "";
        public string links = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            css = Base.PrintCss();
            top = Base.PrintTop();
            string style = "style='background-color:#4298f4 ;border:3px solid #ffffff ;height:120px ;text-align:center ;vertical-align:middle ;line-height: 60px; '";
            links = "";

            List<string> list = new List<string>();
            #region Sorting
            {
                string sortType = Request.QueryString["sort"];

                if (sortType != null && sortType != "")
                {
                    if (sortType == "name")
                    {
                        string query = "SELECT gameName FROM tGames ORDER BY gameName";
                        DataTable table = Base.GetDataBase(query);
                        foreach (DataRow row in table.Rows)
                        {
                            list.Add(row[table.Columns["gameName"]].ToString());
                        }
                    }
                    else if (sortType == "popularity")
                    {
                        string query = "SELECT tGames.gameName , Count(tGames.gameId) , tGames.gameName FROM tGames INNER JOIN tFavorite ON tGames.gameId = tFavorite.gameId GROUP BY tGames.gameId , tGames.gameName ORDER BY Count(tGames.gameId) , gameName";
                        DataTable temp = Base.GetDataBase(query);
                        foreach (DataRow r in temp.Rows)
                        {
                            list.Add(r[temp.Columns["gameName"]].ToString());
                        }

                        list.Reverse();

                        query = "SELECT tGames.gameName FROM tGames ORDER BY gameName";
                        temp = Base.GetDataBase(query);
                        foreach (DataRow r in temp.Rows)
                        {
                            string gameName = r[temp.Columns["gameName"]].ToString();
                            if (!list.Contains(gameName))
                            {
                                list.Add(gameName);
                            }
                        }

                    }
                    else if (sortType == "uploadDate")
                    {
                        string query = "SELECT gameName FROM tGames ORDER BY gameId";
                        DataTable table = Base.GetDataBase(query);
                        foreach (DataRow r in table.Rows)
                        {
                            list.Add(r[table.Columns["gameName"]].ToString());
                        }
                    }
                    else
                    {
                        string query = "SELECT gameName FROM tGames";
                        DataTable table = Base.GetDataBase(query);
                        foreach (DataRow r in table.Rows)
                        {
                            list.Add(r[table.Columns["gameName"]].ToString());
                        }
                    }
                }
                else
                {
                    string query = "SELECT gameName FROM tGames";
                    DataTable table = Base.GetDataBase(query);
                    foreach (DataRow r in table.Rows)
                    {
                        list.Add(r[table.Columns["gameName"]].ToString());
                    }
                }
            }
            #endregion

            foreach(string s in list)
            {
                string gameName = s;

                //Fetching Data about each game to display.
                string sql = String.Format(@"SELECT tGames.gameId , COUNT(tGames.gameId) AS [gameLikes] 
                                            FROM tGames 
                                            INNER JOIN tFavorite ON tGames.gameId = tFavorite.gameId 
                                            WHERE tGames.gameName='{0}'
                                            GROUP BY tGames.gameId" , gameName);
                DataTable temp = Base.GetDataBase(sql);

                int likes = -1;

                if (temp.Rows.Count == 1)
                {
                    likes = int.Parse(temp.Rows[0]["gameLikes"].ToString());
                }
                else if(temp.Rows.Count == 0)
                {
                    likes = 0;
                }
                else
                {
                    Base.VoidAlert("An error occured : " + temp.Rows.Count + " : " + gameName);
                    Base.VoidRedirectTo("/index.aspx");
                    return;
                }

                links += String.Format("<div {0} ><a href='/FrontEnd/Game.aspx?game={1}'>{1}</a><br/>Liked By : {2}</div>" , style , gameName , likes);
            }
        }
    }
}