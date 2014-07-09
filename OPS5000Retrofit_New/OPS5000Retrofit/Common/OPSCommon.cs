//////////////////////////////////////////////////////////////////////
// File Name     ：OPSCommon.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：OPSCommon file
// Creator       ：CongNC
// Create Date   ：2014/06/20
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OPS5000Retrofit.Common;

namespace OPS5000Retrofit
{
    /// <summary>
    /// OPS Common Structure
    /// </summary>
    public static class OPSCommon
    {
        public static OPS6000_T_HSTDB_HED GetDayBulkIndex6K(string inputFilename)
        {
            List<OPS6000_T_HSTDB_IDX> indexDataList = new List<OPS6000_T_HSTDB_IDX>();
            short[] yobi = new short[47];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(512, SeekOrigin.Begin);

                byte[] b = new byte[512];
                fs.Read(b, 0, 512);

                OPS6000_T_HSTDB_HED indexPart = new OPS6000_T_HSTDB_HED();
                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);

                for (int i = 1; i <= Constants.OPS6000_S_HSTDB_MAXMEN_P; i++)
                {
                    OPS6000_T_HSTDB_IDX idx = new OPS6000_T_HSTDB_IDX();
                    idx.yea_s = BitConverter.ToInt16(b, i * 16);
                    idx.mon_s = BitConverter.ToInt16(b, i * 16 + 2);
                    idx.lst_s = BitConverter.ToInt16(b, i * 16 + 4);
                    idx.yobi01_s = BitConverter.ToInt16(b, i * 16 + 6);
                    idx.men_s = BitConverter.ToInt16(b, i * 16 + 8);
                    idx.prt_s = BitConverter.ToInt16(b, i * 16 + 10);
                    idx.yobi02_s = Utility.GetShortArray4(b, i * 16 + 12);

                    indexDataList.Add(idx);
                }

                byte[] dailyByte = new byte[2];
                fs.Seek(928, SeekOrigin.Begin);
                fs.Read(dailyByte, 0, 2);

                // Read yobi
                byte[] yobiByte = new byte[94];
                fs.Seek(930, SeekOrigin.Begin);
                fs.Read(yobiByte, 0, 94);

                for (int i = 0; i < 47; i++)
                {
                    yobi[i] = BitConverter.ToInt16(yobiByte, i * 2);
                }

                indexPart.indx = indexDataList;
                indexPart.chgday_s = BitConverter.ToInt16(dailyByte, 0); ;
                indexPart.yobi02_s = yobi;

