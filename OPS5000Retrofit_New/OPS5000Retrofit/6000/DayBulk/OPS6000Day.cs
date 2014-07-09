//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000Day.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000Day file
// Creator       ：CongNC
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
    /// Day Structure
    /// </summary>
    public class OPS6000Day
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000Day()
        {

        }
        public List<OPS6000_T_HSTDB_HISTORY> ReadDataPart(int menNo, int partNo, string inputFilename)
        {
            List<OPS6000_T_HSTDB_HISTORY> historyList = new List<OPS6000_T_HSTDB_HISTORY>();

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                int partSize = Constants.OPS6000_CNF_HIST_N * 80;
                int menSize = partSize * Constants.OPS6000_TOTAL_PART_IN_MEN_DAY;
                int startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + Constants.OPS6000_DAY_DATA_PART_START_INDEX;

                fs.Seek(startIndex, SeekOrigin.Begin);
                byte[] partData = new byte[partSize];
                fs.Read(partData, 0, partSize);

                for (int i = 0; i < Constants.OPS6000_CNF_HIST_N; i++)
                {
                    OPS6000_T_HSTDB_HISTORY dayAmount = new OPS6000_T_HSTDB_HISTORY();
                    for (int j = 0; j < 5; j++)
                    {
                        OPS6000_T_HSTDB_DAT amount = new OPS6000_T_HSTDB_DAT();
                        amount.sts_s = BitConverter.ToInt16(partData, i * 80 + j * 16);
                        amount.yobi_s = BitConverter.ToInt16(partData, i * 80 + j * 16 + 2);
                        amount.inf_s = GetAddictionInfo(partData, i * 80 + j * 16 + 4);
                        amount.dat_d = BitConverter.ToDouble(partData, i * 80 + j * 16 + 8);
                        dayAmount.data_lst.Add(amount);
                    }
                    historyList.Add(dayAmount);
                }
            }
            return historyList;
        }
        public Dictionary<String, String> CreateMenMappingTable(Dictionary<string, string> OPS6kMapping, Dictionary<string, string> OPS5kMapping)
        {
            Dictionary<String, String> menMapping = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> entry in OPS6kMapping)
            {
                if (OPS5kMapping.ContainsKey(entry.Key))
                {
                    menMapping.Add(entry.Value, OPS5kMapping[entry.Key]);
                }
            }
            return menMapping;
        }
        public Dictionary<string, string> GetAllMenNo(OPS6000_T_HSTDB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS6000_T_HSTDB_IDX index in dataIndex.indx)
            {
                string yearMonthKey = Utility.CreateYearMonthKey(index.yea_s, index.mon_s);
                targetMenList.Add(yearMonthKey, index.men_s.ToString());
            }

            return targetMenList;
        }

        public Dictionary<string, string> GetMenNoByTime(DateTime startDateTime, DateTime endDateTime, OPS6000_T_HSTDB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS6000_T_HSTDB_IDX index in dataIndex.indx)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, 1);
                if (Utility.InRange(indexDateTime, startDateTime, endDateTime))
                {
                    targetMenList.Add(Utility.CreateYearMonthKey(index.yea_s, index.mon_s), index.men_s.ToString());
                }
            }

            return targetMenList;
        }

        public short GetMenOfStartMonth(short Year, short Month, OPS6000_T_HSTDB_HED dataIndex)
        {
            short targetMen = -1;
            foreach (OPS6000_T_HSTDB_IDX index in dataIndex.indx)
            {
                if (index.yea_s == Year && index.mon_s == Month)
                {
                    targetMen = index.prt_s;
                }
            }

            return targetMen;
        }
        public short GetMenOfEndMonth(short Year, short Month, OPS6000_T_HSTDB_HED dataIndex)
        {
            short targetMen = -1;
            foreach (OPS6000_T_HSTDB_IDX index in dataIndex.indx)
            {
                if (index.yea_s == Year && index.mon_s == Month)
                {
                    targetMen = index.prt_s;
                }
            }

            return targetMen;
        }

        public OPS6000_T_HSTDB_HED GetIndexPart(string inputFilename)
        {
            List<OPS6000_T_HSTDB_IDX> indexDataList = new List<OPS6000_T_HSTDB_IDX>();
            short[] yobi = new short[47];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(512, SeekOrigin.Begin);

                byte[] b = new byte[512];
                fs.Read(b, 0, 512);

                OPS6000_T_HSTDB_HED indexPart = new OPS6000_T_HSTDB_HED();
                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);

                for (int i = 1; i <= Constants.OPS6000_S_HSTDB_MAXMEN_P; i++)
                {
                    OPS6000_T_HSTDB_IDX idx = new OPS6000_T_HSTDB_IDX();
                    idx.yea_s = BitConverter.ToInt16(b, i * 16);
                    idx.mon_s = BitConverter.ToInt16(b, i * 16 + 2);
                    idx.lst_s = BitConverter.ToInt16(b, i * 16 + 4);
                    idx.yobi01_s = BitConverter.ToInt16(b, i * 16 + 6);
                    idx.men_s = BitConverter.ToInt16(b, i * 16 + 8);
                    idx.prt_s = BitConverter.ToInt16(b, i * 16 + 10);
                    idx.yobi02_s = Utility.GetShortArray4(b, i * 16 + 12);

                    indexDataList.Add(idx);
                }

                byte[] dailyByte = new byte[2];
                fs.Seek(928, SeekOrigin.Begin);
                fs.Read(dailyByte, 0, 2);

                // Read yobi
                byte[] yobiByte = new byte[94];
                fs.Seek(930, SeekOrigin.Begin);
                fs.Read(yobiByte, 0, 94);

                for (int i = 0; i < 47; i++)
                {
                    yobi[i] = BitConverter.ToInt16(yobiByte, i * 2);
                }

                indexPart.indx = indexDataList;
                indexPart.chgday_s = BitConverter.ToInt16(dailyByte, 0); ;
                indexPart.yobi02_s = yobi;

                return indexPart;
            }
        }


        private short[] GetAddictionInfo(byte[] arr, int startIdx)
        {
            short[] ret = new short[2];

            ret[0] = BitConverter.ToInt16(arr, startIdx);
            ret[1] = BitConverter.ToInt16(arr, startIdx + 2);
            return ret;
        }

        /// <summary>
        /// Convert to OPS5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="fromDay"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="toDay"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        public void ConvertToOPS5K(bool moveAll,
                                    short fromYear,
                                    short fromMonth,
                                    short fromDay,
                                    short toYear,
                                    short toMonth,
                                    short toDay,
                                    string inputFolderPath,
                                    string ouputFolder)
        {
            OPS5000Day ops5k = new OPS5000Day();
            OPS6000Day ops6k = new OPS6000Day();

            string dayDataFile6K = inputFolderPath + @"\HSTDB";
            string dayDataFile5K = ouputFolder + @"\number\HSTDB";
            string ops6KBDKesoFilePath = inputFolderPath + @"\OPSBDkeso";
            string ops5KBDKesoFilePath = ouputFolder + @"\load\OPSBDkeso";
            string ops5KDDtptFilePath = ouputFolder + @"\load\OPSDDtpt";

            OPS6000_T_HSTDB_HED indexPart6k = ops6k.GetIndexPart(dayDataFile6K);
            OPS5000_T_HSTDB_HED indexPart5k = ops5k.GetIndexPart(dayDataFile5K);

            // Create history mapping table
            Dictionary<string, string> ops6kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable6K(ops6KBDKesoFilePath);
            Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
            Dictionary<string, string> historyMapping = OPSBDCommon.CreateHistoryMappingTable6K_5K(ops6kMapping, ops5kMapping);

            if (moveAll)
            {
                Dictionary<string, string> ops6KTargetMenList = ops6k.GetAllMenNo(indexPart6k);
                Dictionary<string, string> ops5KTargetMenList = ops5k.GetAllMenNo(indexPart5k);
                Dictionary<string, string> menNoMapping = ops6k.CreateMenMappingTable(ops6KTargetMenList, ops5KTargetMenList);

                foreach (KeyValuePair<string, string> entry in menNoMapping)
                {
                    // Convert target menNo
                    short menNo = Int16.Parse(entry.Key);

                    // Determine fromPartNo and toPartNo 
                    short fromPartNo = 1;
                    short toPartNo = OPSCommon.GetLastPartNoInDayData(menNo, indexPart6k);

                    // Execute convert
                    for (int i = fromPartNo; i <= toPartNo; i++)
                    {
                        // Get part data on OPS6k
                        List<OPS6000_T_HSTDB_HISTORY> partData6k = ops6k.ReadDataPart(menNo, i, dayDataFile6K);

                        // Write to OPS5k file
                        ops5k.WriteDayData(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData6k);
                    }
                }
            }
            else
            {
                // Convert to datetime
                DateTime startDateTime = new DateTime(fromYear, fromMonth, 1, 0, 0, 0);
                DateTime endDateTime = new DateTime(toYear, toMonth, 1, 0, 0, 0);

                // Create men no mapping table
                Dictionary<string, string> ops6kTargetMenList = ops6k.GetMenNoByTime(startDateTime, endDateTime, indexPart6k);
                Dictionary<string, string> ops5kTargetMenList = ops5k.GetMenNoListByTime(startDateTime, endDateTime, indexPart5k);
                Dictionary<string, string> menMapping = ops6k.CreateMenMappingTable(ops6kTargetMenList, ops5kTargetMenList);

                // Execute convert
                foreach (KeyValuePair<string, string> entry in menMapping)
                {
                    // Convert target menNo
                    short menNo = Int16.Parse(entry.Key);

                    // Determine fromPartNo and toPartNo 
                    short fromPartNo = 1;
                    short toPartNo = OPSCommon.GetLastPartNoInDayData(menNo, indexPart6k);

                    if (menNo == ops6k.GetMenOfStartMonth(fromYear, fromMonth, indexPart6k))
                    {
                        // Get start hour 
                        fromPartNo = toDay;

                        // If menNo equals menNo of end month, loop to end date
                        if (menNo == ops6k.GetMenOfEndMonth(toYear, toMonth, indexPart6k))
                        {
                            // If input hour smaller than last part no
                            if (toDay < toPartNo)
                            {
                                toPartNo = toDay;
                            }

                            // Execute convert
                            for (int i = fromPartNo; i <= toPartNo; i++)
                            {
                                // Get part data on OPS6k
                                List<OPS6000_T_HSTDB_HISTORY> partData6k = ops6k.ReadDataPart(menNo, i, dayDataFile6K);

                                // Write to OPS5k file
                                ops5k.WriteDayData(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData6k);
                            }
                        }
                        else
                        {
                            // Execute convert
                            for (int i = fromPartNo; i <= toPartNo; i++)
                            {
                                // Get part data on OPS6k
                                List<OPS6000_T_HSTDB_HISTORY> partData6k = ops6k.ReadDataPart(menNo, i, dayDataFile6K);

                                // Write to OPS5k file
                                ops5k.WriteDayData(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData6k);
                            }
                        }
                    }
                    else if (menNo != ops6k.GetMenOfStartMonth(fromYear, fromMonth, indexPart6k))
                    {
                        if (menNo == ops6k.GetMenOfEndMonth(toYear, toMonth, indexPart6k))
                        {
                            // If input hour smaller than last part no
                            if (toDay < toPartNo)
                            {
                                toPartNo = toDay;
                            }

                            // Execute convert
                            for (int i = fromPartNo; i <= toPartNo; i++)
                            {
                                // Get part data on OPS6k
                                List<OPS6000_T_HSTDB_HISTORY> partData6k = ops6k.ReadDataPart(menNo, i, dayDataFile6K);

                                // Write to OPS5k file
                                ops5k.WriteDayData(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData6k);
                            }
                        }
                        else
                        {
                            // Execute convert
                            for (int i = fromPartNo; i <= toPartNo; i++)
                            {
                                // Get part data on OPS6k
                                List<OPS6000_T_HSTDB_HISTORY> partData6k = ops6k.ReadDataPart(menNo, i, dayDataFile6K);

                                // Write to OPS5k file
                                ops5k.WriteDayData(Int16.Parse(entry.Value), i, historyMapping, dayDataFile5K, partData6k);
                            }
                        }
                    }
                }
            }
        }

    }
}
