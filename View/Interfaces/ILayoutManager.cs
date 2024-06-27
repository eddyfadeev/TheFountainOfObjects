namespace View.Interfaces;

public interface ILayoutManager
{
    public Spectre.Console.Layout MainWindow { get; }
    public Spectre.Console.Layout SupportWindowTop { get; }
    public Spectre.Console.Layout SupportWindowBottom { get; }
    public bool SupportWindowIsVisible { get; set; }
    void UpdateLayout();
    Table CreateTableLayout(string menuName);
    Table CreateInnerTable();
}