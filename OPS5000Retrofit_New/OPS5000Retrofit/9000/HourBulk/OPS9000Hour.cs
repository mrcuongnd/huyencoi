//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000Hour.cs
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
using System.Windows.Forms;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Hour Structure
    /// </summary>
    public class OPS9000Hour
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000Hour() { }

        /// <summary>
        /// Convert Hour 9K to 5K
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        public void ConvertToOPS5K(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int fromDate,
                                    int fromHour,
                                    int toYear,
                                    int toMonth,
                                    int toDate,
                                    int toHour,
                                    string ops9KBDKesoFilePath,
                                    string ops9KBDtptFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string hourDataFile9K,
                                    string hourDataFile5K)
        {

            try
            {
                OPS5000Hour ops5k = new OPS5000Hour();
                OPS9000Hour ops9k = new OPS9000Hour();

                OPS5000_T_HSTHB_HED indexPart5k = ops5k.GetIndexPart(hourDataFile5K);
                OPS9000_T_HSTHB_HED indexPart9k = ops9k.GetIndexPart(hourDataFile9K);

                // Create history mapping table
                Dictionary<string, string> ops9kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable9K(ops9KBDKesoFilePath, ops9KBDtptFilePath);
                Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
                Dictionary<string, string> historyMapping = OPSBDCommon.CreateHistoryMappingTable9K_5K(ops9kMapping, ops5kMapping);

                if (moveAll)
                {
                    // Create men no mapping table
                    Dictionary<string, string> ops9kTargetMenList = ops9k.GetAllMenNo(indexPart9k);
                    Dictionary<string, string> ops5kTargetMenList = ops5k.GetAllMenNo(indexPart5k);
                    Dictionary<string, string> menMapping = ops9k.CreateMenMappingTable(ops9kTargetMenList, ops5kTargetMenList);

                    // Execute convert                    
                    foreach (KeyValuePair<string, string> entry in menMapping)
                    {
                        // Convert target menNo
                        short menNo = Int16.Parse(entry.Key);

                        // Determine fromPartNo and toPartNo 
                        short fromPartNo = 1;
                        //short toPartNo = Retrofit5000Common.GetLastPartNoInHourData(menNo, indexPart9k);
                        short toPartNo = OPS9000Hour.GetLastPartNoInHourData(menNo, indexPart9k);

                        // Execute convert
                        for (int i = fromPartNo; i <= toPartNo; i++)
                        {
                            // Get part data on OPS9k
                            List<OPS9000_T_HSTHB_DAT> partData9k = ops9k.ReadDataPart(menNo, i, hourDataFile9K);

                            // Write to OPS5k file
                            ops5k.WriteHourData9K(Int16.Parse(menMapping[menNo.ToString()]), i, historyMapping, hourDataFile5K, partData9k);
                        }
                    }
                }
                else
                {
                    // Get start time
                    int startYear = fromYear;
                    int startMonth = fromMonth;
                    int startDay = fromDate;

                    // Get end time
                    int endYear = toYear;
                    int endMonth = toMonth;
                    int endDay = toDate;

                    // Convert to datetime
                    DateTime startDateTime = new DateTime(startYear, startMonth, startDay, 0, 0, 0);
                    DateTime endDateTime = new DateTime(endYear, endMonth, endDay, 0, 0, 0);

                    // Create men no mapping table
                    Dictionary<string, string> ops9kTargetMenList = ops9k.GetMenNoByTime(startDateTime, endDateTime, indexPart9k);
                    Dictionary<string, string> ops5kTargetMenList = ops5k.GetMenNoListByTime(startDateTime, endDateTime, indexPart5k);
                    Dictionary<string, string> menMapping = ops9k.CreateMenMappingTable(ops9kTargetMenList, ops5kTargetMenList);
                    // Execute convert
                    foreach (KeyValuePair<string, string> entry in menMapping)
                    {
                        // Convert target men no
                        short menNo = Int16.Parse(entry.Key);

                        // Determine fromPartNo and toPartNo 
                        short fromPartNo = 1;
                        short toPartNo = OPS9000Hour.GetLastPartNoInHourData(menNo, indexPart9k);

                        // If menNo equals menNo of start date, loop from start hour 
                        if (menNo == ops9k.GetMenOfStartDate(startDateTime, indexPart9k))
                        {
                            // If menNo equals menNo of end date, loop to end hour
                            if (menNo == ops9k.GetMenOfEndDate(endDateTime, indexPart9k))
                            {
                                // Get start hour 
                                fromPartNo = Int16.Parse(fromHour.ToString());

                                // If input hour smaller than last part no
                                if (Int16.Parse(toHour.ToString()) < toPartNo)
                                {
                                    toPartNo = Int16.Parse(toHour.ToString());
                                }

                                // Execute convert
                                for (int i = fromPartNo; i <= toPartNo; i++)
                                {
                                    // Get part data on OPS9k
                                    List<OPS9000_T_HSTHB_DAT> partData9k = ops9k.ReadDataPart(menNo, i, hourDataFile9K);

                                    // Write to OPS5k file
                                    ops5k.WriteHourData9K(Int16.Parse(entry.Value), i, historyMapping, hourDataFile5K, partData9k);
                                }
                            }
                            else
                            {
                                // Get start hour
                                fromPartNo = Int16.Parse(fromHour.ToString());

                                // Execute convert
                                for (int i = fromPartNo; i <= toPartNo; i++)
                                {
                                    // Get part data on OPS9k
                                    List<OPS9000_T_HSTHB_DAT> partData9k = ops9k.ReadDataPart(menNo, i, hourDataFile9K);

                                    // Write to OPS5k file
                                    ops5k.WriteHourData9K(Int16.Parse(entry.Value), i, historyMapping, hourDataFile5K, partData9k);
                                }
                            }
                        }
                        else if (menNo != ops9k.GetMenOfStartDate(startDateTime, indexPart9k))
                        {
                            // If menNo equals menNo of end date, loop to end hour
                            if (menNo == ops9k.GetMenOfEndDate(endDateTime, indexPart9k))
                            {
                                // If input hour smaller than last part no
                                if (Int16.Parse(toHour.ToString()) < toPartNo)
                                {
                                    toPartNo = Int16.Parse(toHour.ToString());
                                }

                                // Execute convert
                                for (int i = fromPartNo; i <= toPartNo; i++)
                                {
                                    // Get part data on OPS6k
                                    List<OPS9000_T_HSTHB_DAT> partData9k = ops9k.ReadDataPart(menNo, i, hourDataFile9K);

                                    // Write to OPS5k file
                                    ops5k.WriteHourData9K(Int16.Parse(entry.Value), i, historyMapping, hourDataFile5K, partData9k);
                                }
                            }
                            else
                            {
                                // Execute convert
                                for (int i = fromPartNo; i <= toPartNo; i++)
                                {
                                    // Get part data on OPS6k
                                    List<OPS9000_T_HSTHB_DAT> partData9k = ops9k.ReadDataPart(menNo, i, hourDataFile9K);

                                    // Write to OPS5k file
                                    ops5k.WriteHourData9K(Int16.Parse(entry.Value), i, historyMapping, hourDataFile5K, partData9k);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is IOException)
                {
                    string path = ((System.IO.FileNotFoundException)(ex)).FileName;
                    string filename = Path.GetFileName(path);
                    Logger.OutputErrorLog(Application.StartupPath, string.Format(Constants.FILE_OPEN_FAIL, filename));
                }
                else
                {
                    string path = Application.StartupPath;
                    string filename = Path.GetFileName(path);
                    Logger.OutputErrorLog(Application.StartupPath, string.Format(Constants.FILE_OPEN_FAIL, filename));
                }
            }
        }


        /// <summary>
        /// Create Mapping HistoryId and Tagno of OPS9000
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Dictionary<string, string> CreateHistoryId_TagNoMappingTable(string path)
        {
            Dictionary<string, string> OPS9kMapping = new Dictionary<string, string>();

            List<OPS9000_T_BD_KESO_KEIKI> OPSBDkesoData = new List<OPS9000_T_BD_KESO_KEIKI>();

            string OPSBDkesoFile = path + "OPSBDkeso";

            byte[] OPSBDkesoByteArr = File.ReadAllBytes(OPSBDkesoFile);

            int kekiCount = OPSBDkesoByteArr.Length / 108;

            for (int i = 0; i < kekiCount; i++)
            {
                byte[] keikiByteArr = new byte[108];

                for (int j = 0; j < 108; j++)
                {
                    keikiByteArr[j] = OPSBDkesoByteArr[i * 108 + j];
                }

                OPS9000_T_BD_KESO_KEIKI temp = new OPS9000_T_BD_KESO_KEIKI();
                temp.tgno_c = System.Text.Encoding.ASCII.GetString(keikiByteArr, 0, 6);
                temp.tgnm_c = System.Text.Encoding.GetEncoding(932).GetString(keikiByteArr, 14, 34);
                OPSBDkesoData.Add(temp);
            }

            List<OPS9000_T_STATION> stationList = new List<OPS9000_T_STATION>();
            for (int p = 0; p < 36; p++)
            {
                OPS9000_T_STATION station = new OPS9000_T_STATION();
                station.station_no = p;
                for (int q = 0; q < 513; q++)
                {
                    OPSBDkesoData[p * 513 + q].keiki_no = q;
                    station.keiki_lst.Add(OPSBDkesoData[p * 513 + q]);
                }
                stationList.Add(station);
            }

            List<OPS9000_T_DD_TPT> T_DD_tptData = new List<OPS9000_T_DD_TPT>();

            string OOPSBDtptFile = path + "OPSDDtpt";

            byte[] OPSBDtptByteArr = File.ReadAllBytes(OOPSBDtptFile);

            int historyIDCount = OPSBDtptByteArr.Length / 4;

            for (int i = 0; i < historyIDCount; i++)
            {
                byte[] historyIdByteArr = new byte[4];

                for (int j = 0; j < 4; j++)
                {
                    historyIdByteArr[j] = OPSBDtptByteArr[i * 4 + j];
                }

                OPS9000_T_DD_TPT temp = new OPS9000_T_DD_TPT();
                temp.history_id = i + 1;
                temp.stno_s = BitConverter.ToInt16(historyIdByteArr, 0);
                temp.tpt_s = BitConverter.ToInt16(historyIdByteArr, 2);
                T_DD_tptData.Add(temp);
            }

            foreach (OPS9000_T_DD_TPT item in T_DD_tptData)
            {
                if (item.stno_s != -1)
                {
                    OPS9kMapping.Add(item.history_id.ToString(), stationList[item.stno_s].keiki_lst[item.tpt_s].tgno_c);
                }
            }
            return OPS9kMapping;
        }

        /// <summary>
        /// Read Amount Data
        /// </summary>
        /// <param name="historyID"></param>
        /// <param name="historyList"></param>
        /// <returns></returns>
        public OPS9000_T_HSTHB_DAT ReadAmountData(int historyID, List<OPS9000_T_HSTHB_DAT> historyList)
        {
            return historyList[historyID - 1];
        }

        public Dictionary<string, string> GetMenPartByTime(DateTime startDateTime, DateTime endDateTime, OPS9000_T_HSTHB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS9000_T_HSTHB_IDX index in dataIndex.index_lst)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s);
                if (Utility.InRange(indexDateTime, startDateTime, endDateTime))
                {
                    targetMenList.Add(indexDateTime.ToShortDateString(), index.men_s.ToString());
                }
            }

            return targetMenList;
        }

        /// <summary>
        /// Get index part
        /// </summary>
        /// <param name="inputFilename"></param>
        /// <returns></returns>
        public OPS9000_T_HSTHB_HED GetIndexPart(string inputFilename)
        {
            List<OPS9000_T_HSTHB_IDX> indexDataList = new List<OPS9000_T_HSTHB_IDX>();
            short[] yobi = new short[47];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(512, SeekOrigin.Begin);

                byte[] b = new byte[2976];
                fs.Read(b, 0, 2976);

                OPS9000_T_HSTHB_HED indexPart = new OPS9000_T_HSTHB_HED();
                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);

                for (int i = 1; i <= 185; i++)
                {
                    OPS9000_T_HSTHB_IDX idx = new OPS9000_T_HSTHB_IDX();
                    idx.yea_s = BitConverter.ToInt16(b, i * 16);
                    idx.mon_s = BitConverter.ToInt16(b, i * 16 + 2);
                    idx.day_s = BitConverter.ToInt16(b, i * 16 + 4);
                    idx.cal_s = BitConverter.ToInt16(b, i * 16 + 6);
                    idx.men_s = BitConverter.ToInt16(b, i * 16 + 8);
                    idx.prt_s = BitConverter.ToInt16(b, i * 16 + 10);
                    idx.yobi01_s = Utility.GetShortArray4(b, i * 16 + 12);

                    indexDataList.Add(idx);
                }
                indexPart.index_lst = indexDataList;

                byte[] dailyByte = new byte[2];
                fs.Seek(3488, SeekOrigin.Begin);
                fs.Read(dailyByte, 0, 2);

                // Read daily time
                indexPart.chghor_s = BitConverter.ToInt16(dailyByte, 0);

                // Read yobi
                byte[] yobiByte = new byte[94];
                fs.Seek(3490, SeekOrigin.Begin);
                fs.Read(yobiByte, 0, 94);

                for (int i = 0; i < 47; i++)
                {
                    yobi[i] = BitConverter.ToInt16(yobiByte, i * 2);
                }
                indexPart.yobi02_s = yobi;

                return indexPart;
            }
        }

        /// <summary>
        /// Get all Menno by Hour Index
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetAllMenNo(OPS9000_T_HSTHB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS9000_T_HSTHB_IDX index in dataIndex.index_lst)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s);
                targetMenList.Add(indexDateTime.ToShortDateString(), index.men_s.ToString());
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
        public Dictionary<string, string> GetMenNoByTime(DateTime startDateTime, DateTime endDateTime, OPS9000_T_HSTHB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS9000_T_HSTHB_IDX index in dataIndex.index_lst)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s);
                if (Utility.InRange(indexDateTime, startDateTime, endDateTime))
                {
                    targetMenList.Add(indexDateTime.ToShortDateString(), index.men_s.ToString());
                }
            }

            return targetMenList;
        }

        /// <summary>
        /// Create Men Mapping Table for OPS9000 and OPS5000
        /// </summary>
        /// <param name="OPS9kMapping"></param>
        /// <param name="OPS5kMapping"></param>
        /// <returns></returns>
        public Dictionary<String, String> CreateMenMappingTable(Dictionary<string, string> OPS9kMapping, Dictionary<string, string> OPS5kMapping)
        {
            Dictionary<String, String> menMapping = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> entry in OPS9kMapping)
            {
                if (OPS5kMapping.ContainsKey(entry.Key))
                {
                    menMapping.Add(entry.Value, OPS5kMapping[entry.Key]);
                }
            }
            return menMapping;
        }

        /// <summary>
        /// Get info by array
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
        /// Get last part no in hour
        /// </summary>
        /// <param name="menNo"></param>
        /// <param name="indexData"></param>
        /// <returns></returns>
        public static short GetLastPartNoInHourData(short menNo, OPS9000_T_HSTHB_HED indexData)
        {
            short partNo = -1;
            foreach (OPS9000_T_HSTHB_IDX index in indexData.index_lst)
            {
                if (menNo == index.men_s)
                {
                    partNo = index.prt_s;
                }
            }
            return partNo;
        }

        /// <summary>
        /// Get Men by Start Date
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public int GetMenOfStartDate(DateTime startDateTime, OPS9000_T_HSTHB_HED dataIndex)
        {
            int targetMen = -1;
            foreach (OPS9000_T_HSTHB_IDX index in dataIndex.index_lst)
            {
                if (DateTime.Compare(Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s), startDateTime) == 0)
                {
                    targetMen = index.men_s;
                }
            }

            return targetMen;
        }

        /// <summary>
        /// Get Men by End Date
        /// </summary>
        /// <param name="endDateTime"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public short GetMenOfEndDate(DateTime endDateTime, OPS9000_T_HSTHB_HED dataIndex)
        {
            short targetMen = -1;
            foreach (OPS9000_T_HSTHB_IDX index in dataIndex.index_lst)
            {
                if (DateTime.Compare(Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s), endDateTime) == 0)
                {
                    targetMen = index.men_s;
                }
            }

            return targetMen;
        }

        /// <summary>
        /// Read Data Part
        /// </summary>
        /// <param name="menNo"></param>
        /// <param name="partNo"></param>
        /// <param name="inputFilename"></param>
        /// <returns></returns>
        public List<OPS9000_T_HSTHB_DAT> ReadDataPart(int menNo, int partNo, string inputFilename)
        {
            List<OPS9000_T_HSTHB_DAT> historyList = new List<OPS9000_T_HSTHB_DAT>();
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                int partSize = Constants.OPS9000_CNF_HIST_N * 16;
                int menSize = partSize * Constants.OPS9000_TOTAL_PART_IN_MEN_HOUR;
                int startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + Constants.OPS9000_HOUR_DATA_PART_START_INDEX;

                fs.Seek(startIndex, SeekOrigin.Begin);
                byte[] partData = new byte[partSize];
                fs.Read(partData, 0, partSize);

                for (int i = 0; i < Constants.OPS9000_CNF_HIST_N; i++)
                {
                    OPS9000_T_HSTHB_DAT amount = new OPS9000_T_HSTHB_DAT();
                    amount.sts_s = BitConverter.ToInt16(partData, i * 16);
                    amount.yobi_s = BitConverter.ToInt16(partData, i * 16 + 2);
                    amount.inf_s = GetAddictionInfo(partData, i * 16 + 4);
                    amount.dat_d = BitConverter.ToDouble(partData, i * 16 + 8);
                    historyList.Add(amount);
                }
            }
            return historyList;
        }
    }
}
