using Ardalis.Result;

namespace Clean.Architecture.UseCases.Contributors.Create;

/// <summary>
/// Create a new Contributor.
/// </summary>
/// <param name="Name"></param>
public record CreateContributorCommand(
  string Email,
  string FirstName,
  string LastName,
  int Followers,
  int Following,
  int Stars,
  string Status)
  : Ardalis.SharedKernel.ICommand<Result<int>>;
