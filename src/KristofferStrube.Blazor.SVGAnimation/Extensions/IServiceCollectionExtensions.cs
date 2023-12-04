using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

public static class IServiceCollectionExtensions
{
    [Obsolete("This doesn't follow the pattern for creation of wrapper object that we wish to continue with so it will be removed in the next major release. Use one of the static SVGAnimationElement.CreateAsync methods instead.")]
    public static IServiceCollection AddSVGAnimationService(this IServiceCollection services)
    {
        return services.AddScoped<ISVGAnimationService>(sp => new SVGAnimationService(sp.GetRequiredService<IJSRuntime>()));
    }
}
