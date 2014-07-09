//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTMB_DAT.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTMB_DAT file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB DAT Structure
    /// </summary>
    class OPS6000_T_HSTMB_DAT
    {
        /// <summary>
        /// Status
        /// </summary>
        public short sts_s { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public short yobi_s { get; set; }

        /// <summary>
        /// Information
        /// </summary>
        public short[] inf_s { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public double dat_d { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTMB_DAT()
        {
            sts_s = 0;
            yobi_s = 0;
            inf_s = new short[2];
            dat_d = 0;
        }
    }
}
