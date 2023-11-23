using SignalRChatApp.Persistence;
using SignalRChatApp.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigurePersistence(builder.Configuration);

builder.Services.ConfigureApplicationCookie(opt =>
    {
        opt.LoginPath = "/User/Login";
        opt.LogoutPath = "/User/Logout";
        opt.Cookie = new CookieBuilder
        {
            Name = "SignalRChatAppCookie",
            HttpOnly = false,
            SameSite = SameSiteMode.Lax,
            SecurePolicy = CookieSecurePolicy.Always
        };
        opt.SlidingExpiration = true;
        opt.ExpireTimeSpan = TimeSpan.FromHours(1);
        opt.AccessDeniedPath = "/shared/error";
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
