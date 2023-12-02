using Clean.Architecture.Core.ContributorAggregate;
using Xunit;

namespace Clean.Architecture.IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
  [Fact]
  public async Task DeletesItemAfterAddingIt()
  {
    // add a Contributor
    var repository = GetRepository();
    string initialName = Guid.NewGuid().ToString();
    var contributor = new Contributor(
      email: "Email1@Microsoft.com",
      firstName: "Test",
      lastName: "Contributor",
      followers: 1,
      following: 2,
      stars: 3,
      status: ContributorStatus.NotSet.Name);
    await repository.AddAsync(contributor);

    // delete the item
    await repository.DeleteAsync(contributor);

    // verify it's no longer there
    Assert.DoesNotContain(await repository.ListAsync(),
        Contributor => Contributor.FirstName == initialName);
  }
}
