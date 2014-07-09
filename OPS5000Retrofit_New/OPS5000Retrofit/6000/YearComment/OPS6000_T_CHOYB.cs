//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_CHOYB.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_CHOYB file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit.YearComment
{
    /// <summary>
    /// CHOYB Structure
    /// </summary>
    class OPS6000_T_CHOYB
    {
        /// <summary>
        /// Buffer
        /// </summary>
        public short[] buffer { get; set; }

        /// <summary>
        /// HOYB year
        /// </summary>
        public OPS6000_T_CHOYB_YEA[] T_CHOYB_yea { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_CHOYB()
        {
            buffer = new short[512];
            T_CHOYB_yea = new OPS6000_T_CHOYB_YEA[3];
            for (int i = 0; i < 3; i++)
            {
                T_CHOYB_yea[i] = new OPS6000_T_CHOYB_YEA();
            }
        }
    }
}
