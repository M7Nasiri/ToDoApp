using App.Domain.AppServices.CategoryAgg;
using App.Domain.AppServices.TaskAgg;
using App.Domain.AppServices.UserAgg;
using App.Domain.Core.CategoryAgg.Contracts.AppServices;
using App.Domain.Core.CategoryAgg.Contracts.Repositories;
using App.Domain.Core.CategoryAgg.Contracts.Services;
using App.Domain.Core.Common.Contracts.Services;
using App.Domain.Core.TaskAgg.Contracts.AppServices;
using App.Domain.Core.TaskAgg.Contracts.Repositories;
using App.Domain.Core.TaskAgg.Contracts.Services;
using App.Domain.Core.UserAgg.Contracts.AppServices;
using App.Domain.Core.UserAgg.Contracts.Repositories;
using App.Domain.Core.UserAgg.Contracts.Services;
using App.Domain.Services.CategoryAgg;
using App.Domain.Services.FileAgg;
using App.Domain.Services.TaskAgg;
using App.Domain.Services.UserAgg;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using App.Infra.Data.Repo.Ef.CategoryAgg;
using App.Infra.Data.Repo.Ef.TaskAgg;
using App.Infra.Data.Repo.Ef.UserAgg;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<ITaskAppService, TaskAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();

builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
});

builder.Services.AddDbContext<AppDbContext>(optionsBuilder => optionsBuilder.UseSqlServer("Server=LAPTOP-6U51JF85\\SQL2022;Database=Maktab135_HW_19;Trusted_Connection=True;TrustServerCertificate=True;"));






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
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
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
