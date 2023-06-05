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
        private Task _logerTask;

        internal DataLogger() 
        {
            string tempPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string loggersDirectory = Path.Combine(tempPath, "Loggers");
            _pathToFile = Path.Combine(loggersDirectory, "DataBallLog1.json");
            _ballsConcurrentQueue = new ConcurrentQueue<JObject>();
            Task.Run(SaveToFile);
            if (File.Exists(_pathToFile))
            {
                try
                {
                    string input = File.ReadAllText(_pathToFile);
                    _logArray = JArray.Parse(input);
                }
                catch(Exception ex)
                {
                    _logArray = new JArray();
                }
                
            }
            else
            {
                _logArray = new JArray();
                FileStream file = File.Create(_pathToFile);
                file.Dispose();
                file.Close();

            }
        }

        public void AddBall(IDataBall ball)
        {
            _queueMutex.WaitOne();
            try
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
            finally
            {
                _queueMutex.ReleaseMutex();
            }
        }

        public void AddBoard(IDataBoard board)
        {
            JObject log = JObject.FromObject(board);
            _ballsConcurrentQueue.Enqueue(log);
            _logArray.Add(log);
        }

        private void SaveDataToLog()
        {

            _writeMutex.WaitOne();
            String diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);
            while (_ballsConcurrentQueue.TryDequeue(out JObject ball)) 
            {
                diagnosticData = JsonConvert.SerializeObject(ball);
            }
            try
            {
                File.AppendAllText(_pathToFile, diagnosticData + "AA" + "\n");
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
                File.WriteAllText(_pathToFile, string.Empty);
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }
        private void SaveToFile()
        {
            bool appended = false;
            while(!appended) 
            {
                try
                {
                    ClearLogFile();
                    File.AppendAllText(_pathToFile, "{");
                    appended = true;
                }
                catch (Exception ex)
                {
                    //nothing
                }
                
            }
            
            while (true)
            {
                String diagnosticData = "";
                while (_ballsConcurrentQueue.TryDequeue(out JObject ball))
                {
                    diagnosticData = JsonConvert.SerializeObject(ball);
                    try
                    {
                        File.AppendAllText(_pathToFile, diagnosticData + ",\n");
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
                    File.AppendAllText(_pathToFile, "}");
                    saved = true;
                }
                catch(Exception ex)
                {

                }
            } 
        }
    }
}
