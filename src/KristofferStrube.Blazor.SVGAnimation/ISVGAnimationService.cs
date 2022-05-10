using Microsoft.AspNetCore.Components;

namespace KristofferStrube.Blazor.SVGAnimation
{
    public interface ISVGAnimationService
    {
        ValueTask DisposeAsync();
        ValueTask<SVGAnimationElement> GreateSVGAnimationElement(ElementReference elementReference);
    }
}