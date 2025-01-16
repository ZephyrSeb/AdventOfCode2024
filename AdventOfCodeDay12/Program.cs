using System.Globalization;

namespace AdventofCodeDay12 {
    class Program {
        static Dictionary<(int,int), int> indexPairs = new Dictionary<(int,int), int>();
        static Dictionary<int, int> indexConnect = new Dictionary<int, int>();
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            char[,] rawGrid = new char[rawInput.Length,rawInput.Length];
            for (int i = 0; i < rawInput.Length; i++) {
                for (int j = 0; j < rawInput.Length; j++) {
                    rawGrid[i,j] = rawInput[i][j];
                }
            }
            int index = 1;
            indexConnect[index] = 0;
            for (int i = 0; i < rawInput.Length; i++) {
                for (int j = 0; j < rawInput.Length; j++) {
                    if (rawGrid[i,j] != '.') {
                        rawGrid = calculatePlot(rawGrid, i, j, rawInput.Length, index);
                        index ++;
                        indexConnect[index] = 0;
                    }
                }
            }
            
            for (int i = 0; i < rawInput.Length; i++) {
                for (int j = 0; j < rawInput.Length; j++) {
                    Console.Write(indexPairs[(i,j)]);
                }
                Console.WriteLine(" ");
            }
            int result = 0;
            for (int i = 1; i < index; i++) {
                Console.WriteLine("index: " + i + ";" + findArea(i,rawInput.Length) + "," + findPerimeter(i,rawInput.Length));
                result += findArea(i,rawInput.Length) * findPerimeter(i,rawInput.Length);
            }
            Console.WriteLine("Result: " + result);
        }

        public static char[,] calculatePlot(char[,] grid, int i, int j, int length, int index) {
            indexPairs[(i,j)] = index;
            char self = grid[i,j];
            char topLeft;
            char top;
            char topRight;
            char left;
            char right;
            char bottomLeft;
            char bottom;
            char bottomRight;
            if (i > 0 && j > 0) {topLeft = grid[i - 1, j - 1];} else topLeft = '.';
            if (j > 0) {top = grid[i, j - 1];} else top = '.';
            if (i < length - 1 && j > 0) {topRight = grid[i + 1, j - 1];} else topRight = '.';
            if (i > 0) {left = grid[i - 1, j];} else left = '.';
            if (i < length - 1) {right = grid[i + 1, j];} else right = '.';
            if (i > 0 && j < length - 1) {bottomLeft = grid[i - 1, j + 1];} else bottomLeft = '.';
            if (j < length - 1) {bottom = grid[i, j + 1];} else bottom = '.';
            if (i < length - 1 && j < length - 1) {bottomRight = grid[i + 1, j + 1];} else bottomRight = '.';
            if (grid[i,j] == topLeft && grid[i,j] == top && grid[i,j] == left) {
                indexConnect[index]++;
            }
            if (grid[i,j] == topRight && grid[i,j] == top && grid[i,j] == right) {
                indexConnect[index]++;
            }
            if (grid[i,j] == bottomLeft && grid[i,j] == bottom && grid[i,j] == left) {
                indexConnect[index]++;
            }
            if (grid[i,j] == bottomRight && grid[i,j] == bottom && grid[i,j] == right) {
                indexConnect[index]++;
            }
            grid[i,j] = '.';
            if (i > 0) {
                if (!indexPairs.ContainsKey((i - 1,j)) && grid[i - 1,j] == self) {
                    grid = calculatePlot(grid,i - 1,j,length,index);
                }
            }
            if (j > 0) {
                if (!indexPairs.ContainsKey((i,j - 1)) && grid[i,j - 1] == self) {
                    grid = calculatePlot(grid,i,j - 1,length,index);
                }}
            if (i < length - 1) {
                if (!indexPairs.ContainsKey((i + 1,j)) && grid[i + 1,j] == self) {
                    grid = calculatePlot(grid,i + 1,j,length,index);
                }}
            if (j < length - 1) {
                if (!indexPairs.ContainsKey((i,j + 1)) && grid[i,j + 1] == self) {
                    grid = calculatePlot(grid,i,j + 1,length,index);
                }}
            return grid;
        }

        public static int findArea(int index, int length) {
            int count = 0;
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < length; j++) {
                    if (indexPairs[(i,j)] == index) {
                        count++;
                    }
                }
            }
            return count;
        }

        public static int findPerimeter(int index, int length) {
            int result = 0;
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < length; j++) {
                    int[,] count = {{0,0,0}, {0,0,0}, {0,0,0}};
                    if (indexPairs[(i,j)] == index) {
                        if (indexPairs.ContainsKey((i - 1,j - 1))) if (indexPairs[(i - 1,j - 1)] == index) count[0,0] = 1;
                        if (indexPairs.ContainsKey((i,j - 1))) if (indexPairs[(i,j - 1)] == index) count[1,0] = 1;
                        if (indexPairs.ContainsKey((i + 1,j - 1))) if (indexPairs[(i + 1,j - 1)] == index) count[2,0] = 1;
                        if (indexPairs.ContainsKey((i - 1,j))) if (indexPairs[(i - 1,j)] == index) count[0,1] = 1;
                        if (indexPairs.ContainsKey((i + 1,j))) if (indexPairs[(i + 1,j)] == index) count[2,1] = 1;
                        if (indexPairs.ContainsKey((i - 1,j + 1))) if (indexPairs[(i - 1,j + 1)] == index) count[0,2] = 1;
                        if (indexPairs.ContainsKey((i,j + 1))) if (indexPairs[(i,j + 1)] == index) count[1,2] = 1;
                        if (indexPairs.ContainsKey((i + 1,j + 1))) if (indexPairs[(i + 1,j + 1)] == index) count[2,2] = 1;
                        if (count[1,0] == 0 && (count[0,1] == 0 || count [0,0] == 1)) {
                            result++;
                        }
                        if (count[1,0] == 0 && (count [2,1] == 0 || count [2,0] == 1)) {
                            result++;
                        }
                        if (count[1,2] == 0 && (count [0,1] == 0 || count [0,2] == 1)) {
                            result++;
                        }
                        if (count[1,2] == 0 && (count [2,1] == 0 || count [2,2] == 1)) {
                            result++;
                        }
                    }
                }
            }
            return result;
        }
    }
}