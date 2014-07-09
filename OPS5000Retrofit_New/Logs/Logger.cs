//////////////////////////////////////////////////////////////////////
// File Name     ：Logger.cs
// System Name   ：Solar Data Display System
// Description   ：Write log content to local file
// Creator       ：CongNC
// Create Date   ：2014/02/12
// Copyright (C) Meiden System Solution All Rights Reserved.
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using OPS5000Retrofit.Common;

namespace OPS5000Retrofit
{
    /// <summary>
    /// Log Writer Class
    /// </summary>
    public static class Logger
    {

        /// <summary>
        /// Write error log
        /// </summary>
        /// <param name="message">Message</param>
        public static void OutputErrorLog(string applicationPath, string message)
        {

            //Define a mmutex object
            Mutex mutex = new Mutex(false, @"Global\MyMutex");

            //Get the logfile name
            string fileName = applicationPath + "\\" + Constants.ERROR_FILE_NAME;

            // Check bytesize of message and substring if lagger 256 byte
            message = LimitByteLength(message);

            // Init current line variable
            int currentLine = 0;

            // Write log process
            try
            {

                // Blocks the current thread until the current WaitHandle receives a signal
                mutex.WaitOne();

                // Determines whether the log file exists
                if (File.Exists(fileName))
                {

                    // Read content file to arraylist
                    ArrayList fileContent = new ArrayList();
                    fileContent.AddRange(File.ReadAllLines(fileName));

                    // Read current line
                    currentLine = Int32.Parse(fileContent[0].ToString());

                    // if current line is max line limit
                    if (currentLine == Constants.S_ELOGREC_N)
                    {
                        // Set current line to 1
                        currentLine = 1;

                        // Overwrite log at 1st line
                        fileContent[currentLine] = DateTime.Now.ToString(Constants.YYYYMMDD_HHMMSS_SLASH) + " " + message;
                    }

                    // if current line and toltal line count are smaller max line limit
                    else if (currentLine < Constants.S_ELOGREC_N && fileContent.Count < Constants.S_ELOGREC_N + 1)
                    {
                        // Increase current line index
                        currentLine++;

                        // Create new log line
                        fileContent.Add(DateTime.Now.ToString(Constants.YYYYMMDD_HHMMSS_SLASH) + " " + message);
                    }
                    else if (currentLine < Constants.S_ELOGREC_N && fileContent.Count == Constants.S_ELOGREC_N + 1)
                    {
                        // Increase current line index
                        currentLine++;

                        // Overwrite log at next of line
                        fileContent[currentLine] = DateTime.Now.ToString(Constants.YYYYMMDD_HHMMSS_SLASH) + " " + message;
                    }

                    // Set new current line
                    fileContent[0] = currentLine.ToString();

                    //Write new log to file
                    using (StreamWriter writer = new StreamWriter(new FileStream(fileName, FileMode.Create)))
                    {
                        foreach (var s in fileContent)
                        {
                            writer.WriteLine(s.ToString());
                        }
                    }
                }
                // If file not exits create and write the first log
                else
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(fileName, FileMode.CreateNew)))
                    {
                        currentLine++;
                        writer.WriteLine(currentLine.ToString());
                        writer.WriteLine("{0} {1}", DateTime.Now.ToString(Constants.YYYYMMDD_HHMMSS_SLASH), message);
                    }
                }
            }
            catch (Exception)
            {
                //Not show error Exception
            }
            finally
            {
                // Release mutex object
                mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Check length of input string
        /// </summary>
        /// <param name="input">Input string</param>
        private static String LimitByteLength(String input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                //Remove new line charater
                input = input.Replace("\r", String.Empty);
                input = input.Replace("\n", String.Empty);

                // Define encoding for count byte of string
                Encoding shiftJIS = Encoding.GetEncoding(Constants.SHIFT_JIS_ENCODING);

                // Get bytesize of input string
                int byteSize = shiftJIS.GetByteCount(input);

                // Substring if lagger 256
                if (byteSize > Constants.S_ELOGSIZ_P)
                {
                    for (int i = input.Length - 1; i >= 0; i--)
                    {
                        if (Encoding.GetEncoding(Constants.SHIFT_JIS_ENCODING).GetByteCount(input.Substring(0, i + 1)) <= Constants.S_ELOGSIZ_P)
                        {
                            return input.Substring(0, i + 1);
                        }
                    }
                }
            }
            // No change if bytesize smaller 256
            return input;
        }

        public static void OutputErrorLogOpenFile(string applicationPath, Exception ex)
        {
            if (ex is IOException)
            {
                
                string messEx = ex.Message;
                string messLog = messEx.Substring(messEx.LastIndexOf("\\") + 1, messEx.LastIndexOf("'") - messEx.LastIndexOf("\\") - 1);
                OutputErrorLog(applicationPath, string.Format(Constants.FILE_OPEN_FAIL, messLog));
            }
            else
            {
                OutputErrorLog(applicationPath, ex.Message);
            }
        }
    }
}
