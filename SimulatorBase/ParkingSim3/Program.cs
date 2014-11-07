using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSim3
{
    class Program
    {
        class Parkers
        {
            public string vType;
            public string plate;
            public DateTime lastParked;
            public SortedList<DateTime, DateTime> violations;
            public int totalFines;
            public DateTime lastViolation { get { return violations.ElementAt(violations.Count - 1).Value; } }

            public bool CanPark(DateTime d)
            {
                // two violations, within a year, and this date is within the 6 month suspension period
                if (violations.Count >= 3 &&
                    (violations.ElementAt(violations.Count - 1).Value.Subtract(violations.ElementAt(violations.Count - 3).Value) < new TimeSpan(365, 0, 0, 0)) &&
                    d < violations.ElementAt(violations.Count - 1).Value.AddMonths(6))
                    return false;
                return true;
            }

            public Parkers(string s)
            {
                violations = new SortedList<DateTime, DateTime>();
                totalFines = 0;

                string[] tokens = s.Split();
                int hrs = Convert.ToInt32(tokens[0]);
                vType = tokens[1];
                plate = tokens[2];
                lastParked = Convert.ToDateTime(tokens[3]);

                if (hrs > 5)
                {
                    violations.Add(lastParked, lastParked);
                    totalFines = 40 * (hrs - 5);
                }
            }
            public void Merge(Parkers p)
            {
                foreach (DateTime v in p.violations.Keys)
                {
                    violations.Add(v, v);
                }
                totalFines += p.totalFines;
                if (p.lastParked > lastParked)
                    lastParked = p.lastParked;
            }
        }
        static void Main(string[] args)
        {
            List<string> inputStrings = new List<string>();
            if (args.Count() < 1)
            {
                inputStrings.Add("../../inputs/input 1.txt");
                inputStrings.Add("../../inputs/input 2.txt");
                inputStrings.Add("../../inputs/input 3.txt");
            }
            else
                for (int i = 0; i < args.Count(); i++)
                    inputStrings.Add(args[i]);

            foreach (string inputString in inputStrings)
            {

                Dictionary<string, Parkers> customers = new Dictionary<string, Parkers>();

                string s;
                System.IO.StreamReader f = GetInputFile(inputString);
                while (f != null && (s = f.ReadLine()) != null)
                {
                    Parkers p = new Parkers(s);
                    if (customers.ContainsKey(p.plate))
                    {
                        if (customers[p.plate].CanPark(p.lastParked))
                            customers[p.plate].Merge(p);
                    }
                    else
                        customers.Add(p.plate, p);
                }
                foreach (string pl in customers.Keys)
                {
                    Parkers p = customers[pl];
                    DateTime nextPark = p.lastViolation.AddMonths(6);
                    Console.WriteLine(p.vType + " " + p.plate + " " +
                        (!p.CanPark(DateTime.Now) ? "T " : "F ") + p.lastParked.Year + "-" + p.lastParked.Month.ToString("D2") + "-" + p.lastParked.Day.ToString("D2") + " " +
                        p.totalFines.ToString() + " " +
                        (p.CanPark(DateTime.Now) ? "" : nextPark.Year + "-" + nextPark.Month.ToString("D2") + "-" + nextPark.Day.ToString("D2") + " ")

                        );
                }

                Console.WriteLine("");
                Console.WriteLine("");
            }

        }
        static private System.IO.StreamReader GetInputFile(string inputFile)
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
    }
}
