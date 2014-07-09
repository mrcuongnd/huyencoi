//////////////////////////////////////////////////////////////////////
// File Name     ：Constants.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Constants file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////
namespace OPS5000Retrofit.Common
{
    /// <summary>
    /// Constants class
    /// </summary>
    public static class Constants
    {
        #region Others
        // Date format
        public const string YYYYMMDD_HHMMSS_SLASH = "yyyy/MM/dd HH:mm:ss";

        // Max line in log file
        public const int S_ELOGREC_N = 1000;

        // Max line size in log file
        public const int S_ELOGSIZ_P = 256;

        // SHIFT-JIS Encodinng
        public const int SHIFT_JIS_ENCODING = 932;

        // Log file name
        public const string ERROR_FILE_NAME = "Errlog.txt";

        public const int DATA_BUFFER_512 = 512;
        #endregion

        #region OPS6000 Constants
        /// <summary>
        /// OPS6000_CNF_HIST_N = 4608
        /// </summary>
        public const int OPS6000_CNF_HIST_N = 4608;

        /// <summary>
        /// OPS6000_DAY_DATA_PART_START_INDEX = 1024
        /// </summary>
        public const int OPS6000_DAY_DATA_PART_START_INDEX = 1024;

        /// <summary>
        /// OPS6000_HOUR_DATA_PART_START_INDEX = 2048;
        /// </summary>
        public const int OPS6000_HOUR_DATA_PART_START_INDEX = 2048;

        /// <summary>
        /// OPS6000_TOTAL_PART_IN_MEN_HOUR = 24
        /// </summary>
        public const int OPS6000_TOTAL_PART_IN_MEN_HOUR = 24;

        /// <summary>
        /// OPS6000_TOTAL_PART_IN_MEN_DAY = 31
        /// </summary>
        public const int OPS6000_TOTAL_PART_IN_MEN_DAY = 31;

        /// <summary>
        /// OPS6000_S_HSTDB_MAXMEN_P = 25
        /// </summary>
        public const int OPS6000_S_HSTDB_MAXMEN_P = 25;

        /// <summary>
        /// OPS6000_TOTAL_PART_IN_MEN_MONTH = 12
        /// </summary>
        public const int OPS6000_TOTAL_PART_IN_MEN_MONTH = 12;

        /// <summary>
        /// OPS6000_MONTH_DATA_PART_START_INDEX = 1024
        /// </summary>
        public const int OPS6000_MONTH_DATA_PART_START_INDEX = 1024;

        /// <summary>
        /// OPS6000_CHOMB_MEN_SIZE = 319488
        /// </summary>
        public const int OPS6000_CHOMB_MEN_SIZE = 319488;

        /// <summary>
        /// OPS6000_CHODB_MEN_SIZE = 825344
        /// </summary>
        public const int OPS6000_CHODB_MEN_SIZE = 825344;

        /// <summary>
        /// OPS6000_CHOYB_MEN_SIZE = 26624
        /// </summary>
        public const int OPS6000_CHOYB_MEN_SIZE = 26624;

        /// <summary>
        /// OPS6000_CHOMB_MONTH_SIZE = 26624
        /// </summary>
        public const int OPS6000_CHOMB_MONTH_SIZE = 26624;

        /// <summary>
        /// OPS6000_CHODB_DAY_SIZE = 26624
        /// </summary>
        public const int OPS6000_CHODB_DAY_SIZE = 26624;
        #endregion

        #region OPS5000 Constants
        /// <summary>
        /// OPS5000_CNF_HIST_N = 6000
        /// </summary>
        public const int OPS5000_CNF_HIST_N = 6000;

        /// <summary>
        /// OPS5000_S_HSTHB_MAXMEN_P = 3654
        /// </summary>
        public const int OPS5000_S_HSTHB_MAXMEN_P = 3654;

        /// <summary>
        /// OPS5000_HOUR_DATA_PART_START_INDEX = 58880
        /// </summary>
        public const long OPS5000_HOUR_DATA_PART_START_INDEX = 58880;

        /// <summary>
        /// OPS5000_DAY_DATA_PART_START_INDEX = 2304
        /// </summary>
        public const int OPS5000_DAY_DATA_PART_START_INDEX = 2304;

        /// <summary>
        /// OPS5000_MONTH_DATA_PART_START_INDEX = 512
        /// </summary>
        public const int OPS5000_MONTH_DATA_PART_START_INDEX = 512;

        /// <summary>
        /// OPS5000_YEAR_DATA_PART_START_INDEX = 512
        /// </summary>
        public const int OPS5000_YEAR_DATA_PART_START_INDEX = 512;

        /// <summary>
        /// OPS5000_TOTAL_PART_IN_MEN_HOUR = 24
        /// </summary>
        public const int OPS5000_TOTAL_PART_IN_MEN_HOUR = 24;

        /// <summary>
        /// OPS5000_TOTAL_PART_IN_MEN_DAY = 31
        /// </summary>
        public const int OPS5000_TOTAL_PART_IN_MEN_DAY = 31;

        /// <summary>
        /// OPS5000_S_HSTDB_MAXMEN_P = 121
        /// </summary>
        public const int OPS5000_S_HSTDB_MAXMEN_P = 121;

        /// <summary>
        /// OPS5000_TOTAL_PART_IN_MEN_MONTH = 12
        /// </summary>
        public const int OPS5000_TOTAL_PART_IN_MEN_MONTH = 12;

        /// <summary>
        /// OPS5000_STATION_NO = 8
        /// </summary>
        public const int OPS5000_STATION_NO = 8;

