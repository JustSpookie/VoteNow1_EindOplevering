using DataTransferObjects;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserContainer
    {
        IUserContainer context;

        public UserContainer(IUserContainer DAL)
        {
            this.context = DAL;
        }

        public int AddUser(User user)
        {
            return context.AddUser(user.TODTO());
        }

        public bool DeleteUser(User user)
        {
            return context.DeleteUser(user.TODTO());
        }

        public User GetUser(int userid)
        {
            return new User(context.GetUser(userid));
        }

        public User GetUserByName(User user)
        {
            return new User(context.GetUserByName(user.TODTO()));
        }


        public User inloggen(User user)
        {
            return new User(context.Inloggen(user.TODTO()));
        }

        public bool CheckUser(string username)
        {
            return context.CheckForUser(username);
        }


        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            foreach(UserDTO userDTO in context.GetUsers())
            {
                users.Add(new User(userDTO));
            }
            return users;
        }
    }
}
