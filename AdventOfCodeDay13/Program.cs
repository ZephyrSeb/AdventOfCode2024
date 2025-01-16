using System.IO.Pipelines;

namespace AdventofCodeDay12 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            List<int> a1 = new List<int>();
            List<int> a2 = new List<int>();
            List<int> b1 = new List<int>();
            List<int> b2 = new List<int>();
            List<int> prize1 = new List<int>();
            List<int> prize2 = new List<int>();
            for (int i = 0; i < rawInput.Length; i++) {
                string[] temp = rawInput[i].Split(" ");
                a1.Add(Convert.ToInt32(temp[2].Remove(temp[2].Length-1,1).Remove(0,2)));
                a2.Add(Convert.ToInt32(temp[3].Remove(0,2)));
                temp = rawInput[i + 1].Split(" ");
                b1.Add(Convert.ToInt32(temp[2].Remove(temp[2].Length-1,1).Remove(0,2)));
                b2.Add(Convert.ToInt32(temp[3].Remove(0,2)));
                temp = rawInput[i + 2].Split(" ");
                prize1.Add(Convert.ToInt32(temp[1].Remove(temp[1].Length-1,1).Remove(0,2)));
                prize2.Add(Convert.ToInt32(temp[2].Remove(0,2)));
                i += 3;
            }
            long result = 0;
            for (int i = 0; i < a1.Count; i++) {
                (long,long) solution = SimultaneousEquation(a1[i],b1[i],a2[i],b2[i],prize1[i] + 10000000000000,prize2[i] + 10000000000000);
                if (solution != (0,0)) {
                    Console.WriteLine("(" + solution.Item1 + "," + solution.Item2 + ") = " + (3 * solution.Item1 + solution.Item2));
                }
                result += (3 * solution.Item1) + solution.Item2;
            }
            Console.WriteLine("Result: " + result);
        }

        private static (long,long) SimultaneousEquation(long x1, long x2, long y1, long y2, long sol1, long sol2) {
            long a = (y2 * sol1 - x2 * sol2) / (y2 * x1 - y1 * x2);
            long b = (y1 * sol1 - x1 * sol2) / (y1 * x2 - y2 * x1);
            if (a < 0 || b < 0 || (y2 * sol1 - x2 * sol2) % (y2 * x1 - y1 * x2) != 0 || (y1 * sol1 - x1 * sol2) % (y1 * x2 - y2 * x1) != 0) {
                return (0,0);
            }
            else return (a,b);
        }
    }
}