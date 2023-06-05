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
using System.Diagnostics;
using System.Numerics;

namespace Data
{
    internal class DataLogger:IDisposable
    {
        private ConcurrentQueue<JObject> _ballsConcurrentQueue;
        private JArray _logArray;
        private string _pathToFile;
        private string _pathToFile2;
        private Mutex _writeMutex = new Mutex();
        private Mutex _queueMutex = new Mutex();
        StreamWriter ballLogFile;
        StreamWriter boardLogFile;

        internal DataLogger() 
        {
            string tempPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string loggersDirectory = Path.Combine(tempPath, "Loggers");
            _pathToFile = Path.Combine(loggersDirectory, "DataBallLog.json");
            _pathToFile2 = Path.Combine(loggersDirectory, "BoardLog.json");
            _ballsConcurrentQueue = new ConcurrentQueue<JObject>();
            ballLogFile = File.CreateText(_pathToFile);
            boardLogFile = File.CreateText(_pathToFile2);
            Task.Run(SaveToFile);
        }

        public void AddBall(Vector2 position,int ID)
        {
            JObject log = JObject.FromObject(position);
            log["Time"] = DateTime.Now.ToString("HH:mm:ss");
            log.Add("Ball ID", ID);
            if(_ballsConcurrentQueue.Count < 1000)
            {
                _ballsConcurrentQueue.Enqueue(log);
            }
        }

        public void AddBoard(IDataBoard board)
        {
            JObject log = JObject.FromObject(board);
            string data = JsonConvert.SerializeObject(log);
            boardLogFile.Write(data);
            boardLogFile.Close();
        }

        private void SaveToFile()
        {
            bool appended = false;
            ballLogFile.Write("{");
            int i = 0;
            while (true)
            {
                String diagnosticData = "";
                if (_ballsConcurrentQueue.TryDequeue(out JObject ball))
                {
                    diagnosticData = "\"Log" + i + "\":" + JsonConvert.SerializeObject(ball);
                    ballLogFile.WriteLine(diagnosticData + ",");
                    i++;
                }
            }
            
        }

        public void Dispose()
        {
            bool saved = false;
            while (!saved)
            {
                ballLogFile.WriteLine("}");
                ballLogFile.Close();
                saved = true;
            } 
        }
    }
}
