using WEB_153504_SIVY.Models;
using WEB_153504_SIVY.Services.CarBodyService;
using WEB_153504_SIVY.Services.CarModelService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ICarBodyTypeService, MemoryCarBodyTypeService>();
//builder.Services.AddScoped<ICarModelService, MemoryCarModelService>();

var UriData = new UriData()
{
    ApiUri = builder.Configuration.GetSection("UriData").GetSection("ApiUri").Value,
    IsUri = builder.Configuration.GetSection("UriData").GetSection("IsUri").Value,
};

builder.Services.AddHttpClient<ICarModelService, ApiCarModelService>(opt => opt.BaseAddress = new Uri(UriData.ApiUri));
builder.Services.AddHttpClient<ICarBodyTypeService, ApiCarBodyTypeService>(opt => opt.BaseAddress = new Uri(UriData.ApiUri));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


UriData ApiUri = builder.Configuration.GetValue<UriData>("UriData");