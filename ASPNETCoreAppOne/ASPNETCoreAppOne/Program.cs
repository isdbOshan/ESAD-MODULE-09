using ASPNETCoreAppOne.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarInformationDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddControllers()
    .AddNewtonsoftJson(op =>
    {
        op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
        op.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
    });
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();