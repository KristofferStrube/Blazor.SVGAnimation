using Microsoft.AspNetCore.Components;

namespace KristofferStrube.Blazor.SVGAnimation
{
    public interface ISVGAnimationService
    {
        ValueTask DisposeAsync();

        [Obsolete("This doesn't follow the pattern for creation of wrapper object that we wish to continue with so it will be removed in the next major release. Use one of the static SVGAnimationElement.CreateAsync methods instead.")]
        ValueTask<SVGAnimationElement> GreateSVGAnimationElement(ElementReference elementReference);
    }
}