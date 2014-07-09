//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTDB_DAT.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB DAT Structure
    /// </summary>
    public class OPS9000_T_HSTDB_DAT
    {
        /// <summary>
        ///  Status
        /// </summary>
        public short sts_s { get; set; }

        /// <summary>
        /// Reseve
        /// </summary>
        public short yobi_s { get; set; }

        /// <summary>
        /// Other info
        /// </summary>
        public short[] inf_s { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public double dat_d { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_HSTDB_DAT()
        {
            this.inf_s = new short[2];
        }
    }
}
