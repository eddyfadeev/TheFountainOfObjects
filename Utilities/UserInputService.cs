namespace Utilities;

public static class UserInputService
{
    // TODO: Esc press is not handled correctly
    public static string? AskForInput()
    {
        var inputHandler = new ConsoleInputHandler();
        inputHandler.Initialize();

        while (true)
        {
            var keyChar = inputHandler.ReadKey();
            
            if (keyChar == null)
            {
                continue;
            }

            if (inputHandler.HandleSpecialKeys(keyChar.Value) is null)
            {
                return null;
            }
            
            if (inputHandler.HandleSpecialKeys(keyChar.Value) is true)
            {
                return inputHandler.GetInput();
            }

            if (IsValidCharacter(keyChar.Value))
            {
                inputHandler.InsertCharacter(keyChar.Value);
            }
        }
    }

    private static bool IsValidCharacter(char keyChar) =>
        char.IsLetterOrDigit(keyChar) || char.IsWhiteSpace(keyChar) || char.IsPunctuation(keyChar) || char.IsSymbol(keyChar);
}