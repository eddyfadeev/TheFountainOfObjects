using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Emit;
using DataObjects.Player;

namespace Services.Utilities;

public static partial class Utilities
{
    public static List<KeyValuePair<Enum,string>> PrepareEnum(IEnumerable<PlayerDTO> enumData, string enumName)
    {
        var generatedEnum = InitializeEnum(enumData, enumName);
        var preparedData = GetEnumEntries(generatedEnum);

        return preparedData;
    }
    
    private static Type InitializeEnum(IEnumerable<PlayerDTO> enumData, string enumName)
    {
        var loadPlayerEnum = BuildEnum(enumData, enumName);

        return loadPlayerEnum;
    }

    private static List<KeyValuePair<Enum, string>> GetEnumEntries(Type dynamicallyCreatedEnum)
    {
        var entries = new List<KeyValuePair<Enum, string>>();
        foreach (var value in Enum.GetValues(dynamicallyCreatedEnum))
        {
            var displayName = GetDisplayName(value as Enum ?? throw new InvalidOperationException("Enum value is null."));
            entries.Add(new KeyValuePair<Enum, string>((Enum) value, displayName));
        }

        return entries;
    }
    
    private static Type CreateEnumType(string enumName, List<PlayerDTO> enumData)
    {
        var assemblyName = new AssemblyName("DynamicEnum");
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");

        var enumBuilder = moduleBuilder.DefineEnum(enumName, TypeAttributes.Public, typeof(int));

        foreach (var player in enumData)
        {
            enumBuilder.DefineLiteral(player.Name, (int)player.Id);
        }

        return enumBuilder.CreateType();
    }
    
    private static string GetDisplayName(Enum value) => 
        value.GetType().GetField(value.ToString())!
            .GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
    
    private static Type BuildEnum(IEnumerable<PlayerDTO> enumData, string enumName) => 
        CreateEnumType(enumName, enumData.ToList());
}