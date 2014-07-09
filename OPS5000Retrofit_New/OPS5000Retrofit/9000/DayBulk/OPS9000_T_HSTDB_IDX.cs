﻿//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTDB_IDX.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB IDX Structure
    /// </summary>
    public class OPS9000_T_HSTDB_IDX
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
        /// The last day
        /// </summary>
        public short lst_s { get; set; }

        /// <summary>
        /// Yobi 01
        /// </summary>
        public short yobi01_s { get; set; }

        /// <summary>
        /// Men
        /// </summary>
        public short men_s { get; set; }

        /// <summary>
        /// Part
        /// </summary>
        public short prt_s { get; set; }

        /// <summary>
        /// Yobi 02
        /// </summary>
        public short[] yobi02_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_HSTDB_IDX()
        {
            this.yobi02_s = new short[2];
        }
    }
}
