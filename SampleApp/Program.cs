using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Acumatica;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Screen();
            context.CookieContainer = new System.Net.CookieContainer();
            context.Login("yourlogin", "yourpassword");

            var paymentMethodManager = new PaymentMethodManager(context);
            int tokenID = paymentMethodManager.AddCreditCard("ABARTENDE", "MASTERCARD", "5111111111111118", "122016", "123", "John Doe");
            paymentMethodManager.UpdateCreditCardExpirationDate("ABARTENDE", "MASTERCARD", tokenID, "032017");
            paymentMethodManager.MakeCardInactive("ABARTENDE", "MASTERCARD", tokenID);
        }
    }
}
