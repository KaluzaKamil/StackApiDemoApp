using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "rabbitmq",
    Port = AmqpTcpEndpoint.UseDefaultPort,
    UserName = ConnectionFactory.DefaultUser,
    Password = ConnectionFactory.DefaultPass
};

factory.ClientProvidedName = "Rabbit Receiver";
try
{
    var connection = factory.CreateConnection();
    using
    var channel = connection.CreateModel();

    var queueName = "RabbitQueue";

    channel.QueueDeclare(queueName, false, false, false, null);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += async (sender, args) =>
    {
        var body = args.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine($"Message received: {message}");
    };

    channel.BasicConsume(queueName, true, consumer);

    Thread.Sleep(Timeout.Infinite);
}
catch(Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
}
