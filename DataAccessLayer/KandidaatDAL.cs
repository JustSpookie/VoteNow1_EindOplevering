using DataTransferObjects;
using Interfaces;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class KandidaatDAL : IKandidaat, IKandidaatContainer
    {
        public Database database = new Database();

        /// <summary>
        /// Voegt een kandidaat toe aan de database
        /// </summary>
        /// <param name="kandidaatDTO">Welke kandidaat die je wilt toevoegen</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool AddKandidaat(KandidaatDTO kandidaatDTO)
        {
            database.OpenConnection();

            string query = "INSERT INTO dbo.Kandidaten (KandidatenNaam, UserID) VALUES (@Kandidaatnaam, @UserID)";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@Kandidaatnaam", kandidaatDTO.KandidaatNaam);
            if(kandidaatDTO.UserID > 0)
            {
                command.Parameters.AddWithValue("@UserID", kandidaatDTO.UserID);
            }
            else { command.Parameters.AddWithValue("@UserID", 0); }

            return (command.ExecuteNonQuery() == 1);


            database.CloseConnection();
        }

        /// <summary>
        /// Controlleert of een bepaalde kandidaat in een verkiezing zit
        /// </summary>
        /// <param name="kandidaatDTO">De kandidaat die je wilt controleren</param>
        /// <returns>Een bool waaraan je kunt zien of de kandidaat wel of niet in een verkiezing zit</returns>
        public bool CheckKSV(KandidaatDTO kandidaatDTO)
        {
            database.OpenConnection();

            string query = "SELECT * FROM dbo.KV WHERE KandidaatID=@kandidatenID";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@KandidatenID", kandidaatDTO.KandidaatID);

            if(command.ExecuteScalar() == null)
            {
                return true;
            }
            else
            {
                return false;
            }

            database.CloseConnection();
        }

        /// <summary>
        /// Verwijdert een speciefieke kandidaat uit de database
        /// </summary>
        /// <param name="kandidaatDTO">De kandidaat die je wilt verwijderen</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool DeleteKandidaat(KandidaatDTO kandidaatDTO)
        {
            if (CheckKSV(kandidaatDTO))
            {
                database.OpenConnection();

                string query = "DELETE FROM dbo.Kandidaten WHERE KandidatenID=@kandidatenID";
                SqlCommand command = new SqlCommand(query, database.conn);
                command.Parameters.AddWithValue("@KandidatenID", kandidaatDTO.KandidaatID);

                bool Bool = command.ExecuteNonQuery() == 1;

                database.CloseConnection();

                return Bool;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Pakt een speciefieke kandidaat uit de database
        /// </summary>
        /// <param name="kandidaatID">Het ID van de kandidaat die je wil pakken</param>
        /// <returns>Geeft je de kandidaat terug</returns>
        public KandidaatDTO GetKandidaat(int kandidaatID)
        {
            KandidaatDTO kandidaatDTO = new KandidaatDTO();

            database.OpenConnection();

            string query = "Select * FROM dbo.Kandidaten WHERE KandidatenID=@kandidatenID";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@kandidatenID", kandidaatID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                kandidaatDTO.KandidaatID = Convert.ToInt32(reader["KandidatenID"]);
                kandidaatDTO.KandidaatNaam = Convert.ToString(reader["KandidatenNaam"]);
                kandidaatDTO.UserID = Convert.ToInt32(reader["UserID"]);

            }

            database.CloseConnection();

            return kandidaatDTO;
        }

        /// <summary>
        /// Pakt alle kandidaten die in de database zitten en stopt ze in een lijst
        /// </summary>
        /// <returns>De lijst met kandidaten</returns>
        public List<KandidaatDTO> GetKandidaten(int userid)
        {
            List<KandidaatDTO> AlleKandidatenDTO = new List<KandidaatDTO>();

            database.OpenConnection();

            string query = "SELECT * FROM dbo.Kandidaten WHERE UserID=@userid";

            SqlCommand command = new SqlCommand(query, database.conn);

            command.Parameters.AddWithValue("@userid", userid);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                AlleKandidatenDTO.Add(new KandidaatDTO(
                    Convert.ToString(reader["KandidatenNaam"]),
                    Convert.ToInt32(reader["KandidatenID"])));
            }

            database.CloseConnection();

            return AlleKandidatenDTO;
        }

        /// <summary>
        /// Pakt alle kandidaten die gekoppeld zijn aan een bepaalde verkiezing uit de database
        /// En stopt ze in een lijst 
        /// </summary>
        /// <param name="verkiezingID">Het ID van de verkiezing waarvan je de kandidaten wilt</param>
        /// <returns>De lijst met kandidaten die gekoppeld zijn aan de verkiezing</returns>
        public List<KandidaatDTO> GetKandidatenFromVerkiezing(int verkiezingID)
        {
            List<KandidaatDTO> AlleKandidatenDTO = new List<KandidaatDTO>();

            database.OpenConnection();

            string query = "SELECT * FROM dbo.KV WHERE VerkiezingID=@verkiezingID";

            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@verkiezingID", verkiezingID);
            SqlDataReader reader = command.ExecuteReader();

            List<int> IDs = new List<int>();
            while (reader.Read())
            {
                IDs.Add(Convert.ToInt32(reader["KandidaatID"]));
            }
            reader.Close();

            foreach(int ID in IDs)
            {
                string query2 = "SELECT * FROM dbo.Kandidaten WHERE KandidatenID=@kandidatenID";

                SqlCommand command2 = new SqlCommand(query2, database.conn);
                command2.Parameters.AddWithValue("@kandidatenID", ID);
                SqlDataReader reader2 = command2.ExecuteReader();

                while (reader2.Read())
                {
                    AlleKandidatenDTO.Add(new KandidaatDTO(
                    Convert.ToString(reader2["KandidatenNaam"]),
                    Convert.ToInt32(reader2["KandidatenID"])));
                }
                reader2.Close();
            }

            database.CloseConnection();

            return AlleKandidatenDTO;
        }

        /// <summary>
        /// Update de kandidaat in de database met nieuwe informatie
        /// </summary>
        /// <param name="kandidaatDTO">De kandidaat die je wil updaten met de nieuwe informatie</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool UpdateKandidaat(KandidaatDTO kandidaatDTO)
        {
            database.OpenConnection();

            string query = "UPDATE dbo.Kandidaten SET KandidatenNaam=(@KandidatenNaam) WHERE (KandidatenID=(@KandidaatID))";

            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@KandidatenNaam", kandidaatDTO.KandidaatNaam);
            command.Parameters.AddWithValue("@KandidaatID", kandidaatDTO.KandidaatID);

            return (command.ExecuteNonQuery() == 1);

            database.CloseConnection();
        }
    }
}
