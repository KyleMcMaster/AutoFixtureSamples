using Clean.Architecture.Core.ContributorAggregate;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Clean.Architecture.IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
  [Fact]
  public async Task UpdatesItemAfterAddingIt()
  {
    // add a Contributor
    var repository = GetRepository();
    var contributor = new Contributor(
      email: "Email1@Microsoft.com",
      firstName: "Test",
      lastName: "Contributor",
      followers: 1,
      following: 2,
      stars: 3,
      status: ContributorStatus.NotSet.Name);

    await repository.AddAsync(contributor);

    // detach the item so we get a different instance
    _dbContext.Entry(contributor).State = EntityState.Detached;

    // fetch the item and update its title
    var newContributor = (await repository.ListAsync())
        .FirstOrDefault(Contributor => Contributor.FirstName == contributor.FirstName);
    if (newContributor == null)
    {
      Assert.NotNull(newContributor);
      return;
    }
    Assert.NotSame(contributor, newContributor);
    var newName = Guid.NewGuid().ToString();
    newContributor.UpdateFirstName(newName);

    // Update the item
    await repository.UpdateAsync(newContributor);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
        .FirstOrDefault(Contributor => Contributor.FirstName == newName);

    Assert.NotNull(updatedItem);
    Assert.NotEqual(contributor.FirstName, updatedItem?.FirstName);
    Assert.Equal(contributor.Status, updatedItem?.Status);
    Assert.Equal(newContributor.Id, updatedItem?.Id);
  }
}
