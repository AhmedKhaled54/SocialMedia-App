using Core.ConfiqurationDependancies;
using Core.MidddleWare;
using Data.Identity;
using Infrastructure.ConfiqDependencies;
using Infrastructure.Hup;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity;
using Services.ConfiqDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
//dependencies services
builder.Services
    .AddInfrastrsucturedependencies()
    .AddInfrstructureRegitserServices(builder.Configuration)
    .AddRegisterServices()
    .AddCoreConfiqRegister().AddServicesDependanciesConfiq(builder.Configuration);

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
    app.UseMiddleware<ErrorHandlingMiddleWare>();
}
app.MapHub<ChatHup>("/ChatHup");
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
