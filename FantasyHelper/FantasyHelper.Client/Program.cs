//using FantasyHelper.API;
using FantasyHelper.Client.Data;
//using FantasyHelper.Data;
using FantasyHelper.Shared;
using FantasyHelper.Shared.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<FPLOptions>(builder.Configuration.GetSection(FPLOptions.Key));
builder.Services.Configure<AllsvenskanOptions>(builder.Configuration.GetSection(AllsvenskanOptions.Key));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.Key));
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.Key));

builder.Services
    .AddFantasyMappings();
    //.AddFantasyAPI("v1", "Fantasy API")
    //.AddFantasyData();

builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//app.UseFantasyAPI();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
