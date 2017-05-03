using System;
using System.Data;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Dbapy_Games
{
    public static class Base
    {

        #region Variables
            public static int minUsernameLength = 6;
            public static int maxUsernameLength = 32;
            public static int minPasswordLength = 8;
            public static int maxPasswordLength = 32;
        #endregion

        //Calculates a hash of the given string
        public static string CalculateHash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            string temp = sb.ToString();
            string returnValue = "";
            foreach (char c in temp)
            {
                if(c >= 'A' && c <= 'Z')
                {
                    returnValue += Char.ToLower(c);
                }
                else
                {
                    returnValue += c;
                }
            }
            return returnValue;
        }

        #region Database
        //Returns a data table of the given sql query
        public static DataTable GetDataBase(string sql)
        {
            HttpContext context = System.Web.HttpContext.Current;
            string connectString = "PROVIDER=Microsoft.Ace.OLEDB.12.0;DATA SOURCE=" + context.Server.MapPath("\\App_Data\\Database.accdb");
            OleDbConnection objCon;
            OleDbCommand objCmd;
            DataSet Ds = new DataSet();

            string GetSql = string.Empty;
            GetSql = sql;
            using (objCon = new OleDbConnection(connectString))
            {
                using (objCmd = new OleDbCommand(GetSql, objCon))
                {
                    OleDbDataAdapter Da = new OleDbDataAdapter(objCmd);
                    Da.Fill(Ds);
                }
            }

            return Ds.Tables[0];
        }

        //Executes a query on the database
        public static void ExecNonQuery(string query)
        {
            HttpContext context = System.Web.HttpContext.Current;
            OleDbConnection conn = null;
            OleDbCommand cmd;
            try
            {
                conn = new OleDbConnection("PROVIDER=Microsoft.Ace.OLEDB.12.0;DATA SOURCE=" + context.Server.MapPath("\\App_Data\\Database.accdb"));
                conn.Open();
                cmd = new OleDbCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exx)
            {
                throw exx;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        #endregion

        #region Javascript
        //Redirects the page to the given string url
        public static string RedirectTo(string s)
        {
            return ("<script type='text/javascript'>function Redirect() {window.location='" + s + "';}Redirect();</script>");
        }

        //A nice pop-up message for the user
        public static string Alert(string s)
        {
            return ("<script type='text/javascript'>function alertUser() {alert('" + s + "');}alertUser();</script>");
        }

        public static void VoidRedirectTo(string s)
        {
            System.Web.HttpContext.Current.Response.Write(RedirectTo(s));
        }

        public static void VoidAlert(string s)
        {
            System.Web.HttpContext.Current.Response.Write(Alert(s));
        }
        #endregion

        #region Cookies
        //Return the value of the given cookie
        public static string GetCookie(string s, HttpResponse response)
        {
            return response.Cookies[s].Value;
        }

        //Deletes the given cookie
        public static void DeleteCookie(string cookieName , HttpResponse response)
        {
            if(response.Cookies[cookieName] != null)
            {
                response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion

        #region User Data
        //The name of the current user name
        public static string GetUserName()
        {
            HttpSessionState session = System.Web.HttpContext.Current.Session;

            if (session["UserName"] != null)
            {
                return session["UserName"].ToString();
            }
            else
            {
                return "";
            } 

        }

        //The status of the current user.
        public static int GetUserStatus()
        {
            HttpSessionState session = System.Web.HttpContext.Current.Session;

            if (session["status"] == null)
            {
                return -1;
            }
            else
            {
                return int.Parse(session["Status"].ToString());
            }
        }
        #endregion

        #region Print Assist
        //Prints the css link tag that should be in the head tag.
        public static string PrintCss()
        {
            return "<link rel=\"stylesheet\" href=\"\\Design.css\">";
        }

        // --------------------------- Prints the top bar ---------------------------
        public static string PrintTop()
        {
            HttpSessionState session = System.Web.HttpContext.Current.Session;
            string returnValue = "";
            returnValue += "<table style='width:100%'><th>";

            //Navigation menu
            string topBar = "<td style='text-align:left'><table><th>";
            topBar += "<td style='text-align:left'><a href=/index.aspx><img src='/Photos/Logo.png' width=200px height=100px></a></td><td style='text-align:left'><a href=/FrontEnd/CategoriesShower.aspx><img src='/Photos/TopCategories.png' width=100px height=100px></a></td>";
            topBar += "<td style='text-align:left'><a href=/FrontEnd/GamesShower.aspx><img src='/Photos/TopGames.png' width=100px height=100px></a></td>";
            if (GetUserName() != "" && GetUserName() != null)
            {
                topBar += "<td style='text-align:left'><a href=/FrontEnd/Profile.aspx?username=" + GetUserName() + "><img src='/Photos/TopProfile.png' width=100px height=100px></a></td>";

                if(GetUserStatus() == 2 || GetUserStatus() == 1)
                {
                    topBar += "<td style='text-align:left'><a href='/FrontEnd/UploadGame.aspx'><img src='/Photos/TopUploadGame.png' width=100px heigth=100px></a></td>";
                }

                if(GetUserStatus() == 2)
                {
                    topBar += "<td style='text-align:left'><a href='/FrontEnd/AdminPanel.aspx'><img src='/Photos/TopAdminPanel.png' width=100px heigth=100px></a></td>";
                }
            }
            topBar += "</th></table></td><td width='40%'></td>";

            returnValue += (topBar);

            //Login and disconnect part
            if (GetUserName() == "" || GetUserName() == null)
            {
                string loginForm = "<td style='text-align:right'><form action='/BackEnd/Login.aspx' method='POST'>" +
                "<table><tr><td>Username : </td><td><input type='text' name='username'></td></tr>" +
                "<tr><td>Password : </td><td><input type='password' name='password'></td></tr>" + 
                "<tr><td><input type='submit' value='Login'></td></form><td><form action='/FrontEnd/Register.aspx'><input type='submit' value='Register'></form></td></tr>" +
                "</table></td>";

                returnValue += loginForm;

            }
            //User settings : disconnect , admin panel ...
            else
            {
                string userConnected = "";
                userConnected += "<td>";

                userConnected += "<table><tr><td><h4 style='margin:0;padding:0;'>Hey There " + GetUserName() + "</h4></td></tr>";
                userConnected += "<tr><td><form action='/BackEnd/Disconnect.aspx'><input type='submit' value='Disconnect'></form></td></tr></table>";

                userConnected += "</td>";
                returnValue += userConnected;
            }

            returnValue += "</th></table>";
            return returnValue;
        }
        #endregion

    }
}