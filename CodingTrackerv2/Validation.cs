using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerv2
{
    public class Validation
    {
        static public bool isNumberValid(string value)
        {
            if (int.TryParse(value, out int result) && result >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        static public bool isDateValid(string date)
        {
            string format = "dd-MM-yy HH:mm";
            DateTime dateValue;
            bool isValidDate = DateTime.TryParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                out dateValue);
            if (isValidDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
