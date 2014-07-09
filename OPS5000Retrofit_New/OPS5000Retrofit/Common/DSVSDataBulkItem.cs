//////////////////////////////////////////////////////////////////////
// File Name     ：DSVSDataBulkItem.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：DSVSDataBulkItem file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// DSVS Data Bulk Item Structure
    /// </summary>
    class DSVSDataBulkItem
    {
        /// <summary>
        /// Status
        /// </summary>
        public short[] status { get; set; }

        /// <summary>
        /// Information 1
        /// </summary>
        public short[] inf1 { get; set; }

        /// <summary>
        /// Information 2
        /// </summary>
        public short[] inf2 { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public double[] data { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="countItem"></param>
        public DSVSDataBulkItem(int countItem)
        {
            status = new short[countItem];
            inf1 = new short[countItem];
            inf2 = new short[countItem];
            data = new double[countItem];
        }

    }
}
