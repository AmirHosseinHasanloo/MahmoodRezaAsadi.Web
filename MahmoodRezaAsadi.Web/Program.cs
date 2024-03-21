using Core.Convertors;
using Core.Services;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#region DB Context

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
#endregion




#region IOC 

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IUserAdminService, UserAdminService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
#endregion

#region Authorization

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

#endregion

var app = builder.Build();


#region Check User Can Download Course Or not

app.Use(async (context, next) =>
{
    var callingUrl = context.Request.Headers["referer"].ToString();

    if (context.Request.Path.Value.ToString().ToLower().StartsWith("/episodes") ||
     context.Request.Path.Value.ToString().ToLower().StartsWith("/freeepisodes") ||
     context.Request.Path.Value.ToString().ToLower().StartsWith("/courseroot"))
    {

        if (callingUrl != "" && (callingUrl.StartsWith("https://localhost:7216") || callingUrl.StartsWith("http://localhost:7216")))
        {
            await next.Invoke();
        }
        else
        {
            context.Response.Redirect("/Login");
        }
    }
    else
    {
        await next.Invoke();
    }
});
#endregion

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

app.MapRazorPages();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
