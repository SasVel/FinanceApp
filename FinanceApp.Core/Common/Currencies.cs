using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Common
{
    public enum Currencies
    {
        [Description("&#8364;")]
        EUR,
        [Description("&#36;")]
        USD,
        [Description("&#163;")]
        GBP,
        [Description("&#165;")]
        JPY,
        [Description("лв.")]
        BGN
    }

    
}
