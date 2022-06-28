using ExperianTest.Backgroundservices;
using ExperianTest.infra;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ExperianTest.RabbitMqConfiguration
{
    public interface IConsumerService
    {
        Task ReadMessgaes();
    }

    public class ConsumerService : IConsumerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        public ConsumerService(IRabbitMqService rabbitMqService)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare("todoQueue", ExchangeType.Fanout, durable: true, autoDelete: false);
            _model.QueueBind(_queueName, "todoQueue", string.Empty);
        }
        const string _queueName = "todoQueue";
        public async Task ReadMessgaes()
        {
      var consumer = new AsyncEventingBasicConsumer(_model);
           //  var consumer = new EventingBasicConsumer(_model);

            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var text = System.Text.Encoding.UTF8.GetString(body);
                apicall o = new apicall();
                o.GetCompanySummary(text);
                Console.WriteLine(text);
                //await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);
            };
            

            _model.BasicConsume(_queueName, false, consumer);
             await Task.CompletedTask;
        }

        

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
