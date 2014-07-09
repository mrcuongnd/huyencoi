//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_CHOYB_YEA.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit.YearComment
{
    /// <summary>
    /// CHOYB YEA Structure
    /// </summary>
    class OPS9000_T_CHOYB_YEA
    {
        /// <summary>
        /// List comment data
        /// </summary>
        public OPS9000_T_CHOYB_KIJ[] cmty { get; set; }

        /// <summary>
        /// List charator data
        /// </summary>
        public OPS9000_T_CHOYB_KIJ[] chay { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public int[] yobi_i { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_CHOYB_YEA()
        {
            cmty = new OPS9000_T_CHOYB_KIJ[192];
            for (int i = 0; i < 192; i++)
            {
                cmty[i] = new OPS9000_T_CHOYB_KIJ();
            }
            chay = new OPS9000_T_CHOYB_KIJ[10];
            for (int i = 0; i < 10; i++)
            {
                chay[i] = new OPS9000_T_CHOYB_KIJ();
            }
            yobi_i = new int[192];
        }
    }
}
