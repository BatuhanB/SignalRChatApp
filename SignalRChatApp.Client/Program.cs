var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.ConfigureApplicationCookie(_ =>
{
    _.LoginPath = new PathString("/User/Login");
    _.LogoutPath = new PathString("/User/Logout");
    _.Cookie = new CookieBuilder
    {
        Name = "AspNetCoreIdentityCookie", 
        HttpOnly = false, 
        Expiration = TimeSpan.FromMinutes(2), 
        SameSite = SameSiteMode.Lax, 
        SecurePolicy = CookieSecurePolicy.Always 
    };
    _.SlidingExpiration = true; 
    _.ExpireTimeSpan = TimeSpan.FromMinutes(2);
    _.AccessDeniedPath = new PathString("/Shared/Error");
});

builder.Services.AddHttpClient("API", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7270/");
});


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
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
