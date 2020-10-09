using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanLibrary;

namespace FanOutPutRestService
{
    public static class Utility
    {
        public static Random random = new Random(DateTime.Now.Millisecond);

        public static List<string> FanNames = new List<string>()
        {
            "Ceiling1",
            "Ceiling2",
            "CeilingFloor",
            "CeilingFloorElectricBoogaloo",
            "Airway3",
            "DoorOffice",
            "Closet",
            "Closet3",
            "Lamp",
            "Table",
            "NightStand1",
            "NightStand2",
            "Tv1",
            "Tv2",
            "Tv3",
            "Tv4",
        };

        public static List<FanOutput> GenerateFanOutputs(int numberOfGens)
        {

            List<FanOutput> generatedFanOutputs = new List<FanOutput>();
            for (int i = 0; i < numberOfGens && i < FanNames.Count; i++)
            {
                generatedFanOutputs.Add(new FanOutput(i,FanNames[i],GenerateDouble(15,25),GenerateDouble(30,80)));
            }


            return generatedFanOutputs;
        }

        public static double GenerateDouble(double min, double max)
        {
            return (random.NextDouble() * (max - min)) + min;
        }
    }
}
