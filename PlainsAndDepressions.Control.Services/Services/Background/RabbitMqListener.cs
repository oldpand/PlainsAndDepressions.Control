using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlainsAndDepressions.Control.Contracts.Requests;
using PlainsAndDepressions.Control.Services.Commands;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PlainsAndDepressions.Control.Services.Services.Background
{
    public class RabbitMqListener : BackgroundService
    {
        private const string _dipressionPacks = "DipressionPacks";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScope _scope;
        private readonly IMediator _mediator;

        public RabbitMqListener(IServiceScopeFactory serviceScopeFactory)
        {
            _scope = serviceScopeFactory.CreateScope();
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();

            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _dipressionPacks, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, arg) =>
            {
                var message = Encoding.UTF8.GetString(arg.Body.ToArray());

                var pack = JsonSerializer.Deserialize<PutDepressionsRequest>(message);

                if (pack is not null)
                {
                    await _mediator.Send(new PutPuckCommand(pack.PackId, pack.Pack));
                }

                _channel.BasicAck(arg.DeliveryTag, false);
            };

            _channel.BasicConsume(_dipressionPacks, false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            _scope.Dispose();
            base.Dispose();
        }
    }
}
