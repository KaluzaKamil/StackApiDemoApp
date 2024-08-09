using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackApiDemo.Handlers;
using System.Text;
using System.Threading.Channels;

namespace StackApiDemo.RabbitMQ
{
    public class RabbitMQSender : IRabbitMQSender
    {
        public void RefreshDatabase()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = AmqpTcpEndpoint.UseDefaultPort,
                UserName = ConnectionFactory.DefaultUser,
                Password = ConnectionFactory.DefaultPass
            };
            factory.ClientProvidedName = "Rabbit Sender";

            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();

            var exchangeName = "RabbitExchange";
            var routingKey = "RabbitRoutingKey";
            var queueName = "RabbitQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var body = Encoding.UTF8.GetBytes("Refresh");

            channel.BasicPublish(exchangeName, routingKey, null, body);
        }
    }
}
