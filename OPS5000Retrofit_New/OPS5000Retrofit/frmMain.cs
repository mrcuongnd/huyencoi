//////////////////////////////////////////////////////////////////////
// File Name     ：frmMain.cs
// System Name   ：OPS5000 Retrofit System
// Description   ：Convert data from OPS6000 and OPS9000 to OPS5000
// Creator       ：CuongND
// Create Date   ：2014/06/03
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using OPS5000Retrofit.Common;

namespace OPS5000Retrofit
{
    /// <summary>
    /// frmMain Class
    /// </summary>
    public partial class frmMain : Form
    {
        #region Decalare variable
        public enum SystemType
        {
            OPS6000 = 0,
            DSVS6000 = 1,
            OPS9000 = 2,
            DSVS9000 = 3
        }

        public enum DataType
        {
            HourData = 0,
            DayData = 1,
            MonthData = 2,
            YearData = 3,
            CommentData = 4
        }

        public enum ConvertType
        {
            All = 0,
            Time = 1
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        #region Button Click

        /// <summary>
        /// Click button Browser for event get folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Click button for start convert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            SystemType systemType = (SystemType)cboxSystemType.SelectedIndex;
            DataType dataType = (DataType)cboxDataType.SelectedIndex;
            ConvertType convertType = ConvertType.All;
            if (rbtnMoveAll.Checked)
            {
                convertType = ConvertType.All;
            }
            else
            {
                convertType = ConvertType.Time;
            }

            string inputDataFolder = txtPath.Text;

            string outputFolder = Environment.GetEnvironmentVariable("PATH_FILE", EnvironmentVariableTarget.User);

            if (!Directory.Exists(outputFolder))
            {
                MessageBox.Show("出力するフォルダは存在しません。");
                return;
            }

            if (!Directory.Exists(inputDataFolder))
            {
                MessageBox.Show("入力したフォルダは存在しません。");
                return;
            }
            switch (systemType)
            {
                case SystemType.OPS6000:
                    switch (dataType)
                    {
                        case DataType.HourData:
                            OPS6KHourConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.DayData:
                            OPS6KDayConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.MonthData:
                            OPS6KMonthConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.YearData:
                            OPS6KYearConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.CommentData:
                            OPS6KCommentConvert(convertType, inputDataFolder, outputFolder);
                            break;
                    }
                    break;
                case SystemType.DSVS6000:
                    switch (dataType)
                    {
                        case DataType.HourData:
                            DSVS6KHourConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.DayData:
                            DSVS6KDayConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.MonthData:
                            DSVS6KMonthConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.YearData:
                            DSVS6KYearConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.CommentData:
                            DSVS6KCommentConvert(convertType, inputDataFolder, outputFolder);
                            break;
                    }
                    break;
                case SystemType.OPS9000:
                    switch (dataType)
                    {
                        case DataType.HourData:
                            OPS9KHourConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.DayData:
                            OPS9KDayConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.MonthData:
                            OPS9KMonthConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.YearData:
                            OPS9KYearConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.CommentData:
                            OPS9KCommentConvert(convertType, inputDataFolder, outputFolder);
                            break;
                    }
                    break;
                case SystemType.DSVS9000:
                    switch (dataType)
                    {
                        case DataType.HourData:
                            DSVS6KHourConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.DayData:
                            DSVS6KDayConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.MonthData:
                            DSVS6KMonthConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.YearData:
                            DSVS6KYearConvert(convertType, inputDataFolder, outputFolder);
                            break;
                        case DataType.CommentData:
                            DSVS6KCommentConvert(convertType, inputDataFolder, outputFolder);
                            break;
                    }
                    break;
            }

        }

        /// <summary>
        /// Convert hour data from OPS60000 to OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS6KHourConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            try
            {
                // Flag check file exists
                bool isExistsFile = true;

                // Move all data or by time
                bool moveAll;
                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }

                // Init object convert data
                OPS6000Hour ops6k = new OPS6000Hour();

