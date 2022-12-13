using FinanceApp.Core.Common;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System;

namespace FinanceApp.Controllers.ViewComponents
{
    public class CurrencyChoicesLoadViewComponent : ViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            var model = new CurrencyViewModel();
            var enumCount = Enum.GetValues(typeof(Currencies)).Length;
            for (int i = 0; i < enumCount; i++)
            {
                model.CurrencyNames.Add(Enum.GetName(typeof(Currencies), i));
                model.CurrencyCodes.Add(Enum.Get, i));
            }
        }

        private string ToDescriptionString(this Currencies val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

    }
}
