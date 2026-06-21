using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Create;

public sealed class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommandRequest, CreateTaskItemCommandResponse>
{
    public Task<CreateTaskItemCommandResponse> Handle(CreateTaskItemCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
