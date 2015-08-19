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
        int totalRegularParkHours;

        public Dictionary<string, int> zoneLimits;
        Dictionary<string, double> zoneRates;
        public Dictionary<string, double> zoneBonus;
        Dictionary<string, string> zoneRestrictions;
        Dictionary<string, int> currentZone;
        public Dictionary<string, int> totalZoneHours;

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
            totalParkCount.Add("H", 0);
            totalRegularParkHours = 0;

            incrementalParkers = new List<string>();
            zoneLimits = new Dictionary<string, int>();
            currentZone = new Dictionary<string, int>();
            zoneRestrictions = new Dictionary<string, string>();
            totalZoneHours = new Dictionary<string, int>();
            zoneRates = new Dictionary<string, double>();
            zoneBonus = new Dictionary<string, double>();

            System.IO.StreamReader f = GetInputFile(inputFile);
            if (f == null)
                return !(inputParseError = true);

            string s;

            while (!inputParseError && (s = f.ReadLine()) != null)
            {
                if (incrementalParkers.Count == 0 && zoneLimits.Count == 0) // first line
                {
                    if (s.IndexOf("R") >= 0)   // it sets up zones
                    {
                        foreach (string zone in s.Trim().Split())
                        {
                            string zoneType = zone.Substring(zone.Length - 1);
                            int zoneCount = Convert.ToInt32(zone.Substring(0, zone.Length - 1));

                            zoneLimits.Add(zoneType, zoneCount);
                            currentZone.Add(zoneType, 0);
                            totalZoneHours.Add(zoneType, 0);
                        }
                        zoneRestrictions.Add("C", "MC");
                        zoneRestrictions.Add("H", "H");
                        zoneRates.Add("R", 5);
                        zoneRates.Add("C", 4);
                        zoneRates.Add("H", 4);
                        zoneBonus.Add("R", 0.0);
                        zoneBonus.Add("C", 1.5);
                        zoneBonus.Add("H", 2);
                        continue;
                    }
                    else
                    {
                        zoneLimits.Add("R", 99999);
                        currentZone.Add("R", 0);
                        totalZoneHours.Add("R", 0);
                        zoneRates.Add("R", 5.0);
                        zoneBonus.Add("R", 0.0);
                    }
                }
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

            result = 0;
            foreach (string zone in zoneLimits.Keys)
            {
                result += totalZoneHours[zone] * (zoneRates[zone] + zoneBonus[zone]);       // so far...
            }
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

                // see if this parker qualifies for a zone restriction
                string myZone = "R";
                foreach( string zone in zoneRestrictions.Keys )
                    if (zoneRestrictions[zone].IndexOf(parkType) >= 0 && currentZone[zone] < zoneLimits[zone])   // ok, could park here
                    {
                        myZone = zone;
                        break;
                    }
                int max = zoneLimits[myZone] - currentZone[myZone];
                if (parkCount > max)
                {
                    currentZone[myZone] += max;
                    int remainder = parkCount - max;
                    int rmax = zoneLimits["R"] - currentZone["R"];
                    if (remainder > rmax)
                        remainder = rmax; // and the rest have to circle...
                    currentZone["R"] += remainder;
                    totalParkCount["R"] += remainder;
                }
                else
                {
                    currentZone[myZone] += parkCount;
                }
                totalParkCount[parkType] += parkCount;

            }
        }
        private void UpdateTotalParkHours()
        {
            foreach (string s in zoneLimits.Keys)
            {
                totalZoneHours[s] += currentZone[s];
            }

            foreach (string s in totalParkCount.Keys)
            {
                totalRegularParkHours += totalParkCount[s];
            }
        }

    }
}
