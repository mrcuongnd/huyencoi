//////////////////////////////////////////////////////////////////////
// File Name     ：MonthDataBulkCsv.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：MonthDataBulkCsv file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using OPS5000Retrofit.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Month Data Bulk Csv Structure
    /// </summary>
    class MonthDataBulkCsv
    {
        #region Declare variable
        public int countItem { get; set; }
        public int year { get; set; }
        public string[] tagNoList { get; set; }
        public DSVSDataBulkItem[] dataItem { get; set; }
        # endregion

        #region Function read data csv file
        /// <summary>
        /// Read data csv file
        /// </summary>
        /// <param name="fileName"></param>
        public bool ReadData(string fileName)
        {
            // Read all file
            string[] lines = File.ReadAllLines(fileName, Encoding.GetEncoding(932));

            // Check definition file
            if (DSVSCommon.CsvFileType.MonthDataBulk == (DSVSCommon.CsvFileType)Utility.CheckDoubleReplace(lines[0].Split(',')[0], 0))
            {
                // Get count item
                countItem = Utility.CheckIntegerReplace(lines[1].Split(',')[0], 0);

                // Get date
                year = Utility.CheckIntegerReplace(lines[2].Split(',')[0], 0);

                // Get tagno list
                tagNoList = new string[countItem];
                tagNoList = lines[3].Split(',');
                for (int i = 0; i < tagNoList.Length; i++)
                {
                    tagNoList[i] = tagNoList[i].PadRight(8);
                }

                // Init data item array
                dataItem = new DSVSDataBulkItem[Constants.DSVS_NUMBER_OF_MONTH];
                for (int i = 0; i < Constants.DSVS_NUMBER_OF_MONTH; i++)
                {
                    // Init data item
                    dataItem[i] = new DSVSDataBulkItem(countItem);

                    // Get status
                    int index = -1;
                    foreach (string strStatus in lines[i * 4 + 4].Split(','))
                    {
                        index++;
                        dataItem[i].status[index] = (short)Utility.CheckIntegerReplace(strStatus, 0);
                    }

                    // Get info 1
                    index = -1;
                    foreach (string strInfo1 in lines[i * 4 + 5].Split(','))
                    {
                        index++;
                        dataItem[i].inf1[index] = (short)Utility.CheckIntegerReplace(strInfo1, 0);
                    }

                    // Get info 2
                    index = -1;
                    foreach (string strInfo2 in lines[i * 4 + 6].Split(','))
                    {
                        index++;
                        dataItem[i].inf2[index] = (short)Utility.CheckIntegerReplace(strInfo2, 0);
                    }

                    // Get data
                    index = -1;
                    foreach (string strData in lines[i * 4 + 7].Split(','))
                    {
                        index++;
                        dataItem[i].data[index] = Utility.CheckDoubleReplace(strData, 0d);
                    }
                }
                return true;
            }
            // Return definition file
            return false;

        }
        # endregion

        #region Function convert data to file binary month OPS5000
        /// <summary>
        /// Convert into HSTMB ops5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ops5kHSTMBFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int toYear,
                                    int toMonth,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string inputFolderCsvPath,
                                    string ops5kHSTMBFilePath)
        {
            // Read data OPS6000
            OPS5000_Month ops5kMonthbulk = new OPS5000_Month();
            ops5kMonthbulk.ReadData(ops5kHSTMBFilePath);

            // Declare maping men
            short menMapping = -1;

            // Declare history maping variable
            Dictionary<string, int> historyMapping = new Dictionary<string, int>();

            // Directory folder read all csv month file
            foreach (string cvsFile in Directory.EnumerateFiles(inputFolderCsvPath, "*.csv"))
            {
                // Read file cvs and check definition file month
                if (ReadData(cvsFile))
                {

                    // Check fillter condition data by year true or fillter all
                    if (moveAll || (!moveAll && (year >= fromYear && year <= toYear)))
                    {

                        // Create history mapping table [history id 5k, item index]
                        Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
                        foreach (KeyValuePair<string, string> item in ops5kMapping)
                        {
                            for (int i = 0; i < tagNoList.Length; i++)
                            {
                                if (item.Value == tagNoList[i])
                                {
                                    historyMapping.Add(item.Key, i);
                                }
                            }
                        }

                        // Init menMapping variable
                        menMapping = -1;

                        // Get men maping
                        for (int i = 0; i < ops5kMonthbulk.indexData.indx.Length; i++)
                        {
                            if (ops5kMonthbulk.indexData.indx[i].yea_s == year)
                            {
                                menMapping = ops5kMonthbulk.indexData.indx[i].men_s;
                            }
                        }
                    }

                    // If men maping exists
                    if (menMapping != -1)
                    {

                        int partStart = 0;
                        int partEnd = 0;
                        int itemIndex = 0;
                        int historyId5000 = 0;
                        int partSize = 0;
                        int menSize = 0;
                        int startIndex = 0;

                        // Convert data
                        using (FileStream fs = new FileStream(ops5kHSTMBFilePath, FileMode.Open, FileAccess.Write))
                        {
                            partSize = Constants.OPS5000_CNF_HIST_N * 16 * 8;
                            menSize = partSize * Constants.OPS5000_TOTAL_PART_IN_MEN_MONTH;

                            partStart = 1;
                            partEnd = 12;
                            if (!moveAll)
                            {
                                // Get start, end for loop part when move data by month
                                partStart = fromMonth;
                                partEnd = toMonth;
                            }

                            for (int part = partStart; part <= partEnd; part++)
                            {
                                // Point start part
                                startIndex = (menMapping - 1) * menSize + (part - 1) * partSize + Constants.OPS5000_MONTH_DATA_PART_START_INDEX;

                                foreach (KeyValuePair<string, int> item in historyMapping)
                                {
                                    historyId5000 = Utility.CheckIntegerReplace(item.Key.ToString(), 0);
                                    itemIndex = Utility.CheckIntegerReplace(item.Value.ToString(), 0);

                                    for (int dat = 0; dat < 8; dat++)
                                    {
                                        fs.Seek(startIndex + (historyId5000 - 1) * 128 + dat * 16, SeekOrigin.Begin);

                                        byte[] bytes = new byte[16];

                                        byte[] status = BitConverter.GetBytes(dataItem[part - 1].status[itemIndex]);
                                        //byte[] yobi_s = BitConverter.GetBytes(dataItem[part].yobi_s);
                                        byte[] inf_s1 = BitConverter.GetBytes(dataItem[part - 1].inf1[itemIndex]);
                                        byte[] inf_s2 = BitConverter.GetBytes(dataItem[part - 1].inf2[itemIndex]);
                                        byte[] data = BitConverter.GetBytes(dataItem[part - 1].data[itemIndex]);

                                        bytes[0] = status[0];
                                        bytes[1] = status[1];
                                        //bytes[2] = yobi_s[0];
                                        //bytes[3] = yobi_s[1];
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
        #endregion

    }
}
