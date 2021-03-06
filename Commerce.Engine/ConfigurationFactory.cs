using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Commerce.Engine.Configuration;
using Commerce.Engine.Contracts;
using Commerce.Common.Contracts;
using Commerce.Common;

namespace Commerce.Engine
{
    public class ConfigurationFactory : IConfigurationFactory
    {
        public ConfigurationFactory()
        {
            CommerceEngineConfigurationSection config = ConfigurationManager.GetSection("commerceEngine") as CommerceEngineConfigurationSection;
            if (config != null)
            {
                IPaymentProcessor paymentProcessor = Activator.CreateInstance(Type.GetType(config.PaymentProcessor.Type)) as IPaymentProcessor;
                IMailer mailer = Activator.CreateInstance(Type.GetType(config.Mailer.Type)) as IMailer;
                mailer.FromAddress = config.Mailer.FromAddress;
                mailer.SmtpServer = config.Mailer.SmtpServer;

                _PaymentProcessor = paymentProcessor;
                _Mailer = mailer;

                // initialize modules
                _Events = new CommerceEvents();
                foreach (ProviderSettings moduleElement in config.Modules)
                {
                    ICommerceModule module = Activator.CreateInstance(Type.GetType(moduleElement.Type)) as ICommerceModule;
                    module.Initialize(_Events, moduleElement.Parameters);
                }
            }
        }

        IPaymentProcessor _PaymentProcessor;

        IMailer _Mailer;
        CommerceEvents _Events;

        IPaymentProcessor IConfigurationFactory.GetPaymentProcessor()
        {
            return _PaymentProcessor;
        }

        IMailer IConfigurationFactory.GetMailer()
        {
            return _Mailer;
        }

        CommerceEvents IConfigurationFactory.GetEvents()
        {
            return _Events;
        }
    }
}