        /// <summary>
        /// OPS5000_CHOMB_MEN_SIZE = 1689600
        /// </summary>
        public const int OPS5000_CHOMB_MEN_SIZE = 1689600;

        /// <summary>
        /// OPS5000_CHODB_MEN_SIZE = 4380672
        /// </summary>
        public const int OPS5000_CHODB_MEN_SIZE = 4380672;

        /// <summary>
        /// OPS5000_CHOYB_MEN_SIZE = 140800
        /// </summary>
        public const int OPS5000_CHOYB_MEN_SIZE = 140800;

        /// <summary>
        /// OPS5000_CHOMB_MONTH_SIZE = 140800
        /// </summary>
        public const int OPS5000_CHOMB_MONTH_SIZE = 140800;

        /// <summary>
        /// OPS5000_CHODB_DAY_SIZE = 141312
        /// </summary>
        public const int OPS5000_CHODB_DAY_SIZE = 141312;
        #endregion

        #region OPS9000 Constants
        /// <summary>
        /// OPS9000_S_HSTHB_MAXMEN_P = 185
        /// </summary>
        public const int OPS9000_S_HSTHB_MAXMEN_P = 185;

        /// <summary>
        /// OPS9000_HOUR_DATA_PART_START_INDEX = 3584
        /// </summary>
        public const int OPS9000_HOUR_DATA_PART_START_INDEX = 3584;

        /// <summary>
        /// OPS9000_CNF_HIST_N = 6048
        /// </summary>
        public const int OPS9000_CNF_HIST_N = 6048;

        /// <summary>
        /// OPS9000_TOTAL_PART_IN_MEN_HOUR = 24
        /// </summary>
        public const int OPS9000_TOTAL_PART_IN_MEN_HOUR = 24;

        /// <summary>
        /// OPS9000_TOTAL_PART_IN_MEN_DAY = 31
        /// </summary>
        public const int OPS9000_TOTAL_PART_IN_MEN_DAY = 31;

        /// <summary>
        /// OPS9000_S_HSTDB_MAXMEN_P = 25
        /// </summary>
        public const int OPS9000_S_HSTDB_MAXMEN_P = 25;

        /// <summary>
        /// OPS9000_DAY_DATA_PART_START_INDEX = 1024
        /// </summary>
        public const int OPS9000_DAY_DATA_PART_START_INDEX = 1024;

        /// <summary>
        /// OPS9000_S_HSTMB_PMDMX_P = 6
        /// </summary>
        public const int OPS9000_S_HSTMB_PMDMX_P = 6;

        /// <summary>
        /// OPS9000_YEAR_DATA_PART_START_INDEX = 1024
        /// </summary>
        public const int OPS9000_YEAR_DATA_PART_START_INDEX = 1024;

        /// <summary>
        /// OPS9000_S_HSTYB_MAXMEN_P = 6
        /// </summary>
        public const int OPS9000_S_HSTYB_MAXMEN_P = 6;

        /// <summary>
        /// OPS9000_CHODB_MEN_SIZE = 825344
        /// </summary>
        public const int OPS9000_CHODB_MEN_SIZE = 825344;

        /// <summary>
        /// OPS9000_CHODB_DAY_SIZE = 26624
        /// </summary>
        public const int OPS9000_CHODB_DAY_SIZE = 26624;

        /// <summary>
        /// OPS9000_CHOMB_MEN_SIZE = 319488
        /// </summary>
        public const int OPS9000_CHOMB_MEN_SIZE = 319488;

        /// <summary>
        /// OPS9000_CHOYB_MEN_SIZE = 26624
        /// </summary>
        public const int OPS9000_CHOYB_MEN_SIZE = 26624;

        /// <summary>
        /// OPS9000_CNF_MAXCMT_N = 192
        /// </summary>
        public const int OPS9000_CNF_MAXCMT_N = 192;

        /// <summary>
        /// OPS9000_CNF_MAXCHA_N = 10
        /// </summary>
        public const int OPS9000_CNF_MAXCHA_N = 10;

        /// <summary>
        /// OPS9000_CNF_MAXWTH_N = 3
        /// </summary>
        public const int OPS9000_CNF_MAXWTH_N = 3;

        /// <summary>
        /// OPS9000_CNF_MAXWND_N = 3
        /// </summary>
        public const int OPS9000_CNF_MAXWND_N = 3;

        /// <summary>
        /// OPS9000_CHOMB_MONTH_SIZE = 26624
        /// </summary>
        public const int OPS9000_CHOMB_MONTH_SIZE = 26624;
        #endregion

        #region DSVS Constants
        /// <summary>
        /// DSVS_NUMBER_OF_HOUR = 24
        /// </summary>
        public const int DSVS_NUMBER_OF_HOUR = 24;

        /// <summary>
        /// DSVS_NUMBER_OF_DAY = 31
        /// </summary>
        public const int DSVS_NUMBER_OF_DAY = 31;

        /// <summary>
        /// DSVS_NUMBER_OF_MONTH = 12
        /// </summary>
        public const int DSVS_NUMBER_OF_MONTH = 12;

        /// <summary>
        /// DSVS_NUMBER_OF_YEAR = 1
        /// </summary>
        public const int DSVS_NUMBER_OF_YEAR = 1;
        #endregion

        #region Error Message
        /// <summary>
        /// File open failed message
        /// </summary>
        public const string FILE_OPEN_FAIL = "{0}ファイルのオープンに失敗しました。";

        /// <summary>
        /// TagNo not exists message
        /// </summary>
        public const string TAGNO_NOT_EXISTS = "移行先システムにタグ番号が存在しません。（{0}）";
        #endregion
    }
}
