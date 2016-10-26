using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Common.DataModels;
using Pipeline;
using Commerce.Common.Contracts;
using Commerce.Common.Entities;

namespace Commerce.Common.Pipeline
{
    public class CommerceContext : PipelineContext
    {
        public IStoreRepository StoreRepository { get; set; }
        public IPaymentProcessor PaymentProcessor { get; set; }
        public IMailer Mailer { get; set; }
        public OrderData OrderData { get; set; }
        public Customer Customer { get; set; }
        public CommerceEvents Events { get; set; }
    }
}
