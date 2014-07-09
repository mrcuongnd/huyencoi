//////////////////////////////////////////////////////////////////////
// File Name     ：YearDataCommentCsv.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：YearDataCommentCsv file
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
    /// Year Data Comment Csv Structure
    /// </summary>
    class YearDataCommentCsv
    {
        #region Declare variable

        /// <summary>
        /// Year
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// Data item
        /// </summary>
        public DSVSDataCommentItem dataItem { get; set; }
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
            if (DSVSCommon.CsvFileType.YearCommentData == (DSVSCommon.CsvFileType)Utility.CheckDoubleReplace(lines[0].Split(',')[0], 0))
            {
                // Get date
                year = Utility.CheckIntegerReplace(lines[1].Split(',')[0].ToString(), 0);

                // Init data
                dataItem = new DSVSDataCommentItem();
                for (int day = 0; day < Constants.DSVS_NUMBER_OF_YEAR; day++)
                {
                    // Read cmt
                    for (int i = 0; i < 192; i++)
                    {
                        dataItem.cmt[i] = lines[day * 201 + i + 2].Replace(",", "").Replace("\"", "");
                    }

                    // Read cmt
                    for (int i = 0; i < 9; i++)
                    {
                        dataItem.cha[i] = lines[day * 201 + i + 2 + 192].Replace(",", "").Replace("\"", "");
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
        /// <param name="toYear"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="inputFolderCsvPath"></param>
        /// <param name="ops5kHSTMBFilePath"></param>
        /// <param name="ops5kCHOMBFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int toYear,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string inputFolderCsvPath,
                                    string ops5kHSTYBFilePath,
                                    string ops5kCHOYBFilePath)
        {
            // Read data OPS6000
            OPS5000_Year ops5k = new OPS5000_Year();
            ops5k.ReadData(ops5kHSTYBFilePath);

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
                    for (int i = 0; i < ops5k.indexData.indx.Length; i++)
                    {
                        if (ops5k.indexData.indx[i].yea_s == year)
                        {
                            if (moveAll)
                            {
                                menMapping = ops5k.indexData.indx[i].men_s;
                            }
                            else if (!moveAll && year >= fromYear && year <= toYear)
                            {
                                menMapping = ops5k.indexData.indx[i].men_s;
                            }
                        }
                    }

                    // If men maping exists
                    if (menMapping != -1)
                    {

                        int startIndex = 0;

                        // Convert data
                        using (FileStream fs = new FileStream(ops5kCHOYBFilePath, FileMode.Open, FileAccess.Write))
                        {
                            // Declare byte array.
                            byte[] bytes;

                            // Read data cmtd
                            for (int cmtd = 1; cmtd <= 192; cmtd++)
                            {
                                // Init array
                                bytes = new byte[128];

                                // Convert data string to array char
                                byte[] kij_c = Encoding.GetEncoding(932).GetBytes(dataItem.cmt[cmtd - 1]);

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
                                startIndex = (menMapping - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
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
                                byte[] kij_c = Encoding.GetEncoding(932).GetBytes(dataItem.cha[chad - 1]);

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
                                startIndex = (menMapping - 1) * Constants.OPS5000_CHOYB_MEN_SIZE + cmtdSize5k + (chad - 1) * 128 + Constants.DATA_BUFFER_512;
                                fs.Seek(startIndex, SeekOrigin.Begin);

                                // Write data into 5k
                                fs.Write(bytes, 0, 128);
                            }

                        }
                    }
                }

            }
        }
        #endregion

    }
}
