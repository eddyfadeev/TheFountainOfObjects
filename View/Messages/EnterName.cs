using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class EnterName : IMessage
{
    #region Methods

    public string GetMessage() => "[white]Please enter your name:[/]";

    #endregion
}