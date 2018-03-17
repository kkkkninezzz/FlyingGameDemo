using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGF
{
    public static class DebuggerExtension
    {
        [System.Diagnostics.Conditional(ConfigConstants.ENABLE_LOG_CONDITION)]
        public static void Log(this object obj, string message)
        {
            if (!Debugger.EnableLog)
                return;

            Debugger.Log(GetLogTag(obj), message);
        }

        public static void Log(this object obj, string format, params object[] args)
        {
            if (!Debugger.EnableLog)
                return;

            Debugger.Log(GetLogTag(obj), format, args);
        }

        public static void LogError(this object obj, string message)
        {
            Debugger.LogError(GetLogTag(obj), message);
        }

        public static void LogError(this object obj, string format, params object[] args)
        {
            Debugger.LogError(GetLogTag(obj), format, args);
        }

        public static void LogWarning(this object obj, string message)
        {
            Debugger.LogWarning(GetLogTag(obj), message);
        }

        public static void LogWarning(this object obj, string format, params object[] args)
        {
            Debugger.LogWarning(GetLogTag(obj), format, args);
        }

        private static string GetLogTag(this object obj)
        {
            return obj.GetType().Name;
        }
    }
}

