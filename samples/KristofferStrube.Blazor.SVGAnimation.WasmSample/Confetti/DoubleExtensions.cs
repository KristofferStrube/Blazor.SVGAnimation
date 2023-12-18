﻿using System.Globalization;

namespace KristofferStrube.Blazor.SVGAnimation.WasmSample.Confetti;

public static class DoubleExtensions
{
    public static string AsString(this double d) => d.ToString(CultureInfo.InvariantCulture);
}