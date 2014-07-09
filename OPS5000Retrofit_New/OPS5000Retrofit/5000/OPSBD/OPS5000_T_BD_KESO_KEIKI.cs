//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000_T_BD_KESO_KEIKI.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000_T_BD_KESO_KEIKI file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// BD KESO KEIKI Structure
    /// </summary>
    public class OPS5000_T_BD_KESO_KEIKI
    {
        /// <summary>
        /// Keiki number
        /// </summary>
        public int keiki_no { get; set; }

        /// <summary>
        /// Tag number
        /// </summary>
        public string tgno_c { get; set; }

        /// <summary>
        /// Tag name
        /// </summary>
        public string tgnm_c { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public OPS5000_T_BD_KESO_KEIKI()
        {

        }
    }
}
