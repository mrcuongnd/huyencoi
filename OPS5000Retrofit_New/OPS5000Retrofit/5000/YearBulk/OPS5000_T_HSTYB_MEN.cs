//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_HSTYB_MEN.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_HSTYB_MEN file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTYB MEN Structure
    /// </summary>
    class OPS5000_T_HSTYB_MEN
    {
        /// <summary>
        /// Part
        /// </summary>
        public OPS5000_T_HSTYB_PART[] part { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_HSTYB_MEN()
        {
            part = new OPS5000_T_HSTYB_PART[6000];
            for (int i = 0; i < 6000; i++)
            {
                part[i] = new OPS5000_T_HSTYB_PART();
            }
        }
    }
}
