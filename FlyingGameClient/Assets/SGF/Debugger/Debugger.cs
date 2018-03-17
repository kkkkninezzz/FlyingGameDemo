using System;
using System.IO;

namespace UnityEngine
{
    public class Debugger
    {
        public static bool EnableLog;
        public static bool EnableTime = true;
        public static bool EnableSave = false;
        public static bool EnableStack = false;
        public static string LogFileDir = "/Log/DebuggerLog/";
        public static string LogFileName = "";
        public static string Prefix = "> ";
        public static StreamWriter LogFileWriter = null;

        public static void Log(object message)
        {
            if (!Debugger.EnableLog)
                return;

            string msg = GetLogTime() + message;

            Debug.Log(Prefix + msg, null);
            InfoLogToFile(msg);
        }

        public static void Log(object message, Object context)
        {
            if (!Debugger.EnableLog)
                return;

            string msg = GetLogTime() + message;

            Debug.Log(Prefix + msg, context);
            InfoLogToFile(msg);
        }

        public static void LogError(object message)
        {
            string msg = GetLogTime() + message;

            Debug.LogError(Prefix + msg, null);
            ErrorLogToFile(msg);
        }

        public static void LogError(object message, Object context)
        {
            string msg = GetLogTime() + message;

            Debug.LogError(Prefix + msg, context);
            ErrorLogToFile(msg);
        }

        public static void LogWarning(object message)
        {
            string msg = GetLogTime() + message;

            Debug.LogWarning(Prefix + msg, null);
            WarningLogToFile(msg);
        }

        public static void LogWarning(object message, Object context)
        {
            string msg = GetLogTime() + message;

            Debug.LogWarning(Prefix + msg, context);
            WarningLogToFile(msg);
        }


        public static void Log(string tag, string message)
        {
            if (!Debugger.EnableLog)
                return;

            message = GetLogText(tag, message);
            Debug.Log(Prefix + message);
            InfoLogToFile(message);
        }

        public static void Log(string tag, string format, params object[] args)
        {
            if (!Debugger.EnableLog)
                return;

            string message = GetLogText(tag, string.Format(format, args));
            Debug.Log(Prefix + message);
            InfoLogToFile(message);
        }

        public static void LogError(string tag, string message)
        {
            message = GetLogText(tag, message);
            Debug.LogError(Prefix + message);
            ErrorLogToFile(message, true);
        }

        public static void LogError(string tag, string format, params object[] args)
        {
            string message = GetLogText(tag, string.Format(format, args));
            Debug.LogError(Prefix + message);
            ErrorLogToFile(message, true);
        }


        private static string GetLogText(string tag, string message)
        {
            string str = GetLogTime();

            str = str + tag + " :: " + message;
            return str;
        }

        private static string GetLogTime()
        {
            if (Debugger.EnableTime)
            {
                DateTime now = DateTime.Now;
                return now.ToString("HH:mm:ss.fff") + " ";
             }

            return string.Empty;
        }

        private static void InfoLogToFile(string msg, bool EnableStack = false)
        {
            LogToFile("[I]" + msg, EnableStack);
        }

        private static void ErrorLogToFile(string msg, bool EnableStack = false)
        {
            LogToFile("[E]" + msg, EnableStack);
        }
        

        private static void WarningLogToFile(string msg, bool EnableStack = false)
        {
            LogToFile("[W]" + msg, EnableStack);
        }

        private static void LogToFile(string msg, bool EnableStack = false)
        {
            if (!EnableSave)
                return;

            if (LogFileWriter == null)
            {
                LogFileName = createLogFileName();

                string fullPath = LogFileDir + LogFileName;

                try
                {
                    if (!Directory.Exists(LogFileDir))
                        Directory.CreateDirectory(LogFileDir);

                    LogFileWriter = File.AppendText(fullPath);
                    LogFileWriter.AutoFlush = true;
                } catch (Exception e)
                {
                    LogFileWriter = null;
                    Debug.LogError("LogToFile() " + e.Message + e.StackTrace);
                    return;
                }
            }

            if (LogFileWriter != null)
            {
                try
                {
                    LogFileWriter.WriteLine(msg);
                    if (EnableStack || Debugger.EnableStack)
                        LogFileWriter.WriteLine(StackTraceUtility.ExtractStackTrace());
                } catch (Exception e)
                {
                    return;
                }
            }
        }

        private static string createLogFileName()
        {
            DateTime now = DateTime.Now;
            string FileName = now.GetDateTimeFormats('s')[0].ToString();
            FileName = FileName.Replace("-", "_");
            FileName = FileName.Replace(":", "_");
            FileName = FileName.Replace(" ", "");
            FileName += ".log";

            return FileName;
        }
    }

}

