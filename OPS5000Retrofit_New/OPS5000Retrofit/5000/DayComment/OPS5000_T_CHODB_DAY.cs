//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_CHODB_DAY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_CHODB_DAY file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHODB DAY Structure
    /// </summary>
    class OPS5000_T_CHODB_DAY
    {
        /// <summary>
        /// Comment
        /// </summary>
        public OPS5000_T_CHODB_KIJ[] cmtd { get; set; }

        /// <summary>
        /// Character
        /// </summary>
        public OPS5000_T_CHODB_KIJ[] chad { get; set; }

        /// <summary>
        /// Weather
        /// </summary>
        public OPS5000_T_CHODB_KIJ[] wthd { get; set; }

        /// <summary>
        /// Wind
        /// </summary>
        public OPS5000_T_CHODB_KIJ[] wndd { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_CHODB_DAY()
        {
            cmtd = new OPS5000_T_CHODB_KIJ[999];
            for (int i = 0; i < 999; i++)
            {
                cmtd[i] = new OPS5000_T_CHODB_KIJ();
            }
            chad = new OPS5000_T_CHODB_KIJ[99];
            for (int i = 0; i < 99; i++)
            {
                chad[i] = new OPS5000_T_CHODB_KIJ();
            }
            wthd = new OPS5000_T_CHODB_KIJ[3];
            for (int i = 0; i < 3; i++)
            {
                wthd[i] = new OPS5000_T_CHODB_KIJ();
            }
            wndd = new OPS5000_T_CHODB_KIJ[3];
            for (int i = 0; i < 3; i++)
            {
                wndd[i] = new OPS5000_T_CHODB_KIJ();
            }
        }
    }
}
