//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000DAY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using OPS5000Retrofit.Common;

namespace OPS5000Retrofit
{
    /// <summary>
    /// OPS9000Day Structure
    /// </summary>
    public class OPS9000Day
    {
        /// <summary>
        /// OPS9000Day
        /// </summary>
        public OPS9000Day()
        {

        }

        /// <summary>
        /// Convert to OPS5K
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="fromDay"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="toDay"></param>
        /// <param name="ops9KBDKesoFilePath"></param>
        /// <param name="ops9KBDtptFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="dayDataFile9K"></param>
        /// <param name="dayDataFile5K"></param>
        public void ConvertToOPS5K(bool moveAll,
                                    short fromYear,
                                    short fromMonth,
                                    short fromDay,
                                    short toYear,
                                    short toMonth,
                                    short toDay,
                                    string ops9KBDKesoFilePath,
                                    string ops9KBDtptFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string dayDataFile9K,
                                    string dayDataFile5K)
        {
            OPS5000Day ops5k = new OPS5000Day();
            OPS9000Day ops9k = new OPS9000Day();

            OPS5000_T_HSTDB_HED indexPart5k = ops5k.GetIndexPart(dayDataFile5K);
            OPS9000_T_HSTDB_HED indexPart9k = ops9k.GetIndexPart(dayDataFile9K);

            // Create history mapping table
            Dictionary<string, string> ops9kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable9K(ops9KBDKesoFilePath, ops9KBDtptFilePath);
            Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
            Dictionary<string, string> historyMapping = OPSBDCommon.CreateHistoryMappingTable9K_5K(ops9kMapping, ops5kMapping);


            if (moveAll)
            {
                // Create men no mapping table
                Dictionary<string, string> ops9kTargetMenList = ops9k.GetAllMenNo(indexPart9k);
                Dictionary<string, string> ops5kTargetMenList = ops5k.GetAllMenNo(indexPart5k);

                Dictionary<string, string> menMapping = OPSBDCommon.CreateMenMappingTable(ops9kTargetMenList, ops5kTargetMenList);

                // Execute convert
                foreach (KeyValuePair<string, string> entry in menMapping)
                {
                    // Convert target menNo
                    short menNo = Int16.Parse(entry.Key);

                    // Determine fromPartNo and toPartNo 
                    short fromPartNo = 1;
                    //short toPartNo = Retrofit5000Common.GetLastPartNoInDayData(menNo, indexPart9k);
                    short toPartNo = OPS9000Day.GetLastPartNoInDayData(menNo, indexPart9k);

                    // Execute convert
                    for (int i = fromPartNo; i <= toPartNo; i++)
                    {
                        // Get part data on OPS9k
                        List<OPS9000_T_HSTDB_HISTORY> partData9k = ops9k.ReadDataPart(menNo, i, dayDataFile9K);

                        // Write to OPS5k file
                        ops5k.WriteDayData9K(Int16.Parse(menMapping[menNo.ToString()]), i, historyMapping, dayDataFile5K, partData9k);

                    }
                }

            }
            else
            {
                // Get start time
                short startYear = fromYear;
                short startMonth = fromMonth;
                short startDay = fromDay;

                // Get end time
                short endYear = toYear;
                short endMonth = toMonth;
                short endDay = toDay;

                // Convert to datetime
                DateTime startDateTime = new DateTime(startYear, startMonth, 1, 0, 0, 0);
                DateTime endDateTime = new DateTime(endYear, endMonth, 1, 0, 0, 0);

                // Create men no mapping table
                Dictionary<string, string> ops9kTargetMenList = ops9k.GetMenNoByTime(startDateTime, endDateTime, indexPart9k);
                Dictionary<string, string> ops5kTargetMenList = ops5k.GetMenNoListByTime(startDateTime, endDateTime, indexPart5k);
                Dictionary<string, string> menMapping = OPSBDCommon.CreateMenMappingTable(ops9kTargetMenList, ops5kTargetMenList);

                // Execute convert
                foreach (KeyValuePair<string, string> entry in menMapping)
                {
                    // Convert target men no
                    short menNo = Int16.Parse(entry.Key);

                    // Determine fromPartNo and toPartNo 
                    short fromPartNo = 1;
                    short toPartNo = OPS9000Day.GetLastPartNoInDayData(menNo, indexPart9k);

                    // If menNo equals menNo of start date, loop from start day 
                    if (menNo == ops9k.GetMenOfStartMonth(startYear, startMonth, indexPart9k))
                    {
                        // If menNo equals menNo of end date, loop to end day
                        if (menNo == ops9k.GetMenOfEndMonth(endYear, endMonth, indexPart9k))
                        {
                            // Get start day 
                            fromPartNo = fromDay;

                            // If input day smaller than last part no
                            if (toDay < toPartNo)
                            {
                                toPartNo = toDay;
                            }

                            // Execute convert
                            for (int i = fromPartNo; i <= toPartNo; i++)
                            {
                                // Get part data on OPS9k
                                List<OPS9000_T_HSTDB_HISTORY> partData9k = ops9k.ReadDataPart(menNo, i, dayDataFile9K);

                                // Write to OPS5k file
                                ops5k.WriteDayData9K(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData9k);
                            }
                        }
                        else
                        {
                            // Get start day
                            fromPartNo = fromDay;

                            // Execute convert
                            for (int i = fromPartNo; i <= toPartNo; i++)
                            {
                                // Get part data on OPS9k
                                List<OPS9000_T_HSTDB_HISTORY> partData9k = ops9k.ReadDataPart(menNo, i, dayDataFile9K);

                                // Write to OPS5k file
                                ops5k.WriteDayData9K(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData9k);
                            }
                        }
                    }
                    else if (menNo != ops9k.GetMenOfStartMonth(startYear, startMonth, indexPart9k))
                    {
                        // If menNo equals menNo of end date, loop to end day
                        if (menNo == ops9k.GetMenOfEndMonth(endYear, endMonth, indexPart9k))
                        {
                            // If input day smaller than last part no
                            if (toDay < toPartNo)
                            {
                                toPartNo = toDay;
                            }

                            // Execute convert
                            for (int i = fromPartNo; i <= toPartNo; i++)
                            {
                                // Get part data on OPS6k
                                List<OPS9000_T_HSTDB_HISTORY> partData9k = ops9k.ReadDataPart(menNo, i, dayDataFile9K);

                                // Write to OPS5k file
                                ops5k.WriteDayData9K(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData9k);
                            }
                        }
                        else
                        {
                            // Execute convert
                            for (int i = fromPartNo; i <= toPartNo; i++)
                            {
                                // Get part data on OPS6k
                                List<OPS9000_T_HSTDB_HISTORY> partData9k = ops9k.ReadDataPart(menNo, i, dayDataFile9K);

                                // Write to OPS5k file
                                ops5k.WriteDayData9K(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData9k);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get all Men No for OPS9000
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetAllMenNo(OPS9000_T_HSTDB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS9000_T_HSTDB_IDX index in dataIndex.indx)
            {
                string yearMonthKey = Utility.CreateYearMonthKey(index.yea_s, index.mon_s);
                targetMenList.Add(yearMonthKey, index.men_s.ToString());
            }

            return targetMenList;
        }

        /// <summary>
        /// Get Men No by Time
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetMenNoByTime(DateTime startDateTime, DateTime endDateTime, OPS9000_T_HSTDB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS9000_T_HSTDB_IDX index in dataIndex.indx)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, 1);
                if (Utility.InRange(indexDateTime, startDateTime, endDateTime))
                {
                    targetMenList.Add(Utility.CreateYearMonthKey(index.yea_s, index.mon_s), index.men_s.ToString());
                }
            }

            return targetMenList;
        }

        /// <summary>
        /// Get index part
        /// </summary>
        /// <param name="inputFilename"></param>
        /// <returns></returns>
        public OPS9000_T_HSTDB_HED GetIndexPart(string inputFilename)
        {
            List<OPS9000_T_HSTDB_IDX> indexDataList = new List<OPS9000_T_HSTDB_IDX>();
            short[] yobi = new short[47];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(512, SeekOrigin.Begin);

                byte[] b = new byte[512];
                fs.Read(b, 0, 512);
                OPS9000_T_HSTDB_HED indexPart = new OPS9000_T_HSTDB_HED();

                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);

                for (int i = 1; i <= Constants.OPS9000_S_HSTDB_MAXMEN_P; i++)
                {
                    OPS9000_T_HSTDB_IDX idx = new OPS9000_T_HSTDB_IDX();
                    idx.yea_s = BitConverter.ToInt16(b, i * 16);
                    idx.mon_s = BitConverter.ToInt16(b, i * 16 + 2);
                    idx.lst_s = BitConverter.ToInt16(b, i * 16 + 4);
                    idx.yobi01_s = BitConverter.ToInt16(b, i * 16 + 6);
                    idx.men_s = BitConverter.ToInt16(b, i * 16 + 8);
                    idx.prt_s = BitConverter.ToInt16(b, i * 16 + 10);
                    idx.yobi02_s = Utility.GetShortArray4(b, i * 16 + 12);

                    indexDataList.Add(idx);
                }
                indexPart.indx = indexDataList;

                byte[] dailyByte = new byte[2];
                dailyByte[0] = b[416];
                dailyByte[1] = b[417];
                // Read daily time
                indexPart.MonthlyTime = BitConverter.ToInt16(dailyByte, 0);

                // Read yobi
                byte[] yobiByte = new byte[94];

                for (int i = 0; i < 47; i++)
                {
                    yobi[i] = BitConverter.ToInt16(b, 418 + i * 2);
                }
                indexPart.yobi02_s = yobi;

                return indexPart;
            }
        }

