//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000Hour.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000Hour file
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
    /// Hour Structure
    /// </summary>
    public class OPS6000Hour
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000Hour()
        {

        }

        /// <summary>
        /// Convert hour data from ops6000 to ops5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="fromDate"></param>
        /// <param name="fromHour"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="toDate"></param>
        /// <param name="toHour"></param>
        /// <param name="ops6KBDKesoFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="ops5kHSTHBFilePath"></param>
        /// <param name="ops6KHSTHBFilePath"></param>
        public void ConvertToOPS5K(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int fromDate,
                                    int fromHour,
                                    int toYear,
                                    int toMonth,
                                    int toDate,
                                    int toHour,
                                    string ops6KBDKesoFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string ops5kHSTHBFilePath,
                                    string ops6KHSTHBFilePath)
        {
            OPS5000Hour ops5k = new OPS5000Hour();
            OPS6000_T_HSTHB_HED indexPart6k = OPSCommon.GetHourBulkIndex6K(ops6KHSTHBFilePath);
            OPS5000_T_HSTHB_HED indexPart5k = OPSCommon.GetHourBulkIndex5K(ops5kHSTHBFilePath);

            // Create history mapping table
            Dictionary<string, string> ops6kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable6K(ops6KBDKesoFilePath);
            Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
            Dictionary<string, string> historyMapping = OPSBDCommon.CreateHistoryMappingTable6K_5K(ops6kMapping, ops5kMapping);

            if (moveAll)
            {
                // Create men no mapping table
                Dictionary<string, string> ops6kTargetMenList = GetAllMenNo(indexPart6k);
                Dictionary<string, string> ops5kTargetMenList = ops5k.GetAllMenNo(indexPart5k);
                Dictionary<string, string> menMapping = CreateMenMappingTable(ops6kTargetMenList, ops5kTargetMenList);

                foreach (KeyValuePair<string, string> entry in menMapping)
                {
                    // Convert target menNo
                    short menNo = Int16.Parse(entry.Key);

                    // Determine fromPartNo and toPartNo 
                    short fromPartNo = 1;
                    short toPartNo = OPSCommon.GetLastPartNoInHourData(menNo, indexPart6k);

                    // Execute convert
                    for (int i = fromPartNo; i <= toPartNo; i++)
                    {
                        // Get part data on OPS6k
                        List<OPS6000_T_HSTHB_DAT> partData6k = ReadDataPart(menNo, i, ops6KHSTHBFilePath);

                        // Write to OPS5k file
                        ops5k.WriteHourData6K(Int16.Parse(menMapping[menNo.ToString()]), i, historyMapping, ops5kHSTHBFilePath, partData6k);
                    }
                }
            }
            else
            {
                // Convert to datetime
                DateTime startDateTime = new DateTime(fromYear, fromMonth, fromDate, 0, 0, 0);
                DateTime endDateTime = new DateTime(toYear, toMonth, toDate, 0, 0, 0);

                // Create men no mapping table
                Dictionary<string, string> ops6kTargetMenList = GetMenNoListByTime(startDateTime, endDateTime, indexPart6k);
                Dictionary<string, string> ops5kTargetMenList = ops5k.GetMenNoListByTime(startDateTime, endDateTime, indexPart5k);
                Dictionary<string, string> menMapping = CreateMenMappingTable(ops6kTargetMenList, ops5kTargetMenList);

                // Execute convert
                foreach (KeyValuePair<string, string> entry in menMapping)
                {
                    // Convert target menNo
                    short menNo = Int16.Parse(entry.Key);

                    // Determine fromPartNo and toPartNo 
                    short fromPartNo = 1;
                    short toPartNo = OPSCommon.GetLastPartNoInHourData(menNo, indexPart6k);

                    // If menNo equals menNo of start date, loop from start hour 
                    if (menNo == GetMenOfStartDate(startDateTime, indexPart6k))
                    {
                        // If menNo equals menNo of end date, loop to end hour
                        if (menNo == GetMenOfEndDate(endDateTime, indexPart6k))
                        {
                            // Get start hour 
                            fromPartNo = Int16.Parse(fromHour.ToString());

                            // If input hour smaller than last part no
                            if (Int16.Parse(toHour.ToString()) < toPartNo)
                            {
                                toPartNo = Int16.Parse(toHour.ToString());
                            }
                        }
                        else
                        {
                            // Get start hour
                            fromPartNo = Int16.Parse(fromHour.ToString());
                        }
                    }
                    else if (menNo != GetMenOfStartDate(startDateTime, indexPart6k))
                    {
                        // If menNo equals menNo of end date, loop to end hour
                        if (menNo == GetMenOfEndDate(endDateTime, indexPart6k))
                        {
                            // If input hour smaller than last part no
                            if (Int16.Parse(toHour.ToString()) < toPartNo)
                            {
                                toPartNo = Int16.Parse(toHour.ToString());
                            }
                        }
                    }

                    // Execute convert
                    for (int i = fromPartNo; i <= toPartNo; i++)
                    {
                        // Get part data on OPS6k
                        List<OPS6000_T_HSTHB_DAT> partData6k = ReadDataPart(menNo, i, ops6KHSTHBFilePath);

                        // Write to OPS5k file
                        ops5k.WriteHourData6K(Int16.Parse(entry.Value), i, historyMapping, ops5kHSTHBFilePath, partData6k);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OPS6kMapping"></param>
        /// <param name="OPS5kMapping"></param>
        /// <returns></returns>
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

        public List<OPS6000_T_HSTHB_DAT> ReadDataPart(int menNo, int partNo, string inputFilename)
        {
            List<OPS6000_T_HSTHB_DAT> historyList = new List<OPS6000_T_HSTHB_DAT>();
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                int partSize = Constants.OPS6000_CNF_HIST_N * 16;
                int menSize = partSize * Constants.OPS6000_TOTAL_PART_IN_MEN_HOUR;
                int startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + 2048;

                fs.Seek(startIndex, SeekOrigin.Begin);
                byte[] partData = new byte[partSize];
                fs.Read(partData, 0, partSize);


                for (int i = 0; i < Constants.OPS6000_CNF_HIST_N; i++)
                {
                    OPS6000_T_HSTHB_DAT amount = new OPS6000_T_HSTHB_DAT();
                    amount.sts_s = BitConverter.ToInt16(partData, i * 16);
                    amount.yobi_s = BitConverter.ToInt16(partData, i * 16 + 2);
                    amount.inf_s = GetAddictionInfo(partData, i * 16 + 4);
                    amount.dat_d = BitConverter.ToDouble(partData, i * 16 + 8);
                    historyList.Add(amount);
                }
            }
            return historyList;
        }

        public Dictionary<string, string> GetAllMenNo(OPS6000_T_HSTHB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS6000_T_HSTHB_IDX index in dataIndex.indx)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s);
                targetMenList.Add(indexDateTime.ToShortDateString(), index.men_s.ToString());
            }

            return targetMenList;
        }

        public Dictionary<string, string> GetMenNoListByTime(DateTime startDateTime, DateTime endDateTime, OPS6000_T_HSTHB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS6000_T_HSTHB_IDX index in dataIndex.indx)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s);
                if (Utility.InRange(indexDateTime, startDateTime, endDateTime))
                {
                    targetMenList.Add(indexDateTime.ToShortDateString(), index.men_s.ToString());
                }
            }

            return targetMenList;
        }
        public int GetMenOfStartDate(DateTime startDateTime, OPS6000_T_HSTHB_HED dataIndex)
        {
            int targetMen = -1;
            foreach (OPS6000_T_HSTHB_IDX index in dataIndex.indx)
            {
                if (DateTime.Compare(Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s), startDateTime) == 0)
                {
                    targetMen = index.men_s;
                }
            }

            return targetMen;
        }
        public short GetMenOfEndDate(DateTime endDateTime, OPS6000_T_HSTHB_HED dataIndex)
        {
            short targetMen = -1;
            foreach (OPS6000_T_HSTHB_IDX index in dataIndex.indx)
            {
                if (DateTime.Compare(Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s), endDateTime) == 0)
                {
                    targetMen = index.men_s;
                }
            }

            return targetMen;
        }
        public OPS6000_T_HSTHB_HED GetIndexPart(string inputFilename)
        {
            List<OPS6000_T_HSTHB_IDX> indexDataList = new List<OPS6000_T_HSTHB_IDX>();
            short dailyTime;
            short[] yobi = new short[255];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(512, SeekOrigin.Begin);

                byte[] b = new byte[1024];
                fs.Read(b, 0, 1024);

                for (int i = 1; i <= 63; i++)
                {
                    OPS6000_T_HSTHB_IDX idx = new OPS6000_T_HSTHB_IDX();
                    idx.yea_s = BitConverter.ToInt16(b, i * 16);
                    idx.mon_s = BitConverter.ToInt16(b, i * 16 + 2);
                    idx.day_s = BitConverter.ToInt16(b, i * 16 + 4);
                    idx.cal_s = BitConverter.ToInt16(b, i * 16 + 6);
                    idx.men_s = BitConverter.ToInt16(b, i * 16 + 8);
                    idx.prt_s = BitConverter.ToInt16(b, i * 16 + 10);
                    idx.yobi01_s = Utility.GetShortArray4(b, i * 16 + 12);

                    indexDataList.Add(idx);
                }

                byte[] dailyByte = new byte[2];
                fs.Seek(1536, SeekOrigin.Begin);
                fs.Read(dailyByte, 0, 2);
                // Read daily time
                dailyTime = BitConverter.ToInt16(dailyByte, 0);

                // Read yobi
                byte[] yobiByte = new byte[510];
                fs.Seek(1538, SeekOrigin.Begin);
                fs.Read(yobiByte, 0, 510);

                for (int i = 0; i < 255; i++)
                {
                    yobi[i] = BitConverter.ToInt16(yobiByte, i * 2);
                }

                OPS6000_T_HSTHB_HED indexPart = new OPS6000_T_HSTHB_HED();

                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);


                indexPart.indx = indexDataList;
                indexPart.chghor_s = dailyTime;
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

    }
}
