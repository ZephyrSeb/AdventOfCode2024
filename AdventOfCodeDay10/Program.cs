using System.IO.Pipelines;
using System.Security.Cryptography.X509Certificates;

namespace AdventofCodeDay9 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawGrid = File.ReadAllLines("testInput.txt");
            int[,] topGrid = new int[rawGrid.Length,rawGrid[0].Length];
            int[,] topTrail = new int[rawGrid.Length,rawGrid[0].Length];
            for (int i = 0; i < rawGrid[0].Length; i++) {
                for (int j = 0; j < rawGrid.Length; j++) {
                    topGrid[i,j] = Convert.ToInt32(Convert.ToString(rawGrid[i][j]));
                    Console.Write(topGrid[i,j]);
                }
                Console.WriteLine("");
            }
            int result = 0;
            for (int i = 0; i < rawGrid[0].Length; i++) {
                for (int j = 0; j < rawGrid.Length; j++) {
                    if (topGrid[i,j] == 0) {
                        result += GetTrail(topGrid, i, j,rawGrid.Length).ToArray().Length;
                    }
                }
            }
            Console.WriteLine("Result: " + result);
        }

        private static List<(int,int)> GetTrail(int[,] trail, int x, int y, int length) {
            List<(int,int)> result = new List<(int,int)>();
            if (trail[x,y] < 9) {
                if (x > 0) {
                    if (trail[x - 1,y] == trail[x,y] + 1) {
                        result.AddRange(GetTrail(trail, x - 1, y, length));
                    }
                }
                if (y > 0) {
                    if (trail[x,y - 1] == trail[x,y] + 1) {
                        result.AddRange(GetTrail(trail, x, y - 1, length));
                    }
                }
                if (x < length - 1) {
                    if (trail[x + 1,y] == trail[x,y] + 1) {
                        result.AddRange(GetTrail(trail, x + 1, y, length));
                    }
                }
                if (y < length - 1) {
                    if (trail[x,y + 1] == trail[x,y] + 1) {
                        result.AddRange(GetTrail(trail, x, y + 1, length));
                    }
                }
            }
            if (trail[x,y] == 9) {
                result.Add((x, y));
            }

            return result;
        }
    }
}