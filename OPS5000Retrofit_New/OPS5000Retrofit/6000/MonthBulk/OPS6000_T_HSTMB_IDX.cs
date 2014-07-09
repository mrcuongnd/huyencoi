//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTMB_IDX.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTMB_IDX file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB IDX Structure
    /// </summary>
    class OPS6000_T_HSTMB_IDX
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
        /// Yobi 01
        /// </summary>
        public short[] yobi01_s { get; set; }

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
        public OPS6000_T_HSTMB_IDX()
        {
            yea_s = 0;
            mon_s = 0;
            yobi01_s = new short[2];
            yobi01_s[0] = new short();
            yobi01_s[1] = new short();
            men_s = 0;
            prt_s = 0;
            yobi02_s = new short[2];
            yobi02_s[0] = new short();
            yobi02_s[1] = new short();
        }
    }
}
