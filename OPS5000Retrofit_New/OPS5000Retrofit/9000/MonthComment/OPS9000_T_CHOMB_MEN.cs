//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_CHOMB_MEN.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHOMB MEN Structure
    /// </summary>
    class OPS9000_T_CHOMB_MEN
    {
        /// <summary>
        /// Month men
        /// </summary>
        public OPS9000_T_CHOMB_MTH[] mthm { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_CHOMB_MEN()
        {
            for (int i = 0; i < 12; i++)
            {
                mthm[i] = new OPS9000_T_CHOMB_MTH();
            }
        }
    }
}
