using System.Security.Cryptography.X509Certificates;

namespace AdventofCodeDay14 {
    class Program {
            static int robotX = 0;
            static int robotY = 0;

        public static void Main() {
            Console.Clear();
            string[] rawGrid = File.ReadAllLines("gridInput.txt");
            string rawInput = File.ReadAllText("moveInput.txt");
            char[,] grid = new char[rawGrid[0].Length * 2,rawGrid.Length];
            for (int i = 0; i < rawGrid.Length; i++) {
                for (int j = 0; j < rawGrid[0].Length; j++) {
                    if (rawGrid[i][j] == '#') {
                        grid[2*j,i] = '#';
                        grid[2*j + 1,i] = '#';
                    }
                    if (rawGrid[i][j] == '.') {
                        grid[2*j,i] = '.';
                        grid[2*j + 1,i] = '.';
                    }
                    if (rawGrid[i][j] == '@') {
                        grid[2*j,i] = '@';
                        grid[2*j + 1,i] = '.';
                        robotX = 2*j;
                        robotY = i;
                    }
                    if (rawGrid[i][j] == 'O') {
                        grid[2*j,i] = '[';
                        grid[2*j + 1,i] = ']';
                    }
                }
            }
            foreach (char movement in rawInput) {
                if (movement == '<') {
                    moveObject(grid, robotX, robotY, -1, 0);
                }
                else if (movement == '>') {
                    moveObject(grid, robotX, robotY, 1, 0);
                }
                else if (movement == '^') {
                    moveObject(grid, robotX, robotY, 0, -1);
                }
                else if (movement == 'v') {
                    moveObject(grid, robotX, robotY, 0, 1);
                }
            }
            int result = 0;
            for (int i = 0; i < rawGrid.Length; i++) {
                for (int j = 0; j < rawGrid[0].Length * 2; j++) {
                    Console.Write(grid[j,i]);
                    if (grid[j,i] == '[') {
                        result += 100 * i + j;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Result: " + result);
        }

        private static (char[,],bool) moveObject(char[,] grid, int x, int y, int dirX, int dirY) {
            bool success = true;
            if (grid[x,y] == '@') {
                if (grid[x + dirX,y + dirY] == '.') {
                    grid[x + dirX,y + dirY] = '@';
                    grid[x,y] = '.';
                    robotX = x + dirX;
                    robotY = y + dirY;
                }
                else {
                    success = moveObject(grid, x + dirX, y + dirY, dirX, dirY).Item2;
                    if (success) grid = moveObject(grid,x,y,dirX,dirY).Item1;
                }
            }
            else if (grid[x,y] == '#') {
                success = false;
            }
            else if (grid[x,y] == '[') {
                if (dirX != 0) {
                    if (grid[x + dirX * 2,y] == '.') {
                        grid[x + dirX * 2,y] = ']';
                        grid[x + dirX,y] = '[';
                        grid[x,y] = '.';
                    }
                    else {
                        success = moveObject(grid, x + dirX * 2, y, dirX, dirY).Item2;
                        if (success) grid = moveObject(grid, x, y, dirX, dirY).Item1;
                    }
                }
                if (dirY != 0) {
                    if (grid[x,y + dirY] == '.' && grid[x + 1,y+ dirY] == '.') {
                        grid[x,y + dirY] = '[';
                        grid[x + 1,y + dirY] = ']';
                        grid[x,y] = '.';
                        grid[x + 1,y] = '.';
                    }
                    else {
                        if (grid[x,y + dirY] == '[') {
                            success = moveObject(grid, x, y + dirY, dirX, dirY).Item2;
                            if (success) grid = moveObject(grid, x, y, dirX, dirY).Item1;
                        }
                        else {
                            char[,] grid2 = (char[,])grid.Clone();
                            bool success1 = moveObject(grid2, x, y + dirY, dirX, dirY).Item2;
                            bool success2 = moveObject(grid2, x + 1, y + dirY, dirX, dirY).Item2;
                            if (success = success1 && success2) {
                                moveObject(grid, x, y + dirY, dirX, dirY);
                                moveObject(grid, x + 1, y + dirY, dirX, dirY);
                                grid = moveObject(grid, x, y, dirX, dirY).Item1;
                            }
                        }
                    }
                }
            }
            else if (grid[x,y] == ']') {
                if (dirX != 0) {
                    if (grid[x + dirX * 2,y] == '.') {
                        grid[x + dirX * 2,y] = '[';
                        grid[x + dirX,y] = ']';
                        grid[x,y] = '.';
                    }
                    else {
                        success = moveObject(grid, x + dirX * 2, y, dirX, dirY).Item2;
                        if (success) grid = moveObject(grid, x, y, dirX, dirY).Item1;
                    }
                }
                if (dirY != 0) {
                    if (grid[x,y + dirY] == '.' && grid[x - 1,y+ dirY] == '.') {
                        grid[x - 1,y + dirY] = '[';
                        grid[x,y + dirY] = ']';
                        grid[x,y] = '.';
                        grid[x - 1,y] = '.';
                    }
                    else {
                        if (grid[x,y + dirY] == ']') {
                            success = moveObject(grid, x, y + dirY, dirX, dirY).Item2;
                            if (success) grid = moveObject(grid, x, y, dirX, dirY).Item1;
                        }
                        else {
                            char[,] grid2 = (char[,])grid.Clone();
                            bool success1 = moveObject(grid2, x, y + dirY, dirX, dirY).Item2;
                            bool success2 = moveObject(grid2, x - 1, y + dirY, dirX, dirY).Item2;
                            if (success = success1 && success2) {
                                moveObject(grid, x, y + dirY, dirX, dirY);
                                moveObject(grid, x - 1, y + dirY, dirX, dirY);
                                grid = moveObject(grid, x, y, dirX, dirY).Item1;
                            }
                        }
                    }
                }
            }
            return (grid,success);
        }
    }
}