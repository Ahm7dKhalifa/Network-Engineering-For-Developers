using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.CreateNewOrder
{
    public interface ICreateNewOrderUseCase
    {
        Task<NewOrderResponse> Execute(NewOrderRequest orderToCreate);
    }
}
