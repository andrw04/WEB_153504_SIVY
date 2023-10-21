using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.Models;
using WEB_153504_SIVY.Services.CarBodyService;
using WEB_153504_SIVY.Services.CarModelService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var UriData = new UriData()
{
    ApiUri = builder.Configuration.GetSection("UriData").GetSection("ApiUri").Value,
    IsUri = builder.Configuration.GetSection("UriData").GetSection("IsUri").Value,
};

builder.Services.AddHttpClient<ICarModelService, ApiCarModelService>(opt => opt.BaseAddress = new Uri(UriData.ApiUri));
builder.Services.AddHttpClient<ICarBodyTypeService, ApiCarBodyTypeService>(opt => opt.BaseAddress = new Uri(UriData.ApiUri));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "cookie";
    opt.DefaultChallengeScheme = "oidc";
})
    .AddCookie("cookie")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
        options.ClientId = builder.Configuration["InteractiveServiceSettings:ClientId"];
        options.ClientSecret = builder.Configuration["InteractiveServiceSettings:ClientSecret"];

        options.GetClaimsFromUserInfoEndpoint = true;

        options.ResponseType = "code";
        options.ResponseMode = "query";
        options.SaveTokens = true;
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages().RequireAuthorization();

app.Run();