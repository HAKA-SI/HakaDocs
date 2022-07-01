using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Helpers
{
    public static class Extensions
    {
      public static void AddApplicationError(this HttpResponse response, string message)
      {
        response.Headers.Add("Application-Error", message);
        response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
        response.Headers.Add("Access-Control-Allow-Origin", "*");
      }

      public static void AddPagination(this HttpResponse response,
        int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
          var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
          var camelCaseFormatter = new JsonSerializerSettings();
          camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
          response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader,camelCaseFormatter));
          response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

      public static int CalculateAge(this DateTime theDateTime)
      {
        var age = 0;
        if(theDateTime!=null)
        {
          age = DateTime.Today.Year - Convert.ToInt32(theDateTime.Year);
          if(theDateTime.AddYears(age) > DateTime.Today)
            age--;
        }
        return age;
      }

      public static string DayIntToName(this int dayInt)
      {
        string dayName = "";
        switch (dayInt)
        {
          case 1:
            dayName = "lundi";
            break;
          case 2:
            dayName = "mardi";
            break;
          case 3:
            dayName = "mercredi";
            break;
          case 4:
            dayName = "jeudi";
            break;
          case 5:
            dayName = "vendredi";
            break;
          case 6:
            dayName = "samedi";
            break;
          case 7:
            dayName = "dimanche";
            break;
          default:
            dayName = "";
            break;
        }

        return dayName;
      }

      public static string CalculateTop(this DateTime startHourMin)
      {
        //to be retrieved from appSettings
        var scheduleHourSize = Convert.ToDouble(Startup.StaticConfig.GetSection("AppSettings:DimHourSchedule").Value);
        var startCourseHour = Convert.ToDouble(Startup.StaticConfig.GetSection("AppSettings:CoursesHourStart").Value);
        // var startCourseMin = Convert.ToDouble(Startup.StaticConfig.GetSection("AppSettings:CoursesMinStart").Value);

        var netHours = startHourMin.Hour - startCourseHour;
        var mins = startHourMin.Minute;
        var top = scheduleHourSize * (netHours + (double)mins/60) + 1 * netHours;
        top = Math.Round(top, 2);
        return (top + "px").Replace(",", ".");
      }

      public static string CalculateHeight(this DateTime startHourMin, DateTime endHourMin)
      {
        var scheduleHourSize =  Convert.ToDouble(Startup.StaticConfig.GetSection("AppSettings:DimHourSchedule").Value);

        TimeSpan span = endHourMin.Subtract(startHourMin);
        double height = span.TotalMinutes * scheduleHourSize/60;
        height = Math.Round(height, 2);
        return (height + "px").Replace(",", ".");
      }

      public static string CalculateWidth(this DateTime startHourMin, DateTime endHourMin)
      {
        var scheduleHourSize =  Convert.ToDouble(Startup.StaticConfig.GetSection("AppSettings:dimHourDashboard").Value);

        TimeSpan span = endHourMin.Subtract(startHourMin);
        double width = span.TotalMinutes * scheduleHourSize/60;
        width = Math.Round(width, 2);
        return (width + "px").Replace(",", ".");
      }

      public static string GetTimeAgo(this DateTime date)
      {
        DateTime today = DateTime.Now;
        TimeSpan span = today.Subtract(date);
        var days = span.TotalDays;
        if(days >= 1)
        {
          return Math.Round(days, 0) + " j";
        }
        else
        {
          var hours = span.TotalHours;
          if(hours >= 1)
          {
            return Math.Round(hours, 0) + " h";
          }
          else
          {
            var min = span.TotalMinutes;
            if(min >= 1)
            {
              return Math.Round(min, 0) + " mn";
            }
            else
            {
              var sec = span.TotalSeconds;
              var sec0 = Math.Round(sec, 0);
              return sec0 >= 1 ? sec0 + " sec" : "connecté";
            }
          }
        }
      }

      public static string GetDayString(this int day)
      {
        var dayString = "";

        switch (day)
        {
          case 1:
            dayString = "lundi";
            break;
          case 2:
            dayString = "mardi";
            break;
          case 3:
            dayString = "mercredi";
            break;
          case 4:
            dayString = "jeudi";
            break;
          case 5:
            dayString = "vendredi";
            break;
          case 6:
            dayString = "samedi";
            break;
          default:
            dayString = "";
            break;
        }

        return dayString;
      }

      public static bool IsNumeric(this string str)
      {
        if (str == null)
          return false;
        try
        {
          double result;
          if (double.TryParse(str.Replace(".", ","), out result))
            return true;
          else
            return false;
        }
        catch
        {
          return false;
        }
      }

      // Checks whether there are duplicates in a list.
      public static bool AreAnyDuplicates<T>(this IEnumerable<T> list)
      {
        var hashset = new HashSet<T>();
        return list.Any(e => !hashset.Add(e));
      }

      // Checks whether or not a date is a valid date.
      public static bool IsDate(this string str)
      {
        try
        {
          DateTime dt = DateTime.Parse(str);

          if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
            return true;
          return false;
        }
        catch
        {
          return false;
        }
      }

      public static string FirstLetterToUpper(this string s)
      {
        if (string.IsNullOrEmpty(s))
        {
          return string.Empty;
        }

        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
      }

      public static string UppercaseWords(this string value)
      {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
          if (char.IsLower(array[0]))
          {
            array[0] = char.ToUpper(array[0]);
          }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
          if (array[i - 1] == ' ')
          {
            if (char.IsLower(array[i]))
            {
              array[i] = char.ToUpper(array[i]);
            }
          }
        }
        return new string(array);
      }

      public static string To5Digits(this string data)
      {
        switch (data.Length)
        {
          case 1:
            data = "0000" + data;
            break;
          case 2:
            data = "000" + data;
            break;
          case 3:
            data = "00" + data;
            break;
          case 4:
            data = "0" + data;
            break;
        }
        return data;
      }
      
      public static string FormatPhoneNumber(this string phone)
      {
        if(phone == null)
          return phone;
        if (phone.Length == 10)
        {
          return String.Format("{0}.{1}.{2}.{3}.{4}", phone.Substring(0, 2), phone.Substring(2, 2),
            phone.Substring(4, 2), phone.Substring(6, 2), phone.Substring(8));
        }
        return phone;
      }

      public static string RegNum5digits(string refNum)
      {
        string idnum = "";

        if (refNum.Length == 1)
          idnum += "0000" + refNum;
        else if (refNum.Length == 2)
          idnum += "000" + refNum;
        else if (refNum.Length == 3)
          idnum += "00" + refNum;
        else if (refNum.Length == 4)
          idnum += "0" + refNum;
        else
          idnum += refNum;

        return idnum;
      }

      //email validation
      public static bool EmailValid(this string email)
      {
        string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
        pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";
        System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
        if (match.Success)
          return true;
        else
          return false;
      }

      public static int GetOrderNumber(this int orderId)
      {
        var today = DateTime.Now;
        string year = today.Year.ToString().Substring(2);
        string month = today.Month.ToString().Length == 1 ? "0" + today.Month.ToString() : today.Month.ToString();
        string day = today.Day.ToString().Length == 1 ? "0" +  today.Day.ToString() : today.Day.ToString();
        var data = year + month + day + orderId.ToString();
        return Convert.ToInt32(data);
      }

      public static string GetSubDomain(string url)
      {
        if (url.Split('.').Length > 2)
        {
          int lastIndex = url.LastIndexOf(".");
          int index = url.LastIndexOf(".", lastIndex - 1);
          return url.Substring(0, index).ToLower();
        }
        return null;
      }

      public static Boolean IsDarkColor(this string hexColor)
      {
        if(!hexColor.IsNumeric())
        {
          return false;
        }
        else
        {
          string shortColor = hexColor.Substring(1,1);
          if(Convert.ToInt32(shortColor) < 5)
            return true;
          else
            return false;
        }
      }

      public static string CreateString(int stringLength)
      {
        Random rd = new Random();
        const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789-";
        char[] chars = new char[stringLength];

        for (int i = 0; i < stringLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
      }

      // public static String NumberToLetter(long number) {
      //   if (number == 0) {
      //     return "zero";
      //   }

      //   String snumber = number.ToString();// Long.toString(number);
      //   String mask = "000000000000";
      //   DecimalFormat df = new DecimalFormat(mask);
      //   snumber = df.format(number);

      //   int lesMilliards = Integer.parseInt(snumber.substring(0, 3));
      //   int lesMillions = Integer.parseInt(snumber.substring(3, 6));
      //   int lesCentMille = Integer.parseInt(snumber.substring(6, 9));
      //   int lesMille = Integer.parseInt(snumber.substring(9, 12));

      //   String tradMilliards;
      //   switch (lesMilliards) {
      //   case 0:
      //     tradMilliards = "";
      //     break;
      //   case 1:
      //     tradMilliards = convertLessThanOneThousand(lesMilliards) + " milliard ";
      //     break;
      //   default:
      //     tradMilliards = convertLessThanOneThousand(lesMilliards) + " milliards ";
      //   }
      //   String resultat = tradMilliards;

      //   String tradMillions;
      //   switch (lesMillions) {
      //   case 0:
      //     tradMillions = "";
      //     break;
      //   case 1:
      //     tradMillions = convertLessThanOneThousand(lesMillions) + " million ";
      //     break;
      //   default:
      //     tradMillions = convertLessThanOneThousand(lesMillions) + " millions ";
      //   }
      //   resultat = resultat + tradMillions;

      //   String tradCentMille;
      //   switch (lesCentMille) {
      //   case 0:
      //     tradCentMille = "";
      //     break;
      //   case 1:
      //     tradCentMille = "mille ";
      //     break;
      //   default:
      //     tradCentMille = convertLessThanOneThousand(lesCentMille) + " mille ";
      //   }
      //   resultat = resultat + tradCentMille;

      //   String tradMille;
      //   tradMille = convertLessThanOneThousand(lesMille);
      //   resultat = resultat + tradMille;

      //   return resultat;
      // }

      // private static string[] dizaineNames = { "", //
      //       "", //
      //       "vingt", //
      //       "trente", //
      //       "quarante", //
      //       "cinquante", //
      //       "soixante", //
      //       "soixante", //
      //       "quatre-vingt", //
      //       "quatre-vingt" //
      // };//from w  w w.  j  a  va  2  s  .  c  o  m

      // private static final String[] uniteNames1 = { "", //
      //       "un", //
      //       "deux", //
      //       "trois", //
      //       "quatre", //
      //       "cinq", //
      //       "six", //
      //       "sept", //
      //       "huit", //
      //       "neuf", //
      //       "dix", //
      //       "onze", //
      //       "douze", //
      //       "treize", //
      //       "quatorze", //
      //       "quinze", //
      //       "seize", //
      //       "dix-sept", //
      //       "dix-huit", //
      //       "dix-neuf" //
      // };

      // private static final String[] uniteNames2 = { "", //
      //       "", //
      //       "deux", //
      //       "trois", //
      //       "quatre", //
      //       "cinq", //
      //       "six", //
      //       "sept", //
      //       "huit", //
      //       "neuf", //
      //       "dix" //
      // };

      // private Main() {
      // }

      // private static String convertZeroToHundred(int number) {

      //     int laDizaine = number / 10;
      //     int lUnite = number % 10;
      //     String resultat = "";

      //     switch (laDizaine) {
      //     case 1:
      //     case 7:
      //     case 9:
      //       lUnite = lUnite + 10;
      //       break;
      //     default:
      //     }

      //     String laLiaison = "";
      //     if (laDizaine > 1) {
      //       laLiaison = "-";
      //     }
      //     switch (lUnite) {
      //     case 0:
      //       laLiaison = "";
      //       break;
      //     case 1:
      //       if (laDizaine == 8) {
      //           laLiaison = "-";
      //       } else {
      //           laLiaison = " et ";
      //       }
      //       break;
      //     case 11:
      //       if (laDizaine == 7) {
      //           laLiaison = " et ";
      //       }
      //       break;
      //     default:
      //     }

      //     // dizaines en lettres
      //     switch (laDizaine) {
      //     case 0:
      //       resultat = uniteNames1[lUnite];
      //       break;
      //     case 8:
      //       if (lUnite == 0) {
      //           resultat = dizaineNames[laDizaine];
      //       } else {
      //           resultat = dizaineNames[laDizaine] + laLiaison + uniteNames1[lUnite];
      //       }
      //       break;
      //     default:
      //       resultat = dizaineNames[laDizaine] + laLiaison + uniteNames1[lUnite];
      //     }
      //     return resultat;
      // }

      // private static String convertLessThanOneThousand(int number) {

      //     int lesCentaines = number / 100;
      //     int leReste = number % 100;
      //     String sReste = convertZeroToHundred(leReste);

      //     String resultat;
      //     switch (lesCentaines) {
      //     case 0:
      //       resultat = sReste;
      //       break;
      //     case 1:
      //       if (leReste > 0) {
      //           resultat = "cent " + sReste;
      //       } else {
      //           resultat = "cent";
      //       }
      //       break;
      //     default:
      //       if (leReste > 0) {
      //           resultat = uniteNames2[lesCentaines] + " cent " + sReste;
      //       } else {
      //           resultat = uniteNames2[lesCentaines] + " cents";
      //       }
      //     }
      //     return resultat;
      // }
    }
}