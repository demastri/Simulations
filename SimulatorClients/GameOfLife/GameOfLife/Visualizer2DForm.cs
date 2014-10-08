using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Visualizer2DForm : Form
    {
        public Life game;
        Color backColor = Color.White;
        Color foreColor = Color.Black;

        public Visualizer2DForm()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            if (inResize)
                return;
            try
            {
                // get pixel grid for this game
                int xPix = canvas.Width / game.gridSizeLimit[0];
                int yPix = canvas.Height / game.gridSizeLimit[1];
                int pix = xPix > yPix ? yPix : xPix;
                Graphics g = e.Graphics;
                Pen fPen = new Pen(foreColor);
                Pen bPen = new Pen(backColor);

                foreach (Location l in game.curState.Keys)
                {
                    switch (game.geometry)
                    {
                        case 4:
                            g.DrawRectangle(
                                game.curState[l] == 1 ? fPen : bPen,
                                pix * l.index[0], pix * l.index[1], pix - 1, pix - 1);
                            break;
                        case 3:
                            bool up = (((l.index[0] % 2) + (l.index[1] % 2) % 2) == 1);
                            Point[] upArr = {  new Point( pix * l.index[0]+(pix-1)/2, pix * l.index[1]), 
                                                 new Point( pix * l.index[0]-pix/2, pix * l.index[1]+pix-1), 
                                                 new Point( pix * l.index[0]+pix/2+pix-1, pix * l.index[1]+pix-1) };
                            Point[] dnArr = {  new Point( pix * l.index[0]-pix/2, pix * l.index[1]), 
                                                 new Point( pix * l.index[0]+pix/2 + pix - 1, pix * l.index[1]), 
                                                 new Point( pix * l.index[0]+(pix-1)/2, pix * l.index[1]+pix-1) };

                            g.DrawPolygon(game.curState[l] == 1 ? fPen : bPen, up ? upArr : dnArr);
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                System.Console.WriteLine(ee.Message);
            }
        }

        private bool inResize = false;
        private void Visualizer2DForm_ResizeBegin(object sender, EventArgs e)
        {
            inResize = true;
        }

        private void Visualizer2DForm_ResizeEnd(object sender, EventArgs e)
        {
            inResize = false;
        }
    }
}
