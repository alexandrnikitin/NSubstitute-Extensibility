using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Commerce.Common.Entities;
using Commerce.Common.Pipeline;
using Pipeline;

namespace Commerce.Modules
{
    public class ValidateCustomerModule : PipelineModule<CommercePipelineEvents>
    {
        public override void Initialize(CommercePipelineEvents events, NameValueCollection parameters)
        {
            events.ValidateCustomer += OnValidateCustomer;
        }
        void OnValidateCustomer(CommerceContext context)
        {
            Customer customer = context.StoreRepository.GetCustomerByEmail(context.OrderData.CustomerEmail);
            if (customer == null)
                throw new ApplicationException(string.Format("No customer on file with email {0}.", context.OrderData.CustomerEmail));

            context.Customer = customer;
        }
    }
}
