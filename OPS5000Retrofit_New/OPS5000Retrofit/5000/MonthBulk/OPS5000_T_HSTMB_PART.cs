//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTMB_PART.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTMB_PART file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB PART Structure
    /// </summary>
    class OPS5000_T_HSTMB_PART
    {
        /// <summary>
        /// History
        /// </summary>
        public OPS5000_T_HSTMB_HISTORY[] history { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_HSTMB_PART()
        {
            history = new OPS5000_T_HSTMB_HISTORY[6000];
            for (int i = 0; i < 6000; i++)
            {
                history[i] = new OPS5000_T_HSTMB_HISTORY();
            }
        }
    }
}
