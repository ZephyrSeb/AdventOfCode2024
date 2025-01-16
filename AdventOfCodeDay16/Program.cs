namespace AdventofCodeDay16 {
    class Program {
            static int reindeerX = 0;
            static int reindeerY = 0;
            static int reindeerRot = 0;
            static Dictionary<(int,int),int> minScore = new Dictionary<(int,int),int>();
            static int endRot;

        public static void Main() {
            Console.Clear();
            int goalX = 0;
            int goalY = 0;
            string[] rawInput = File.ReadAllLines("testInput.txt");
            char[,] grid = new char[rawInput[0].Length,rawInput.Length];
            for (int i = 0; i < rawInput.Length; i++) {
                for (int j = 0; j < rawInput[0].Length; j++) {
                    grid[j,i] = rawInput[i][j];
                    if (grid[j,i] == 'S') {
                        reindeerX = j;
                        reindeerY = i;
                    }
                    if (grid[j,i] == 'E') {
                        goalX = j;
                        goalY = i;
                    }
                }
            }
            minScore[(reindeerX,reindeerY)] = 0;
            moveReindeer(grid, reindeerX, reindeerY, reindeerRot, 0);
            grid = bestPath(grid,goalX,goalY,endRot);
            Console.WriteLine(endRot);
            int result = 0;
            for (int i = 0; i < rawInput.Length; i++) {
                for (int j = 0; j < rawInput[0].Length; j++) {
                    Console.Write(grid[j,i]);
                    if (grid[i,j] == 'O') {
                        result += 1;
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("Result: " + minScore[(4,7)]);
            Console.WriteLine("Result: " + minScore[(5,7)]);
            Console.WriteLine("Result: " + result);
        }

        private static void moveReindeer(char[,] grid, int x, int y, int rot, int trials) {
            if (trials < 1000) {
                int dirX = 0;
                int dirY = 0;
                if (rot == 0) {dirX = 1; dirY = 0;}
                if (rot == 1) {dirX = 0; dirY = -1;}
                if (rot == 2) {dirX = -1; dirY = 0;}
                if (rot == 3) {dirX = 0; dirY = 1;}
                if (grid[x + dirX, y + dirY] != '#') {
                    bool cont = false;
                    if (!minScore.ContainsKey((x + dirX, y + dirY))) {
                        minScore[(x + dirX, y + dirY)] = minScore[(x,y)] + 1;
                        cont = true;
                        if (grid[x + dirX, y + dirY] == 'E') {
                            endRot = rot;
                        }
                    }
                    else if (minScore.ContainsKey((x + dirX, y + dirY))) {
                        if (minScore[(x + dirX, y + dirY)] >= minScore[(x,y)] + 1) {
                            minScore[(x + dirX, y + dirY)] = minScore[(x,y)] + 1;
                            cont = true;
                        if (grid[x + dirX, y + dirY] == 'E') {
                            endRot = rot;
                        }
                        }
                    }
                    if (grid[x + dirX, y + dirY] != 'E' && cont) {
                        moveReindeer(grid, x + dirX, y + dirY, rot, trials + 1);
                    }
                }
                if (rot == 0) {dirX = 0; dirY = -1;}
                if (rot == 1) {dirX = -1; dirY = 0;}
                if (rot == 2) {dirX = 0; dirY = 1;}
                if (rot == 3) {dirX = 1; dirY = 0;}
                if (grid[x + dirX, y + dirY] != '#') {
                    bool cont = false;
                    if (!minScore.ContainsKey((x + dirX, y + dirY))) {
                        minScore[(x + dirX, y + dirY)] = minScore[(x,y)] + 1001;
                        cont = true;
                    }
                    else if (minScore.ContainsKey((x + dirX, y + dirY))) {
                        if (minScore[(x + dirX, y + dirY)] >= minScore[(x,y)] + 1001) {
                            minScore[(x + dirX, y + dirY)] = minScore[(x,y)] + 1001;
                            cont = true;
                        }
                    }
                    if (grid[x + dirX, y + dirY] != 'E' && cont) {
                        int newRot = (rot + 1) % 4;
                        moveReindeer(grid, x + dirX, y + dirY, newRot, trials + 1);
                    }
                }
                if (rot == 0) {dirX = 0; dirY = 1;}
                if (rot == 1) {dirX = 1; dirY = 0;}
                if (rot == 2) {dirX = 0; dirY = -1;}
                if (rot == 3) {dirX = -1; dirY = 0;}
                if (grid[x + dirX, y + dirY] != '#') {
                    bool cont = false;
                    if (!minScore.ContainsKey((x + dirX, y + dirY))) {
                        minScore[(x + dirX, y + dirY)] = minScore[(x,y)] + 1001;
                        cont = true;
                    }
                    else if (minScore.ContainsKey((x + dirX, y + dirY))) {
                        if (minScore[(x + dirX, y + dirY)] >= minScore[(x,y)] + 1001) {
                            minScore[(x + dirX, y + dirY)] = minScore[(x,y)] + 1001;
                            cont = true;
                        }
                    }
                    if (grid[x + dirX, y + dirY] != 'E' && cont) {
                        int newRot = rot - 1;
                        if (newRot == -1) newRot = 3;
                        moveReindeer(grid, x + dirX, y + dirY, newRot, trials + 1);
                    }
                }
            }
        }

        private static char[,] bestPath(char[,] grid, int x, int y, int rot) {
            if (grid[x,y] != 'S') {
                grid[x,y] = 'O';
                int dirX = 0;
                int dirY = 0;
                if (rot == 0) {dirX = -1; dirY = 0;}
                if (rot == 1) {dirX = 0; dirY = 1;}
                if (rot == 2) {dirX = 1; dirY = 0;}
                if (rot == 3) {dirX = 0; dirY = -1;}
                if (grid[x + dirX, y + dirY] != '#') {
                    if (minScore[(x + dirX, y + dirY)] == minScore[(x,y)] - 1) {
                        bestPath(grid,x + dirX, y + dirY, rot);
                    }
                    if (minScore[(x + dirX, y + dirY)] == minScore[(x,y)] - 1001) {
                        bestPath(grid,x + dirX, y + dirY, rot);
                    }
                    if (minScore[(x + dirX, y + dirY)] == minScore[(x,y)] + 999) {
                        bestPath(grid,x + dirX, y + dirY, rot);
                    }
                }
                
                if (rot == 0) {dirX = 0; dirY = 1;}
                if (rot == 1) {dirX = 1; dirY = 0;}
                if (rot == 2) {dirX = 0; dirY = -1;}
                if (rot == 3) {dirX = -1; dirY = 0;}
                if (grid[x + dirX, y + dirY] != '#') {
                    int newRot = (rot + 1) % 4;
                    if (minScore[(x + dirX, y + dirY)] == minScore[(x,y)] - 1) {
                        bestPath(grid,x + dirX, y + dirY, newRot);
                    }
                    if (minScore[(x + dirX, y + dirY)] == minScore[(x,y)] - 1001) {
                        bestPath(grid,x + dirX, y + dirY, newRot);
                    }
                }


                if (rot == 0) {dirX = 0; dirY = -1;}
                if (rot == 1) {dirX = -1; dirY = 0;}
                if (rot == 2) {dirX = 0; dirY = 1;}
                if (rot == 3) {dirX = 1; dirY = 0;}
                if (grid[x + dirX, y + dirY] != '#') {
                    int newRot = rot - 1;
                    if (newRot == -1) newRot = 3;
                    if (minScore[(x + dirX, y + dirY)] == minScore[(x,y)] - 1) {
                        bestPath(grid,x + dirX, y + dirY, newRot);
                    }
                    if (minScore[(x + dirX, y + dirY)] == minScore[(x,y)] - 1001) {
                        bestPath(grid,x + dirX, y + dirY, newRot);
                    }
                }
            }
            else {
                grid[x,y] = 'O';
            }
            return grid;
        }
    }
}