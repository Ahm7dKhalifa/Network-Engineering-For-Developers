using Domain.UseCases.CreateNewOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateNewOrderUseCase CreateNewOrderUseCase;

        public OrdersController(ICreateNewOrderUseCase createNewOrderUseCase)
        {
            CreateNewOrderUseCase = createNewOrderUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewOrderRequest orderToCreate)
        {
            await CreateNewOrderUseCase.Execute(orderToCreate);

            return Ok();
        }
    }
}
