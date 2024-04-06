using Domain.Interfaces.MessageQueue.Publishers;
using Domain.Interfaces.Messages;
using MessageQueue.RabbitMQ.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MessageQueue.RabbitMQ.Publishers
{
    public class EmailsQueue : IEmailsQueue
    {
        private readonly RabbitMqConnectionFactory RabbitMqConnectionFactory;

        public EmailsQueue(RabbitMqConnectionFactory rabbitMqConnectionFactory)
        {
            RabbitMqConnectionFactory = rabbitMqConnectionFactory;
        }

        public void Publish(string clientEmail , string orderNumber)
        {

            var email = new EmailMessage
            {
                ClientEmail = clientEmail,
                OrderNumber = orderNumber
            };

            var messageJson = JsonConvert.SerializeObject(email);

            var body = Encoding.UTF8.GetBytes(messageJson);

            var Channel = RabbitMqConnectionFactory
                                       .Channel;

            var currentSequenceNumber = RabbitMqConnectionFactory
                                       .Channel
                                       .NextPublishSeqNo;

            Channel.BasicPublish(exchange: "NotificationsExchange",
                                routingKey: "Emails",
                                mandatory: true,
                                basicProperties: null,
                                body: body);


            Channel.WaitForConfirmsOrDie();


        }


    }
}
