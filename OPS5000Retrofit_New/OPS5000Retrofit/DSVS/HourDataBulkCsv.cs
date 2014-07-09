//////////////////////////////////////////////////////////////////////
// File Name     ：HourDataBulkCsv.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert DSVS Hour data to DSVS5000
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using OPS5000Retrofit.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace OPS5000Retrofit
{
    class HourDataBulkCsv
    {

        /// <summary>
        /// Convert to OPS5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="fromDay"></param>
        /// <param name="fromHour"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="toDay"></param>
        /// <param name="toHour"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    short fromYear,
                                    short fromMonth,
                                    short fromDay,
                                    short fromHour,
                                    short toYear,
                                    short toMonth,
                                    short toDay,
                                    short toHour,
                                    string inputFolderPath,
                                    string ouputFolder)
        {

            List<string> fileList = GetHourBulkFileList(inputFolderPath);

            OPS5000Hour ops5k = new OPS5000Hour();

            string hourDataFile5K = ouputFolder + @"\number\HSTHB";
            string ops5KBDKesoFilePath = ouputFolder + @"\load\OPSBDkeso";
            string ops5KDDtptFilePath = ouputFolder + @"\load\OPSDDtpt";

            OPS5000_T_HSTHB_HED indexPart5k = ops5k.GetIndexPart(hourDataFile5K);

            // Create history mapping table
            Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);


            if (moveAll)
            {
                foreach (string file in fileList)
                {
                    List<Dictionary<string, DSVSUnitBulkData>> partList = ReadAllData(file);
                    string dateTime = DSVSCommon.GetDateTime(file, DSVSCommon.CsvFileType.HourDataBulk);
                    long menNo = (long)ops5k.GetMenNoByTime(dateTime, indexPart5k);

                    for (int i = 1; i <= Constants.DSVS_NUMBER_OF_HOUR; i++)
                    {
                        Dictionary<string, DSVSUnitBulkData> partData = partList[i - 1];

                        foreach (KeyValuePair<string, DSVSUnitBulkData> entry in partData)
                        {
                            long historyId = (long)OPSBDCommon.GetHistoryIdByTagNo(entry.Key, ops5kMapping);
                            if (historyId != -1)
                            {
                                DSVSUnitBulkData unit = (DSVSUnitBulkData)entry.Value;
                                OPS5000_T_HSTHB_DAT amount = DSVSCommon.ConvertDSVSHourDataToOPSHourData(unit);
                                ops5k.WriteHourDataFromCSV(menNo, i, historyId, hourDataFile5K, amount);
                            }
                        }
                    }
                }
            }
            else
            {
                // Convert to datetime
                DateTime startDateTime = new DateTime(fromYear, fromMonth, fromDay, 0, 0, 0);
                DateTime endDateTime = new DateTime(toYear, toMonth, toDay, 0, 0, 0);


                foreach (string file in fileList)
                {
                    string dateTime = DSVSCommon.GetDateTime(file, DSVSCommon.CsvFileType.HourDataBulk);
                    if (Utility.InRange(dateTime, startDateTime, endDateTime))
                    {
                        int fromPartNo = 1;
                        int toPartNo = Constants.DSVS_NUMBER_OF_HOUR;
                        List<Dictionary<string, DSVSUnitBulkData>> partList = ReadAllData(file);
                        long menNo = (long)ops5k.GetMenNoByTime(dateTime, indexPart5k);

                        if (Utility.DayCompare(dateTime, startDateTime) == 0)
                        {
                            fromPartNo = fromHour;
                            if (Utility.DayCompare(dateTime, endDateTime) == 0)
                            {
                                toPartNo = toHour;
                            }
                        }

                        for (int i = fromPartNo; i <= toPartNo; i++)
                        {
                            Dictionary<string, DSVSUnitBulkData> partData = partList[i - 1];
                            foreach (KeyValuePair<string, DSVSUnitBulkData> entry in partData)
                            {
                                long historyId = (long)OPSBDCommon.GetHistoryIdByTagNo(entry.Key, ops5kMapping);
                                if (historyId != -1)
                                {
                                    DSVSUnitBulkData unit = (DSVSUnitBulkData)entry.Value;
                                    OPS5000_T_HSTHB_DAT amount = DSVSCommon.ConvertDSVSHourDataToOPSHourData(unit);
                                    ops5k.WriteHourDataFromCSV(menNo, i, historyId, hourDataFile5K, amount);
                                }
                            }
                        }
                    }
                }
            }
        }

        private List<string> GetHourBulkFileList(string inputFolder)
        {
            List<string> hourBulkFileList = new List<string>();
            foreach (string file in Directory.EnumerateFiles(inputFolder, "*.csv"))
            {
                if (DSVSCommon.CsvFileType.HourDataBulk == DSVSCommon.GetFileType(file))
                {
                    hourBulkFileList.Add(file);
                }
            }
            return hourBulkFileList;
        }

        private List<Dictionary<string, DSVSUnitBulkData>> ReadAllData(string fileName)
        {

            List<Dictionary<string, DSVSUnitBulkData>> partList = new List<Dictionary<string, DSVSUnitBulkData>>();
            string[] tagNoArray = DSVSCommon.GetLine(fileName, 4).Split(new char[] { ',' });

            string[] lines = File.ReadAllLines(fileName);
            int numberTag = DSVSCommon.GetTotalTag(fileName);


            for (int i = 0; i < Constants.DSVS_NUMBER_OF_HOUR; i++)
            {
                Dictionary<string, DSVSUnitBulkData> partData = new Dictionary<string, DSVSUnitBulkData>();
                string[] status = lines[4 * i + 4].Split(new char[] { ',' });
                string[] yobi1 = lines[4 * i + 1 + 4].Split(new char[] { ',' });
                string[] yobi2 = lines[4 * i + 2 + 4].Split(new char[] { ',' });
                string[] data = lines[4 * i + 3 + 4].Split(new char[] { ',' });

                for (int j = 0; j < numberTag; j++)
                {
                    DSVSUnitBulkData unit = new DSVSUnitBulkData();

                    // Get status
                    if (status[j] == " ")
                    {
                        unit.sts_s = 0;
                    }
                    else
                    {
                        unit.sts_s = Int16.Parse(status[j]);
                    }

                    // Get addtion info 1
                    if (yobi1[j] == " ")
                    {
                        unit.yobi1_s = 0;
                    }
                    else
                    {
                        unit.yobi1_s = Int16.Parse(yobi1[j]);
                    }

                    // Get addtion info 2
                    if (yobi2[j] == " ")
                    {
                        unit.yobi2_s = 0;
                    }
                    else
                    {
                        unit.yobi2_s = Int16.Parse(yobi2[j]);
                    }

                    // Get data
                    if (data[j] == " ")
                    {
                        unit.dat_d = 0;
                    }
                    else
                    {
                        unit.dat_d = Double.Parse(data[j]);
                    }

                    partData.Add(Utility.FillSpaceToTagNo(tagNoArray[j]), unit);
                }
                partList.Add(partData);
            }
            return partList;
        }
    }
}
