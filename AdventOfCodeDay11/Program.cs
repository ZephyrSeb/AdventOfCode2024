using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventofCodeDay11 {
    class Program {
        public static Dictionary<(long,int),long> cache = new Dictionary<(long,int),long>();
        public static void Main() {
            Console.Clear();
            string rawInput = File.ReadAllText("testInput.txt");
            string[] stones = rawInput.Split(" ");
            long result = 0;
            List<long> stoneList = new List<long>();
            int blinks = 75;
            for (int i = 0; i < stones.Length; i++) {
                stoneList.Add(Convert.ToInt64(stones[i]));
            }
            foreach (long stone in stoneList) {
                result += Blink2(stone, blinks);
            }
            //for (int i = 0; i < blinks; i++) {
            //    stoneList = Blink(stoneList);
            //}
            /*foreach (long stone in stoneList) {
                Console.Write(stone + " ");
            }*/
            //Console.WriteLine("Part 1: " + stoneList.ToArray().Length);
            Console.WriteLine("Result: " + result);
        }

        private static List<long> Blink(List<long> stones) {
            List<long> newStones = new List<long>();
            foreach (long stone in stones) {
                if (stone == 0) {
                    newStones.Add(1);
                }
                else if (stone.ToString().Length % 2 == 0) {
                    int length = stone.ToString().Length / 2;
                    newStones.Add(Convert.ToInt64(stone.ToString().Remove(length,length)));
                    newStones.Add(Convert.ToInt64(stone.ToString().Remove(0,length)));
                }
                else {
                    newStones.Add(stone * 2024);
                }
            }
            return newStones;
        }

        private static long Blink2(long stone, int iterations) {
            long ogStone = stone;
            if (cache.ContainsKey((stone,iterations))) {
                return cache[(stone,iterations)];
            }
            else {
                long result = 1;
                for (int i = iterations; i > 0; i--) {
                    if (stone == 0) {
                        stone = 1;
                    }
                    else if (Convert.ToString(stone).Length % 2 == 0) {
                        int length = Convert.ToString(stone).Length / 2;
                        if (i > 0) {
                            result += Blink2(Convert.ToInt64(Convert.ToString(stone).Remove(length,length)), i - 1);
                            result += Blink2(Convert.ToInt64(Convert.ToString(stone).Remove(0,length)), i - 1);
                            result -= 1;
                            i = 0;
                        }
                    }
                    else {
                        stone *= 2024;
                    }
                }
                cache[(ogStone,iterations)] = result;
                return result;
            }
        }
    }
}