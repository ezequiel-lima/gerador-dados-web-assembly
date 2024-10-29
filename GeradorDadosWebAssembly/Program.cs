using Bogus;
using GeradorDadosWebAssembly;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

builder.Services.AddRefitClient<IEnderecoService>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://brasilapi.com.br/api"));

builder.Services.AddScoped<Faker>();
builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddScoped<IGeradorCpfService, GeradorCpfService>();
builder.Services.AddScoped<IGeradorCnpjService, GeradorCnpjService>();
builder.Services.AddScoped<IGeradorCepService, GeradorCepService>();
builder.Services.AddScoped<IGeradorEmpresaService, GeradorEmpresaService>();
builder.Services.AddScoped<IGeradorPessoaService, GeradorPessoaService>();
builder.Services.AddScoped<IGeradorNifService, GeradorNifService>();

await builder.Build().RunAsync();
