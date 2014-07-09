//////////////////////////////////////////////////////////////////////
// File Name     ：OPS6000_T_STATION.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS6000_T_STATION file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// STATION Structure
    /// </summary>
    public class OPS6000_T_STATION
    {
        /// <summary>
        /// Station number
        /// </summary>
        public int station_no { get; set; }

        /// <summary>
        /// Keiki list
        /// </summary>
        public List<OPS6000_T_BD_KESO_KEIKI> keiki_lst { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS6000_T_STATION()
        {
            this.keiki_lst = new List<OPS6000_T_BD_KESO_KEIKI>();
        }
    }

}
