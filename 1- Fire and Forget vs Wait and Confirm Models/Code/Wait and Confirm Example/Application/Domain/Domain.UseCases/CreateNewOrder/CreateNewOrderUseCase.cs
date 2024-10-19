using Domain.Interfaces.MessageQueue.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.CreateNewOrder
{
    public class CreateNewOrderUseCase : ICreateNewOrderUseCase
    {
        private readonly IEmailsQueue EmailsQueue;
        public CreateNewOrderUseCase(IEmailsQueue emailsQueue)
        {
            EmailsQueue = emailsQueue;
        }
        public async Task<NewOrderResponse> Execute(NewOrderRequest orderToCreate)
        {
            CreateNewOrder(orderToCreate);
            
            await SaveNewOrderOnDatabase();

            EmailsQueue.Publish(orderToCreate.ClientEmail,
                                orderToCreate.OrderNumber);

            return new NewOrderResponse();
        }

        public void CreateNewOrder(NewOrderRequest model)
        {
            //write here your business logic to create order
            // Order newOrder = Order.Create(...)

        }

        public async Task SaveNewOrderOnDatabase()
        {
            //write here your code to save order on database
            //await OrdersRepository.AddNew(newOrder);
        }
    }
}
