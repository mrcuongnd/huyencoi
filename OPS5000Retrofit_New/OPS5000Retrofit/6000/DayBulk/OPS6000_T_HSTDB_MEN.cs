﻿//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTDB_MEN.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTDB_MEN file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB MEN Structure
    /// </summary>
    public class OPS6000_T_HSTDB_MEN
    {
        /// <summary>
        /// Men number
        /// </summary>
        public int men_no { get; set; }

        /// <summary>
        /// Part list
        /// </summary>
        public List<OPS6000_T_HSTDB_PART> part_lst { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTDB_MEN()
        {
            this.part_lst = new List<OPS6000_T_HSTDB_PART>();
        }
    }
}
