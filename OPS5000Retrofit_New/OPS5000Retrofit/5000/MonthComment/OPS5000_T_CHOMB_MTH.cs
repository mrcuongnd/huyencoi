//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_Month.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_Month file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHOMB MTH Structure
    /// </summary>
    class OPS5000_T_CHOMB_MTH
    {
        /// <summary>
        /// Comment
        /// </summary>
        public OPS5000_T_CHOMB_KIJ[] cmtm { get; set; }

        /// <summary>
        /// Character
        /// </summary>
        public OPS5000_T_CHOMB_KIJ[] cham { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public int[] yobi_i { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_CHOMB_MTH()
        {
            cmtm = new OPS5000_T_CHOMB_KIJ[999];
            for (int i = 0; i < 999; i++)
            {
                cmtm[i] = new OPS5000_T_CHOMB_KIJ();
            }
            cham = new OPS5000_T_CHOMB_KIJ[99];
            for (int i = 0; i < 99; i++)
            {
                cham[i] = new OPS5000_T_CHOMB_KIJ();
            }
            yobi_i = new int[64];
        }
    }
}
