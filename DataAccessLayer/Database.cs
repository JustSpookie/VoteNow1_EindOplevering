using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class Database
    {
        private static string connectionString = "Server=mssqlstud.fhict.local;Database=dbi489572;User Id=dbi489572;Password=Thor1983;";

        public SqlConnection conn;
        
        /// <summary>
        /// Dit opent de connectie met de database
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                this.conn = new SqlConnection(connectionString);
                //Console.WriteLine("aan het openen");
                conn.Open();
                //MessageBox.Show("connectie open");
            }
            catch (Exception exc)
            {
                //Console.WriteLine(exc);
            }
        }

        /// <summary>
        /// Sluit de connectie met de database
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                this.conn = new SqlConnection(connectionString);
                conn.Close();
                //Console.WriteLine("Connection closed");
            }
            catch (Exception exc)
            {
                //Console.WriteLine(exc);
            }
        }
    }
}