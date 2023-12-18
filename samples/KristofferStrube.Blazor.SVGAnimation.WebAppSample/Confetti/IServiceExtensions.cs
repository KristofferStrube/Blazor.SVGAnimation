namespace KristofferStrube.Blazor.SVGAnimation.WebAppSample.Confetti;

public static class IServiceExtensions
{
    public static IServiceCollection AddConfettiService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<ConfettiService>();
    }
}
