using NetArchTest.Rules;
using CleanArch.DataAccess.Contracts;
using CleanArch.DataAccess.SqlServer;
using CleanArch.Controllers.Common;

namespace CleanArch.ArchitectureTests;

public class ControllersTests
{
    private readonly Type _baseControllerType = typeof(ApiControllerBase);

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
        var result = Types
            .InCurrentDomain()
            .That().Inherit(_baseControllerType)
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
        var result = Types
            .InCurrentDomain()
            .That().Inherit(_baseControllerType)
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
        var result = Types
            .InCurrentDomain()
            .That().Inherit(_baseControllerType)
            .ShouldNot().HaveDependencyOnAny("CleanArch.Entities", "CleanArxh.DomainServices")
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(result);
    }
}