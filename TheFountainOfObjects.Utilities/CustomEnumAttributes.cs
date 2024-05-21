namespace TheFountainOfObjects.Utilities;

public static class CustomEnumAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class MethodAttribute(string methodName) : Attribute
    {
        public string MethodName { get; } = methodName;
    }
}