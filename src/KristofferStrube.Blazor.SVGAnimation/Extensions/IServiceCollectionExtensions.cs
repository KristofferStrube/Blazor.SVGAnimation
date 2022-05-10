using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSVGAnimationService(this IServiceCollection services)
    {
        return services.AddScoped<ISVGAnimationService>(sp => new SVGAnimationService(sp.GetRequiredService<IJSRuntime>()));
    }
}
