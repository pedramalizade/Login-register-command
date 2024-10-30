

InMemoryDB db = new InMemoryDB();
User loggedIn = null;
UserService userService = new UserService(db);


while (true)
{
    Console.Write("Enter command:");
    string command = Console.ReadLine();
    try
    {
        ProcessCommand(command);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}


void ProcessCommand(string command)
{
    try
    {
        string[] parts = command.Split("--");
        if (parts.Length < 2)
        {
            Console.WriteLine("Invalid command format.");
            return;
        }

        string instruction = parts[0].Trim().ToLower();
        var parameters = new Dictionary<string, string>();

        for (int i = 1; i < parts.Length; i++)
        {
            var keyValue = parts[i].Split(' ');
            if (keyValue.Length >= 2)
            {
                parameters[keyValue[0].Trim().ToLower()] = keyValue[1].Trim();
            }
        }

        switch (instruction)
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
                loggedIn = null;
                break;
            default:
                Console.WriteLine("Unknown command."); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing command: {ex.Message}");
    }
}

