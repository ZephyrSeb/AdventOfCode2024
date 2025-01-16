namespace AdventofCodeDay18 {
    class Program {
        static Dictionary<string, long> towelCache = new Dictionary<string, long>();
        public static void Main() {
            Console.Clear();
            string towelInput = File.ReadAllText("towelInput.txt");
            string[] patternInput = File.ReadAllLines("patternInput.txt");
            string[] towels = towelInput.Split(", ");
            List<string> smartTowels = new List<string>();
            for (int i = 0; i < towels.Length; i++) {
                string[] modifiedTowels = (string[])towels.Clone();
                modifiedTowels[i] = "x";
                if (!CanMakePattern(modifiedTowels, towels[i])) {
                    smartTowels.Add(towels[i]);
                }
            }
            long result = 0;
            for (int i = 0; i < patternInput.Length; i++) {
                if (CanMakePattern(smartTowels.ToArray(),patternInput[i])) {
                    result += PatternNumber(towels, patternInput[i]);
                }
                Console.WriteLine(result);
            }
            Console.WriteLine("Result: " + result);
        }

        private static bool CanMakePattern(string[] input, string pattern) {
            bool success = false;
            foreach (string towel in input) {
                if (pattern.Length >= towel.Length) {
                    if (towel == pattern.Substring(0,towel.Length)) {
                        string newPattern = (string)pattern.Clone();
                        newPattern = newPattern.Remove(0, towel.Length);
                        if (newPattern.Length > 0) {
                            bool patternLength = CanMakePattern(input, newPattern);
                            if (patternLength) success = true;
                        }
                        else success = true;
                    }
                }
                if (success) return true;
            }
            return success;
        }

        private static long PatternNumber(string[] input, string pattern) {
            if (towelCache.ContainsKey(pattern)) {
                return towelCache[pattern];
            }
            else {
                long temp = 0;
                foreach (string towel in input) {
                    if (pattern.Length >= towel.Length) {
                        if (towel == pattern.Substring(0,towel.Length)) {
                        string newPattern = (string)pattern.Clone();
                        newPattern = newPattern.Remove(0, towel.Length);
                        if (newPattern.Length > 0) {
                            temp += PatternNumber(input, newPattern);
                        }
                        else temp++;
                        }
                    }

                }
                towelCache[pattern] = temp;
                return temp;
            }
        }
    }
}