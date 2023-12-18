using Microsoft.AspNetCore.Components;

namespace KristofferStrube.Blazor.SVGAnimation.WasmSample.Confetti;

public class ConfettiOptions : EventArgs
{
    public int Pieces { get; set; } = 300;

    public int Miliseconds { get; set; } = 1000;

    public int VariationInMilliseconds { get; set; } = 200;

    public string[] Colors { get; set; } = ["#F4F", "#44F", "#4F4", "#F44", "#9F0"];

    public ConfettiOriginMode Mode { get; set; } = ConfettiOriginMode.FromBottomCenter;

    public ElementReference? Origin { get; set; }
}
