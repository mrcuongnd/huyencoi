﻿//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTHB_IDX.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTHB_IDX file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTHB IDX Structure
    /// </summary>
    public class OPS6000_T_HSTHB_IDX
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
        /// Men
        /// </summary>
        public short men_s { get; set; }

        /// <summary>
        /// Part
        /// </summary>
        public short prt_s { get; set; }

        /// <summary>
        /// Yobi 01
        /// </summary>
        public short[] yobi01_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTHB_IDX()
        {

        }
    }
}
