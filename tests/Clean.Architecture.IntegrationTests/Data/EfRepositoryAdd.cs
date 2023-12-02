using Clean.Architecture.Core.ContributorAggregate;
using Xunit;

namespace Clean.Architecture.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsContributorAndSetsId()
  {
    string testContributorFirstName = "Kyle";
    string testContributorStatus = ContributorStatus.NotSet.Name;
    var repository = GetRepository();
    var contributor = new Contributor(
      email: "Email1@Microsoft.com",
      firstName: testContributorFirstName,
      lastName: "Contributor",
      followers: 1,
      following: 2,
      stars: 3,
      status: testContributorStatus);

    await repository.AddAsync(contributor);

    var newContributor = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal(testContributorFirstName, newContributor?.FirstName);
    Assert.Equal(testContributorStatus, newContributor?.Status);
    Assert.True(newContributor?.Id > 0);
  }
}
