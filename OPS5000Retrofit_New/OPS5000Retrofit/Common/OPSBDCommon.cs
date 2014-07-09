//////////////////////////////////////////////////////////////////////
// File Name     ：OPSBDCommon.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPSBDCommon file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using OPS5000Retrofit.Common;
using System.IO;
using System.Windows.Forms;

namespace OPS5000Retrofit
{
    /// <summary>
    /// OPSBD Common Class
    /// </summary>
    public static class OPSBDCommon
    {
        /// <summary>
        /// Create History Tag No Mapping for OPS9K
        /// </summary>
        /// <param name="opsbdKesoFilePath">opsbdKeso File Path</param>
        /// <param name="opsDDFilePath"></param>
        /// <returns>Mapping table</returns>
        public static Dictionary<string, string> CreateHistoryId_TagNoMappingTable9K(string opsbdKesoFilePath, string opsDDFilePath)
        {
            Dictionary<string, string> OPS9kMapping = new Dictionary<string, string>();

            List<OPS9000_T_BD_KESO_KEIKI> OPSBDkesoData = new List<OPS9000_T_BD_KESO_KEIKI>();

            byte[] OPSBDkesoByteArr = File.ReadAllBytes(opsbdKesoFilePath);

            int kekiCount = OPSBDkesoByteArr.Length / 108;

            for (int i = 0; i < kekiCount; i++)
            {
                byte[] keikiByteArr = new byte[108];

                for (int j = 0; j < 108; j++)
                {
                    keikiByteArr[j] = OPSBDkesoByteArr[i * 108 + j];
                }

                OPS9000_T_BD_KESO_KEIKI temp = new OPS9000_T_BD_KESO_KEIKI();
                temp.tgno_c = System.Text.Encoding.ASCII.GetString(keikiByteArr, 0, 8);
                temp.tgnm_c = System.Text.Encoding.GetEncoding(932).GetString(keikiByteArr, 14, 34);
                OPSBDkesoData.Add(temp);
            }

            List<OPS9000_T_STATION> stationList = new List<OPS9000_T_STATION>();
            for (int p = 0; p < 36; p++)
            {
                OPS9000_T_STATION station = new OPS9000_T_STATION();
                station.station_no = p;
                for (int q = 0; q < 513; q++)
                {
                    OPSBDkesoData[p * 513 + q].keiki_no = q;
                    station.keiki_lst.Add(OPSBDkesoData[p * 513 + q]);
                }
                stationList.Add(station);
            }

            List<OPS9000_T_DD_TPT> T_DD_tptData = new List<OPS9000_T_DD_TPT>();


            byte[] OPSBDtptByteArr = File.ReadAllBytes(opsDDFilePath);

            int historyIDCount = OPSBDtptByteArr.Length / 4;

            for (int i = 0; i < historyIDCount; i++)
            {
                byte[] historyIdByteArr = new byte[4];

                for (int j = 0; j < 4; j++)
                {
                    historyIdByteArr[j] = OPSBDtptByteArr[i * 4 + j];
                }

                OPS9000_T_DD_TPT temp = new OPS9000_T_DD_TPT();
                temp.history_id = i + 1;
                temp.stno_s = BitConverter.ToInt16(historyIdByteArr, 0);
                temp.tpt_s = BitConverter.ToInt16(historyIdByteArr, 2);
                T_DD_tptData.Add(temp);
            }

            foreach (OPS9000_T_DD_TPT item in T_DD_tptData)
            {
                if (item.stno_s != -1)
                {
                    OPS9kMapping.Add(item.history_id.ToString(), stationList[item.stno_s].keiki_lst[item.tpt_s].tgno_c);
                }
            }
            return OPS9kMapping;
        }

