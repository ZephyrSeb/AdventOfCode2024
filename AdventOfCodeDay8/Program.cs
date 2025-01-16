namespace AdventofCodeDay8 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawGrid = File.ReadAllLines("testInput.txt");
            char[,] grid = new char[rawGrid[0].Length,rawGrid.Length];
            char[,] antinodes = new char[rawGrid[0].Length,rawGrid.Length];
            List<char> nodes = new List<char>();
            int result = 0;
            for (int i = 0; i < rawGrid.Length; i++) {
                for (int j = 0; j < rawGrid[i].Length; j++) {
                    grid[i,j] = rawGrid[i].ElementAt(j);
                    if (!nodes.Contains(rawGrid[i].ElementAt(j)) && rawGrid[i].ElementAt(j) != '#' && rawGrid[i].ElementAt(j) != '.') {
                        nodes.Add(rawGrid[i].ElementAt(j));
                    }
                }
            }
            foreach (char node in nodes) {
                antinodes = FindAntiNodes(grid,node,antinodes, rawGrid.Length, rawGrid[0].Length);
            }
            for (int i = 0; i < rawGrid.Length; i++) {
                for (int j = 0; j < rawGrid[i].Length; j++) {
                    if (antinodes[i,j] == '#') {
                        result += 1;
                    }
                }
            }
            Console.WriteLine("Result: " + result);
        }

        private static char[,] FindAntiNodes(char[,] grid, char node, char[,] nodeChart, int length, int width) {
            for(int i = 0; i < length; i++) {
                for (int j = 0; j < width; j++) {
                    if (grid[i,j] == node) {
                        for (int k = 0; k < length; k++) {
                            for (int l = 0; l < width; l++) {
                                if ((i != k || j != l) && grid[i,j] == grid[k,l]) {
                                    int xdiff = i - k;
                                    int ydiff = j - l;
                                    while (i + xdiff >= 0 && j + ydiff >= 0 && i + xdiff < length && j + ydiff < width) {
                                        nodeChart[i + xdiff,j + ydiff] = '#';
                                        xdiff += i - k;
                                        ydiff += j - l;
                                    }
                                }
                                if (i == k && j == l && NodeCount(grid,node,length,width) > 1) {
                                    nodeChart[i,j] = '#';
                                }
                            }
                        }
                    }
                }
            }
            return nodeChart;
        }

        private static int NodeCount(char[,] grid, char node, int length, int width) {
            int count = 0;
            for(int i = 0; i < length; i++) {
                for (int j = 0; j < width; j++) {
                    if (grid[i,j] == node) {
                        count += 1;
                    }
                }
            }
            return count;
        }
    }
}