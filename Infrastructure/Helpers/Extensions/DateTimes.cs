﻿using System;

namespace Infrastructure.Helpers;
public static class DateTimeHelpers
{
   public static DateTime? ResolveToDate(this string? input)
   {
      if (string.IsNullOrEmpty(input)) return null;
      if (input.Length != 6)
      {
         throw new ArgumentException("Input must be in the format 'yyMMdd'");
      }

      // Extract parts from the input string
      int year = int.Parse(input.Substring(0, 2));
      int month = int.Parse(input.Substring(2, 2));
      int day = int.Parse(input.Substring(4, 2));

      // Return the resolved date
      return new DateTime(year + 1911, month, day);
   }
   public static DateTime? ToStartDate(this string? input)
	{
		if(string.IsNullOrEmpty(input)) return null;

      var startDate = input.ToDatetimeOrNull();
		if (startDate.HasValue)
		{
			var dateStart = Convert.ToDateTime(startDate);
			return new DateTime(dateStart.Year, dateStart.Month, dateStart.Day, 0, 0, 0);
		}
		else return null;
	}

	public static DateTime? ToEndDate(this string? input)
	{
      if (string.IsNullOrEmpty(input)) return null;

      var endDate = input.ToDatetimeOrNull();
		if (endDate.HasValue)
		{
			var dateEnd = Convert.ToDateTime(endDate);
			return dateEnd.ToEndDate();
		}
		else return null;
	}

	public static DateTime ToEndDate(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
	
	public static DateTime? ToDatetimeOrNull(this string str)
	{
		if(String.IsNullOrEmpty(str)) return null;

		DateTime dateValue;
		if (DateTime.TryParse(str, out dateValue)) return dateValue;
		return null;
	}
	public static DateTime ToDatetimeOrDefault(this string str, DateTime defaultValue)
	{
		DateTime dateValue;
		if (DateTime.TryParse(str, out dateValue)) return dateValue;

		return defaultValue;

	}
	public static DateTime ToDatetime(this int val)
	{
		var strVal = val.ToString();

		int year = strVal.Substring(0, 4).ToInt();
		int month = strVal.Substring(4, 2).ToInt();
		int day = strVal.Substring(6, 2).ToInt();

		return new DateTime(year, month, day);
	}

	public static string GetDateString(this DateTime dateTime, bool roc = false)
		=> roc ? GetDateString(dateTime.Year - 1911, dateTime.Month, dateTime.Day) : GetDateString(dateTime.Year, dateTime.Month, dateTime.Day);
   public static string GetDateString(int year, int month, int day)
   {
      string syear = year.ToString();
      string smonth = month < 10 ? $"0{month}" : month.ToString();
      string sday = day < 10 ? $"0{day}" : day.ToString();

		return $"{syear}{smonth}{sday}";
   }
   static string GetTimeString(DateTime dateTime, bool toMileSecond = false)
	{
		string hour = dateTime.Hour.ToString();
		string minute = dateTime.Minute.ToString();
		string second = dateTime.Second.ToString();
		string mileSecond = dateTime.Millisecond.ToString();

		if (dateTime.Hour < 10) hour = "0" + hour;
		if (dateTime.Minute < 10) minute = "0" + minute;
		if (dateTime.Second < 10) second = "0" + second;

		if (!toMileSecond) return hour + minute + second;


		if (dateTime.Millisecond < 10)
		{
			mileSecond = "00" + mileSecond;
		}
		else if (dateTime.Millisecond < 100)
		{
			mileSecond = "0" + mileSecond;
		}

		return hour + minute + second + mileSecond;

	}
	public static int ToDateNumber(this DateTime input) => Convert.ToInt32(GetDateString(input.Date));
	public static int ToTimeNumber(this DateTime input) => Convert.ToInt32(GetTimeString(input));
	public static string ToDateString(this DateTime input) => input.ToString("yyyy-MM-dd");
	public static string ToDateString(this DateTime? input) => input.HasValue ? input.Value.ToDateString() : string.Empty;
	public static string ToDateTimeString(this DateTime input) => input.ToString("yyyy-MM-dd H:mm:ss");
	public static string ToDateTimeString(this DateTime? input) => input.HasValue ? input.Value.ToDateTimeString() : string.Empty;

}
