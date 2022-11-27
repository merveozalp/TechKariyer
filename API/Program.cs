using Business.Abstract;
using Business.Concrete;
using Business.Map;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramewrok.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("Tech"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(MyContext)).GetName().Name);
    });


});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MyContext>();
builder.Services.AddScoped<IProductDal,EfProductDal>();
builder.Services.AddScoped(typeof(IProductService),typeof(ProductManager));
builder.Services.AddScoped<ICategoryDal,EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddAutoMapper(typeof(MapProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
