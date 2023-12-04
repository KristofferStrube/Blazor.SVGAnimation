using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.SVGAnimation.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation;

/// <summary>
/// <see href="https://svgwg.org/specs/animations/#InterfaceSVGAnimationElement">SVGAnimationElement browser specs</see>
/// </summary>
public class SVGAnimationElement : EventTarget
{
    // Old Obsolete fields.
    protected readonly IJSInProcessObjectReference helper;

    // New fields that will be the only ones used after the next major version
    protected readonly Lazy<Task<IJSObjectReference>> svgAnimationHelperTask;

    public static new Task<SVGAnimationElement> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult<SVGAnimationElement>(new(jSRuntime, jSReference));
    }

    public static new async Task<SVGAnimationElement> CreateAsync(IJSRuntime jSRuntime, ElementReference elementReference)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getJSReference", elementReference);
        return new(jSRuntime, jSInstance);
    }

    [Obsolete("This is not compatible with Blazor WASM so it will be removed in the next major version. Either use the SVGAnimationElement(IJSRuntime jSRuntime, IJSObjectReference jSReference) constructor instead.")]
    internal SVGAnimationElement(IJSRuntime jSRuntime, IJSObjectReference jSReference, IJSInProcessObjectReference helper) : this(jSRuntime, jSReference)
    {
        this.helper = helper;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. This will be removed once we move await from the IJSInProcessObjectReference in next major release.
    protected SVGAnimationElement(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        svgAnimationHelperTask = new(jSRuntime.GetHelperAsync);
    }

    [Obsolete("This is not compatible with Blazor WASM so it will be removed in the next major version. Either use the SVGAnimationElementInProcess variant or use the GetTargetElementAsync method instead.")]
    public IJSObjectReference TargetElement => helper.Invoke<IJSObjectReference>("getAttribute", JSReference, "targetElement");

    public async Task<IJSObjectReference> GetTargetElementAsync()
    {
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "targetElement");
    }

    public async Task<EventListener<Event>> AddOnBeginEventListenerAsync(Func<Event, Task> callback, AddEventListenerOptions? options = null)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, callback);
        await AddEventListenerAsync("beginEvent", eventListener, options);
        return eventListener;
    }

    public async Task RemoveOnBeginEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("beginEvent", callback, options);
    }

    [Obsolete("This used an old way to make events where you could not unsubscribe so it will be removed in the next major version. Use AddOnBeginEventListener instead.")]
    public async ValueTask SubscribeToBeginAsync(Action action)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, (Event e) => { action(); return Task.CompletedTask; });
        await AddEventListenerAsync("beginEvent", eventListener);
    }

    public async Task<EventListener<Event>> AddOnEndEventListenerAsync(Func<Event, Task> callback, AddEventListenerOptions? options = null)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, callback);
        await AddEventListenerAsync("endEvent", eventListener, options);
        return eventListener;
    }

    public async Task RemoveOnEndEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("endEvent", callback, options);
    }

    [Obsolete("This used an old way to make events where you could not unsubscribe so it will be removed in the next major version. Use AddOnEndEventListener instead.")]
    public async ValueTask SubscribeToEndAsync(Action action)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, (Event e) => { action(); return Task.CompletedTask; });
        await AddEventListenerAsync("endEvent", eventListener);
    }

    public async Task<EventListener<Event>> AddOnRepeatEventListenerAsync(Func<Event, Task> callback, AddEventListenerOptions? options = null)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, callback);
        await AddEventListenerAsync("repeatEvent", eventListener, options);
        return eventListener;
    }

    public async Task RemoveOnRepeatEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("repeatEvent", callback, options);
    }

    [Obsolete("This used an old way to make events where you could not unsubscribe so it will be removed in the next major version. Use AddOnRepeatEventListener instead.")]
    public async ValueTask SubscribeToRepeatAsync(Action action)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, (Event e) => { action(); return Task.CompletedTask; });
        await AddEventListenerAsync("repeatEvent", eventListener);
    }

    public async ValueTask<float> GetStartTimeAsync()
    {
        return await JSReference.InvokeAsync<float>("getStartTime");
    }

    public async ValueTask<float> GetCurrentTimeAsync()
    {
        return await JSReference.InvokeAsync<float>("getCurrentTime");
    }

    public async ValueTask<float> GetSimpleDurationAsync()
    {
        return await JSReference.InvokeAsync<float>("getSimpleDuration");
    }

    public async ValueTask BeginElementAsync()
    {
        await JSReference.InvokeVoidAsync("beginElement");
    }

    public async ValueTask BeginElementAtAsync(float offset)
    {
        await JSReference.InvokeVoidAsync("beginElementAt", offset);
    }

    public async ValueTask EndElementAsync()
    {
        await JSReference.InvokeVoidAsync("endElement");
    }

    public async ValueTask EndElementAtAsync(float offset)
    {
        await JSReference.InvokeVoidAsync("endElementAt", offset);
    }

    public new async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        if (svgAnimationHelperTask.IsValueCreated)
        {
            IJSObjectReference module = await svgAnimationHelperTask.Value;
            await module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
