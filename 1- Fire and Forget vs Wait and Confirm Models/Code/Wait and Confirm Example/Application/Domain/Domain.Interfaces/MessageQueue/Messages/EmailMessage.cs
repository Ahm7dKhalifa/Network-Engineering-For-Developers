using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Messages
{
    public class EmailMessage
    {
        public string ClientEmail { get; set; } = "";
        public string OrderNumber { get; set; }
    }
}
