using Spectre.Console;

namespace Interfaces.View.Menu;

public interface ISideMenu<in TEnum>
{
    public Table GetSideTable(TEnum menuType);
}