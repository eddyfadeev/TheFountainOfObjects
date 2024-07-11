namespace View.Views.CreatePlayerScreen;

public class CreatePlayerScreen
{
    public string AskForUserName()
    {
        const string message = "Please enter your name:";
        var userName = GetUserInput($"[white]{ message }[/]");
        
        return userName;
    }

    public void ShowAlreadyExistsMessage()
    {
        const string message = "This name is already taken. Please, choose another one.";
        
        AnsiConsole.MarkupLine($"[red]{ message }[/]");
    }
    
    public void ShowPlayerCreatedMessage()
    {
        const string message = "Player created";
        
        AnsiConsole.MarkupLine($"[green]{ message }[/]");
    }
    
}