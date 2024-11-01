
//InMemoryDB db = new InMemoryDB();
//UserService userService = new UserService(db);

using HW_week_10.contract;

string connectionString = "Server=DESKTOP-O8SFUP7\\SQLEXP; DataBase=HW 10; Integrated Security=True; TrustServerCertificate=True;";
UserRepository userRepo = new UserRepository(connectionString);
UserService userService = new UserService(userRepo);
User loggedIn = null;



start();

void start()
{
    while (true)
    {
        Console.Write("Enter command:");
        string command = Console.ReadLine();
        try
        {
            CheckCommand(command);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error : {ex.Message}");
        }
    }
}

void CheckCommand(string command)
{
    try
    {

        string[] parts = command.Split("--");
        if (parts.Length < 2)
        {
            Console.WriteLine("Invalid command format.");
            return;
        }
        var parameters = new Dictionary<string, string>();
        string order = parts[0].Trim().ToLower();

        for (int i = 1; i < parts.Length; i++)
        {
            var keyValue = parts[i].Split(' ');
            if (keyValue.Length >= 2)
            {
                parameters[keyValue[0].Trim().ToLower()] = keyValue[1].Trim();
            }
        }

        switch (order)
        {
            case "register":
                userService.Register(parameters); break;
            case "login":
                loggedIn = userService.Login(parameters); break;
            case "change":
                userService.ChangeStatus(loggedIn, parameters); break;
            case "search":
                userService.Search(parameters); break;
            case "changepassword":
                userService.ChangePassword(loggedIn, parameters); break;
            case "logout":
                userService.Logout();
                Console.WriteLine("logout successfully.");
                start();

                break;
            default:
                Console.WriteLine("Unknown command."); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error : {ex.Message}");
    }
}

