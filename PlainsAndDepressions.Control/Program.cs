using MediatR;
using PlainsAndDepressions.Control.Services.Commands;
using PlainsAndDepressions.Control.Services.Handlers;
using PlainsAndDepressions.Control.Services.Results;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMediatR(Assembly.GetExecutingAssembly())
    .AddScoped<IRequestHandler<PutPuckCommand, ControlledPackResult>, PutDepressionsCommandHandler>()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
