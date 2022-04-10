using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRover.Test
{
    public class Tests
    {


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()  
        {
            /* INPUT 
               5 5
               1 2 N
               LMLMLMLMM
               3 3 E
               MMRMMRMRRM
            */
            List<string> test = new List<string>();
            test.Add("1 3 N");
            test.Add("5 1 E");

            List<string> result = new List<string>();

            List<Tuple<string[], string>> signal = new List<Tuple<string[], string>>();

            var serviceProvider = DI.Configure();
            var roverService = serviceProvider.GetService<IRover>();

            String S = "1 2 N";
            var startPositions = S.Trim().Split(' ');
            signal.Add(new(startPositions, "LMLMLMLMM"));

            S = "3 3 E";
            startPositions = S.Trim().Split(' ');

            signal.Add(new(startPositions, "MMRMMRMRRM"));

            StartPositionRequest startPositionRequest = new();
            StartMovingRequest startMovingRequest = new();
            foreach (var i in signal)
            {
                startPositionRequest.X = Convert.ToInt32(i.Item1[0]);
                startPositionRequest.Y = Convert.ToInt32(i.Item1[1]);
                startPositionRequest.Direction = (Direction)Enum.Parse(typeof(Direction), i.Item1[2]);
                roverService.StartPosition(startPositionRequest);

                startMovingRequest.AreaX = 5;
                startMovingRequest.AreaY = 5;
                startMovingRequest.Command = i.Item2;
                roverService.StartMoving(startMovingRequest);

                result.Add(roverService.CurrentRoverPositon());
            }

            Assert.AreEqual(result, test);

           
        }

     
        
        [Test]
        public void Test2()  // Out Of Size
        {

            try
            {
                var serviceProvider = DI.Configure();

                var roverService = serviceProvider.GetService<IRover>();

                StartPositionRequest startPositionRequest = new();
                startPositionRequest.X = 6;
                startPositionRequest.Y = 3;
                startPositionRequest.Direction = Direction.E;

                roverService.StartPosition(startPositionRequest);

                StartMovingRequest startMovingRequest = new();
                startMovingRequest.AreaX = 5;
                startMovingRequest.AreaY = 5;
                startMovingRequest.Command = "MMRMMRMRRM";
                roverService.StartMoving(startMovingRequest);
            }
           
            catch(ArgumentException ex)
            {
                Assert.True(true);
            }

}




        [Test]
        public void Test3()
        {
            var serviceProvider = DI.Configure();

            var roverService = serviceProvider.GetService<IRover>();

            StartPositionRequest startPositionRequest = new();
            startPositionRequest.X = 1;
            startPositionRequest.Y = 4;
            startPositionRequest.Direction = Direction.S;

            roverService.StartPosition(startPositionRequest);

            StartMovingRequest startMovingRequest = new();
            startMovingRequest.AreaX = 5;
            startMovingRequest.AreaY = 5;
            startMovingRequest.Command = "LMLM";
            roverService.StartMoving(startMovingRequest);


            Assert.AreEqual(roverService.CurrentRoverPositon(), "2 5 N");

        }


    }
}