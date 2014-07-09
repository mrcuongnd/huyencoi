//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_DAYComment.cs
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
    /// Day Comment Structure
    /// </summary>
    class OPS9000_DayComment
    {
        /// <summary>
        /// Converty data
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="ops9KBDKesoFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="ops5kCHODBFilePath"></param>
        /// <param name="ops9kCHODBFilePath"></param>
        /// <param name="ops9kHSTDBFilePath"></param>
        /// <param name="ops5kHSTDBFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int toYear,
                                    int toMonth,
                                    string ops9KBDKesoFilePath,
                                    string ops9KDDtptFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string ops5kCHODBFilePath,
                                    string ops9kCHODBFilePath,
                                    string ops9kHSTDBFilePath,
                                    string ops5kHSTDBFilePath)
        {
            // Read data index OPS9000
            OPS9000Day ops9k = new OPS9000Day();
            OPS9000_T_HSTDB_HED indexPart9k = ops9k.GetIndexPart(ops9kHSTDBFilePath);

            // Read data index OPS9000
            OPS5000Day ops5k = new OPS5000Day();
            OPS5000_T_HSTDB_HED indexPart5k = ops5k.GetIndexPart(ops5kHSTDBFilePath);

            // Create history mapping table
            Dictionary<string, string> ops9kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable9K(ops9KBDKesoFilePath, ops9KDDtptFilePath);
            Dictionary<string, string> ops5kMapping = OPSBDCommon.CreateHistoryId_TagNoMappingTable5K(ops5KBDKesoFilePath, ops5KDDtptFilePath);
            Dictionary<string, string> historyMapping = OPSBDCommon.CreateHistoryMappingTable9K_5K(ops5kMapping, ops9kMapping);

            // Declare maping men
            Dictionary<short, short> menMapping = new Dictionary<short, short>();
            Dictionary<short, short> menPartMaping = new Dictionary<short, short>();

            // Declare variable mark point fromdate, todate
            int part_PointStart = 0;
            int part_PointEnd = 0;

            if (moveAll)
            {
                // Loop OPS5000
                for (int i = 0; i < 121; i++)
                {
                    // Loop OPS9000
                    for (int j = 0; j < 25; j++)
                    {
                        if (indexPart5k.indx[i].yea_s == indexPart9k.indx[j].yea_s && indexPart5k.indx[i].mon_s == indexPart9k.indx[j].mon_s)
                        {
                            menMapping.Add(indexPart5k.indx[i].men_s, indexPart9k.indx[j].men_s);
                            menPartMaping.Add(indexPart9k.indx[j].men_s, indexPart9k.indx[j].prt_s);
                        }
                    }
                }
            }
            else
            {
                // move by time
                // Loop OPS5000
                for (int i = 0; i < 121; i++)
                {
                    // Loop OPS9000
                    for (int j = 0; j < 25; j++)
                    {
                        if (indexPart5k.indx[i].yea_s == indexPart9k.indx[j].yea_s && indexPart5k.indx[i].mon_s == indexPart9k.indx[j].mon_s &&
                            indexPart9k.indx[j].yea_s >= fromYear && indexPart9k.indx[j].yea_s <= toYear &&
                            indexPart9k.indx[j].mon_s >= fromMonth && indexPart9k.indx[j].mon_s <= toMonth)
                        {
                            menMapping.Add(indexPart5k.indx[i].men_s, indexPart9k.indx[j].men_s);
                            menPartMaping.Add(indexPart9k.indx[j].men_s, indexPart9k.indx[j].prt_s);

                            // Mark from date
                            if (indexPart9k.indx[j].yea_s == fromYear && indexPart9k.indx[j].mon_s == fromMonth)
                            {
                                part_PointStart = indexPart5k.indx[i].men_s;
                            }

                            // Mark to date
                            if (indexPart9k.indx[j].yea_s == toYear && indexPart9k.indx[j].mon_s == toMonth)
                            {
                                part_PointEnd = indexPart5k.indx[i].men_s;
                            }
                        }
                    }
                }
            }

            int dayStart = 0;
            int dayEnd = 0;
            short dayLast = 0;
            short menOps9000 = 0;
            int startIndex9k = 0;
            int startIndex5k = 0;

            // Convert data
            using (FileStream fs5k = new FileStream(ops5kCHODBFilePath, FileMode.Open, FileAccess.Write))
            {
                using (FileStream fs9k = new FileStream(ops9kCHODBFilePath, FileMode.Open, FileAccess.Read))
                {
                    foreach (KeyValuePair<short, short> menItem in menMapping)
                    {
                        dayStart = 1;
                        dayEnd = 31;
                        menOps9000 = menItem.Value;
                        if (menPartMaping.ContainsKey(menOps9000))
                        {
                            dayLast = (short)menPartMaping[menOps9000];
                            if (!moveAll)
                            {
                                if (part_PointStart == menItem.Key)
                                {
                                    dayStart = fromMonth;
                                    dayEnd = 31;
                                }
                                if (part_PointEnd == menItem.Key)
                                {
                                    // If month different
                                    if (part_PointStart != part_PointEnd)
                                    {
                                        dayStart = 1;
                                    }

                                    // Check day
                                    if (dayLast < toMonth)
                                    {
                                        dayEnd = dayLast;
                                    }
                                    else
                                    {
                                        dayEnd = toMonth;
                                    }
                                }

                            }
                            else 
                            {
                                //Check last part
                                if (dayLast < dayEnd) 
                                {
                                    dayEnd = dayLast;
                                }
                            }

                            for (int day = dayStart; day <= dayEnd; day++)
                            {
                                // Init byte array.
                                byte[] bytes = new byte[128];

                                // Read data cmtd
                                for (int cmtd = 1; cmtd <= Constants.OPS9000_CNF_MAXCMT_N; cmtd++)
                                {
                                    // Move cursor into 9k file
                                    startIndex9k = (menOps9000 - 1) * Constants.OPS9000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS9000_CHODB_DAY_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs9k.Seek(startIndex9k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
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
                                for (int chad = 1; chad <= 20; chad++)
                                {
                                    // Move cursor into 9k file
                                    if (chad < 11)
                                    {
                                        startIndex9k = (menOps9000 - 1) * Constants.OPS9000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS9000_CHODB_DAY_SIZE + cmtdSize9k + (2 * (chad - 1) + 1) * 62 + 4 + Constants.DATA_BUFFER_512;
                                        fs9k.Seek(startIndex9k, SeekOrigin.Begin);
                                    }
                                    else
                                    {
                                        startIndex9k = (menOps9000 - 1) * Constants.OPS9000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS9000_CHODB_DAY_SIZE + cmtdSize9k + 2 * (chad - 10) * 62 + 4 + 62 + Constants.DATA_BUFFER_512;
                                        fs9k.Seek(startIndex9k, SeekOrigin.Begin);
                                    }

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + chad * 128 + Constants.DATA_BUFFER_512;
                                    fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                                    // Read data 9k
                                    fs9k.Read(bytes, 0, 128);

                                    // Write data into 5k
                                    fs5k.Write(bytes, 0, 128);
                                }

                                // Declare size chad
                                int chadSize9k = 25856;// 192 * 128 + 10 * 128   
                                int chadSize5k = 140544;// 999 * 128 + 99 * 128

                                // Read data wthd
                                for (int wthd = 1; wthd <= 3; wthd++)
                                {
                                    // Move cursor into 9k file
                                    startIndex9k = (menOps9000 - 1) * Constants.OPS9000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS9000_CHODB_DAY_SIZE + cmtdSize9k + chadSize9k + (wthd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs9k.Seek(startIndex9k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + chadSize5k + (wthd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                                    // Read data 9k
                                    fs9k.Read(bytes, 0, 128);

                                    // Write data into 5k
                                    fs5k.Write(bytes, 0, 128);
                                }

                                // Declare size wthd
                                int wthdSize9k = 27392;// 192 * 128 + 10 * 128 + 3 * 512 
                                int wthdSize5k = 142080;// 999 * 128 + 99 * 128 + 3 * 512

                                // Read data wndd
                                for (int wndd = 1; wndd <= 3; wndd++)
                                {
                                    // Move cursor into 9k file
                                    startIndex9k = (menOps9000 - 1) * Constants.OPS9000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS9000_CHODB_DAY_SIZE + cmtdSize9k + chadSize9k + wthdSize9k + (wndd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs9k.Seek(startIndex9k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + chadSize5k + wthdSize5k + (wndd - 1) * 128 + Constants.DATA_BUFFER_512;
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

    }
}
