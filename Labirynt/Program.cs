using System;
using System.Runtime.InteropServices;

namespace GameOfLife
{

    public class LifeSimulation
    {
        private int Heigth;
        private int Width;
        private bool[,] cells;

        public LifeSimulation(int Heigth, int Width)
        {
            this.Heigth = Heigth;
            this.Width = Width;
            cells = new bool[Heigth, Width];
            GenerateField();
        }

        public void DrawAndGrow()
        {
            DrawGame();
            Grow();
        }

        private void Grow()
        {
            for (int i = 2; i < Heigth - 2; i++)
            {
                for (int j = 2; j < Width - 2; j++)
                {
                    int numOfAliveNeighbors = GetNeighbors(i, j);
                    if (cells[i, j] == true)
                    {
                        if (numOfAliveNeighbors > 2)
                        {
                            cells[i, j] = false;
                        }
                        if (numOfAliveNeighbors >= 1 && numOfAliveNeighbors <= 5)
                        {
                            cells[i, j] = true;
                        }
                        if (numOfAliveNeighbors == 0)
                        {
                            cells[i, j] = false;
                        }
                        else if (numOfAliveNeighbors == 3)
                        {
                                cells[i - 1, j - 1] = false;
                                cells[i + 1, j + 1] = false;
                        }
                    }
                    else if (cells[i, j] == false)
                    {
                        if (numOfAliveNeighbors == 2)
                        {
                            cells[i, j] = true;
                        }
                        if (numOfAliveNeighbors == 5 || numOfAliveNeighbors == 6)
                        {
                            cells[i - 1, j - 1] = false;
                            cells[i + 1, j + 1] = false;
                        }
                        if (cells[i + 1, j] == true && cells[i - 1, j] == true)
                        {
                            Random generator = new Random();
                            int number;
                            number = generator.Next(2);
                            if (number == 0)
                            {
                                cells[i, j + 1] = false;
                            }
                            else
                            {
                                cells[i, j - 1] = false;
                            }
                        }
                    }

                }
            }
        }
        private void FinishingTouches()
        {
            for (int i = 2; i < Heigth - 1; i++)
            {
                for (int j = 2; j < Width - 1; j++)
                {
                    if (cells[i, j] == false && (cells[i + 1, j] == true) &&
                               (cells[i, j + 1] == true) && (cells[i - 1, j] == true) && (cells[i, j - 1] == true))
                    {
                        cells[i, j] = true;
                    }
                }
            }
        }

        private int GetNeighbors(int x, int y)
        {
            int NumOfAliveNeighbors = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0)))
                    {
                        if (cells[i, j] == true) NumOfAliveNeighbors++;
                    }
                }
            }
            return NumOfAliveNeighbors;
        }

        private void DrawGame()
        {
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(cells[i, j] ? "█" : " ");
                    if (j == Width - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        private void GenerateField()
        {
            Random generator = new Random();
            int number;
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == 0 || i == Heigth - 1 || j == 0 || j == Width - 1)
                    {
                        cells[i, j] = true;
                    }
                    else
                    {
                        number = generator.Next(2);
                        cells[i, j] = ((number == 0) ? false : true);
                    }
                }
            }
        }

        internal class Program
        {
            private const int Heigth = 25;
            private const int Width = 100;
            private const uint MaxRuns = 15;

            private static void Main(string[] args)
            {
                int runs = 0;
                LifeSimulation sim = new LifeSimulation(Heigth, Width);

                while (runs++ < MaxRuns)
                {
                    sim.DrawAndGrow();
                    //System.Threading.Thread.Sleep(10);
                }
                sim.FinishingTouches();
                sim.DrawGame();
                Console.ReadLine();
            }
        }
    }
}

