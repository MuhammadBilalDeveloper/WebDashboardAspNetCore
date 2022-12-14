using DevExpress.DataAccess.Sql;
using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

IFileProvider? fileProvider = builder.Environment.ContentRootFileProvider;
IConfiguration? configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDevExpressControls();
builder.Services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) => {
    DashboardConfigurator configurator = new DashboardConfigurator();

    configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(configuration));
    configurator.SetDashboardStorage(new CustomDashboardFileStorage(fileProvider.GetFileInfo("Data/Dashboards").PhysicalPath));

    return configurator;
});

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

app.UseDevExpressControls();
EndpointRouteBuilderExtension.MapDashboardRoute(app, "api/dashboard", "DefaultDashboard");

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();