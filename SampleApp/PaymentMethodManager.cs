using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Acumatica;

namespace SampleApp
{
    public class PaymentMethodManager
    {
        private Screen _context;
        AR303010Content _AR303010;
           
        public PaymentMethodManager(Screen context)
        {
            _context = context;
        }

        public int AddCreditCard(string customerID, string paymentMethod, string cardNumber, string expirationDate, string cvv, string nameOnCard)
        {
            if(_AR303010 == null) _AR303010 = _context.AR303010GetSchema();
            _context.AR303010Clear();
            
            var commands = new Command[]
            {
                new Value { Value = customerID, LinkedCommand = _AR303010.PaymentMethodSelection.Customer },
                new Value { Commit = true, LinkedCommand = _AR303010.Actions.Insert },
                new Value { Value = paymentMethod, LinkedCommand = _AR303010.PaymentMethodSelection.PaymentMethod },
                new Value { Value = "CCDNUM", LinkedCommand = _AR303010.PaymentMethodDetails.Description },
                new Value { Value = cardNumber, LinkedCommand = _AR303010.PaymentMethodDetails.Value, Commit = true },
                new Value { Value = "EXPDATE", LinkedCommand = _AR303010.PaymentMethodDetails.Description },
                new Value { Value = expirationDate,  LinkedCommand = _AR303010.PaymentMethodDetails.Value, Commit = true},
                new Value { Value = "CVV", LinkedCommand = _AR303010.PaymentMethodDetails.Description },
                new Value { Value = cvv, LinkedCommand = _AR303010.PaymentMethodDetails.Value, Commit = true },
                new Value { Value = "NAMEONCC", LinkedCommand = _AR303010.PaymentMethodDetails.Description },
                new Value { Value = nameOnCard,  LinkedCommand = _AR303010.PaymentMethodDetails.Value, Commit = true },
                _AR303010.Actions.Save,
                _AR303010.PaymentMethodSelection.TokenID
            };

            var result = _context.AR303010Submit(commands.ToArray());
            return int.Parse(result[0].PaymentMethodSelection.TokenID.Value);
        }

        public void UpdateCreditCardExpirationDate(string customerID, string paymentMethod, int tokenID, string expirationDate)
        {
            if (_AR303010 == null) _AR303010 = _context.AR303010GetSchema();
            _context.AR303010Clear();

            var commands = new Command[]
            {
                new Value { Value = customerID, LinkedCommand = _AR303010.PaymentMethodSelection.Customer },
                new Value { Commit = true, LinkedCommand = _AR303010.Actions.Insert },
                new Value { Value = paymentMethod, LinkedCommand = _AR303010.PaymentMethodSelection.PaymentMethod },
                new Value { Value = tokenID.ToString(), LinkedCommand = _AR303010.PaymentMethodSelection.TokenID },
                new Value { Value = "EXPDATE", LinkedCommand = _AR303010.PaymentMethodDetails.Description },
                new Value { Value = expirationDate,  LinkedCommand = _AR303010.PaymentMethodDetails.Value, Commit = true},
                _AR303010.Actions.Save,
            };

            var result = _context.AR303010Submit(commands.ToArray());
        }

        public void MakeCardInactive(string customerID, string paymentMethod, int tokenID)
        {
            if (_AR303010 == null) _AR303010 = _context.AR303010GetSchema();
            _context.AR303010Clear();

            var commands = new Command[]
            {
                new Value { Value = customerID, LinkedCommand = _AR303010.PaymentMethodSelection.Customer },
                new Value { Commit = true, LinkedCommand = _AR303010.Actions.Insert },
                new Value { Value = paymentMethod, LinkedCommand = _AR303010.PaymentMethodSelection.PaymentMethod },
                new Value { Value = tokenID.ToString(), LinkedCommand = _AR303010.PaymentMethodSelection.TokenID },
                new Value { Value = "False", LinkedCommand = _AR303010.PaymentMethodSelection.Active },
                _AR303010.Actions.Save,
            };

            var result = _context.AR303010Submit(commands.ToArray());
        }
    }
}
