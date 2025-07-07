using Application.IRepo;
using Core.IRepo;
using Data.Repo;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repo;
using Infrastructure.Services.Implementation;
using Infrastructure.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Web.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SchoolDbContext>();
builder.Services.AddIdentity<Persona, Role>(opt =>
{
    opt.SignIn.RequireConfirmedPhoneNumber = false;
    opt.SignIn.RequireConfirmedAccount = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredLength = 8;
    opt.User.RequireUniqueEmail = false;

}).AddRoles<Role>()
  .AddEntityFrameworkStores<SchoolDbContext>()
  .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = "/";
});

builder.Services.AddTransient<IProgramRepo, ProgramRepo>();
builder.Services.AddTransient<ISessionRepo, SessionRepo>();
builder.Services.AddTransient<ITermRepo, TermRepo>();
builder.Services.AddTransient<ILocationsSevices, LocationServices>();
builder.Services.AddTransient<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddScoped<ISectionRepo, SectionRepo>();
builder.Services.AddScoped<IClassRepo, ClassRepo>();
builder.Services.AddHostedService<DbSeed>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
