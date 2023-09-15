using System.Reflection;

namespace CleanArch.ArchitectureTests.Common;

internal static class TypeExtensions
{
    public static bool IsRecord(this Type type)
        => type.GetMethod("<Clone>$", BindingFlags.Instance | BindingFlags.Public) is not null;
}
