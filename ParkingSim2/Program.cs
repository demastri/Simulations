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
            string inputString = (args.Count() < 1 ? "../../inputs/input 2.txt" : args[0]);

            ThisSim mySim = new ThisSim();

            mySim.Init(inputString);

            if (mySim.inputParseError)
                Console.WriteLine("One Or More Problems Parsing Input File.  Exiting.");
            else
            {
                mySim.Run();
                foreach (string k in mySim.totalParkCount.Keys)
                    Console.WriteLine(mySim.totalParkCount[k].ToString() + k);
                Console.WriteLine(mySim.result.ToString("F0"));
            }
        }
    }
}
