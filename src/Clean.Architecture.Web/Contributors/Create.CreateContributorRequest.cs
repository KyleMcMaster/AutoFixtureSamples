using System.ComponentModel.DataAnnotations;
using Clean.Architecture.Core.ContributorAggregate;

namespace Clean.Architecture.Web.Endpoints.ContributorEndpoints;

public class CreateContributorRequest
{
  public const string Route = "/Contributors";

  [Required]
  public string FirstName { get; set; } = string.Empty;
  public string Email { get; internal set; } = string.Empty;
  public string LastName { get; internal set; } = string.Empty;
  public int Followers { get; internal set; }
  public int Following { get; internal set; }
  public int Stars { get; internal set; }
  public string Status { get; internal set; } = ContributorStatus.NotSet.Name;
}
