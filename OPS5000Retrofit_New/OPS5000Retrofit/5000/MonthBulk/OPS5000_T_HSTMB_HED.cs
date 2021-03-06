﻿//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTMB_HED.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTMB_HED file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB HED Structure
    /// </summary>
    class OPS5000_T_HSTMB_HED
    {
        /// <summary>
        /// Year
        /// </summary>
        public short yea_s { get; set; }

        /// <summary>
        /// Months
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
        /// Yobi 01
        /// </summary>
        public short[] yobi01_s { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        public OPS6000_T_HSTMB_IDX[] indx { get; set; }

        /// <summary>
        /// Monthly Time
        /// </summary>
        public short chgmon_s { get; set; }

        /// <summary>
        /// Yobi 02
        /// </summary>
        public short[] yobi02_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_HSTMB_HED()
        {
            yea_s = 0;
            mon_s = 0;
            day_s = 0;
            cal_s = 0;
            hor_s = 0;
            yobi01_s = new short[3];
            indx = new OPS6000_T_HSTMB_IDX[11];
            for (int i = 0; i < 11; i++)
            {
                indx[i] = new OPS6000_T_HSTMB_IDX();
            }
            chgmon_s = 0;
            yobi02_s = new short[31];
        }
    }
}
