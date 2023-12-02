using FastEndpoints;
using Clean.Architecture.Web.Endpoints.ContributorEndpoints;
using Clean.Architecture.UseCases.Contributors.Create;
using MediatR;

namespace Clean.Architecture.Web.ContributorEndpoints;

/// <summary>
/// Create a new Contributor
/// </summary>
/// <remarks>
/// Creates a new Contributor given a name.
/// </remarks>
public class Create(IMediator _mediator)
  : Endpoint<CreateContributorRequest, CreateContributorResponse>
{
  public override void Configure()
  {
    Post(CreateContributorRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      // XML Docs are used by default but are overridden by these properties:
      //s.Summary = "Create a new Contributor.";
      //s.Description = "Create a new Contributor. A valid name is required.";
      s.ExampleRequest = new CreateContributorRequest 
      {
        Email = "Contributor Email",
        FirstName = "Contributor FirstName",
        LastName = "Contributor LastName",
        Followers = 123,
        Following = 456,
        Stars = 789,
        Status = "Not Set"
      };
    });
  }

  public override async Task HandleAsync(
    CreateContributorRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateContributorCommand(
      Email: request.Email,
      FirstName: request.FirstName,
      LastName: request.LastName,
      Followers: request.Followers,
      Following: request.Following,
      Stars: request.Stars,
      Status: request.Status),
      cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateContributorResponse(result.Value, request.FirstName, request.LastName);
      return;
    }
    // TODO: Handle other cases as necessary
  }
}
