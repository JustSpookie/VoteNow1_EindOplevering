using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class User
    {
        public int ID;
        public string Name;
        public string Password;

        public User(int id, string name, string password)
        {
            ID = id;
            Name = name;
            Password = password;
        }

        public UserDTO TODTO()
        {
            return new UserDTO(ID, Name, Password);
        }

        public User(UserDTO userDTO)
        {
            ID = userDTO.ID;
            Name = userDTO.Name;
            Password = userDTO.Password;
        }
    }
}
