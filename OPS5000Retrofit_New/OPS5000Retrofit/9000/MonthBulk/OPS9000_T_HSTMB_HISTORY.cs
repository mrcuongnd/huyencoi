//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTMB_HISTORY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB HISTORY Structure
    /// </summary>
    class OPS9000_T_HSTMB_HISTORY
    {
        /// <summary>
        /// Data
        /// </summary>
        public OPS9000_T_HSTMB_DAT[] dat { get; set; }

        /// <summary>
        /// Contructer
        /// </summary>
        public OPS9000_T_HSTMB_HISTORY()
        {
            dat = new OPS9000_T_HSTMB_DAT[8];
            for (int i = 0; i < 8; i++)
            {
                dat[i] = new OPS9000_T_HSTMB_DAT();
            }
        }
    }
}
