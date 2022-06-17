using System.ComponentModel.DataAnnotations;

namespace ASP_View.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserModel(int id, string username, string password)
        {
            Id = id;
            UserName = username;
            Password = password;
        }
        public UserModel(string username, string password)
        {
            UserName = username;
            Password = password;
            Id = 0;
        }

        public UserModel() { }
    }
}
