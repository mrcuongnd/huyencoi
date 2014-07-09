//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTYB_IDX.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTYB IDX Structure
    /// </summary>
    class OPS9000_T_HSTYB_IDX
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
        /// Yobi 02
        /// </summary>
        public short[] yobi02_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_HSTYB_IDX()
        {
            yea_s = 0;
            mon_s = 0;
            yobi01_s = new short[2];
            for (int i = 0; i < 2; i++)
            {
                yobi01_s[i] = new short();
            }
            men_s = 0;
            yobi02_s = new short[3];
            for (int i = 0; i < 3; i++)
            {
                yobi02_s[i] = new short();
            }
        }

    }
}
