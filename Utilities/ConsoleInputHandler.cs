using System.Text;
using Spectre.Console;

namespace Utilities;

internal class ConsoleInputHandler
{
    private StringBuilder? _nameBuilder;
    private int _cursorPosition;

    public void Initialize()
    {
        Console.CursorVisible = true;
        _nameBuilder = new StringBuilder();
        _cursorPosition = 0;
    }

    public char? ReadKey()
    {
        var keyInt = Console.Read();
        return keyInt == -1 ? (char?)null : (char)keyInt;
    }

    public bool? HandleSpecialKeys(char keyChar)
    {
        switch (keyChar)
        {
            case '\u001b': // Escape key
                return null;
            case '\r':
            case '\n': // Enter key
                if (!string.IsNullOrWhiteSpace(_nameBuilder?.ToString()))
                {
                    Console.WriteLine();
                    return true;
                }
                break;
            case '\b': // Backspace key
                HandleBackspace();
                break;
            case (char)127: // Delete key (ASCII 127)
                HandleDelete();
                break;
            case '\t': // Ignore Tab key
                break;
        }
        return false;
    }

    public void InsertCharacter(char keyChar)
    {
        _nameBuilder.Insert(_cursorPosition, keyChar);
        _cursorPosition++;
        AnsiConsole.Write(_nameBuilder.ToString().Substring(_cursorPosition - 1));
        Console.CursorLeft = _cursorPosition;
    }

    public string GetInput() => _nameBuilder.ToString();

    private void HandleBackspace()
    {
        if (_cursorPosition > 0)
        {
            _nameBuilder.Remove(_cursorPosition - 1, 1);
            _cursorPosition--;
            Console.CursorLeft--;
            UpdateDisplay();
        }
    }

    private void HandleDelete()
    {
        if (_cursorPosition < _nameBuilder.Length)
        {
            _nameBuilder.Remove(_cursorPosition, 1);
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        Console.Write(_nameBuilder.ToString().Substring(_cursorPosition) + " ");
        Console.CursorLeft = _cursorPosition;
    }
}