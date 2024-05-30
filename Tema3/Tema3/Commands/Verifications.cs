using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema3.Commands
{
    public class Verifications
    {
        public bool IsValidDate(string dateString)
        {
            DateTime dateValue;
            string format = "yyyy-MM-dd";
            CultureInfo provider = CultureInfo.InvariantCulture;
            bool isValid = DateTime.TryParseExact(dateString, format, provider, DateTimeStyles.None, out dateValue);

            return isValid;
        }
        public int IsIntegerNumber(string dateString)
        {
            int numberInInteger = -1;
            if (!int.TryParse(dateString, out numberInInteger))
            {
                Console.WriteLine("Cantitatea trebuie să fie un număr întreg valid.");
                numberInInteger = -1;
            }
            return numberInInteger;
        }
    }
}
