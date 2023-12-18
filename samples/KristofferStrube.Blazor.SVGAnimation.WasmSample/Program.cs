using KristofferStrube.Blazor.SVGAnimation.WasmSample;
using KristofferStrube.Blazor.SVGAnimation.WasmSample.Confetti;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddConfettiService();

await builder.Build().RunAsync();
