using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
//using HW_week_10.Interface;
class UserService
{
    private InMemoryDB db;

    public UserService(InMemoryDB db)
    {
        this.db = db;
    }

    public void Register(Dictionary<string, string> parameters)
    {
        try
        {
            if (!parameters.ContainsKey("username") || !parameters.ContainsKey("password"))
            {
                Console.WriteLine("Invalid register command.");
                return;
            }

            string username = parameters["username"];
            string password = parameters["password"];

            if (db.Users.Exists(u => u.Username == username))
            {
                Console.WriteLine("Register failed! Username already exists.");
            }
            else
            {
                db.Users.Add(new User(username, password));
                Console.WriteLine("User registered successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during registration: {ex.Message}");
        }
    }

    public User Login(Dictionary<string, string> parameters)
    {
        try
        {
            if (!parameters.ContainsKey("username") || !parameters.ContainsKey("password"))
            {
                Console.WriteLine("Invalid login command.");
                return null;
            }

            string X = parameters["username"];
            string Y = parameters["password"];

            User user = db.Users.Find(u => u.Username == X && u.Password == Y);
            if (user != null)
            {
                Console.WriteLine("Login successful.");
                return user;
            }
            else
            {
                Console.WriteLine("Login failed! Incorrect username or password.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during login: {ex.Message}");
            return null;
        }
    }

    public void ChangeStatus(User loggedInUser, Dictionary<string, string> parameters)
    {
        try
        {
            if (loggedInUser == null)
            {
                Console.WriteLine("Access denied! Please login first.");
                return;
            }

            if (!parameters.ContainsKey("status"))
            {
                Console.WriteLine("Invalid change status command.");
                return;
            }

            string status = parameters["status"];
            loggedInUser.Status = status;
            Console.WriteLine($"Status changed to {status}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing status: {ex.Message}");
        }
    }

    public void Search(Dictionary<string, string> parameters)
    {
        try
        {
            if (!parameters.ContainsKey("username"))
            {
                Console.WriteLine("Invalid search command.");
                return;
            }

            string usernameQuery = parameters["username"].ToLower();
            foreach (var user in db.Users)
            {
                if (user.Username.ToLower().Contains(usernameQuery))
                {
                    Console.WriteLine($"{user.Username} | status: {user.Status}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during search: {ex.Message}");
        }
    }

    public void ChangePassword(User loggedInUser, Dictionary<string, string> parameters)
    {
        try
        {
            if (loggedInUser == null)
            {
                Console.WriteLine("Access denied! Please login first.");
                return;
            }

            if (!parameters.ContainsKey("old") || !parameters.ContainsKey("new"))
            {
                Console.WriteLine("Invalid change password command.");
                return;
            }

            string oldPassword = parameters["old"];
            string newPassword = parameters["new"];

            if (loggedInUser.Password == oldPassword)
            {
                loggedInUser.Password = newPassword;
                Console.WriteLine("Password changed successfully.");
            }
            else
            {
                Console.WriteLine("Change password failed! Incorrect old password.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing password: {ex.Message}");
        }
    }

    public void Logout()
    {
        try
        {
            Console.WriteLine("Logout successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during logout: {ex.Message}");
        }
    }
}
