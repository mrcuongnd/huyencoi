//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_CHODB_MEN.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// CHODB MEN Structure
    /// </summary>
    class OPS9000_T_CHODB_MEN
    {
        /// <summary>
        /// List Day
        /// </summary>
        public OPS9000_T_CHODB_DAY[] dayd { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_CHODB_MEN()
        {
            // Init List Day by Month - 31 day
            dayd = new OPS9000_T_CHODB_DAY[31];
            for (int i = 0; i < 31; i++)
            {
                dayd[i] = new OPS9000_T_CHODB_DAY();
            }
        }
    }
}
