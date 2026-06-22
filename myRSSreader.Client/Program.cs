using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using myRSSreader.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
 #if DEBUG
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5152/") });
 #else
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://my-rss-reader-api.onrender.com/") });
#endif
//builder.Services.AddStorageExtensions();

await builder.Build().RunAsync();
