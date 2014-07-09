//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_Year.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_Year file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System;
using System.IO;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Year Structure
    /// </summary>
    class OPS5000_Year
    {
        /// <summary>
        /// Struct index
        /// </summary>
        public OPS5000_T_HSTYB_HED indexData { get; set; }

        /// <summary>
        /// Struct men
        /// </summary>
        public OPS5000_T_HSTYB_MEN[] menData { get; set; }

        /// <summary>
        /// Init variable
        /// </summary>
        public OPS5000_Year()
        {
            indexData = new OPS5000_T_HSTYB_HED();
            menData = new OPS5000_T_HSTYB_MEN[11];
            for (int i = 0; i < 11; i++)
            {
                menData[i] = new OPS5000_T_HSTYB_MEN();
            }

        }

        /// <summary>
        /// Read data file into struct
        /// </summary>
        public void ReadData(string opsHSTYFilePath)
        {
            using (FileStream fs = new FileStream(opsHSTYFilePath, FileMode.Open, FileAccess.Read))
            {
                // Move cursor to first index in index data
                fs.Seek(256, SeekOrigin.Begin);

                // Init byte array. 101376256 total size including data and index
                byte[] bytes = new byte[8448256];

                // Read bytes
                fs.Read(bytes, 0, 8448256);

                // Read data index
                indexData.yea_s = BitConverter.ToInt16(bytes, 0);
                indexData.mon_s = BitConverter.ToInt16(bytes, 2);
                indexData.day_s = BitConverter.ToInt16(bytes, 4);
                indexData.cal_s = BitConverter.ToInt16(bytes, 6);
                indexData.hor_s = BitConverter.ToInt16(bytes, 8);
                indexData.yobi01_s[0] = BitConverter.ToInt16(bytes, 10);
                indexData.yobi01_s[1] = BitConverter.ToInt16(bytes, 12);
                indexData.yobi01_s[2] = BitConverter.ToInt16(bytes, 14);

                for (int i = 0; i < 11; i++)
                {
                    indexData.indx[i].yea_s = BitConverter.ToInt16(bytes, i * 16 + 16);
                    indexData.indx[i].mon_s = BitConverter.ToInt16(bytes, i * 16 + 18);
                    indexData.indx[i].yobi01_s[0] = BitConverter.ToInt16(bytes, i * 16 + 20);
                    indexData.indx[i].yobi01_s[1] = BitConverter.ToInt16(bytes, i * 16 + 22);
                    indexData.indx[i].men_s = BitConverter.ToInt16(bytes, i * 16 + 24); ;
                    indexData.indx[i].yobi02_s[0] = BitConverter.ToInt16(bytes, i * 16 + 26);
                    indexData.indx[i].yobi02_s[1] = BitConverter.ToInt16(bytes, i * 16 + 28);
                    indexData.indx[i].yobi02_s[2] = BitConverter.ToInt16(bytes, i * 16 + 30);
                }

                for (int i = 0; i < 32; i++)
                {
                    indexData.yobi02_s[i] = BitConverter.ToInt16(bytes, i * 2 + 192);
                }

                // Read data men
                for (int men = 0; men < 11; men++)
                {
                    for (int part = 0; part < 6000; part++)
                    {
                        for (int dat = 0; dat < 8; dat++)
                        {
                            menData[men].part[part].dat[dat].sts_s = BitConverter.ToInt16(bytes, 256 + men * 768000 + part * 128 + dat * 16);
                            menData[men].part[part].dat[dat].yobi_s = BitConverter.ToInt16(bytes, 258 + men * 768000 + part * 128 + dat * 16);
                            menData[men].part[part].dat[dat].inf_s[0] = BitConverter.ToInt16(bytes, 260 + men * 768000 + part * 128 + dat * 16);
                            menData[men].part[part].dat[dat].inf_s[1] = BitConverter.ToInt16(bytes, 262 + men * 768000 + part * 128 + dat * 16);
                            menData[men].part[part].dat[dat].dat_d = BitConverter.ToDouble(bytes, 264 + men * 768000 + part * 128 + dat * 16);
                        }
                    }
                }
            }
        }
    }
}