                // Init part
                string hourDataFile6K = inputFolderPath + @"\HSTHB";
                string hourDataFile5K = ouputFolder + @"\number\HSTHB";
                string ops6KBDKesoFilePath = inputFolderPath + @"\OPSBDkeso";
                string ops5KBDKesoFilePath = ouputFolder + @"\load\OPSBDkeso";
                string ops5KDDtptFilePath = ouputFolder + @"\load\OPSDDtpt";


                if (isExistsFile)
                {
                    // Convert data
                    ops6k.ConvertToOPS5K(moveAll,
                                         Int32.Parse(numStartYear.Value.ToString()),
                                         Int32.Parse(numStartMonth.Value.ToString()),
                                         Int32.Parse(numStartDay.Value.ToString()),
                                         Int32.Parse(numStartHour.Value.ToString()),
                                         Int32.Parse(numEndYear.Value.ToString()),
                                         Int32.Parse(numEndMonth.Value.ToString()),
                                         Int32.Parse(numEndDay.Value.ToString()),
                                         Int32.Parse(numEndHour.Value.ToString()),
                                         ops6KBDKesoFilePath,
                                         ops5KBDKesoFilePath,
                                         ops5KDDtptFilePath,
                                         hourDataFile5K,
                                         hourDataFile6K);
                }

            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }
        }

        /// <summary>
        /// Convert day data from OPS6000 to OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS6KDayConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            try
            {
                // Move all data or by time
                bool moveAll;
                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }

                // Convert data
                OPS6000Day convert = new OPS6000Day();
                convert.ConvertToOPS5K(moveAll,
                                    (short)numStartYear.Value,
                                    (short)numStartMonth.Value,
                                    (short)numStartDay.Value,
                                    (short)numEndYear.Value,
                                    (short)numEndMonth.Value,
                                    (short)numEndDay.Value,
                                    inputFolderPath,
                                    ouputFolder);
            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }
        }

        /// <summary>
        /// Convert data month bullk OPS6000 -> OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS6KMonthConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            try
            {
                // Flag check file exists
                bool isExistsFile = true;

                // Move all data or by time
                bool moveAll;
                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }

                // Convert data
                if (isExistsFile)
                {
                    OPS6000_Month convertMonth = new OPS6000_Month();
                    convertMonth.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numStartMonth.Value,
                                        (int)numEndYear.Value,
                                        (int)numEndMonth.Value,
                                        inputFolderPath + @"\OPSBDkeso",
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        ouputFolder + @"\number\HSTMB",
                                        inputFolderPath + @"\HSTMB");
                }
            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }
        }

        /// <summary>
        /// Convert data year bullk OPS6000 -> OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS6KYearConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            try
            {
                // Flag check file exists
                bool isExistsFile = true;

                //Move all or by time
                bool moveAll;

                // Convert data
                if (isExistsFile)
                {
                    if (convertType == ConvertType.All)
                    {
                        moveAll = true;
                    }
                    else
                    {
                        moveAll = false;
                    }

                    OPS6000_Year convertYear = new OPS6000_Year();
                    convertYear.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numEndYear.Value,
                                        inputFolderPath + @"\OPSBDkeso",
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        ouputFolder + @"\number\HSTYB",
                                        inputFolderPath + @"\HSTYB");
                }
            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }
        }

        /// <summary>
        /// Convert data comment OPS6000 -> OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS6KCommentConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {

            // Flag check file exists
            bool isExistsFile = true;

            // Move all data or by time
            bool moveAll;

            // Convert data
            if (isExistsFile)
            {
                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }

                try
                {
                    // Convert data comment day
                    OPS6000_DayComment convertCommentDay = new OPS6000_DayComment();
                    convertCommentDay.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numStartMonth.Value,
                                        (int)numStartDay.Value,
                                        (int)numEndYear.Value,
                                        (int)numEndMonth.Value,
                                        (int)numEndDay.Value,
                                        inputFolderPath + @"\OPSBDkeso",
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        ouputFolder + @"\number\CHODB",
                                        inputFolderPath + @"\CHODB",
                                        inputFolderPath + @"\HSTDB",
                                        ouputFolder + @"\number\HSTDB");
                }
                catch (Exception ex)
                {
                    Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
                }

                try
                {
                    // Convert data comment month
                    OPS6000_MonthComment convertCommentMonth = new OPS6000_MonthComment();
                    convertCommentMonth.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numStartMonth.Value,
                                        (int)numEndYear.Value,
                                        (int)numEndMonth.Value,
                                        inputFolderPath + @"\OPSBDkeso",
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        ouputFolder + @"\number\CHOMB",
                                        inputFolderPath + @"\CHOMB",
                                        inputFolderPath + @"\HSTMB",
                                        ouputFolder + @"\number\HSTMB");
                }
                catch (Exception ex)
                {
                    Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
                }

                try
                {
                    // Convert data comment year
                    OPS6000_YearComment convertCommentYear = new OPS6000_YearComment();
                    convertCommentYear.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numEndYear.Value,
                                        inputFolderPath + @"\OPSBDkeso",
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        ouputFolder + @"\number\CHOYB",
                                        inputFolderPath + @"\CHOYB",
                                        inputFolderPath + @"\HSTYB",
                                        ouputFolder + @"\number\HSTYB");
                }
                catch (Exception ex)
                {
                    Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
                }
            }

        }

        /// <summary>
        /// Convert data hour bullk CSV OPS6000 -> OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void DSVS6KHourConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            try
            {
                bool moveAll;
                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }

                HourDataBulkCsv dsvs6K = new HourDataBulkCsv();
                dsvs6K.ConvertToOPS5k(moveAll, (short)numStartYear.Value,
                                    (short)numStartMonth.Value,
                                    (short)numStartDay.Value,
                                    (short)numStartHour.Value,
                                    (short)numEndYear.Value,
                                    (short)numEndMonth.Value,
                                    (short)numEndDay.Value,
                                    (short)numEndHour.Value,
                                    inputFolderPath,
                                    ouputFolder);
            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }

        }

        /// <summary>
        /// Convert data day bullk CSV OPS6000 -> OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void DSVS6KDayConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            try
            {
                bool moveAll;
                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }

                DayDataBulkCsv dsvs6K = new DayDataBulkCsv();
                dsvs6K.ConvertToOPS5k(moveAll, (short)numStartYear.Value,
                                    (short)numStartMonth.Value,
                                    (short)numStartDay.Value,
                                    (short)numEndYear.Value,
                                    (short)numEndMonth.Value,
                                    (short)numEndDay.Value,
                                    inputFolderPath,
                                    ouputFolder);
            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }
        }

        /// <summary>
        /// Convert data month bullk CSV OPS6000 -> OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void DSVS6KMonthConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            try
            {

                // Move all data or by time
                bool moveAll;

                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }

                // Convert data
                MonthDataBulkCsv convert = new MonthDataBulkCsv();
                convert.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numStartMonth.Value,
                                        (int)numEndYear.Value,
                                        (int)numEndMonth.Value,
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        inputFolderPath,
                                        ouputFolder + @"\number\HSTMB");

            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }
        }

        /// <summary>
        /// Convert data year bullk CSV OPS6000 -> OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void DSVS6KYearConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            try
            {
                // Move all data or by time
                bool moveAll;

                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }

                // Convert data
                YearDataBulkCsv convert = new YearDataBulkCsv();
                convert.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numEndYear.Value,
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        inputFolderPath,
                                        ouputFolder + @"\number\HSTYB");
            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }
        }

        /// <summary>
        /// Convert data month comment CSV OPS6000 -> OPS5000
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void DSVS6KCommentConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {

            // Move all data or by time
            bool moveAll;



            if (convertType == ConvertType.All)
            {
                moveAll = true;
            }
            else
            {
                moveAll = false;
            }

            // Convert data comment day
            try
            {
                DayDataCommentCsv dayConvert = new DayDataCommentCsv();
                dayConvert.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numStartMonth.Value,
                                        (int)numStartDay.Value,
                                        (int)numEndYear.Value,
                                        (int)numEndMonth.Value,
                                        (int)numEndDay.Value,
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        inputFolderPath,
                                        ouputFolder + @"\number\HSTDB",
                                        ouputFolder + @"\number\CHODB");
            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }

            // Convert data comment month
            try
            {
                MonthDataCommentCsv monthConvert = new MonthDataCommentCsv();
                monthConvert.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numStartMonth.Value,
                                        (int)numEndYear.Value,
                                        (int)numEndMonth.Value,
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        inputFolderPath,
                                        ouputFolder + @"\number\HSTMB",
                                        ouputFolder + @"\number\CHOMB");
            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }

            // Convert data comment year
            try
            {
                YearDataCommentCsv yearConvert = new YearDataCommentCsv();
                yearConvert.ConvertToOPS5k(moveAll,
                                        (int)numStartYear.Value,
                                        (int)numEndYear.Value,
                                        ouputFolder + @"\load\OPSBDkeso",
                                        ouputFolder + @"\load\OPSDDtpt",
                                        inputFolderPath,
                                        ouputFolder + @"\number\HSTYB",
                                        ouputFolder + @"\number\CHOYB");

            }
            catch (Exception ex)
            {
                Logger.OutputErrorLogOpenFile(Application.StartupPath, ex);
            }
        }

        /// <summary>
        /// Convert 9K Hour Data
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS9KHourConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {

            try
            {
                bool moveAll;
                if (convertType == ConvertType.All)
                {
                    moveAll = true;
                }
                else
                {
                    moveAll = false;
                }
                OPS9000Hour ops9k = new OPS9000Hour();

                //Create Path
                string hourDataFile9K = inputFolderPath + @"\HSTHB";
                string hourDataFile5K = ouputFolder + @"\number\HSTHB";

                string ops9KBDKesoFilePath = inputFolderPath + @"\OPSBDkeso";
                string ops9KBDtptFilePath = inputFolderPath + @"\OPSDDtpt";
                string ops5KBDKesoFilePath = ouputFolder + @"\load\OPSBDkeso";
                string ops5KDDtptFilePath = ouputFolder + @"\load\OPSDDtpt";
                ops9k.ConvertToOPS5K(moveAll,
                             Int32.Parse(numStartYear.Value.ToString()),
                             Int32.Parse(numStartMonth.Value.ToString()),
                             Int32.Parse(numStartDay.Value.ToString()),
                             Int32.Parse(numStartHour.Value.ToString()),
                             Int32.Parse(numEndYear.Value.ToString()),
                             Int32.Parse(numEndMonth.Value.ToString()),
                             Int32.Parse(numEndDay.Value.ToString()),
                             Int32.Parse(numEndHour.Value.ToString()),
                             ops9KBDKesoFilePath,
                             ops9KBDtptFilePath,
                             ops5KBDKesoFilePath,
                             ops5KDDtptFilePath,
                             hourDataFile9K,
                             hourDataFile5K);

            }
            catch (Exception ex)
            {
                if (ex is IOException)
                    MessageBox.Show("IO exception");
            }

        }

        /// <summary>
        /// Convert 9k Day Data
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS9KDayConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {


            bool moveAll;
            if (convertType == ConvertType.All)
            {
                moveAll = true;
            }
            else
            {
                moveAll = false;
            }
            OPS9000Day ops9k = new OPS9000Day();

            //Create Path
            string dayDataFile9K = inputFolderPath + @"\HSTDB";
            string dayDataFile5K = ouputFolder + @"\number\HSTDB";

            string ops9KBDKesoFilePath = inputFolderPath + @"\OPSBDkeso";
            string ops9KBDtptFilePath = inputFolderPath + @"\OPSDDtpt";
            string ops5KBDKesoFilePath = ouputFolder + @"\load\OPSBDkeso";
            string ops5KDDtptFilePath = ouputFolder + @"\load\OPSDDtpt";
            ops9k.ConvertToOPS5K(moveAll,
                         Int16.Parse(numStartYear.Value.ToString()),
                         Int16.Parse(numStartMonth.Value.ToString()),
                         Int16.Parse(numStartDay.Value.ToString()),
                         Int16.Parse(numEndYear.Value.ToString()),
                         Int16.Parse(numEndMonth.Value.ToString()),
                         Int16.Parse(numEndDay.Value.ToString()),
                         ops9KBDKesoFilePath,
                         ops9KBDtptFilePath,
                         ops5KBDKesoFilePath,
                         ops5KDDtptFilePath,
                         dayDataFile9K,
                         dayDataFile5K);




        }

        /// <summary>
        /// Convert 9k Month Data
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS9KMonthConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            bool moveAll;
            if (convertType == ConvertType.All)
            {
                moveAll = true;
            }
            else
            {
                moveAll = false;
            }
            OPS9000_Month convertMonth = new OPS9000_Month();
            convertMonth.ConvertToOPS5k(moveAll,
                                (int)numStartYear.Value,
                                (int)numStartMonth.Value,
                                (int)numEndYear.Value,
                                (int)numEndMonth.Value,
                                inputFolderPath + @"\OPSBDkeso",
                                inputFolderPath + @"\OPSDDtpt",
                                ouputFolder + @"\load\OPSBDkeso",
                                ouputFolder + @"\load\OPSDDtpt",
                                ouputFolder + @"\number\HSTMB",
                                inputFolderPath + @"\HSTMB");
        }

        /// <summary>
        /// Convert 9k Year Data
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS9KYearConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            bool moveAll;
            if (convertType == ConvertType.All)
            {
                moveAll = true;
            }
            else
            {
                moveAll = false;
            }
            OPS9000_Year convertYear = new OPS9000_Year();
            convertYear.ConvertToOPS5k(moveAll,
                                (int)numStartYear.Value,
                                (int)numEndYear.Value,
                                inputFolderPath + @"\OPSBDkeso",
                                inputFolderPath + @"\OPSDDtpt",
                                ouputFolder + @"\load\OPSBDkeso",
                                ouputFolder + @"\load\OPSDDtpt",
                                ouputFolder + @"\number\HSTYB",
                                inputFolderPath + @"\Hstyb");
        }

        /// <summary>
        /// convert 9k Comment Data
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="ouputFolder"></param>
        private void OPS9KCommentConvert(ConvertType convertType, string inputFolderPath, string ouputFolder)
        {
            bool moveAll;
            if (convertType == ConvertType.All)
            {
                moveAll = true;
            }
            else
            {
                moveAll = false;
            }

            // Convert data comment day
            OPS9000_DayComment convertCommentDay = new OPS9000_DayComment();
            convertCommentDay.ConvertToOPS5k(moveAll,
                                (int)numStartYear.Value,
                                (int)numStartMonth.Value,
                                (int)numEndYear.Value,
                                (int)numEndMonth.Value,
                                inputFolderPath + @"\OPSBDkeso",
                                inputFolderPath + @"\OPSDDtpt",
                                ouputFolder + @"\load\OPSBDkeso",
                                ouputFolder + @"\load\OPSDDtpt",
                                ouputFolder + @"\number\CHODB",
                                inputFolderPath + @"\CHODB",
                                inputFolderPath + @"\HSTDB",
                                ouputFolder + @"\number\HSTDB");

            // Convert data comment month
            OPS9000_MonthComment convertCommentMonth = new OPS9000_MonthComment();
            convertCommentMonth.ConvertToOPS5k(moveAll,
                                (int)numStartYear.Value,
                                (int)numStartMonth.Value,
                                (int)numEndYear.Value,
                                (int)numEndMonth.Value,
                                inputFolderPath + @"\OPSBDkeso",
                                inputFolderPath + @"\OPSDDtpt",
                                ouputFolder + @"\load\OPSBDkeso",
                                ouputFolder + @"\load\OPSDDtpt",
                                ouputFolder + @"\number\CHOMB",
                                inputFolderPath + @"\CHOMB",
                                inputFolderPath + @"\HSTMB",
                                ouputFolder + @"\number\HSTMB");

            // Convert data comment year
            OPS9000_YearComment convertCommentYear = new OPS9000_YearComment();
            convertCommentYear.ConvertToOPS5k(moveAll,
                                (int)numStartYear.Value,
                                (int)numStartMonth.Value,
                                (int)numEndYear.Value,
                                (int)numEndMonth.Value,
                                inputFolderPath + @"\OPSBDkeso",
                                inputFolderPath + @"\OPSDDtpt",
                                ouputFolder + @"\load\OPSBDkeso",
                                ouputFolder + @"\load\OPSDDtpt",
                                ouputFolder + @"\number\CHOYB",
                                inputFolderPath + @"\CHOYB",
                                inputFolderPath + @"\HSTYB",
                                ouputFolder + @"\number\HSTYB");
        }

        /// <summary>
        /// Test Show Data 9k
        /// </summary>
        private void Convert9Kto5KHourShowData()
        {
            OPS5000Hour ops5k = new OPS5000Hour();
            OPS9000Hour ops9k = new OPS9000Hour();
            string hourDataFile9K = @"D:\Working\dotnet project\ConvertData\Dataconvert\OPS9000\HSTHB";
            string hourDataFile5K = @"D:\Working\dotnet project\ConvertData\Dataconvert\OPS5000\number\HSTHB";
            //string ops9KesoDataFile = @"D:\Working\dotnet project\ConvertData\Dataconvert\OPS9000\OPSBDkeso";
            //string ops5KesoDataFile = @"D:\Working\dotnet project\ConvertData\Dataconvert\OPS5000\load\";
            int startYear = Int32.Parse(numStartYear.Value.ToString());
            int startMonth = Int32.Parse(numStartMonth.Value.ToString());
            int startDay = Int32.Parse(numStartDay.Value.ToString());

            int endYear = Int32.Parse(numEndYear.Value.ToString());
            int endMonth = Int32.Parse(numEndMonth.Value.ToString());
            int endDay = Int32.Parse(numEndDay.Value.ToString());

            DateTime startDateTime = new DateTime(startYear, startMonth, startDay, 0, 0, 0);
            DateTime endDateTime = new DateTime(endYear, endMonth, endDay, 0, 0, 0);
            OPS5000_T_HSTHB_HED indexPart5k = ops5k.GetIndexPart(hourDataFile5K);
            OPS9000_T_HSTHB_HED indexPart9k = ops9k.GetIndexPart(hourDataFile9K);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\5KIndexData.txt", false))
            {
                file.WriteLine("最終更新時刻");
                file.Write("年:{0}|", indexPart5k.yea_s);
                file.Write("月:{0}|", indexPart5k.mon_s);
                file.Write("日:{0}|", indexPart5k.day_s);
                file.Write("曜日:{0}|", indexPart5k.cal_s);
                file.Write("時:{0}|", indexPart5k.hor_s);
                file.WriteLine("予備:{0}", Utility.ArrayToString(indexPart5k.yobi01_s));
                file.WriteLine();
                file.WriteLine("索引");
                file.WriteLine("年\t|月\t|日\t|曜日\t|時データ面 No.\t|最終処理Part No.|予備\t|No\t|");
                int idx = 1;
                foreach (OPS5000_T_HSTHB_IDX i in indexPart5k.indx)
                {

                    file.WriteLine("{0}\t|{1}\t|{2}\t|{3}\t|{4}\t\t|{5}\t\t |{6}\t|{7}\t|", i.yea_s, i.mon_s, i.day_s, i.cal_s, i.men_s, i.prt_s, Utility.ArrayToString(i.yobi01_s), idx);
                    idx++;
                }
                file.WriteLine();
                file.WriteLine("日替わり時刻");
                file.WriteLine(indexPart5k.chghor_s);
                file.WriteLine();
                file.WriteLine("予備");
                file.WriteLine(Utility.ArrayToString(indexPart5k.yobi02_s));
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\9KIndexData.txt", false))
            {
                file.WriteLine("最終更新時刻");
                file.Write("年:{0}|", indexPart9k.yea_s);
                file.Write("月:{0}|", indexPart9k.mon_s);
                file.Write("日:{0}|", indexPart9k.day_s);
                file.Write("曜日:{0}|", indexPart9k.cal_s);
                file.Write("時:{0}|", indexPart9k.hor_s);
                file.WriteLine("予備:{0}", Utility.ArrayToString(indexPart9k.yobi01_s));
                file.WriteLine();
                file.WriteLine("索引");
                file.WriteLine("年\t|月\t|日\t|曜日\t|時データ面 No.\t|最終処理Part No.|予備\t|");
                foreach (OPS9000_T_HSTHB_IDX i in indexPart9k.index_lst)
                {
                    file.WriteLine("{0}\t|{1}\t|{2}\t|{3}\t|{4}\t\t|{5}\t\t |{6}\t|", i.yea_s, i.mon_s, i.day_s, i.cal_s, i.men_s, i.prt_s, Utility.ArrayToString(i.yobi01_s));
                }
                file.WriteLine();
                file.WriteLine("日替わり時刻");
                file.WriteLine(indexPart9k.chghor_s);
                file.WriteLine();
                file.WriteLine("予備");
                file.WriteLine(Utility.ArrayToString(indexPart9k.yobi02_s));
            }
        }

        /// <summary>
        /// Click button for cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region Event selected change for all control

        /// <summary>
        /// Event: Disable for select Start and End Date when click to rbtnMoveAll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnMoveAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMoveAll.Checked == true)
            {
                //Disable for select Start and End Date when click to rbtnMoveAll
                numStartYear.Enabled = false;
                numStartMonth.Enabled = false;
                numStartDay.Enabled = false;
                numStartHour.Enabled = false;
                numEndYear.Enabled = false;
                numEndMonth.Enabled = false;
                numEndDay.Enabled = false;
                numEndHour.Enabled = false;
            }

        }

        /// <summary>
        /// Event: Enable for select Start and End Date when click to rbtnMoveAll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnMoveBySelect_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMoveByTime.Checked == true)
            {
                //Enable for select Start and End Date when click to rbtnMoveAll
                numStartYear.Enabled = true;
                numStartMonth.Enabled = true;
                numStartDay.Enabled = true;
                numStartHour.Enabled = true;
                numEndYear.Enabled = true;
                numEndMonth.Enabled = true;
                numEndDay.Enabled = true;
                numEndHour.Enabled = true;
            }
        }
        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (rbtnMoveAll.Checked == true)
            {
                //Disable for select Start and End Date when click to rbtnMoveAll
                numStartYear.Enabled = false;
                numStartMonth.Enabled = false;
                numStartDay.Enabled = false;
                numStartHour.Enabled = false;
                numEndYear.Enabled = false;
                numEndMonth.Enabled = false;
                numEndDay.Enabled = false;
                numEndHour.Enabled = false;
            }
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            numStartYear.Value = Convert.ToInt32(dt.Year);
            numStartMonth.Value = Convert.ToInt32(dt.Month);
            numStartDay.Value = Convert.ToInt32(dt.Day);
            numStartHour.Value = Convert.ToInt32(dt.Hour);
            numEndYear.Value = Convert.ToInt32(dt.Year);
            numEndMonth.Value = Convert.ToInt32(dt.Month);
            numEndDay.Value = Convert.ToInt32(dt.Day);
            numEndHour.Value = Convert.ToInt32(dt.Hour);
        }
        #endregion
    }
}
