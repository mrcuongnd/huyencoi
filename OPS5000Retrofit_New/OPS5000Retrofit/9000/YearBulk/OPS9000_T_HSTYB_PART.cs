//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTYB_PART.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTYB PART Structure
    /// </summary>
    class OPS9000_T_HSTYB_PART
    {
        /// <summary>
        /// Data
        /// </summary>
        public OPS9000_T_HSTYB_DAT[] dat { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_HSTYB_PART()
        {
            dat = new OPS9000_T_HSTYB_DAT[8];
            for (int i = 0; i < 8; i++)
            {
                dat[i] = new OPS9000_T_HSTYB_DAT();
            }
        }
    }
}
