using Microsoft.AspNetCore.Components.Web;

namespace KristofferStrube.Blazor.SVGAnimation.WebAppSample;
public static class CustomRenderModes
{
    public static readonly InteractiveServerRenderMode InteractiveServerRenderModeNoPreRender
        = new InteractiveServerRenderMode(prerender: false);
}