                return indexPart;
            }
        }
        public static OPS6000_T_HSTHB_HED GetHourBulkIndex6K(string inputFilename)
        {
            List<OPS6000_T_HSTHB_IDX> indexDataList = new List<OPS6000_T_HSTHB_IDX>();
            short dailyTime;
            short[] yobi = new short[255];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(512, SeekOrigin.Begin);

                byte[] b = new byte[1024];
                fs.Read(b, 0, 1024);

                for (int i = 1; i <= 63; i++)
                {
                    OPS6000_T_HSTHB_IDX idx = new OPS6000_T_HSTHB_IDX();
                    idx.yea_s = BitConverter.ToInt16(b, i * 16);
                    idx.mon_s = BitConverter.ToInt16(b, i * 16 + 2);
                    idx.day_s = BitConverter.ToInt16(b, i * 16 + 4);
                    idx.cal_s = BitConverter.ToInt16(b, i * 16 + 6);
                    idx.men_s = BitConverter.ToInt16(b, i * 16 + 8);
                    idx.prt_s = BitConverter.ToInt16(b, i * 16 + 10);
                    idx.yobi01_s = Utility.GetShortArray4(b, i * 16 + 12);

                    indexDataList.Add(idx);
                }

                byte[] dailyByte = new byte[2];
                fs.Seek(1536, SeekOrigin.Begin);
                fs.Read(dailyByte, 0, 2);
                // Read daily time
                dailyTime = BitConverter.ToInt16(dailyByte, 0);

                // Read yobi
                byte[] yobiByte = new byte[510];
                fs.Seek(1538, SeekOrigin.Begin);
                fs.Read(yobiByte, 0, 510);

                for (int i = 0; i < 255; i++)
                {
                    yobi[i] = BitConverter.ToInt16(yobiByte, i * 2);
                }

                OPS6000_T_HSTHB_HED indexPart = new OPS6000_T_HSTHB_HED();

                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);

                indexPart.indx = indexDataList;
                indexPart.chghor_s = dailyTime;
                indexPart.yobi02_s = yobi;

                return indexPart;
            }
        }

        public static OPS5000_T_HSTHB_HED GetHourBulkIndex5K(string inputFilename)
        {
            List<OPS5000_T_HSTHB_IDX> indexDataList = new List<OPS5000_T_HSTHB_IDX>();
            short[] yobi = new short[142];

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(256, SeekOrigin.Begin);

                byte[] b = new byte[58480];
                fs.Read(b, 0, 58480);

                OPS5000_T_HSTHB_HED indexPart = new OPS5000_T_HSTHB_HED();
                indexPart.yea_s = BitConverter.ToInt16(b, 0);
                indexPart.mon_s = BitConverter.ToInt16(b, 2);
                indexPart.day_s = BitConverter.ToInt16(b, 4);
                indexPart.cal_s = BitConverter.ToInt16(b, 6);
                indexPart.hor_s = BitConverter.ToInt16(b, 8);
                indexPart.yobi01_s = Utility.GetShortArray6(b, 10);

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

                byte[] dailyByte = new byte[2];
                fs.Seek(58736, SeekOrigin.Begin);
                fs.Read(dailyByte, 0, 2);
                indexPart.chghor_s = BitConverter.ToInt16(dailyByte, 0); ;

                // Read yobi
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

        public static short GetLastPartNoInHourData(short menNo, OPS6000_T_HSTHB_HED indexData)
        {
            short partNo = -1;
            foreach (OPS6000_T_HSTHB_IDX index in indexData.indx)
            {
                if (menNo == index.men_s)
                {
                    partNo = index.prt_s;
                }
            }
            return partNo;
        }
        public static short GetLastPartNoInDayData(short menNo, OPS6000_T_HSTDB_HED indexData)
        {
            short partNo = -1;
            foreach (OPS6000_T_HSTDB_IDX index in indexData.indx)
            {
                if (menNo == index.men_s)
                {
                    partNo = index.prt_s;
                }
            }
            return partNo;
        }

        public static void WriteIndexPart5KDay(string inputFile, OPS5000_T_HSTDB_HED indexpart5k)
        {
            using (StreamWriter file = new StreamWriter(@"E:\5KIndexDayData.txt", false))
            {
                file.WriteLine("最終更新時刻");
                file.Write("年:{0}|", indexpart5k.yea_s);
                file.Write("月:{0}|", indexpart5k.mon_s);
                file.Write("日:{0}|", indexpart5k.day_s);
                file.Write("曜日:{0}|", indexpart5k.cal_s);
                file.Write("時:{0}|", indexpart5k.hor_s);
                file.WriteLine("予備:{0}", ArrayToString(indexpart5k.yobi01_s));
                file.WriteLine();
                file.WriteLine("索引");
                file.WriteLine("No\t|年\t|月\t|当月最終日\t|予備\t|面No.\t|処理中Part No.|予備\t|");
                int idx = 1;
                foreach (OPS5000_T_HSTDB_IDX i in indexpart5k.indx)
                {
                    file.WriteLine("{0}\t|{1}\t|{2}\t|{3}\t\t|{4}\t\t|{5}\t\t |{6}\t|{7}\t|", idx, i.yea_s, i.mon_s, i.lst_s, i.yobi01_s, i.men_s, i.prt_s, ArrayToString(i.yobi02_s));
                    idx++;
                }
                file.WriteLine();
                file.WriteLine("月替わり時刻");
                file.WriteLine(indexpart5k.chgday_s);
                file.WriteLine();
                file.WriteLine("予備");
                file.WriteLine(ArrayToString(indexpart5k.yobi02_s));
            }
        }

        public static void WriteIndexPart6KDay(string inputFile, OPS6000_T_HSTDB_HED indexpart6k)
        {
            using (StreamWriter file = new StreamWriter(@"E:\6KIndexDayData.txt", false))
            {
                file.WriteLine("最終更新時刻");
                file.Write("年:{0}|", indexpart6k.yea_s);
                file.Write("月:{0}|", indexpart6k.mon_s);
                file.Write("日:{0}|", indexpart6k.day_s);
                file.Write("曜日:{0}|", indexpart6k.cal_s);
                file.Write("時:{0}|", indexpart6k.hor_s);
                file.WriteLine("予備:{0}", ArrayToString(indexpart6k.yobi01_s));
                file.WriteLine();
                file.WriteLine("索引");
                file.WriteLine("No\t|年\t|月\t|当月最終日\t|予備\t|面No.\t|処理中Part No.|予備\t|");
                int idx = 1;
                foreach (OPS6000_T_HSTDB_IDX i in indexpart6k.indx)
                {
                    file.WriteLine("{0}\t|{1}\t|{2}\t|{3}\t|{4}\t\t|{5}\t\t |{6}\t|{7}\t|", idx, i.yea_s, i.mon_s, i.lst_s, i.yobi01_s, i.men_s, i.prt_s, ArrayToString(i.yobi02_s));
                    idx++;
                }
                file.WriteLine();
                file.WriteLine("月替わり時刻");
                file.WriteLine(indexpart6k.chgday_s);
                file.WriteLine();
                file.WriteLine("予備");
                file.WriteLine(ArrayToString(indexpart6k.yobi02_s));
            }
        }
        private static string ArrayToString(short[] arr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(arr[i].ToString());
                if (i < arr.Length - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
    }
}
