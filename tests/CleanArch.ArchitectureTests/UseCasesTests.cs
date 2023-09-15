using System.Collections;
using System.Reflection;

using MediatR;
using NetArchTest.Rules;

using CleanArch.ArchitectureTests.Common;

namespace CleanArch.ArchitectureTests;

public class UseCasesTests
{
    private readonly Type _requestType = typeof(IBaseRequest);
    private readonly Type[] _targetTypes;

    public UseCasesTests()
    {
        AssemblyHelper.LoadAssemblies("CleanArch.UseCases");

        _targetTypes = Types
            .InCurrentDomain()
            .That().ImplementInterface(_requestType)
            .And().AreNotInterfaces()
            .Or().HaveNameEndingWith("Dto", StringComparison.OrdinalIgnoreCase)
            .GetTypes()
            .ToArray();
    }

    [Fact]
    public void Requests_Dtos_ShouldBeRecords()
    {
        // Act
        var result = _targetTypes
            .All(type => type.IsRecord());

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Requests_Dtos_ShouldContainsArrayCollections()
    {
        // Arrange
        var collectionsTypes = _targetTypes
            .SelectMany(t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            .Where(p => p.PropertyType.IsAssignableTo(typeof(IEnumerable)))
            .Where(p => !p.PropertyType.IsAssignableTo(typeof(string)));

        // Act
        var result = collectionsTypes
            .All(p => p.PropertyType.IsArray);

        // Assert
        Assert.True(result);
    }
}
