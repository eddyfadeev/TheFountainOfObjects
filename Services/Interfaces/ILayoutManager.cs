using Spectre.Console;

namespace Services.Interfaces;

public interface ILayoutManager
{
    public Layout MainWindow { get; }
    public Layout SupportWindowTop { get; }
    public Layout SupportWindowBottom { get; }
    public bool SupportWindowIsVisible { get; set; }
    void UpdateLayout();
    Table CreateTableLayout(string menuName);
}