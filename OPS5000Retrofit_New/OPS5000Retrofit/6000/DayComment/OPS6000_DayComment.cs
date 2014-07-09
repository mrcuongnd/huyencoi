//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_DayComment.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_DayComment file
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
    /// Day Comment Structure
    /// </summary>
    class OPS6000_DayComment
    {

        /// <summary>
        /// Converty data
        /// </summary>
        /// <param name="moveAll"></param>
        /// <param name="fromYear"></param>
        /// <param name="fromMonth"></param>
        /// <param name="toYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="ops6KBDKesoFilePath"></param>
        /// <param name="ops5KBDKesoFilePath"></param>
        /// <param name="ops5KDDtptFilePath"></param>
        /// <param name="ops5kCHODBFilePath"></param>
        /// <param name="ops6kCHODBFilePath"></param>
        /// <param name="ops6kHSTDBFilePath"></param>
        /// <param name="ops5kHSTDBFilePath"></param>
        public void ConvertToOPS5k(bool moveAll,
                                    int fromYear,
                                    int fromMonth,
                                    int fromDay,
                                    int toYear,
                                    int toMonth,
                                    int toDay,
                                    string ops6KBDKesoFilePath,
                                    string ops5KBDKesoFilePath,
                                    string ops5KDDtptFilePath,
                                    string ops5kCHODBFilePath,
                                    string ops6kCHODBFilePath,
                                    string ops6kHSTDBFilePath,
                                    string ops5kHSTDBFilePath)
        {
            // Read data index OPS6000
            OPS6000Day ops6k = new OPS6000Day();
            OPS6000_T_HSTDB_HED indexPart6k = ops6k.GetIndexPart(ops6kHSTDBFilePath);

            // Read data index OPS6000
            OPS5000Day ops5k = new OPS5000Day();
            OPS5000_T_HSTDB_HED indexPart5k = ops5k.GetIndexPart(ops5kHSTDBFilePath);

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
                    // Loop OPS6000
                    for (int j = 0; j < 25; j++)
                    {
                        if (indexPart5k.indx[i].yea_s == indexPart6k.indx[j].yea_s && indexPart5k.indx[i].mon_s == indexPart6k.indx[j].mon_s)
                        {
                            menMapping.Add(indexPart5k.indx[i].men_s, indexPart6k.indx[j].men_s);
                            menPartMaping.Add(indexPart6k.indx[j].men_s, indexPart6k.indx[j].prt_s);
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
                    // Loop OPS6000
                    for (int j = 0; j < 25; j++)
                    {
                        if (indexPart5k.indx[i].yea_s == indexPart6k.indx[j].yea_s && indexPart5k.indx[i].mon_s == indexPart6k.indx[j].mon_s &&
                            indexPart6k.indx[j].yea_s >= fromYear && indexPart6k.indx[j].yea_s <= toYear &&
                            indexPart6k.indx[j].mon_s >= fromMonth && indexPart6k.indx[j].mon_s <= toMonth)
                        {
                            menMapping.Add(indexPart5k.indx[i].men_s, indexPart6k.indx[j].men_s);
                            menPartMaping.Add(indexPart6k.indx[j].men_s, indexPart6k.indx[j].prt_s);

                            // Mark from date
                            if (indexPart6k.indx[j].yea_s == fromYear && indexPart6k.indx[j].mon_s == fromMonth)
                            {
                                part_PointStart = indexPart5k.indx[i].men_s;
                            }

                            // Mark to date
                            if (indexPart6k.indx[j].yea_s == toYear && indexPart6k.indx[j].mon_s == toMonth)
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
            short menOps6000 = 0;
            int startIndex6k = 0;
            int startIndex5k = 0;

            // Convert data
            using (FileStream fs5k = new FileStream(ops5kCHODBFilePath, FileMode.Open, FileAccess.Write))
            {
                using (FileStream fs6k = new FileStream(ops6kCHODBFilePath, FileMode.Open, FileAccess.Read))
                {
                    foreach (KeyValuePair<short, short> menItem in menMapping)
                    {
                        dayStart = 1;
                        dayEnd = 31;
                        menOps6000 = menItem.Value;
                        if (menPartMaping.ContainsKey(menOps6000))
                        {
                            dayLast = (short)menPartMaping[menOps6000];
                            if (!moveAll)
                            {
                                // Get start for loop part when move data by month
                                if (part_PointStart == menItem.Key)
                                {
                                    dayStart = fromDay;
                                    dayEnd = 31;
                                }

                                // Get end for loop part when move data by month
                                if (part_PointEnd == menItem.Key)
                                {
                                    // If month different
                                    if (part_PointStart != part_PointEnd)
                                    {
                                        dayStart = 1;
                                    }

                                    // Check day
                                    if (dayLast < toDay)
                                    {
                                        dayEnd = dayLast;
                                    }
                                    else
                                    {
                                        dayEnd = toDay;
                                    }
                                }
                            }
                            else
                            {
                                // Check last part
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
                                for (int cmtd = 1; cmtd <= 192; cmtd++)
                                {
                                    // Move cursor into 6k file
                                    startIndex6k = (menOps6000 - 1) * Constants.OPS6000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS6000_CHODB_DAY_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs6k.Seek(startIndex6k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + (cmtd - 1) * 128 + Constants.DATA_BUFFER_512;
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
                                for (int chad = 1; chad <= 9; chad++)
                                {
                                    // Move cursor into 6k file
                                    startIndex6k = (menOps6000 - 1) * Constants.OPS6000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS6000_CHODB_DAY_SIZE + cmtdSize6k + (chad - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs6k.Seek(startIndex6k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + (chad - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                                    // Read data 6k
                                    fs6k.Read(bytes, 0, 128);

                                    // Write data into 5k
                                    fs5k.Write(bytes, 0, 128);
                                }

                                // Declare size chad
                                int chadSize6k = 25728;// 192 * 128 + 9 * 128
                                int chadSize5k = 140544;// 999 * 128 + 99 * 128

                                // Read data wthd
                                for (int wthd = 1; wthd <= 3; wthd++)
                                {
                                    // Move cursor into 6k file
                                    startIndex6k = (menOps6000 - 1) * Constants.OPS6000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS6000_CHODB_DAY_SIZE + cmtdSize6k + chadSize6k + (wthd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs6k.Seek(startIndex6k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + chadSize5k + (wthd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs5k.Seek(startIndex5k, SeekOrigin.Begin);

                                    // Read data 6k
                                    fs6k.Read(bytes, 0, 128);

                                    // Write data into 5k
                                    fs5k.Write(bytes, 0, 128);
                                }

                                // Declare size wthd
                                int wthdSize6k = 27264;// 192 * 128 + 9 * 128 + 3 * 512 
                                int wthdSize5k = 142080;// 999 * 128 + 99 * 128 + 3 * 512

                                // Read data wndd
                                for (int wndd = 1; wndd <= 3; wndd++)
                                {
                                    // Move cursor into 6k file
                                    startIndex6k = (menOps6000 - 1) * Constants.OPS6000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS6000_CHODB_DAY_SIZE + cmtdSize6k + chadSize6k + wthdSize6k + (wndd - 1) * 128 + Constants.DATA_BUFFER_512;
                                    fs6k.Seek(startIndex6k, SeekOrigin.Begin);

                                    // Move cursor into 5k file
                                    startIndex5k = (menItem.Key - 1) * Constants.OPS5000_CHODB_MEN_SIZE + (day - 1) * Constants.OPS5000_CHODB_DAY_SIZE + cmtdSize5k + chadSize5k + wthdSize5k + (wndd - 1) * 128 + Constants.DATA_BUFFER_512;
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


    }
}
