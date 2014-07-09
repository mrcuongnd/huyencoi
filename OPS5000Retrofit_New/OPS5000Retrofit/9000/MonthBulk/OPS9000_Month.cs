//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_Month.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using OPS5000Retrofit.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Month Structure
    /// </summary>
    class OPS9000_Month
    {
        /// <summary>
        /// Struct index
        /// </summary>
        public OPS9000_T_HSTMB_HED indexData { get; set; }

        /// <summary>
        /// Struct data
        /// </summary>
        public OPS9000_T_HSTMB_MEN[] menData { get; set; }

        /// <summary>
        /// Init variable
        /// </summary>
        public OPS9000_Month()
        {
            indexData = new OPS9000_T_HSTMB_HED();
            menData = new OPS9000_T_HSTMB_MEN[6];
            for (int i = 0; i < 6; i++)
            {
                menData[i] = new OPS9000_T_HSTMB_MEN();
            }

        }

        /// <summary>
        /// Read data file into struct
        /// </summary>
        /// <param name="ops9KHSTMFilePath"></param>
        public void ReadData(string ops9KHSTMFilePath)
        {
            using (FileStream fs = new FileStream(ops9KHSTMFilePath, FileMode.Open, FileAccess.Read))
            {
                // Move cursor to first index in index data
                fs.Seek(512, SeekOrigin.Begin);

                // Init byte array. 101376256 total size including data and index
                byte[] indexHead = new byte[55738880];

                // Read bytes
                fs.Read(indexHead, 0, 55738880);

                // Read data index
                indexData.yea_s = BitConverter.ToInt16(indexHead, 0);
                indexData.mon_s = BitConverter.ToInt16(indexHead, 2);
                indexData.day_s = BitConverter.ToInt16(indexHead, 4);
                indexData.cal_s = BitConverter.ToInt16(indexHead, 6);
                indexData.hor_s = BitConverter.ToInt16(indexHead, 8);
                indexData.yobi01_s[0] = BitConverter.ToInt16(indexHead, 10);
                indexData.yobi01_s[1] = BitConverter.ToInt16(indexHead, 12);
                indexData.yobi01_s[2] = BitConverter.ToInt16(indexHead, 14);

                for (int i = 0; i < 6; i++)
                {
                    indexData.indx[i].yea_s = BitConverter.ToInt16(indexHead, i * 16 + 16);
                    indexData.indx[i].mon_s = BitConverter.ToInt16(indexHead, i * 16 + 18);
                    indexData.indx[i].yobi01_s[0] = BitConverter.ToInt16(indexHead, i * 16 + 20);
                    indexData.indx[i].yobi01_s[1] = BitConverter.ToInt16(indexHead, i * 16 + 22);
                    indexData.indx[i].men_s = BitConverter.ToInt16(indexHead, i * 16 + 24); ;
                    indexData.indx[i].prt_s = BitConverter.ToInt16(indexHead, i * 16 + 26); ;
                    indexData.indx[i].yobi02_s[0] = BitConverter.ToInt16(indexHead, i * 16 + 28);
                    indexData.indx[i].yobi02_s[1] = BitConverter.ToInt16(indexHead, i * 16 + 30);
                }

                indexData.chgmon_s = BitConverter.ToInt16(indexHead, 112);

                for (int i = 0; i < 199; i++)
                {
                    indexData.yobi02_s[i] = BitConverter.ToInt16(indexHead, i * 2 + 114);
                }

                // Read data men
                for (int men = 0; men < 6; men++)
                {
                    for (int part = 0; part < 12; part++)
                    {
                        for (int history = 0; history < 6048; history++)
                        {
                            for (int dat = 0; dat < 8; dat++)
                            {
                                menData[men].part[part].history[history].dat[dat].sts_s = BitConverter.ToInt16(indexHead, 512 + men * 9289728 + part * 774144 + history * 128 + dat * 16);
                                menData[men].part[part].history[history].dat[dat].yobi_s = BitConverter.ToInt16(indexHead, 514 + men * 9289728 + part * 774144 + history * 128 + dat * 16);
                                menData[men].part[part].history[history].dat[dat].inf_s[0] = BitConverter.ToInt16(indexHead, 516 + men * 9289728 + part * 774144 + history * 128 + dat * 16);
                                menData[men].part[part].history[history].dat[dat].inf_s[1] = BitConverter.ToInt16(indexHead, 518 + men * 9289728 + part * 774144 + history * 128 + dat * 16);
                                menData[men].part[part].history[history].dat[dat].dat_d = BitConverter.ToDouble(indexHead, 520 + men * 9289728 + part * 774144 + history * 128 + dat * 16);
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Convert data to OPS5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="ops9KBDKesoFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
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
                                    string ops5kHSTMBFilePath,
                                    string ops9KHSTMBFilePath)
        {
            // Read data OPS9000
            ReadData(ops9KHSTMBFilePath);

            // Create history mapping table
            Dictionary<string, string> ops9kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable9K(ops9KBDKesoFilePath, ops9KDDtptFilePath);
            Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
            Dictionary<string, string> historyMapping = OPSBDCommon.CreateHistoryMappingTable9K_5K(ops9kMapping, ops5kMapping);

            // Declare maping men
            Dictionary<short, short> menMapping = new Dictionary<short, short>();
            Dictionary<short, short> menPartMaping = new Dictionary<short, short>();

            // Declare OPS5000 and read data
            OPS5000_Month ops5000Month = new OPS5000_Month();
            ops5000Month.ReadData(ops5kHSTMBFilePath);

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
                        if (ops5000Month.indexData.indx[i].yea_s == indexData.indx[j].yea_s)
                        {
                            menMapping.Add(ops5000Month.indexData.indx[i].men_s, indexData.indx[j].men_s);
                            menPartMaping.Add(indexData.indx[j].men_s, indexData.indx[j].prt_s);
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
                        if (ops5000Month.indexData.indx[i].yea_s == indexData.indx[j].yea_s &&
                            indexData.indx[j].yea_s >= fromYear && indexData.indx[j].yea_s <= toYear)
                        {
                            menMapping.Add(ops5000Month.indexData.indx[i].men_s, indexData.indx[j].men_s);
                            menPartMaping.Add(indexData.indx[j].men_s, indexData.indx[j].prt_s);
                            if (indexData.indx[j].yea_s == fromYear && indexData.indx[j].mon_s == fromMonth)
                            {
                                part_PointStart = ops5000Month.indexData.indx[i].men_s;
                            }
                            if (indexData.indx[j].yea_s == toYear && indexData.indx[j].mon_s == toMonth)
                            {
                                part_PointEnd = ops5000Month.indexData.indx[i].men_s;
                            }
                        }
                    }
                }
            }

            int monthStart = 0;
            int monthEnd = 0;
            short monthLast = 0;
            short menOps9000 = 0;
            int historyId9000 = 0;
            int historyId5000 = 0;
            int partSize = 0;
            int menSize = 0;
            int startIndex = 0;

            // Convert data
            using (FileStream fs = new FileStream(ops5kHSTMBFilePath, FileMode.Open, FileAccess.Write))
            {
                partSize = Constants.OPS5000_CNF_HIST_N * 16 * 8;
                menSize = partSize * Constants.OPS5000_TOTAL_PART_IN_MEN_MONTH;
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
                            if (monthLast < monthEnd) 
                            {
                                monthEnd = monthLast;
                            }
                        }
                        for (int part = monthStart; part <= monthEnd; part++)
                        {
                            startIndex = (menItem.Key - 1) * menSize + (part - 1) * partSize + Constants.OPS5000_MONTH_DATA_PART_START_INDEX;

                            foreach (KeyValuePair<string, string> item in historyMapping)
                            {
                                historyId9000 = short.Parse(item.Key.ToString());
                                historyId5000 = short.Parse(item.Value.ToString());

                                for (int dat = 0; dat < 8; dat++)
                                {
                                    fs.Seek(startIndex + (historyId5000 - 1) * 128 + dat * 16, SeekOrigin.Begin);
                                    byte[] bytes = new byte[16];
                                    byte[] status = BitConverter.GetBytes(menData[menOps9000 - 1].part[part - 1].history[historyId9000 - 1].dat[dat].sts_s);
                                    byte[] yobi_s = BitConverter.GetBytes(menData[menOps9000 - 1].part[part - 1].history[historyId9000 - 1].dat[dat].yobi_s);
                                    byte[] inf_s1 = BitConverter.GetBytes(menData[menOps9000 - 1].part[part - 1].history[historyId9000 - 1].dat[dat].inf_s[0]);
                                    byte[] inf_s2 = BitConverter.GetBytes(menData[menOps9000 - 1].part[part - 1].history[historyId9000 - 1].dat[dat].inf_s[1]);
                                    byte[] data = BitConverter.GetBytes(menData[menOps9000 - 1].part[part - 1].history[historyId9000 - 1].dat[dat].dat_d);
                                    bytes[0] = status[0];
                                    bytes[1] = status[1];
                                    bytes[2] = yobi_s[0];
                                    bytes[3] = yobi_s[1];
                                    bytes[4] = inf_s1[0];
                                    bytes[5] = inf_s1[1];
                                    bytes[6] = inf_s2[0];
                                    bytes[7] = inf_s2[1];
                                    bytes[8] = data[0];
                                    bytes[9] = data[1];
                                    bytes[10] = data[2];
                                    bytes[11] = data[3];
                                    bytes[12] = data[4];
                                    bytes[13] = data[5];
                                    bytes[14] = data[6];
                                    bytes[15] = data[7];
                                    fs.Write(bytes, 0, 16);
                                }
                            }
                        }

                    }
                }
            }
        }


    }
}
