using System.Xml.Linq;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Clean.Architecture.Core.ContributorAggregate;

public class Contributor(
  string email,
  string firstName,
  string lastName,
  int followers,
  int following,
  int stars,
  string status) : EntityBase, IAggregateRoot
{
  // Example of validating primary constructor inputs
  // See: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors#initialize-base-class
  public string Email { get; internal set; } = Guard.Against.NullOrEmpty(email, nameof(email));
  public string FirstName { get; set; } = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
  public string LastName { get; internal set; } = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
  public int Followers { get; internal set; } = Guard.Against.Negative(followers, nameof(followers));
  public int Following { get; internal set; } = Guard.Against.Negative(following, nameof(following));
  public int Stars { get; internal set; } = Guard.Against.Negative(stars, nameof(stars));
  public string Status { get; internal set; } = Guard.Against.NullOrEmpty(status, nameof(status));

  public void UpdateFirstName(string firstName)
  {
    FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
  }
}
