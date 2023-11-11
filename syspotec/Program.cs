using Syspotec.Core.Interfaces;
using Syspotec.Core.Services;
using Syspotec.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyeccion de dependencias - Repositorios SQL SERVER
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAssignedRepository, AssignedRepository>();
builder.Services.AddTransient<IStateRepository, StateRepository>();
// Inyeccion de dependencias - Logica de negocios
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStateService, StateService>();
builder.Services.AddTransient<IAssignedService, AssignedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
