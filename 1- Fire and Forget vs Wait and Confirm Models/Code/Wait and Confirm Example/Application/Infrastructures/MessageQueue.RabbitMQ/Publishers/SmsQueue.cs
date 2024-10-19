using Domain.Interfaces.MessageQueue.Publishers;
using Domain.Interfaces.Messages;
using MessageQueue.RabbitMQ.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.RabbitMQ.Publishers
{
    public class SmsQueue : ISmsQueue
    {
        private readonly RabbitMqConnectionFactory RabbitMqConnectionFactory;

        public SmsQueue(RabbitMqConnectionFactory rabbitMqConnectionFactory)
        {
            RabbitMqConnectionFactory = rabbitMqConnectionFactory;
        }

        public void Publish(string clientPhoneNumber, long orderNumber)
        {

            var email = new SmsMessage
            {
                ClientPhoneNumber = clientPhoneNumber,
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
                                routingKey: "Sms",
                                mandatory: true,
                                basicProperties: null,
                                body: body);


            Channel.WaitForConfirmsOrDie();


        }


    }
}
