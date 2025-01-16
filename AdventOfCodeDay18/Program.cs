namespace AdventofCodeDay18 {
    class Program {
            static Dictionary<(int,int),int> minScore = new Dictionary<(int,int),int>();
            static int goalX = 70;
            static int goalY = 70;
            static int simulationDistance = 3000;
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            List<(int,int)> gridCoords = new List<(int,int)>();
            bool success = false;
            int j = 2950;
            while (!success) {
                j++;
                gridCoords.Clear();
                minScore.Clear();
                for (int i = 0; i < j; i++) {
                    gridCoords.Add((Convert.ToInt32(rawInput[i].Split(",")[0]),Convert.ToInt32(rawInput[i].Split(",")[1])));
                }
                minScore[(0,0)] = 0;
                try {
                    Move(gridCoords, 0, 0);
                    Console.WriteLine(j + ": " + minScore[(goalX,goalY)]);
                }
                catch (KeyNotFoundException) {
                    success = true;
                }
                if (j >= simulationDistance) {success = true;}
            }
            Console.WriteLine("Result: " + gridCoords.Last());
        }

        private static void Move(List<(int,int)> grid, int x, int y) {
            if (!grid.Contains((x - 1, y)) && x > 0) {
                bool cont = false;
                if (!minScore.ContainsKey((x - 1, y))) {
                        minScore[(x - 1, y)] = minScore[(x,y)] + 1;
                        cont = true;
                }
                else if (minScore.ContainsKey((x - 1, y))) {
                    if (minScore[(x - 1, y)] > minScore[(x,y)] + 1) {
                        minScore[(x - 1, y)] = minScore[(x,y)] + 1;
                        cont = true;
                    }
                }
                if ((x != goalX || y != goalY) && cont) {
                    Move(grid, x - 1, y);
                }
            }
            if (!grid.Contains((x + 1, y)) && x < goalX) {
                bool cont = false;
                if (!minScore.ContainsKey((x + 1, y))) {
                        minScore[(x + 1, y)] = minScore[(x,y)] + 1;
                        cont = true;
                }
                else if (minScore.ContainsKey((x + 1, y))) {
                    if (minScore[(x + 1, y)] > minScore[(x,y)] + 1) {
                        minScore[(x + 1, y)] = minScore[(x,y)] + 1;
                        cont = true;
                    }
                }
                if ((x != goalX || y != goalY) && cont) {
                    Move(grid, x + 1, y);
                }
            }
            if (!grid.Contains((x, y - 1)) && y > 0) {
                bool cont = false;
                if (!minScore.ContainsKey((x, y - 1))) {
                        minScore[(x, y - 1)] = minScore[(x,y)] + 1;
                        cont = true;
                }
                else if (minScore.ContainsKey((x, y - 1))) {
                    if (minScore[(x, y - 1)] > minScore[(x,y)] + 1) {
                        minScore[(x, y - 1)] = minScore[(x,y)] + 1;
                        cont = true;
                    }
                }
                if ((x != goalX || y != goalY) && cont) {
                    Move(grid, x, y - 1);
                }
            }
            if (!grid.Contains((x, y + 1)) && y < goalY) {
                bool cont = false;
                if (!minScore.ContainsKey((x, y + 1))) {
                        minScore[(x, y + 1)] = minScore[(x,y)] + 1;
                        cont = true;
                }
                else if (minScore.ContainsKey((x, y + 1))) {
                    if (minScore[(x, y + 1)] > minScore[(x,y)] + 1) {
                        minScore[(x, y + 1)] = minScore[(x,y)] + 1;
                        cont = true;
                    }
                }
                if ((x != goalX || y != goalY) && cont) {
                    Move(grid, x, y + 1);
                }
            }
        }
    }
}