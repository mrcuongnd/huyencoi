//////////////////////////////////////////////////////////////////////
// File Name     ：OPS5000Hour.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPS5000Hour file
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
    /// OPS5000 HSTHB handle class
    /// </summary>
    public class OPS5000Hour
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OPS5000Hour() { }

        #region Read data method
        /// <summary>
        /// Read data part by men no and part no
        /// </summary>
        /// <param name="menNo">Men No</param>
        /// <param name="partNo">Part no</param>
        /// <param name="inputFilename">File Name</param>
        /// <returns>List data</returns>
        public List<OPS5000_T_HSTHB_DAT> ReadDataPart(int menNo, int partNo, string inputFilename)
        {
            List<OPS5000_T_HSTHB_DAT> historyList = new List<OPS5000_T_HSTHB_DAT>();

            // Read file
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                // Caculate part size
                int partSize = Constants.OPS5000_CNF_HIST_N * 16;

                // Caculate men size
                int menSize = partSize * Constants.OPS5000_S_HSTHB_MAXMEN_P;

                // Caculate start index
                long startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + Constants.OPS5000_HOUR_DATA_PART_START_INDEX;

                // Read file
                fs.Seek(startIndex, SeekOrigin.Begin);
                byte[] partData = new byte[partSize];
                fs.Read(partData, 0, partSize);

                // Get data from byte array
                for (int i = 0; i < Constants.OPS5000_CNF_HIST_N; i++)
                {
                    OPS5000_T_HSTHB_DAT amount = new OPS5000_T_HSTHB_DAT();
                    amount.sts_s = BitConverter.ToInt16(partData, i * 16);
                    amount.yobi_s = BitConverter.ToInt16(partData, i * 16 + 2);
                    amount.inf_s = GetAddictionInfo(partData, i * 16 + 4);
                    amount.dat_d = BitConverter.ToDouble(partData, i * 16 + 8);
                    historyList.Add(amount);
                }
            }
            return historyList;
        }

        /// <summary>
        /// Read history
        /// </summary>
        /// <param name="historyID">History Id</param>
        /// <param name="historyList">History List</param>
        /// <returns>Data</returns>
        public OPS5000_T_HSTHB_DAT ReadAmountData(int historyID, List<OPS5000_T_HSTHB_DAT> historyList)
        {
            return historyList[historyID - 1];
        }

        /// <summary>
        /// Get all men no
        /// </summary>
        /// <param name="dataIndex">Data part</param>
        /// <returns>Men No Table</returns>
        public Dictionary<string, string> GetAllMenNo(OPS5000_T_HSTHB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS5000_T_HSTHB_IDX index in dataIndex.indx)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s);
                targetMenList.Add(indexDateTime.ToShortDateString(), index.men_s.ToString());
            }

            return targetMenList;
        }

        /// <summary>
        /// Get men no by date time
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetMenNoListByTime(DateTime startDateTime, DateTime endDateTime, OPS5000_T_HSTHB_HED dataIndex)
        {
            Dictionary<string, string> targetMenList = new Dictionary<string, string>();
            foreach (OPS5000_T_HSTHB_IDX index in dataIndex.indx)
            {
                DateTime indexDateTime = Utility.CreateDateTime(index.yea_s, index.mon_s, index.day_s);
                if (Utility.InRange(indexDateTime, startDateTime, endDateTime))
                {
                    targetMenList.Add(indexDateTime.ToShortDateString(), index.men_s.ToString());
                }
            }

            return targetMenList;
        }

        /// <summary>
        /// Read index part data
        /// </summary>
        /// <param name="inputFilename">HSTHB file name</param>
        /// <returns>OPS5000_T_HSTHB_HED</returns>
        public OPS5000_T_HSTHB_HED GetIndexPart(string inputFilename)
        {
            List<OPS5000_T_HSTHB_IDX> indexDataList = new List<OPS5000_T_HSTHB_IDX>();
            short[] yobi = new short[142];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                // Set start pointer
                fs.Seek(256, SeekOrigin.Begin);

                // Read to byte array
                byte[] b = new byte[58480];
                fs.Read(b, 0, 58480);

                // Get header data
                OPS5000_T_HSTHB_HED indexPart = new OPS5000_T_HSTHB_HED();
                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);

                // Get index data
                for (int i = 1; i <= 3654; i++)
                {
                    OPS5000_T_HSTHB_IDX idx = new OPS5000_T_HSTHB_IDX();
                    idx.yea_s = BitConverter.ToInt16(b, i * 16);
                    idx.mon_s = BitConverter.ToInt16(b, i * 16 + 2);
                    idx.day_s = BitConverter.ToInt16(b, i * 16 + 4);
                    idx.cal_s = BitConverter.ToInt16(b, i * 16 + 6);
                    idx.men_s = BitConverter.ToInt16(b, i * 16 + 8);
                    idx.prt_s = BitConverter.ToInt16(b, i * 16 + 10);
                    idx.yobi01_s = Utility.GetShortArray4(b, i * 16 + 12);

                    indexDataList.Add(idx);
                }
                indexPart.indx = indexDataList;

                // Get daily time
                byte[] dailyByte = new byte[2];
                fs.Seek(58736, SeekOrigin.Begin);
                fs.Read(dailyByte, 0, 2);
                indexPart.chghor_s = BitConverter.ToInt16(dailyByte, 0); ;

                // Get yobi
                byte[] yobiByte = new byte[142];
                fs.Seek(58738, SeekOrigin.Begin);
                fs.Read(yobiByte, 0, 142);

                for (int i = 0; i < 71; i++)
                {
                    yobi[i] = BitConverter.ToInt16(yobiByte, i * 2);
                }
                indexPart.yobi02_s = yobi;

                return indexPart;
            }
        }

        /// <summary>
        /// Get men no by time
        /// </summary>
        /// <param name="dataTime"></param>
        /// <param name="indexPartData"></param>
        /// <returns></returns>
        public short GetMenNoByTime(string dataTime, OPS5000_T_HSTHB_HED indexPartData)
        {
            short menNo = -1;
            string[] separateTime = dataTime.Split(new char[] { '/' });
            foreach (OPS5000_T_HSTHB_IDX index in indexPartData.indx)
            {
                if (index.yea_s == Int16.Parse(separateTime[0]) &&
                    (index.mon_s == Int16.Parse(separateTime[1])) &&
                        (index.day_s == Int16.Parse(separateTime[2])))
                {
                    menNo = index.men_s;
                }
            }
            return menNo;
        }
        #endregion

        #region Write data method
        /// <summary>
        /// Write to HSTHB file from OPS6000
        /// </summary>
        /// <param name="menNo">Men No</param>
        /// <param name="partNo">Part No</param>
        /// <param name="historyMapping">History Mapping</param>
        /// <param name="inputFilename">HSTHB file path</param>
        /// <param name="partData">Data</param>
        public void WriteHourData6K(int menNo, int partNo, Dictionary<string, string> historyMapping, string inputFilename, List<OPS6000_T_HSTHB_DAT> partData)
        {
            // Caculate part size
            long partSize = Constants.OPS5000_CNF_HIST_N * 16;

            // Caculate men size
            long menSize = partSize * Constants.OPS5000_TOTAL_PART_IN_MEN_HOUR;

            // Caculate start index
            long startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + Constants.OPS5000_HOUR_DATA_PART_START_INDEX;

            // Write data to file
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Write))
            {
                foreach (KeyValuePair<string, string> item in historyMapping)
                {
                    int historyId6K = Int32.Parse(item.Key.ToString());
                    int historyId5K = Int32.Parse(item.Value.ToString());
                    OPS6000_T_HSTHB_DAT amount = partData[historyId6K - 1];

                    fs.Seek(startIndex + (historyId5K - 1) * 16, SeekOrigin.Begin);
                    byte[] b = ConvertToByteArray6K(amount);
                    fs.Write(b, 0, 16);
                }
            }
        }

        /// <summary>
        /// Write to HSTHB file from OPS9000
        /// </summary>
        /// <param name="menNo">Men No</param>
        /// <param name="partNo">Part No</param>
        /// <param name="historyMapping">History Mapping</param>
        /// <param name="inputFilename">HSTHB file path</param>
        /// <param name="partData">Data</param>
        public void WriteHourData9K(int menNo, int partNo, Dictionary<string, string> historyMapping, string inputFilename, List<OPS9000_T_HSTHB_DAT> partData)
        {
            // Caculate part size
            long partSize = Constants.OPS5000_CNF_HIST_N * 16;

            // Caculate men size
            long menSize = partSize * Constants.OPS5000_TOTAL_PART_IN_MEN_HOUR;

            // Caculate start index
            long startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + Constants.OPS5000_HOUR_DATA_PART_START_INDEX;

            // Write data ti file
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Write))
            {
                foreach (KeyValuePair<string, string> item in historyMapping)
                {
                    int historyId9K = Int32.Parse(item.Key.ToString());
                    int historyId5K = Int32.Parse(item.Value.ToString());
                    OPS9000_T_HSTHB_DAT amount = partData[historyId9K - 1];

                    fs.Seek(startIndex + (historyId5K - 1) * 16, SeekOrigin.Begin);
                    byte[] b = ConvertToByteArray9K(amount);
                    fs.Write(b, 0, 16);
                }
            }
        }

        /// <summary>
        /// Write to HSTHB file from DSVS data
        /// </summary>
        /// <param name="menNo">Men No</param>
        /// <param name="partNo">Part No</param>
        /// <param name="historyId">History Id</param>
        /// <param name="inputFilename">HSTHB file path</param>
        /// <param name="amount">Data</param>
        public void WriteHourDataFromCSV(long menNo, long partNo, long historyId, string inputFilename, OPS5000_T_HSTHB_DAT amount)
        {
            // Caculate part size
            long partSize = Constants.OPS5000_CNF_HIST_N * 16;

            // Caculate part size
            long menSize = partSize * Constants.OPS5000_TOTAL_PART_IN_MEN_HOUR;

            // Caculate start index
            long startIndex = (menNo - 1) * menSize + (partNo - 1) * partSize + (historyId - 1) * 16 + Constants.OPS5000_HOUR_DATA_PART_START_INDEX;

            // Write data to file
            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Write))
            {
                fs.Seek(startIndex, SeekOrigin.Begin);
                byte[] b = ConvertToByteArray5K(amount);
                fs.Write(b, 0, 16);
            }
        }
        #endregion

        #region Private method
        /// <summary>
        /// Get infor data
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="startIdx"></param>
        /// <returns></returns>
        private short[] GetAddictionInfo(byte[] arr, int startIdx)
        {
            short[] ret = new short[2];

            ret[0] = BitConverter.ToInt16(arr, startIdx);
            ret[1] = BitConverter.ToInt16(arr, startIdx + 2);
            return ret;
        }

        /// <summary>
        /// Convert history unit data 6K to byte array
        /// </summary>
        /// <param name="amount">OPS6000_T_HSTHB_DAT</param>
        /// <returns>Byte array</returns>
        private byte[] ConvertToByteArray6K(OPS6000_T_HSTHB_DAT amount)
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
        /// Convert history unit data 9K to byte array
        /// </summary>
        /// <param name="amount">OPS9000_T_HSTHB_DAT</param>
        /// <returns>Byte array</returns>
        private byte[] ConvertToByteArray9K(OPS9000_T_HSTHB_DAT amount)
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
        /// Convert history unit data 5K to byte array
        /// </summary>
        /// <param name="amount">OPS5000_T_HSTHB_DAT</param>
        /// <returns>Byte array</returns>
        private byte[] ConvertToByteArray5K(OPS5000_T_HSTHB_DAT amount)
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
        #endregion
    }
}
