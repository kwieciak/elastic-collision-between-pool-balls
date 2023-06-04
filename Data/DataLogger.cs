using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.IO;
using System.Data;
using System.Threading.Tasks;

namespace Data
{
    internal class DataLogger : DataLoggerAPI
    {
        private ConcurrentQueue<JObject> _ballsConcurrentQueue;
        private JArray _logArray;
        private string _pathToFile;
        private Mutex _writeMutex = new Mutex();
        private Mutex _queueMutex = new Mutex();
        private Task _logerTask;

        internal DataLogger() 
        {
            string tempPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string loggersDirectory = Path.Combine(tempPath, "Loggers");
            _pathToFile = Path.Combine(loggersDirectory, "DataBallLog.json");
            _ballsConcurrentQueue = new ConcurrentQueue<JObject>();

            if (File.Exists(_pathToFile))
            {
                try
                {
                    string input = File.ReadAllText(_pathToFile);
                    _logArray = JArray.Parse(input);
                }
                catch(JsonReaderException ex)
                {
                    _logArray = new JArray();
                }
                
            }
            else
            {
                _logArray = new JArray();
                FileStream file = File.Create(_pathToFile);
                file.Close();
            }
        }

        public override void AddBall(IDataBall ball)
        {
            _queueMutex.WaitOne();
            try
            {
                JObject log = JObject.FromObject(ball.Position);
                log["Time"] = DateTime.Now.ToString("HH:mm:ss");
                log.Add("Ball ID", ball.ID);

                _ballsConcurrentQueue.Enqueue(log);
                if (_logerTask == null || _logerTask.IsCompleted)
                {
                    _logerTask = Task.Run(SaveDataToLog);
                }
            }
            finally
            {
                _queueMutex.ReleaseMutex();
            }
        }

        public override void AddBoard(IDataBoard board)
        {
            ClearLogFile();
            JObject log = JObject.FromObject(board);
            _logArray.Add(log);
            string diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);

            _writeMutex.WaitOne();
            try
            {
                File.WriteAllText(_pathToFile, diagnosticData);
            }
            finally
            { 
                _writeMutex.ReleaseMutex(); 
            } 
        }

        private void SaveDataToLog()
        {
            _writeMutex.WaitOne();
            while (_ballsConcurrentQueue.TryDequeue(out JObject ball)) 
            {
                _logArray.Add(ball);
            }
            String diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);
            try
            {
                File.WriteAllText(_pathToFile, diagnosticData);
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }

        
        private void ClearLogFile()
        {
            _writeMutex.WaitOne();
            try
            {
                _logArray.Clear();
                File.WriteAllText(_pathToFile, string.Empty);
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }
    }
}
