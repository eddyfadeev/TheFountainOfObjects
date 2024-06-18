using System.ComponentModel.DataAnnotations;

namespace Services.Utilities;

public static partial class Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class MethodAttribute(string methodName) : Attribute
    {
        public string MethodName { get; } = methodName;
    }
}