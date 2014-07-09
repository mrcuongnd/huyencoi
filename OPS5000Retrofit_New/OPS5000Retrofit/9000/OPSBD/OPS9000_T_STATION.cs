//////////////////////////////////////////////////////////////////////
// File Name     ：OPS9000_T_STATION.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace OPS5000Retrofit
{
    /// <summary>
    /// STATION Structure
    /// </summary>
    public class OPS9000_T_STATION
    {
        /// <summary>
        /// Station number
        /// </summary>
        public int station_no { get; set; }

        /// <summary>
        /// Keiki list
        /// </summary>
        public List<OPS9000_T_BD_KESO_KEIKI> keiki_lst { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS9000_T_STATION()
        {
            keiki_lst = new List<OPS9000_T_BD_KESO_KEIKI>();
        }
    }
}
