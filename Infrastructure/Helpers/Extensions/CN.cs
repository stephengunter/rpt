using System;

namespace Infrastructure.Helpers;
public static class CNHelpers
{
   static string[] cnNumbers = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

   public static string ToCNNumber(this int val)
   {
      string strVal = val.ToString();
      int length = strVal.Length;
      string intStr = strVal.Substring(length - 1, 1);

      if (length == 1) return cnNumbers[intStr.ToInt()];

      intStr = intStr.ToInt() == 0 ? "" : cnNumbers[intStr.ToInt()];

      string ten = strVal.Substring(length - 2, 1);
      string tenStr = cnNumbers[ten.ToInt()];
      if (length == 2)
      {
         if (String.IsNullOrEmpty(intStr))
         {
            return $"{tenStr}十{intStr}";
         }
         else
         {
            if (ten.ToInt() > 1) return $"{tenStr}十{intStr}";
            else return $"十{intStr}";
         }

      }

      string hundred = strVal.Substring(length - 3, 1);
      string hundredStr = cnNumbers[hundred.ToInt()];
      if (length == 3)
      {
         if (String.IsNullOrEmpty(intStr))
         {
            return $"{hundredStr}百{tenStr}十";
         }
         else
         {
            return $"{hundredStr}百{tenStr}十${intStr}";

         }
      }

      return "";

   }

   public static bool IsValidRocDate(this int val)
   {
      string str_val = val.ToString();
      return str_val.Length == 6 || str_val.Length == 7;
   }

   public static string ToRocDateText(this int val)
   {
      string strVal = val.ToString();
      if (strVal.Length == 6)
      {
         return $"{strVal.Substring(0, 2)}-{strVal.Substring(2, 2)}-{strVal.Substring(4, 2)}";
      }
      if (strVal.Length == 7)
      {
         return $"{strVal.Substring(0, 3)}-{strVal.Substring(3, 2)}-{strVal.Substring(5, 2)}";
      }
      return "";

   }
   public static bool IsValidTWID(this string id)
   {
      // Trim the input
      id = id.Trim();

      // Regular expression to check the format: first letter, followed by 1 or 2, then 8 digits
      if (!System.Text.RegularExpressions.Regex.IsMatch(id, "^[A-Z][12]\\d{8}$"))
      {
         return false;
      }

      // Convert the first letter to a number
      string conver = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
      int firstLetterValue = conver.IndexOf(id[0]) + 10;

      // Replace the first letter with its corresponding numeric value
      id = firstLetterValue.ToString() + id.Substring(1);

      // Weights for the checksum calculation
      int[] weights = { 1, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1 };

      // Calculate the checksum
      int checkSum = 0;
      for (int i = 0; i < id.Length; i++)
      {
         checkSum += (id[i] - '0') * weights[i];
      }

      // Check if the checksum is divisible by 10
      return checkSum % 10 == 0;
   }
   public static bool IsValidPhoneNumber(this string input)
   {
      // Regular expression to match phone number starting with 0 and containing exactly 10 digits
      var regex = new System.Text.RegularExpressions.Regex(@"^0\d{9}$");

      // Return true if the input matches the regular expression
      return regex.IsMatch(input);
   }

   public static bool IsValidDob(this string input)
   {
      // Check if input is exactly 6 characters long
      if (input.Length != 6)
      {
         return false;
      }

      // Parse the parts of the input
      string yearPart = input.Substring(0, 2);  // First two characters for the year
      string monthPart = input.Substring(2, 2); // Middle two characters for the month
      string dayPart = input.Substring(4, 2);   // Last two characters for the day

      // Convert the parts to integers
      if (!int.TryParse(yearPart, out int year) || !int.TryParse(monthPart, out int month) || !int.TryParse(dayPart, out int day))
      {
         return false;  // Input must contain valid numbers
      }

      // Check if year is between 15 and 99
      if (year < 15 || year > 99)
      {
         return false;
      }

      // Check if month is between 1 and 12
      if (month < 1 || month > 12)
      {
         return false;
      }

      // Check if the day is valid for the given month
      if (!IsValidDayForMonth(month, day))
      {
         return false;
      }

      // If all checks pass, the DOB is valid
      return true;
   }

   static bool IsValidDayForMonth(int month, int day)
   {
      // Days per month (index corresponds to month number, where index 1 is January, index 2 is February, etc.)
      int[] daysInMonth = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

      // Check if the day is within the valid range for the given month
      return day >= 1 && day <= daysInMonth[month];
   }


}
