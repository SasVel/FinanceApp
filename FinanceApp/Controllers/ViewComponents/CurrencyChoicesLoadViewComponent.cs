using FinanceApp.Core.Common;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System;
using FinanceApp.Core.Helpers;

namespace FinanceApp.Controllers.ViewComponents
{
    public class CurrencyChoicesLoadViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new CurrencyViewModel();
            var enumNames = Enum.GetNames(typeof(Currencies));
            var enumCount = enumNames.Length;
            for (int i = 0; i < enumCount; i++)
            {
                model.CurrencyNames.Add(enumNames[i]);
                model.CurrencyCodes.Add(EnumHelper.GetEnumDescription((Currencies)i));
            }

            return View(model);
        }

    }
}
