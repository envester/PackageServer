using Microsoft.EntityFrameworkCore;
using package_management.Data;
using package_management.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"))
);

builder.Services.AddScoped<IPackageRepository, PackageRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

//Endpoints
app.MapGet("packages/get", (IPackageRepository service) =>
{
    return service.GetPackages();
});
app.MapGet("packages/get/{id}", (int id, IPackageRepository service) =>
{
    return service.GetPackage(id);
});
app.MapPost("package/create", (Package package, IPackageRepository service) =>
{
    service.AddPackage(package);
});
app.MapPut("package/update", (Package package, IPackageRepository service) =>
{
    service.UpdatePackage(package);
});
app.MapDelete("packages/delete/{id}", (int id, IPackageRepository service) =>
{
    service.RemovePackage(id);
});

app.Run();
