using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.SVGAnimation.Extensions;

internal static class IJSRuntimeExtensions
{
    private const string scriptUrl = "./_content/KristofferStrube.Blazor.SVGAnimation/KristofferStrube.Blazor.SVGAnimation.js";

    internal static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSObjectReference>(
            "import", scriptUrl);
    }
    internal static async Task<IJSInProcessObjectReference> GetInProcessHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", scriptUrl);
    }
}
