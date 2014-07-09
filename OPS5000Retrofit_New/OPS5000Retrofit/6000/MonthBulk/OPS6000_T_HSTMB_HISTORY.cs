//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTMB_HISTORY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTMB_HISTORY file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB HISTORY Structure
    /// </summary>
    class OPS6000_T_HSTMB_HISTORY
    {
        /// <summary>
        /// Data
        /// </summary>
        public OPS6000_T_HSTMB_DAT[] dat { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTMB_HISTORY()
        {
            dat = new OPS6000_T_HSTMB_DAT[8];
            for (int i = 0; i < 8; i++)
            {
                dat[i] = new OPS6000_T_HSTMB_DAT();
            }
        }
    }
}
