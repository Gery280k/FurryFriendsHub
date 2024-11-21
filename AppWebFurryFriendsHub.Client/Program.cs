using AppWebFurryFriendsHub.Client;
using AppWebFurryFriendsHub.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5230") });
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, productService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<SearchService>();
builder.Services.AddSingleton<CartService>();


await builder.Build().RunAsync();