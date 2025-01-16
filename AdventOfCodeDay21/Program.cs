namespace AdventofCodeDay21 {
    class Program {
        static Dictionary<(char, char, long), long> cache = new Dictionary<(char, char, long), long>();
        public static void Main() {
            Console.Clear();
            string[] rawInput = File.ReadAllLines("testInput.txt");
            Dictionary<(int, int), char> numpad = new Dictionary<(int, int), char>();
            numpad[(0,0)] = '7'; numpad[(1,0)] = '8'; numpad[(2,0)] = '9';
            numpad[(0,1)] = '4'; numpad[(1,1)] = '5'; numpad[(2,1)] = '6';
            numpad[(0,2)] = '1'; numpad[(1,2)] = '2'; numpad[(2,2)] = '3';
            numpad[(0,3)] = ' '; numpad[(1,3)] = '0'; numpad[(2,3)] = 'A';
            Dictionary<(int, int), char> dirpad = new Dictionary<(int, int), char>();
            dirpad[(0,0)] = ' '; dirpad[(1,0)] = '^'; dirpad[(2,0)] = 'A';
            dirpad[(0,1)] = '<'; dirpad[(1,1)] = 'v'; dirpad[(2,1)] = '>';
            List<string> output = new List<string>();
            long result = 0;
            foreach (string code in rawInput) {
                output.Add(UseKeypad(numpad, code, 2, 3, false));
            }
            int trials = 25;
            for (int i = 0; i < rawInput.Length; i++) {
                long subResult = 0;
                for (int j = 0; j < output[i].Length; j++) {
                    if (j == 0) subResult += Evaluate(dirpad, output[i][j], trials, 'A');
                    if (j > 0) {subResult += Evaluate(dirpad, output[i][j], trials, output[i][j-1]);}
                }
                Console.WriteLine(subResult + " * " + Convert.ToInt32(rawInput[i].Remove(rawInput[i].Length - 1, 1)));
                result += subResult * Convert.ToInt32(rawInput[i].Remove(rawInput[i].Length - 1, 1));
            }
            /*foreach (string code in rawInput) {
                output.Add(UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(dirpad, UseKeypad(numpad, code, 2, 3, false), 2, 0, true), 2, 0, true), 2, 0, true), 2, 0, true), 2, 0, true), 2, 0, true), 2, 0, true), 2, 0, true), 2, 0, true), 2, 0, true));
            }
            for (int i = 0; i < rawInput.Length; i++) {
                Console.WriteLine(output[i].Length + " * " + Convert.ToInt32(rawInput[i].Remove(rawInput[i].Length - 1, 1)));
                result += output[i].Length * Convert.ToInt32(rawInput[i].Remove(rawInput[i].Length - 1, 1));
            }*/
            //Console.WriteLine(output[0]);
            Console.WriteLine("Result: " + result);
        }

        private static string UseKeypad(Dictionary<(int,int), char> grid, string input, int x, int y, bool reverse) {
            string result = "";
            //Console.WriteLine(input);
            Dictionary<char, (int,int)> reverseGrid = grid.ToDictionary(x => x.Value, y => y.Key);
            foreach (char letter in input) {
                int targetX = reverseGrid[letter].Item1;
                int targetY = reverseGrid[letter].Item2;
                int x1 = x;
                int y1 = y;
                if (!reverse && ((x != 0 && y != 3) || (targetX != 0 && targetY != 3))) {
                    if (x - targetX > 0) {
                        for (int i = 0; i < x - targetX; i++) {
                            result += '<';
                            x1--;
                        }
                    }
                    if (y - targetY < 0) {
                        for (int i = 0; i < targetY - y; i++) {
                            result += 'v';
                            y1++;
                        }
                    }
                    if (y - targetY > 0) {
                        for (int i = 0; i < y - targetY; i++) {
                            result += '^';
                            y1--;
                        }
                    }
                    if (x - targetX < 0) {
                        for (int i = 0; i < targetX - x; i++) {
                            result += '>';
                            x1++;
                        }
                    }
                }
                else if (!reverse) {
                    if (x - targetX < 0) {
                        for (int i = 0; i < targetX - x; i++) {
                            result += '>';
                            x1++;
                        }
                    }
                    if (y - targetY > 0) {
                        for (int i = 0; i < y - targetY; i++) {
                            result += '^';
                            y1--;
                        }
                    }
                    if (y - targetY < 0) {
                        for (int i = 0; i < targetY - y; i++) {
                            result += 'v';
                            y1++;
                        }
                    }
                    if (x - targetX > 0) {
                        for (int i = 0; i < x - targetX; i++) {
                            result += '<';
                            x1--;
                        }
                    }
                }
                if (reverse && ((x != 0 && y != 0) || (targetX != 0 && targetY != 0))) {
                    if (x - targetX > 0) {
                        for (int i = 0; i < x - targetX; i++) {
                            result += '<';
                            x1--;
                        }
                    }
                    if (y - targetY > 0) {
                        for (int i = 0; i < y - targetY; i++) {
                            result += '^';
                            y1--;
                        }
                    }
                    if (y - targetY < 0) {
                        for (int i = 0; i < targetY - y; i++) {
                            result += 'v';
                            y1++;
                        }
                    }
                    if (x - targetX < 0) {
                        for (int i = 0; i < targetX - x; i++) {
                            result += '>';
                            x1++;
                        }
                    }
                }
                else if (reverse) {
                    if (x - targetX < 0) {
                        for (int i = 0; i < targetX - x; i++) {
                            result += '>';
                            x1++;
                        }
                    }
                    if (y - targetY > 0) {
                        for (int i = 0; i < y - targetY; i++) {
                            result += '^';
                            y1--;
                        }
                    }
                    if (y - targetY < 0) {
                        for (int i = 0; i < targetY - y; i++) {
                            result += 'v';
                            y1++;
                        }
                    }
                    if (x - targetX > 0) {
                        for (int i = 0; i < x - targetX; i++) {
                            result += '<';
                            x1--;
                        }
                    }
                }
                x = x1;
                y = y1;
                result += 'A';
            }
            return result;
        }

        private static long Evaluate(Dictionary<(int,int), char> grid, char move, int iterations, char start) {
            long result = 1;
            Dictionary<char, (int,int)> reverseGrid = grid.ToDictionary(x => x.Value, y => y.Key);
            int x = reverseGrid[start].Item1;
            int y = reverseGrid[start].Item2;
            if (iterations > 0) {
                if (cache.ContainsKey((start, move, iterations))) {
                    return cache[(start, move, iterations)];
                }
                else {
                    result = 0;
                    string iterative = UseKeypad(grid, move.ToString(), x, y, true);
                    for (int i = 0; i < iterative.Length; i++) {
                        if (i == 0) result += Evaluate(grid, iterative[i], iterations - 1, 'A');
                        else result += Evaluate(grid, iterative[i], iterations - 1, iterative[i - 1]);
                    }
                    cache[(start, move, iterations)] = result;
                }
            }
            return result;
        }
    }
}