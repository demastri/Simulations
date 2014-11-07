using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;

namespace ParkingSim
{
    class ThisSim : Simulator.Simulator
    {
        public Dictionary<string, int> totalParkCount;
        int totalParkHours;
        int parkingRate = 5;

        List<string> incrementalParkers;

        public ThisSim()
            : base()
        {
        }

        override public void Reset()
        {
            Init(inputStr);
        }
        override public bool Init(string inputFile)
        {
            inputStr = inputFile;

            inputParseError = false;

            currentTime = 0;
            executionError = false;


            totalParkCount = new Dictionary<string, int>();
            totalParkCount.Add("M", 0);
            totalParkCount.Add("C", 0);
            totalParkCount.Add("S", 0);
            totalParkCount.Add("V", 0);
            totalParkCount.Add("T", 0);
            totalParkHours = 0;

            incrementalParkers = new List<string>();

            System.IO.StreamReader f = GetInputFile(inputFile);
            if (f == null)
                return !(inputParseError = true);

            string s;

            while (!inputParseError && (s = f.ReadLine()) != null)
            {
                incrementalParkers.Add(s.Trim());
            }
            return inputParseError;
        }

        private System.IO.StreamReader GetInputFile(string inputFile)
        {
            System.IO.StreamReader f = null;
            if (inputFile == null)
                Console.WriteLine("No Input File Specified.  Exiting.");
            else if (!System.IO.File.Exists(inputFile))
                Console.WriteLine("Input File Does Not Exist.  Exiting.");
            else
                f = new System.IO.StreamReader(inputFile);

            return f;
        }
        override public bool Done()
        {
            return incrementalParkers.Count == 0;
        }
        override public void AdvanceState()
        {
            currentTime++;

            string nextParkers = "";
            if( incrementalParkers.Count > 0 )
                nextParkers = incrementalParkers[0];

            ProcessNewParkers(nextParkers);
            UpdateTotalParkHours();

            if (incrementalParkers.Count > 0)
                incrementalParkers.RemoveAt(0);

            result = totalParkHours*parkingRate;       // so far...
        }

        private void ProcessNewParkers(string inputStr)
        {
            if (inputStr.Trim() == "")
                return;
            foreach (string s in inputStr.Split(new Char[] { ' ', '\t' }))
            {
                string ss = s.Trim();
                string parkType = ss.Substring(ss.Length - 1);
                int parkCount = Convert.ToInt32( ss.Substring(0,ss.Length - 1) );

                if( totalParkCount.ContainsKey( parkType ) )
                    totalParkCount[parkType] += parkCount;

            }
        }
        private void UpdateTotalParkHours()
        {
            foreach (string s in totalParkCount.Keys)
            {
                totalParkHours += totalParkCount[s];
            }
        }

    }
}
