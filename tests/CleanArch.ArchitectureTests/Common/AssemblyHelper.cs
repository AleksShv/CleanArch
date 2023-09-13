using System.Reflection;

namespace CleanArch.ArchitectureTests.Common;

internal class AssemblyHelper
{
    private static readonly string _currentDirectory = Directory.GetCurrentDirectory();

    public static Assembly[] LoadAllAssemblies()
    {
        return Directory.GetFiles(_currentDirectory, "CleanArch.*.dll")
            .Select(Assembly.LoadFrom)
            .ToArray();
    }

    public static IDictionary<string, Assembly> LoadAssemblies(params string[] assembyNamePattern)
    {
        var dict = new Dictionary<string, Assembly>();

        foreach (var name in assembyNamePattern)
        {
            var assemblyFileName = name.EndsWith(".dll")
                ? name
                : name + ".dll";

            foreach (var fileName in Directory.GetFiles(_currentDirectory, assemblyFileName))
            {
                dict[fileName] = Assembly.LoadFrom(fileName);
            }
        }

        return dict;
    }
}
