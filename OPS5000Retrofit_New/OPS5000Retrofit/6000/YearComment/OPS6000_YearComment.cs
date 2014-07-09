//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_YearComment.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_YearComment file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using OPS5000Retrofit.Common;
using System.Collections.Generic;
using System.IO;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Year Comment Structure
    /// </summary>
    class OPS6000_YearComment
    {

        /// <summary>
        /// Convert data commnet to OPS5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="toYear"></param>
        /// <param name="ops6KBDKesoFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="ops5kCHOYBFilePath"></param>
        /// <param name="ops6kCHOYBFilePath"></param>
        /// <param name="ops6kHSTYBFilePath"></param>
        /// <param name="ops5kHSTYBFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int toYear,
                                    string ops6KBDKesoFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string ops5kCHOYBFilePath,
                                    string ops6kCHOYBFilePath,
                                    string ops6kHSTYBFilePath,
                                    string ops5kHSTYBFilePath)
        {
            // Read data OPS6000
            OPS6000_Year ops6kMonthbulk = new OPS6000_Year();
            ops6kMonthbulk.ReadData(ops6kHSTYBFilePath);

            // Read data OPS6000
            OPS5000_Year ops5kMonthbulk = new OPS5000_Year();
            ops5kMonthbulk.ReadData(ops5kHSTYBFilePath);

            // Declare maping men
            Dictionary<short, short> menMapping = new Dictionary<short, short>();

            // Maping men
            if (moveAll)
            {
                // Loop OPS5000
                for (int i = 0; i < 11; i++)
                {
                    // Loop OPS6000
                    for (int j = 0; j < 3; j++)
                    {
                        if (ops5kMonthbulk.indexData.indx[i].yea_s == ops6kMonthbulk.indexData.indx[j].yea_s)
                        {
                            menMapping.Add(ops5kMonthbulk.indexData.indx[i].men_s, ops6kMonthbulk.indexData.indx[j].men_s);
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
                    // Loop OPS6000
                    for (int j = 0; j < 3; j++)
                    {
                        if (ops5kMonthbulk.indexData.indx[i].yea_s == ops6kMonthbulk.indexData.indx[j].yea_s &&
                            ops6kMonthbulk.indexData.indx[j].yea_s >= fromYear && ops6kMonthbulk.indexData.indx[j].yea_s <= toYear)
                        {
                            menMapping.Add(ops5kMonthbulk.indexData.indx[i].men_s, ops6kMonthbulk.indexData.indx[j].men_s);
                        }
                    }
                }
            }

            int startIndex6k = 0;
            int startIndex5k = 0;

            // Convert data
            using (FileStream fs5k = new FileStream(ops5kCHOYBFilePath, FileMode.Open, FileAccess.Write))
            {
                using (FileStream fs6k = new FileStream(ops6kCHOYBFilePath, FileMode.Open, FileAccess.Read))
                {
                    foreach (KeyValuePair<short, short> menItem in menMapping)
                    {
                        // Init byte array.
                        byte[] bytes = new byte[128];

                        // Read data cmtd
                        for (int cmtd = 1; cmtd <= 192; cmtd++)
                        {
                            // Move cursor into 6k file
                            startIndex6k = (menItem.Value - 1) * Constants.OPS6000_CHOYB_MEN_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                            fs6k.Seek(startIndex6k, SeekOrigin.Begin);

                            // Move cursor into 5k file
                            startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHOYB_MEN_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                            fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                            // Read data 6k
                            fs6k.Read(bytes, 0, 128);

                            // Write data into 5k
                            fs5k.Write(bytes, 0, 128);
                        }

                        // Declare size cmtd
                        int cmtdSize6k = 24576;// 192 * 128
                        int cmtdSize5k = 127872;// 999 * 128

                        // Read data cham
                        for (int cham = 1; cham <= 9; cham++)
                        {
                            // Move cursor into 6k file
                            startIndex6k = (menItem.Value - 1) * Constants.OPS6000_CHOYB_MEN_SIZE + cmtdSize6k + (cham - 1) * 128 + Constants.DATA_BUFFER_512;
                            fs6k.Seek(startIndex6k, SeekOrigin.Begin);

                            // Move cursor into 5k file
                            startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHOYB_MEN_SIZE + cmtdSize5k + (cham - 1) * 128 + Constants.DATA_BUFFER_512;
                            fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                            // Read data 6k
                            fs6k.Read(bytes, 0, 128);

                            // Write data into 5k
                            fs5k.Write(bytes, 0, 128);
                        }

                    }
                }
            }
        }


    }
}
