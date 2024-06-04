using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Emit;
using Model.DataObjects;

namespace Services;

public class EnumBuilderService
{
    public Type CreateEnumType(string enumName, List<PlayerDTO> enumData)
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
}