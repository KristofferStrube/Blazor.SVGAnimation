using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

/// <summary>
/// <see href="https://svgwg.org/specs/animations/#InterfaceSVGAnimationElement">SVGAnimationElement browser specs</see>
/// </summary>
public class SVGAnimationElement : IAsyncDisposable
{
    public readonly ElementReference ElementReference;
    protected readonly IJSInProcessObjectReference helper;
    protected Lazy<Task<IJSInProcessObjectReference>> jSElementReference;
    private DotNetObjectReference<SVGAnimationElement> objRef;
    private Action? OnBegin = null;
    private Action? OnEnd = null;
    private Action? OnRepeat = null;

    internal SVGAnimationElement(ElementReference elementReference, IJSInProcessObjectReference helper)
    {
        ElementReference = elementReference;
        this.helper = helper;
        jSElementReference = new(() => helper.InvokeAsync<IJSInProcessObjectReference>("getJSReference", elementReference).AsTask());
        objRef = DotNetObjectReference.Create(this);
    }

    public IJSObjectReference TargetElement => helper.Invoke<IJSObjectReference>("targetElement", ElementReference);

    public async ValueTask SubscribeToBeginAsync(Action action)
    {
        if (OnBegin is null)
        {
            await helper.InvokeVoidAsync("subscribeToBegin", ElementReference, objRef);
        }
        OnBegin += action;
    }

    public async ValueTask SubscribeToEndAsync(Action action)
    {
        if (OnEnd is null)
        {
            await helper.InvokeVoidAsync("subscribeToEnd", ElementReference, objRef);
        }
        OnEnd += action;
    }

    public async ValueTask SubscribeToRepeatAsync(Action action)
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

    public async ValueTask<float> GetStartTimeAsync()
    {
        var element = await jSElementReference.Value;
        return await element.InvokeAsync<float>("getStartTime");
    }

    public async ValueTask<float> GetCurrentTimeAsync()
    {
        var element = await jSElementReference.Value;
        return await element.InvokeAsync<float>("getCurrentTime");
    }

    public async ValueTask<float> GetSimpleDurationAsync()
    {
        var element = await jSElementReference.Value;
        return await element.InvokeAsync<float>("getSimpleDuration");
    }

    public async ValueTask BeginElementAsync()
    {
        var element = await jSElementReference.Value;
        await element.InvokeVoidAsync("beginElement");
    }

    public async ValueTask BeginElementAtAsync(float offset)
    {
        var element = await jSElementReference.Value;
        await element.InvokeVoidAsync("beginElementAt", offset);
    }

    public async ValueTask EndElementAsync()
    {
        var element = await jSElementReference.Value;
        await element.InvokeVoidAsync("endElement");
    }

    public async ValueTask EndElementAtAsync(float offset)
    {
        var element = await jSElementReference.Value;
        await element.InvokeVoidAsync("endElementAt", offset);
    }

    public async ValueTask DisposeAsync()
    {
        if (jSElementReference.IsValueCreated)
        {
            var element = await jSElementReference.Value;
            await element.DisposeAsync();
        }
    }
}
