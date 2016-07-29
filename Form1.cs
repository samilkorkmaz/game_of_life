using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Contains GUI.
 */
namespace Game_of_Life
{
    public partial class View : Form
    {
        public const int NUMBER_OF_HORIZONTAL_CELLS = 35;
        public const int NUMBER_OF_VERTICAL_CELLS = 25;

        private static View instance;
        private const int GRID_X_OFFSET = 10;
        private const int GRID_Y_OFFSET = 20;
        private SolidBrush brushAlive = new SolidBrush(Color.Green);
        private SolidBrush brushDead = new SolidBrush(Color.Yellow);
        private Pen myPen = new Pen(Color.Black);

        public static View create(System.EventHandler cbPatternEventHandler)
        {
            if (instance == null)
            {
                instance = new View();
                instance.cbPattern.SelectedIndexChanged += cbPatternEventHandler;
            }
            return instance;            
        }

        private View()
        {
            InitializeComponent();
            cbPattern.SelectedIndex = 2;
            this.DoubleBuffered = true; //to remove flicker when updating drawing
            this.ResizeRedraw = true; //to resize drawing when window is resized 
        }

        public void updateDrawing()
        {
            instance.Invalidate();
        }

        public static int getSelectedPattern()
        {
            return instance.cbPattern.SelectedIndex;
        }
                
        private int calcCellSize()
        {
            int w = this.Width - 2 * GRID_X_OFFSET;
            int h = this.Height - 2 * GRID_Y_OFFSET;
            int s1 = w / NUMBER_OF_HORIZONTAL_CELLS;
            int s2 = h / NUMBER_OF_VERTICAL_CELLS;
            return s1 < s2 ? s1 : s2;
        }

        private void drawVertical(Graphics g, int iCol, int cellSize)
        {
            int xv0 = GRID_X_OFFSET + iCol * cellSize;
            int yv0 = GRID_Y_OFFSET;
            int xv1 = xv0;
            int yv1 = yv0 + NUMBER_OF_VERTICAL_CELLS * cellSize;
            g.DrawLine(myPen, xv0, yv0, xv1, yv1);
        }

        private void drawHorizontal(Graphics g, int iRow, int cellSize)
        {
            int xh0 = GRID_X_OFFSET;
            int yh0 = GRID_Y_OFFSET + iRow * cellSize;
            int xh1 = xh0 + NUMBER_OF_HORIZONTAL_CELLS * cellSize;
            int yh1 = yh0;
            g.DrawLine(myPen, xh0, yh0, xh1, yh1);
        }

        private void drawGrid(Graphics g, int cellSize)
        {
            
            myPen.Width = 1;
            for (int iRow = 0; iRow < NUMBER_OF_VERTICAL_CELLS; iRow++)
            {
                for (int iCol = 0; iCol < NUMBER_OF_HORIZONTAL_CELLS; iCol++)
                {
                    drawVertical(g, iCol, cellSize);
                    drawHorizontal(g, iRow, cellSize);
                }
            }
            //Right-most vertical
            drawVertical(g, NUMBER_OF_HORIZONTAL_CELLS, cellSize);            
            //Bottom horizontal
            drawHorizontal(g, NUMBER_OF_VERTICAL_CELLS, cellSize);            
        }

        private void drawDeadAlive(Graphics g, int cellSize)
        {
            //Draw alive cells as filled rects            
            for (int iRow = 0; iRow < NUMBER_OF_VERTICAL_CELLS; iRow++)
            {
                for (int iCol = 0; iCol < NUMBER_OF_HORIZONTAL_CELLS; iCol++)
                {
                    int x = GRID_X_OFFSET + iCol * cellSize;
                    int y = GRID_Y_OFFSET + iRow * cellSize;
                    Rectangle cellRect = new Rectangle(x, y, cellSize, cellSize);
                    if (Controller.isAlive(iRow, iCol))
                    {
                        g.FillRectangle(brushAlive, cellRect);
                    }
                    else
                    {
                        g.FillRectangle(brushDead, cellRect);
                    }
                }
            }            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap myBitmap = new Bitmap(this.Width, this.Height); //Bitmap is used to prevent flicker. It resizes according to current form size.
            Graphics g = Graphics.FromImage(myBitmap);
            int cellSize = calcCellSize();
            drawDeadAlive(g, cellSize);
            drawGrid(g, cellSize);
            e.Graphics.DrawImage(myBitmap, 0, 0);
            g.Dispose();
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            brushAlive.Dispose();
            brushDead.Dispose();
            myPen.Dispose();
            Application.Exit();
        }

    }
}
