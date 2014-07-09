//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_YearComment.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
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
    class OPS9000_YearComment
    {
        /// <summary>
        /// Convert data commnet to OPS5000
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="ops9KBDKesoFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="ops5kCHOYBFilePath"></param>
        /// <param name="ops9kCHOYBFilePath"></param>
        /// <param name="ops9kHSTYBFilePath"></param>
        /// <param name="ops5kHSTYBFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int toYear,
                                    int toMonth,
                                    string ops9KBDKesoFilePath,
                                    string ops9KDDtptFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string ops5kCHOYBFilePath,
                                    string ops9kCHOYBFilePath,
                                    string ops9kHSTYBFilePath,
                                    string ops5kHSTYBFilePath)
        {
            // Read data OPS9000
            OPS9000_Year ops9kYearbulk = new OPS9000_Year();
            ops9kYearbulk.ReadData(ops9kHSTYBFilePath);

            // Read data OPS9000
            OPS5000_Year ops5kYearbulk = new OPS5000_Year();
            ops5kYearbulk.ReadData(ops5kHSTYBFilePath);

            // Create history mapping table
            Dictionary<string, string> ops9kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable9K(ops9KBDKesoFilePath, ops9KDDtptFilePath);
            Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
            Dictionary<string, string> historyMapping = OPSBDCommon.CreateHistoryMappingTable9K_5K(ops5kMapping, ops9kMapping);

            // Declare maping men
            Dictionary<short, short> menMapping = new Dictionary<short, short>();

            // Maping men
            if (moveAll)
            {
                // Loop OPS5000
                for (int i = 0; i < 11; i++)
                {
                    // Loop OPS9000
                    for (int j = 0; j < 6; j++)
                    {
                        if (ops5kYearbulk.indexData.indx[i].yea_s == ops9kYearbulk.indexData.indx[j].yea_s)
                        {
                            menMapping.Add(ops5kYearbulk.indexData.indx[i].men_s, ops9kYearbulk.indexData.indx[j].men_s);
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
                        if (ops5kYearbulk.indexData.indx[i].yea_s == ops9kYearbulk.indexData.indx[j].yea_s &&
                            ops9kYearbulk.indexData.indx[j].yea_s >= fromYear && ops9kYearbulk.indexData.indx[j].yea_s <= toYear)
                        {
                            menMapping.Add(ops5kYearbulk.indexData.indx[i].men_s, ops9kYearbulk.indexData.indx[j].men_s);
                        }
                    }
                }
            }

            int startIndex9k = 0;
            int startIndex5k = 0;

            // Convert data
            using (FileStream fs5k = new FileStream(ops5kCHOYBFilePath, FileMode.Open, FileAccess.Write))
            {
                using (FileStream fs9k = new FileStream(ops9kCHOYBFilePath, FileMode.Open, FileAccess.Read))
                {
                    foreach (KeyValuePair<short, short> menItem in menMapping)
                    {
                        // Init byte array.
                        byte[] bytes = new byte[128];

                        // Read data cmtd
                        for (int cmtd = 1; cmtd <= 192; cmtd++)
                        {
                            // Move cursor into 9k file
                            startIndex9k = (menItem.Value - 1) * Constants.OPS9000_CHOYB_MEN_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                            fs9k.Seek(startIndex9k, SeekOrigin.Begin);

                            // Move cursor into 5k file
                            startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHOYB_MEN_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                            fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                            // Read data 9k
                            fs9k.Read(bytes, 0, 128);

                            // Write data into 5k
                            fs5k.Write(bytes, 0, 128);
                        }

                        // Declare size cmtd
                        int cmtdSize9k = 24576;// 192 * 128
                        int cmtdSize5k = 127872;// 999 * 128

                        // Read data cham
                        for (int cham = 1; cham <= 10; cham++)
                        {
                            // Move cursor into 9k file
                            startIndex9k = (menItem.Value - 1) * Constants.OPS9000_CHOYB_MEN_SIZE + cmtdSize9k + (cham - 1) * 128 + Constants.DATA_BUFFER_512;
                            fs9k.Seek(startIndex9k, SeekOrigin.Begin);

                            // Move cursor into 5k file
                            startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHOYB_MEN_SIZE + cmtdSize5k + (cham - 1) * 128 + Constants.DATA_BUFFER_512;
                            fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                            // Read data 9k
                            fs9k.Read(bytes, 0, 128);

                            // Write data into 5k
                            fs5k.Write(bytes, 0, 128);
                        }
                    }
                }
            }
        }
    }
}
