namespace KristofferStrube.Blazor.SVGAnimation.WasmSample.Confetti;

public record struct ConfettiPiece(string Color, int Milliseconds, (double X, double Y)[] Positions);