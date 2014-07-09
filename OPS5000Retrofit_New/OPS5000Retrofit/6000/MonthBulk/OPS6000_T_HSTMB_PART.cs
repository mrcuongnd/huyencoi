//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTMB_PART.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTMB_PART file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    class OPS6000_T_HSTMB_PART
    {
        /// <summary>
        /// History
        /// </summary>
        public OPS6000_T_HSTMB_HISTORY[] history { get; set; }

        /// <summary>
        /// Contructer
        /// </summary>
        public OPS6000_T_HSTMB_PART()
        {
            history = new OPS6000_T_HSTMB_HISTORY[4608];
            for (int i = 0; i < 4608; i++)
            {
                history[i] = new OPS6000_T_HSTMB_HISTORY();
            }
        }
    }
}
