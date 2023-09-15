using NetArchTest.Rules;

using CleanArch.DataAccess.Contracts;
using CleanArch.DataAccess.SqlServer;
using CleanArch.Controllers.Common;

namespace CleanArch.ArchitectureTests;

public class ControllersTests
{
    private readonly Type _baseControllerType = typeof(ApiControllerBase);
    private readonly PredicateList _targetTypes;

    public ControllersTests()
    {
        _targetTypes = Types
            .InCurrentDomain()
            .That().Inherit(_baseControllerType);
    }


    [Fact]
    public void ControllersShouldNotHaveDependencyOnDataAccess()
    {
        // Arrange
        var dataAccessDependencies = new string?[]
        {
            typeof(IApplicationDbContext).Namespace,
            typeof(ApplicationDbContext).Namespace
        };

        // Act
        var result = _targetTypes
            .ShouldNot().HaveDependencyOnAny(dataAccessDependencies)
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ControllersShouldNotHaveDependencyOnInfrastructure()
    {
        // Act
        var result = _targetTypes
            .ShouldNot().HaveDependencyOnAny("CleanArch.Infrastructure.Contracts", "CleanArch.Infrastructure.Implementations")
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ControllersShouldNotHaveDependencyOnEntities()
    {
        // Act
        var result = _targetTypes
            .ShouldNot().HaveDependencyOnAny("CleanArch.Entities", "CleanArch.DomainServices")
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(result);
    }
}