//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTYB_HED.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTYB_HED file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTYB HED Structure
    /// </summary>
    class OPS5000_T_HSTYB_HED
    {
        /// <summary>
        /// Year
        /// </summary>
        public short yea_s { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public short mon_s { get; set; }

        /// <summary>
        /// Day
        /// </summary>
        public short day_s { get; set; }

        /// <summary>
        /// Calendar
        /// </summary>
        public short cal_s { get; set; }

        /// <summary>
        /// Hour
        /// </summary>
        public short hor_s { get; set; }

        /// <summary>
        /// yobi 01
        /// </summary>
        public short[] yobi01_s { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        public OPS6000_T_HSTYB_IDX[] indx { get; set; }

        /// <summary>
        /// Yobi 02
        /// </summary>
        public short[] yobi02_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_HSTYB_HED()
        {
            yea_s = 0;
            mon_s = 0;
            day_s = 0;
            cal_s = 0;
            hor_s = 0;
            yobi01_s = new short[3];
            for (int i = 0; i < 3; i++)
            {
                yobi01_s[i] = new short();
            }
            indx = new OPS6000_T_HSTYB_IDX[11];
            for (int i = 0; i < 11; i++)
            {
                indx[i] = new OPS6000_T_HSTYB_IDX();
            }
            yobi02_s = new short[32];
            for (int i = 0; i < 32; i++)
            {
                yobi02_s[i] = new short();
            }
        }
    }
}
