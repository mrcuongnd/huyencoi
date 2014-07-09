//////////////////////////////////////////////////////////////////////
// File Name     ：DSVSUnitBulkData.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：DSVSUnitBulkData file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// DSVS Unit Bulk Data Structure
    /// </summary>
    public class DSVSUnitBulkData
    {
        /// <summary>
        /// Status
        /// </summary>
        public short sts_s { get; set; }

        /// <summary>
        /// Yobi 01
        /// </summary>
        public short yobi1_s { get; set; }

        /// <summary>
        /// Yobi 02
        /// </summary>
        public short yobi2_s { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public double dat_d { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public DSVSUnitBulkData()
        {

        }
    }
}
