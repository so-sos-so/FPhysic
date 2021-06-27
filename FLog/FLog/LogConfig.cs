using System;

namespace FLog
{
    public enum LoggerType
    {
        Unity,
        Console,
    }

    public enum LogColor
    {
        None,
        Red,
        Green,
        Blue,
        Cyan,
        Magenta,
        Yellow
    }

    public class LogConfig
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enableLog = true;

        /// <summary>
        /// 日志前缀
        /// </summary>
        public string logPrefix = "#";

        /// <summary>
        /// 时间标记
        /// </summary>
        public bool enableTime = true;

        /// <summary>
        /// 间隔符号
        /// </summary>
        public string logSeparate = ">>";

        /// <summary>
        /// 线程ID
        /// </summary>
        public bool enableThreadID = true;

        /// <summary>
        /// 堆栈信息
        /// </summary>
        public bool enableTrace = true;

        /// <summary>
        /// 文件保存
        /// </summary>
        public bool enableSave = true;

        /// <summary>
        /// 日志覆盖
        /// </summary>
        public bool enableCover = true;

        private string savePath;

        public string SavePath
        {
            get
            {
                if (savePath != null) return savePath;
                if (loggerEnum == LoggerType.Unity)
                {
                    Type type = Type.GetType("UnityEngine.Application, UnityEngine");
                    savePath = type?.GetProperty("persistentDataPath")?.GetValue(null) + "/PELog/";
                }
                else
                {
                    savePath = $"{AppDomain.CurrentDomain.BaseDirectory}Logs\\";
                }

                return savePath;
            }
            set => savePath = value;
        }

        /// <summary>
        /// 日志文件名称
        /// </summary>
        public string saveName = "ConsolePELog.txt";

        /// <summary>
        /// 日志输出器类型
        /// </summary>
        public LoggerType loggerEnum = LoggerType.Console;

        /// <summary>
        /// TODO
        /// </summary>
        public Action<int> GetFrameIndex;
    }

    public interface ILogger
    {
        void Log(string msg, LogColor logColor = LogColor.None);
        void Warn(string msg);
        void Error(string msg);
    }
}