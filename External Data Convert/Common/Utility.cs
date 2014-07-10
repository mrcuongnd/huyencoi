//////////////////////////////////////////////////////////////////////
// File Name     ：Utility.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Utilities class
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////
using System;
using System.Text;

namespace OPS5000Retrofit.Common
{
    /// <summary>
    /// Utility class
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Get short array from byte array
        /// </summary>
        /// <param name="arr">Byte array</param>
        /// <param name="startIdx">Start index</param>
        /// <returns></returns>
        public static short[] GetShortArray4(byte[] arr, int startIdx)
        {
            short[] ret = new short[2];

            ret[0] = BitConverter.ToInt16(arr, startIdx);
            ret[1] = BitConverter.ToInt16(arr, startIdx + 2);

            return ret;
        }

        /// <summary>
        /// Get short array from byte array
        /// </summary>
        /// <param name="arr">Byte array</param>
        /// <param name="startIdx">Start index</param>
        /// <returns></returns>
        public static short[] GetShortArray6(byte[] arr, int startIdx)
        {
            short[] ret = new short[3];

            ret[0] = BitConverter.ToInt16(arr, startIdx);
            ret[1] = BitConverter.ToInt16(arr, startIdx + 2);
            ret[2] = BitConverter.ToInt16(arr, startIdx + 4);

            return ret;
        }

        /// <summary>
        /// Create date time 
        /// </summary>
        /// <param name="Year">Year</param>
        /// <param name="Month">Month</param>
        /// <param name="Day">Day</param>
        /// <returns></returns>
        public static DateTime CreateDateTime(short Year, short Month, short Day)
        {
            return new DateTime(Year, Month, Day);
        }

        /// <summary>
        /// Compare two date
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int DayCompare(string date1, DateTime date2)
        {
            DateTime dt = Convert.ToDateTime(date1);
            return DateTime.Compare(dt, date2);
        }

        /// <summary>
        /// Compare month 
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static int MonthCompare(string dateTime1, string dateTime2)
        {
            return String.Compare(dateTime1, dateTime2);
        }

        /// <summary>
        /// Create Year + Month Key
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static String CreateYearMonthKey(short Year, short Month)
        {
            return Year.ToString() + Month.ToString();
        }

        /// <summary>
        /// Check date time in a range
        /// </summary>
        /// <param name="dateToCheck"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static bool InRange(DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck <= endDate;
        }

        /// <summary>
        /// Check date time in a range
        /// </summary>
        /// <param name="dateToCheck"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static bool InRange(string dateToCheck, DateTime startDate, DateTime endDate)
        {
            DateTime checkDate = new DateTime();
            string[] dateArray = dateToCheck.Split(new char[] { '/' });
            if (dateArray.Length == 2)
            {
                checkDate = new DateTime(Int32.Parse(dateArray[0]), Int32.Parse(dateArray[1]), 1);
            }
            else if (dateArray.Length == 3)
            {
                checkDate = new DateTime(Int32.Parse(dateArray[0]), Int32.Parse(dateArray[1]), Int32.Parse(dateArray[2]));
            }

            return checkDate >= startDate && checkDate <= endDate;
        }

        /// <summary>
        /// Fill space to tag no
        /// </summary>
        /// <param name="tagNo"></param>
        /// <returns></returns>
        public static string FillSpaceToTagNo(string tagNo)
        {
            return tagNo.PadRight(8);
        }

        /// <summary>
        /// Convert array to string
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToString(short[] arr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(arr[i].ToString());
                if (i < arr.Length - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Check data type integer and repalce
        /// </summary>
        /// <param name="pValue"></param>
        /// <param name="pReplaceValue"></param>
        /// <returns></returns>
        public static int CheckIntegerReplace(object pValue, int pReplaceValue)
        {
            int ReturnValue;
            try
            {
                ReturnValue = int.Parse(pValue.ToString());
            }
            catch (Exception)
            {
                ReturnValue = pReplaceValue;
            }
            return ReturnValue;
        }

        /// <summary>
        /// Check data type double and repalce
        /// </summary>
        /// <param name="pValue"></param>
        /// <param name="pReplaceValue"></param>
        /// <returns></returns>
        public static double CheckDoubleReplace(object pValue, double pReplaceValue)
        {
            double ReturnValue;
            try
            {
                ReturnValue = double.Parse(pValue.ToString());
            }
            catch (Exception)
            {
                ReturnValue = pReplaceValue;
            }
            return ReturnValue;
        }
    }
}
