using Microsoft.EntityFrameworkCore;
using package_management.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"))
);

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
app.MapGet("packages/get", async (DatabaseContext databaseContext) =>
{
    List<Package> packages = await databaseContext.Packages
        .Include(p => p.Contact)
        .Include(p => p.InfoPackage)
        .ToListAsync();
  
    return Results.Ok(packages);


});
app.MapGet("packages/get/{id}", async (int id, DatabaseContext databaseContext) =>
{
    var packages = await databaseContext.Packages
        .Include(p => p.Contact)
        .Include(p => p.InfoPackage)
        .FirstOrDefaultAsync(p => p.Id == id);
    if (packages == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(packages);
});
app.MapPost("package/create", async (Package package, DatabaseContext databaseContext) =>
{
    databaseContext.Packages.Add(package);
    await databaseContext.SaveChangesAsync();
    return Results.Ok();
});
app.MapPut("package/update", async (Package package, DatabaseContext databaseContext) =>
{
    var dbPackage = await databaseContext.Packages.FindAsync(package.Id);
    if(dbPackage == null)
    {
        return Results.NotFound();
    }
    dbPackage.PhoneNumber = package.PhoneNumber;
    dbPackage.AddressLine = package.AddressLine;
    dbPackage.PostCode = package.PostCode;
    dbPackage.Contact = package.Contact;
    dbPackage.InfoPackage = package.InfoPackage;
    

    await databaseContext.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("packages/delete/{id}", async (int id, DatabaseContext databaseContext) =>
{
    var dbPackage = await databaseContext.Packages.FindAsync(id);
    if(dbPackage == null)
    {
        return Results.NotFound();
    }

    databaseContext.Packages.Remove(dbPackage);
    await databaseContext.SaveChangesAsync();
    return Results.Ok();

});

app.Run();
