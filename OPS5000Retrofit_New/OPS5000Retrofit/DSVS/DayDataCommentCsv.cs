//////////////////////////////////////////////////////////////////////
// File Name     ：DayDataCommentCsv.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：DayDataCommentCsv file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using OPS5000Retrofit.Common;
using System;
using System.IO;
using System.Text;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Day Data Comment Csv Structure
    /// </summary>
    class DayDataCommentCsv
    {
        #region Declare variable

        /// <summary>
        /// Year
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int month { get; set; }

        /// <summary>
        /// Data item
        /// </summary>
        public DSVSDataCommentItem[] dataItem { get; set; }
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
            if (DSVSCommon.CsvFileType.DayCommentData == (DSVSCommon.CsvFileType)Utility.CheckDoubleReplace(lines[0].Split(',')[0], 0))
            {
                // Get date
                year = Utility.CheckIntegerReplace(lines[1].Split(',')[0].ToString(), 0);
                month = Utility.CheckIntegerReplace(lines[1].Split(',')[1].ToString(), 0);

                // Init data
                dataItem = new DSVSDataCommentItem[Constants.DSVS_NUMBER_OF_DAY];
                for (int day = 0; day < Constants.DSVS_NUMBER_OF_DAY; day++)
                {
                    dataItem[day] = new DSVSDataCommentItem();
                    // Read cmt
                    for (int i = 0; i < 192; i++)
                    {
                        dataItem[day].cmt[i] = lines[day * 207 + i + 2].Replace(",", "").Replace("\"", "");
                    }

                    // Read cmt
                    for (int i = 0; i < 9; i++)
                    {
                        dataItem[day].cha[i] = lines[day * 207 + i + 2 + 192].Replace(",", "").Replace("\"", "");
                    }

                    // Read wth
                    for (int i = 0; i < 3; i++)
                    {
                        dataItem[day].wth[i] = lines[day * 207 + i + 2 + 192 + 9].Replace(",", "").Replace("\"", "");
                    }

                    // Read wnd
                    for (int i = 0; i < 3; i++)
                    {
                        dataItem[day].wnd[i] = lines[day * 207 + i + 2 + 192 + 9 + 3].Replace(",", "").Replace("\"", "");
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
        /// Convert data csv to binary file
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="fromDay"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="toDay"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="inputFolderCsvPath"></param>
        /// <param name="ops5kHSTDBFilePath"></param>
        /// <param name="ops5kCHODBFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int fromDay,
                                    int toYear,
                                    int toMonth,
                                    int toDay,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string inputFolderCsvPath,
                                    string ops5kHSTDBFilePath,
                                    string ops5kCHODBFilePath)
        {
            // Read data OPS6000
            OPS5000Day ops5k = new OPS5000Day();
            OPS5000_T_HSTDB_HED indexPart5k = ops5k.GetIndexPart(ops5kHSTDBFilePath);

            // Declare maping men
            short menMapping = -1;

            // Directory folder read all csv month file
            foreach (string cvsFile in Directory.EnumerateFiles(inputFolderCsvPath, "*.csv"))
            {
                // Read file cvs and check definition file month
                if (ReadData(cvsFile))
                {
                    // Init data
                    menMapping = -1;

                    // Get men maping
                    for (int i = 0; i < indexPart5k.indx.Count; i++)
                    {
                        if (indexPart5k.indx[i].yea_s == year && indexPart5k.indx[i].mon_s == month)
                        {
                            if (moveAll)
                            {
                                menMapping = indexPart5k.indx[i].men_s;
                            }
                            else if (!moveAll && year >= fromYear && year <= toYear && month >= fromMonth && month <= toMonth)
                            {
                                menMapping = indexPart5k.indx[i].men_s;
                            }
                        }
                    }

                    // If men maping exists
                    if (menMapping != -1)
                    {

                        int dayStart = 1;
                        int dayEnd = 31;
                        int startIndex = 0;

                        if (!moveAll)
                        {
                            // Get start, end for loop part when move data by month
                            dayStart = fromDay;
                            dayEnd = toDay;
                        }

                        // Convert data
                        using (FileStream fs = new FileStream(ops5kCHODBFilePath, FileMode.Open, FileAccess.Write))
                        {
                            for (int day = dayStart; day <= dayEnd; day++)
                            {
                                // Declare byte array.
                                byte[] bytes;

                                // Read data cmtd
                                for (int cmtd = 1; cmtd <= 192; cmtd++)
                                {
                                    // Init array
                                    bytes = new byte[128];

                                    // Convert data string to array char
                                    byte[] kij_c = Encoding.GetEncoding(932).GetBytes(dataItem[day - 1].cmt[cmtd - 1]);

                                    // Get length char
                                    byte[] lng_s = BitConverter.GetBytes(kij_c.Length);

                                    // Asign data into byte array
                                    bytes[0] = lng_s[0];
                                    bytes[1] = lng_s[1];
                                    //bytes[2] = yobi_s[0];
                                    //bytes[3] = yobi_s[1];

                                    // Convert array char into bytes
                                    for (int i = 0; i < kij_c.Length; i++)
                                    {
                                        if (i < 124)
                                        {
                                            bytes[i + 4] = kij_c[i];
                                        }
                                    }

                                    // Move cursor into 5k file
                                    startIndex = (menMapping - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs.Seek(startIndex, SeekOrigin.Begin);

                                    // Write data into 5k
                                    fs.Write(bytes, 0, 128);
                                }

                                // Declare size cmtd
                                int cmtdSize5k = 127872;// 999 * 128

                                // Read data chad
                                for (int chad = 1; chad <= 9; chad++)
                                {
                                    // Init array
                                    bytes = new byte[128];

                                    // Convert data string to array char
                                    byte[] kij_c = Encoding.GetEncoding(932).GetBytes(dataItem[day - 1].cha[chad - 1]);

                                    // Get length char
                                    byte[] lng_s = BitConverter.GetBytes(kij_c.Length);

                                    // Asign data into byte array
                                    bytes[0] = lng_s[0];
                                    bytes[1] = lng_s[1];
                                    //bytes[2] = yobi_s[0];
                                    //bytes[3] = yobi_s[1];

                                    // Convert array char into bytes
                                    for (int i = 0; i < kij_c.Length; i++)
                                    {
                                        if (i < 124)
                                        {
                                            bytes[i + 4] = kij_c[i];
                                        }
                                    }
                                    // Move cursor into 5k file
                                    startIndex = (menMapping - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + (chad - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs.Seek(startIndex, SeekOrigin.Begin);

                                    // Write data into 5k
                                    fs.Write(bytes, 0, 128);
                                }

                                // Declare size chad
                                int chadSize5k = 140544;// 999 * 128 + 99 * 128

                                // Read data wthd
                                for (int wthd = 1; wthd <= 3; wthd++)
                                {
                                    // Init array
                                    bytes = new byte[128];

                                    // Convert data string to array char
                                    byte[] kij_c = Encoding.GetEncoding(932).GetBytes(dataItem[day - 1].wth[wthd - 1]);

                                    // Get length char
                                    byte[] lng_s = BitConverter.GetBytes(kij_c.Length);

                                    // Asign data into byte array
                                    bytes[0] = lng_s[0];
                                    bytes[1] = lng_s[1];
                                    //bytes[2] = yobi_s[0];
                                    //bytes[3] = yobi_s[1];

                                    // Convert array char into bytes
                                    for (int i = 0; i < kij_c.Length; i++)
                                    {
                                        if (i < 124)
                                        {
                                            bytes[i + 4] = kij_c[i];
                                        }
                                    }

                                    // Move cursor into 5k file
                                    startIndex = (menMapping - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + chadSize5k + (wthd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs.Seek(startIndex, SeekOrigin.Begin);

                                    // Write data into 5k
                                    fs.Write(bytes, 0, 128);
                                }

                                // Declare size wthd
                                int wthdSize5k = 142080;// 999 * 128 + 99 * 128 + 3 * 512

                                // Read data wndd
                                for (int wndd = 1; wndd <= 3; wndd++)
                                {
                                    // Init array
                                    bytes = new byte[128];

                                    // Convert data string to array char
                                    byte[] kij_c = Encoding.GetEncoding(932).GetBytes(dataItem[day - 1].wnd[wndd - 1]);

                                    // Get length char
                                    byte[] lng_s = BitConverter.GetBytes(kij_c.Length);

                                    // Asign data into byte array
                                    bytes[0] = lng_s[0];
                                    bytes[1] = lng_s[1];
                                    //bytes[2] = yobi_s[0];
                                    //bytes[3] = yobi_s[1];

                                    // Convert array char into bytes
                                    for (int i = 0; i < kij_c.Length; i++)
                                    {
                                        if (i < 124)
                                        {
                                            bytes[i + 4] = kij_c[i];
                                        }
                                    }

                                    // Move cursor into 5k file
                                    startIndex = (menMapping - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + chadSize5k + wthdSize5k + (wndd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs.Seek(startIndex, SeekOrigin.Begin);

                                    // Write data into 5k
                                    fs.Write(bytes, 0, 128);
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
