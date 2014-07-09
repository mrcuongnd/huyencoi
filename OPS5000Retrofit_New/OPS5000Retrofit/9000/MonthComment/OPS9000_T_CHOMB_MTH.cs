//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_CHOMB_MTH.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHOMB MTH Structure
    /// </summary>
    class OPS9000_T_CHOMB_MTH
    {
        /// <summary>
        /// List comment data
        /// </summary>
        public OPS9000_T_CHOMB_KIJ[] cmtm { get; set; }

        /// <summary>
        /// List Character data
        /// </summary>
        public OPS9000_T_CHOMB_KIJ[] cham { get; set; }

        /// <summary>
        /// List Weather data
        /// </summary>
        public OPS9000_T_CHOMB_KIJ[] wthm { get; set; }

        /// <summary>
        /// List wind data
        /// </summary>
        public OPS9000_T_CHOMB_KIJ[] wndm { get; set; }

        /// <summary>
        /// Yobi
        /// </summary>
        public int[] yobi_i { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_CHOMB_MTH()
        {
            // Init 192 unit comment data
            cmtm = new OPS9000_T_CHOMB_KIJ[192];
            for (int i = 0; i < 192; i++)
            {
                cmtm[i] = new OPS9000_T_CHOMB_KIJ();
            }

            // Init 10 unit charator data
            cham = new OPS9000_T_CHOMB_KIJ[10];
            for (int i = 0; i < 10; i++)
            {
                cham[i] = new OPS9000_T_CHOMB_KIJ();
            }

            // Init 3 unit weather data
            wthm = new OPS9000_T_CHOMB_KIJ[3];
            for (int i = 0; i < 3; i++)
            {
                wthm[i] = new OPS9000_T_CHOMB_KIJ();
            }

            // Init 3 unit wind data
            wndm = new OPS9000_T_CHOMB_KIJ[3];
            for (int i = 0; i < 3; i++)
            {
                wndm[i] = new OPS9000_T_CHOMB_KIJ();
            }
            yobi_i = new int[0];
        }
    }
}
