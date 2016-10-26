using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Commerce.Common.Pipeline;
using Pipeline;

namespace Commerce.Modules
{
    public class SendNotificationModule : PipelineModule<CommercePipelineEvents>
    {
        public override void Initialize(CommercePipelineEvents events, NameValueCollection parameters)
        {
            events.SendNotification += OnSendNotification;
        }
        
        void OnSendNotification(CommerceContext context)
        {
            context.Mailer.SendInvoiceEmail(context.OrderData);
        }
    }
}
