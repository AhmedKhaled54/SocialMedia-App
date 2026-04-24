using Core.ConfiqurationDependancies;
using Data.Identity;
using Infrastructure.ConfiqDependencies;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity;
using Services.ConfiqDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependencies services
builder.Services
    .AddInfrastrsucturedependencies()
    .AddInfrstructureRegitserServices(builder.Configuration)
    .AddRegisterServices()
    .AddCoreConfiqRegister();
var app = builder.Build();
//seed user & role  data 
using (var scope = app.Services.CreateScope())
{
    var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await UserSeeder.SeedUser(usermanager);
    await RoleSeeder.SeedRole(rolemanager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
