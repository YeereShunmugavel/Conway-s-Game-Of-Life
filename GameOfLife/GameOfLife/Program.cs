using System;
using System.Collections.Generic;

namespace GameOfLife
{
    class Program
    {
        public static void Main()
        {
            int M = 25, N = 25;
            List<string> initalGenerations = new List<string>();
            
            // Initializing the grid. (25, 25)
            int[,] grid = new int[M,N];

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    grid[i, j] = 0;
                }
            }
        
            Console.WriteLine("Which generation's population position are you interested in?\r");
            int NOG = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How many Input population you want to give for Generation ZERO\r");
            int NOP = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Input the population positions of Generation ZERO\r");
            for (int i = 0; i < NOP; i++)
            {
                var line = Console.ReadLine();
                var data = line.Split(' ');
                grid[int.Parse(data[0]), int.Parse(data[1])] = 1;
            }

            // Displaying the inital generation
            Console.WriteLine("\nGeneration ZERO");
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (grid[i, j] > 0)
                        initalGenerations.Add(string.Format("({0},{1})", i, j));
                }
            }
            Console.WriteLine("[{0}]", string.Join(",", initalGenerations));
            findGeneration(grid, M, N, NOG);
            Console.ReadKey();
        }

        // Function to print Nth generation
        static void findGeneration(int[,] grid,
                                   int M, int N, int NOG)
        {
            int[,] future = new int[M, N];
            List<string> finalGeneration = new List<string>();

            for (int z = 0; z < NOG; z++)
            {
                if(z > 0)
                {
                    grid = future;
                    future = new int[M, N];
                }
                // Loop through every cell
                for (int l = 1; l < M - 1; l++)
                {
                    for (int m = 1; m < N - 1; m++)
                    {

                        // finding no Of Neighbours
                        // that are alive
                        int aliveNeighbours = 0;
                        for (int i = -1; i <= 1; i++)
                            for (int j = -1; j <= 1; j++)
                                aliveNeighbours +=
                                        grid[l + i, m + j];

                        // The cell needs to be subtracted from its neighbours as it was counted before
                        aliveNeighbours -= grid[l, m];

                        // Implementing the Rules of Life

                        // Cell is lonely and dies
                        if ((grid[l, m] == 1) &&
                                    (aliveNeighbours < 2))
                            future[l, m] = 0;

                        // Cell dies due to over population
                        else if ((grid[l, m] == 1) &&
                                     (aliveNeighbours > 3))
                            future[l, m] = 0;

                        // A new cell is born
                        else if ((grid[l, m] == 0) &&
                                    (aliveNeighbours == 3))
                            future[l, m] = 1;

                        // Remains the same
                        else
                            future[l, m] = grid[l, m];
                    }
                }
            }

            // Printing Nth Generation population positions

            Console.WriteLine("\n\nGeneration " + NOG);

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (future[i, j] > 0)
                        finalGeneration.Add(string.Format("({0},{1})", i, j));

                }
            }
            Console.WriteLine("[{0}]", string.Join(",", finalGeneration));
        }
    }
}
