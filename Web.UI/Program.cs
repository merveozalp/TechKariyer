
using Business.Abstract;
using Business.Concrete;
using Core.DataAccess;
using Core.EntityFrmework;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramewrok.Context;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddDbContext<MyContext>(x =>
//{
//    x.UseSqlServer(builder.Configuration.GetConnectionString("Tech"), option =>
//     {
//         option.MigrationsAssembly(Assembly.GetAssembly(typeof(MyContext)).GetName().Name);
//     });


//});
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<MyContext>();

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
