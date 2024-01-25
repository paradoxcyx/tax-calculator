using PayspaceTax.Web.Options;
using PayspaceTax.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IApiService, ApiService>();

//builder.Services.Configure<AppOptions>(options => builder.Configuration.GetSection("App").Bind(options));

var app = builder.Build();
builder.Services.Configure<AppOptions>(options => builder.Configuration.GetSection("App").Bind(options));

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
    pattern: "{controller=TaxCalculator}/{action=Index}/{id?}");

app.Run();