using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

public class SVGAnimationElement : IAsyncDisposable
{
    public readonly ElementReference ElementReference;
    protected readonly IJSInProcessObjectReference helper;

    internal SVGAnimationElement(ElementReference elementReference, IJSInProcessObjectReference helper)
    {
        ElementReference = elementReference;
        this.helper = helper;
    }

    public async ValueTask BeginElementAsync()
    {
        await helper.InvokeVoidAsync("beginElement", ElementReference);
    }

    public async ValueTask EndElementAsync()
    {
        await helper.InvokeVoidAsync("endElement", ElementReference);
    }

    public ValueTask DisposeAsync()
    {
        // TODO
        return ValueTask.CompletedTask;
    }
}
