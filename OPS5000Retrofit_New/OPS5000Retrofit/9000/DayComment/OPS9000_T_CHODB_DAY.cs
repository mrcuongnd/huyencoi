//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_CHODB_DAY.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using OPS5000Retrofit.Common;

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHODB DAY Structure
    /// </summary>
    class OPS9000_T_CHODB_DAY
    {
        /// <summary>
        /// Comment
        /// </summary>
        public OPS9000_T_CHODB_KIJ[] cmtd { get; set; }

        /// <summary>
        /// Character
        /// </summary>
        public OPS9000_T_CHODB_KIJ[] chad { get; set; }

        /// <summary>
        /// Weather
        /// </summary>
        public OPS9000_T_CHODB_KIJ[] wthd { get; set; }

        /// <summary>
        /// Wind
        /// </summary>
        public OPS9000_T_CHODB_KIJ[] wndd { get; set; }

        /// <summary>
        /// Yobi 
        /// </summary>
        public int[] yobi_i { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_CHODB_DAY()
        {
            // Init 192 unit for comment text
            cmtd = new OPS9000_T_CHODB_KIJ[Constants.OPS9000_CNF_MAXCMT_N];
            for (int i = 0; i < Constants.OPS9000_CNF_MAXCMT_N; i++)
            {
                cmtd[i] = new OPS9000_T_CHODB_KIJ();
            }

            // Init 10 unit for chad text
            chad = new OPS9000_T_CHODB_KIJ[Constants.OPS9000_CNF_MAXCHA_N];
            for (int i = 0; i < Constants.OPS9000_CNF_MAXCHA_N; i++)
            {
                chad[i] = new OPS9000_T_CHODB_KIJ();
            }

            // Init 3 unit for weather text
            wthd = new OPS9000_T_CHODB_KIJ[Constants.OPS9000_CNF_MAXWTH_N];
            for (int i = 0; i < Constants.OPS9000_CNF_MAXWTH_N; i++)
            {
                wthd[i] = new OPS9000_T_CHODB_KIJ();
            }

            // Init 3 unit for wind text
            wndd = new OPS9000_T_CHODB_KIJ[Constants.OPS9000_CNF_MAXWND_N];
            for (int i = 0; i < Constants.OPS9000_CNF_MAXWND_N; i++)
            {
                wndd[i] = new OPS9000_T_CHODB_KIJ();
            }
            yobi_i = new int[0];
        }
    }
}
