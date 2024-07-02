namespace View.Views.CreatePlayerScreen;

public class CreatePlayerScreen
{
    public string AskForUserName()
    {
        const string message = "Please enter your name:";
        var userName = GetUserInput(ChangeStringColor(message, Color.White));
        
        return userName;
    }

    public void ShowAlreadyExistsMessage()
    {
        const string message = "This name is already taken. Please, choose another one.";
        
        AnsiConsole.MarkupLine(ChangeStringColor(message, Color.Red));
    }
    
    public void ShowPlayerCreatedMessage()
    {
        const string message = "Player created";
        
        AnsiConsole.MarkupLine(ChangeStringColor(message, Color.Green));
    }
    
    public void ShowPlayerDoesNotExistMessage()
    {
        const string message = "Player does not exist. Please, create a new one.";
        
        AnsiConsole.MarkupLine(ChangeStringColor(message, Color.Red));
    }
}