using DataTransferObjects;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class VerkiezingDAL : IVerkiezing, IVerkiezingContainer
    {
        public Database database = new Database();

        /// <summary>
        /// Hij voegt een verkiezing toe aan de database en maakt een connectie tussen de kandidaten die bij de verkiezing horen en de verkiezing zelf
        /// </summary>
        /// <param name="verkiezingDTO">De verkiezing die je wilt maken</param>
        /// <param name="kandidaatDTOs">De kandidaten die je aan de verkiezing wil koppelen</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool AddVerkiezing(VerkiezingDTO verkiezingDTO, List<KandidaatDTO> kandidaatDTOs)
        {
            int verkiezingID = 0;
            List<KandidaatDTO> kandidatenVerkiezing = kandidaatDTOs;

            database.OpenConnection();

            string query = "INSERT INTO dbo.Verkiezing (VerkiezingNaam,UserID) OUTPUT inserted.VerkiezingID VALUES (@VerkiezingNaam, @UserID)";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@VerkiezingNaam", verkiezingDTO.VerkiezingNaam);
            if(verkiezingDTO.UserID > 0)
            {
                command.Parameters.AddWithValue("@UserID", verkiezingDTO.UserID);
            }
            else { command.Parameters.AddWithValue("@UserID", 1); }
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                verkiezingID = Convert.ToInt32(reader["VerkiezingID"]);
            }
            ADDKV(verkiezingID, kandidatenVerkiezing);
            
            database.CloseConnection();
            

            return true;
        }

        /// <summary>
        /// Hierin maak je de connectie tussen de kandidaten en de verkiezing
        /// </summary>
        /// <param name="verkiezingID">Het ID van de verkiezing waaraan je de kandidaten wil koppelen</param>
        /// <param name="kandidaatDTOs">De kandidaten die je aan de verkiezing wil koppelen</param>
        public void ADDKV(int verkiezingID, List<KandidaatDTO> kandidaatDTOs)
        {
            database.OpenConnection();

            foreach(KandidaatDTO kandidaatDTO in kandidaatDTOs)
            {
                string query = "INSERT INTO dbo.KV (KandidaatID, VerkiezingID) VALUES (@KandidaatID, @VerkiezingID)";
                SqlCommand command = new SqlCommand(query, database.conn);
                command.Parameters.AddWithValue("@VerkiezingID", verkiezingID);
                command.Parameters.AddWithValue("@KandidaatID", kandidaatDTO.KandidaatID);

                command.ExecuteNonQuery();
            }

            database.CloseConnection();
        }

        /// <summary>
        /// Dit haalt alle verkiezingen uit de database op en stopt ze in een lijst
        /// </summary>
        /// <returns>De lijst met alle verkiezingen uit de database</returns>
        public List<VerkiezingDTO> GetAllVerkiezingen(int userid)
        {
            List<VerkiezingDTO> allverkiezingDTOs = new List<VerkiezingDTO>();

            database.OpenConnection();

            string query = "SELECT * FROM dbo.Verkiezing WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@UserID", userid);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                allverkiezingDTOs.Add(new VerkiezingDTO(
                    Convert.ToInt32(reader["VerkiezingID"]),
                    Convert.ToString(reader["VerkiezingNaam"])));
            }

            database.CloseConnection();

            return allverkiezingDTOs;
        }

        /// <summary>
        /// Haalt een specifieke verkiezing door zijn ID te gebruiken
        /// </summary>
        /// <param name="verkiezingID">Het idee van de verkiezing die die op wilt halen</param>
        /// <returns>De verkiezing die je wilt ophalen</returns>
        public VerkiezingDTO GetVerkiezing(int verkiezingID)
        {
            VerkiezingDTO verkiezingDTO = new VerkiezingDTO();

            database.OpenConnection();

            string query = "Select * FROM dbo.Verkiezing WHERE VerkiezingID=@verkiezingID";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@verkiezingID", verkiezingID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                verkiezingDTO.VerkiezingID = Convert.ToInt32(reader["VerkiezingID"]);
                verkiezingDTO.VerkiezingNaam = Convert.ToString(reader["VerkiezingNaam"]);
                verkiezingDTO.UserID = Convert.ToInt32(reader["UserID"]);

            }

            database.CloseConnection();

            return verkiezingDTO;
        }

        /// <summary>
        /// Verwijdert een specifieke verkiezing
        /// </summary>
        /// <param name="verkiezingDTO">De verkiezing die je wilt verwijderen</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool RemoveVerkiezing(VerkiezingDTO verkiezingDTO)
        {

            database.OpenConnection();

            string query = "DELETE FROM dbo.Verkiezing WHERE VerkiezingID=@verkiezingID";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@verkiezingID", verkiezingDTO.VerkiezingID);

            command.ExecuteNonQuery();

            string query2 = "DELETE FROM dbo.KV WHERE VerkiezingID=@verkiezingID";
            SqlCommand command2 = new SqlCommand(query2, database.conn);
            command2.Parameters.AddWithValue("@verkiezingID", verkiezingDTO.VerkiezingID);

            command2.ExecuteNonQuery();

            string query3 = "DELETE FROM dbo.stemmen WHERE VerkiezingID=@verkiezingID";
            SqlCommand command3 = new SqlCommand(query3, database.conn);
            command3.Parameters.AddWithValue("@verkiezingID", verkiezingDTO.VerkiezingID);

            command3.ExecuteNonQuery();

            database.CloseConnection();
            return true;
            
        }

        /// <summary>
        /// Update een verkiezing(niet de kandidaten)
        /// </summary>
        /// <param name="verkiezingDTO">De verkiezing die je wilt updaten met de nieuwe info</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool UpdateVerkiezing(VerkiezingDTO verkiezingDTO)
        {
            database.OpenConnection();

            string query = "UPDATE dbo.Verkiezing SET VerkiezingNaam=(@verkiezingNaam) WHERE (VerkiezingID=(@verkiezingID))";

            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@verkiezingNaam", verkiezingDTO.VerkiezingNaam);
            command.Parameters.AddWithValue("@verkiezingID", verkiezingDTO.VerkiezingID);

            return (command.ExecuteNonQuery() == 1);

            database.CloseConnection();
        }

        /// <summary>
        /// Update de kandidaten van een verkiezing door de kandidaten die je er niet meer in wilt te verwijderen uit de database
        /// En de kandidaten die je er nieuw in wilt voegt die toe aan de database
        /// </summary>
        /// <param name="verkiezingDTO">De verkiezing waarvan je de kandidaten wil updaten</param>
        /// <param name="kandidaatListADD">De kandidaten die je wilt toevoegen</param>
        /// <param name="kandidaatListDELETE">De kandidaten die je wilt verwijderen</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool UpdateVerkiezingKandidaat(VerkiezingDTO verkiezingDTO, List<KandidaatDTO> kandidaatListADD, List<KandidaatDTO> kandidaatListDELETE)
        {
            DeleteKV(verkiezingDTO.VerkiezingID, kandidaatListDELETE);
            ADDKV(verkiezingDTO.VerkiezingID, kandidaatListADD);
            return true;

        }

        /// <summary>
        /// Verwijdert de connectie tussen kandidaten en verkiezingen
        /// </summary>
        /// <param name="verkiezingID">Het ID van de verkiezing waarvan je de kandidaten wilt verwijderen</param>
        /// <param name="kandidaatDTOs">De kandidaten die je uit de verkiezing wilt halen</param>
        public void DeleteKV(int verkiezingID, List<KandidaatDTO> kandidaatDTOs)
        {
            database.OpenConnection();

            foreach (KandidaatDTO kandidaatDTO in kandidaatDTOs)
            {
                string query = "DELETE FROM dbo.KV WHERE KandidaatID=@KandidaatID AND VerkiezingID=@VerkiezingID";
                SqlCommand command = new SqlCommand(query, database.conn);
                command.Parameters.AddWithValue("@VerkiezingID", verkiezingID);
                command.Parameters.AddWithValue("@KandidaatID", kandidaatDTO.KandidaatID);

                command.ExecuteNonQuery();
            }

            database.CloseConnection();
        }

        public List<VerkiezingDTO> GetAllVerkiezingenVote()
        {
            List<VerkiezingDTO> allverkiezingDTOs = new List<VerkiezingDTO>();

            database.OpenConnection();

            string query = "SELECT * FROM dbo.Verkiezing;";
            SqlCommand command = new SqlCommand(query, database.conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                allverkiezingDTOs.Add(new VerkiezingDTO(
                    Convert.ToInt32(reader["VerkiezingID"]),
                    Convert.ToString(reader["VerkiezingNaam"]),
                    Convert.ToInt32(reader["UserID"])));
            }

            database.CloseConnection();

            return allverkiezingDTOs;
        }
    }
}
