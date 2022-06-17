using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StemDAL : IStemContainer
    {
        public Database database = new Database();

        /// <summary>
        /// Voegt een stem toe op een kandidaat die in een bepaalde verkiezing zit
        /// </summary>
        /// <param name="verkiezingID">Het ID van de verkiezing waarin de kandidaat zit</param>
        /// <param name="kandidaatID">Het ID van de kandidaat waarop gestemt wordt</param>
        /// <param name="userID">Het ID van de user die stemt</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool AddStem(int verkiezingID, int kandidaatID, int userID)
        {
            database.OpenConnection();
            DateTime dateTime = DateTime.Now;

            string query = "INSERT INTO dbo.stemmen (userid,kandidaatid,verkiezingid,date) VALUES (@userid,@kandidaatid,@verkiezingid,@date)";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@userid", userID);
            command.Parameters.AddWithValue("@kandidaatid", kandidaatID);
            command.Parameters.AddWithValue("@verkiezingid", verkiezingID);
            command.Parameters.AddWithValue("@date", dateTime);

            command.ExecuteNonQuery();

            database.CloseConnection();

            return true;
        }

        /// <summary>
        /// Telt hoeveel stemmen erop een bepaalde kandidaat in de verkiezing zijn gestemt
        /// </summary>
        /// <param name="kandidaatID">Het ID van de kandidaat waarvan je de stemmen wilt hebben</param>
        /// <param name="verkiezingID">Het ID van de verkiezing waar de kandidaat in zit</param>
        /// <returns></returns>
        public int GetStemCount(int kandidaatID, int verkiezingID)
        {
            int stemmen;

            database.OpenConnection();

            string query = "SELECT COUNT(*) FROM dbo.stemmen WHERE (kandidaatid=@kandidaatid AND verkiezingid=@verkiezingid)";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@kandidaatid", kandidaatID);
            command.Parameters.AddWithValue("@verkiezingid", verkiezingID);

            stemmen = (int)command.ExecuteScalar();

            database.CloseConnection();

            return stemmen;
        }

        public bool CheckStem(int verkiezingID, int userID)
        {
            bool Checked = false;

            database.OpenConnection();
            
            string query = "SELECT COUNT(*) FROM dbo.stemmen WHERE (userid=@userid AND verkiezingid=@verkiezingid)";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@userid", userID);
            command.Parameters.AddWithValue("@verkiezingid", verkiezingID);

            if((int) command.ExecuteScalar() > 0) { Checked = true; }


            database.CloseConnection();

            return Checked;
        }
    }
}
