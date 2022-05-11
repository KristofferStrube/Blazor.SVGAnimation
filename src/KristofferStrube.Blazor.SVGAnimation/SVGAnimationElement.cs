using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

public class SVGAnimationElement : IDisposable
{
    public readonly ElementReference ElementReference;
    protected readonly IJSInProcessObjectReference helper;
    private DotNetObjectReference<SVGAnimationElement> objRef;
    private Action? OnBegin = null;
    private Action? OnEnd = null;
    private Action? OnRepeat = null;

    internal SVGAnimationElement(ElementReference elementReference, IJSInProcessObjectReference helper)
    {
        ElementReference = elementReference;
        this.helper = helper;
        objRef = DotNetObjectReference.Create(this);
    }

    public async ValueTask BeginElementAsync()
    {
        await helper.InvokeVoidAsync("beginElement", ElementReference);
    }

    public async ValueTask EndElementAsync()
    {
        await helper.InvokeVoidAsync("endElement", ElementReference);
    }

    public async ValueTask SubscribeToBegin(Action action)
    {
        if (OnBegin is null)
        {
            await helper.InvokeVoidAsync("subscribeToBegin", ElementReference, objRef);
        }
        OnBegin += action;
    }

    public async ValueTask SubscribeToEnd(Action action)
    {
        if (OnEnd is null)
        {
            await helper.InvokeVoidAsync("subscribeToEnd", ElementReference, objRef);
        }
        OnEnd += action;
    }

    public async ValueTask SubscribeToRepeat(Action action)
    {
        if (OnRepeat is null)
        {
            await helper.InvokeVoidAsync("subscribeToRepeat", ElementReference, objRef);
        }
        OnRepeat += action;
    }

    [JSInvokable("InvokeOnBegin")]
    public void InvokeOnBegin() => OnBegin?.Invoke();

    [JSInvokable("InvokeOnEnd")]
    public void InvokeOnEnd() => OnEnd?.Invoke();

    [JSInvokable("InvokeOnRepeat")]
    public void InvokeOnRepeat() => OnRepeat?.Invoke();

    public void Dispose()
    {
        objRef.Dispose();
    }
}
