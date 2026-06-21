using FocusFlow.Api.Shared.EndPoints;
using System.Reflection;

namespace FocusFlow.Api.Extensions;
public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var endpointTypes = assembly.GetTypes()
            .Where(type =>
                typeof(IEndpoint).IsAssignableFrom(type) &&
                type is { IsAbstract: false, IsInterface: false });

        foreach (var endpointType in endpointTypes)
        {
            var endpoint = Activator.CreateInstance(endpointType) as IEndpoint;

            endpoint?.MapEndpoint(app);
        }

    }
}