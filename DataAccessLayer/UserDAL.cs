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
    public class UserDAL : IUserContainer
    {
        public Database database = new Database();

        public int AddUser(UserDTO userDTO)
        {
            database.OpenConnection();

            string query = "INSERT INTO dbo.Users (naam, password) VALUES (@naam, @password)";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("naam", userDTO.Name);
            command.Parameters.AddWithValue("password", userDTO.Password);
            command.ExecuteNonQuery();

            database.CloseConnection();

            return (1);

        }

        public bool DeleteUser(UserDTO userDTO)
        {
            database.OpenConnection();

            string query = "DELETE FROM dbo.Users WHERE User_id=@userid";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@userid", userDTO.ID);

            return (command.ExecuteNonQuery() > 0);
        }

        public UserDTO GetUser(int userID)
        {
            UserDTO userDTO = new UserDTO();

            database.OpenConnection();

            string query = "SELECT * FROM dbo.Users WHERE User_id=@userid";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@user", userID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userDTO = new UserDTO(
                    Convert.ToInt32(reader["User_id"]),
                    Convert.ToString(reader["Naam"]),
                    Convert.ToString(reader["Password"]));
            }

            database.CloseConnection();

            return userDTO;
        }

        public UserDTO GetUserByName(UserDTO userDTOtemp)
        {
            UserDTO userDTO = new UserDTO();

            database.OpenConnection();

            string query = "SELECT * FROM dbo.Users WHERE Naam=@username";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@username",  userDTOtemp.Name);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userDTO = new UserDTO(
                    Convert.ToInt32(reader["User_id"]),
                    Convert.ToString(reader["Naam"]),
                    Convert.ToString(reader["Password"]));
            }

            database.CloseConnection();

            return userDTO;
        }

        public List<UserDTO> GetUsers()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();

            database.OpenConnection();

            string query = "SELECT * FROM dbo.Users";
            SqlCommand command = new SqlCommand(query, database.conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userDTOs.Add(new UserDTO(
                    Convert.ToInt32(reader["User_id"]),
                    Convert.ToString(reader["Naam"]),
                    Convert.ToString(reader["Password"])));
            }

            database.CloseConnection();

            return userDTOs;
        }

        public UserDTO Inloggen(UserDTO userDTO)
        {
            UserDTO userDTO1 = new UserDTO();

            database.OpenConnection();

            string query = "SELECT * FROM dbo.Users WHERE Naam=@naam AND Password=@password";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@naam", userDTO.Name);
            command.Parameters.AddWithValue("@password", userDTO.Password);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userDTO1 = new UserDTO(
                    Convert.ToInt32(reader["User_id"]),
                    Convert.ToString(reader["Naam"]),
                    Convert.ToString(reader["Password"]));
            }

            database.CloseConnection();

            return userDTO1;
        }

        public bool CheckForUser(string naam)
        {
            bool found = false;

            database.OpenConnection();

            string query = "SELECT COUNT(*) FROM dbo.Users WHERE Naam=@naam";
            SqlCommand command = new SqlCommand(query, database.conn);
            command.Parameters.AddWithValue("@naam", naam);

            if((Int32) command.ExecuteScalar() > 0) { found = true; }
            
            database.CloseConnection();

            return found;

        }
    }
}
