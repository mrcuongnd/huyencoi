//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTHB_PART.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTHB_PART file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTHB PART Structure
    /// </summary>
    public class OPS5000_T_HSTHB_PART
    {
        /// <summary>
        /// Part number
        /// </summary>
        public int part_no { get; set; }

        /// <summary>
        /// History list
        /// </summary>
        public List<OPS5000_T_HSTHB_DAT> history_lst { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_HSTHB_PART()
        {
            this.history_lst = new List<OPS5000_T_HSTHB_DAT>();
        }
    }
}
