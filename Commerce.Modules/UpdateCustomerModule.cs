using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Commerce.Common.DataModels;
using Commerce.Common.Entities;
using Commerce.Common.Pipeline;
using Pipeline;

namespace Commerce.Modules
{
    public class UpdateCustomerModule : PipelineModule<CommercePipelineEvents>
    {
        public override void Initialize(CommercePipelineEvents events, NameValueCollection parameters)
        {
            events.UpdateCustomer += OnUpdateCustomer;
        }
        void OnUpdateCustomer(CommerceContext context)
        {
            foreach (OrderLineItemData lineItem in context.OrderData.LineItems)
            {
                for (int i = 0; i < lineItem.Quantity; i++)
                    context.Customer.Purchases.Add(new PurchasedItem() { Sku = lineItem.Sku, PurchasePrice = lineItem.PurchasePrice, PurchasedOn = DateTime.Now });
                Console.WriteLine("Added {0} unit(s) or product {1} to customer's purchase history.", lineItem.Quantity, lineItem.Sku);
            }
        }
    }
}
