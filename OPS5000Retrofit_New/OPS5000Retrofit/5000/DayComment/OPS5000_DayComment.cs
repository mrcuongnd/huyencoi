//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_DayComment.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_DayComment file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System;
using System.IO;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Day Comment Structure
    /// </summary>
    class OPS5000_DayComment
    {

        /// <summary>
        /// Struct men
        /// </summary>
        public OPS5000_T_CHODB_MEN[] menData { get; set; }

        /// <summary>
        /// Init variable
        /// </summary>
        public OPS5000_DayComment()
        {

            menData = new OPS5000_T_CHODB_MEN[121];
            for (int i = 0; i < 121; i++)
            {
                menData[i] = new OPS5000_T_CHODB_MEN();
            }

        }

        /// <summary>
        /// Read data file into struct
        /// </summary>
        public void ReadData(string opsCHODBFilePath)
        {
            using (FileStream fs = new FileStream(opsCHODBFilePath, FileMode.Open, FileAccess.Read))
            {
                // Move cursor to first index data
                fs.Seek(512, SeekOrigin.Begin);

                // Init byte array
                byte[] bytes = new byte[4380672];

                // Read bytes
                fs.Read(bytes, 0, 4380672);


                for (int men = 0; men < 1; men++)
                {
                    for (int day = 0; day < 31; day++)
                    {
                        for (int cmtd = 0; cmtd < 999; cmtd++)
                        {
                            menData[men].dayd[day].cmtd[cmtd].lng_s = BitConverter.ToInt16(bytes, men * 4380672 + day * 141312 + cmtd * 128 + 0);
                            menData[men].dayd[day].cmtd[cmtd].yobi_s = BitConverter.ToInt16(bytes, men * 4380672 + day * 141312 + cmtd * 128 + 2);
                            for (int kij_c = 0; kij_c < 124; kij_c++)
                            {
                                menData[men].dayd[day].cmtd[cmtd].kij_c[kij_c] = Convert.ToChar(bytes[men * 4380672 + day * 141312 + cmtd * 128 + 4]);
                            }
                        }

                        for (int chad = 0; chad < 99; chad++)
                        {
                            menData[men].dayd[day].chad[chad].lng_s = BitConverter.ToInt16(bytes, men * 4380672 + day * 141312 + chad * 128 + 0);
                            menData[men].dayd[day].chad[chad].yobi_s = BitConverter.ToInt16(bytes, men * 4380672 + day * 141312 + chad * 128 + 2);
                            for (int kij_c = 0; kij_c < 124; kij_c++)
                            {
                                menData[men].dayd[day].chad[chad].kij_c[kij_c] = Convert.ToChar(bytes[men * 4380672 + day * 141312 + chad * 128 + 4]);
                            }
                        }

                        for (int wthd = 0; wthd < 3; wthd++)
                        {
                            menData[men].dayd[day].wthd[wthd].lng_s = BitConverter.ToInt16(bytes, men * 4380672 + day * 141312 + wthd * 128 + 0);
                            menData[men].dayd[day].wthd[wthd].yobi_s = BitConverter.ToInt16(bytes, men * 4380672 + day * 141312 + wthd * 128 + 2);
                            for (int kij_c = 0; kij_c < 124; kij_c++)
                            {
                                menData[men].dayd[day].wthd[wthd].kij_c[kij_c] = Convert.ToChar(bytes[men * 4380672 + day * 141312 + wthd * 128 + 4]);
                            }
                        }

                        for (int wndd = 0; wndd < 3; wndd++)
                        {
                            menData[men].dayd[day].wndd[wndd].lng_s = BitConverter.ToInt16(bytes, men * 4380672 + day * 141312 + wndd * 128 + 0);
                            menData[men].dayd[day].wndd[wndd].yobi_s = BitConverter.ToInt16(bytes, men * 4380672 + day * 141312 + wndd * 128 + 2);
                            for (int kij_c = 0; kij_c < 124; kij_c++)
                            {
                                menData[men].dayd[day].wndd[wndd].kij_c[kij_c] = Convert.ToChar(bytes[men * 4380672 + day * 141312 + wndd * 128 + 4]);
                            }
                        }

                    }


                }
            }
        }
    }
}