        /// <summary>
        /// Create History Tag No Mapping for OPS6K
        /// </summary>
        /// <param name="path">OPSBD File Path</param>
        /// <returns>Mapping table</returns>
        public static Dictionary<string, string> CreateHistoryId_TagNoMappingTable6K(string path)
        {
            Dictionary<string, string> OPS6kMapping = new Dictionary<string, string>();

            List<OPS6000_T_BD_KESO_KEIKI> OPSBDkesoData = new List<OPS6000_T_BD_KESO_KEIKI>();

            byte[] OPSBDkesoByteArr = File.ReadAllBytes(path);

            int kekiCount = OPSBDkesoByteArr.Length / 108;

            for (int i = 0; i < kekiCount; i++)
            {
                byte[] keikiByteArr = new byte[108];

                for (int j = 0; j < 108; j++)
                {
                    keikiByteArr[j] = OPSBDkesoByteArr[i * 108 + j];
                }

                OPS6000_T_BD_KESO_KEIKI temp = new OPS6000_T_BD_KESO_KEIKI();
                temp.tgno_c = System.Text.Encoding.ASCII.GetString(keikiByteArr, 0, 8);
                temp.tgnm_c = System.Text.Encoding.GetEncoding(932).GetString(keikiByteArr, 14, 26);
                OPSBDkesoData.Add(temp);
            }

            List<OPS6000_T_BD_KESO_KEIKI> OPSBDkesoDataTrue = new List<OPS6000_T_BD_KESO_KEIKI>();
            for (int i = 0; i < OPSBDkesoData.Count; i++)
            {
                if (i % 513 != 0)
                {
                    OPSBDkesoDataTrue.Add(OPSBDkesoData[i]);
                }
            }

            List<OPS6000_T_STATION> stationList = new List<OPS6000_T_STATION>();
            for (int p = 0; p <= 8; p++)
            {
                OPS6000_T_STATION station = new OPS6000_T_STATION();
                for (int q = 0; q < 512; q++)
                {
                    station.keiki_lst.Add(OPSBDkesoDataTrue[p * 512 + q]);
                }
                stationList.Add(station);
            }

            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    OPS6kMapping.Add((i * 512 + j + 1).ToString(), stationList[i].keiki_lst[j].tgno_c);
                }
            }
            return OPS6kMapping;
        }

        /// <summary>
        /// Create history id mapping table on OPS6K and OPS5K
        /// </summary>
        /// <param name="OPS6kMapping">HistoryId TagNo Mapping on 6K</param>
        /// <param name="OPS5kMapping">HistoryId TagNo Mapping on 5K</param>
        /// <returns>Mapping table</returns>
        public static Dictionary<String, String> CreateHistoryMappingTable6K_5K(Dictionary<string, string> OPS6kMapping, Dictionary<string, string> OPS5kMapping)
        {
            Dictionary<String, String> historyMapping = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> entry in OPS6kMapping)
            {
                if (OPS5kMapping.ContainsValue(entry.Value))
                {
                    historyMapping.Add(entry.Key, OPS5kMapping.FirstOrDefault(x => x.Value == entry.Value).Key);
                }
                else
                {
                    // Write log
                    Logger.OutputErrorLog(Application.StartupPath, string.Format(Constants.TAGNO_NOT_EXISTS, entry.Value));
                }
            }

            return historyMapping;
        }

        /// <summary>
        /// Create history mapping table on OPS9K and OPS5K
        /// </summary>
        /// <param name="OPS9kMapping"></param>
        /// <param name="OPS5kMapping"></param>
        /// <returns></returns>
        public static Dictionary<String, String> CreateHistoryMappingTable9K_5K(Dictionary<string, string> OPS9kMapping, Dictionary<string, string> OPS5kMapping)
        {
            Dictionary<String, String> historyMapping = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> entry in OPS9kMapping)
            {
                if (OPS5kMapping.ContainsValue(entry.Value))
                {
                    historyMapping.Add(entry.Key, OPS5kMapping.FirstOrDefault(x => x.Value == entry.Value).Key);
                }
                else
                {
                    // Write log
                    Logger.OutputErrorLog(Application.StartupPath, string.Format(Constants.TAGNO_NOT_EXISTS, entry.Value));
                }
            }
            return historyMapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OPS9kMapping"></param>
        /// <param name="OPS5kMapping"></param>
        /// <returns></returns>
        public static int GetHistoryIdByTagNo(string tagNo, Dictionary<string, string> tagNoMapping)
        {
            int historyId = -1;
            if (tagNoMapping.ContainsValue(tagNo))
            {
                historyId = Int32.Parse(tagNoMapping.FirstOrDefault(x => x.Value == tagNo).Key);
            }

            return historyId;
        }

        /// <summary>
        /// Create historyid tag no mapping table for 5K
        /// </summary>
        /// <param name="opsbdKesoFilePath"></param>
        /// <param name="opsDDFilePath"></param>
        /// <returns></returns>
        public static Dictionary<string, string> CreateHistoryId_TagNoMappingTable5K(string opsbdKesoFilePath, string opsDDFilePath)
        {
            Dictionary<string, string> OPS5kMapping = new Dictionary<string, string>();

            List<OPS5000_T_BD_KESO_KEIKI> OPSBDkesoData = new List<OPS5000_T_BD_KESO_KEIKI>();

            byte[] OPSBDkesoByteArr = File.ReadAllBytes(opsbdKesoFilePath);

            int kekiCount = OPSBDkesoByteArr.Length / 116;

            for (int i = 0; i < kekiCount; i++)
            {
                byte[] keikiByteArr = new byte[116];

                for (int j = 0; j < 116; j++)
                {
                    keikiByteArr[j] = OPSBDkesoByteArr[i * 116 + j];
                }

                OPS5000_T_BD_KESO_KEIKI temp = new OPS5000_T_BD_KESO_KEIKI();
                temp.tgno_c = System.Text.Encoding.ASCII.GetString(keikiByteArr, 0, 8);
                temp.tgnm_c = System.Text.Encoding.GetEncoding(932).GetString(keikiByteArr, 14, 34);
                OPSBDkesoData.Add(temp);
            }

            List<OPS5000_T_STATION> stationList = new List<OPS5000_T_STATION>();
            for (int p = 0; p < 51; p++)
            {
                OPS5000_T_STATION station = new OPS5000_T_STATION();
                station.station_no = p;
                for (int q = 0; q < 1025; q++)
                {
                    OPSBDkesoData[p * 1025 + q].keiki_no = q;
                    station.keiki_lst.Add(OPSBDkesoData[p * 1025 + q]);
                }
                stationList.Add(station);
            }

            List<OPS5000_T_DD_TPT> T_DD_tptData = new List<OPS5000_T_DD_TPT>();

            byte[] OPSBDtptByteArr = File.ReadAllBytes(opsDDFilePath);

            int historyIDCount = OPSBDtptByteArr.Length / 4;

            for (int i = 0; i < historyIDCount; i++)
            {
                byte[] historyIdByteArr = new byte[4];

                for (int j = 0; j < 4; j++)
                {
                    historyIdByteArr[j] = OPSBDtptByteArr[i * 4 + j];
                }

                OPS5000_T_DD_TPT temp = new OPS5000_T_DD_TPT();
                temp.history_id = i + 1;
                temp.stno_s = BitConverter.ToInt16(historyIdByteArr, 0);
                temp.tpt_s = BitConverter.ToInt16(historyIdByteArr, 2);
                T_DD_tptData.Add(temp);
            }

            foreach (OPS5000_T_DD_TPT item in T_DD_tptData)
            {
                if (item.stno_s != -1)
                {
                    OPS5kMapping.Add(item.history_id.ToString(), stationList[item.stno_s].keiki_lst[item.tpt_s].tgno_c);
                }
            }
            return OPS5kMapping;
        }

        /// <summary>
        /// Create men mapping table
        /// </summary>
        /// <param name="mappingFrom"></param>
        /// <param name="mappingTo"></param>
        /// <returns></returns>
        public static Dictionary<String, String> CreateMenMappingTable(Dictionary<string, string> mappingFrom, Dictionary<string, string> mappingTo)
        {
            Dictionary<String, String> menMapping = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> entry in mappingFrom)
            {
                if (mappingTo.ContainsKey(entry.Key))
                {
                    menMapping.Add(entry.Value, mappingTo[entry.Key]);
                }
            }
            return menMapping;
        }
    }
}
