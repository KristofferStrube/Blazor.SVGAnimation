using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

public class SVGAnimationElement : IAsyncDisposable
{
    public readonly ElementReference ElementReference;
    protected readonly IJSInProcessObjectReference helper;
    private Action? OnBegin = null;
    private Action? OnEnd = null;
    private Action? OnRepeat = null;

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

    public async ValueTask SubscribeToBegin(Action action)
    {
        if (OnBegin is null)
        {
            await helper.InvokeVoidAsync("subscribeToBegin", ElementReference, DotNetObjectReference.Create(this));
        }
        OnBegin += action;
    }

    public async ValueTask SubscribeToEnd(Action action)
    {
        if (OnEnd is null)
        {
            await helper.InvokeVoidAsync("subscribeToEnd", ElementReference, DotNetObjectReference.Create(this));
        }
        OnEnd += action;
    }

    public async ValueTask SubscribeToRepeat(Action action)
    {
        if (OnRepeat is null)
        {
            await helper.InvokeVoidAsync("subscribeToRepeat", ElementReference, DotNetObjectReference.Create(this));
        }
        OnRepeat += action;
    }

    [JSInvokable("InvokeOnBegin")]
    public void InvokeOnBegin() => OnBegin?.Invoke();

    [JSInvokable("InvokeOnEnd")]
    public void InvokeOnEnd() => OnEnd?.Invoke();

    [JSInvokable("InvokeOnRepeat")]
    public void InvokeOnRepeat() => OnRepeat?.Invoke();

    public ValueTask DisposeAsync()
    {
        // TODO
        return ValueTask.CompletedTask;
    }
}
