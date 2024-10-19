using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MessageQueue.RabbitMQ.Configurations
{
    public class RabbitMqConnectionFactory
    {
        ConnectionFactory ConnectionFactory;
        IConnection Connection;
        public IModel Channel { get; private set; }

        public RabbitMqConnectionFactory()
        {
            Connect();
        }

        public void Connect()
        {
            try
            {
                ConnectToRabbitMqServer();

                CreateChannal();

                DeclareNotificationsExchange();
                DeclareEmailsQueueAndBindWithNotificationsExchange();
                DeclareSmsQueueAndBindWithNotificationsExchange();

                HandleEvents();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error On Connection Factory : " + ex.ToString());
            }
        }




        private void ConnectToRabbitMqServer()
        {
            ConnectionFactory = new ConnectionFactory();

            ConnectionFactory.HostName = @"localhost";
            ConnectionFactory.UserName = @"guest";
            ConnectionFactory.Password = @"guest";
            ConnectionFactory.Port = 5672;
            ConnectionFactory.VirtualHost = @"/";
            ConnectionFactory.RequestedConnectionTimeout = new TimeSpan(0, 0, 30);
            ConnectionFactory.AutomaticRecoveryEnabled = true;
            ConnectionFactory.NetworkRecoveryInterval = TimeSpan.FromSeconds(5);

            Connection = ConnectionFactory.CreateConnection();

            
        }

        private void CreateChannal()
        {
            Channel = Connection.CreateModel();
            Channel.ConfirmSelect();
        }

        private void DeclareNotificationsExchange()
        {
            Channel.ExchangeDeclare("NotificationsExchange", ExchangeType.Direct);
        }

        private void DeclareEmailsQueueAndBindWithNotificationsExchange()
        {
            Channel.QueueDeclare("EmailsQueue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Channel.QueueBind("EmailsQueue",
                              "NotificationsExchange",
                              "Emails");
        }

        private void DeclareSmsQueueAndBindWithNotificationsExchange()
        {
            Channel.QueueDeclare("SmsQueue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Channel.QueueBind("SmsQueue",
                              "NotificationsExchange",
                              "Sms");
        }

        private void HandleEvents()
        {
            Connection.ConnectionBlocked += OnConnectionBlockedEvent;
            Connection.ConnectionShutdown += OnConnectionShutdownEvent;

            Console.WriteLine(
               $"Start SequenceNumber for 'ConfirmSelect' is: {Channel.NextPublishSeqNo}"
           );

            Channel.BasicAcks += (sender, ea) =>
            {
                Console.WriteLine(
                    $"Message with delivery tag '{ea.DeliveryTag}' success, multiple is {ea.Multiple}."
                );
            };

            Channel.BasicNacks += (sender, ea) =>
            {
                Console.WriteLine(
                    $"Message with delivery tag '{ea.DeliveryTag}' failed, multiple is {ea.Multiple}."
                );
            };
        }


        private void OnConnectionBlockedEvent(object? sender, ConnectionBlockedEventArgs e)
        {
            Console.WriteLine($"RabbitMq was disconnected! attempting to reconnect...\n{e.Reason}");
        }

        private void OnConnectionShutdownEvent(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"RabbitMq was disconnected! attempting to reconnect...\n{e.Cause}");
        }

    }
}
