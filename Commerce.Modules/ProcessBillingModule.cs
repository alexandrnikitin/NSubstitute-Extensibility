using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Commerce.Common.DataModels;
using Commerce.Common.Pipeline;
using Pipeline;

namespace Commerce.Modules
{
    public class ProcessBillingModule : PipelineModule<CommercePipelineEvents>
    {
        public override void Initialize(CommercePipelineEvents events, NameValueCollection parameters)
        {
            events.ProcessBilling += OnProcessBilling;
        }
        void OnProcessBilling(CommerceContext context)
        {
            double amount = 0;
            foreach (OrderLineItemData lineItem in context.OrderData.LineItems)
                amount += (lineItem.PurchasePrice * lineItem.Quantity);
            bool paymentSuccess = context.PaymentProcessor.ProcessCreditCard(context.Customer.Name, context.OrderData.CreditCard, context.OrderData.ExpirationDate, amount);
            if (!paymentSuccess)
                throw new ApplicationException(string.Format("Credit card {0} could not be processed.", context.OrderData.CreditCard));
        }
    }
}
