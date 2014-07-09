//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_CHODB_DAY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_CHODB_DAY file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// Month Structure
    /// </summary>
    class OPS6000_T_CHODB_DAY
    {
        /// <summary>
        /// Comment
        /// </summary>
        public OPS6000_T_CHODB_KIJ[] cmtd { get; set; }

        /// <summary>
        /// Character
        /// </summary>
        public OPS6000_T_CHODB_KIJ[] chad { get; set; }

        /// <summary>
        /// Weather
        /// </summary>
        public OPS6000_T_CHODB_KIJ[] wthd { get; set; }

        /// <summary>
        /// Wind
        /// </summary>
        public OPS6000_T_CHODB_KIJ[] wndd { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public int[] yobi_i { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_CHODB_DAY()
        {
            cmtd = new OPS6000_T_CHODB_KIJ[192];
            for (int i = 0; i < 192; i++)
            {
                cmtd[i] = new OPS6000_T_CHODB_KIJ();
            }
            chad = new OPS6000_T_CHODB_KIJ[9];
            for (int i = 0; i < 9; i++)
            {
                chad[i] = new OPS6000_T_CHODB_KIJ();
            }
            wthd = new OPS6000_T_CHODB_KIJ[3];
            for (int i = 0; i < 3; i++)
            {
                wthd[i] = new OPS6000_T_CHODB_KIJ();
            }
            wndd = new OPS6000_T_CHODB_KIJ[3];
            for (int i = 0; i < 3; i++)
            {
                wndd[i] = new OPS6000_T_CHODB_KIJ();
            }
            yobi_i = new int[32];
        }
    }
}
