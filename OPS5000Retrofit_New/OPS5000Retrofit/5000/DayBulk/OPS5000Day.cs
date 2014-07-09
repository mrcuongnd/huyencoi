//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000Day.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000Day file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using OPS5000Retrofit.Common;

namespace OPS5000Retrofit
{
    /// <summary>
    /// OPS5000 HSTDB handle class
    /// </summary>
    public class OPS5000Day
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OPS5000Day()
        {
        }

        #region Read Method
        /// <summary>
        /// Get all men no
        /// </summary>
        /// <param name="dataIndex">Data part</param>
        /// <returns>Maping table</returns>
        public Dictionary<string, string> GetAllMenNo(OPS5000_T_HSTDB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS5000_T_HSTDB_IDX index in dataIndex.indx)
            {
                string yearMonthKey = Utility.CreateYearMonthKey(index.yea_s, index.mon_s);
                targetMenList.Add(yearMonthKey, index.men_s.ToString());
            }

            return targetMenList;
        }

        /// <summary>
        /// Get men no by date time
        /// </summary>
        /// <param name="dataTime">Date time</param>
        /// <param name="indexPartData">index part</param>
        /// <returns>Men no</returns>
        public short GetMenNoByTime(string dataTime, OPS5000_T_HSTDB_HED indexPartData)
        {
            short menNo = -1;
            string[] separateTime = dataTime.Split(new char[] { '/' });
            foreach (OPS5000_T_HSTDB_IDX index in indexPartData.indx)
            {
                if (index.yea_s == Int16.Parse(separateTime[0]) &&
                    (index.mon_s == Int16.Parse(separateTime[1])))
                {
                    menNo = index.men_s;
                }
            }
            return menNo;
        }

        /// <summary>
        /// Get list men no by date time
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="dataIndex"></param>
        /// <returns>Mapping table</returns>
        public Dictionary<string, string> GetMenNoListByTime(DateTime startDateTime, DateTime endDateTime, OPS5000_T_HSTDB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS5000_T_HSTDB_IDX index in dataIndex.indx)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, 1);
                if (Utility.InRange(indexDateTime, startDateTime, endDateTime))
                {
                    targetMenList.Add(Utility.CreateYearMonthKey(index.yea_s, index.mon_s), index.men_s.ToString());
                }
            }

            return targetMenList;
        }

        /// <summary>
        /// Get index data part
        /// </summary>
        /// <param name="inputFilename">File name</param>
        /// <returns>OPS5000_T_HSTDB_HED</returns>
        public OPS5000_T_HSTDB_HED GetIndexPart(string inputFilename)
        {
            List<OPS5000_T_HSTDB_IDX> indexDataList = new List<OPS5000_T_HSTDB_IDX>();
            short[] yobi = new short[47];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                // Set pointer to read
                fs.Seek(256, SeekOrigin.Begin);

                // Read data
                byte[] b = new byte[2048];
                fs.Read(b, 0, 2048);

                // Get index data
                OPS5000_T_HSTDB_HED indexPart = new OPS5000_T_HSTDB_HED();
                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);

                for (int i = 1; i <= Constants.OPS5000_S_HSTDB_MAXMEN_P; i++)
                {
                    OPS5000_T_HSTDB_IDX idx = new OPS5000_T_HSTDB_IDX();
                    idx.yea_s = BitConverter.ToInt16(b, i * 16);
                    idx.mon_s = BitConverter.ToInt16(b, i * 16 + 2);
                    idx.lst_s = BitConverter.ToInt16(b, i * 16 + 4);
                    idx.yobi01_s = BitConverter.ToInt16(b, i * 16 + 6);
                    idx.men_s = BitConverter.ToInt16(b, i * 16 + 8);
                    idx.prt_s = BitConverter.ToInt16(b, i * 16 + 10);
                    idx.yobi02_s = Utility.GetShortArray4(b, i * 16 + 12);

                    indexDataList.Add(idx);
                }
                indexPart.indx = indexDataList;

                // Get daily time
                byte[] dailyByte = new byte[2];
                dailyByte[0] = b[1952];
                dailyByte[1] = b[1953];
                indexPart.chgday_s = BitConverter.ToInt16(dailyByte, 0);

                // Get yobi
                byte[] yobiByte = new byte[94];

                for (int i = 0; i < 47; i++)
                {
                    yobi[i] = BitConverter.ToInt16(b, 1954 + i * 2);
                }
                indexPart.yobi02_s = yobi;

                // Return result
                return indexPart;
            }
        }
        #endregion

        #region Write File Method
        /// <summary>
        /// Write data to HSTDB file from OPS6K
        /// </summary>
        /// <param name="menNo">Men No</param>
        /// <param name="partNo">Part No</param>
        /// <param name="historyMapping">History Mapping Table</param>
        /// <param name="inputFilename">HSTDB file path</param>
        /// <param name="partData">Data</param>
        public void WriteDayData(int menNo, int partNo, Dictionary<string, string> historyMapping, string inputFilename, List<OPS6000_T_HSTDB_HISTORY> partData)
        {
            // Caculate part size
            long partSize = Constants.OPS5000_CNF_HIST_N * 80;

            // Caculate men size
            long menSize = partSize * Constants.OPS5000_TOTAL_PART_IN_MEN_DAY;

            // Caculte start index
            long startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + Constants.OPS5000_DAY_DATA_PART_START_INDEX;

            // Write data
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Write))
            {
                foreach (KeyValuePair<string, string> item in historyMapping)
                {
                    int historyId6K = Int32.Parse(item.Key.ToString());
                    int historyId5K = Int32.Parse(item.Value.ToString());
                    OPS6000_T_HSTDB_HISTORY amount = partData[historyId6K - 1];

                    fs.Seek(startIndex + (historyId5K - 1) * 80, SeekOrigin.Begin);
                    byte[] b = ConvertToByteArray(amount);
                    fs.Write(b, 0, 80);
                }
            }
        }

        /// <summary>
        /// Write to HSTDB file from DSVS data
        /// </summary>
        /// <param name="menNo">Men No</param>
        /// <param name="partNo">Part No</param>
        /// <param name="historyId">History Id</param>
        /// <param name="inputFilename">File Name</param>
        /// <param name="amount">Data</param>
        public void WriteDayDataFromCSV(long menNo, long partNo, long historyId, string inputFilename, OPS5000_T_HSTDB_HISTORY amount)
        {
            // Caculate part size
            long partSize = Constants.OPS5000_CNF_HIST_N * 80;

            // Caculate men size
            long menSize = partSize * Constants.OPS5000_TOTAL_PART_IN_MEN_DAY;

            // Get start index
            long startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + (historyId - 1) * 80 + Constants.OPS5000_DAY_DATA_PART_START_INDEX;

            // Write data to file
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Write))
            {
                fs.Seek(startIndex, SeekOrigin.Begin);
                byte[] b = ConvertToByteArray(amount);
                fs.Write(b, 0, 80);
            }
        }

        /// <summary>
        /// Write to HSTDB file from OPS9000 data
        /// </summary>
        /// <param name="menNo">Men No</param>
        /// <param name="partNo">Part No</param>
        /// <param name="historyMapping">History Mapping</param>
        /// <param name="inputFilename">HSTDB file path</param>
        /// <param name="partData">Data</param>
        public void WriteDayData9K(int menNo, int partNo, Dictionary<string, string> historyMapping, string inputFilename, List<OPS9000_T_HSTDB_HISTORY> partData)
        {
            // Caculate part size
            long partSize = Constants.OPS5000_CNF_HIST_N * 80;

            // Caculate men size
            long menSize = partSize * Constants.OPS5000_TOTAL_PART_IN_MEN_DAY;

            // Get start index
            long startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + Constants.OPS5000_DAY_DATA_PART_START_INDEX;

            // Write file
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Write))
            {
                foreach (KeyValuePair<string, string> item in historyMapping)
                {
                    int historyId9K = Int32.Parse(item.Key.ToString());
                    int historyId5K = Int32.Parse(item.Value.ToString());
                    OPS9000_T_HSTDB_HISTORY amount = partData[historyId9K - 1];

                    fs.Seek(startIndex + (historyId5K - 1) * 80, SeekOrigin.Begin);
                    byte[] b = ConvertToByteArray9K(amount);
                    fs.Write(b, 0, 80);
                }
            }
        }
        #endregion

        #region Private method
        /// <summary>
        /// Get information data
        /// </summary>
        /// <param name="arr">Byte array</param>
        /// <param name="startIdx">Start index</param>
        /// <returns>Short array</returns>
        private short[] GetAddictionInfo(byte[] arr, int startIdx)
        {
            short[] ret = new short[2];

            ret[0] = BitConverter.ToInt16(arr, startIdx);
            ret[1] = BitConverter.ToInt16(arr, startIdx + 2);

            return ret;
        }

        /// <summary>
        /// Convert history data 9K to byte array
        /// </summary>
        /// <param name="amount">OPS9000_T_HSTDB_HISTORY</param>
        /// <returns>Byte array</returns>
        private byte[] ConvertToByteArray9K(OPS9000_T_HSTDB_HISTORY amount)
        {
            byte[] b = new byte[80];
            byte[] b1 = new byte[16];
            byte[] b2 = new byte[16];
            byte[] b3 = new byte[16];
            byte[] b4 = new byte[16];
            byte[] b5 = new byte[16];

            b1 = ConvertToByteArrayUnit9K(amount.dat_lst[0]);
            b2 = ConvertToByteArrayUnit9K(amount.dat_lst[1]);
            b3 = ConvertToByteArrayUnit9K(amount.dat_lst[2]);
            b4 = ConvertToByteArrayUnit9K(amount.dat_lst[3]);
            b5 = ConvertToByteArrayUnit9K(amount.dat_lst[4]);

            for (int i = 0; i < 16; i++)
            {
                b[i] = b1[i];
            }

            for (int i = 16; i < 32; i++)
            {
                b[i] = b2[i - 16];
            }

            for (int i = 32; i < 48; i++)
            {
                b[i] = b3[i - 32];
            }

            for (int i = 48; i < 64; i++)
            {
                b[i] = b4[i - 48];
            }
            for (int i = 64; i < 80; i++)
            {
                b[i] = b5[i - 64];
            }
            return b;
        }

        /// <summary>
        /// Convert history unit data 9K to byte array
        /// </summary>
        /// <param name="amount">OPS9000_T_HSTDB_DAT</param>
        /// <returns>Byte array</returns>
        private byte[] ConvertToByteArrayUnit9K(OPS9000_T_HSTDB_DAT amount)
        {
            byte[] b = new byte[16];

            byte[] status = BitConverter.GetBytes(amount.sts_s);
            byte[] yobi_s = BitConverter.GetBytes(amount.yobi_s);
            byte[] hour = BitConverter.GetBytes(amount.inf_s[0]);
            byte[] minute = BitConverter.GetBytes(amount.inf_s[1]);
            byte[] data = BitConverter.GetBytes(amount.dat_d);

            b[0] = status[0];
            b[1] = status[1];
            b[2] = yobi_s[0];
            b[3] = yobi_s[1];
            b[4] = hour[0];
            b[5] = hour[1];
            b[6] = minute[0];
            b[7] = minute[1];
            b[8] = data[0];
            b[9] = data[1];
            b[10] = data[2];
            b[11] = data[3];
            b[12] = data[4];
            b[13] = data[5];
            b[14] = data[6];
            b[15] = data[7];

            return b;
        }

        /// <summary>
        /// Convert history data 6K to byte array
        /// </summary>
        /// <param name="amount">OPS6000_T_HSTDB_HISTORY</param>
        /// <returns>Byte array</returns>
        private byte[] ConvertToByteArray(OPS6000_T_HSTDB_HISTORY amount)
        {
            byte[] b = new byte[80];
            byte[] b1 = new byte[16];
            byte[] b2 = new byte[16];
            byte[] b3 = new byte[16];
            byte[] b4 = new byte[16];
            byte[] b5 = new byte[16];

            b1 = ConvertToByteArrayUnit(amount.data_lst[0]);
            b2 = ConvertToByteArrayUnit(amount.data_lst[1]);
            b3 = ConvertToByteArrayUnit(amount.data_lst[2]);
            b4 = ConvertToByteArrayUnit(amount.data_lst[3]);
            b5 = ConvertToByteArrayUnit(amount.data_lst[4]);

            for (int i = 0; i < 16; i++)
            {
                b[i] = b1[i];
            }

            for (int i = 16; i < 32; i++)
            {
                b[i] = b2[i - 16];
            }

            for (int i = 32; i < 48; i++)
            {
                b[i] = b3[i - 32];
            }

            for (int i = 48; i < 64; i++)
            {
                b[i] = b4[i - 48];
            }
            for (int i = 64; i < 80; i++)
            {
                b[i] = b5[i - 64];
            }
            return b;
        }

        /// <summary>
        /// Convert history unit data 6K to byte array
        /// </summary>
        /// <param name="amount">OPS6000_T_HSTDB_DAT</param>
        /// <returns>Byte Array</returns>
        private byte[] ConvertToByteArrayUnit(OPS6000_T_HSTDB_DAT amount)
        {
            byte[] b = new byte[16];

            byte[] status = BitConverter.GetBytes(amount.sts_s);
            byte[] yobi_s = BitConverter.GetBytes(amount.yobi_s);
            byte[] hour = BitConverter.GetBytes(amount.inf_s[0]);
            byte[] minute = BitConverter.GetBytes(amount.inf_s[1]);
            byte[] data = BitConverter.GetBytes(amount.dat_d);

            b[0] = status[0];
            b[1] = status[1];
            b[2] = yobi_s[0];
            b[3] = yobi_s[1];
            b[4] = hour[0];
            b[5] = hour[1];
            b[6] = minute[0];
            b[7] = minute[1];
            b[8] = data[0];
            b[9] = data[1];
            b[10] = data[2];
            b[11] = data[3];
            b[12] = data[4];
            b[13] = data[5];
            b[14] = data[6];
            b[15] = data[7];

            return b;
        }

        /// <summary>
        /// Convert history data 5K to byte array
        /// </summary>
        /// <param name="amount">OPS5000_T_HSTDB_HISTORY</param>
        /// <returns>Byte Array</returns>
        private byte[] ConvertToByteArray(OPS5000_T_HSTDB_HISTORY amount)
        {
            byte[] b = new byte[80];
            byte[] b1 = new byte[16];
            byte[] b2 = new byte[16];
            byte[] b3 = new byte[16];
            byte[] b4 = new byte[16];
            byte[] b5 = new byte[16];

            b1 = ConvertToByteArrayUnit(amount.history_lst[0]);
            b2 = ConvertToByteArrayUnit(amount.history_lst[1]);
            b3 = ConvertToByteArrayUnit(amount.history_lst[2]);
            b4 = ConvertToByteArrayUnit(amount.history_lst[3]);
            b5 = ConvertToByteArrayUnit(amount.history_lst[4]);

            for (int i = 0; i < 16; i++)
            {
                b[i] = b1[i];
            }

            for (int i = 16; i < 32; i++)
            {
                b[i] = b2[i - 16];
            }

            for (int i = 32; i < 48; i++)
            {
                b[i] = b3[i - 32];
            }

            for (int i = 48; i < 64; i++)
            {
                b[i] = b4[i - 48];
            }
            for (int i = 64; i < 80; i++)
            {
                b[i] = b5[i - 64];
            }
            return b;
        }

        /// <summary>
        /// Convert history unit data 5K to byte array
        /// </summary>
        /// <param name="amount">OPS5000_T_HSTDB_DAT</param>
        /// <returns>Byte Array</returns>
        private byte[] ConvertToByteArrayUnit(OPS5000_T_HSTDB_DAT amount)
        {
            byte[] b = new byte[16];

            byte[] status = BitConverter.GetBytes(amount.sts_s);
            byte[] yobi_s = BitConverter.GetBytes(amount.yobi_s);
            byte[] inf_s1 = BitConverter.GetBytes(amount.inf_s[0]);
            byte[] inf_s2 = BitConverter.GetBytes(amount.inf_s[1]);
            byte[] data = BitConverter.GetBytes(amount.dat_d);

            b[0] = status[0];
            b[1] = status[1];
            b[2] = yobi_s[0];
            b[3] = yobi_s[1];
            b[4] = inf_s1[0];
            b[5] = inf_s1[1];
            b[6] = inf_s2[0];
            b[7] = inf_s2[1];
            b[8] = data[0];
            b[9] = data[1];
            b[10] = data[2];
            b[11] = data[3];
            b[12] = data[4];
            b[13] = data[5];
            b[14] = data[6];
            b[15] = data[7];

            return b;
        }
        #endregion
    }
}
