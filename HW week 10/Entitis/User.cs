using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class User
{
    public string Username { get; private set; }
    public string Password { get; set; }
    public string Status { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Status = "available";
    }
}

