using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Contains order of steps.
 */
namespace Game_of_Life
{
    class Controller
    {
        private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private static View view;
        public const int ALIVE = 1;
        public const int DEAD = 0;
        private const int TIMER_INTERVAL_MS = 100;

        public static void firstSetup()
        {
            view = View.create(new EventHandler(CBPatternCallback));
            view.Visible = true;
            timer.Enabled = true;
            timer.Interval = TIMER_INTERVAL_MS;
            timer.Tick += new EventHandler(TimerCallback);
            init();
        }

        public static void init()
        {
            Model.Pattern pattern = (Model.Pattern)View.getSelectedPattern();            
            Model.initCells(View.NUMBER_OF_VERTICAL_CELLS, View.NUMBER_OF_HORIZONTAL_CELLS, pattern);
        }
        
        private static void TimerCallback(object sender, EventArgs e)
        {
            Model.updateCells();
            view.updateDrawing();
        }

        private static void CBPatternCallback(object sender, EventArgs e)
        {
            Controller.init();            
        }

        public static bool isAlive(int iRow, int iCol)
        {
            return Model.getCells()[iRow, iCol] == ALIVE;
        }
    }
}
