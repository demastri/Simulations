using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSim
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputStrings = new List<string>();
            if (args.Count() < 1)
            {
                inputStrings.Add("../../inputs/input 1.txt");
                inputStrings.Add("../../inputs/input 2.txt");
                inputStrings.Add("../../inputs/input 3.txt");
                inputStrings.Add("../../inputs/input 2-1.txt");
                inputStrings.Add("../../inputs/input 2-2.txt");
            }
            else
                for (int i = 0; i < args.Count(); i++)
                    inputStrings.Add(args[i]);

            foreach (string inputString in inputStrings)
            {
                ThisSim mySim = new ThisSim();

                mySim.Init(inputString);

                if (mySim.inputParseError)
                    Console.WriteLine("One Or More Problems Parsing Input File.  Exiting.");
                else
                {
                    mySim.Run();

                    if (mySim.zoneLimits.Count == 1)    // unzoned
                    {
                        foreach (string k in mySim.totalParkCount.Keys)
                            if (k != "H")
                                Console.WriteLine(mySim.totalParkCount[k].ToString() + k);
                        Console.WriteLine(mySim.result.ToString("F0"));
                    }
                    else
                    {
                        // zoned
                        Console.WriteLine((mySim.totalZoneHours["C"] * mySim.zoneBonus["C"] +
                            (mySim.totalZoneHours.ContainsKey("H") ? mySim.totalZoneHours["H"] : 0) * mySim.zoneBonus["H"]).ToString("F1"));
                        Console.WriteLine(mySim.result.ToString("F1"));
                    }

                    Console.WriteLine("");
                    Console.WriteLine("");
                }
            }
        }
    }
}
