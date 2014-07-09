//////////////////////////////////////////////////////////////////////
// File Name     ：DSVSCommentDataItem.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：DSVSCommentDataItem file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// DSVS Comment Data Item Structure
    /// </summary>
    public class DSVSCommentDataItem
    {
        /// <summary>
        /// Conmment
        /// </summary>
        public string[] cmt { get; set; }

        /// <summary>
        /// Character
        /// </summary>
        public string[] cha { get; set; }

        /// <summary>
        /// Wearther
        /// </summary>
        public string[] wth { get; set; }

        /// <summary>
        /// Wind
        /// </summary>
        public string[] wnd { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public DSVSCommentDataItem()
        {
            cmt = new string[192];
            cha = new string[9];
            wth = new string[3];
            wnd = new string[3];
        }
    }
}
