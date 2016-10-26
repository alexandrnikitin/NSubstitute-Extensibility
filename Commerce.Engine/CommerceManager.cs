using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Common;
using Commerce.Common.Contracts;
using Commerce.Common.DataModels;
using Commerce.Common.Pipeline;
using Commerce.Engine.Contracts;
using Pipeline;

namespace Commerce.Engine
{
    public class CommerceManager : ICommerceManager
    {
        public CommerceManager(IStoreRepository storeRepository, IConfigurationFactory configurationFactory)
        {
            _StoreRepository = storeRepository;
            
            // load providers
            _PaymentProcessor = configurationFactory.GetPaymentProcessor();
            _Mailer = configurationFactory.GetMailer();

            _Events = configurationFactory.GetEvents();
        }

        IStoreRepository _StoreRepository;
        IPaymentProcessor _PaymentProcessor;
        IMailer _Mailer;
        CommerceEvents _Events;

        #region ICommerceManager Members

        public void ProcessOrder(OrderData orderData)
        {
            try
            {
                CommerceContext commerceContext = new CommerceContext() 
                {
                    StoreRepository = _StoreRepository,
                    PaymentProcessor = _PaymentProcessor,
                    Mailer = _Mailer,
                    OrderData = orderData,
                    Events = _Events
                };

                IBackbone<CommercePipelineEvents, CommerceContext> backbone =
                    new Backbone<CommercePipelineEvents, CommerceContext>("commerce");

                backbone.Execute(backbone.Initialize(), commerceContext);
            }
            catch (Exception)
            {
                _Mailer.SendRejectionEmail(orderData);
                throw;
            }
        }

        #endregion
    }
}
