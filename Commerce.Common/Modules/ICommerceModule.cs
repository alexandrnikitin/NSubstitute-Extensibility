using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Commerce.Common
{
    public interface ICommerceModule
    {
        void Initialize(CommerceEvents events, NameValueCollection config);
    }
}
