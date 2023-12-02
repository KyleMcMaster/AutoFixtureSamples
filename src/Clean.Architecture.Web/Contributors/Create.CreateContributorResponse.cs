namespace Clean.Architecture.Web.Endpoints.ContributorEndpoints;

public class CreateContributorResponse(int id, string firstName, string lastName)
{
  public int Id { get; set; } = id;
  public string FirstName { get; set; } = firstName;
  public string LastName { get; set; } = lastName;
}
