using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Model.Services;
using Services;
using View.LoadPlayerMenu;

namespace Controller;

public sealed class LoadPlayerController : BaseController<Enum>
{
    public Enum ShowLoadPlayerMenu()
    {
        Console.Clear();
        
        var entries = GetEnumEntries(InitializeEnum());
        var selectedPlayer = LoadPlayerView.ShowLoadPlayerMenu(entries);

        return selectedPlayer;
    }

    private Type InitializeEnum()
    {
        var databaseManager = new DatabaseManager();
        var enumBuilderService = new EnumBuilderService();
        var enumData = databaseManager.RetrievePlayers();
        var loadPlayerEnum = enumBuilderService.CreateEnumType("LoadPlayerEnum", enumData.ToList());

        return loadPlayerEnum;
    }

    private List<KeyValuePair<Enum, string>> GetEnumEntries(Type dynamicallyCreatedEnum)
    {
        var entries = new List<KeyValuePair<Enum, string>>();
        foreach (var value in Enum.GetValues(dynamicallyCreatedEnum))
        {
            var displayName = value.GetType().GetField(value.ToString())
                .GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
            entries.Add(new KeyValuePair<Enum, string>((Enum) value, displayName));
        }

        return entries;
    }
}