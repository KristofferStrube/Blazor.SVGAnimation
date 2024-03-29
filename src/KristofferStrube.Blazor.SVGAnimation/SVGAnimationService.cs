using KristofferStrube.Blazor.SVGAnimation.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

public class SVGAnimationService : IAsyncDisposable, ISVGAnimationService
{
    protected readonly IJSRuntime jSRuntime;
    private readonly Lazy<Task<IJSInProcessObjectReference>> helperTask;

    [Obsolete("This doesn't follow the pattern for creation of wrapper object that we wish to continue with so it will be removed in the next major release. Use one of the static SVGAnimationElement.CreateAsync methods instead.")]
    public SVGAnimationService(IJSRuntime jsRuntime)
    {
        this.jSRuntime = jsRuntime;
        helperTask = new(jSRuntime.GetInProcessHelperAsync);
    }

    [Obsolete("This doesn't follow the pattern for creation of wrapper object that we wish to continue with so it will be removed in the next major release. Use one of the static SVGAnimationElement.CreateAsync methods instead.")]
    public async ValueTask<SVGAnimationElement> GreateSVGAnimationElement(ElementReference elementReference)
    {
        IJSInProcessObjectReference helper = await helperTask.Value;
        IJSObjectReference jsReference = await helper.InvokeAsync<IJSObjectReference>("getJSReference", elementReference);
        return new SVGAnimationElement(jSRuntime, jsReference, helper);
    }

    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSInProcessObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
    }
}
