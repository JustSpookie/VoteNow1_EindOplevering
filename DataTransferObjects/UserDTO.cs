using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class UserDTO
    {
        public int ID;
        public string Name;
        public string Password;

        public UserDTO() { }

        public UserDTO(int id, string name, string password)
        {
            ID = id;
            Name = name;
            Password = password;

        }
    }
}
