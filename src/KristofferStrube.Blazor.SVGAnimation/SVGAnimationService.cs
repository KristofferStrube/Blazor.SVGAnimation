using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class SVGAnimationService : IAsyncDisposable, ISVGAnimationService
{
    private readonly Lazy<Task<IJSInProcessObjectReference>> helperTask;

    public SVGAnimationService(IJSRuntime jsRuntime)
    {
        helperTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.SVGAnimation/KristofferStrube.Blazor.SVGAnimation.js").AsTask());
    }

    public async ValueTask<SVGAnimationElement> GreateSVGAnimationElement(ElementReference elementReference)
    {
        var helper = await helperTask.Value;
        return new SVGAnimationElement(elementReference, helper);
    }

    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            var module = await helperTask.Value;
            await module.DisposeAsync();
        }
    }
}
