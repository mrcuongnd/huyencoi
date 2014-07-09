//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTMB_HISTORY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTMB_HISTORY file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB HISTORY Structure
    /// </summary>
    class OPS5000_T_HSTMB_HISTORY
    {
        /// <summary>
        /// Data
        /// </summary>
        public OPS5000_T_HSTMB_DAT[] dat { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_HSTMB_HISTORY()
        {
            dat = new OPS5000_T_HSTMB_DAT[8];
            for (int i = 0; i < 8; i++)
            {
                dat[i] = new OPS5000_T_HSTMB_DAT();
            }
        }
    }
}
