namespace KristofferStrube.Blazor.SVGAnimation.WebAppSample.Confetti;

public class ConfettiService
{
    public event ActivatedEventHandler Activated;

    public void Activate(ConfettiOptions options)
    {
        Activated?.Invoke(this, options);
    }

    public delegate void ActivatedEventHandler(Object sender, ConfettiOptions e);
}
