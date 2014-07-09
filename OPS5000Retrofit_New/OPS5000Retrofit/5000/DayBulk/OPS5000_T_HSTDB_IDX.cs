//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTDB_IDX.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTDB_IDX file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB index structure
    /// </summary>
    public class OPS5000_T_HSTDB_IDX
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
        /// Last day of month
        /// </summary>
        public short lst_s { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public short yobi01_s { get; set; }

        /// <summary>
        /// Men No
        /// </summary>
        public short men_s { get; set; }

        /// <summary>
        /// Part No
        /// </summary>
        public short prt_s { get; set; }

        /// <summary>
        /// Yobi 02
        /// </summary>
        public short[] yobi02_s { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OPS5000_T_HSTDB_IDX()
        {
            this.yobi02_s = new short[2];
        }
    }
}
