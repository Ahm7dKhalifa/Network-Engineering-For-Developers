using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.CreateNewOrder
{
    public class NewOrderRequest
    {
        public string ClientEmail { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
    }
}
