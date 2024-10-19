using Domain.Interfaces.MessageQueue.Publishers;
using Domain.UseCases.CreateNewOrder;
using MessageQueue.RabbitMQ.Publishers;

namespace Api.DependencyInjections.Domain
{
    public static class UseCasesExtensionMethod
    {
        public static void AddUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICreateNewOrderUseCase, CreateNewOrderUseCase>();
        }
    }
}
