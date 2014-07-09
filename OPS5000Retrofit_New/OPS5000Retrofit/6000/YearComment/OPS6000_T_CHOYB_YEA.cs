//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_CHOYB_YEA.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_CHOYB_YEA file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit.YearComment
{
    /// <summary>
    /// CHOYB YEA Structure
    /// </summary>
    class OPS6000_T_CHOYB_YEA
    {
        /// <summary>
        /// Comment year
        /// </summary>
        public OPS6000_T_CHOYB_KIJ[] cmty { get; set; }

        /// <summary>
        /// Character year
        /// </summary>
        public OPS6000_T_CHOYB_KIJ[] chay { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public int[] yobi_i { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_CHOYB_YEA()
        {
            cmty = new OPS6000_T_CHOYB_KIJ[192];
            for (int i = 0; i < 192; i++)
            {
                cmty[i] = new OPS6000_T_CHOYB_KIJ();
            }
            chay = new OPS6000_T_CHOYB_KIJ[9];
            for (int i = 0; i < 9; i++)
            {
                chay[i] = new OPS6000_T_CHOYB_KIJ();
            }
            yobi_i = new int[224];
        }
    }
}
