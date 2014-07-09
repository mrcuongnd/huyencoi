//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTMB_HED.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB HED Structure
    /// </summary>
    class OPS9000_T_HSTMB_HED
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
        /// Day of week
        /// </summary>
        public short cal_s { get; set; }

        /// <summary>
        /// Hour
        /// </summary>
        public short hor_s { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public short[] yobi01_s { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        public OPS9000_T_HSTMB_IDX[] indx { get; set; }

        /// <summary>
        /// Change month
        /// </summary>
        public short chgmon_s { get; set; }

        /// <summary>
        /// Yobi 02
        /// </summary>
        public short[] yobi02_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_HSTMB_HED()
        {
            yea_s = 0;
            mon_s = 0;
            day_s = 0;
            cal_s = 0;
            hor_s = 0;
            yobi01_s = new short[3];
            // S_HSTMB_PMDMX_P = 6
            indx = new OPS9000_T_HSTMB_IDX[6];
            for (int i = 0; i < 6; i++)
            {
                indx[i] = new OPS9000_T_HSTMB_IDX();
            }
            chgmon_s = 0;
            yobi02_s = new short[199];
        }
    }
}
