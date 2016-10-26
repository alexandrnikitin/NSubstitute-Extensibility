using System;
using System.Collections.Generic;
using System.Linq;
using Pipeline;

namespace Commerce.Common.Pipeline
{
    public class CommercePipelineEvents : PipelineEvents
    {
        [PipelineEvent(Order = 0, TransactionScopeOption = TransactionScopeOption.Required)]
        public PipelineContext<CommerceContext> ValidateCustomer { get; set; }

        [PipelineEvent(Order = 1, TransactionScopeOption = TransactionScopeOption.Required)]
        public PipelineContext<CommerceContext> AdjustInventory { get; set; }

        [PipelineEvent(Order = 2, TransactionScopeOption = TransactionScopeOption.Required)]
        public PipelineContext<CommerceContext> UpdateCustomer { get; set; }

        [PipelineEvent(Order = 3, TransactionScopeOption = TransactionScopeOption.Required)]
        public PipelineContext<CommerceContext> ProcessBilling { get; set; }

        [PipelineEvent(Order = 4, TransactionScopeOption = TransactionScopeOption.Required)]
        public PipelineContext<CommerceContext> SendNotification { get; set; }
    }
}
