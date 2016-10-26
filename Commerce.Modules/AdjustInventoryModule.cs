using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Commerce.Common;
using Commerce.Common.DataModels;
using Commerce.Common.Entities;
using Commerce.Common.Pipeline;
using Pipeline;

namespace Commerce.Modules
{
    public class AdjustInventoryModule : PipelineModule<CommercePipelineEvents>
    {
        public override void Initialize(CommercePipelineEvents events, NameValueCollection parameters)
        {
            events.AdjustInventory += OnAdjustInventory;
        }

        void OnAdjustInventory(CommerceContext context)
        {
            foreach (OrderLineItemData lineItem in context.OrderData.LineItems)
            {
                // process added order line item modules
                if (context.Events.OrderItemProcessed != null)
                {
                    OrderItemProcessedEventArgs args = new OrderItemProcessedEventArgs(context.Customer, lineItem);
                    context.Events.OrderItemProcessed(args);
                    if (args.Cancel)
                        throw new ApplicationException(args.MessageText);
                }

                Product product = context.StoreRepository.Products.Where(item => item.Sku == lineItem.Sku).FirstOrDefault();
                if (product == null)
                    throw new ApplicationException(string.Format("Sku {0} not found in store inventory.", lineItem.Sku));

                Inventory inventoryOnHand = context.StoreRepository.ProductInventory.Where(item => item.Sku == lineItem.Sku).FirstOrDefault();
                if (inventoryOnHand == null)
                    throw new ApplicationException(string.Format("Error attempting to determine on-hand inventory quantity for product {0}.", lineItem.Sku));

                if (inventoryOnHand.QuantityInStock < lineItem.Quantity)
                    throw new ApplicationException(string.Format("Not enough quantity on-hand to satisfy product {0} purchase of {1} units.", lineItem.Sku, lineItem.Quantity));

                inventoryOnHand.QuantityInStock -= lineItem.Quantity;
                Console.WriteLine("Inventory for product {0} reduced by {1} units.", lineItem.Sku, lineItem.Quantity);
            }
        }
    }
}
