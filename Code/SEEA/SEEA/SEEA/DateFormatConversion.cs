using System;
using System.Linq;

namespace SEEA
{
    public static class DateFormatConversion
    {
        public static DateTime ConvertStringToDate(String date)
        {

            string[] dateAndTime = date.Replace(".", "/").Split(',');
            dateAndTime = (from e in dateAndTime
                           select e.Trim()).ToArray();

            String newDateAsString = dateAndTime[0] + " " + dateAndTime[1];

            DateTime convertedDate = Convert.ToDateTime(newDateAsString, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

            return convertedDate;
        }


    }
}
