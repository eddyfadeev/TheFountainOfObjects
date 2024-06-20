namespace View.Interfaces;

public interface IMenuCommandFactory
{
    ICommand Create(MenuType menuType);
}