﻿//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTDB_HED.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTDB_HED file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB HED Structure
    /// </summary>
    public class OPS6000_T_HSTDB_HED
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
        public List<OPS6000_T_HSTDB_IDX> indx { get; set; }

        /// <summary>
        /// Change day
        /// </summary>
        public short chgday_s { get; set; }

        /// <summary>
        /// Yobi 02
        /// </summary>
        public short[] yobi02_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTDB_HED()
        {
            this.yobi01_s = new short[3];
            this.yobi02_s = new short[47];
        }
    }
}
