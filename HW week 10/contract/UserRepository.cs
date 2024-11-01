using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_week_10.contract
{
    public class UserRepository
    {
        private string connectionString;
        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Register(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var existingUser = connection.QueryFirstOrDefault<User>("select * from Users where Username = @Username", new { Username = username });
                if (existingUser != null)
                {
                    Console.WriteLine("Username alreasy exist");
                    return false;
                }
                connection.Execute("insert into Users (Username, Password, Status) values (@Username, @Password, @Status)", new {Username = username, Password = password, Status = "available"});
                Console.WriteLine("Registered Successflly");
                return true;
            }
        } 

        public User Login(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<User>("select * from Users where username = @Username And Password = @Password", new { Username = username, Password = password });
            }
        }
        public void ChangeStatus(User user, string  status)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("update Users set Status = @Status where Username = @Username", new {Status = status, Username = user.Username});
                Console.WriteLine($"status change to {status}");
            }
        }
        public List<User> Search(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
               return connection.Query<User>($"select * from Users where username like '{@username}%'", new { Username = username }).ToList();
            }
        }

        public bool ChangePassword(User user, string oldPassword, string newPassword)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var existingUser = connection.QueryFirstOrDefault<User>("select * from Users where Id = @Id" , new {Id =  user.Id}); 
                if (existingUser == null)
                {
                    Console.WriteLine("Incorrect old password");
                    return false;
                }
                connection.Execute("update Users set Password = @NewPassword where Username = @Username", new { newPassword = newPassword , Username = user.Username});
                Console.WriteLine("Password Change,");
                return true;
            }
        }
    }
}
