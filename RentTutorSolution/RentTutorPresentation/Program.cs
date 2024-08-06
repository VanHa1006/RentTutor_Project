using BusinessAccess.Business;
using BusinessAccess.DAO;
using BusinessAccess.Repository;
using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
// Add services to the container.s
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSignalR();
builder.Services.AddRazorPages().AddRazorPagesOptions(options => { options.Conventions.AddPageRoute("/HomePage", ""); });

var configuration = builder.Configuration;

// Register UserDAO with a factory method to inject the connection string
builder.Services.AddSingleton<UserDAO>(provider =>
{
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new UserDAO(connectionString);
});
builder.Services.AddDbContext<RenTurtorToStudentContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
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
app.UseSession();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<TutorHub>("/tutorHub");
});

app.MapRazorPages();

app.Run();
