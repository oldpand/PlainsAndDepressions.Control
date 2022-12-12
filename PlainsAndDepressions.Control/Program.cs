using MediatR;
using PlainsAndDepressions.Control.Services.Commands;
using PlainsAndDepressions.Control.Services.Handlers;
using PlainsAndDepressions.Control.Services.Results;
using PlainsAndDepressions.Control.Services.Services.Background;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMediatR(typeof(PutDepressionsCommandHandler).Assembly)
    .AddScoped<IRequestHandler<PutPuckCommand, ControlledPackResult>, PutDepressionsCommandHandler>()
    ;

builder.Services
    .AddHostedService<RabbitMqListener>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
