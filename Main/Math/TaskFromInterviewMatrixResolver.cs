using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Math
{
    public class SubMatrix
    {
        public int x_min, y_min, x_max, y_max;
        public int index;

        public SubMatrix(int x_0, int y_0, int x = 0, int y = 0)
        {
            x_min = x_0;
            x_max = x;
            y_min = y_0;
            y_max = y;
        }

        public int Area
        {
            get
            {
                int w = x_max - x_min;
                int h = y_max - y_min;

                return w * h;
            }
        }
    }

    public class TaskFromInterviewMatrixResolver
    {
        const int max_X = 10;
        const int max_Y = 10;

        private readonly int[][] _martix = new int[max_X][];

        public TaskFromInterviewMatrixResolver()
        {
            for (int x = 0; x < max_X; x++)
            {
                _martix[x] = new int[max_Y];
            }

            for (int x = 0; x < max_X; x++)
            {
                for (int y = 0; y < max_Y; y++)
                {
                    _martix[x][y] = 0;
                }
            }

            _martix[0][0] = 1;
            _martix[4][0] = 1;

            _martix[8][0] = 1;
            _martix[9][0] = 1;
            _martix[8][1] = 1;
            _martix[9][1] = 1;
            _martix[8][2] = 1;
            _martix[9][2] = 1;

            _martix[2][4] = 1;
            _martix[4][2] = 1; // 

            _martix[3][8] = 1;
            _martix[4][8] = 1;
            _martix[5][8] = 1;

            _martix[3][9] = 1;
            _martix[4][9] = 1;
            _martix[5][9] = 1;
        }

        public void Process()
        {
            List<SubMatrix> subMatrices = new List<SubMatrix>();

            int counter = 2;

            while (IsMatrixEmpty == false)
            {
                List<SubMatrix> tmpList = new List<SubMatrix>();

                for (int x = 0; x < max_X; x++)
                    for (int y = 0; y < max_Y; y++)
                    {
                        if (_martix[x][y] != 1)
                            tmpList.Add(GetSubMatrix(x, y));
                    }

                SubMatrix best = null;
                tmpList.ForEach(sm =>
                {
                    if (best == null)
                        best = sm;

                    if (sm.Area > best.Area)
                        best = sm;
                });

                best.index = 1;
                SaveToMatrix(best);
                best.index = counter++;
                subMatrices.Add(best);
            }

            subMatrices.ForEach(SaveToMatrix);

            subMatrices.ForEach(sm =>
            {
                Console.WriteLine("Index: {0}, Area: {1}", sm.index, sm.Area);
            });

            Draw(_martix);
        }

        private void SaveToMatrix(SubMatrix sm)
        {
            for (int x = sm.x_min; x < sm.x_max; x++)
                for (int y = sm.y_min; y < sm.y_max; y++)
                {
                    _martix[x][y] = sm.index;
                }
        }

        private bool IsMatrixEmpty
        {
            get
            {
                for (int x = 0; x < max_X; x++)
                    for (int y = 0; y < max_Y; y++)
                        if (_martix[x][y] == 0)
                            return false;

                return true;
            }
        }

        public void Draw(int[][] matrix)
        {
            for (int y = 0; y < max_Y; y++)
            {
                if (y == 0)
                {
                    Console.Write("  ");
                    for (int x = 0; x < max_X; x++)
                    {
                        Console.Write(x + " ");
                    }
                    Console.WriteLine();
                }

                for (int x = 0; x < max_X; x++)
                {
                    if (x == 0)
                        Console.Write("{0} ", y);

                    DrawCell(matrix[x][y]);
                }
                Console.WriteLine();
            }
        }

        private void DrawCell(int i)
        {
            Console.ResetColor();

            switch (i)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("-");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("1");
                    break;
                case 10:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("X");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(i);
                    break;
            }
            Console.ResetColor();
            Console.Write(" ");
        }

        private SubMatrix GetSubMatrix(int x_l, int y_l)
        {
            SubMatrix hor = GetSubMatrixHor(x_l, y_l);
            SubMatrix ver = GetSubMatrixVer(x_l, y_l);

            return hor.Area > ver.Area ? hor : ver;
        }

        private SubMatrix GetSubMatrixVer(int x_0, int y_0)
        {
            SubMatrix sm = new SubMatrix(x_0, y_0, max_X, max_Y);

            for (int y = sm.y_min; y < sm.y_max; y++)
            {
                if (_martix[sm.x_min][y] == 1)
                {
                    sm.y_max = y;
                    break;
                }

                for (int x = sm.x_min; x < sm.x_max; x++)
                {
                    if (_martix[x][y] == 1)
                    {
                        sm.x_max = x;
                        break;
                    }
                }
            }
            return sm;
        }

        private SubMatrix GetSubMatrixHor(int x_0, int y_0)
        {
            SubMatrix sm = new SubMatrix(x_0, y_0, max_X, max_Y);

            sm.x_max = max_X;
            sm.y_max = max_Y;

            for (int x = sm.x_min; x < sm.x_max; x++)
            {
                if (_martix[x][sm.y_min] == 1)
                {
                    sm.x_max = x;
                    break;
                }

                for (int y = sm.y_min; y < sm.y_max; y++)
                {
                    if (_martix[x][y] == 1)
                    {
                        sm.y_max = y;
                        break;
                    }
                }
            }
            return sm;
        }
    }
}
