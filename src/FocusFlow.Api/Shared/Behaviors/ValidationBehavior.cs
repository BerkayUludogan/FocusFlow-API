using FluentValidation;
using MediatR;

namespace FocusFlow.Api.Shared.Behaviors;

public class ValidationBehavior<TReq, TRes>(
    IEnumerable<IValidator<TReq>> validators)
    : IPipelineBehavior<TReq, TRes>
    where TReq : IRequest<TRes>
{
    public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TReq>(request);

        var errors = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .Select(f => f.ErrorMessage)
            .Distinct()
            .ToList();

        if (errors.Any())
            throw new Exceptions.RequestValidationException(errors);

        return await next(cancellationToken);
    }
}
