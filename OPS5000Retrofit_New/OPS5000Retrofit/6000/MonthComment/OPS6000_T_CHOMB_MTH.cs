//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_CHOMB_MTH.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_CHOMB_MTH file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHOMB MTH Structure
    /// </summary>
    class OPS6000_T_CHOMB_MTH
    {
        /// <summary>
        /// Comment
        /// </summary>
        public OPS6000_T_CHOMB_KIJ[] cmtm { get; set; }

        /// <summary>
        /// Character
        /// </summary>
        public OPS6000_T_CHOMB_KIJ[] cham { get; set; }

        /// <summary>
        /// Weather
        /// </summary>
        public OPS6000_T_CHOMB_KIJ[] wthm { get; set; }

        /// <summary>
        /// Wind
        /// </summary>
        public OPS6000_T_CHOMB_KIJ[] wndm { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public int[] yobi_i { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_CHOMB_MTH()
        {
            cmtm = new OPS6000_T_CHOMB_KIJ[192];
            for (int i = 0; i < 192; i++)
            {
                cmtm[i] = new OPS6000_T_CHOMB_KIJ();
            }
            cham = new OPS6000_T_CHOMB_KIJ[9];
            for (int i = 0; i < 9; i++)
            {
                cham[i] = new OPS6000_T_CHOMB_KIJ();
            }
            wthm = new OPS6000_T_CHOMB_KIJ[3];
            for (int i = 0; i < 3; i++)
            {
                wthm[i] = new OPS6000_T_CHOMB_KIJ();
            }
            wndm = new OPS6000_T_CHOMB_KIJ[3];
            for (int i = 0; i < 3; i++)
            {
                wndm[i] = new OPS6000_T_CHOMB_KIJ();
            }
            yobi_i = new int[32];
        }
    }
}