        /// <summary>
        /// Get last part no in day
        /// </summary>
        /// <param name="menNo"></param>
        /// <param name="indexData"></param>
        /// <returns></returns>
        public static short GetLastPartNoInDayData(short menNo, OPS9000_T_HSTDB_HED indexData)
        {
            short partNo = -1;
            foreach (OPS9000_T_HSTDB_IDX index in indexData.indx)
            {
                if (menNo == index.men_s)
                {
                    partNo = index.prt_s;
                }
            }
            return partNo;
        }

        /// <summary>
        /// Read Data Part
        /// </summary>
        /// <param name="menNo"></param>
        /// <param name="partNo"></param>
        /// <param name="inputFilename"></param>
        /// <returns></returns>
        public List<OPS9000_T_HSTDB_HISTORY> ReadDataPart(int menNo, int partNo, string inputFilename)
        {
            List<OPS9000_T_HSTDB_HISTORY> historyList = new List<OPS9000_T_HSTDB_HISTORY>();

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                int partSize = Constants.OPS9000_CNF_HIST_N * 80;
                int menSize = partSize * Constants.OPS9000_TOTAL_PART_IN_MEN_DAY;
                int startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + Constants.OPS9000_DAY_DATA_PART_START_INDEX;

                fs.Seek(startIndex, SeekOrigin.Begin);
                byte[] partData = new byte[partSize];
                fs.Read(partData, 0, partSize);

                for (int i = 0; i < Constants.OPS9000_CNF_HIST_N; i++)
                {
                    OPS9000_T_HSTDB_HISTORY dayAmount = new OPS9000_T_HSTDB_HISTORY();
                    for (int j = 0; j < 5; j++)
                    {
                        OPS9000_T_HSTDB_DAT amount = new OPS9000_T_HSTDB_DAT();
                        amount.sts_s = BitConverter.ToInt16(partData, i * 80 + j * 16);
                        amount.yobi_s = BitConverter.ToInt16(partData, i * 80 + j * 16 + 2);
                        amount.inf_s = GetAddictionInfo(partData, i * 80 + j * 16 + 4);
                        amount.dat_d = BitConverter.ToDouble(partData, i * 80 + j * 16 + 8);
                        dayAmount.dat_lst.Add(amount);
                    }
                    historyList.Add(dayAmount);
                }
            }
            return historyList;
        }

