//////////////////////////////////////////////////////////////////////
// File Name     ：DSVSDataCommentItem.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：DSVSDataCommentItem file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

namespace OPS5000Retrofit
{
    /// <summary>
    /// DSVS Data Comment Item Structure
    /// </summary>
    public class DSVSDataCommentItem
    {
        /// <summary>
        /// Conment
        /// </summary>
        public string[] cmt { get; set; }

        /// <summary>
        /// Character
        /// </summary>
        public string[] cha { get; set; }

        /// <summary>
        /// Weather
        /// </summary>
        public string[] wth { get; set; }

        /// <summary>
        /// Wind
        /// </summary>
        public string[] wnd { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public DSVSDataCommentItem()
        {
            cmt = new string[192];
            cha = new string[9];
            wth = new string[3];
            wnd = new string[3];
        }
    }
}
