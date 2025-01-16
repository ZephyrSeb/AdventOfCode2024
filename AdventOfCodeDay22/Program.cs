namespace AdventofCodeDay22 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            List<int> prices = new List<int>();
            Dictionary<(int, int, int, int), long> priceTracker = new Dictionary<(int, int, int, int), long>();
            List<(int, int, int, int)> usedKeys = new List<(int, int, int, int)>();
            long result = 0;
            for (int i = 0; i < rawInput.Length; i++) {
                prices.Clear();
                usedKeys.Clear();
                long secretNumber = Convert.ToInt64(rawInput[i]);
                int prevPrice = Trailing(secretNumber);
                for (int j = 0; j < 2000; j++) {
                    secretNumber = Modulo(XOR(secretNumber, secretNumber * 64), 16777216);
                    secretNumber = Modulo(XOR(secretNumber, secretNumber / 32), 16777216);
                    secretNumber = Modulo(XOR(secretNumber, secretNumber * 2048), 16777216);
                    prices.Add(Trailing(secretNumber) - prevPrice);
                    prevPrice = Trailing(secretNumber);
                    if (prices.Count == 4) {
                        if (priceTracker.ContainsKey((prices[0],prices[1],prices[2],prices[3])) && !usedKeys.Contains((prices[0],prices[1],prices[2],prices[3]))) {
                            priceTracker[(prices[0],prices[1],prices[2],prices[3])] += Trailing(secretNumber);
                        }
                        else if (!usedKeys.Contains((prices[0],prices[1],prices[2],prices[3]))) {
                            priceTracker[(prices[0],prices[1],prices[2],prices[3])] = Trailing(secretNumber);
                        }
                        //Console.WriteLine((prices[0],prices[1],prices[2],prices[3]));
                        //if (priceTracker.ContainsKey((-2,2,-1,-1)) && prices[0] == -2 && prices[1] == 2 && prices[2] == -1 && prices[3] == -1) {
                        //    Console.WriteLine(priceTracker[(-2,2,-1,-1)] + " " + j);
                        //}
                        usedKeys.Add((prices[0],prices[1],prices[2],prices[3]));
                        prices.RemoveAt(0);
                    }
                }
            }
            foreach (var key in priceTracker) {
                if (priceTracker[key.Key] > result) {
                    result = priceTracker[key.Key];
                    Console.WriteLine(key.Key);
                }
            }
            Console.WriteLine("Result: " + result);
        }

        private static long XOR(long a, long b) {
            string bitA = Convert.ToString(a,2);
            string bitB = Convert.ToString(b,2);
            string bitResult = "";
            while (bitA.Length != bitB.Length) {
                if (bitA.Length < bitB.Length) bitA = "0" + bitA;
                if (bitA.Length > bitB.Length) bitB = "0" + bitB;
            }
            for (int i = 0; i < bitA.Length; i++) {
                if (bitA[i] != bitB[i]) {bitResult += "1";}
                else {bitResult += "0";}
            }
            return Convert.ToInt64(bitResult, 2);
        }

        private static long Modulo(long a, long b) {
            return a % b;
        }

        private static int Trailing(long a) {
            string astring = Convert.ToString(a);
            return Convert.ToInt32(astring.Remove(0,astring.Length - 1));
        }
    }
}