//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_HSTYB_MEN.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////


namespace OPS5000Retrofit
{
    /// <summary>
    /// HSTYB MEN Structure
    /// </summary>
    class OPS9000_T_HSTYB_MEN
    {
        /// <summary>
        /// Part
        /// </summary>
        public OPS9000_T_HSTYB_PART[] part { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_HSTYB_MEN()
        {
            part = new OPS9000_T_HSTYB_PART[6048];
            for (int i = 0; i < 6048; i++)
            {
                part[i] = new OPS9000_T_HSTYB_PART();
            }
        }
    }
}
