using System;
using System.Collections.Generic;
using System.Linq;

namespace Commerce.Common
{
    public class CommerceEvents
    {
        public CommerceModuleDelegate<OrderItemProcessedEventArgs> OrderItemProcessed { get; set; }
    }
}
