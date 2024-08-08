using BusinessAccess.DAO;
using BusinessAccess.Repository;
using BusinessAccess.Services;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentTutorPresentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true; // Ensure the cookie is accessible only to the server.
    options.Cookie.IsEssential = true; // Make the session cookie essential.
});
builder.Services.AddHttpContextAccessor();
// Add services to the container.s
builder.Services.AddRazorPages();
builder.Services.AddScoped<LoginRepository>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<ITutorServices, TutorServices>();
builder.Services.AddScoped<IUserApprovalLogService, UserApprovalLogService>();
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

app.MapHub<SignalrServer>("/signalrServer");

app.MapRazorPages();

app.Run();
