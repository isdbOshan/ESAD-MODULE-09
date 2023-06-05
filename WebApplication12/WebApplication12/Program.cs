using Microsoft.EntityFrameworkCore;
using WebApplication12.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();



app.Run();
