//////////////////////////////////////////////////////////////////////
// File Name     ：DSVSCommon.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：DSVSCommon file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System;
using System.IO;

namespace OPS5000Retrofit
{
    /// <summary>
    /// DSVS Common Structure
    /// </summary>
    public static class DSVSCommon
    {
        public enum CsvFileType
        {
            none = 0,
            HourDataBulk = 1,
            DayDataBulk = 2,
            MonthDataBulk = 3,
            YearDataBulk = 4,
            DayCommentData = 5,
            MonthCommentData = 6,
            YearCommentData = 7
        }

        public static OPS5000_T_HSTHB_DAT ConvertDSVSHourDataToOPSHourData(DSVSUnitBulkData unit)
        {
            OPS5000_T_HSTHB_DAT amount = new OPS5000_T_HSTHB_DAT();
            amount.sts_s = unit.sts_s;
            amount.inf_s[0] = unit.yobi1_s;
            amount.inf_s[1] = unit.yobi2_s;
            amount.dat_d = unit.dat_d;

            return amount;
        }

        public static OPS5000_T_HSTDB_HISTORY ConvertDSVSDayDataToOPSDayData(DSVSUnitBulkData unit)
        {
            OPS5000_T_HSTDB_HISTORY amount = new OPS5000_T_HSTDB_HISTORY();
            OPS5000_T_HSTDB_DAT dayUnit = new OPS5000_T_HSTDB_DAT();

            dayUnit.sts_s = unit.sts_s;
            dayUnit.inf_s[0] = unit.yobi1_s;
            dayUnit.inf_s[1] = unit.yobi2_s;
            dayUnit.dat_d = unit.dat_d;

            for (int i = 0; i < 5; i++)
            {
                amount.history_lst.Add(dayUnit);
            }

            return amount;
        }
        public static string GetDateTime(string file, DSVSCommon.CsvFileType fileType)
        {
            string line = GetLine(file, 3);
            switch (fileType)
            {
                case DSVSCommon.CsvFileType.HourDataBulk:
                    string[] hour = line.Split(new char[] { ',' });
                    return hour[0] + "/" + hour[1] + "/" + hour[2];
                case DSVSCommon.CsvFileType.DayDataBulk:
                    string[] day = line.Split(new char[] { ',' });
                    return day[0] + "/" + day[1];
                case DSVSCommon.CsvFileType.MonthDataBulk:
                    string[] month = line.Split(new char[] { ',' });
                    return month[0];
                case DSVSCommon.CsvFileType.YearDataBulk:
                    string[] year = line.Split(new char[] { ',' });
                    return year[0];
            }
            return String.Empty;
        }



        public static DSVSCommon.CsvFileType GetFileType(string fileName)
        {
            string line = GetLine(fileName, 1);
            return (DSVSCommon.CsvFileType)Int32.Parse(line.Substring(0, 1));
        }

        public static string GetLine(string fileName, int line)
        {
            using (var sr = new StreamReader(fileName))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }

        public static int GetTotalTag(string file)
        {
            string line = GetLine(file, 2);
            int fisrtCommaIdx = line.IndexOf(",");

            return Int32.Parse(line.Substring(0, fisrtCommaIdx));
        }


    }
}
