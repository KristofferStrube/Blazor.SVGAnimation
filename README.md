[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE.md)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.SVGAnimation)](https://github.com/KristofferStrube/Blazor.SVGAnimation/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.SVGAnimation)](https://github.com/KristofferStrube/Blazor.SVGAnimation/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.SVGAnimation)](https://github.com/KristofferStrube/Blazor.SVGAnimation/stargazers)

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.SVGAnimation?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.SVGAnimation/)

# Introduction
A Blazor WASM wrapper for the SVG Animation browser API.

With the wrapper we can `begin`, `end`, subscribe to events and see the status of SVG Animations from Blazor WASM without writing any JS.

The specs for the API can be found at https://svgwg.org/specs/animations/#IDL

## Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.SVGAnimation/

On each page you can find the corresponding code for the example in the top right corner.

# Getting Started
## Prerequisites
You need to install .NET 6.0 or newer to use the library.

[Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

## Installation
You can install the package via Nuget with the Package Manager in your IDE or alternatively using the command line:
```bash
dotnet add package KristofferStrube.Blazor.SVGAnimation
```

# Usage
The package can be used in Blazor WebAssembly projects.
## Import
You also need to reference the package in order to use it in your pages. This can be done in `_Import.razor` by adding the following.
```razor
@using KristofferStrube.Blazor.SVGAnimation
```
## Add to service collection
An easy way to make the service available in all your pages is by registering it in the `IServiceCollection` so that it can be dependency injected in the pages that need it. This is done in `Program.cs` by adding the following before you build the host and run it.
```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Other services are added.

builder.Services.AddSVGAnimationService();

await builder.Build().RunAsync();
```
## Inject in page
Then the service can be injected in a page like so:
```razor
@inject ISVGAnimationService SVGAnimationService;
```
Then you can use `SVGAnimationService` to wrap an `animate`, `set`, `animateMotion`, `mpath`, `animateTransform`, or `discard` element like so:
```razor
<svg width="100px" height="100px">
    <rect @onclick="OnClick"
          x="20"
          y="10"
          width="60"
          height="80"
          fill="yellow">
        <animate @ref="elementReference"
                    attributeName="fill"
                    values="yellow;green"
                    begin="indefinite"
                    dur="3s"
                    fill="freeze" />
    </rect>
</svg>

@code {
    protected ElementReference elementReference { get; set; }
    protected SVGAnimationElement animation;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            animation = await SVGAnimationService.GreateSVGAnimationElement(elementReference);
        }
    }

    private async void OnClick()
    {
        await animation.BeginElementAsync();
    }
}
```

# Issues
Feel free to open issues on the repository if you find any errors with the package or have wishes for features.

# Related articles
This repository was build with inspiration and help from the following series of articles:

- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
