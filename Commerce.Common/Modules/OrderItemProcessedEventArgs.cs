using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Common.Contracts;
using Commerce.Common.DataModels;
using Commerce.Common.Entities;
using System.ComponentModel;

namespace Commerce.Common
{
    public class OrderItemProcessedEventArgs : CancelEventArgs
    {
        public OrderItemProcessedEventArgs(Customer customer, OrderLineItemData orderLineItemData)
        {
            Customer = customer;
            OrderLineItemData = orderLineItemData;
        }

        public Customer Customer { get; set; }
        public OrderLineItemData OrderLineItemData { get; set; }
        public string MessageText { get; set; }
    }
}
