//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_MonthComment.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_MonthComment file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using OPS5000Retrofit.Common;
using System.Collections.Generic;
using System.IO;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Month Comment Structure
    /// </summary>
    class OPS6000_MonthComment
    {
        /// <summary>
        /// Convert data commnet to OPS5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="ops6KBDKesoFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="ops5kCHOMBFilePath"></param>
        /// <param name="ops6kCHOMBFilePath"></param>
        /// <param name="ops6kHSTMFilePath"></param>
        /// <param name="ops5kHSTMFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int toYear,
                                    int toMonth,
                                    string ops6KBDKesoFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string ops5kCHOMBFilePath,
                                    string ops6kCHOMBFilePath,
                                    string ops6kHSTMBFilePath,
                                    string ops5kHSTMBFilePath)
        {
            // Read data OPS6000
            OPS6000_Month ops6kMonthbulk = new OPS6000_Month();
            ops6kMonthbulk.ReadData(ops6kHSTMBFilePath);

            // Read data OPS6000
            OPS5000_Month ops5kMonthbulk = new OPS5000_Month();
            ops5kMonthbulk.ReadData(ops5kHSTMBFilePath);

            // Declare maping men
            Dictionary<short, short> menMapping = new Dictionary<short, short>();
            Dictionary<short, short> menPartMaping = new Dictionary<short, short>();

            // Declare variable mark point fromdate, todate
            int part_PointStart = 0;
            int part_PointEnd = 0;

            if (moveAll)
            {
                // Loop OPS5000
                for (int i = 0; i < 11; i++)
                {
                    // Loop OPS6000
                    for (int j = 0; j < 3; j++)
                    {
                        if (ops5kMonthbulk.indexData.indx[i].yea_s == ops6kMonthbulk.indexData.indx[j].yea_s)
                        {
                            menMapping.Add(ops5kMonthbulk.indexData.indx[i].men_s, ops6kMonthbulk.indexData.indx[j].men_s);
                            menPartMaping.Add(ops6kMonthbulk.indexData.indx[j].men_s, ops6kMonthbulk.indexData.indx[j].prt_s);
                        }
                    }
                }
            }
            else
            {
                // move by time
                // Loop OPS5000
                for (int i = 0; i < 11; i++)
                {
                    // Loop OPS6000
                    for (int j = 0; j < 3; j++)
                    {
                        if (ops5kMonthbulk.indexData.indx[i].yea_s == ops6kMonthbulk.indexData.indx[j].yea_s &&
                            ops6kMonthbulk.indexData.indx[j].yea_s >= fromYear && ops6kMonthbulk.indexData.indx[j].yea_s <= toYear)
                        {
                            menMapping.Add(ops5kMonthbulk.indexData.indx[i].men_s, ops6kMonthbulk.indexData.indx[j].men_s);
                            menPartMaping.Add(ops6kMonthbulk.indexData.indx[j].men_s, ops6kMonthbulk.indexData.indx[j].prt_s);

                            // Mark from date
                            if (ops6kMonthbulk.indexData.indx[j].yea_s == fromYear)
                            {
                                part_PointStart = ops5kMonthbulk.indexData.indx[i].men_s;
                            }

                            // Mark to date
                            if (ops6kMonthbulk.indexData.indx[j].yea_s == toYear)
                            {
                                part_PointEnd = ops5kMonthbulk.indexData.indx[i].men_s;
                            }
                        }
                    }
                }
            }

            int monthStart = 0;
            int monthEnd = 0;
            short monthLast = 0;
            short menOps6000 = 0;
            int startIndex6k = 0;
            int startIndex5k = 0;

            // Convert data
            using (FileStream fs5k = new FileStream(ops5kCHOMBFilePath, FileMode.Open, FileAccess.Write))
            {
                using (FileStream fs6k = new FileStream(ops6kCHOMBFilePath, FileMode.Open, FileAccess.Read))
                {
                    foreach (KeyValuePair<short, short> menItem in menMapping)
                    {
                        monthStart = 1;
                        monthEnd = 12;
                        menOps6000 = menItem.Value;
                        if (menPartMaping.ContainsKey(menOps6000))
                        {
                            monthLast = (short)menPartMaping[menOps6000];
                            if (!moveAll)
                            {
                                // Get start for loop part when move data by month
                                if (part_PointStart == menItem.Key)
                                {
                                    monthStart = fromMonth;
                                    monthEnd = 12;
                                }

                                // Get end for loop part when move data by month
                                if (part_PointEnd == menItem.Key)
                                {
                                    // If month different
                                    if (part_PointStart != part_PointEnd)
                                    {
                                        monthStart = 1;
                                    }

                                    // Check month
                                    if (monthLast < toMonth)
                                    {
                                        monthEnd = monthLast;
                                    }
                                    else
                                    {
                                        monthEnd = toMonth;
                                    }
                                }
                            }
                            else
                            {
                                // Check last part
                                if (monthLast < monthEnd)
                                {
                                    monthEnd = monthLast;
                                }
                            }
                            for (int month = monthStart; month <= monthEnd; month++)
                            {
                                // Init byte array.
                                byte[] bytes = new byte[128];

                                // Read data cmtd
                                for (int cmtd = 1; cmtd <= 192; cmtd++)
                                {
                                    // Move cursor into 6k file
                                    startIndex6k = (menOps6000 - 1) * Constants.OPS6000_CHOMB_MEN_SIZE + (month - 1) * Constants.OPS6000_CHOMB_MONTH_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs6k.Seek(startIndex6k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHOMB_MEN_SIZE + (month - 1) * Constants.OPS5000_CHOMB_MONTH_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                                    // Read data 6k
                                    fs6k.Read(bytes, 0, 128);

                                    // Write data into 5k
                                    fs5k.Write(bytes, 0, 128);
                                }

                                // Declare size cmtd
                                int cmtdSize6k = 24576;// 192 * 128
                                int cmtdSize5k = 127872;// 999 * 128

                                // Read data cham
                                for (int cham = 1; cham <= 9; cham++)
                                {
                                    // Move cursor into 6k file
                                    startIndex6k = (menOps6000 - 1) * Constants.OPS6000_CHOMB_MEN_SIZE + (month - 1) * Constants.OPS6000_CHOMB_MONTH_SIZE + cmtdSize6k + (cham - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs6k.Seek(startIndex6k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHOMB_MEN_SIZE + (month - 1) * Constants.OPS5000_CHOMB_MONTH_SIZE + cmtdSize5k + (cham - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                                    // Read data 6k
                                    fs6k.Read(bytes, 0, 128);

                                    // Write data into 5k
                                    fs5k.Write(bytes, 0, 128);
                                }

                            }
                        }
                    }
                }
            }
        }


    }
}
