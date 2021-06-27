using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace FLog
{
    internal static class ExtensionMethods
    {
        public static void Log(this object obj, params object[] args)
        {
            FLog.Log(args);
        }

        public static void ColorLog(this object obj, LogColor color, params object[] args)
        {
            FLog.ColorLog(color, args);
        }

        public static void Trace(this object obj, params object[] args)
        {
            FLog.Trace(args);
        }

        public static void Warn(this object obj, params object[] args)
        {
            FLog.Warn(args);
        }

        public static void Error(this object obj, params object[] args)
        {
            FLog.Error(args);
        }
    }

    public static class FLog
    {
        class UnityLogger : ILogger
        {
            
            private MethodInfo logMethod;
            private MethodInfo warnMethod;
            private MethodInfo errorMethod;

            public UnityLogger()
            {
                var type = Type.GetType("UnityEngine.Debug, UnityEngine");
                if (type != null)
                {
                    logMethod = type.GetMethod("Log", new[] {typeof(object)});
                    warnMethod = type.GetMethod("LogWarning", new[] {typeof(object)});
                    errorMethod = type.GetMethod("LogError", new[] {typeof(object)});
                }
            }

            public void Log(string msg, LogColor color = LogColor.None)
            {
                if (color != LogColor.None)
                {
                    msg = ColorUnityLog(msg, color);
                }

                logMethod.Invoke(null, new object[] {msg});
            }

            public void Warn(string msg)
            {
                warnMethod.Invoke(null, new object[] {msg});
            }

            public void Error(string msg)
            {
                errorMethod.Invoke(null, new object[] {msg});
            }

            private string ColorUnityLog(string msg, LogColor color)
            {
                switch (color)
                {
                    case LogColor.Red:
                        msg = $"<color=#FF0000>{msg}</color>";
                        break;
                    case LogColor.Green:
                        msg = $"<color=#00FF00>{msg}</color>";
                        break;
                    case LogColor.Blue:
                        msg = $"<color=#0000FF>{msg}</color>";
                        break;
                    case LogColor.Cyan:
                        msg = $"<color=#00FFFF>{msg}</color>";
                        break;
                    case LogColor.Magenta:
                        msg = $"<color=#FF00FF>{msg}</color>";
                        break;
                    case LogColor.Yellow:
                        msg = $"<color=#FFFF00>{msg}</color>";
                        break;
                    case LogColor.None:
                    default:
                        break;
                }

                return msg;
            }
        }

        class ConsoleLogger : ILogger
        {
            public void Log(string msg, LogColor color = LogColor.None)
            {
                WriteConsoleLog(msg, color);
            }

            public void Warn(string msg)
            {
                WriteConsoleLog(msg, LogColor.Yellow);
            }

            public void Error(string msg)
            {
                WriteConsoleLog(msg, LogColor.Red);
            }

            private void WriteConsoleLog(string msg, LogColor color)
            {
                switch (color)
                {
                    case LogColor.Red:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColor.Green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColor.Blue:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColor.Cyan:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColor.Magenta:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColor.Yellow:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColor.None:
                    default:
                        Console.WriteLine(msg);
                        break;
                }
            }
        }

        public static ILogger Logger;
        public static LogConfig Config { get; private set; }
        private static StreamWriter logFileWriter = null;
        private static object logLock = new object();

        public static void InitSettings(LogConfig cfg = null)
        {
            if (cfg == null)
            {
                cfg = new LogConfig();
            }

            Config = cfg;

            if (cfg.loggerEnum == LoggerType.Console)
            {
                Logger = new ConsoleLogger();
            }
            else
            {
                Logger = new UnityLogger();
            }

            if (cfg.enableSave == false)
            {
                return;
            }

            if (cfg.enableCover)
            {
                string path = cfg.SavePath + cfg.saveName;
                try
                {
                    if (Directory.Exists(cfg.SavePath))
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(cfg.SavePath);
                    }

                    logFileWriter = File.AppendText(path);
                    logFileWriter.AutoFlush = true;
                }
                catch (Exception)
                {
                    logFileWriter = null;
                }
            }
            else
            {
                string prefix = DateTime.Now.ToString("yyyyMMdd@HH-mm-ss");
                string path = cfg.SavePath + prefix + cfg.saveName;
                try
                {
                    if (Directory.Exists(cfg.SavePath) == false)
                    {
                        Directory.CreateDirectory(cfg.SavePath);
                    }

                    logFileWriter = File.AppendText(path);
                    logFileWriter.AutoFlush = true;
                }
                catch (Exception)
                {
                    logFileWriter = null;
                }
            }
        }
        
        public static void Log(params object[] args)
        {
            if (Config.enableLog == false)
            {
                return;
            }

            string msg = DecorateLog(Config.enableTrace, args);
            Logger.Log(msg);
            if (Config.enableSave)
            {
                WriteToFile($"[L]{msg}");
            }
        }

        public static void ColorLog(LogColor color, params object[] args)
        {
            if (Config.enableLog == false)
            {
                return;
            }

            string msg = DecorateLog(Config.enableTrace, args);

            Logger.Log(msg, color);
            if (Config.enableSave)
            {
                WriteToFile($"[L]{msg}");
            }
        }
        
        public static void Trace(params object[] args)
        {
            if (Config.enableLog == false)
            {
                return;
            }

            string msg = DecorateLog(Config.enableTrace, args);
            
            Logger.Log(msg, LogColor.Magenta);
            if (Config.enableSave)
            {
                WriteToFile($"[T]{msg}");
            }
        }
        
        public static void Warn(params object[] args)
        {
            if (Config.enableLog == false)
            {
                return;
            }

            string msg = DecorateLog(Config.enableTrace, args);
            
            Logger.Warn(msg);
            if (Config.enableSave)
            {
                WriteToFile($"[W]{msg}");
            }
        }

        /// <summary>
        /// 错误日志（红色，输出堆栈）
        /// </summary>
        public static void Error(params object[] args)
        {
            if (Config.enableLog == false)
            {
                return;
            }

            string msg = DecorateLog(Config.enableTrace, args);
            
            Logger.Error(msg);
            if (Config.enableSave)
            {
                WriteToFile($"[E]{msg}");
            }
        }

        private static Stack<StringBuilder> stringBuilders = new Stack<StringBuilder>();

        //Tool
        private static string DecorateLog(bool isTrace, params object[] args)
        {
            StringBuilder stringBuilder;
            lock (logLock)
            {
                stringBuilder = stringBuilders.Count > 0 ? stringBuilders.Pop() : new StringBuilder();
            }

            stringBuilder.Append(Config.logPrefix);

            if (Config.enableTime)
            {
                stringBuilder.AppendFormat(" {0:hh:mm:ss-fff}", DateTime.Now);
            }

            if (Config.enableThreadID)
            {
                stringBuilder.AppendFormat(" {0}", GetThreadId());
            }

            stringBuilder.Append(Config.logSeparate);

            foreach (var obj in args)
            {
                stringBuilder.Append($"{obj}\t");
            }

            if (isTrace)
            {
                stringBuilder.AppendFormat("\nStackTrace:{0}", GetLogTrace());
            }

            var result = stringBuilder.ToString();
            stringBuilder.Clear();
            lock (logLock)
            {
                stringBuilders.Push(stringBuilder);
            }

            return result;
        }

        private static string GetThreadId()
        {
            return $" ThreadID:{Thread.CurrentThread.ManagedThreadId}";
        }

        private static string GetLogTrace()
        {
            StackTrace st = new StackTrace(3, true); //跳跃3帧
            string traceInfo = "";
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                if (sf != null)
                    traceInfo += $"\n    {sf.GetFileName()}::{sf.GetMethod()} Line:{sf.GetFileLineNumber()}";
            }

            return traceInfo;
        }

        private static void WriteToFile(string msg)
        {
            lock (logLock)
            {
                if (!Config.enableSave || logFileWriter == null) return;
                try
                {
                    logFileWriter.WriteLine(msg);
                }
                catch (Exception)
                {
                    logFileWriter = null;
                }
            }
        }

        //打印数组数据For Debug
        public static void PrintBytesArray(byte[] bytes, string prefix, Action<string> printer = null)
        {
            string str = prefix + "->\n";
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i % 10 == 0)
                {
                    str += bytes[i] + "\n";
                }

                str += bytes[i] + " ";
            }

            if (printer != null)
            {
                printer(str);
            }
            else
            {
                Log(str);
            }
        }
    }
}