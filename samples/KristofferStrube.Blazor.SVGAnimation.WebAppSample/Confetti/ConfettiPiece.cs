namespace KristofferStrube.Blazor.SVGAnimation.WebAppSample.Confetti;

public record struct ConfettiPiece(string Color, int Milliseconds, (double X, double Y)[] Positions);