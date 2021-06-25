using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hapcan
{
    //declare a delegate type for the event
    public delegate void LoggerEvent();

    public static class Logger
    {
        //EVENTS
        public static event LoggerEvent LogSaved;      //log saved event

        //FIELDS
        private static string _logFilePath = Application.ProductName + ".log"; //default log file path "appname.log"
        private static StringBuilder _logText = new StringBuilder();
        private static int _logTextSize = 1000;
        private static string _logTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        private static ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        private static System.Timers.Timer _logTimer;
        private static int _logTimerInterval = 1;
        


        //PROPERTIES
        /// <summary>
        /// Logging file path. Must include file extension.
        /// </summary>
        public static string LogFilePath { get { return _logFilePath; } set { _logFilePath = value; } }
        /// <summary>
        /// Last 1000 lines of logs since app run.
        /// </summary>
        public static StringBuilder LogText { get { return _logText; } }
        /// <summary>
        /// Logger time format. Default is 'yyyy-MM-dd HH:mm:ss.fff'
        /// </summary>
        public static string LogTimeFormat { get { return _logTimeFormat; } set { _logTimeFormat = value; } }
        /// <summary>
        /// Logger saving interval in seconds. Default is 2s
        /// </summary>
        public static int LogSavingInterval { get { return _logTimerInterval; } set { _logTimerInterval = value; } }


        //METHODS
        /// <summary>
        /// Puts new log in format "DateTime [level] loging text".
        /// </summary>
        /// <param name="lev">Logging level</param>
        /// <param name="log">Logging text</param>
        public static void Log(string lev, string log)
        {
            //queue log to update
            string text = System.DateTime.Now.ToString(_logTimeFormat) + " [" + lev + "] " + log;
            _queue.Enqueue(text);
            SetSavingTimer();
        }
        //prepare timer for saving
        private static void SetSavingTimer()
        {
            if (_logTimer == null)
            {
                _logTimer = new System.Timers.Timer();
                _logTimer.Interval = _logTimerInterval * 1000;
                _logTimer.Elapsed += SavingTimerTick;
                _logTimer.AutoReset = false;
                _logTimer.Enabled = true;
            }
            else if (_logTimer.Enabled == false)
                _logTimer.Enabled = true;
        }
        //save logs
        private static void SavingTimerTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            while (_queue.TryDequeue(out var text))
            {
                UpdateLogText(text);
                UpdateLogFile(text);
            }
            //raise event
            if (LogSaved != null)       //event handler exists?               
                LogSaved();             //raise event
        }
        //update logs in LogText property
        private static void UpdateLogText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                //add new line
                _logText.AppendLine(text);
                //remove old line
                if (_logText.ToString().Split('\n').Length > _logTextSize)
                {
                    _logText.Remove(0, _logText.ToString().Split('\n').FirstOrDefault().Length);
                }
            }
        }
        //update logs in log file
        private static void UpdateLogFile(string text)
        {
            try
            {
                //make sure directory exists
                if (!Directory.Exists(Path.GetDirectoryName(_logFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath));

                //check file
                if (File.Exists(_logFilePath))
                {
                    //enable writing to file
                    File.SetAttributes(_logFilePath, FileAttributes.Normal);
                    //make a copy if file is bigger than 1MB
                    if (new FileInfo(_logFilePath).Length > 1048576)
                    {
                        if (File.Exists(_logFilePath + "2"))
                            File.SetAttributes(_logFilePath + "2", FileAttributes.Normal);
                        File.Copy(_logFilePath, _logFilePath + "2", true);
                        File.Delete(_logFilePath);
                    }
                }

                //save log file
                using (var writer = new System.IO.StreamWriter(_logFilePath, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine(text);
                }

                //make read-only file
                File.SetAttributes(_logFilePath, FileAttributes.ReadOnly);

            }
            catch (Exception ex)
            {
                UpdateLogText(System.DateTime.Now.ToString(_logTimeFormat) + " [Log error] " + ex);
            }
        }
    }
}
