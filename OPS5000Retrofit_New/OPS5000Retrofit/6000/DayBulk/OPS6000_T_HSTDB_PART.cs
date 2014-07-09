//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTDB_PART.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTDB_PART file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB PART Structure
    /// </summary>
    public class OPS6000_T_HSTDB_PART
    {
        /// <summary>
        /// Part number
        /// </summary>
        public int part_no { get; set; }

        /// <summary>
        /// History list
        /// </summary>
        public List<OPS6000_T_HSTDB_HISTORY> history_lst { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTDB_PART()
        {
            this.history_lst = new List<OPS6000_T_HSTDB_HISTORY>();
        }
    }
}
