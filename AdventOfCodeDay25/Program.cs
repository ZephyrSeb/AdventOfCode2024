namespace AdventofCodeDay24 {
    class Program {
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            List<int[]> locks = new List<int[]>();
            List<int[]> keys = new List<int[]>();
            for (int i = 0; i < rawInput.Length; i += 8) {
                if (rawInput[i][0] == '#') {
                    int[] templist = new int[5];
                    for (int j = 0; j < 5; j++) {
                        int temp = 0;
                        for (int k = 0; k < 7; k++) {
                            if (rawInput[i + k][j] == '.') {
                                templist[j] = k - 1;
                                break;
                            }
                        }
                    }
                locks.Add(templist);
                }
                else {
                    int[] templist = new int[5];
                    for (int j = 0; j < 5; j++) {
                        int temp = 0;
                        for (int k = 0; k < 7; k++) {
                            if (rawInput[i + k][j] == '#') {
                                templist[j] = 6 - k;
                                break;
                            }
                        }
                    }
                keys.Add(templist);
                }
            }
            int result = 0;
            foreach (int[] key in keys) {
                foreach (int[] _lock in locks) {
                    int count = 0;
                    for (int i = 0; i < 5; i++) {
                        if (key[i] + _lock[i] <= 5) {
                            count++;
                        }
                    }
                    if (count == 5) result++;
                }
            }
            Console.WriteLine(keys.Count);
            Console.WriteLine(locks.Count);
            Console.WriteLine("Result: " + result);
        }
    }
}