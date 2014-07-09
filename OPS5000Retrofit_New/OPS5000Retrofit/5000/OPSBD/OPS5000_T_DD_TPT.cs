//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_DD_TPT.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_DD_TPT file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// DD TPT Structure
    /// </summary>
    public class OPS5000_T_DD_TPT
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
        /// History file
        /// </summary>
        public short tpt_s { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_DD_TPT()
        {

        }
    }
}
