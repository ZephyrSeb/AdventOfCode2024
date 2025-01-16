using System.IO.Pipelines;
using System.Reflection;

namespace AdventofCodeDay5 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawRoute = File.ReadAllLines("testInput.txt");
            char[,] route = new char[rawRoute[0].Length,rawRoute.Length];
            int result = 0;
            for (int i = 0; i < rawRoute.Length; i++) {
                for (int j = 0; j < rawRoute[i].Length; j++) {
                    route[i,j] = rawRoute[i].ElementAt(j);
                }
            }
            int startX = 0;
            int startY = 0;
            for (int i = 0; i < rawRoute.Length; i++) {
                for (int j = 0; j < rawRoute[0].Length; j++) {
                    if (route[i,j] == '^') {
                        startX = i;
                        startY = j;
                    }
                }
            }
            for (int i = 0; i < rawRoute.Length; i++) {
                for (int j = 0; j < rawRoute[0].Length; j++) {
                    if (route[i,j] == '.') {
                        char[,] route2 = (char[,])route.Clone();
                        route2[i,j] = '#';
                        int temp = pathDistance(route2, startX, startY, rawRoute.Length, rawRoute[0].Length);
                        if (temp == -1) {
                            result += 1;
                        }
                    }
                }
            }
            Console.WriteLine("Result: " + result);
        }

        private static int pathDistance(char[,] input, int startX, int startY, int length, int width) {
            char[,] path = input;
            bool inMap = true;
            int locX = startX;
            int locY = startY;
            int timeout = 0;
            while (inMap && timeout < 10000) {
                if (path[locX,locY] == '<' && locY == 0) {
                    path[locX,locY] = 'X';
                    inMap = false;
                }
                else if (path[locX,locY] == '>' && locY == length - 1) {
                    path[locX,locY] = 'X';
                    inMap = false;
                }
                else if (path[locX,locY] == '^' && locX == 0) {
                    path[locX,locY] = 'X';
                    inMap = false;
                }
                else if (path[locX,locY] == 'v' && locX == width - 1) {
                    path[locX,locY] = 'X';
                    inMap = false;
                }
                else if (path[locX,locY] == '<' && path[locX,locY - 1] != '#') {
                    path[locX,locY] = 'X';
                    path[locX,locY - 1] = '<';
                    locY -= 1;
                }
                else if (path[locX,locY] == '>' && path[locX,locY + 1] != '#') {
                    path[locX,locY] = 'X';
                    path[locX,locY + 1] = '>';
                    locY += 1;
                    
                }
                else if (path[locX,locY] == '^' && path[locX - 1,locY] != '#') {
                    path[locX,locY] = 'X';
                    path[locX - 1,locY] = '^';
                    locX -= 1;
                    
                }
                else if (path[locX,locY] == 'v' && path[locX + 1,locY] != '#') {
                    path[locX,locY] = 'X';
                    path[locX + 1,locY] = 'v';
                    locX += 1;
                }
                else if (path[locX,locY] == '<' && path[locX,locY - 1] == '#') {
                    path[locX,locY] = '^';
                }
                else if (path[locX,locY] == 'v' && path[locX + 1,locY] == '#') {
                    path[locX,locY] = '<';
                }
                else if (path[locX,locY] == '>' && path[locX,locY + 1] == '#') {
                    path[locX,locY] = 'v';
                }
                else if (path[locX,locY] == '^' && path[locX - 1,locY] == '#') {
                    path[locX,locY] = '>';
                }
                timeout += 1;
            }
            if (inMap) {
                return -1;
            }
            else {
                int dist = 0;
                for (int k = 0; k < length; k++) {
                    for (int l = 0; l < width; l++) {
                        if (path[k,l] == 'X') {
                            dist += 1;
                        }
                    }
                }
                return dist;
            }
        }
    }
}