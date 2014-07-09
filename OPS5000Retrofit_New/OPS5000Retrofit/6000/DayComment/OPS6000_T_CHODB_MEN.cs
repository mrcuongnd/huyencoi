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
    /// CHODB MEN Structure
    /// </summary>
    class OPS6000_T_CHODB_MEN
    {
        /// <summary>
        /// Day
        /// </summary>
        public OPS6000_T_CHODB_DAY[] dayd { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_CHODB_MEN()
        {
            dayd = new OPS6000_T_CHODB_DAY[31];
            for (int i = 0; i < 31; i++)
            {
                dayd[i] = new OPS6000_T_CHODB_DAY();
            }
        }
    }
}
