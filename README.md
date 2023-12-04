[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.SVGAnimation)](https://github.com/KristofferStrube/Blazor.SVGAnimation/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.SVGAnimation)](https://github.com/KristofferStrube/Blazor.SVGAnimation/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.SVGAnimation)](https://github.com/KristofferStrube/Blazor.SVGAnimation/stargazers)

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.SVGAnimation?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.SVGAnimation/)

# Introduction
A Blazor WASM wrapper for the SVG Animation browser API.

With the wrapper, we can listen to the `begin`, `end`, and `repeat` events, see the status of SVG Animations, and start and stop animations at specific times from Blazor.

The specs for the API can be found at https://svgwg.org/specs/animations/#IDL

## Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.SVGAnimation/

On each page, you can find the corresponding code for the example in the top right corner.

# Getting Started
## Prerequisites
You need to install .NET 7.0 or newer to use the library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

## Installation
You can install the package via Nuget with the Package Manager in your IDE or using the command line:
```bash
dotnet add package KristofferStrube.Blazor.SVGAnimation
```

# Usage
The package can be used in all Blazor projects.
## Import
You also need to reference the package in order to use it in your pages. This can be done in `_Import.razor` by adding the following.
```razor
@using KristofferStrube.Blazor.SVGAnimation
```

## Creating wrapper instances
The primary part of this library is a wrapper class for `SVGAnimationElement`s which can be instantiated from your code using the static `CreateAsync` methods on the wrapper class.
```razor
@inject IJSRuntime JSRuntime

<svg width="100px" height="100px">
    <rect @onclick="SquareClicked"
          width="100"
          height="100"
          fill="red">
        <animate @ref="colorAnimationElementReference"
                 attributeName="fill"
                 values="red;yellow;red"
                 begin="indefinite"
                 dur="3s" />
    </rect>
</svg>

@code {
    private ElementReference colorAnimationElementReference = default!;

    private async void SquareClicked()
    {
        SVGAnimationElement colorAnimationElement = await SVGAnimationElement.CreateAsync(JSRuntime, colorAnimationElementReference);
        await colorAnimationElement.BeginElementAsync();
    }
}
```
You can also use a `SVGAnimationElement` to listen for the `beginEvent`, `endEvent`, and `repeatEvent` events. The following is an example of listening for when a repeat event happens.
```razor
@using KristofferStrube.Blazor.DOM
@implements IAsyncDisposable
@inject IJSRuntime JSRuntime

<b>Number of repeats: </b> @repeats
<br />
<svg width="100px" height="100px">
    <rect @onclick="SquareClicked"
          width="100"
          height="100"
          fill="red">
        <animate @ref="colorAnimationElementReference"
                 attributeName="fill"
                 values="red;yellow;red"
                 begin="indefinite"
                 repeatCount="indefinite"
                 dur="1s" />
    </rect>
</svg>

@code {
    private ElementReference colorAnimationElementReference = default!;
    private SVGAnimationElement colorAnimationElement = default!;
    private EventListener<Event> repeatListener = default!;
    private int repeats = 0;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        colorAnimationElement = await SVGAnimationElement.CreateAsync(JSRuntime, colorAnimationElementReference);
        repeatListener = await colorAnimationElement.AddOnRepeatEventListenerAsync(_ =>
        {
            repeats++;
            StateHasChanged();
            return Task.CompletedTask;
        });
    }

    private async void SquareClicked()
    {
        await colorAnimationElement.BeginElementAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await colorAnimationElement.RemoveOnRepeatEventListenerAsync(repeatListener);
    }
}
```

# Issues
Feel free to open issues on the repository if you find any errors with the package or have wishes for features.

# Related articles
This repository was built with inspiration and help from the following series of articles:

- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
