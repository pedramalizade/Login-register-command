using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_week_10.Coniguration
{
    public static class Configuration
    {
        public static string ConnectionString { get; set; }
        static Configuration()
        {
            ConnectionString = "Server=DESKTOP-O8SFUP7\\SQLEXP; DataBase=HW 10; Integrated Security=True;";
        }
    }
}
