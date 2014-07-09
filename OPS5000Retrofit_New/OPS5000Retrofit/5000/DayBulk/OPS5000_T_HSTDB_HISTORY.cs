//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTDB_HISTORY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTDB_HISTORY file
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
    public class OPS5000_T_HSTDB_HISTORY
    {
        /// <summary>
        /// History list Structure
        /// </summary>
        public List<OPS5000_T_HSTDB_DAT> history_lst { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OPS5000_T_HSTDB_HISTORY()
        {
            this.history_lst = new List<OPS5000_T_HSTDB_DAT>();
        }
    }
}
