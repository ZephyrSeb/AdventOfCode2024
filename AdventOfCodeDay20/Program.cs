namespace AdventofCodeDay20 {
    class Program {
        static int startX;
        static int startY;
        static int goalX;
        static int goalY;
        static int currentX;
        static int currentY;
        static Dictionary<(int,int),int> minScore = new Dictionary<(int,int),int>();
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            char[,] grid = new char[rawInput[0].Length,rawInput.Length];
            for (int i = 0; i < rawInput.Length; i++) {
                for (int j = 0; j < rawInput.Length; j++) {
                    grid[j,i] = rawInput[i][j];
                    if (grid[j,i] == 'S') {startX = j; startY = i;}
                    if (grid[j,i] == 'E') {goalX = j; goalY = i;}
                }
            }
            minScore[(startX,startY)] = 0;
            currentX = startX;
            currentY = startY;
            while (currentX != goalX || currentY != goalY) {
                Move(grid,currentX,currentY,rawInput[0].Length,rawInput.Length);
            }
            int result = 0;
            int timeSave = 100;
            for (int i = 0; i < rawInput.Length; i++) {
                for (int j = 0; j < rawInput[0].Length; j++) {
                    result += cheatDistance(grid,j,i,rawInput[0].Length,rawInput.Length,timeSave);
                }
            }
            Console.WriteLine("Result: " + result);
        }

        private static void Move(char[,] grid, int x, int y, int width, int length) {
            if (x > 0) {
                if (grid[x-1,y] != '#') {
                    bool cont = false;
                    if (!minScore.ContainsKey((x - 1, y))) {
                            minScore[(x - 1, y)] = minScore[(x,y)] + 1;
                            cont = true;
                    }
                    if ((x != goalX || y != goalY) && cont) {
                        currentX--;
                    }
                }
            }
            if (x < width - 1) {
                if (grid[x+1,y] != '#') {
                    bool cont = false;
                    if (!minScore.ContainsKey((x + 1, y))) {
                            minScore[(x + 1, y)] = minScore[(x,y)] + 1;
                            cont = true;
                    }
                    if ((x != goalX || y != goalY) && cont) {
                        currentX++;
                    }
                }
            }
            if (y > 0) {
                if (grid[x,y-1] != '#') {
                    bool cont = false;
                    if (!minScore.ContainsKey((x, y - 1))) {
                            minScore[(x, y - 1)] = minScore[(x,y)] + 1;
                            cont = true;
                    }
                    if ((x != goalX || y != goalY) && cont) {
                        currentY--;
                    }
                }
            }
            if (y < length - 1) {
                if (grid[x,y+1] != '#') {
                    bool cont = false;
                    if (!minScore.ContainsKey((x, y + 1))) {
                            minScore[(x, y + 1)] = minScore[(x,y)] + 1;
                            cont = true;
                    }
                    if ((x != goalX || y != goalY) && cont) {
                        currentY++;
                    }
                }
            }
        }

        private static int cheatDistance(char[,] grid, int x, int y, int width, int length, int test) {
            int result = 0;
            if (grid[x,y] != '#' && x > 0 && x < width - 1 && y > 0 && y < length - 1) {
                for (int i = Math.Max(x-20,0); i <= Math.Min(x+20,width-1); i++) {
                    for (int j = Math.Max(y-20,0); j <= Math.Min(y+20,length-1); j++) {
                        if (Math.Abs(i - x) + Math.Abs(j - y) <= 20 && grid[i, j] != '#') {
                            int distance = minScore[(i, j)] - minScore[(x,y)] - (Math.Abs(i - x) + Math.Abs(j - y));
                            if (distance >= test) result++;
                        }
                    }
                }
                return result;
            }
            else return 0;
        }
    }
}