using Clean.Architecture.Core.ContributorAggregate;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.ContributorAggregate;

public class ContributorConstructor
{
  private readonly string _testEmail = "john.doe@Microsot.com";
  private readonly string _testFirstName = "test first name";
  private readonly string _testLastName = "test last name";
  private readonly int _testFollowers = 1;
  private readonly int _testFollowing = 2;
  private readonly int _testStars = 3;
  private readonly string _testStatus = ContributorStatus.NotSet.Name;

  [Fact]
  public void InitializesFirstName()
  {
    var contributor = new Contributor(
      _testEmail,
      _testFirstName,
      _testLastName,
      _testFollowers,
      _testFollowing,
      _testStars,
      _testStatus);

    Assert.Equal(_testFirstName, contributor.FirstName);
  }
}
