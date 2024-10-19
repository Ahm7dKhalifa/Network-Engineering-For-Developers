using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.MessageQueue.Publishers
{
    public interface ISmsQueue
    {
        void Publish(string clientPhoneNumber, long orderNumber);
    }
}
