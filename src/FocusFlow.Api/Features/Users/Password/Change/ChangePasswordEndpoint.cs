using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Users.Password.Change;

public sealed class ChangePasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/password", async (
            ChangePasswordRequest request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var command = new ChangePasswordCommandRequest
            {
                UserId = httpContext.User.GetUserId(),
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword,
            };

            var response = await sender.Send(command, cancellationToken);

            return Results.Ok(response);
        }).WithTags("Users").RequireAuthorization();
    }
}
