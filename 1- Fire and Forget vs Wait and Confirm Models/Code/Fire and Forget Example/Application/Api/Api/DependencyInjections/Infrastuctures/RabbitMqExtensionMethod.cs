using Domain.Interfaces.MessageQueue.Publishers;
using MessageQueue.RabbitMQ.Configurations;
using MessageQueue.RabbitMQ.Publishers;

namespace Api.DependencyInjections.Infrastuctures
{
    public static class RabbitMqExtensionMethod
    {
        public static void AddRabbitMq(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<RabbitMqConnectionFactory>();

            builder.Services.AddScoped<IEmailsQueue, EmailsQueue>();
            builder.Services.AddScoped<ISmsQueue, SmsQueue>();



        }
    }
}
