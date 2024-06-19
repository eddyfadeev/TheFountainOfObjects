namespace View.Interfaces;

public interface IMenuCommandFactory
{
    ICommand Create(string menuName);
}