//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTMB_PART.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using OPS5000Retrofit.Common;

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB PART Structure
    /// </summary>
    class OPS9000_T_HSTMB_PART
    {
        /// <summary>
        /// History
        /// </summary>
        public OPS9000_T_HSTMB_HISTORY[] history { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_HSTMB_PART()
        {
            history = new OPS9000_T_HSTMB_HISTORY[Constants.OPS9000_CNF_HIST_N];
            for (int i = 0; i < Constants.OPS9000_CNF_HIST_N; i++)
            {
                history[i] = new OPS9000_T_HSTMB_HISTORY();
            }
        }
    }
}
