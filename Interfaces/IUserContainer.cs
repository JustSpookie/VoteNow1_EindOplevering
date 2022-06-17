using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUserContainer
    {
        public UserDTO GetUser(int userID);
        public List<UserDTO> GetUsers();
        public int AddUser(UserDTO userDTO);
        public bool DeleteUser(UserDTO userDTO);
        public UserDTO Inloggen(UserDTO userDTO);
        public UserDTO GetUserByName(UserDTO userDTO);
        public bool CheckForUser(string naam);
    }
}
