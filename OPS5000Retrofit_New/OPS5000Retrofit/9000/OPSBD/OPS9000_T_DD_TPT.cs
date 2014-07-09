//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_DD_TPT.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// DD_TPT Structure
    /// </summary>
    public class OPS9000_T_DD_TPT
    {
        /// <summary>
        /// History id
        /// </summary>
        public int history_id { get; set; }

        /// <summary>
        /// Status number
        /// </summary>
        public short stno_s { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public short tpt_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_DD_TPT()
        {

        }
    }
}
