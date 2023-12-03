using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.Tracing;

namespace KristofferStrube.Blazor.SVGAnimation;

public class SVGAnimationElementInProcess : SVGAnimationElement, IEventTargetInProcess
{
    protected readonly IJSInProcessObjectReference domInProcessHelper;
    protected readonly IJSInProcessObjectReference inProcessHelper;

    public new IJSInProcessObjectReference JSReference { get; set; }

    public static async Task<SVGAnimationElementInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference domInProcessHelper = await DOM.Extensions.IJSRuntimeExtensions.GetInProcessHelperAsync(jSRuntime);
        IJSInProcessObjectReference inProcessHelper = await Extensions.IJSRuntimeExtensions.GetInProcessHelperAsync(jSRuntime);
        return new SVGAnimationElementInProcess(jSRuntime, domInProcessHelper, inProcessHelper, jSReference);
    }

    public new static async Task<SVGAnimationElementInProcess> CreateAsync(IJSRuntime jSRuntime, ElementReference elementReference)
    {
        IJSInProcessObjectReference domInProcessHelper = await DOM.Extensions.IJSRuntimeExtensions.GetInProcessHelperAsync(jSRuntime);
        IJSInProcessObjectReference inProcessHelper = await Extensions.IJSRuntimeExtensions.GetInProcessHelperAsync(jSRuntime);
        var jSInstance = await inProcessHelper.InvokeAsync<IJSInProcessObjectReference>("getJSReference", elementReference);
        return new SVGAnimationElementInProcess(jSRuntime, domInProcessHelper, inProcessHelper, jSInstance);
    }

    protected SVGAnimationElementInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference domInProcessHelper, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.domInProcessHelper = domInProcessHelper;
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    public new IJSObjectReference TargetElement => inProcessHelper.Invoke<IJSObjectReference>("getAttribute", JSReference, "targetElement");

    /// <inheritdoc/>
    public void AddEventListener<TInProcessEvent, TEvent>(string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.AddEventListener(domInProcessHelper, type, callback, options);
    }

    /// <inheritdoc/>
    public void AddEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.AddEventListener(domInProcessHelper, callback, options);
    }

    /// <inheritdoc/>
    public void RemoveEventListener<TInProcessEvent, TEvent>(string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.RemoveEventListener(domInProcessHelper, type, callback, options);
    }

    /// <inheritdoc/>
    public void RemoveEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.RemoveEventListener(domInProcessHelper, callback, options);
    }

    public bool DispatchEvent(Event eventInstance)
    {
        return IEventTargetInProcessExtensions.DispatchEvent(this, eventInstance);
    }

    public async Task<EventListenerInProcess<EventInProcess, Event>> AddOnBeginEventListenerAsync(Action<EventInProcess> callback, AddEventListenerOptions? options = null)
    {
        EventListenerInProcess<EventInProcess, Event> eventListener = await EventListenerInProcess<EventInProcess, Event>.CreateAsync(JSRuntime, callback);
        AddEventListener("beginEvent", eventListener, options);
        return eventListener;
    }

    public void RemoveOnBeginEventListener(EventListenerInProcess<EventInProcess, Event> callback, EventListenerOptions? options = null)
    {
        RemoveEventListener("beginEvent", callback, options);
    }

    public async Task<EventListenerInProcess<EventInProcess, Event>> AddOnEndEventListenerAsync(Action<EventInProcess> callback, AddEventListenerOptions? options = null)
    {
        EventListenerInProcess<EventInProcess, Event> eventListener = await EventListenerInProcess<EventInProcess, Event>.CreateAsync(JSRuntime, callback);
        AddEventListener("endEvent", eventListener, options);
        return eventListener;
    }

    public void RemoveOnEndEventListener(EventListenerInProcess<EventInProcess, Event> callback, EventListenerOptions? options = null)
    {
        RemoveEventListener("endEvent", callback, options);
    }

    public async Task<EventListenerInProcess<EventInProcess, Event>> AddOnRepeatEventListenerAsync(Action<EventInProcess> callback, AddEventListenerOptions? options = null)
    {
        EventListenerInProcess<EventInProcess, Event> eventListener = await EventListenerInProcess<EventInProcess, Event>.CreateAsync(JSRuntime, callback);
        AddEventListener("repeatEvent", eventListener, options);
        return eventListener;
    }

    public void RemoveOnRepeatEventListener(EventListenerInProcess<EventInProcess, Event> callback, EventListenerOptions? options = null)
    {
        RemoveEventListener("repeatEvent", callback, options);
    }
    public float GetStartTime()
    {
        return inProcessHelper.Invoke<float>("getStartTime");
    }

    public float GetCurrentTime()
    {
        return inProcessHelper.Invoke<float>("getCurrentTime");
    }

    public float GetSimpleDuration()
    {
        return inProcessHelper.Invoke<float>("getSimpleDuration");
    }

    public void BeginElement()
    {
        inProcessHelper.InvokeVoid("beginElement");
    }

    public void BeginElementAt(float offset)
    {
        inProcessHelper.InvokeVoid("beginElementAt", offset);
    }

    public void EndElement()
    {
        inProcessHelper.InvokeVoid("endElement");
    }

    public void EndElementAt(float offset)
    {
        inProcessHelper.InvokeVoid("endElementAt", offset);
    }

    public new async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await domInProcessHelper.DisposeAsync();
        await inProcessHelper.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
