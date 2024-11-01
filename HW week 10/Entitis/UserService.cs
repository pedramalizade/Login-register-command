using HW_week_10.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
//using HW_week_10.Interface;
class UserService
{
    User loggedIn = null;

    private UserRepository userRepo;
    public UserService(UserRepository userRepo)
    {
        this.userRepo = userRepo;
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
            string X = parameters["username"];
            string Y = parameters["password"];

            userRepo.Register(X, Y);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
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

            var user = userRepo.Login(X, Y);
            if(user != null)
            {
                Console.WriteLine("Login Successful.");
            }
            else
            {
                Console.WriteLine("Incorrect Username or Password.");
            }
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    public void ChangeStatus(User loggedIn, Dictionary<string, string> parameters)
    {
        try
        {
            if (loggedIn == null)
            {
                Console.WriteLine("Please login first.");
                return;
            }

            if (!parameters.ContainsKey("status"))
            {
                Console.WriteLine("Invalid change status command.");
                return;
            }

            string status = parameters["status"];
            userRepo.ChangeStatus(loggedIn, status);
            //loggedIn.Status = status;
            //Console.WriteLine($"Status changed to {status}.");
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

            string username = parameters["username"].ToLower();
            var users = userRepo.Search(username);
            foreach(var user in users)
            {
                Console.WriteLine($"Username : {user.Username} | Status {user.Status}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during search: {ex.Message}");
        }
    }

    public void ChangePassword(User loggedIn, Dictionary<string, string> parameters)
    {
        try
        {
            if (loggedIn == null)
            {
                Console.WriteLine("Please login first.");
                return;
            }

            if (!parameters.ContainsKey("old") || !parameters.ContainsKey("new"))
            {
                Console.WriteLine("Invalid change password command.");
                return;
            }

            string oldPass = parameters["old"];
            string newPass = parameters["new"];

            userRepo.ChangePassword(loggedIn, oldPass, newPass);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void Logout()
    {
        try
        {
            if(loggedIn == null)
            {
                Console.WriteLine("not User loged in");
            }
            else
            {
                loggedIn = null;
                Console.WriteLine("Logout successfully");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error : {ex.Message}");
        }
    }
}
