using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Create;

public sealed class CreateTaskItemCommandRequest : IRequest<CreateTaskItemCommandResponse>
{
}
