using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataBall:IDataBall,IDisposable
    {

        public override event EventHandler<DataEventArgs>? ChangedPosition;

        private Vector2 _position;
        public override Vector2 Position
        {
            get => _position;
        }

        public override Vector2 Speed { get; set; }

        private bool ContinueMoving;
        private DataLoggerAPI _logger;
        public override int ID { get; }

        public DataBall(int posX, int posY, int radius, int weight, int xSpeed, int ySpeed, DataLoggerAPI logger, int id)
        {
            ID = id;
            _position = new Vector2(posX, posY);
            Speed = new Vector2(xSpeed, ySpeed);
            ContinueMoving = true;
            this._logger = logger;
            Task.Run(StartMovement);
        }

        public async void StartMovement()
        {
            Stopwatch stopWatch = new Stopwatch();
            int baseMovementTime = 10; // in milliseconds
            while (ContinueMoving)
            {
                stopWatch.Start();
                Move();
                _logger.AddBall(this);
                stopWatch.Stop();
                if (baseMovementTime > (int)stopWatch.ElapsedMilliseconds)
                {
                    await Task.Delay(baseMovementTime - (int)stopWatch.ElapsedMilliseconds);
                }
                stopWatch.Reset();
            }
        }

        private void Move()
        {
            Vector2 tempPos = _position;
            Vector2 tempSpeed = Speed;
            tempPos = new Vector2(tempPos.X + tempSpeed.X, tempPos.Y + tempSpeed.Y);
            _position = tempPos;
            DataEventArgs args = new DataEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }

        public override void Dispose()
        {
            ContinueMoving = false;
        }
    }
}
