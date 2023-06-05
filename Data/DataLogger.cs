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

namespace Data
{
    internal class DataLogger:IDisposable
    {
        private ConcurrentQueue<JObject> _ballsConcurrentQueue;
        private JArray _logArray;
        private string _pathToFile;
        private Mutex _writeMutex = new Mutex();
        private Mutex _queueMutex = new Mutex();
        StreamWriter sw;

        internal DataLogger() 
        {
            string tempPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string loggersDirectory = Path.Combine(tempPath, "Loggers");
            _pathToFile = Path.Combine(loggersDirectory, "DataBallLog1.json");
            _ballsConcurrentQueue = new ConcurrentQueue<JObject>();
            sw = File.CreateText(_pathToFile);
            Task.Run(SaveToFile);
        }

        public void AddBall(IDataBall ball)
        {
            JObject log = JObject.FromObject(ball.Position);
            log["Time"] = DateTime.Now.ToString("HH:mm:ss");
            log.Add("Ball ID", ball.ID);
            if(_ballsConcurrentQueue.Count < 1000)
            {
                _ballsConcurrentQueue.Enqueue(log);
            }
            else
            {
                Console.WriteLine("XD");
            }
        }

        public void AddBoard(IDataBoard board)
        {
            JObject log = JObject.FromObject(board);
            string data = JsonConvert.SerializeObject(log) +",";
            sw.WriteLine(data);
        }

        private void SaveToFile()
        {
            bool appended = false;
            sw.Write("{");
            while (true)
            {
                String diagnosticData = "";
                while (_ballsConcurrentQueue.TryDequeue(out JObject ball))
                {
                    diagnosticData = JsonConvert.SerializeObject(ball);
                    try
                    {
                        sw.WriteLine(diagnosticData + ",");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            
        }

        public void Dispose()
        {
            bool saved = false;
            while (!saved)
            {
                try
                {
                    sw.WriteLine("}");
                    sw.Close();
                    saved = true;
                }
                catch(Exception ex)
                {

                }
            } 
        }
    }
}
