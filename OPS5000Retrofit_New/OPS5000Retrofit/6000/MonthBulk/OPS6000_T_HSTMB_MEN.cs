//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTMB_MEN.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTMB_MEN file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTMB MEN Structure
    /// </summary>
    class OPS6000_T_HSTMB_MEN
    {
        /// <summary>
        /// Part
        /// </summary>
        public OPS6000_T_HSTMB_PART[] part { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTMB_MEN()
        {
            part = new OPS6000_T_HSTMB_PART[12];
            for (int i = 0; i < 12; i++)
            {
                part[i] = new OPS6000_T_HSTMB_PART();
            }
        }
    }
}
