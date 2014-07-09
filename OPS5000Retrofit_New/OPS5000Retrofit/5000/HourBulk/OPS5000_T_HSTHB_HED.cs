//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTHB_HED.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTHB_HED file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTHB HED Structure
    /// </summary>
    public class OPS5000_T_HSTHB_HED
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
        /// Yobi 01
        /// </summary>
        public short[] yobi01_s { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        public List<OPS5000_T_HSTHB_IDX> indx { get; set; }

        /// <summary>
        /// Daily time
        /// </summary>
        public short chghor_s { get; set; }

        /// <summary>
        /// Yobi 02
        /// </summary>
        public short[] yobi02_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_HSTHB_HED()
        {
            this.yobi01_s = new short[3];
            this.yobi02_s = new short[71];
        }
    }
}
