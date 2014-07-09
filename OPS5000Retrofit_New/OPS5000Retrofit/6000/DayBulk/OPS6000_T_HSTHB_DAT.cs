//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTDB_DAT.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTDB_DAT file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB DAT Structure
    /// </summary>
    public class OPS6000_T_HSTDB_DAT
    {
        /// <summary>
        /// Status
        /// </summary>
        public short sts_s { get; set; }

        /// <summary>
        /// Yobi 01
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
        public OPS6000_T_HSTDB_DAT()
        {
            this.inf_s = new short[2];
        }
    }
}
