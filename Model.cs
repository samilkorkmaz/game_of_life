using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Convay's Game of Life algorithms. Reference: Wikipedia.
 */
namespace Game_of_Life
{
    class Model
    {
        public enum Pattern
        {
            Blinker,
            Glider,
            RPentomino,
        };

        private static int[,] cells;

        public static void initCells(int nVerticalCell, int nHorizontalCells, Pattern pattern)
        {
            cells = new int[nVerticalCell, nHorizontalCells];
            for (int iRow = 0; iRow < nVerticalCell; iRow++)
            {
                for (int iCol = 0; iCol < nHorizontalCells; iCol++)
                {
                    cells[iRow, iCol] = Controller.DEAD;
                }
            }
            switch (pattern)
            {
                case Pattern.Blinker:
                    cells[1, 1] = 1;
                    cells[1, 2] = 1;
                    cells[1, 3] = 1;
                    break;
                case Pattern.Glider:
                    cells[1, 0] = 1;
                    cells[2, 1] = 1;
                    cells[0, 2] = 1;
                    cells[1, 2] = 1;
                    cells[2, 2] = 1;
                    break;
                case Pattern.RPentomino:
                    cells[2, 1] = 1;
                    cells[1, 2] = 1;
                    cells[2, 2] = 1;
                    cells[3, 2] = 1;
                    cells[1, 3] = 1;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException(pattern.ToString());
            }
        }

        public static int[,] getCells()
        {
            return cells;
        }

        public static void updateCells()
        {
            int[,] tempCells = new int[cells.GetLength(0), cells.GetLength(1)];
            for (int iRow = 0; iRow < View.NUMBER_OF_VERTICAL_CELLS; iRow++)
            {
                for (int iCol = 0; iCol < View.NUMBER_OF_HORIZONTAL_CELLS; iCol++)
                {
                    tempCells[iRow, iCol] = cells[iRow, iCol];

                    int wCell = Controller.DEAD;
                    int nCell = Controller.DEAD;
                    int eCell = Controller.DEAD;
                    int sCell = Controller.DEAD;
                    int nwCell = Controller.DEAD;
                    int neCell = Controller.DEAD;
                    int swCell = Controller.DEAD;
                    int seCell = Controller.DEAD;

                    int iW = iCol - 1;
                    int iN = iRow - 1;
                    int iE = iCol + 1;
                    int iS = iRow + 1;

                    if (iW >= 0)
                    {
                        wCell = cells[iRow, iW];
                        if (iN >= 0)
                        {
                            nwCell = cells[iN, iW];
                        }
                        if (iS < View.NUMBER_OF_VERTICAL_CELLS)
                        {
                            swCell = cells[iS, iW];
                        }
                    }
                    if (iE < View.NUMBER_OF_HORIZONTAL_CELLS)
                    {
                        eCell = cells[iRow, iE];
                        if (iN >= 0)
                        {
                            neCell = cells[iN, iE];
                        }
                        if (iS < View.NUMBER_OF_VERTICAL_CELLS)
                        {
                            seCell = cells[iS, iE];
                        }
                    }
                    if (iN >= 0)
                    {
                        nCell = cells[iN, iCol];
                    }
                    if (iS < View.NUMBER_OF_VERTICAL_CELLS)
                    {
                        sCell = cells[iS, iCol];
                    }
                    //Game of life rules:
                    int sum = wCell + nCell + eCell + sCell + nwCell + neCell + swCell + seCell;
                    if (sum < 2 || sum > 3)
                    {
                        tempCells[iRow, iCol] = Controller.DEAD;
                    }
                    if (sum == 3)
                    {
                        tempCells[iRow, iCol] = Controller.ALIVE;
                    }
                }
            }
            Model.cells = tempCells;
        }
    }
}
