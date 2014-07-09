//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTDB_HISTORY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTDB HISTORY Structure
    /// </summary>
    public class OPS9000_T_HSTDB_HISTORY
    {

        /// <summary>
        /// List Data
        /// </summary>
        public List<OPS9000_T_HSTDB_DAT> dat_lst { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_HSTDB_HISTORY()
        {
            this.dat_lst = new List<OPS9000_T_HSTDB_DAT>();
        }
    }
}
