using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Messages
{
    public class SmsMessage
    {
        public string ClientPhoneNumber { get; set; } = "";
        public long OrderNumber { get; set; }
        public DateTime OrderCreationTime { get; set; }
    }
}
