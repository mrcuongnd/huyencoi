//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_HSTYB_MEN.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_HSTYB_MEN file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTYB MEN Structure
    /// </summary>
    class OPS6000_T_HSTYB_MEN
    {
        /// <summary>
        /// Part
        /// </summary>
        public OPS6000_T_HSTYB_PART[] part { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_HSTYB_MEN()
        {
            part = new OPS6000_T_HSTYB_PART[4608];
            for (int i = 0; i < 4608; i++)
            {
                part[i] = new OPS6000_T_HSTYB_PART();
            }
        }
    }
}
