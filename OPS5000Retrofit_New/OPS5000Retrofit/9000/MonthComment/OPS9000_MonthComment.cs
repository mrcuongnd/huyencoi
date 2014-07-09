//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_MonthComment.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
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
    class OPS9000_MonthComment
    {
        /// <summary>
        /// Convert data commnent to OPS5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="ops9KBDKesoFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="ops5kCHOMBFilePath"></param>
        /// <param name="ops9kCHOMBFilePath"></param>
        /// <param name="ops9kHSTMFilePath"></param>
        /// <param name="ops5kHSTMFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int toYear,
                                    int toMonth,
                                    string ops9KBDKesoFilePath,
                                    string ops9KDDtptFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string ops5kCHOMBFilePath,
                                    string ops9kCHOMBFilePath,
                                    string ops9kHSTMBFilePath,
                                    string ops5kHSTMBFilePath)
        {
            // Read data OPS9000
            OPS9000_Month ops9kMonthbulk = new OPS9000_Month();
            ops9kMonthbulk.ReadData(ops9kHSTMBFilePath);

            // Read data OPS9000
            OPS5000_Month ops5kMonthbulk = new OPS5000_Month();
            ops5kMonthbulk.ReadData(ops5kHSTMBFilePath);

            // Create history mapping table
            Dictionary<string, string> ops9kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable9K(ops9KBDKesoFilePath, ops9KDDtptFilePath);
            Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
            Dictionary<string, string> historyMapping = OPSBDCommon.CreateHistoryMappingTable9K_5K(ops5kMapping, ops9kMapping);

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
                    // Loop OPS9000
                    for (int j = 0; j < 6; j++)
                    {
                        if (ops5kMonthbulk.indexData.indx[i].yea_s == ops9kMonthbulk.indexData.indx[j].yea_s)
                        {
                            menMapping.Add(ops5kMonthbulk.indexData.indx[i].men_s, ops9kMonthbulk.indexData.indx[j].men_s);
                            menPartMaping.Add(ops9kMonthbulk.indexData.indx[j].men_s, ops9kMonthbulk.indexData.indx[j].prt_s);
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
                    // Loop OPS9000
                    for (int j = 0; j < 6; j++)
                    {
                        if (ops5kMonthbulk.indexData.indx[i].yea_s == ops9kMonthbulk.indexData.indx[j].yea_s &&
                            ops9kMonthbulk.indexData.indx[j].yea_s >= fromYear && ops9kMonthbulk.indexData.indx[j].yea_s <= toYear)
                        {
                            menMapping.Add(ops5kMonthbulk.indexData.indx[i].men_s, ops9kMonthbulk.indexData.indx[j].men_s);
                            menPartMaping.Add(ops9kMonthbulk.indexData.indx[j].men_s, ops9kMonthbulk.indexData.indx[j].prt_s);

                            // Mark from date
                            if (ops9kMonthbulk.indexData.indx[j].yea_s == fromYear && ops9kMonthbulk.indexData.indx[j].mon_s == fromMonth)
                            {
                                part_PointStart = ops5kMonthbulk.indexData.indx[i].men_s;
                            }

                            // Mark to date
                            if (ops9kMonthbulk.indexData.indx[j].yea_s == toYear && ops9kMonthbulk.indexData.indx[j].mon_s == toMonth)
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
            short menOps9000 = 0;
            int startIndex9k = 0;
            int startIndex5k = 0;

            // Convert data
            using (FileStream fs5k = new FileStream(ops5kCHOMBFilePath, FileMode.Open, FileAccess.Write))
            {
                using (FileStream fs9k = new FileStream(ops9kCHOMBFilePath, FileMode.Open, FileAccess.Read))
                {
                    foreach (KeyValuePair<short, short> menItem in menMapping)
                    {
                        monthStart = 1;
                        monthEnd = 12;
                        menOps9000 = menItem.Value;
                        if (menPartMaping.ContainsKey(menOps9000))
                        {
                            monthLast = (short)menPartMaping[menOps9000];
                            if (!moveAll)
                            {
                                // Get start, end for loop part when move data by month

                                if (part_PointStart == menItem.Key)
                                {
                                    monthStart = fromMonth;
                                    monthEnd = 12;
                                }
                                if (part_PointEnd == menItem.Key)
                                {
                                    if (part_PointStart != part_PointEnd)
                                    {
                                        monthStart = 1;
                                    }
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
                                // Checkt last part
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
                                    // Move cursor into 9k file
                                    startIndex9k = (menOps9000 - 1) * Constants.OPS9000_CHOMB_MEN_SIZE + (month - 1) * Constants.OPS9000_CHOMB_MONTH_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs9k.Seek(startIndex9k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHOMB_MEN_SIZE + (month - 1) * Constants.OPS5000_CHOMB_MONTH_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                                    // Read data 9k
                                    fs9k.Read(bytes, 0, 128);

                                    // Write data into 5k
                                    fs5k.Write(bytes, 0, 128);
                                }

                                // Declare size cmtd
                                int cmtdSize9k = 24576;// 192 * 128
                                int cmtdSize5k = 127872;// 999 * 128

                                // Read data cham
                                for (int cham = 1; cham <= 9; cham++)
                                {
                                    // Move cursor into 9k file
                                    startIndex9k = (menOps9000 - 1) * Constants.OPS9000_CHOMB_MEN_SIZE + (month - 1) * Constants.OPS9000_CHOMB_MONTH_SIZE + cmtdSize9k + (cham - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs9k.Seek(startIndex9k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHOMB_MEN_SIZE + (month - 1) * Constants.OPS5000_CHOMB_MONTH_SIZE + cmtdSize5k + (cham - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                                    // Read data 9k
                                    fs9k.Read(bytes, 0, 128);

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
