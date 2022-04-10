using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace MarsRover
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                var serviceProvider = DI.Configure();

                var roverService = serviceProvider.GetService<IRover>();

                Console.WriteLine("Surface size :");

                var areaSize = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToList();

                List<Tuple<string[], string>> signal = new List<Tuple<string[], string>>();

                while (true)
                {

                    Console.WriteLine("Rover position :");
                    var startPositions = Console.ReadLine().Trim().Split(' ');
                    Console.WriteLine("Rover command :");
                    var moves = Console.ReadLine().ToUpper();
                    signal.Add(new(startPositions, moves));
                    Console.WriteLine("Do you want to add another rover ? (Y/N)");
                    var addRoverInput = Console.ReadLine();

                    if (!addRoverInput.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        break;
                    }
                }

                StartPositionRequest startPositionRequest = new();
                StartMovingRequest startMovingRequest = new();
                foreach (var i in signal)
                {
                    startPositionRequest.X = Convert.ToInt32(i.Item1[0]);
                    startPositionRequest.Y = Convert.ToInt32(i.Item1[1]);
                    startPositionRequest.Direction = (Direction)Enum.Parse(typeof(Direction), i.Item1[2]);
                    roverService.StartPosition(startPositionRequest);

                    startMovingRequest.AreaX = areaSize[0];
                    startMovingRequest.AreaY = areaSize[1];
                    startMovingRequest.Command = i.Item2;
                    roverService.StartMoving(startMovingRequest);

                    roverService.Output();
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

    }

}
