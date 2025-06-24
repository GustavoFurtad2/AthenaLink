using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string webAPIAdress = "https://localhost:7025";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(webAPIAdress) });

await builder.Build().RunAsync();
