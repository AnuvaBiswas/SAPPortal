using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using SAPPortal;
using SAPPortal.Components;
using SAPPortal.Components.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<SAPConnection>();
builder.Services.AddScoped<ItemParameterService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<InitializationService>();

var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer("DefaultConnection"), ServiceLifetime.Scoped);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
