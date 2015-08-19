using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affinity
{
    public partial class Form1 : Form
    {
        ThisSim mySim;
        Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange };

        public Form1()
        {
            InitializeComponent();

            mySim = new ThisSim();
            mySim.baseForm = this;

            InitGrid();
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            InitGrid();
            mySim.Run();
            UpdateGrid();
        }
        public void InitGrid()
        {
            int grps = Convert.ToInt32(GroupCount.Text);
            int aff = Convert.ToInt32(AffinityPct.Text);
            int open = Convert.ToInt32(OpeningPct.Text);
            int steps = Convert.ToInt32(StepCount.Text);

            mySim.Init(grps.ToString()+":"+aff.ToString()+":"+open.ToString());
            mySim.stepLimit = steps;

            displayGrid.Rows.Clear();
            displayGrid.Columns.Clear();

            for (int j = 0; j < ThisSim.colCount; j++)
            {
                displayGrid.Columns.Add("Col" + j.ToString(), "Col" + j.ToString());
                displayGrid.Columns[j].Width = 15;
            }
            for (int i = 0; i < ThisSim.rowCount; i++)
                displayGrid.Rows.Add();
            CurStepLabel.Text = "Step Count: " + mySim.currentTime.ToString();
            UpdateGrid();
        }
        public void UpdateGrid()
        {
            for (int i = 0; i < ThisSim.rowCount; i++)
            {
                for (int j = 0; j < ThisSim.colCount; j++)
                {
                    displayGrid.Rows[i].Cells[j].Value = mySim.FindCitizen(i, j).group;
                    (displayGrid.Rows[i].Cells[j]).Style.BackColor = colors[mySim.FindCitizen(i, j).group];
                }
            }
            CurStepLabel.Text = "Step Count: " + mySim.currentTime.ToString();
            Refresh();
        }
    }
}
