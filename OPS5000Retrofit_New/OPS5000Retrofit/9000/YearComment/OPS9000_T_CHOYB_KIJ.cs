//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_CHOYB_KIJ.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHOYB KIJ Structure
    /// </summary>
    class OPS9000_T_CHOYB_KIJ
    {
        /// <summary>
        /// Length data
        /// </summary>
        public short lng_s { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public short yobi_s { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public byte[] kij_c { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_CHOYB_KIJ()
        {
            lng_s = 0;
            yobi_s = 0;
            kij_c = new byte[124];
        }
    }
}
