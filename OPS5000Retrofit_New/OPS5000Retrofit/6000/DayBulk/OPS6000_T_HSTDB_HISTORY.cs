//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTDB_HISTORY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTDB_HISTORY file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB HISTORY Structure
    /// </summary>
    public class OPS6000_T_HSTDB_HISTORY
    {
        /// <summary>
        /// Data list
        /// </summary>
        public List<OPS6000_T_HSTDB_DAT> data_lst { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTDB_HISTORY()
        {
            this.data_lst = new List<OPS6000_T_HSTDB_DAT>();
        }
    }
}
