using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Affinity
{
    class ThisSim : Simulator.Simulator
    {
        #region Sim Parameters
        public const int rowCount = 25;
        public const int colCount = 25;
        public List<Resident> citizens = new List<Resident>();
        public List<Location> city = new List<Location>();

        int nbrGroups;
        int affinityPct;
        int openingPct;

        public Affinity.Form1 baseForm = null;
        #endregion

        public class Resident
        {
            public Location location;
            public int group;
            public int affinity;
            public bool moving;
        }
        public class Location
        {
            public Point location;
            public Resident curCitizen;
            public List<int> curaffinityScore;
        }

        public override bool Init(string s)
        {
            // format = "nbrGroups:affinityPct:openingPct"
            int index = s.IndexOf(':');
            nbrGroups = Convert.ToInt32(s.Substring(0, index));
            int nextIndex = s.IndexOf(':',index+1);
            affinityPct = Convert.ToInt32(s.Substring(index + 1, nextIndex - index - 1));
            openingPct = Convert.ToInt32(s.Substring(nextIndex + 1));

            Reset();
            return true;
        }
        public override void Reset()
        {
            citizens.Clear();
            city.Clear();
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    Resident r = new Resident();
                    Location l = new Location();
                    l.location = new Point(i, j);
                    
                    r.group = oracle.Next(nbrGroups);
                    r.affinity = oracle.Next(100);
                    r.moving = false;

                    r.location = l;
                    l.curCitizen = r;

                    city.Add(l);
                    citizens.Add(r);
                }
            CalcAffinityScores();
        }
        public override bool Done()
        {
            return false;   // should kill on step count only for now
        }      
        public override void AdvanceState()
        {
            currentTime++;
            List<Resident> mover = new List<Resident>();
            List<Location> openLoc = new List<Location>();
            /// ok - for each cell - see if this is an opening
            /// generate an affinity score (for each group and opening)
            foreach (Resident r in citizens)
            {
                if (oracle.Next(100) < openingPct)
                {
                    r.moving = true;
                    mover.Add(r);
                    openLoc.Add(r.location);
                    r.location.curCitizen = null;   // ok, they're out...
                }
            }
            // need list of targets and affinities by group
            // need list of movers who care
            // while there are movers who care
            //  for the highest affinity score where there's someone who cares for that group
            //      move this citizen to this location (remove from target and movers list)

            /// find final mappings for those that care - /* can't stay put ### */
            while (MoversWhoCare(mover) > 0)
            {
                List<Location> highestAffinityByGroup = GetAffinityTargets(openLoc);
                List<int> groupOrder = SortAffinityByGroup(highestAffinityByGroup);

                bool foundOne = false;
                for (int i = 0; !foundOne && i < nbrGroups; i++)
                {
                    foreach( Resident r in mover )
                        if (r.group == groupOrder[i])  // ok - we have a mover who cares
                        {
                            Location target = highestAffinityByGroup[groupOrder[i]];

                            mover.Remove( target.curCitizen = r );
                            openLoc.Remove( r.location = target );
 
                            foundOne = true;
                            break;
                        }
                }
            }

            /// randomize final mappings for those that don't - /* can't stay put ### */
            while (mover.Count > 0)
            {
                Resident r = mover[0];
                Location target = openLoc[oracle.Next(openLoc.Count)];

                mover.Remove(target.curCitizen = r);
                openLoc.Remove(r.location = target);
            }
            
            /// update local data for new mappings
            CalcAffinityScores();
            
            /// trigger driver application update if appropriate
            if( currentTime % 20 == 0 )
                baseForm.UpdateGrid();
        }

        public Resident FindCitizen(int r, int c)
        {
            foreach (Location l in city)
                if (l.location.X == r && l.location.Y == c)
                    return l.curCitizen;
            return null;
        }
        
        private List<int> SortAffinityByGroup(List<Location> affByGrp)
        {
            List<int> outList = new List<int>();
            for (int i = 0; i < nbrGroups; i++)
                outList.Add(i);
            bool done = false;
            while (!done)
            {
                done = true;
                for (int i = 0; i < nbrGroups-1; i++)
                    if (affByGrp[outList[i]].curaffinityScore[outList[i]] < affByGrp[outList[i + 1]].curaffinityScore[outList[i + 1]])
                    {
                        int temp = outList[i];
                        outList[i] = outList[i + 1];
                        outList[i + 1] = temp;
                        done = false;
                    }
            }
            return outList;
        }
        private List<Location> GetAffinityTargets(List<Location> openLoc) 
        {
            List<Location> outList = new List<Location>();
            for (int i = 0; i < nbrGroups; i++)
            {
                int curMax = -999;
                Location curLoc = null;
                foreach (Location l in openLoc)
                {
                    if (l.curaffinityScore[i] > curMax)
                    {
                        curMax = l.curaffinityScore[i];
                        curLoc = l;
                    }
                }
                outList.Add(curLoc);
            }
            return outList;
        }
        private int MoversWhoCare(List<Resident> m)
        {
            int count = 0;
            foreach (Resident r in m)
                if (r.affinity < affinityPct)
                    count++;
            return count;
        }
        private void CalcAffinityScores()
        {
            foreach (Location l in city)
            {
                l.curaffinityScore = new List<int>();
                for (int i = 0; i < nbrGroups; i++)
                {
                    int affinityScore = 0;
                    for (int row = -1; row <= +1; row++) // row 
                        for (int col = -1; col <= +1; col++)
                        {
                            int actRow = (row + l.location.X + rowCount) % colCount;
                            int actCol = (col + l.location.Y + colCount) % rowCount;

                            if (FindCitizen(actRow, actCol).group == i)
                                affinityScore++;
                            else
                                affinityScore--;
                        }
                    l.curaffinityScore.Add(affinityScore);
                }
            }
        }
    }
}
