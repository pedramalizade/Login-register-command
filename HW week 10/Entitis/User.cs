using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class User
{
    public int Id { get; set; }
    public string Username { get; private set; }
    public string Password { get; set; }
    public string Status { get; set; }

    public User(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
        Status = "available";
    }
    public User()
    {
        
    }
}
