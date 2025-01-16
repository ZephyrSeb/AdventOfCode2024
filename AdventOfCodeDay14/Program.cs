namespace AdventofCodeDay14 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            int gridWidth = 101;
            int gridHeight = 103;
            int second = 6532;
            //int result = 0;
            //while (second < 105) {
                List<(int,int)> gridCoords = new List<(int,int)>();
                //result = 0;
                for (int i = 0; i < rawInput.Length; i++) {
                    string[] temp = rawInput[i].Split(" ");
                    string[] pos = temp[0].Remove(0,2).Split(",");
                    string[] vel = temp[1].Remove(0,2).Split(",");
                    int x = Convert.ToInt32(pos[0]) + (Convert.ToInt32(vel[0]) * second);
                    int y = Convert.ToInt32(pos[1]) + (Convert.ToInt32(vel[1]) * second);
                    x += gridWidth * second;
                    y += gridHeight * second;
                    x = x % gridWidth;
                    y = y % gridHeight;
                    gridCoords.Add((x,y));
                }
                /*double xs = 0;
                double ys = 0;
                double xt = 0;
                double yt = 0;
                foreach ((int,int) coord in gridCoords) {
                    xs += coord.Item1;
                    ys += coord.Item2;
                    xt += Math.Pow(coord.Item1,2);
                    yt += Math.Pow(coord.Item2,2);
                }
                xs /= rawInput.Length;
                ys /= rawInput.Length;
                xt /= rawInput.Length;
                yt /= rawInput.Length;
                xs = Math.Pow(xs,2) - xt;
                ys = Math.Pow(ys,2) - yt;
                if (Math.Abs(xs) < 400) Console.WriteLine("x = " + second);
                if (Math.Abs(ys) < 400) Console.WriteLine("y = " + second);
                second++;*/
            //}
            for (int i = 0; i < 101; i++) {
                for (int j = 0; j < 103; j++) {
                    if (gridCoords.Contains((j,i))) Console.Write("#");
                    else Console.Write(".");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Result: ");
        }
    }
}