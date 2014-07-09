//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_CHOYB_KIJ.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_CHOYB_KIJ file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHOYB KIJ Structure
    /// </summary>
    class OPS6000_T_CHOYB_KIJ
    {
        /// <summary>
        /// Length
        /// </summary>
        public short lng_s { get; set; }

        /// <summary>
        /// Yobi 
        /// </summary>
        public short yobi_s { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public char[] kij_c { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_CHOYB_KIJ()
        {
            lng_s = 0;
            yobi_s = 0;
            kij_c = new char[124];
        }
    }
}
