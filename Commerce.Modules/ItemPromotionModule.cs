using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Commerce.Common;

namespace Commerce.Modules
{
    public class ItemPromotionModule : ICommerceModule
    {
        public void Initialize(CommerceEvents events, NameValueCollection config)
        {
            events.OrderItemProcessed += OnOrderItemProcessed;

            string promotionStartDate = config["startDate"];
            if (string.IsNullOrWhiteSpace(promotionStartDate))
                throw new ArgumentNullException("Missing config argument 'startDate'");

            string promotionEndDate = config["endDate"];
            if (string.IsNullOrWhiteSpace(promotionEndDate))
                throw new ArgumentNullException("Missing config argument 'endDate'");

            _PromotionStartDate = Convert.ToDateTime(promotionStartDate);
            _PromotionEndDate = Convert.ToDateTime(promotionEndDate);

            config.Remove("name");
            config.Remove("type");
            config.Remove("startDate");
            config.Remove("endDate");

            if (config.Count > 0)
                throw new ApplicationException("Unknown config arguments detected.");
        }

        DateTime _PromotionStartDate;
        DateTime _PromotionEndDate;

        void OnOrderItemProcessed(OrderItemProcessedEventArgs e)
        {
            DateTime today = DateTime.Now;
            if (today >= _PromotionStartDate && today <= _PromotionEndDate)
            {
                if (e.OrderLineItemData.Sku == 102)
                {
                    // great news, special on the Asus Motherboard - $30 off from $479
                    e.OrderLineItemData.PurchasePrice -= 30.00;
                }
            }
        }
    }
}
