using System;
using System.Collections.Generic;

namespace MarsRover
{
   
    public class Rover : IRover
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Direction DirectionFace { get; set; }

        public Rover()
        { 
        }


        #region private metods
        private void SpinLeft()
        {
            switch (this.DirectionFace)
            {
                case Direction.N:
                    this.DirectionFace = Direction.W;
                    break;
                case Direction.S:
                    this.DirectionFace = Direction.E;
                    break;
                case Direction.E:
                    this.DirectionFace = Direction.N;
                    break;
                case Direction.W:
                    this.DirectionFace = Direction.S;
                    break;
                default:
                    break;
            }
        }

        private void SpinRight()
        {
            switch (this.DirectionFace)
            {
                case Direction.N:
                    this.DirectionFace = Direction.E;
                    break;
                case Direction.S:
                    this.DirectionFace = Direction.W;
                    break;
                case Direction.E:
                    this.DirectionFace = Direction.S;
                    break;
                case Direction.W:
                    this.DirectionFace = Direction.N;
                    break;
                default:
                    break;
            }
        }

        private void StepForwad()
        {
            switch (this.DirectionFace)
            {
                case Direction.N:
                    this.YPosition += 1;
                    break;
                case Direction.S:
                    this.YPosition -= 1;
                    break;
                case Direction.E:
                    this.XPosition += 1;
                    break;
                case Direction.W:
                    this.XPosition -= 1;
                    break;
                default:
                    break;
            }
        }

        private void CheckAreaSize(int areaX,int areaY)
        {
            if (this.XPosition > areaX)
                throw new ArgumentOutOfRangeException();

            if(this.YPosition > areaY)
                throw new ArgumentOutOfRangeException();
        }
        #endregion
        #region publice metod
        public void StartMoving(StartMovingRequest startMovingRequest)
        {
            foreach (var c in startMovingRequest.Command)
            {
                switch (c)
                {
                    case CommandMessage.Left:
                        this.SpinLeft();
                        break;
                    case CommandMessage.Rigth:
                        this.SpinRight();
                        break;
                    case CommandMessage.Forwad:
                        this.StepForwad();
                        break;
                   
                    default:
                        throw new ArgumentException();
                      
                }

                CheckAreaSize(startMovingRequest.AreaX, startMovingRequest.AreaY);
            }
        }

        public void StartPosition(StartPositionRequest startPositionRequest)
        {
            this.XPosition = startPositionRequest.X;
            this.YPosition = startPositionRequest.Y;
            this.DirectionFace = startPositionRequest.Direction;
        }

        public void Output()
        {
            Console.WriteLine($"{XPosition} {YPosition} {DirectionFace.ToString()}");
        }

        public string CurrentRoverPositon()
        {
            return XPosition.ToString() + " " + YPosition.ToString() + " " + DirectionFace.ToString();
        }
        #endregion
    }



}
