//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_CHOYB.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit.YearComment
{
    /// <summary>
    /// CHOYB Structure
    /// </summary>
    class OPS9000_T_CHOYB
    {
        /// <summary>
        /// Buffer
        /// </summary>
        public short[] buffer { get; set; }

        /// <summary>
        /// CHOYB year
        /// </summary>
        public OPS9000_T_CHOYB_YEA[] T_CHOYB_yea { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_CHOYB()
        {
            buffer = new short[512];
            T_CHOYB_yea = new OPS9000_T_CHOYB_YEA[6];
            for (int i = 0; i < 6; i++)
            {
                T_CHOYB_yea[i] = new OPS9000_T_CHOYB_YEA();
            }
        }
    }
}