        /// <summary>
        /// Get men by start month
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public short GetMenOfStartMonth(short Year, short Month, OPS9000_T_HSTDB_HED dataIndex)
        {
            short targetMen = -1;
            foreach (OPS9000_T_HSTDB_IDX index in dataIndex.indx)
            {
                if (index.yea_s == Year && index.mon_s == Month)
                {
                    targetMen = index.prt_s;
                }
            }

            return targetMen;
        }

        /// <summary>
        /// Get men by end month
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public short GetMenOfEndMonth(short Year, short Month, OPS9000_T_HSTDB_HED dataIndex)
        {
            short targetMen = -1;
            foreach (OPS9000_T_HSTDB_IDX index in dataIndex.indx)
            {
                if (index.yea_s == Year && index.mon_s == Month)
                {
                    targetMen = index.prt_s;
                }
            }

            return targetMen;
        }

        /// <summary>
        /// Get info from Array to short
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="startIdx"></param>
        /// <returns></returns>
        private short[] GetAddictionInfo(byte[] arr, int startIdx)
        {
            short[] ret = new short[2];

            ret[0] = BitConverter.ToInt16(arr, startIdx);
            ret[1] = BitConverter.ToInt16(arr, startIdx + 2);
            return ret;
        }

        /// <summary>
        /// Convert byte type to array type
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private byte[] ConvertToByteArray(OPS9000_T_HSTDB_HISTORY amount)
        {
            byte[] b = new byte[80];
            byte[] b1 = new byte[16];
            byte[] b2 = new byte[16];
            byte[] b3 = new byte[16];
            byte[] b4 = new byte[16];
            byte[] b5 = new byte[16];

            b1 = ConvertToByteArrayUnit(amount.dat_lst[0]);
            b2 = ConvertToByteArrayUnit(amount.dat_lst[1]);
            b3 = ConvertToByteArrayUnit(amount.dat_lst[2]);
            b4 = ConvertToByteArrayUnit(amount.dat_lst[3]);
            b5 = ConvertToByteArrayUnit(amount.dat_lst[4]);

            for (int i = 0; i < 16; i++)
            {
                b[i] = b1[i];
            }

            for (int i = 16; i < 32; i++)
            {
                b[i] = b2[i - 16];
            }

            for (int i = 32; i < 48; i++)
            {
                b[i] = b3[i - 32];
            }

            for (int i = 48; i < 64; i++)
            {
                b[i] = b4[i - 48];
            }
            for (int i = 64; i < 80; i++)
            {
                b[i] = b5[i - 64];
            }
            return b;
        }

        /// <summary>
        /// Convert to byte array unit
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private byte[] ConvertToByteArrayUnit(OPS9000_T_HSTDB_DAT amount)
        {
            byte[] b = new byte[16];

            byte[] status = BitConverter.GetBytes(amount.sts_s);
            byte[] yobi_s = BitConverter.GetBytes(amount.yobi_s);
            byte[] hour = BitConverter.GetBytes(amount.inf_s[0]);
            byte[] minute = BitConverter.GetBytes(amount.inf_s[1]);
            byte[] data = BitConverter.GetBytes(amount.dat_d);

            b[0] = status[0];
            b[1] = status[1];
            b[2] = yobi_s[0];
            b[3] = yobi_s[1];
            b[4] = hour[0];
            b[5] = hour[1];
            b[6] = minute[0];
            b[7] = minute[1];
            b[8] = data[0];
            b[9] = data[1];
            b[10] = data[2];
            b[11] = data[3];
            b[12] = data[4];
            b[13] = data[5];
            b[14] = data[6];
            b[15] = data[7];
            return b;
        }
    }
}
