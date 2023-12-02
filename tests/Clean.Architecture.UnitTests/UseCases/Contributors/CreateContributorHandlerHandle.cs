using System.Reflection;
using Ardalis.SharedKernel;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.UseCases.Contributors.Create;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Clean.Architecture.UnitTests.UseCases.Contributors;


// This file contains the unit tests for the CreateContributorHandler class
// as well as the AutoFixture related classes described in this blog article.
// https://blog.nimblepros.com/blogs/improving-your-tests-with-autofixture/

public class CreateContributorHandlerHandle
{
  private readonly IRepository<Contributor> _repository = Substitute.For<IRepository<Contributor>>();
  private readonly CreateContributorHandler _handler;

  public CreateContributorHandlerHandle()
  {
    _handler = new CreateContributorHandler(_repository);

    _repository.AddAsync(Arg.Any<Contributor>(), Arg.Any<CancellationToken>())
      .Returns(Task.FromResult(new Contributor("John Doe")));
  }

  [Fact]
  public async void ShouldCreateContributorGivenValidValuesFromVariables()
  {
    string email = "JohnDoe@Microsoft.com";
    string firstName = "John";
    string lastName = "Doe";
    int followers = 45;
    int following = 20;
    int stars = 182;
    string status = ContributorStatus.NotSet.Name;
    var command = new CreateContributorCommand(
      Email: email,
      FirstName: firstName,
      LastName: lastName,
      Followers: followers,
      Following: following,
      Stars: stars,
      Status: status);

    var result = await _handler.Handle(command, CancellationToken.None);

    result.IsSuccess.Should().BeTrue();
  }

  [Fact]
  public async void ShouldCreateContributorGivenValuesFromFixture()
  {
    var fixture = new Fixture();
    var command = fixture.Create<CreateContributorCommand>();

    var result = await _handler.Handle(command, CancellationToken.None);

    result.IsSuccess.Should().BeTrue();
  }

  [Fact]
  public async void ShouldCreateContributorGivenValidValuesFromFixtureWithEmailExpression()
  {
    var fixture = new Fixture();
    var command = fixture.Build<CreateContributorCommand>()
      .With(c => c.Email, "JohnDoe@Microsoft.com")
      .Create();

    var result = await _handler.Handle(command, CancellationToken.None);

    result.IsSuccess.Should().BeTrue();
  }

  [Theory, YourDomainData]
  public async void ShouldCreateContributorGivenValidValuesFromAttributes(CreateContributorCommand command)
  {
    var result = await _handler.Handle(command, CancellationToken.None);

    result.IsSuccess.Should().BeTrue();
    result.Value.Should().BeGreaterThan(0);
  }
}

public class ContributorStatusNotSetGenerator : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    var props = request as PropertyInfo;

    if (props is null)
    {
      return new NoSpecimen();
    }

    if (props.PropertyType != typeof(string) || props.Name != "Status")
    {
      return new NoSpecimen();
    }

    return ContributorStatus.NotSet.Name;
  }
}

public class YourDomainSpecificFixture : Fixture
{
  public YourDomainSpecificFixture()
  {
      Customizations.Add(new ContributorStatusNotSetGenerator());
    // Other potential customizations for your domain like a ContributorEmailGenerator, etc.
  }
}

public class YourDomainDataAttribute : AutoDataAttribute
{
  public YourDomainDataAttribute() 
      : base(() => new YourDomainSpecificFixture())
  {
  }
}
